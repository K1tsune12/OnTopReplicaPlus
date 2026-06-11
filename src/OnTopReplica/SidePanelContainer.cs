using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OnTopReplica {
    /// <summary>
    /// Acts as a form that can contain a SidePanel instance.
    /// </summary>
    partial class SidePanelContainer : WindowsFormsAero.AeroForm {
        
        EventHandler RequestClosingHandler;

        MainForm _parent;

        public SidePanelContainer(MainForm mainForm) {
            InitializeComponent();

            _parent = mainForm;
            RequestClosingHandler = new EventHandler(Panel_RequestClosing);

            Theming.ThemeManager.ThemeChanged += ThemeManager_ThemeChanged;
        }

        private void ThemeManager_ThemeChanged(object sender, EventArgs e) {
            Theming.ThemeManager.ApplyTheme(this);
        }

        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            Theming.ThemeManager.ApplyTheme(this);
        }

        void Panel_RequestClosing(object sender, EventArgs e) {
            FreeSidePanel();
            this.Close();
        }

        protected override void OnClosing(CancelEventArgs e) {
            base.OnClosing(e);

            Theming.ThemeManager.ThemeChanged -= ThemeManager_ThemeChanged;

            //Ensure side panel closing code is run
            FreeSidePanel();
        }

        /// <summary>
        /// Sets a new side panel and refreshes the window. The panel is
        /// managed by the SidePanelContainer from now on.
        /// </summary>
        /// <param name="panel">SidePanel to embed and to manage.</param>
        public void SetSidePanel(SidePanel panel) {
            panel.OnFirstShown(_parent);

            this.SuspendLayout();

            //Title
            this.Text = panel.Title;

            //Set panel
            CurrentSidePanel = panel;
            panel.RequestClosing += RequestClosingHandler;
            this.Controls.Add(panel);
            
            var minSize = panel.MinimumSize.Expand(this.Padding);
            this.ClientSize = minSize;
            this.EnsureMinimumClientSize(minSize);
            this.GlassMargins = panel.GlassMargins;

            this.ResumeLayout();

            //Theme the newly embedded panel and its controls.
            Theming.ThemeManager.ApplyTheme(this);
        }

        /// <summary>
        /// Removes the current side panel and disposes it.
        /// </summary>
        public void FreeSidePanel() {
            if (CurrentSidePanel == null)
                return;

            this.SuspendLayout();

            FreeSidePanelCore();

            this.ResumeLayout();
        }

        /// <summary>
        /// Removes the current side panel and disposes it (doesn't suspend layout).
        /// </summary>
        private void FreeSidePanelCore() {
            CurrentSidePanel.OnClosing(_parent);

            //Free hook
            CurrentSidePanel.RequestClosing -= RequestClosingHandler;
            
            //Remove
            this.Controls.Remove(CurrentSidePanel);
            
            //Free
            CurrentSidePanel.Dispose();
            CurrentSidePanel = null;
        }

        public SidePanel CurrentSidePanel {
            get;
            private set;
        }
    }
}
