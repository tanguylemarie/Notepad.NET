using Microsoft.SqlServer.Server;
using Notepad.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notepad.Controls
{
    public class MainMenuStrip : MenuStrip
    {
        private const string NAME = "MainMenuStrip";

        private MainForm _form;
        private FontDialog _fontDialog;

        public MainMenuStrip()
        {
            Name = NAME;
            Dock = DockStyle.Top;

            _fontDialog = new FontDialog();

            FileDropDownMenu();
            EditDropDownMenu();
            FormatDropDownMenu();
            ViewDropDownMenu();

            HandleCreated += (s, e) =>
            {
                _form = FindForm() as MainForm;
            };
        }

        public void FileDropDownMenu()
        {
            var fileDropDownMenu = new ToolStripMenuItem("Fichier");

            var newFile = new ToolStripMenuItem("Nouveau", null, null, Keys.Control | Keys.N);
            var open = new ToolStripMenuItem("Ouvrir...", null, null, Keys.Control | Keys.O);
            var save = new ToolStripMenuItem("Enregistrer", null, null, Keys.Control | Keys.S);
            var saveAs = new ToolStripMenuItem("Enregistrer sous...", null, null, Keys.Control | Keys.Shift | Keys.N);
            var quit = new ToolStripMenuItem("Quitter", null, null, Keys.Alt | Keys.F4);

            newFile.Click += (s, e) =>
            {
                var tabControl = _form.MainTabControl;
                var tabPagesCount = tabControl.TabPages.Count;

                var fileName = $"Sans titre {tabPagesCount + 1}";
                var file = new TextFile(fileName);
                var rtb = new CustomRichTextBox();

                tabControl.TabPages.Add(file.SafeFileName);

                var newTabPage = tabControl.TabPages[tabPagesCount];

                newTabPage.Controls.Add(rtb);
                tabControl.SelectedTab = newTabPage;

                _form.Session.TextFiles.Add(file);
                _form.CurrentFile = file;
                _form.CurrentRtb = rtb;
            };

            fileDropDownMenu.DropDownItems.AddRange(new ToolStripItem[] { newFile, open, save, saveAs, quit });

            Items.Add(fileDropDownMenu);
        }

        public void EditDropDownMenu()
        {
            var editDropDown = new ToolStripMenuItem("Edition");

            var undo = new ToolStripMenuItem("Annuler", null, null, Keys.Control | Keys.Z);
            var redo = new ToolStripMenuItem("Restaurer", null, null, Keys.Control | Keys.Y);

            undo.Click += (s, e) => { if (_form.CurrentRtb.CanUndo) _form.CurrentRtb.Undo(); };
            redo.Click += (s, e) => { if (_form.CurrentRtb.CanRedo) _form.CurrentRtb.Redo(); };

            editDropDown.DropDownItems.AddRange(new ToolStripItem[] { undo, redo });

            Items.Add(editDropDown);
        }

        public void FormatDropDownMenu()
        {
            var formatDropDown = new ToolStripMenuItem("Format");

            var font = new ToolStripMenuItem("Police...");

            font.Click += (s, e) =>
            {
                _fontDialog.Font = _form.CurrentRtb.Font;
                _fontDialog.ShowDialog();

                _form.CurrentRtb.Font = _fontDialog.Font;
            };

            formatDropDown.DropDownItems.AddRange(new ToolStripItem[] { font });

            Items.Add(formatDropDown);
        }

        public void ViewDropDownMenu()
        {
            var viewDropDown = new ToolStripMenuItem("Affichage");
            var alwaysOnTop = new ToolStripMenuItem("Toujours devant");

            var zoomDropDown = new ToolStripMenuItem("Zoom");
            var zoomIn = new ToolStripMenuItem("Zoom avant", null, null, Keys.Control | Keys.Add);
            var zoomOut = new ToolStripMenuItem("Zoom arrière", null, null, Keys.Control | Keys.Subtract);
            var restoreZoom = new ToolStripMenuItem("Restaurer le zoom par défaut", null, null, Keys.Control | Keys.Divide);

            zoomIn.ShortcutKeyDisplayString = "Ctrl+Num +";
            zoomOut.ShortcutKeyDisplayString = "Ctrl+Num -";
            restoreZoom.ShortcutKeyDisplayString = "Ctrl+Num /";

            alwaysOnTop.Click += (s, e) =>
            {
                if (alwaysOnTop.Checked)
                {
                    alwaysOnTop.Checked = false;
                    Program.MainForm.TopMost = false;
                }
                else
                {
                    alwaysOnTop.Checked = true;
                    Program.MainForm.TopMost = true;
                }
            };

            zoomIn.Click += (s, e) =>
            {
                if (_form.CurrentRtb.ZoomFactor < 3F)
                {
                    _form.CurrentRtb.ZoomFactor += 0.3F;
                }
            };

            zoomOut.Click += (s, e) =>
            {
                if (_form.CurrentRtb.ZoomFactor > 0.7F)
                {
                    _form.CurrentRtb.ZoomFactor -= 0.3F;
                }
            };

            restoreZoom.Click += (s, e) => { _form.CurrentRtb.ZoomFactor = 1F; };

            zoomDropDown.DropDownItems.AddRange(new ToolStripItem[] { zoomIn, zoomOut, restoreZoom });

            viewDropDown.DropDownItems.AddRange(new ToolStripItem[] { alwaysOnTop, zoomDropDown });

            Items.Add(viewDropDown);
        }
    }
}