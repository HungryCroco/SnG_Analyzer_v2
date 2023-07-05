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
            txtBoxES.Text = "BETWEEN 20 AND 25";

            //List<StatsModel> allStats = CallStats.CallAllStats();
            //cmbBoxQuery.DataSource = allStats;
            //cmbBoxQuery.ValueMember = "SqlQuery";
            //cmbBoxQuery.DisplayMember = "Name";

            Dictionary<string, string> mapComboBoxVs = new() { { "vs FISHes", "fishes" } , { "vs REGs", "regs" } };
            cmbBoxVs.DataSource = new BindingSource(mapComboBoxVs, null);
            cmbBoxVs.DisplayMember = "Key";
            cmbBoxVs.ValueMember = "Value";


            Dictionary<string, string> mapComboBoxAi = new() { { "NAI", "0" } , { "AI", "1" } };
            cmbBoxAI.DataSource = new BindingSource(mapComboBoxAi, null);
            cmbBoxAI.DisplayMember = "Key";
            cmbBoxAI.ValueMember = "Value";

            Dictionary<string, QueryModel.Query> mapComboBoxQuery = new() { { "BvB ISO", new QueryModel.Query(HeatMapQueries.NoSQL_ExportHeatMapAsJSON, HeatMapQueries.NoSQL_ExportHeatMapByHoleCardsSimple, HeatMapQueries.NoSQL_WhereClauseHero_BvB_Iso , HeatMapQueries.NoSQL_WhereClauseVillain_BvB_Iso)} , 
                                                                                { "BvB oL", new QueryModel.Query(HeatMapQueries.NoSQL_ExportHeatMapAsJSON, HeatMapQueries.NoSQL_ExportHeatMapByHoleCardsSimple, HeatMapQueries.NoSQL_WhereClauseHero_BvB_oL , HeatMapQueries.NoSQL_WhereClauseVillain_BvB_oL)} , 
                                                                                { "BvB oR", new QueryModel.Query(HeatMapQueries.NoSQL_ExportHeatMapAsJSON, HeatMapQueries.NoSQL_ExportHeatMapByHoleCardsSimple, HeatMapQueries.NoSQL_WhereClauseHero_BvB_oR , HeatMapQueries.NoSQL_WhereClauseVillain_BvB_oR)} };
            cmbBoxQuery.DataSource = new BindingSource(mapComboBoxQuery, null);
            cmbBoxQuery.DisplayMember = "Key";
            cmbBoxQuery.ValueMember = "Value";


        }

        private void LoadHeatMap(QueryModel.Query query,  string hero, string tourneyType, string sinceDate, string ai, string size, string vs, string es)
        {
            DateTime startTime = DateTime.Now;


            var request = query.RequestStatsModel_Heatmap(hero, tourneyType, sinceDate, ai, size, vs, es);

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

                //foreach (var c in tableLayoutPanel_HeatMap.Controls)
                //{
                //    try
                //    {
                //        Label l = (Label)c;


                //        l.BackColor = Color.WhiteSmoke;
                //        l.Text = ((CardAllSimple)int.Parse(l.Tag.ToString())).GetDescription();

                //        int statsCurrVal = request[int.Parse(l.Tag.ToString()) - 1].Situations;
                //        l.Text = l.Text + "\n" + request[int.Parse(l.Tag.ToString()) - 1].Situations;
                //        l.BackColor = UIMethods.CalculateColor((double)statsCurrVal / statsMaxValue);

                //    }
                //    catch { }
                //}


                foreach (var c in tableLayoutPanel_HeatMap.Controls)
                {
                    try
                    {
                        Label l = (Label)c;

                        int cardId = int.Parse(l.Tag.ToString());

                        l.BackColor = Color.WhiteSmoke;
                        l.Text = ((CardAllSimple)cardId).GetDescription();



                        int index = request.BinarySearch(new StatsModel { CardId = cardId }, Comparer<StatsModel>.Create((x, y) => x.CardId.CompareTo(y.CardId)));

                        if (index >= 0)
                        {
                            int statsCurrVal = request[index].Situations;
                            l.Text = l.Text + "\n" + request[index].Situations;
                            l.BackColor = UIMethods.CalculateColor((double)statsCurrVal / statsMaxValue);
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                } 
            }

            // calculate time:
            var calculatingTime = (DateTime.Now - startTime).TotalSeconds;
            labelCalculatingTime.Text = "Calculating Time: " + calculatingTime.ToString("0.00");
        }

        private void btn_Request_Click(object sender, EventArgs e)
        {
            LoadHeatMap( (QueryModel.Query)cmbBoxQuery.SelectedValue, txtBoxPlayer.Text, txtBoxTourneyType.Text, txtBoxDate.Text, cmbBoxAI.SelectedValue.ToString(), txtBoxSize.Text, cmbBoxVs.SelectedValue.ToString(), txtBoxES.Text);
        }

        private void label_Click(object sender, MouseEventArgs e)
        {
            QueryModel.Query query = (QueryModel.Query)cmbBoxQuery.SelectedValue;
            //StatsModel myStats = (StatsModel)cmbBoxStats.SelectedItem;
            Label l = (Label)sender;

            dataGridView_HeatMap.DataSource = null;
            dataGridView_HeatMap.DataSource = query.RequestStatsModel_Heatmap_DataGridViewByHCs(txtBoxPlayer.Text, txtBoxTourneyType.Text, txtBoxDate.Text, cmbBoxAI.SelectedValue.ToString(), txtBoxSize.Text, cmbBoxVs.SelectedValue.ToString(), txtBoxES.Text, l.Tag.ToString());

            dataGridView_HeatMap.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            AutoResizeDataGridView(dataGridView_HeatMap);
        }
    }
}
