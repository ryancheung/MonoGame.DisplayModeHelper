using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MonoGame.DisplayModeHelper;

namespace MonoGame.DisplayModeHelper.Test
{
    public partial class Form1 : Form
    {
        private int currentScreenWidth = 0, currentScreenHeight = 0;

        public Form1()
        {
			currentScreenWidth = Screen.PrimaryScreen.Bounds.Width;
			currentScreenHeight = Screen.PrimaryScreen.Bounds.Height;

            InitializeComponent();

            FormClosing += Form1_FormClosing;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ResolutionHelper.ChangeResolution(currentScreenWidth, currentScreenHeight);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DEVMODE1 selectedMode;
            if (DisplayModes.SupportedDisplayModes.TryGetValue(listBox1.SelectedItem.ToString(), out selectedMode))
            {
                ResolutionHelper.ChangeResolution(selectedMode.dmPelsWidth, selectedMode.dmPelsHeight);
            }
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            this.CenterToScreen();

            foreach (var mode in DisplayModes.SupportedDisplayModes)
            {
                this.listBox1.Items.Add(mode.Key);
            }
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			MessageBox.Show(string.Format("User Resolution is {0}x{1}", currentScreenWidth, currentScreenHeight));
		}

        private void button2_Click(object sender, System.EventArgs e)
        {
            ResolutionHelper.ChangeResolution(currentScreenWidth, currentScreenHeight);
        }
    }
}
