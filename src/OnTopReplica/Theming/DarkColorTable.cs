using System.Drawing;
using System.Windows.Forms;

namespace OnTopReplica.Theming {

    /// <summary>
    /// Professional color table that gives context menus a dark appearance.
    /// </summary>
    sealed class DarkColorTable : ProfessionalColorTable {

        private static readonly Color Background = Color.FromArgb(43, 43, 43);
        private static readonly Color ImageMargin = Color.FromArgb(36, 36, 36);
        private static readonly Color Highlight = Color.FromArgb(64, 64, 64);
        private static readonly Color Border = Color.FromArgb(80, 80, 80);

        public override Color ToolStripDropDownBackground { get { return Background; } }
        public override Color MenuStripGradientBegin { get { return Background; } }
        public override Color MenuStripGradientEnd { get { return Background; } }
        public override Color ImageMarginGradientBegin { get { return ImageMargin; } }
        public override Color ImageMarginGradientMiddle { get { return ImageMargin; } }
        public override Color ImageMarginGradientEnd { get { return ImageMargin; } }

        public override Color MenuItemSelected { get { return Highlight; } }
        public override Color MenuItemSelectedGradientBegin { get { return Highlight; } }
        public override Color MenuItemSelectedGradientEnd { get { return Highlight; } }
        public override Color MenuItemPressedGradientBegin { get { return Background; } }
        public override Color MenuItemPressedGradientEnd { get { return Background; } }
        public override Color MenuItemBorder { get { return Border; } }
        public override Color MenuBorder { get { return Border; } }

        public override Color SeparatorDark { get { return Border; } }
        public override Color SeparatorLight { get { return Border; } }

        public override Color CheckBackground { get { return Highlight; } }
        public override Color CheckSelectedBackground { get { return Highlight; } }
        public override Color CheckPressedBackground { get { return Highlight; } }

    }

}
