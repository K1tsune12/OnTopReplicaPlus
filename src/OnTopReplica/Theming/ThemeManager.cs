using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Win32;
using OnTopReplica.Native;
using OnTopReplica.Properties;

namespace OnTopReplica.Theming {

    /// <summary>
    /// Centralized light/dark theming for the whole application.
    /// </summary>
    /// <remarks>
    /// The preference is stored in <see cref="Settings.Theme"/> as "System",
    /// "Light" or "Dark". "System" follows the current Windows apps theme.
    /// All colours live in <see cref="Palette"/> so they are easy to tweak.
    /// </remarks>
    static class ThemeManager {

        /// <summary>Raised whenever the effective theme changes and views must re-apply it.</summary>
        public static event EventHandler ThemeChanged;

        /// <summary>
        /// Gets the current preference ("System", "Light" or "Dark").
        /// </summary>
        public static string Preference {
            get {
                var pref = Settings.Default.Theme;
                return string.IsNullOrEmpty(pref) ? "System" : pref;
            }
        }

        /// <summary>
        /// Sets the theme preference, persists it and notifies listeners.
        /// </summary>
        public static void SetPreference(string preference) {
            Settings.Default.Theme = preference;
            RaiseThemeChanged();
        }

        public static void RaiseThemeChanged() {
            var handler = ThemeChanged;
            if (handler != null)
                handler(null, EventArgs.Empty);
        }

        /// <summary>
        /// Gets whether the effective theme is dark, resolving "System" against Windows.
        /// </summary>
        public static bool IsDark {
            get {
                switch (Preference) {
                    case "Dark": return true;
                    case "Light": return false;
                    default: return IsWindowsInDarkMode();
                }
            }
        }

        private static bool IsWindowsInDarkMode() {
            try {
                using (var key = Registry.CurrentUser.OpenSubKey(
                    @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize")) {
                    if (key != null) {
                        var value = key.GetValue("AppsUseLightTheme");
                        if (value is int i)
                            return i == 0;
                    }
                }
            }
            catch (Exception ex) {
                Log.WriteException("Unable to read Windows theme preference", ex);
            }
            return false;
        }

        #region Palette

        public static Color WindowBackground { get { return IsDark ? Color.FromArgb(32, 32, 32) : SystemColors.Control; } }
        public static Color SurfaceBackground { get { return IsDark ? Color.FromArgb(43, 43, 43) : SystemColors.Control; } }
        public static Color InputBackground { get { return IsDark ? Color.FromArgb(45, 45, 48) : SystemColors.Window; } }
        public static Color TextColor { get { return IsDark ? Color.FromArgb(240, 240, 240) : SystemColors.ControlText; } }
        public static Color InputText { get { return IsDark ? Color.FromArgb(240, 240, 240) : SystemColors.WindowText; } }
        public static Color BorderColor { get { return IsDark ? Color.FromArgb(64, 64, 64) : SystemColors.ControlDark; } }
        public static Color AccentColor { get { return Color.FromArgb(0, 120, 215); } }
        public static Color MenuBackground { get { return IsDark ? Color.FromArgb(43, 43, 43) : SystemColors.Menu; } }
        public static Color MenuText { get { return IsDark ? Color.FromArgb(240, 240, 240) : SystemColors.MenuText; } }
        public static Color MenuHighlight { get { return IsDark ? Color.FromArgb(64, 64, 64) : SystemColors.MenuHighlight; } }

        #endregion

        #region Application

        /// <summary>
        /// Applies the current theme to a top-level form and all of its controls,
        /// including the (Windows 10/11) title bar.
        /// </summary>
        public static void ApplyTheme(Form form) {
            if (form == null)
                return;

            bool dark = IsDark;

            DwmThemeMethods.SetImmersiveDarkMode(form.Handle, dark);

            form.BackColor = WindowBackground;
            form.ForeColor = TextColor;

            foreach (Control child in form.Controls)
                ApplyToControl(child, dark);

            //Force the non-client area to repaint with the new title bar colour.
            if (form.IsHandleCreated) {
                form.Invalidate(true);
            }
        }

        /// <summary>
        /// Recursively themes a single control and its children.
        /// </summary>
        public static void ApplyToControl(Control control, bool dark) {
            //The thumbnail surface itself must stay black, but its placeholder hint
            //sits over the themed window background, so colour it to match the theme.
            if (control is ThumbnailPanel thumbnailPanel) {
                thumbnailPanel.SetPlaceholderForeColor(TextColor);
                return;
            }

            switch (control) {
                case TextBox tb:
                    tb.BackColor = InputBackground;
                    tb.ForeColor = InputText;
                    tb.BorderStyle = dark ? BorderStyle.FixedSingle : BorderStyle.Fixed3D;
                    break;

                case ComboBox cb:
                    cb.BackColor = InputBackground;
                    cb.ForeColor = InputText;
                    cb.FlatStyle = dark ? FlatStyle.Flat : FlatStyle.Standard;
                    break;

                case Button btn:
                    btn.ForeColor = TextColor;
                    btn.BackColor = dark ? SurfaceBackground : SystemColors.Control;
                    btn.FlatStyle = dark ? FlatStyle.Flat : FlatStyle.Standard;
                    if (dark) {
                        btn.FlatAppearance.BorderColor = BorderColor;
                    }
                    break;

                case LinkLabel link:
                    link.BackColor = Color.Transparent;
                    link.ForeColor = TextColor;
                    link.LinkColor = AccentColor;
                    link.ActiveLinkColor = AccentColor;
                    break;

                case Label lbl:
                    lbl.BackColor = Color.Transparent;
                    lbl.ForeColor = TextColor;
                    break;

                case GroupBox grp:
                    grp.BackColor = Color.Transparent;
                    grp.ForeColor = TextColor;
                    //With visual styles on, a GroupBox paints its caption with a fixed
                    //(dark) theme colour and ignores ForeColor. FlatStyle.Flat makes it
                    //honour ForeColor so the title is readable in dark mode.
                    grp.FlatStyle = dark ? FlatStyle.Flat : FlatStyle.System;
                    break;

                case Panel pnl:
                    pnl.BackColor = SurfaceBackground;
                    pnl.ForeColor = TextColor;
                    break;

                default:
                    control.BackColor = SurfaceBackground;
                    control.ForeColor = TextColor;
                    break;
            }

            foreach (Control child in control.Controls)
                ApplyToControl(child, dark);
        }

        /// <summary>
        /// Applies the appropriate renderer to the given context menus: a dark
        /// professional renderer in dark mode, or the native Aero renderer in light mode.
        /// </summary>
        public static void ApplyMenuRenderer(params ToolStrip[] menus) {
            if (menus == null)
                return;

            if (IsDark) {
                var renderer = new DarkToolStripRenderer();
                foreach (var menu in menus) {
                    if (menu == null)
                        continue;
                    menu.RenderMode = ToolStripRenderMode.Professional;
                    menu.Renderer = renderer;
                    menu.ForeColor = MenuText;
                    menu.BackColor = MenuBackground;
                }
            }
            else {
                //Restore the original Aero/native look in light mode.
                Asztal.Szótár.NativeToolStripRenderer.SetToolStripRenderer(menus);
            }
        }

        #endregion

    }

}
