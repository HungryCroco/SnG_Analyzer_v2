using TrackerLibrary;
using TrackerLibrary.CRUD;
using TrackerLibrary.Models;
using TrackerLibrary.Queries.NoSQL;
using TrackerLibrary.Queries.SQL;


namespace TrackerUI.ChildForms
{
    public partial class HeatMap : Form
    {
        /// <summary>
        /// Load HeatMap Form;
        /// </summary>
        public HeatMap()
        {
            InitializeComponent();


        }

        /// <summary>
        /// Displays default HeatMap;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HeatMap_Load(object sender, EventArgs e)
        {
            // deault Filters;
            txtBoxPlayer.Text = "IPray2Buddha";
            txtBoxDate.Text = "1999-01-01";
            txtBoxTourneyType.Text = "3-max";
            txtBoxSize.Text = "BETWEEN 2 AND 4";
            txtBoxES.Text = "BETWEEN 20 AND 25";

            var settings = DataManager_Settings.ReadSettings();

            // comboBox for choosing vs REG/FISH;
            Dictionary<string, string> mapComboBoxVs = new() { { "vs FISHes", "fishes" }, { "vs REGs", "regs" } };
            cmbBoxVs.DataSource = new BindingSource(mapComboBoxVs, null);
            cmbBoxVs.DisplayMember = "Key";
            cmbBoxVs.ValueMember = "Value";

            // ComboBox for choosing ai /nai;
            Dictionary<string, string> mapComboBoxAi = new() { { "NAI", "0" }, { "AI", "1" } };
            cmbBoxAI.DataSource = new BindingSource(mapComboBoxAi, null);
            cmbBoxAI.DisplayMember = "Key";
            cmbBoxAI.ValueMember = "Value";

            // ComboBox with all available queries;
            Dictionary<string, Query> mapComboBoxQuery = new() { { "BvB ISO", settings.DbTypeRead == DataBaseType.NoSQL.GetDescription() ? new Query(NoSQL_HeatMapQueries.NoSQL_ExportHeatMapAsJSON, NoSQL_HeatMapQueries.NoSQL_ExportDataGridViewByHoleCardsSimple, NoSQL_HeatMapQueries.NoSQL_WhereClauseHero_BvB_Iso , NoSQL_HeatMapQueries.NoSQL_WhereClauseVillain_BvB_Iso) :
                                                                                                                                     new Query(SQL_HeatMapQueries.SQL_ExportHeatMapAsJSON,SQL_HeatMapQueries.SQL_ExportDataGridViewByHoleCardsSimple, SQL_HeatMapQueries.SQL_WhereClauseHero_BvB_Iso , "", "bbSeatActionId", "sbSeatActionId" )   } ,
                                                                                { "BvB oL", settings.DbTypeRead == DataBaseType.NoSQL.GetDescription() ? new Query(NoSQL_HeatMapQueries.NoSQL_ExportHeatMapAsJSON, NoSQL_HeatMapQueries.NoSQL_ExportDataGridViewByHoleCardsSimple, NoSQL_HeatMapQueries.NoSQL_WhereClauseHero_BvB_oL , NoSQL_HeatMapQueries.NoSQL_WhereClauseVillain_BvB_oL) :
                                                                                                                                        new Query(SQL_HeatMapQueries.SQL_ExportHeatMapAsJSON ,SQL_HeatMapQueries.SQL_ExportDataGridViewByHoleCardsSimple, SQL_HeatMapQueries.SQL_WhereClauseHero_BvB_oL , "", "sbSeatActionId", "bbSeatActionId")   } ,
                                                                                { "BvB oR", settings.DbTypeRead == DataBaseType.NoSQL.GetDescription() ? new Query(NoSQL_HeatMapQueries.NoSQL_ExportHeatMapAsJSON, NoSQL_HeatMapQueries.NoSQL_ExportDataGridViewByHoleCardsSimple, NoSQL_HeatMapQueries.NoSQL_WhereClauseHero_BvB_oR , NoSQL_HeatMapQueries.NoSQL_WhereClauseVillain_BvB_oR) :
                                                                                                                                        new Query(SQL_HeatMapQueries.SQL_ExportHeatMapAsJSON ,SQL_HeatMapQueries.SQL_ExportDataGridViewByHoleCardsSimple, SQL_HeatMapQueries.SQL_WhereClauseHero_BvB_oR, "", "sbSeatActionId", "bbSeatActionId")   } , };
            cmbBoxQuery.DataSource = new BindingSource(mapComboBoxQuery, null);
            cmbBoxQuery.DisplayMember = "Key";
            cmbBoxQuery.ValueMember = "Value";


        }

        /// <summary>
        /// Loading new HeatMap, used from the Request HeatMap Button;
        /// </summary>
        /// <param name="query">Query to be requested;</param>
        /// <param name="hero">active Player;</param>
        /// <param name="tourneyType">Tournament Type;</param>
        /// <param name="sinceDate">Date since when the Hands will be filtered;</param>
        /// <param name="ai">nai = "0", ia = "1"</param>
        /// <param name="size">size of the bet; Accepting >,<,=, BETWEEN etc;</param>
        /// <param name="vs">vsFISHes = "fishes, vs REGS = "regs"</param>
        /// <param name="es">Effective Stack; ; Accepting >,<,=, BETWEEN etc;</param>
        /// <param name="settings">Settings</param>
        private void LoadHeatMap(Query query, string hero, string tourneyType, string sinceDate, string ai, string size, string vs, string es, TrackerLibrary.Models.Settings settings)
        {
            DateTime startTime = DateTime.Now;


            var request = query.RequestStatsModel_Heatmap(hero, tourneyType, sinceDate, ai, size, vs, es, settings);

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

                // Looping all Controls and changing their text based on the query's data;
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

        /// <summary>
        /// Load a new HeatMap based on the filters;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Request_Click(object sender, EventArgs e)
        {
            TrackerLibrary.Models.Settings currSettings = DataManager_Settings.ReadSettings();
            LoadHeatMap((Query)cmbBoxQuery.SelectedValue, txtBoxPlayer.Text, txtBoxTourneyType.Text, txtBoxDate.Text, cmbBoxAI.SelectedValue.ToString(), txtBoxSize.Text, cmbBoxVs.SelectedValue.ToString(), txtBoxES.Text, currSettings);
        }

        /// <summary>
        /// Loads a new DataGridView based on the pushed label;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label_Click(object sender, MouseEventArgs e)
        {
            Query query = (Query)cmbBoxQuery.SelectedValue;
            TrackerLibrary.Models.Settings currSettings = DataManager_Settings.ReadSettings();
            Label l = (Label)sender;

            dataGridView_HeatMap.DataSource = null;
            dataGridView_HeatMap.DataSource = query.RequestStatsModel_Heatmap_DataGridViewByHCs(txtBoxPlayer.Text, txtBoxTourneyType.Text, txtBoxDate.Text, cmbBoxAI.SelectedValue.ToString(), txtBoxSize.Text, cmbBoxVs.SelectedValue.ToString(), txtBoxES.Text, l.Tag.ToString(), currSettings);

            dataGridView_HeatMap.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView_HeatMap.BackgroundColor = Color.White;
            dataGridView_HeatMap.AutoResizeDataGridView();
        }

        /// <summary>
        /// Loading a HandDisplayer; Not Implemented;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView_HeatMap_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
