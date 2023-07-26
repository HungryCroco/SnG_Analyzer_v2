

using TrackerLibrary;

namespace TrackerUI
{
    public partial class MainForm : Form
    {

        private Form activeForm;


        public MainForm()
        {
            object sender = new();
            EventArgs e = new();
            InitializeComponent();

            lblArrow_Show.Visible = false;
            btnDashBoard.BackColor = GlobalConfig.btnDefault;
            btnCEV.BackColor = GlobalConfig.btnDefault;
            btnBoard.BackColor = GlobalConfig.btnDefault;
            btnVillain.BackColor = GlobalConfig.btnDefault;
            btnImport.BackColor = GlobalConfig.btnDefault;
            btnSettings.BackColor = GlobalConfig.btnDefault;


            btnSettings_Click(sender, e);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Running DashBoard ChildForm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnDashBoard_Click(object sender, EventArgs e)
        {
            try
            {
                Task t1 = Task.Run(() =>
                {
                    OpenChildForm(new ChildForms.Dashboard(), sender);
                });
            }
            catch (Exception)
            {
            }



        }

        /// <summary>
        /// Running CEV ChildForm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnCEV_Click(object sender, EventArgs e)
        {
            Task t1 = Task.Run(() => { OpenChildForm(new ChildForms.CEV(), sender); });
        }

        /// <summary>
        /// Running Board ChildForm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBoard_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Running Villain ChildForm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnVillian_Click(object sender, EventArgs e)
        {

            Task t1 = Task.Run(() => { OpenChildForm(new ChildForms.HeatMap(), sender); });
        }

        /// <summary>
        /// Running Import ChildForm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImport_Click(object sender, EventArgs e)
        {
            Task t1 = Task.Run(() => { OpenChildForm(new ChildForms.Import(), sender); });
        }


        /// <summary>
        /// Running Settings ChildForm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSettings_Click(object sender, EventArgs e)
        {
            Task t1 = Task.Run(() => { OpenChildForm(new ChildForms.Settings(), sender); });
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

            this.panelDesktop.Invoke((MethodInvoker)(() => this.panelDesktop.Controls.Add(_childForm)));
            this.panelDesktop.Invoke((MethodInvoker)(() => this.panelDesktop.Tag = _childForm));
            this.panelDesktop.Invoke((MethodInvoker)(() => _childForm.Show()));
            this.panelDesktop.Invoke((MethodInvoker)(() => _childForm.BringToFront()));

        }

        /// <summary>
        /// Hiding the Menu;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblArrow_Hide_Click(object sender, EventArgs e)
        {
            panelMenu.Width = 0;
            //panel_ArrowShow.Visible = true;
            lblArrow_Show.Visible = true;
            //panel_ArrowShow.BringToFront();
            lblArrow_Show.BringToFront();
            //panel_ArrowShow.BringToFront();


        }


        /// <summary>
        /// Showing the Menu;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblArrow_Show_Click(object sender, EventArgs e)
        {
            panelMenu.Width = 350;
            //panel_ArrowShow.Visible = false;
            lblArrow_Show.Visible = false;
        }

        /// <summary>
        /// Changing the Color if mouse is over the BTN;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_MouseHover(object sender, EventArgs e)
        {
            Button currBtn = (Button)sender;
            currBtn.BackColor = GlobalConfig.btnMouseOver;
        }

        /// <summary>
        /// Returning to old Color if mouse is leaving the BTN;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_MouseLeave(object sender, EventArgs e)
        {
            Button currBtn = (Button)sender;
            currBtn.BackColor = GlobalConfig.btnDefault;
        }
    }
}
