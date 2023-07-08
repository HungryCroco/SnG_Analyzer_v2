using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TrackerLibrary.Models;
using System.Windows.Forms;

namespace TrackerLibrary.CRUD
{
    public class File_Connector
    {
        public static void WriteToFileAsJSON(string fullFilePath, object data, [Optional] JsonSerializerOptions options)
        {


            var output = JsonSerializer.Serialize((SettingsModel.Settings)data, options);

            try
            {
                //// Check if file already exists. If yes, delete it.     
                if (File.Exists(fullFilePath))
                {
                    File.Delete(fullFilePath);
                }

                // Create a new file     
                using (StreamWriter sw = File.CreateText(fullFilePath))
                {
                    sw.Write(output);
                }

                // Write file contents on console.     
                //using (StreamReader sr = File.OpenText(fullFilePath))
                //{
                //    string s = "";
                //    while ((s = sr.ReadLine()) != null)
                //    {
                //        Console.WriteLine(s);
                //    }
                //}
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }


        }

        public static object ReadJsonFromFile<T>(string _fileName, [Optional] JsonSerializerOptions options)
        {
            var data = File.ReadAllText(_fileName);

            //string fileName = @"C:\Users\tatsi\source\repos\NASH_EquilibriumCalculator\EquityArrays\eaPFasJSON.txt";

            var output = JsonSerializer.Deserialize<T>(data, options);

            return output;

        }

        public static string GetFullFilePath()
        {
            string filePath = "";

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {

                // Set the initial directory and filter for the file dialog
                openFileDialog.InitialDirectory = GlobalConfig.projectDirectory;
                openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";

                // Show the file dialog and check if the user selected a file
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the selected file path
                    filePath = openFileDialog.FileName;

                }
            }

            return filePath;
        }
    }
}
