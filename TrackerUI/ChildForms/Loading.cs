

namespace TrackerUI.ChildForms
{
    /// <summary>
    /// This Form is shown while loading a query;
    /// </summary>
    public partial class Loading : Form
    {
        public Loading()
        {
            InitializeComponent();

            PictureBox pictureBox1 = new PictureBox();
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Image = Image.FromFile("C:\\Users\\tatsi\\source\\repos\\SnG_Analyzer_v2\\Images\\Loading.gif");
            

            // Add the PictureBox control to your form
            this.Controls.Add(pictureBox1);

        }
    }
}
