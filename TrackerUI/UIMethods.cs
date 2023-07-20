

namespace TrackerUI
{
    public static class UIMethods
    {
        
        /// <summary>
        /// Calculates Color(Darkness) based on input int; Used in HeatMap to highlight the HoleCards with higher occurance;
        /// </summary>
        /// <param name="val">current Occurance/(max Occurance-min Occurance); max = 1, min = 0;</param>
        /// <returns>Color</returns>
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

        /// <summary>
        /// Resizes DataGridView to fill in the Panel;
        /// </summary>
        /// <param name="dgv">DataGridView, that will be resized;</param>
        public static void AutoResizeDataGridView(this DataGridView dgv)
        {
            // TO DO: This MEthod doesn't work well, for example in the Settings UI; Needs to be rewritten;

            int nLastColumn = dgv.Columns.Count - 1;
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                if (nLastColumn == i)
                {
                    dgv.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
                else
                {
                    dgv.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
            }

            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                int colw = dgv.Columns[i].Width;
                dgv.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgv.Columns[i].Width = colw;
            }
        }

    }
}
