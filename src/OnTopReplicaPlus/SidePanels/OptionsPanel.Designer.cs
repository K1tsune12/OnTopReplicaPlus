namespace OnTopReplica.SidePanels {
    partial class OptionsPanel {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.btnClose = new System.Windows.Forms.Button();
            this.panelMain = new System.Windows.Forms.Panel();
            this.groupHotkeys = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblHotKeyCycleRegion = new System.Windows.Forms.Label();
            this.txtHotKeyCycleRegion = new OnTopReplica.HotKeyTextBox();
            this.lblHotKeyClickThrough = new System.Windows.Forms.Label();
            this.txtHotKeyClickThrough = new OnTopReplica.HotKeyTextBox();
            this.lblHotKeyClickForwarding = new System.Windows.Forms.Label();
            this.txtHotKeyClickForwarding = new OnTopReplica.HotKeyTextBox();
            this.lblHotKeyShowHide = new System.Windows.Forms.Label();
            this.txtHotKeyShowHide = new OnTopReplica.HotKeyTextBox();
            this.lblHotKeyClone = new System.Windows.Forms.Label();
            this.txtHotKeyClone = new OnTopReplica.HotKeyTextBox();
            this.groupLanguage = new System.Windows.Forms.GroupBox();
            this.comboLanguage = new OnTopReplica.ImageComboBox();
            this.lblLanguage = new System.Windows.Forms.Label();
            this.panelMain.SuspendLayout();
            this.groupHotkeys.SuspendLayout();
            this.groupLanguage.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(220, 476);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(87, 27);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = global::OnTopReplica.Strings.MenuClose;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.Close_click);
            // 
            // panelMain
            // 
            this.panelMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelMain.AutoScroll = true;
            this.panelMain.Controls.Add(this.groupHotkeys);
            this.panelMain.Controls.Add(this.groupLanguage);
            this.panelMain.Location = new System.Drawing.Point(7, 7);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(366, 455);
            this.panelMain.TabIndex = 1;
            // 
            // groupHotkeys
            // 
            this.groupHotkeys.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupHotkeys.Controls.Add(this.label1);
            this.groupHotkeys.Controls.Add(this.lblHotKeyCycleRegion);
            this.groupHotkeys.Controls.Add(this.txtHotKeyCycleRegion);
            this.groupHotkeys.Controls.Add(this.lblHotKeyClickThrough);
            this.groupHotkeys.Controls.Add(this.txtHotKeyClickThrough);
            this.groupHotkeys.Controls.Add(this.lblHotKeyClickForwarding);
            this.groupHotkeys.Controls.Add(this.txtHotKeyClickForwarding);
            this.groupHotkeys.Controls.Add(this.lblHotKeyShowHide);
            this.groupHotkeys.Controls.Add(this.txtHotKeyShowHide);
            this.groupHotkeys.Controls.Add(this.lblHotKeyClone);
            this.groupHotkeys.Controls.Add(this.txtHotKeyClone);
            this.groupHotkeys.Location = new System.Drawing.Point(3, 89);
            this.groupHotkeys.Name = "groupHotkeys";
            this.groupHotkeys.Size = new System.Drawing.Size(359, 230);
            this.groupHotkeys.TabIndex = 1;
            this.groupHotkeys.TabStop = false;
            this.groupHotkeys.Text = "Hot keys:";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(7, 224);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(351, 60);
            this.label1.TabIndex = 6;
            this.label1.Text = "These system-wide shortcuts can also be used when OnTopReplica is not in focus.";
            //
            // lblHotKeyCycleRegion
            //
            this.lblHotKeyCycleRegion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.lblHotKeyCycleRegion.BackColor = System.Drawing.Color.Transparent;
            this.lblHotKeyCycleRegion.Location = new System.Drawing.Point(192, 104);
            this.lblHotKeyCycleRegion.Name = "lblHotKeyCycleRegion";
            this.lblHotKeyCycleRegion.Size = new System.Drawing.Size(148, 30);
            this.lblHotKeyCycleRegion.TabIndex = 5;
            this.lblHotKeyCycleRegion.Text = "Cycle regions";
            //
            // txtHotKeyCycleRegion
            //
            this.txtHotKeyCycleRegion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHotKeyCycleRegion.Location = new System.Drawing.Point(7, 102);
            this.txtHotKeyCycleRegion.Name = "txtHotKeyCycleRegion";
            this.txtHotKeyCycleRegion.ReadOnly = true;
            this.txtHotKeyCycleRegion.Size = new System.Drawing.Size(181, 23);
            this.txtHotKeyCycleRegion.TabIndex = 4;
            //
            // lblHotKeyClickThrough
            //
            this.lblHotKeyClickThrough.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.lblHotKeyClickThrough.BackColor = System.Drawing.Color.Transparent;
            this.lblHotKeyClickThrough.Location = new System.Drawing.Point(192, 144);
            this.lblHotKeyClickThrough.Name = "lblHotKeyClickThrough";
            this.lblHotKeyClickThrough.Size = new System.Drawing.Size(148, 30);
            this.lblHotKeyClickThrough.TabIndex = 7;
            this.lblHotKeyClickThrough.Text = "Click-through";
            //
            // txtHotKeyClickThrough
            //
            this.txtHotKeyClickThrough.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHotKeyClickThrough.Location = new System.Drawing.Point(7, 142);
            this.txtHotKeyClickThrough.Name = "txtHotKeyClickThrough";
            this.txtHotKeyClickThrough.ReadOnly = true;
            this.txtHotKeyClickThrough.Size = new System.Drawing.Size(181, 23);
            this.txtHotKeyClickThrough.TabIndex = 8;
            //
            // lblHotKeyClickForwarding
            //
            this.lblHotKeyClickForwarding.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.lblHotKeyClickForwarding.BackColor = System.Drawing.Color.Transparent;
            this.lblHotKeyClickForwarding.Location = new System.Drawing.Point(192, 184);
            this.lblHotKeyClickForwarding.Name = "lblHotKeyClickForwarding";
            this.lblHotKeyClickForwarding.Size = new System.Drawing.Size(148, 30);
            this.lblHotKeyClickForwarding.TabIndex = 9;
            this.lblHotKeyClickForwarding.Text = "Click forwarding";
            //
            // txtHotKeyClickForwarding
            //
            this.txtHotKeyClickForwarding.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHotKeyClickForwarding.Location = new System.Drawing.Point(7, 182);
            this.txtHotKeyClickForwarding.Name = "txtHotKeyClickForwarding";
            this.txtHotKeyClickForwarding.ReadOnly = true;
            this.txtHotKeyClickForwarding.Size = new System.Drawing.Size(181, 23);
            this.txtHotKeyClickForwarding.TabIndex = 10;
            //
            // lblHotKeyShowHide
            //
            this.lblHotKeyShowHide.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.lblHotKeyShowHide.BackColor = System.Drawing.Color.Transparent;
            this.lblHotKeyShowHide.Location = new System.Drawing.Point(192, 24);
            this.lblHotKeyShowHide.Name = "lblHotKeyShowHide";
            this.lblHotKeyShowHide.Size = new System.Drawing.Size(148, 30);
            this.lblHotKeyShowHide.TabIndex = 3;
            this.lblHotKeyShowHide.Text = "Show/Hide";
            // 
            // txtHotKeyShowHide
            // 
            this.txtHotKeyShowHide.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHotKeyShowHide.Location = new System.Drawing.Point(7, 22);
            this.txtHotKeyShowHide.Name = "txtHotKeyShowHide";
            this.txtHotKeyShowHide.ReadOnly = true;
            this.txtHotKeyShowHide.Size = new System.Drawing.Size(181, 23);
            this.txtHotKeyShowHide.TabIndex = 2;
            // 
            // lblHotKeyClone
            // 
            this.lblHotKeyClone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.lblHotKeyClone.BackColor = System.Drawing.Color.Transparent;
            this.lblHotKeyClone.Location = new System.Drawing.Point(192, 64);
            this.lblHotKeyClone.Name = "lblHotKeyClone";
            this.lblHotKeyClone.Size = new System.Drawing.Size(148, 30);
            this.lblHotKeyClone.TabIndex = 1;
            this.lblHotKeyClone.Text = "Clone current window";
            // 
            // txtHotKeyClone
            // 
            this.txtHotKeyClone.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHotKeyClone.Location = new System.Drawing.Point(7, 62);
            this.txtHotKeyClone.Name = "txtHotKeyClone";
            this.txtHotKeyClone.ReadOnly = true;
            this.txtHotKeyClone.Size = new System.Drawing.Size(181, 23);
            this.txtHotKeyClone.TabIndex = 0;
            // 
            // groupLanguage
            // 
            this.groupLanguage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupLanguage.Controls.Add(this.comboLanguage);
            this.groupLanguage.Controls.Add(this.lblLanguage);
            this.groupLanguage.Location = new System.Drawing.Point(3, 3);
            this.groupLanguage.Name = "groupLanguage";
            this.groupLanguage.Size = new System.Drawing.Size(359, 78);
            this.groupLanguage.TabIndex = 0;
            this.groupLanguage.TabStop = false;
            this.groupLanguage.Text = "Language:";
            // 
            // comboLanguage
            // 
            this.comboLanguage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboLanguage.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboLanguage.FormattingEnabled = true;
            this.comboLanguage.IconList = null;
            this.comboLanguage.Location = new System.Drawing.Point(10, 22);
            this.comboLanguage.Name = "comboLanguage";
            this.comboLanguage.Size = new System.Drawing.Size(341, 24);
            this.comboLanguage.TabIndex = 2;
            this.comboLanguage.SelectedIndexChanged += new System.EventHandler(this.LanguageBox_IndexChange);
            // 
            // lblLanguage
            // 
            this.lblLanguage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLanguage.Location = new System.Drawing.Point(7, 50);
            this.lblLanguage.Name = "lblLanguage";
            this.lblLanguage.Size = new System.Drawing.Size(345, 25);
            this.lblLanguage.TabIndex = 1;
            this.lblLanguage.Text = "Requires a restart.";
            // 
            // OptionsPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.btnClose);
            this.MinimumSize = new System.Drawing.Size(380, 515);
            this.Name = "OptionsPanel";
            this.Padding = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.Size = new System.Drawing.Size(380, 515);
            this.panelMain.ResumeLayout(false);
            this.groupHotkeys.ResumeLayout(false);
            this.groupHotkeys.PerformLayout();
            this.groupLanguage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.GroupBox groupLanguage;
        private System.Windows.Forms.Label lblLanguage;
        private ImageComboBox comboLanguage;
        private System.Windows.Forms.GroupBox groupHotkeys;
        private HotKeyTextBox txtHotKeyClone;
        private System.Windows.Forms.Label lblHotKeyShowHide;
        private HotKeyTextBox txtHotKeyShowHide;
        private System.Windows.Forms.Label lblHotKeyClone;
        private System.Windows.Forms.Label lblHotKeyCycleRegion;
        private HotKeyTextBox txtHotKeyCycleRegion;
        private System.Windows.Forms.Label lblHotKeyClickThrough;
        private HotKeyTextBox txtHotKeyClickThrough;
        private System.Windows.Forms.Label lblHotKeyClickForwarding;
        private HotKeyTextBox txtHotKeyClickForwarding;
        private System.Windows.Forms.Label label1;
    }
}
