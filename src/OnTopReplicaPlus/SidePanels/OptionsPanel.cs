using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OnTopReplica.Properties;
using System.Globalization;

namespace OnTopReplica.SidePanels {
    partial class OptionsPanel : SidePanel {

        GroupBox _groupTheme;
        ComboBox _comboTheme;
        Label _lblDescription;
        bool _themeLoading;
        bool _positioning;
        float _lastScale = -1f;

        public OptionsPanel() {
            InitializeComponent();

            LocalizePanel();

            BuildDescriptionLabel();
            BuildThemeControls();
        }

        private void BuildDescriptionLabel() {
            //The Designer description label refuses to render reliably inside the flat-styled
            //group box in dark mode, so recreate it as a plain label on the panel. Its bounds
            //are set proportionally in OnLayout so it scales correctly at any DPI.
            label1.Visible = false;

            _lblDescription = new Label {
                Text = Strings.SettingsHotKeyDescription,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            panelMain.Controls.Add(_lblDescription);
            _lblDescription.BringToFront();
        }

        private void BuildThemeControls() {
            _groupTheme = new GroupBox {
                Text = Strings.SettingsThemeTitle,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                TabStop = false
            };

            _comboTheme = new ComboBox {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            _comboTheme.Items.AddRange(new object[] {
                Strings.SettingsThemeSystem,
                Strings.SettingsThemeLight,
                Strings.SettingsThemeDark
            });
            _comboTheme.SelectedIndexChanged += ThemeBox_IndexChange;

            _groupTheme.Controls.Add(_comboTheme);
            panelMain.Controls.Add(_groupTheme);
        }

        /// <summary>
        /// Positions the code-created controls (the description label and the theme group)
        /// proportionally, relative to the Designer hotkeys group. Because the Designer
        /// controls are auto-scaled by font/DPI, deriving the scale from them keeps these
        /// controls aligned on high-DPI displays instead of using fixed pixel coordinates.
        /// </summary>
        protected override void OnLayout(LayoutEventArgs levent) {
            base.OnLayout(levent);

            if (_positioning || _lblDescription == null || _groupTheme == null)
                return;

            //359 is the hotkeys group's design width; its runtime width gives the scale.
            float scale = groupHotkeys.Width / 359f;

            //Only reposition when the scale actually changes. This avoids redundant work and,
            //crucially, clears the ghost pixels left where the controls sat at the previous
            //(intermediate) scale during auto-scaling.
            if (scale == _lastScale)
                return;
            _lastScale = scale;

            _positioning = true;
            try {
                int gap = Scale(8, scale);

                _lblDescription.SetBounds(
                    groupHotkeys.Left + Scale(4, scale),
                    groupHotkeys.Bottom + gap,
                    groupHotkeys.Width - Scale(8, scale),
                    Scale(52, scale));

                _groupTheme.SetBounds(
                    groupHotkeys.Left,
                    _lblDescription.Bottom + gap,
                    groupHotkeys.Width,
                    Scale(56, scale));

                _comboTheme.SetBounds(
                    Scale(10, scale),
                    Scale(22, scale),
                    _groupTheme.Width - Scale(20, scale),
                    Scale(24, scale));

                //Repaint the whole panel so no ghost of the previous positions remains.
                panelMain.Invalidate(true);
            }
            finally {
                _positioning = false;
            }
        }

        static int Scale(int value, float scale) {
            return (int)Math.Round(value * scale);
        }

        private void ThemeBox_IndexChange(object sender, EventArgs e) {
            if (_themeLoading)
                return;

            string preference;
            switch (_comboTheme.SelectedIndex) {
                case 1: preference = "Light"; break;
                case 2: preference = "Dark"; break;
                default: preference = "System"; break;
            }

            Theming.ThemeManager.SetPreference(preference);
        }

        private void LocalizePanel() {
            groupLanguage.Text = Strings.SettingsLanguageTitle;
            lblLanguage.Text = Strings.SettingsRestartRequired;

            groupHotkeys.Text = Strings.SettingsHotKeyTitle;
            lblHotKeyShowHide.Text = Strings.SettingsHotKeyShowHide;
            lblHotKeyClone.Text = Strings.SettingsHotKeyClone;
            lblHotKeyCycleRegion.Text = Strings.SettingsHotKeyCycleRegion;
            lblHotKeyClickThrough.Text = Strings.SettingsHotKeyClickThrough;
            lblHotKeyClickForwarding.Text = Strings.SettingsHotKeyClickForwarding;
            label1.Text = Strings.SettingsHotKeyDescription;

            btnClose.Text = Strings.MenuClose;
        }

        public override void OnFirstShown(MainForm form) {
            base.OnFirstShown(form);

            PopulateLanguageComboBox();

            //Stop hotkey handling and load current shortcuts
            form.MessagePumpManager.Get<OnTopReplica.MessagePumpProcessors.HotKeyManager>().Enabled = false;
            txtHotKeyShowHide.Text = Settings.Default.HotKeyShowHide;
            txtHotKeyClone.Text = Settings.Default.HotKeyCloneCurrent;
            txtHotKeyCycleRegion.Text = Settings.Default.HotKeyCycleSavedRegion;
            txtHotKeyClickThrough.Text = Settings.Default.HotKeyClickThrough;
            txtHotKeyClickForwarding.Text = Settings.Default.HotKeyClickForwarding;

            //Bring the rows to front from top to bottom, so the lower label always
            //paints over the (tiny) overlap with the row above and its text stays visible.
            lblHotKeyShowHide.BringToFront();
            lblHotKeyClone.BringToFront();
            lblHotKeyCycleRegion.BringToFront();
            lblHotKeyClickThrough.BringToFront();
            lblHotKeyClickForwarding.BringToFront();

            //Load the current theme preference into the combo without firing the change handler.
            _themeLoading = true;
            switch (Theming.ThemeManager.Preference) {
                case "Light": _comboTheme.SelectedIndex = 1; break;
                case "Dark": _comboTheme.SelectedIndex = 2; break;
                default: _comboTheme.SelectedIndex = 0; break;
            }
            _themeLoading = false;
        }

        private void Close_click(object sender, EventArgs e) {
            OnRequestClosing();
        }

        public override string Title {
            get {
                return Strings.SettingsTitle;
            }
        }

        public override void OnClosing(MainForm form) {
            base.OnClosing(form);

            //Update hotkey settings and update processor
            Settings.Default.HotKeyShowHide = txtHotKeyShowHide.Text;
            Settings.Default.HotKeyCloneCurrent = txtHotKeyClone.Text;
            Settings.Default.HotKeyCycleSavedRegion = txtHotKeyCycleRegion.Text;
            Settings.Default.HotKeyClickThrough = txtHotKeyClickThrough.Text;
            Settings.Default.HotKeyClickForwarding = txtHotKeyClickForwarding.Text;
            var manager = form.MessagePumpManager.Get<OnTopReplica.MessagePumpProcessors.HotKeyManager>();
            manager.RefreshHotkeys();
            manager.Enabled = true;
        }

        #region Language

        class CultureWrapper {
            public CultureWrapper(string name, CultureInfo culture, Image img) {
                Culture = culture;
                Image = img;
                Name = name;
            }
            public CultureInfo Culture { get; set; }
            public Image Image { get; set; }
            public string Name { get; set; }
        }

        CultureWrapper[] _languageList = {
            new CultureWrapper("English", new CultureInfo("en-US"), Resources.flag_usa),
            new CultureWrapper("Čeština", new CultureInfo("cs-CZ"), Resources.flag_czech),
            new CultureWrapper("Dansk", new CultureInfo("da-DK"), Resources.flag_danish),
            new CultureWrapper("Deutsch", new CultureInfo("de-DE"), Resources.flag_germany),
            new CultureWrapper("Español", new CultureInfo("es-ES"), Resources.flag_spanish),
            new CultureWrapper("Italiano", new CultureInfo("it-IT"), Resources.flag_ita),
            new CultureWrapper("Polski", new CultureInfo("pl-PL"), Resources.flag_poland),
            new CultureWrapper("简体中文", new CultureInfo("zh-CN"), Resources.flag_china),
            new CultureWrapper("繁體中文", new CultureInfo("zh-TW"), Resources.flag_taiwan),
            new CultureWrapper("Português", new CultureInfo("pt-BR"), Resources.flag_brazil),
            new CultureWrapper("日本語", new CultureInfo("ja-JP"), Resources.help),
        };

        private void PopulateLanguageComboBox() {
            comboLanguage.Items.Clear();

            var imageList = new ImageList() {
                ImageSize = new Size(16, 16),
                ColorDepth = ColorDepth.Depth32Bit
            };
            comboLanguage.IconList = imageList;

            int selectedIndex = -1;
            foreach (var langPair in _languageList) {
                var item = new ImageComboBoxItem(langPair.Name, imageList.Images.Count) {
                    Tag = langPair.Culture
                };
                imageList.Images.Add(langPair.Image);
                comboLanguage.Items.Add(item);

                if (langPair.Culture.Equals(CultureInfo.CurrentUICulture)) {
                    selectedIndex = comboLanguage.Items.Count - 1;
                }
            }

            //Handle case when there is not explicitly set culture (default to first one, i.e. english)
            if (CultureInfo.CurrentUICulture.Equals(CultureInfo.InvariantCulture))
                selectedIndex = 0;

            comboLanguage.SelectedIndex = selectedIndex;
        }

        private void LanguageBox_IndexChange(object sender, EventArgs e) {
            var item = comboLanguage.SelectedItem as ImageComboBoxItem;
            if (item == null)
                return;

            Settings.Default.Language = item.Tag as CultureInfo;
        }

        #endregion

    }

}
