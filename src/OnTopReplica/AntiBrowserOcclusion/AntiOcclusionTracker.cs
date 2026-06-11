using System;
using System.Text;
using OnTopReplica.Native;

namespace OnTopReplica.AntiBrowserOcclusion {

    /// <summary>
    /// Keeps cloned browser windows rendering video by defeating Windows/Chromium
    /// "native window occlusion" detection while a thumbnail is shown.
    /// </summary>
    /// <remarks>
    /// All operations are best-effort: any failure is logged and swallowed, since
    /// anti-occlusion must never prevent normal cloning from working.
    /// </remarks>
    public static class AntiOcclusionTracker {

        //Kept alive for the whole process so the native hook never holds a dangling pointer.
        private static readonly AntiOcclusionMethods.WinEventDelegate WinEventHandler = WinEventProc;

        private static IntPtr _hook = IntPtr.Zero;
        private static bool _showingThumbnails;

        /// <summary>
        /// Raises a SHOW event for our fake task-view window, which makes browsers
        /// stop treating their (cloned) window as occluded.
        /// </summary>
        public static void PerformAntiOcclusion() {
            AntiOcclusionMethods.NotifyWinEvent(
                AntiOcclusionMethods.EVENT_OBJECT_SHOW,
                FakeMultitaskingViewFrame.GetHandle(),
                AntiOcclusionMethods.OBJID_WINDOW,
                0);
        }

        /// <summary>
        /// Starts anti-occlusion tracking. Safe to call repeatedly.
        /// </summary>
        public static void Start() {
            try {
                Stop();

                PerformAntiOcclusion();

                _hook = AntiOcclusionMethods.SetWinEventHook(
                    AntiOcclusionMethods.EVENT_OBJECT_SHOW,
                    AntiOcclusionMethods.EVENT_OBJECT_HIDE,
                    AntiOcclusionMethods.GetModuleHandleW(null),
                    WinEventHandler,
                    0, 0, 0);
            }
            catch (Exception ex) {
                Log.WriteException("Failed to start anti-occlusion tracking", ex);
            }
        }

        /// <summary>
        /// Stops anti-occlusion tracking. Safe to call repeatedly.
        /// </summary>
        public static void Stop() {
            try {
                if (_hook != IntPtr.Zero) {
                    AntiOcclusionMethods.UnhookWinEvent(_hook);
                    _hook = IntPtr.Zero;
                }
            }
            catch (Exception ex) {
                Log.WriteException("Failed to stop anti-occlusion tracking", ex);
            }
        }

        private static void WinEventProc(
            IntPtr hWinEventHook, uint eventType, IntPtr hwnd,
            int idObject, int idChild, uint idEventThread, uint dwmsEventTime) {

            //Ignore events not tied to an actual window object (e.g. carets, mouse moves).
            if (hwnd == IntPtr.Zero || idObject != AntiOcclusionMethods.OBJID_WINDOW)
                return;

            if (eventType == AntiOcclusionMethods.EVENT_OBJECT_SHOW) {
                if (_showingThumbnails)
                    return;

                //Never react to our own fake window.
                if (hwnd == FakeMultitaskingViewFrame.GetHandle())
                    return;

                var className = GetWindowClassName(hwnd);
                if (className == "MultitaskingViewFrame" || className == "TaskListThumbnailWnd")
                    _showingThumbnails = true;

                return;
            }

            if (eventType == AntiOcclusionMethods.EVENT_OBJECT_HIDE) {
                if (!_showingThumbnails)
                    return;

                var className = GetWindowClassName(hwnd);
                if (className == "MultitaskingViewFrame" || className == "TaskListThumbnailWnd") {
                    _showingThumbnails = false;

                    //Real task-view thumbnails were dismissed: re-assert our fake one.
                    PerformAntiOcclusion();
                }
            }
        }

        private static string GetWindowClassName(IntPtr hwnd) {
            //Maximum class name length is 256 characters.
            var buffer = new StringBuilder(257);
            AntiOcclusionMethods.GetClassName(hwnd, buffer, buffer.Capacity);
            return buffer.ToString();
        }

    }

}
