using Notepad.Controls;
using Notepad.Objects;
using System.Windows.Forms;

namespace Notepad
{
    public partial class MainForm : Form
    {
        public RichTextBox CurrentRtb;
        public TextFile CurrentFile;
        public TabControl MainTabControl;
        public Session Session;

        public MainForm()
        {
            InitializeComponent();

            var menuStrip = new MainMenuStrip();
            MainTabControl = new MainTabControl();
            Session = new Session();
            
            Controls.AddRange(new Control[] { MainTabControl, menuStrip });
        }
    }
}
