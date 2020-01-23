using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MonoGame.DisplayModeHelper.Test
{
    public partial class Form1 : Form
    {
        int oldScreenWidth, oldScreenHeight;

        public Form1()
        {
            InitializeComponent();

            FormClosing += Form1_FormClosing;

            oldScreenWidth = DisplayModes.CurrentResolution.Width;
            oldScreenHeight = DisplayModes.CurrentResolution.Height;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ResolutionHelper.ChangeResolution(oldScreenWidth, oldScreenHeight);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DEVMODE1 selectedMode;
            if (DisplayModes.SupportedDisplayModes.TryGetValue(listBox1.SelectedItem.ToString(), out selectedMode))
            {
                ResolutionHelper.ChangeResolution(selectedMode.dmPelsWidth, selectedMode.dmPelsHeight);

                var screenSize = DisplayModes.CurrentResolution;
                label2.Text = $"{screenSize.Width}x{screenSize.Height}";
            }
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            this.CenterToScreen();

            foreach (var mode in DisplayModes.SupportedDisplayModes)
            {
                this.listBox1.Items.Add(mode.Key);
            }

            var screenSize = DisplayModes.CurrentResolution;
            label2.Text = $"{screenSize.Width}x{screenSize.Height}";

        }

		private void button1_Click(object sender, System.EventArgs e)
		{
			MessageBox.Show(string.Format("User Resolution is {0}x{1}", DisplayModes.CurrentResolution.Width, DisplayModes.CurrentResolution.Height));
		}

        private void button2_Click(object sender, System.EventArgs e)
        {
            ResolutionHelper.ChangeResolution(oldScreenWidth, oldScreenHeight);
        }
    }
}
