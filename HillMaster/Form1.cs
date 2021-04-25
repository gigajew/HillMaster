using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HillMaster
{
    /// <summary>
    /// Old-school RuneScape Hill Giant Color Bot
    /// </summary>
    public partial class Form1 : Form
    {
        private Process process;
        private string[] filters = new string[] {  "osrs", "runescape", "osbuddy" };

        public Form1()
        {
            InitializeComponent();
            FindProcess();
        }

        private void FindProcess()
        {
            foreach(Process  p in Process.GetProcesses() ) {
                foreach(string filter in filters )
                {
                    if (p.MainWindowTitle.ToLower().Contains(filter))
                    {
                        process = p;
                    }
                }
                
            }
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            // todo: start a thread and put on repeat
            // todo: detect whether player under attack or not
            DetectClickHillGiant();
        }

        private void DetectClickHillGiant()
        {
            // if cluster > 8x8 { click middle of cluster }

            Bitmap screen = (Bitmap)new ScreenCapture().CaptureWindow(process.MainWindowHandle);

            for (int y = 0; y < 600 /*screen.Height*/; y++)
            {
                for (int x = 0; x < 600 /*screen.Width*/; x++)
                {
                    Color colorPixel = screen.GetPixel(x, y);
                    int r = colorPixel.R;
                    int g = colorPixel.G;
                    int b = colorPixel.B;
                    if ((r >= 117 && r <= 215) &&
                       (g >= 88 && g <= 185) &&
                       (b >= 55 && b <= 150))
                    {
                        // detect specific RGB cluster threshold 
                        screen.SetPixel(x, y, Color.Fuchsia);
                    }
                }
            }

            // save result for debugging purposes
            screen.Save("result.bmp");
        }

        private Bitmap GetScreenshot()
        {
            Rectangle bounds = Screen.PrimaryScreen.Bounds;
            Bitmap b = new Bitmap(bounds.Width, bounds.Height);
            using (Graphics g = Graphics.FromImage(b))
            {
                g.CopyFromScreen(0, 0, 0, 0, bounds.Size);
            }
            return b;
        }
    }
}
