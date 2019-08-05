using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Headquarters4DCS.DevTools
{
    public partial class FormMizToINI : Form
    {
        public FormMizToINI()
        {
            InitializeComponent();
        }

        private void Event_FormLoad(object sender, EventArgs e)
        {

        }

        private void Event_CloseButtonClick(object sender, EventArgs e)
        {
            Close();
        }

        private void Event_ParseButtonClick(object sender, EventArgs e)
        {
            string lX = null;
            string lY = null;

            string[] inputLines = CodeTextBox.Text.Replace("\r\n", "\n").Split('\n');

            List<string> outputLines = new List<string>();

            foreach (string l in inputLines)
            {
                if (l.Contains("[\"x\"]")) lX = l.Replace(" ", "").Replace("\t", "").Replace("=", "").Replace(",", "").Replace("[\"x\"]", "");
                if (l.Contains("[\"y\"]")) lY = l.Replace(" ", "").Replace("\t", "").Replace("=", "").Replace(",", "").Replace("[\"y\"]", "");

                if ((lX != null) && (lY != null))
                {
                    outputLines.Add(lX + "," + lY);
                    lX = null;
                    lY = null;
                }
            }

            CodeTextBox.Text = string.Join("\r\n", outputLines.ToArray());
        }
    }
}
