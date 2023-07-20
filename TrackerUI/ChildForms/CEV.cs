


namespace TrackerUI.ChildForms
{
    /// <summary>
    /// This UI is in development; Currently not used;
    /// </summary>
    public partial class CEV : Form
    {


        public CEV()
        {
            InitializeComponent();
        }



        private void CEV_Resize(object sender, EventArgs e)
        {
            splitCnt_Chart_Panel1_Resize(sender, e);
        }

        private void splitCnt_Chart_Panel1_Resize(object sender, EventArgs e)
        {
            foreach (var item in this.splitCnt_Chart.Panel1.Controls)
            {
                Control c = (Control)item;
                c.Height = splitCnt_Chart.Panel1.Height;
                c.Width = splitCnt_Chart.Panel1.Width;
            }

        }

    }   
}
