using Notepad.Controls;
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
        public MainForm()
        {
            InitializeComponent();

            var menuStrip = new MainMenuStrip();
            var mainTabControl = new MainTabControl();
            var richTextBox = new CustomRichTextBox();

            mainTabControl.TabPages.Add("Onglet 1");
            mainTabControl.TabPages[0].Controls.Add(richTextBox);

            Controls.AddRange(new Control[] { mainTabControl, menuStrip });
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
