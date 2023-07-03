using System;
using System.Globalization;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrackerLibrary;
using TrackerLibrary.Models;
using TrackerLibrary.CRUD;
using TrackerLibrary.Queries.NoSQL;

namespace TrackerUI.ChildForms
{
    public partial class HeatMap : Form
    {

        public HeatMap()
        {
            InitializeComponent();


        }




        private void label_Click(object sender, EventArgs e)
        {
            

        }

        private void AutoResizeDataGridView(DataGridView dgv)
        {
            //autoresize columns but buggish
            //TO DO: search foir better autoresize method

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

        private void HeatMap_Load(object sender, EventArgs e)
        {

            txtBoxPlayer.Text = "IPray2Buddha";
            txtBoxDate.Text = "1999-01-01";
            txtBoxTourneyType.Text = "3-max";
            txtBoxSize.Text = "BETWEEN 2 AND 4";

            //List<StatsModel> allStats = CallStats.CallAllStats();
            //cmbBoxQuery.DataSource = allStats;
            //cmbBoxQuery.ValueMember = "SqlQuery";
            //cmbBoxQuery.DisplayMember = "Name";

            Dictionary<string, string> mapComboBoxVs = new() { { "vs REGs", "regs" }, { "vs FISHes", "fishes" } };
            comboBoxVs.DataSource = new BindingSource(mapComboBoxVs, null);
            comboBoxVs.DisplayMember = "Key";
            comboBoxVs.ValueMember = "Value";


            Dictionary<string, string> mapComboBoxAi = new() { { "AI", "1" }, { "NAI", "0" } };
            comboBoxAI.DataSource = new BindingSource(mapComboBoxAi, null);
            comboBoxAI.DisplayMember = "Key";
            comboBoxAI.ValueMember = "Value";

            Dictionary<string, string> mapComboBoxQuery = new() { { "BvB ISO", HeatMapQueries.sql_ExportHeatMapAsJSON_BvB_Iso } };
            cmbBoxQuery.DataSource = new BindingSource(mapComboBoxQuery, null);
            cmbBoxQuery.DisplayMember = "Key";
            cmbBoxQuery.ValueMember = "Value";


        }

        private void LoadHeatMap(string hero, string tourneyType, string sinceDate, string ai, string size, string vs)
        {
            DateTime startTime = DateTime.Now;


            var request = NoSQL_CalculateHeatMap.RequestStatsModel_BvbIso(hero, tourneyType, sinceDate, ai, size, vs);

            int statsMaxValue = 0;
            if (request != null)
            {

            
            foreach (var stats in request)
            {
                if (stats.Situations > statsMaxValue)
                {
                    statsMaxValue = stats.Situations;
                }
            }

            foreach (var c in tableLayoutPanel_HeatMap.Controls)
            {
                try
                {
                    Label l = (Label)c;

                    int statsCurrVal = request[int.Parse(l.Tag.ToString()) - 1].Situations;
                    l.BackColor = Color.WhiteSmoke;
                    l.Text = ((CardAllSimple)int.Parse(l.Tag.ToString())).GetDescription();
                    l.Text = l.Text + "\n" + request[int.Parse(l.Tag.ToString()) - 1].Situations;
                    l.BackColor = UIMethods.CalculateColor((double)statsCurrVal / statsMaxValue);
                }
                catch { }
            }

            }

            // calculate time:
            var calculatingTime = (DateTime.Now - startTime).TotalSeconds;
            labelCalculatingTime.Text = "Calculating Time: " + calculatingTime.ToString("0.00");
        }

        private void labelCalculatingTime_Click(object sender, EventArgs e)
        {

        }

        private void btn_Request_Click(object sender, EventArgs e)
        {
            LoadHeatMap(txtBoxPlayer.Text, txtBoxTourneyType.Text, txtBoxDate.Text, comboBoxAI.SelectedValue.ToString(), txtBoxSize.Text, comboBoxVs.SelectedValue.ToString());
        }
    }
}
