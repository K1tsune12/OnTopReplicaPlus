using System;
using OnTopReplica.Native;

namespace OnTopReplica.AntiBrowserOcclusion {

    /// <summary>
    /// Creates and owns a hidden window whose class name is "MultitaskingViewFrame".
    /// </summary>
    /// <remarks>
    /// Chromium (and other browsers) treat the appearance of a window of this class
    /// (or "TaskListThumbnailWnd") as the Windows Task View / alt-tab thumbnails being
    /// shown, which suppresses occlusion-based throttling. By owning such a window and
    /// raising a SHOW event for it, we keep cloned browser windows rendering their video.
    /// See: https://chromium.googlesource.com/chromium/src/+/refs/heads/main/ui/aura/native_window_occlusion_tracker_win.cc
    /// </remarks>
    internal static class FakeMultitaskingViewFrame {

        private const string WindowClassName = "MultitaskingViewFrame";

        //Kept alive for the whole process so the native side never holds a dangling pointer.
        private static readonly AntiOcclusionMethods.WndProcDelegate WndProcHandler = WndProc;

        private static bool _initialized;
        private static IntPtr _hwnd = IntPtr.Zero;

        public static IntPtr GetHandle() {
            if (!_initialized) {
                RegisterWindowClass();
                _hwnd = CreateWindow();
                _initialized = true;
            }
            return _hwnd;
        }

        private static IntPtr CreateWindow() {
            return AntiOcclusionMethods.CreateWindowExW(
                0,
                WindowClassName,
                typeof(FakeMultitaskingViewFrame).FullName,
                AntiOcclusionMethods.WS_OVERLAPPED,
                0, 0, 0, 0,
                IntPtr.Zero,
                IntPtr.Zero,
                AntiOcclusionMethods.GetModuleHandleW(null),
                IntPtr.Zero);
        }

        private static void RegisterWindowClass() {
            var wndClass = new AntiOcclusionMethods.WNDCLASS {
                lpszClassName = WindowClassName,
                lpfnWndProc = WndProcHandler,
                hInstance = AntiOcclusionMethods.GetModuleHandleW(null)
            };

            //Return value is ignored: a zero result simply means the class already exists.
            AntiOcclusionMethods.RegisterClassW(ref wndClass);
        }

        private static IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam) {
            return AntiOcclusionMethods.DefWindowProcW(hWnd, msg, wParam, lParam);
        }

    }

}
