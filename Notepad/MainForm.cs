using Notepad.Controls;
using Notepad.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notepad
{
    public partial class MainForm : Form
    {
        public static RichTextBox RichTextBox;
        public MainForm()
        {
            InitializeComponent();
            
            var menuStrip = new MainMenuStrip();
            var mainTabControl = new MainTabControl();
            RichTextBox = new CustomRichTextBox();

            mainTabControl.TabPages.Add("Onglet 1");
            mainTabControl.TabPages[0].Controls.Add(RichTextBox);

            TextFile file = new TextFile("C:/test.txt");
            
            Controls.AddRange(new Control[] { mainTabControl, menuStrip });
        }
    }
}
