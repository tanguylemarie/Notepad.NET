using System.Drawing;
using System.Windows.Forms;

namespace Notepad.Controls
{
    public class CustomRichTextBox : RichTextBox
    {
        private const string NAME = "RtbTextFileContents";

        public CustomRichTextBox()
        {
            Name = NAME;
            AcceptsTab = true;
            Font = new Font("Arial", 12.0F, FontStyle.Regular);
            Dock = DockStyle.Fill;
            BorderStyle = BorderStyle.None;
            ContextMenuStrip = new RichTextBoxContextMenuStrip(this);
        }
    }
}
