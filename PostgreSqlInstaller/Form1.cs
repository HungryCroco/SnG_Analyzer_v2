using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PostgreSqlInstaller
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.BringToFrontAndFocus();
        }

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        private void BringToFrontAndFocus()
        {
            // Bring the form to the front and set focus to it
            SetForegroundWindow(this.Handle);
        }
    }
}
