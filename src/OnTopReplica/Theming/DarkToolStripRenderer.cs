using System.Drawing;
using System.Windows.Forms;

namespace OnTopReplica.Theming {

    /// <summary>
    /// Dark professional renderer that forces readable (light) text and arrows on
    /// menus, since the default renderer paints item text with the system colour
    /// (near-black), which is invisible on a dark background.
    /// </summary>
    sealed class DarkToolStripRenderer : ToolStripProfessionalRenderer {

        private static readonly Color EnabledText = Color.FromArgb(240, 240, 240);
        private static readonly Color DisabledText = Color.FromArgb(140, 140, 140);
        private static readonly Color ArrowColor = Color.FromArgb(220, 220, 220);

        public DarkToolStripRenderer()
            : base(new DarkColorTable()) {
            RoundedEdges = false;
        }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e) {
            e.TextColor = e.Item.Enabled ? EnabledText : DisabledText;
            base.OnRenderItemText(e);
        }

        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e) {
            e.ArrowColor = ArrowColor;
            base.OnRenderArrow(e);
        }

    }

}
