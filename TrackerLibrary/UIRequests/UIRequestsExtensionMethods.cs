using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TrackerLibrary.Models;

namespace TrackerLibrary.UIRequests
{
    public static class UIRequestsExtensionMethods
    {
        public static void WriteEA2FileAsJSON(string _fileName, List<List<CevModel>> _list)
        {


            var output = JsonSerializer.Serialize(_list);

            try
            {
                //// Check if file already exists. If yes, delete it.     
                if (File.Exists(_fileName))
                {
                    File.Delete(_fileName);
                }

                // Create a new file     
                using (StreamWriter sw = File.CreateText(_fileName))
                {
                    sw.Write(output);
                }

                // Write file contents on console.     
                using (StreamReader sr = File.OpenText(_fileName))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(s);
                    }
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }


        }

        public static List<List<CevModel>> ReadEAFromFileAsJSON(string _fileName)
        {
            var data = File.ReadAllText(_fileName);

            //string fileName = @"C:\Users\tatsi\source\repos\NASH_EquilibriumCalculator\EquityArrays\eaPFasJSON.txt";

            var output = JsonSerializer.Deserialize<List<List<CevModel>>>(data);

            return output;

        }
    }
}
