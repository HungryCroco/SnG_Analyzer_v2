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

namespace TrackerUI
{
    public partial class MainForm : Form
    {

        private Form activeForm;

        //[DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        //private extern static void ReleaseCapture();
        //[DllImport("user32.DLL", EntryPoint = "SendMessage")]
        //private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);


        public MainForm()
        {
            object sender = new(); 
            EventArgs e = new();
            InitializeComponent();
            //Task t1 = Task.Run(() => { OpenChildForm(new ChildForms.Dashboard(), sender); });
            //this.Text = string.Empty;
            //this.ControlBox = false;
            //this.FormBorderStyle = FormBorderStyle.None;
            //this.Bounds = GetSecondaryScreen().Bounds;
            //this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void btnDashBoard_Click(object sender, EventArgs e)
        {
            Task t1 = Task.Run(() => { OpenChildForm(new ChildForms.Dashboard(), sender); });

            //OpenChildForm(new ChildForms.Dashboard(), sender);
        }

        private void btnCEV_Click(object sender, EventArgs e)
        {

        }

        private void btnBoard_Click(object sender, EventArgs e)
        {

        }

        private void btnVillian_Click(object sender, EventArgs e)
        {

            //OpenChildForm(new ChildForms.HeatMap(), sender);
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            Task t1 = Task.Run(() => { OpenChildForm(new ChildForms.Import(), sender); });
            //OpenChildForm(new ChildForms.Import(), sender);
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {

        }

        






        private void OpenChildForm(Form _childForm, object sender)
        {
            if (activeForm != null)
            {
                this.activeForm.Invoke((MethodInvoker)(() => activeForm.Close()));
                //activeForm.Close();
            }
            activeForm = _childForm;
            _childForm.TopLevel = false;
            _childForm.FormBorderStyle = FormBorderStyle.None;
            _childForm.Dock = DockStyle.Fill;
            //this.panelDesktop.Controls.Add(_childForm);
            //this.panelDesktop.Tag = _childForm;
            //_childForm.Show();
            //_childForm.BringToFront();
            //panel_ArrowShow.BringToFront();

            this.panelDesktop.Invoke((MethodInvoker)(() => this.panelDesktop.Controls.Add(_childForm)));
            this.panelDesktop.Invoke((MethodInvoker)(() => this.panelDesktop.Tag = _childForm));
            this.panelDesktop.Invoke((MethodInvoker)(() => _childForm.Show()));
            this.panelDesktop.Invoke((MethodInvoker)(() => _childForm.BringToFront()));
            this.panelDesktop.Invoke((MethodInvoker)(() => panel_ArrowShow.BringToFront()));
            //label1.Invoke((MethodInvoker)(() => label1.Text = " "));

            

        }



        private void lblArrow_Hide_Click(object sender, EventArgs e)
        {
            panelMenu.Width = 0;
            panel_ArrowShow.Visible = true;
            lblArrow_Show.Visible = true;


        }



        private void lblArrow_Show_Click(object sender, EventArgs e)
        {
            panelMenu.Width = 450;
            panel_ArrowShow.Visible = false;
            lblArrow_Show.Visible = false;
        }

        private void btn_MouseHover(object sender, EventArgs e)
        {
            Button currBtn = (Button)sender;
            currBtn.BackColor = Color.FromArgb(253, 218, 13);
        }

        private void btn_MouseLeave(object sender, EventArgs e)
        {
            Button currBtn = (Button)sender;
            currBtn.BackColor = Color.FromArgb(255, 255, 192);
        }


    }
}
