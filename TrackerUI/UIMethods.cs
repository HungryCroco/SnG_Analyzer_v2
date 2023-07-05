using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary;

namespace TrackerUI
{
    public static class UIMethods
    {
        

        public static Color CalculateColor(double val)
        {
            Color output = new Color();

            Color c1 = ColorTranslator.FromHtml("#FDFEFE");
            Color c2 = ColorTranslator.FromHtml("#F8F9F9");
            Color c3 = ColorTranslator.FromHtml("#F2F3F4");
            Color c4 = ColorTranslator.FromHtml("#E5E7E9");
            Color c5 = ColorTranslator.FromHtml("#D7DBDD");
            Color c6 = ColorTranslator.FromHtml("#CACFD2");
            Color c7 = ColorTranslator.FromHtml("#BDC3C7");
            Color c8 = ColorTranslator.FromHtml("#A6ACAF");

            switch (val)
            {
                case <= 0.125:
                    output = c1;
                    break;
                case <= 0.25:
                    output = c2;
                    break;
                case <= 0.375:
                    output = c3;
                    break;
                case <= 0.50:
                    output = c4;
                    break;
                case <= 0.625:
                    output = c5;
                    break;
                case <= 0.75:
                    output = c6;
                    break;
                case <= 0.875:
                    output = c7;
                    break;
                case > 0.875:
                    output = c8;
                    break;
                default:
                    output = c1;
                    break;
            }

            return output;
        }

        //public static string ReadRegList(string _listName, bool _isReg)
        //{

        //    var regList = GlobalConfig.FullFilePath(_listName, GlobalConfig.directoryRegFilter).ReadFileReturnString();
        //    if (_isReg)
        //    {
        //        return "in ( " + regList + " )";
        //    }
        //    else
        //    {
        //        return "not in ( " + regList + " )";
        //    }
            
        //}

    }
}
