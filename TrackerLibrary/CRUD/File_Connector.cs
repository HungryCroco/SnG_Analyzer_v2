using System.Runtime.InteropServices;
using System.Text.Json;
using TrackerLibrary.Models;


namespace TrackerLibrary.CRUD
{
    /// <summary>
    /// Contains all necessery Methods to read and write from/to .txt files;
    /// </summary>
    public class File_Connector
    {
        /// <summary>
        /// Writes a JSON string to .txt File;
        /// </summary>
        /// <param name="fullFilePath"> full path of the .txt File to be written on;</param>
        /// <param name="data">object, that would be serialized to JSON and written as string to the .txt File;</param>
        /// <param name="options">[Optional]Options, necessery to convert non standart objects to JSON;</param>
        public static void WriteToFileAsJSON(string fullFilePath, object data, [Optional] JsonSerializerOptions options)
        {

            // Serialize the object to JSON;
            var output = JsonSerializer.Serialize((Settings)data, options);

            try
            {
                //// Check if file already exists. If yes, delete it.     
                if (File.Exists(fullFilePath))
                {
                    //Delete if the file is existing;
                    File.Delete(fullFilePath);
                }

                // Create a new file     
                using (StreamWriter sw = File.CreateText(fullFilePath))
                {
                    sw.Write(output);
                }

            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }


        }

        /// <summary>
        /// Reads a JSON string from .txt File;
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fullFilePath">full path of the .txt File to be written on;</param>
        /// <param name="options">[Optional]Options, necessery to convert non standart objects to JSON;</param>
        /// <returns>Deserialized JSON</returns>
        public static object ReadJsonFromFile<T>(string fullFilePath, [Optional] JsonSerializerOptions options)
        {
            var data = File.ReadAllText(fullFilePath);

            //string fileName = @"C:\Users\tatsi\source\repos\NASH_EquilibriumCalculator\EquityArrays\eaPFasJSON.txt";

            var output = JsonSerializer.Deserialize<T>(data, options);

            return output;

        }

        /// <summary>
        /// Opens window and saves the full Path of the selected file;
        /// </summary>
        /// <returns>The full file path as String;</returns>
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
