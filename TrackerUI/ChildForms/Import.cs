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

        private void PrintConsole()
        {
            
            float[,,,] ea = EVCalculator.ImportDLL.ReadEAFromFileAsFloatArray(GlobalConfig.pfEA);

            

            string entireHH = GlobalConfig.FullFilePath(GlobalConfig.hhFileName, GlobalConfig.directoryHH).ReadFileReturnString();

            string[] splitString = entireHH.SplitStringBySize(60000);

            foreach (string hh in splitString)
            {
                NoSQL_Connector.InsertHandsToNoSqlDb(GlobalConfig.dbName, GlobalConfig.tableName, GlobalConfig.columnName, HHReader.ReadHands(hh));
            }
        }

        private void btn_Import_Click(object sender, EventArgs e)
        {
            Task t1 = Task.Run(() => { PrintConsole(); });
            
        }
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
        }

        public override Encoding Encoding => Encoding.UTF8;
    }

    

}
