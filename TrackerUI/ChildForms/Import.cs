using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using TrackerLibrary;
using TrackerLibrary.CRUD;

namespace TrackerUI.ChildForms
{
    public partial class Import : Form
    {
        public Import()
        {
            InitializeComponent();
            
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Console.SetOut(new TextBoxWriter(richTextBox));
        }

        private void PrintConsole(string filePath)
        {
            
            float[,,,] ea = EVCalculator.ImportDLL.ReadEAFromFileAsFloatArray(GlobalConfig.pfEA);

            

            string entireHH = filePath.ReadFileReturnString();

            string[] splitString = entireHH.SplitStringBySize(60000);

            foreach (string hh in splitString)
            {
                NoSQL_Connector.InsertHandsToNoSqlDb(GlobalConfig.dbName, GlobalConfig.tableName, GlobalConfig.columnName, HHReader.ReadHands(hh));
            }
        }


        //private string GetFullFilePath()
        //{
        //    string filePath ="";

        //    using (OpenFileDialog openFileDialog = new OpenFileDialog())
        //    {
                
        //        // Set the initial directory and filter for the file dialog
        //        openFileDialog.InitialDirectory = GlobalConfig.projectDirectory;
        //        openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";

        //        // Show the file dialog and check if the user selected a file
        //        if (openFileDialog.ShowDialog() == DialogResult.OK)
        //        {
        //            // Get the selected file path
        //            filePath = openFileDialog.FileName;

        //        }
        //    }

        //    return filePath;
        //}

        private void btn_Import_Click(object sender, MouseEventArgs e)
        {
            string filePath = File_Connector.GetFullFilePath();

            Task t1 = Task.Run(() => { PrintConsole(filePath); });
        }
        //private void openFile()
        //{
        //    using (OpenFileDialog openFileDialog = new OpenFileDialog())
        //    {
        //        // Set the initial directory and filter for the file dialog
        //        openFileDialog.InitialDirectory = GlobalConfig.projectDirectory;
        //        openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";

        //        // Show the file dialog and check if the user selected a file
        //        if (openFileDialog.ShowDialog() == DialogResult.OK)
        //        {
        //            // Get the selected file path
        //            string filePath = openFileDialog.FileName;

        //            // TODO: Process the file or perform desired operations
        //            // You can use the filePath variable to access the selected file
        //        }
        //    }
        //}
    }





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
            ((RichTextBox)outputControl).ScrollToCaret();
        }

        public override Encoding Encoding => Encoding.UTF8;
    }

    

}
