using System;
using System.Windows.Forms;
using WindowsFormsAero.Dwm;

namespace OnTopReplica.Platforms {

    class WindowsVista : PlatformSupport {
        
        public override bool CheckCompatibility() {
            if (!WindowsFormsAero.OsSupport.IsCompositionEnabled) {
                MessageBox.Show(Strings.ErrorDwmOffContent, Strings.ErrorDwmOff, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        public override void PostHandleFormInit(MainForm form) {
            DwmManager.SetWindowFlip3dPolicy(form, WindowsFormsAero.Flip3DPolicy.ExcludeAbove);

            //The tray icon (and hiding from the taskbar) is now managed by MainForm,
            //driven by the ShowTrayIcon setting, so it works on every platform.
        }

        public override bool IsHidden(MainForm form) {
            return !form.Visible;
        }

        public override void HideForm(MainForm form) {
            form.Hide();
        }

        public override void RestoreForm(MainForm form) {
            form.Show();
        }

    }

}
