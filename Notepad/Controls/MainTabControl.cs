using System.Windows.Forms;

namespace Notepad.Controls
{
    public class MainTabControl : TabControl
    {
        private const string NAME = "MainTabControl";
        private ContextMenuStrip _contextMenuStrip;

        public MainTabControl()
        {
            _contextMenuStrip = new TabControlContextMenuStrip();

            Name = NAME;
            ContextMenuStrip = _contextMenuStrip;
            Dock = DockStyle.Fill;
        }
    }
}
