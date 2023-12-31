﻿using OxyPlot;
using OxyPlot.Legends;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using TrackerLibrary.CRUD;
using TrackerLibrary.Models;

namespace TrackerUI.ChildForms
{
    /// <summary>
    /// This UI shows a cEV/ real winnings Graph and the adjustedBB winnings/period for most Pos v Pos plays 3way-;
    /// All the necessary data is delivered trough the DashBoardModel Class;
    /// </summary>
    public partial class Dashboard : Form
    {
        /// <summary>
        /// Loading the Page;
        /// </summary>
        public Dashboard()
        {
            InitializeComponent();

            chb_3_3.Checked = true;
            TrackerLibrary.Models.Settings currSettings = DataManager_Settings.ReadSettings();
            DashBoardModel dashboard = DataManager_DashBoard.RequestDashBoard("IPray2Buddha", currSettings);

            LoadChart(PlotDataFromCevModelList(dashboard.CevModel_Total_ByTournament, Convert.ToInt32(currSettings.MinTourney), "CEV/t", 3), PlotDataFromCevModelList(dashboard.CevModel_Total_ByTournament, Convert.ToInt32(currSettings.MinTourney), "Chips/t", 2));
            tlp_overview_Calculate(dashboard, 0);


        }

        /// <summary>
        /// In development; Should show the cases for each Situation;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tlp_MouseEnter(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// In development; Should show the cases for each Situation;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tlp_MouseLeave(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// In development; Should display the current Info to the Graph;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Resizing the Oxyplot Garph;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CEV_Resize(object sender, EventArgs e)
        {
            splitCnt_Chart_Panel1_Resize(sender, e);
        }

        /// <summary>
        /// Resizing the Panel1;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void splitCnt_Chart_Panel1_Resize(object sender, EventArgs e)
        {
            foreach (var item in this.splitCnt_Chart.Panel1.Controls)
            {
                Control c = (Control)item;
                c.Height = splitCnt_Chart.Panel1.Height;
                c.Width = splitCnt_Chart.Panel1.Width;
            }
        }

        /// <summary>
        /// Loading the Graph;
        /// </summary>
        /// <param name="lineSeries1">1. Oxyplot LineSeries to be displayed;</param>
        /// <param name="lineSeries2">2. Oxyplot LineSeries to be displayed;</param>
        private void LoadChart(LineSeries lineSeries1, LineSeries lineSeries2)
        {
            this.splitCnt_Chart.Panel1.Controls.Clear();
            PlotView pv = new PlotView();
            PlotModel plotModel = new PlotModel { };
            pv.Location = new Point(0, 0);
            pv.Size = new Size(splitCnt_Chart.Panel1.Width, splitCnt_Chart.Panel1.Height);
            this.splitCnt_Chart.Panel1.Controls.Add(pv);
            pv.Model = new PlotModel { Title = "DashBoard" };
            pv.Model.Legends.Add(new Legend() { LegendTitle = "Stats:", LegendPosition = LegendPosition.TopLeft });

            pv.ActualModel.Series.Add(lineSeries1);
            pv.ActualModel.Series.Add(lineSeries2);


            //CategoryAxis tourneys = new CategoryAxis();
            //pv.ActualModel.Axes.Add(tourneys);
            //tourneys.MajorStep = 5;

            pv.Refresh();
            pv.Invalidate();
            pv.ActualModel.InvalidatePlot(true);

        }

        /// <summary>
        /// LineSeries plotting Data to the Oxyplot Graph;
        /// </summary>
        /// <param name="cevList">A List containing cEV / Tournament</param>
        /// <param name="minTourneys"> Minimum Tournaments/Period that will be shown; Not implemented;</param>
        /// <param name="title">Title, shown in the Legend</param>
        /// <param name="tag">1 - Tournament Number, 2 - Chips Won, 3 - cEV, default - aBB/ Situation</param>
        /// <returns>LineSeries to be plotted in OxyPlot Graph;</returns>
        private LineSeries PlotDataFromCevModelList(List<CevModel> cevList, int minTourneys, string title, int tag)
        {
            LineSeries lineSeries = new LineSeries();
            lineSeries.Tag = tag;
            lineSeries.Title = title;

            double currPlot = 0;
            for (int i = 0; i < cevList.Count; i++)
            {
                switch (tag)
                {
                    case 1:
                        currPlot = cevList[i].Count_tourneys;
                        break;
                    case 2:
                        currPlot += cevList[i].Amt_won;
                        break;
                    case 3:
                        currPlot += cevList[i].Cev;
                        break;

                    default:
                        currPlot = cevList[i].Abb / cevList[i].Situations;
                        break;
                }

                lineSeries.Points.Add(new DataPoint(i, currPlot));

            }
            return lineSeries;
        }

        /// <summary>
        /// Calculates the Table Layout Panel;
        /// </summary>
        /// <param name="dashBoardModel">DashBoard, that will be diplayed;</param>
        /// <param name="_minTourneys">Minimum Tournaments/Period that will be shown; Not implemented;</param>
        private void tlp_overview_Calculate(DashBoardModel dashBoardModel, int _minTourneys)
        {
            tlp_overview.SuspendLayout();

            tlp_overview_CalculateAverage(dashBoardModel, 4);

            // Looping trough Period in the TOTAL SQL Query and create new Row filling in every column with the data from the related query;
            int currRow = 5;
            for (int i = 0; i < dashBoardModel.CevModel_Total_ByMonth.Count; i++)
            {

                string currPeriod = dashBoardModel.CevModel_Total_ByMonth[i].T_Date.ToString("MMMM.yy");

                if (dashBoardModel.CevModel_Total_ByMonth[i].Count_tourneys > _minTourneys)
                {
                    AddNewRowToTlpOverview(ref currRow);

                    //tlpOverview_Month
                    tlp_overview.GetControlFromPosition(0, currRow).Text = dashBoardModel.CevModel_Total_ByMonth[i].T_Date.ToString("yy-MMM").ToUpper();

                    //tlpOverview_TourneysPlayed
                    tlp_overview.GetControlFromPosition(1, currRow).Text = dashBoardModel.CevModel_Total_ByMonth[i].Count_tourneys.ToString();

                    //tlpOverview_AverageBI
                    tlp_overview.GetControlFromPosition(2, currRow).Text = dashBoardModel.CevModel_Total_ByMonth[i].Abi.ToString("F1");

                    //tlpOverview_Cev/t
                    tlp_overview.GetControlFromPosition(3, currRow).Text = (Double.Parse(dashBoardModel.CevModel_Total_ByMonth[i].Cev.ToString()) / int.Parse(dashBoardModel.CevModel_Total_ByMonth[i].Count_tourneys.ToString())).ToString("F2");

                    //tlpOverview_3W_BTNvBBvREG
                    CalculateTlpOverviewAbbPerMonth(dashBoardModel.CevModel_3W_SBvBTNvREG, ref currPeriod, ref currRow, 4);

                    //tlpOverview_3W_BTNvBBvFISH
                    CalculateTlpOverviewAbbPerMonth(dashBoardModel.CevModel_3W_BTNvBBvFISH, ref currPeriod, ref currRow, 5);

                    //tlpOverview_3W_SBvBTNvREG
                    CalculateTlpOverviewAbbPerMonth(dashBoardModel.CevModel_3W_SBvBTNvREG, ref currPeriod, ref currRow, 6);

                    //tlpOverview_3W_SBvBTNvFISH
                    CalculateTlpOverviewAbbPerMonth(dashBoardModel.CevModel_3W_SBvBTNvFISH, ref currPeriod, ref currRow, 7);

                    //tlpOverview_3W_BBvBTNvREG
                    CalculateTlpOverviewAbbPerMonth(dashBoardModel.CevModel_3W_BBvBTNvREG, ref currPeriod, ref currRow, 8);

                    //tlpOverview_3W_BBvBTNvFISH
                    CalculateTlpOverviewAbbPerMonth(dashBoardModel.CevModel_3W_BBvBTNvFISH, ref currPeriod, ref currRow, 9);

                    //tlpOverview_3W_SBvBBvREG
                    CalculateTlpOverviewAbbPerMonth(dashBoardModel.CevModel_3W_SBvBBvREG, ref currPeriod, ref currRow, 10);

                    //tlpOverview_3W_SBvBBvFISH
                    CalculateTlpOverviewAbbPerMonth(dashBoardModel.CevModel_3W_SBvBBvFISH, ref currPeriod, ref currRow, 11);

                    //tlpOverview_3W_BBvSBvREG
                    CalculateTlpOverviewAbbPerMonth(dashBoardModel.CevModel_3W_BBvSBvREG, ref currPeriod, ref currRow, 12);

                    //tlpOverview_3W_BBvSBvFISH
                    CalculateTlpOverviewAbbPerMonth(dashBoardModel.CevModel_3W_BBvSBvFISH, ref currPeriod, ref currRow, 13);

                    //tlpOverview_HU_SBvBBvREG
                    CalculateTlpOverviewAbbPerMonth(dashBoardModel.CevModel_HU_SBvREG, ref currPeriod, ref currRow, 14);

                    //tlpOverview_HU_SBvBBvFISH
                    CalculateTlpOverviewAbbPerMonth(dashBoardModel.CevModel_HU_SBvFISH, ref currPeriod, ref currRow, 15);

                    //tlpOverview_HU_BBvSBvREG
                    CalculateTlpOverviewAbbPerMonth(dashBoardModel.CevModel_HU_BBvREG, ref currPeriod, ref currRow, 16);

                    //tlpOverview_HU_BBvSBvFISH
                    CalculateTlpOverviewAbbPerMonth(dashBoardModel.CevModel_HU_BBvFISH, ref currPeriod, ref currRow, 17);

                    //tlpOverview_F/R Ratio
                    CalculateTlpOverviewFishRegRatioPerMonth(ref dashBoardModel, ref currPeriod, ref currRow, 18);
                    currRow++;
                }
            }
            tlp_overview.RowCount += 1; //Creating new row;

            TableLayoutRowStyleCollection rowStyles = tlp_overview.RowStyles;
            TableLayoutColumnStyleCollection columnStyles = tlp_overview.ColumnStyles;

            // Resize Rows;
            foreach (RowStyle style in rowStyles)
            {
                style.SizeType = SizeType.Absolute;
                style.Height = 50;
            }

            tlp_overview.ResumeLayout();
        }

        /// <summary>
        /// Calculating the average Data of all Periods and all Columns;
        /// </summary>
        /// <param name="dashBoardModel">DashBoard, that will be diplayed;</param>
        /// <param name="averageRow">Number of the Row, where the info will e shown;</param>
        private void tlp_overview_CalculateAverage(DashBoardModel dashBoardModel, int averageRow)
        {


            //tlpOverview_TourneysPlayed
            tlp_overview.GetControlFromPosition(1, averageRow).Text = (dashBoardModel.CevModel_Total_ByMonth.Sum(model => model.Count_tourneys) / dashBoardModel.CevModel_Total_ByMonth.Count).ToString();

            //tlpOverview_AverageBI
            tlp_overview.GetControlFromPosition(2, averageRow).Text = (Double.Parse(dashBoardModel.CevModel_Total_ByTournament.Sum(model => model.Abi).ToString()) / dashBoardModel.CevModel_Total_ByTournament.Count).ToString("F1");

            //tlpOverview_Cev/t
            tlp_overview.GetControlFromPosition(3, averageRow).Text = (Double.Parse(dashBoardModel.CevModel_Total_ByTournament.Sum(model => model.Cev).ToString()) / dashBoardModel.CevModel_Total_ByTournament.Count).ToString("F2");

            //tlpOverview_3W_BTNvBBvREG
            CalculateTlpOverviewAbbAverage(dashBoardModel.CevModel_3W_SBvBTNvREG, ref averageRow, 4);

            //tlpOverview_3W_BTNvBBvFISH
            CalculateTlpOverviewAbbAverage(dashBoardModel.CevModel_3W_BTNvBBvFISH, ref averageRow, 5);

            //tlpOverview_3W_SBvBTNvREG
            CalculateTlpOverviewAbbAverage(dashBoardModel.CevModel_3W_SBvBTNvREG, ref averageRow, 6);

            //tlpOverview_3W_SBvBTNvFISH
            CalculateTlpOverviewAbbAverage(dashBoardModel.CevModel_3W_SBvBTNvFISH, ref averageRow, 7);

            //tlpOverview_3W_BBvBTNvREG
            CalculateTlpOverviewAbbAverage(dashBoardModel.CevModel_3W_BBvBTNvREG, ref averageRow, 8);

            //tlpOverview_3W_BBvBTNvFISH
            CalculateTlpOverviewAbbAverage(dashBoardModel.CevModel_3W_BBvBTNvFISH, ref averageRow, 9);

            //tlpOverview_3W_SBvBBvREG
            CalculateTlpOverviewAbbAverage(dashBoardModel.CevModel_3W_SBvBBvREG, ref averageRow, 10);

            //tlpOverview_3W_SBvBBvFISH
            CalculateTlpOverviewAbbAverage(dashBoardModel.CevModel_3W_SBvBBvFISH, ref averageRow, 11);

            //tlpOverview_3W_BBvSBvREG
            CalculateTlpOverviewAbbAverage(dashBoardModel.CevModel_3W_BBvSBvREG, ref averageRow, 12);

            //tlpOverview_3W_BBvSBvFISH
            CalculateTlpOverviewAbbAverage(dashBoardModel.CevModel_3W_BBvSBvFISH, ref averageRow, 13);

            //tlpOverview_HU_SBvBBvREG
            CalculateTlpOverviewAbbAverage(dashBoardModel.CevModel_HU_SBvREG, ref averageRow, 14);

            //tlpOverview_HU_SBvBBvFISH
            CalculateTlpOverviewAbbAverage(dashBoardModel.CevModel_HU_SBvFISH, ref averageRow, 15);

            //tlpOverview_HU_BBvSBvREG
            CalculateTlpOverviewAbbAverage(dashBoardModel.CevModel_HU_BBvREG, ref averageRow, 16);

            //tlpOverview_HU_BBvSBvFISH
            CalculateTlpOverviewAbbAverage(dashBoardModel.CevModel_HU_BBvFISH, ref averageRow, 17);

            //tlpOverview_F/R Ratio
            CalculateTlpOverviewFishRegRatioAverage(ref dashBoardModel, ref averageRow, 18);

        }

        /// <summary>
        /// Calculating the average Data of all Periods for Columns showing aBB;
        /// </summary>
        /// <param name="cevModels">List of CevModel, that will be displayed;</param>
        /// <param name="averageRow">Row, where the Info will be displayed</param>
        /// <param name="currCol">Column, where the Info will be displayed</param>
        private void CalculateTlpOverviewAbbAverage(List<CevModel> cevModels, ref int averageRow, int currCol)
        {
            tlp_overview.GetControlFromPosition(currCol, averageRow).Text = Double.Parse((cevModels.Sum(model => model.Abb) / cevModels.Sum(model => model.Situations)).ToString()).ToString("F2");
            tlp_overview.GetControlFromPosition(currCol, averageRow).MouseEnter += new System.EventHandler(this.tlp_MouseEnter);
            tlp_overview.GetControlFromPosition(currCol, averageRow).MouseLeave += new System.EventHandler(this.tlp_MouseLeave);
        }

        /// <summary>
        /// Calculating the average Fish/Reg Ratio;
        /// </summary>
        /// <param name="dashBoardModel">DashBoard, that will be diplayed;</param>
        /// <param name="averageRow">Row, where the Info will be displayed</param>
        /// <param name="currCol">Column, where the Info will be displayed</param>
        private void CalculateTlpOverviewFishRegRatioAverage(ref DashBoardModel dashBoardModel, ref int averageRow, int currCol)
        {
            tlp_overview.GetControlFromPosition(currCol, averageRow).Text = (dashBoardModel.CevModel_HU_SBvFISH.Sum(model => model.Situations) * 100 / (dashBoardModel.CevModel_HU_SBvFISH.Sum(model => model.Situations) + dashBoardModel.CevModel_HU_SBvREG.Sum(model => model.Situations))).ToString("F0");
            tlp_overview.GetControlFromPosition(currCol, averageRow).MouseEnter += new System.EventHandler(this.tlp_MouseEnter);
            tlp_overview.GetControlFromPosition(currCol, averageRow).MouseLeave += new System.EventHandler(this.tlp_MouseLeave);
        }

        /// <summary>
        /// Adds a new Row to the TableLayOut Panel;
        /// </summary>
        /// <param name="currRow">Number of last created Row;</param>
        private void AddNewRowToTlpOverview(ref int currRow)
        {
            tlp_overview.RowCount += 1;
            tlp_overview.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));

            for (int j = 0; j < tlp_overview.ColumnCount; j++)
            {
                Label lblTempalte = new Label();
                lblTempalte.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                tlp_overview.Controls.Add(lblTempalte, j, currRow);
                //tlp_overview.GetControlFromPosition(j,i).Controls.Add(lblTempalte);
                tlp_overview.GetControlFromPosition(j, currRow).Dock = System.Windows.Forms.DockStyle.Fill;
            }
        }

        /// <summary>
        /// Calculating and displaying aBB for a single Period;
        /// </summary>
        /// <param name="cevModels">CevModel, that will be diplayed;</param>
        /// <param name="currPeriod">Period, that will be displayed;</param>
        /// <param name="currRow">Row, where the Info will be displayed</param>
        /// <param name="currCol">Column, where the Info will be displayed</param>
        private void CalculateTlpOverviewAbbPerMonth(List<CevModel> cevModels, ref string currPeriod, ref int currRow, int currCol)
        {
            foreach (var month in cevModels)
            {
                if (currPeriod == month.T_Date.ToString("MMMM.yy"))
                {
                    tlp_overview.GetControlFromPosition(currCol, currRow).Text = Double.Parse((month.Abb / month.Situations).ToString()).ToString("F2");
                    tlp_overview.GetControlFromPosition(currCol, currRow).MouseEnter += new System.EventHandler(this.tlp_MouseEnter);
                    tlp_overview.GetControlFromPosition(currCol, currRow).MouseLeave += new System.EventHandler(this.tlp_MouseLeave);
                    break;
                }
            }
        }

        /// <summary>
        /// Calculating and displaying FISH/REG Ratio for a single Period;
        /// </summary>
        /// <param name="dashBoardModel">DashBoard, that will be diplayed;</param>
        /// <param name="currPeriod">Period, that will be displayed;</param>
        /// <param name="currRow">Row, where the Info will be displayed</param>
        /// <param name="currCol">Column, where the Info will be displayed</param>
        private void CalculateTlpOverviewFishRegRatioPerMonth(ref DashBoardModel dashBoardModel, ref string currPeriod, ref int currRow, int currCol)
        {
            foreach (var month_L1 in dashBoardModel.CevModel_HU_SBvFISH)
            {
                if (currPeriod == month_L1.T_Date.ToString("MMMM.yy"))
                {

                    foreach (var month_L2 in dashBoardModel.CevModel_HU_SBvREG)
                    {
                        if (currPeriod == month_L2.T_Date.ToString("MMMM.yy"))
                        {
                            tlp_overview.GetControlFromPosition(18, currRow).Text = Double.Parse((month_L1.Situations * 100 / (month_L1.Situations + month_L2.Situations)).ToString()).ToString("F0");
                            break;
                        }
                        else
                        {
                            tlp_overview.GetControlFromPosition(18, currRow).Text = " - ";
                        }
                    }
                    break;
                }
            }
        }
    }
}
