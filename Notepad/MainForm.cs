using Notepad.Controls;
using Notepad.Objects;
using System.IO;
using System.Linq;
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

            Controls.AddRange(new Control[] { MainTabControl, menuStrip });

            InitializeFile();
        }

        private async void InitializeFile()
        {
            Session = await Session.Load();

            if (Session.Files.Count == 0)
            {
                var file = new TextFile("Sans titre 1");

                MainTabControl.TabPages.Add(file.SafeFileName);

                var tabPage = MainTabControl.TabPages[0];
                var rtb = new CustomRichTextBox();
                tabPage.Controls.Add(rtb);
                rtb.Select();

                Session.Files.Add(file);

                CurrentFile = file;
                CurrentRtb = rtb;
            }
            else
            {
                var activeIndex = Session.ActiveIndex;

                foreach (var file in Session.Files)
                {
                    if (File.Exists(file.FileName) || File.Exists(file.BackupFileName))
                    {
                        var rtb = new CustomRichTextBox();
                        var tabCount = MainTabControl.TabCount;

                        MainTabControl.TabPages.Add(file.SafeFileName);
                        MainTabControl.TabPages[tabCount].Controls.Add(rtb);

                        rtb.Text = file.Contents;
                    }
                }

                CurrentFile = Session.Files[activeIndex];
                CurrentRtb = (CustomRichTextBox)MainTabControl.TabPages[activeIndex].Controls.Find("RtbTextFileContents", true).First();
                CurrentRtb.Select();
                
                MainTabControl.SelectedIndex = activeIndex;
                Text = $"{CurrentFile.FileName} - Notepad.NET";
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Session.ActiveIndex = MainTabControl.SelectedIndex;
            Session.Save();

            foreach (var file in Session.Files)
            {
                var fileIndex = Session.Files.IndexOf(file);
                var rtb = MainTabControl.TabPages[fileIndex].Controls.Find("RtbTextFileContents", true).First();
                
                if (file.FileName.StartsWith("Sans titre"))
                {
                    file.Contents = rtb.Text;
                    Session.BackupFile(file);
                }
            }
        }
    }
}