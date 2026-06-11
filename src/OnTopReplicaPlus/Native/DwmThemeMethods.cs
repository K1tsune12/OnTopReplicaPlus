using System;
using System.Runtime.InteropServices;

namespace OnTopReplica.Native {

    /// <summary>
    /// DWM helpers used to switch a window's title bar between light and dark.
    /// </summary>
    static class DwmThemeMethods {

        //Attribute id that toggles the immersive dark mode title bar.
        //20 on Windows 10 build 18985+ and Windows 11; 19 on earlier Windows 10 builds.
        private const int DWMWA_USE_IMMERSIVE_DARK_MODE = 20;
        private const int DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20H1 = 19;

        [DllImport("dwmapi.dll", PreserveSig = true)]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        /// <summary>
        /// Requests a dark or light title bar for the given window. Best-effort: does
        /// nothing on Windows versions that do not support the attribute.
        /// </summary>
        public static void SetImmersiveDarkMode(IntPtr hwnd, bool enabled) {
            if (hwnd == IntPtr.Zero)
                return;

            int value = enabled ? 1 : 0;
            //Try the modern attribute first, fall back to the pre-20H1 one.
            if (DwmSetWindowAttribute(hwnd, DWMWA_USE_IMMERSIVE_DARK_MODE, ref value, sizeof(int)) != 0) {
                DwmSetWindowAttribute(hwnd, DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20H1, ref value, sizeof(int));
            }
        }

    }

}
