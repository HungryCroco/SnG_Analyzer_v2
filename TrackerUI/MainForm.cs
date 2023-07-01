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
using TrackerUI.ChildForms;

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

        private async void btnDashBoard_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ChildForms.Loading(), sender);
            Task openDashboardForm = Task.Run(() => { OpenChildForm(new ChildForms.Dashboard(), sender); });
        }

        private async void btnCEV_Click(object sender, EventArgs e)
        {
            Task t1 = Task.Run(() => { OpenChildForm(new ChildForms.Loading(), sender); });
        }

        //private async void btnDashBoard_Click(object sender, EventArgs e)
        //{
        //    //OpenChildForm(new ChildForms.Loading(), sender);
        //    Task loadingTask = Task.Run(() => { OpenChildForm(new ChildForms.Loading(), sender); });
        //    Task dashboardTask = Task.Run(() => { OpenChildForm(new ChildForms.Dashboard(), sender); });

        //    await Task.WhenAll(loadingTask, dashboardTask);
        //}

        //private void btnCEV_Click(object sender, EventArgs e)
        //{
        //    Task t1 = Task.Run(() => { OpenChildForm(new ChildForms.Loading(), sender); });
        //}

        private void btnBoard_Click(object sender, EventArgs e)
        {

        }

        private void btnVillian_Click(object sender, EventArgs e)
        {

            Task t1 = Task.Run(() => { OpenChildForm(new ChildForms.HeatMap(), sender); });
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            Task t1 = Task.Run(() => { OpenChildForm(new ChildForms.Import(), sender); });
        }

        

        private void btnSettings_Click(object sender, EventArgs e)
        {

        }

        //private void OpenChildForm(Type childFormType, object sender)
        //{
        //    if (activeForm != null && !activeForm.IsDisposed)
        //    {
        //        if (activeForm.IsHandleCreated)
        //        {
        //            activeForm.Invoke((MethodInvoker)(() =>
        //            {
        //                activeForm.Close();
        //                activeForm.Dispose();
        //            }));
        //        }
        //        else
        //        {
        //            activeForm.Close();
        //            activeForm.Dispose();
        //        }
        //    }

        //    Form childForm = (Form)Activator.CreateInstance(childFormType);
        //    activeForm = childForm;
        //    childForm.TopLevel = false;
        //    childForm.FormBorderStyle = FormBorderStyle.None;
        //    childForm.Dock = DockStyle.Fill;

        //    panelDesktop.Invoke((MethodInvoker)(() =>
        //    {
        //        panelDesktop.Controls.Add(childForm);
        //        panelDesktop.Tag = childForm;
        //        childForm.Show();
        //        childForm.BringToFront();
        //        panel_ArrowShow.BringToFront();
        //    }));
        //}


        private void OpenChildForm(Form childForm, object sender)
        {
            if (activeForm != null && !activeForm.IsDisposed)
            {
                if (activeForm.IsHandleCreated)
                {
                    activeForm.Invoke((MethodInvoker)(() =>
                    {
                        activeForm.Close();
                        activeForm.Dispose();
                    }));
                }
                else
                {
                    activeForm.Close();
                    activeForm.Dispose();
                }
            }

            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            panelDesktop.Invoke((MethodInvoker)(() =>
            {
                panelDesktop.Controls.Add(childForm);
                panelDesktop.Tag = childForm;
                childForm.Show();
                childForm.BringToFront();
                panel_ArrowShow.BringToFront();
            }));
        }

        //private async Task OpenChildForm(Type childFormType, object sender)
        //{
        //    if (activeForm != null && !activeForm.IsDisposed)
        //    {
        //        if (activeForm.IsHandleCreated)
        //        {
        //            await Task.Factory.FromAsync(activeForm.BeginInvoke(new MethodInvoker(() =>
        //            {
        //                activeForm.Close();
        //                activeForm.Dispose();
        //            })), activeForm.EndInvoke);
        //        }
        //        else
        //        {
        //            activeForm.Close();
        //            activeForm.Dispose();
        //        }
        //    }

        //    Form childForm = (Form)Activator.CreateInstance(childFormType);
        //    activeForm = childForm;
        //    childForm.TopLevel = false;
        //    childForm.FormBorderStyle = FormBorderStyle.None;
        //    childForm.Dock = DockStyle.Fill;

        //    await Task.Factory.FromAsync(panelDesktop.BeginInvoke(new MethodInvoker(() =>
        //    {
        //        panelDesktop.Controls.Add(childForm);
        //        panelDesktop.Tag = childForm;
        //        childForm.Show();
        //        childForm.BringToFront();
        //        panel_ArrowShow.BringToFront();
        //    })), panelDesktop.EndInvoke);
        //}


        //private async void OpenChildForm(Form childForm, object sender)
        //{
        //    if (activeForm != null)
        //    {
        //        activeForm.Close();
        //        activeForm.Dispose();
        //    }

        //    // Show the loading form
        //    Loading loadingForm = new Loading();
        //    loadingForm.Show();

        //    // Run the task asynchronously
        //    //await Task.Run(() =>
        //    //{
        //    //    Form form = childForm;
        //    //    Thread.Sleep(3000);
        //    //});

        //    // Hide the loading form
        //    loadingForm.Close();
        //    loadingForm.Dispose();

        //    // Show the child form
        //    activeForm = childForm;
        //    childForm.TopLevel = false;
        //    childForm.FormBorderStyle = FormBorderStyle.None;
        //    childForm.Dock = DockStyle.Fill;
        //    panelDesktop.Controls.Add(childForm);
        //    childForm.Show();
        //    childForm.BringToFront();
        //}




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
