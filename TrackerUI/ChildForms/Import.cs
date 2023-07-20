using System.Text;
using TrackerLibrary.CRUD;


namespace TrackerUI.ChildForms
{
    public partial class Import : Form
    {
        /// <summary>
        /// Initialize new Import-Form;
        /// </summary>
        public Import()
        {
            InitializeComponent();
            
        }

        /// <summary>
        /// Executes an Import Query based on the Settings;
        /// </summary>
        /// <param name="filePath">full Path of the .txt file that will be imported; </param>
        private void RunNewImport(string filePath)
        {
            TrackerLibrary.Models.Settings settings = DataManager_Settings.ReadSettings();
            Console.SetOut(new TextBoxWriter(richTextBox));

            try
            {
                DataManager_Import.RequestImport(filePath, settings, progressBarImport);

                
            }
            catch (Exception)
            {
                Console.WriteLine("Import Failed! Please choose a correct .txt file!");
            }
            
        }


       /// <summary>
       /// Clicking on the IMPORT Button;
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void btn_Import_Click(object sender, MouseEventArgs e)
        {
            string filePath = File_Connector.GetFullFilePath();
            btn_Import.Text = "Importing ...";
            btn_Import.Enabled = false;
            Task t1 = Task.Run(() => { RunNewImport(filePath); });

            if (t1.IsCompleted)
            {
                btn_Import.Text = "IMPORT";
                btn_Import.Enabled = false;
            }
        }

    }


    /// <summary>
    /// Prints Console to the TextBox;
    /// </summary>
    public class TextBoxWriter : TextWriter
    {
        private Control outputControl;
        public StringBuilder consoleOutput = new StringBuilder();

        public TextBoxWriter(Control control)
        {
            outputControl = control;
        }

        public override void Write(char value)
        {
            outputControl.Invoke(new Action(() =>
            {
                outputControl.Text += value;
                
            }));
            consoleOutput.Append(value);
            //((RichTextBox)outputControl).ScrollToCaret();
        }

        public override Encoding Encoding => Encoding.UTF8;
    }

    

}
