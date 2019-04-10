using System.Drawing;
using System.Windows.Forms;

namespace CDSTools
{
    public static class UIExtensionMethods
    {
        #region RichTextBoxExtensions
        public static void AppendText(this RichTextBox box, string text, Color? color = null, Font font = null)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            if (font != null) { box.SelectionFont = font; }
            if (color.HasValue) { box.SelectionColor = color.Value; }
            
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }
        #endregion
    }
}
