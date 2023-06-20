using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Legends;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrackerLibrary;
using TrackerLibrary.CRUD;
using TrackerLibrary.Models;
using TrackerLibrary.Queries.NoSQL;
using TrackerLibrary.Queries;


namespace TrackerUI.ChildForms
{

    public partial class CEV : Form
    {
        DateTime startTime = DateTime.Now;

        int minTourneys = 500;


        private PlotView pv = new PlotView();

        

        private string regList = "reglist.txt";

        private List<CevModel> cevRequested = new List<CevModel>();

        private List<CevModel> tlpOverview_General = new List<CevModel>();

        private List<CevModel> tlpOverview_3W_BTNvBBvREG = new List<CevModel>();

        private List<CevModel> tlpOverview_3W_BTNvBBvFISH = new List<CevModel>();

        private List<CevModel> tlpOverview_3W_SBvBTNvREG = new List<CevModel>();

        private List<CevModel> tlpOverview_3W_SBvBTNvFISH = new List<CevModel>();

        private List<CevModel> tlpOverview_3W_BBvBTNvREG = new List<CevModel>();

        private List<CevModel> tlpOverview_3W_BBvBTNvFISH = new List<CevModel>();

        private List<CevModel> tlpOverview_3W_SBvBBvREG = new List<CevModel>();

        private List<CevModel> tlpOverview_3W_SBvBBvFISH = new List<CevModel>();

        private List<CevModel> tlpOverview_3W_BBvSBvREG = new List<CevModel>();

        private List<CevModel> tlpOverview_3W_BBvSBvFISH = new List<CevModel>();



        private List<CevModel> tlpOverview_HU_SBvBBvREG = new List<CevModel>();

        private List<CevModel> tlpOverview_HU_SBvBBvFISH = new List<CevModel>();

        private List<CevModel> tlpOverview_HU_BBvSBvREG = new List<CevModel>();

        private List<CevModel> tlpOverview_HU_BBvSBvFISH = new List<CevModel>();


        private void CalculateCevRequested()
        {
            cevRequested = NoSQL_Connector.GetCevByPosParallel("IPray2Buddha", "3-max", QueryMethods.ConcatQueries(cevRequestQueries.sql_ExportCevPerTournamentAsJSON, cevRequestQueries.sql_cevRequestGeneral_test));
        }

        private void CalculateTlpOverview_General()
        {
            tlpOverview_General = NoSQL_Connector.GetCevByPosParallel("IPray2Buddha", "3-max", cevRequestQueries.sql_ExportTlpOverviewAsJSON_GeneralInfo_GroupByMonths);
        }

        private void CalculateTlpOverview_3W_BTNvBBvREG()
        {
            tlpOverview_3W_BTNvBBvREG = NoSQL_Connector.GetCevByPosParallel("3-max", "0", QueryMethods.ConcatQueries(cevRequestQueries.sql_ExportTlpOverviewAsJSON_CevByPos_GroupByMonths, "3W", UIMethods.ReadRegList(regList, true), "bbSeatActionId"));
        }
        private void CalculateTlpOverview_3W_BTNvBBvFISH()
        {
            tlpOverview_3W_BTNvBBvFISH = NoSQL_Connector.GetCevByPosParallel("3-max", "0", QueryMethods.ConcatQueries(cevRequestQueries.sql_ExportTlpOverviewAsJSON_CevByPos_GroupByMonths, "3W", UIMethods.ReadRegList(regList, false), "bbSeatActionId"));
        }
        private void CalculateTlpOverview_3W_SBvBTNvREG()
        {
            tlpOverview_3W_SBvBTNvREG = NoSQL_Connector.GetCevByPosParallel("3-max", "8", QueryMethods.ConcatQueries(cevRequestQueries.sql_ExportTlpOverviewAsJSON_CevByPos_GroupByMonths, "3W", UIMethods.ReadRegList(regList, true), "btnSeatActionId"));
        }
        private void CalculateTlpOverview_3W_SBvBTNvFISH()
        {
            tlpOverview_3W_SBvBTNvFISH = NoSQL_Connector.GetCevByPosParallel("3-max", "8", QueryMethods.ConcatQueries(cevRequestQueries.sql_ExportTlpOverviewAsJSON_CevByPos_GroupByMonths, "3W", UIMethods.ReadRegList(regList, false), "btnSeatActionId"));
        }
        private void CalculateTlpOverview_3W_BBvBTNvREG()
        {
            tlpOverview_3W_BBvBTNvREG = NoSQL_Connector.GetCevByPosParallel("3-max", "9", QueryMethods.ConcatQueries(cevRequestQueries.sql_ExportTlpOverviewAsJSON_CevByPos_GroupByMonths, "3W", UIMethods.ReadRegList(regList, true), "btnSeatActionId"));
        }
        private void CalculateTlpOverview_3W_BBvBTNvFISH()
        {
            tlpOverview_3W_BBvBTNvFISH = NoSQL_Connector.GetCevByPosParallel("3-max", "9", QueryMethods.ConcatQueries(cevRequestQueries.sql_ExportTlpOverviewAsJSON_CevByPos_GroupByMonths, "3W", UIMethods.ReadRegList(regList, false), "btnSeatActionId"));
        }
        private void CalculateTlpOverview_3W_SBvBBvREG()
        {
            tlpOverview_3W_SBvBBvREG = NoSQL_Connector.GetCevByPosParallel("3-max", "8", QueryMethods.ConcatQueries(cevRequestQueries.sql_ExportTlpOverviewAsJSON_CevByPos_GroupByMonths, "3W", UIMethods.ReadRegList(regList, true), "bbSeatActionId"));
        }
        private void CalculateTlpOverview_3W_SBvBBvFISH()
        {
            tlpOverview_3W_SBvBBvFISH = NoSQL_Connector.GetCevByPosParallel("3-max", "8", QueryMethods.ConcatQueries(cevRequestQueries.sql_ExportTlpOverviewAsJSON_CevByPos_GroupByMonths, "3W", UIMethods.ReadRegList(regList, false), "bbSeatActionId"));
        }
        private void CalculateTlpOverview_3W_BBvSBvREG()
        {
            tlpOverview_3W_BBvSBvREG = NoSQL_Connector.GetCevByPosParallel("3-max", "9", QueryMethods.ConcatQueries(cevRequestQueries.sql_ExportTlpOverviewAsJSON_CevByPos_GroupByMonths, "3W", UIMethods.ReadRegList(regList, true), "sbSeatActionId"));
        }
        private void CalculateTlpOverview_3W_BBvSBvFISH()
        {
            tlpOverview_3W_BBvSBvFISH = NoSQL_Connector.GetCevByPosParallel("3-max", "9", QueryMethods.ConcatQueries(cevRequestQueries.sql_ExportTlpOverviewAsJSON_CevByPos_GroupByMonths, "3W", UIMethods.ReadRegList(regList, false), "sbSeatActionId"));
        }


        private void CalculateTlpOverview_HU_SBvBBvREG()
        {
            tlpOverview_HU_SBvBBvREG = NoSQL_Connector.GetCevByPosParallel("3-max", "8", QueryMethods.ConcatQueries(cevRequestQueries.sql_ExportTlpOverviewAsJSON_CevByPos_GroupByMonths, "HU", UIMethods.ReadRegList(regList, true), "bbSeatActionId"));
        }
        private void CalculateTlpOverview_HU_SBvBBvFISH()
        {
            tlpOverview_HU_SBvBBvFISH = NoSQL_Connector.GetCevByPosParallel("3-max", "8", QueryMethods.ConcatQueries(cevRequestQueries.sql_ExportTlpOverviewAsJSON_CevByPos_GroupByMonths, "HU", UIMethods.ReadRegList(regList, false), "bbSeatActionId"));
        }
        private void CalculateTlpOverview_HU_BBvSBvREG()
        {
            tlpOverview_HU_BBvSBvREG = NoSQL_Connector.GetCevByPosParallel("3-max", "9", QueryMethods.ConcatQueries(cevRequestQueries.sql_ExportTlpOverviewAsJSON_CevByPos_GroupByMonths, "HU", UIMethods.ReadRegList(regList, true), "sbSeatActionId"));
        }
        private void CalculateTlpOverview_HU_BBvSBvFISH()
        {
            tlpOverview_HU_BBvSBvFISH = NoSQL_Connector.GetCevByPosParallel("3-max", "9", QueryMethods.ConcatQueries(cevRequestQueries.sql_ExportTlpOverviewAsJSON_CevByPos_GroupByMonths, "HU", UIMethods.ReadRegList(regList, false), "sbSeatActionId"));
        }
        public CEV()
        {

            ////w. Tasks:
            Task t1 = Task.Run(CalculateCevRequested);
            Task t2 = Task.Run(CalculateTlpOverview_General);
            Task t3 = Task.Run(CalculateTlpOverview_3W_BTNvBBvREG);
            Task t4 = Task.Run(CalculateTlpOverview_3W_BTNvBBvFISH);
            Task t5 = Task.Run(CalculateTlpOverview_3W_SBvBTNvREG);
            Task t6 = Task.Run(CalculateTlpOverview_3W_SBvBTNvFISH);
            Task t7 = Task.Run(CalculateTlpOverview_3W_BBvBTNvREG);
            Task t8 = Task.Run(CalculateTlpOverview_3W_BBvBTNvFISH);
            Task t9 = Task.Run(CalculateTlpOverview_3W_SBvBBvREG);
            Task t10 = Task.Run(CalculateTlpOverview_3W_SBvBBvFISH);
            Task t11 = Task.Run(CalculateTlpOverview_3W_BBvSBvREG);
            Task t12 = Task.Run(CalculateTlpOverview_3W_BBvSBvFISH);

            Task t13 = Task.Run(CalculateTlpOverview_HU_SBvBBvREG);
            Task t14 = Task.Run(CalculateTlpOverview_HU_SBvBBvFISH);
            Task t15 = Task.Run(CalculateTlpOverview_HU_BBvSBvREG);
            Task t16 = Task.Run(CalculateTlpOverview_HU_BBvSBvFISH);


            InitializeComponent();
            t1.Wait();
            t2.Wait();
            t3.Wait();
            t4.Wait();
            t5.Wait();
            t6.Wait();
            t7.Wait();
            t8.Wait();
            t9.Wait();
            t10.Wait();
            t11.Wait();
            t12.Wait();
            t13.Wait();
            t14.Wait();
            t15.Wait();
            t16.Wait();

            //loadChart(ReturnCev(OxyColors.DeepPink, "aBB..."));
            
            //loadChart(PlotDataFromCevModelList(tlpOverview_General, "CEV/t", 3), PlotDataFromCevModelList_CA(tlpOverview_General));
            tlp_overview_Calculate();
            loadChart();
            cBox_3_3.Checked = true;


            ////w. Threads:
            //Thread t1 = new Thread(CalculateCevRequested);
            //Thread t2 = new Thread(CalculateTlpOverview_General);
            //Thread t3 = new Thread(CalculateTlpOverview_BTNvBBvREG);
            //Thread t4 = new Thread(CalculateTlpOverview_SBvBTNvREG);
            //Thread t5 = new Thread(CalculateTlpOverview_BBvBTNvREG);
            //Thread t6 = new Thread(CalculateTlpOverview_SBvBBvREG);
            //Thread t7 = new Thread(CalculateTlpOverview_BBvSBvREG);
            //t1.Start();
            //t2.Start();
            //t3.Start();
            //t4.Start();
            //t5.Start();
            //t6.Start();
            //t7.Start();

            //InitializeComponent();
            //t1.Join();
            //t2.Join();
            //t3.Join();
            //t4.Join();
            //t5.Join();
            //t6.Join();
            //t7.Join();
            //loadChart();
            //tlp_overview_Calculate();


            ////wo. Tasks
            //CalculateCevRequested();
            //CalculateTlpOverview_General();
            //CalculateTlpOverview_3W_BTNvBBvREG();
            //CalculateTlpOverview_3W_BTNvBBvFISH();
            //CalculateTlpOverview_3W_SBvBTNvREG();
            //CalculateTlpOverview_3W_SBvBTNvFISH();
            //CalculateTlpOverview_3W_BBvBTNvREG();
            //CalculateTlpOverview_3W_BBvBTNvFISH();
            //CalculateTlpOverview_3W_SBvBBvREG();
            //CalculateTlpOverview_3W_SBvBBvFISH();
            //CalculateTlpOverview_3W_BBvSBvREG();
            //CalculateTlpOverview_3W_BBvSBvFISH();

            //CalculateTlpOverview_HU_SBvBBvREG();
            //CalculateTlpOverview_HU_SBvBBvFISH();
            //CalculateTlpOverview_HU_BBvSBvREG();
            //CalculateTlpOverview_HU_BBvSBvFISH();



            //InitializeComponent();
            //loadChart();
            //tlp_overview_Calculate();

            // calculate time:
            var calculatingTime = (DateTime.Now - startTime).TotalSeconds;
            lbl_calcTime.Text = calculatingTime.ToString();
        }



        private LineSeries PlotDataFromCevModelList(List<CevModel> _cevModelList, string _title, int _tag)
        {


            LineSeries lineSeries = new LineSeries();
            lineSeries.Tag = _tag;
            lineSeries.Title = _title;




            double currPlot = 0;
            for (int i = _cevModelList.Count - 1; i >= 0; i--)
            {
                if (tlpOverview_General[i].Count_tourney > minTourneys)
                {
                    switch (_tag)
                    {
                        case 1:
                            currPlot = _cevModelList[i].Count_tourney;
                            break;
                        case 2:
                            currPlot = _cevModelList[i].Abi;
                            break;
                        case 3:
                            currPlot = _cevModelList[i].Cev / _cevModelList[i].Count_tourney;
                            break;

                        default:
                            currPlot = _cevModelList[i].aBB / _cevModelList[i].Situations;
                            break;
                    }
                }
                lineSeries.Points.Add(new DataPoint(i + 1, currPlot));

            }




            return lineSeries;
        }

        private LinearBarSeries PlotDataFromCevModelList_LBA(List<CevModel> _cevModelList, string _title, int _tag)
        {


            LinearBarSeries linearBarSeries = new LinearBarSeries();
            linearBarSeries.Tag = _tag;
            linearBarSeries.Title = _title;
            linearBarSeries.FillColor = OxyColors.Coral;
            linearBarSeries.BarWidth = 30;



            double currPlot = 0;
            for (int i = _cevModelList.Count - 1; i >= 0; i--)
            {
                if (tlpOverview_General[i].Count_tourney > minTourneys)
                {
                    switch (_tag)
                    {
                        case 1:
                            currPlot = _cevModelList[i].Count_tourney;
                            break;
                        case 2:
                            currPlot = _cevModelList[i].Abi;
                            break;
                        case 3:
                            currPlot = _cevModelList[i].Cev / _cevModelList[i].Count_tourney;
                            break;

                        default:
                            currPlot = _cevModelList[i].aBB / _cevModelList[i].Situations;
                            break;
                    }
                }
                linearBarSeries.Points.Add(new DataPoint(i + 1, currPlot));

            }
            return linearBarSeries;
        }

        private CategoryAxis PlotDataFromCevModelList_CA(List<CevModel> _cevModelList)
        {


            var categoryAxis = new CategoryAxis()
            {
                MajorStep = 3,
                Position = AxisPosition.Bottom

            };



            for (int i = _cevModelList.Count - 1; i >= 0; i--)
            {
                categoryAxis.ActualLabels.Add(_cevModelList[i].Min_Date.ToString("yy-MMM").ToUpper());
            }

            return categoryAxis;
        }

        //private LinearBarSeries ReturnCevByPos(OxyColor color, string title)
        //{
        //    //List<BarItem> barItems = new List<BarItem>();
        //    OxyPlot.Series.LinearBarSeries output = new LinearBarSeries();

        //    output.FillColor = color;
        //    output.Title = title;
        //    output.BarWidth = 40;

        //    double currPlot = 0;
        //    for (int i = 0; i < tlpOverview_3W_BTNvBBvREG.Count; i++)
        //    {
        //        currPlot = tlpOverview_3W_BTNvBBvREG[i].aBB / tlpOverview_3W_BTNvBBvREG[i].Situations;
        //        //barItems.Add(new BarItem() { Value = currPlot });
        //        //output.ItemsSource = barItems;

        //        output.Points.Add(new DataPoint(i + 1, currPlot));
        //    }

        //    return output;
        //}



        private void loadChart(LineSeries _displayLineSeries, LinearAxis _linearAxis)
        {

            PlotModel plotModel = new PlotModel { };
            pv.Location = new Point(0, 0);
            pv.Size = new Size(splitCnt_Chart.Panel1.Width, splitCnt_Chart.Panel1.Height);
            this.splitCnt_Chart.Panel1.Controls.Add(pv);

            pv.Model = new PlotModel { Title = "cEV/T" };

            pv.Model.Series.Add(_displayLineSeries);
            pv.Model.Axes.Add(_linearAxis);
            pv.Model.Legends.Add(new Legend() { LegendTitle = "Stats:", LegendPosition = LegendPosition.TopLeft });

        }

        private void loadChart(LinearBarSeries _displayLineSeries, LinearAxis _linearAxis)
        {

            PlotModel plotModel = new PlotModel { };
            pv.Location = new Point(0, 0);
            pv.Size = new Size(splitCnt_Chart.Panel1.Width, splitCnt_Chart.Panel1.Height);
            this.splitCnt_Chart.Panel1.Controls.Add(pv);

            pv.Model = new PlotModel { Title = "cEV/T" };

            pv.Model.Series.Add(_displayLineSeries);
            pv.Model.Axes.Add(_linearAxis);

            pv.Model.Legends.Add(new Legend() { LegendTitle = "Stats:", LegendPosition = LegendPosition.TopLeft });

        }

        private void loadChart()
        {

            PlotModel plotModel = new PlotModel { };
            pv.Location = new Point(0, 0);
            pv.Size = new Size(splitCnt_Chart.Panel1.Width, splitCnt_Chart.Panel1.Height);
            this.splitCnt_Chart.Panel1.Controls.Add(pv);

            pv.Model = new PlotModel { Title = "DashBoard" };

            pv.Model.Legends.Add(new Legend() { LegendTitle = "Stats:", LegendPosition = LegendPosition.TopLeft });

        }


        private void CEV_Resize(object sender, EventArgs e)
        {
            splitCnt_Chart_Panel1_Resize(sender, e);
        }

        private void splitCnt_Chart_Panel1_Resize(object sender, EventArgs e)
        {
            foreach (var item in this.splitCnt_Chart.Panel1.Controls)
            {
                Control c = (Control)item;
                c.Height = splitCnt_Chart.Panel1.Height;
                c.Width = splitCnt_Chart.Panel1.Width;
            }

        }

        private void tlp_overview_CalculateAverageStats(int _row)
        {
            //Genearl col: 0-3
            int cntTourneys = 0;
            double totBI = 0;
            double totCev = 0;
            for (int i = 0; i < tlpOverview_General.Count; i++)
            {
                cntTourneys += tlpOverview_General[i].Count_tourney;
                totBI += (tlpOverview_General[i].Abi * tlpOverview_General[i].Count_tourney);
                totCev += tlpOverview_General[i].Cev;
            }

            tlp_overview.GetControlFromPosition(1, _row).Text = (cntTourneys / tlpOverview_General.Count).ToString();
            tlp_overview.GetControlFromPosition(2, _row).Text = (totBI / cntTourneys).ToString("F1");
            tlp_overview.GetControlFromPosition(3, _row).Text = (totCev / cntTourneys).ToString("F2");

            //tlpOverview_3W_BTNvBBvREG
            double val_3W_BTNvBBvREG = 0;
            int sit_3W_BTNvBBvREG = 0;
            foreach (var month in tlpOverview_3W_BTNvBBvREG)
            {
                val_3W_BTNvBBvREG = month.aBB;
                sit_3W_BTNvBBvREG = month.Situations;
            }
            tlp_overview.GetControlFromPosition(4, _row).Text = (val_3W_BTNvBBvREG / sit_3W_BTNvBBvREG).ToString("F2");

            //tlpOverview_3W_BTNvBBvFISH
            double val_3W_BTNvBBvFISH = 0;
            int sit_3W_BTNvBBvFISH = 0;
            foreach (var month in tlpOverview_3W_BTNvBBvFISH)
            {
                val_3W_BTNvBBvFISH = month.aBB;
                sit_3W_BTNvBBvFISH = month.Situations;
            }
            tlp_overview.GetControlFromPosition(5, _row).Text = (val_3W_BTNvBBvFISH / sit_3W_BTNvBBvFISH).ToString("F2");

            //tlpOverview_3W_SBvBTNvREG
            double val_3W_SBvBTNvREG = 0;
            int sit_3W_SBvBTNvREG = 0;
            foreach (var month in tlpOverview_3W_SBvBTNvREG)
            {
                val_3W_SBvBTNvREG = month.aBB;
                sit_3W_SBvBTNvREG = month.Situations;
            }
            tlp_overview.GetControlFromPosition(6, _row).Text = (val_3W_SBvBTNvREG / sit_3W_SBvBTNvREG).ToString("F2");

            //tlpOverview_3W_SBvBTNvFISH
            double val_3W_SBvBTNvFISH = 0;
            int sit_3W_SBvBTNvFISH = 0;
            foreach (var month in tlpOverview_3W_SBvBTNvFISH)
            {
                val_3W_SBvBTNvFISH = month.aBB;
                sit_3W_SBvBTNvFISH = month.Situations;
            }
            tlp_overview.GetControlFromPosition(7, _row).Text = (val_3W_SBvBTNvFISH / sit_3W_SBvBTNvFISH).ToString("F2");

            //tlpOverview_3W_BBvBTNvREG
            double val_3W_BBvBTNvREG = 0;
            int sit_3W_BBvBTNvREG = 0;
            foreach (var month in tlpOverview_3W_BBvBTNvREG)
            {
                val_3W_BBvBTNvREG = month.aBB;
                sit_3W_BBvBTNvREG = month.Situations;
            }
            tlp_overview.GetControlFromPosition(8, _row).Text = (val_3W_BBvBTNvREG / sit_3W_BBvBTNvREG).ToString("F2");

            //tlpOverview_3W_BBvBTNvFISH
            double val_3W_BBvBTNvFISH = 0;
            int sit_3W_BBvBTNvFISH = 0;
            foreach (var month in tlpOverview_3W_BBvBTNvFISH)
            {
                val_3W_BBvBTNvFISH = month.aBB;
                sit_3W_BBvBTNvFISH = month.Situations;
            }
            tlp_overview.GetControlFromPosition(9, _row).Text = (val_3W_BBvBTNvFISH / sit_3W_BBvBTNvFISH).ToString("F2");

            //tlpOverview_3W_SBvBBvREG
            double val_3W_SBvBBvREG = 0;
            int sit_3W_SBvBBvREG = 0;
            foreach (var month in tlpOverview_3W_SBvBBvREG)
            {
                val_3W_SBvBBvREG = month.aBB;
                sit_3W_SBvBBvREG = month.Situations;
            }
            tlp_overview.GetControlFromPosition(10, _row).Text = (val_3W_SBvBBvREG / sit_3W_SBvBBvREG).ToString("F2");

            //tlpOverview_3W_SBvBBvFISH
            double val_3W_SBvBBvFISH = 0;
            int sit_3W_SBvBBvFISH = 0;
            foreach (var month in tlpOverview_3W_SBvBBvFISH)
            {
                val_3W_SBvBBvFISH = month.aBB;
                sit_3W_SBvBBvFISH = month.Situations;
            }
            tlp_overview.GetControlFromPosition(11, _row).Text = (val_3W_SBvBBvFISH / sit_3W_SBvBBvFISH).ToString("F2");

            //tlpOverview_3W_BBvSBvREG
            double val_3W_BBvSBvREG = 0;
            int sit_3W_BBvSBvREG = 0;
            foreach (var month in tlpOverview_3W_BBvSBvREG)
            {
                val_3W_BBvSBvREG = month.aBB;
                sit_3W_BBvSBvREG = month.Situations;
            }
            tlp_overview.GetControlFromPosition(12, _row).Text = (val_3W_BBvSBvREG / sit_3W_BBvSBvREG).ToString("F2");

            //tlpOverview_3W_BBvSBvFISH
            double val_3W_BBvSBvFISH = 0;
            int sit_3W_BBvSBvFISH = 0;
            foreach (var month in tlpOverview_3W_BBvSBvFISH)
            {
                val_3W_BBvSBvFISH = month.aBB;
                sit_3W_BBvSBvFISH = month.Situations;
            }
            tlp_overview.GetControlFromPosition(13, _row).Text = (val_3W_BBvSBvFISH / sit_3W_BBvSBvFISH).ToString("F2");

            //tlpOverview_HU_SBvBBvREG
            double val_HU_SBvBBvREG = 0;
            int sit_HU_SBvBBvREG = 0;
            foreach (var month in tlpOverview_HU_SBvBBvREG)
            {
                val_HU_SBvBBvREG = month.aBB;
                sit_HU_SBvBBvREG = month.Situations;
            }
            tlp_overview.GetControlFromPosition(14, _row).Text = (val_HU_SBvBBvREG / sit_HU_SBvBBvREG).ToString("F2");

            //tlpOverview_HU_SBvBBvFISH
            double val_HU_SBvBBvFISH = 0;
            int sit_HU_SBvBBvFISH = 0;
            foreach (var month in tlpOverview_HU_SBvBBvFISH)
            {
                val_HU_SBvBBvFISH = month.aBB;
                sit_HU_SBvBBvFISH = month.Situations;
            }
            tlp_overview.GetControlFromPosition(15, _row).Text = (val_HU_SBvBBvFISH / sit_HU_SBvBBvFISH).ToString("F2");

            //tlpOverview_HU_BBvSBvREG
            double val_HU_BBvSBvREG = 0;
            int sit_HU_BBvSBvREG = 0;
            foreach (var month in tlpOverview_HU_BBvSBvREG)
            {
                val_HU_BBvSBvREG = month.aBB;
                sit_HU_BBvSBvREG = month.Situations;
            }
            tlp_overview.GetControlFromPosition(16, _row).Text = (val_HU_BBvSBvREG / sit_HU_BBvSBvREG).ToString("F2");

            //tlpOverview_HU_BBvSBvFISH
            double val_HU_BBvSBvFISH = 0;
            int sit_HU_BBvSBvFISH = 0;
            foreach (var month in tlpOverview_HU_BBvSBvFISH)
            {
                val_HU_BBvSBvFISH = month.aBB;
                sit_HU_BBvSBvFISH = month.Situations;
            }
            tlp_overview.GetControlFromPosition(17, _row).Text = (val_HU_BBvSBvFISH / sit_HU_BBvSBvFISH).ToString("F2");

            //tlpOverview_F/R Ratio
            tlp_overview.GetControlFromPosition(18, _row).Text = (sit_HU_BBvSBvFISH * 100 / (sit_HU_BBvSBvFISH + sit_HU_BBvSBvREG)).ToString("F0");

        }
        private void tlp_overview_Calculate()
        {

            tlp_overview.SuspendLayout();

            tlp_overview_CalculateAverageStats(4);

            int currRow = 5;
            for (int i = 0; i < tlpOverview_General.Count; i++)
            {
                
                if (tlpOverview_General[i].Count_tourney > minTourneys)
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
                    tlp_overview.GetControlFromPosition(0, currRow).Text = tlpOverview_General[i].Min_Date.ToString("yy-MMM").ToUpper();
                    int cntTourneys = int.Parse(tlpOverview_General[i].Count_tourney.ToString());
                    tlp_overview.GetControlFromPosition(1, currRow).Text = cntTourneys.ToString();
                    tlp_overview.GetControlFromPosition(2, currRow).Text = tlpOverview_General[i].Abi.ToString("F1");
                    tlp_overview.GetControlFromPosition(3, currRow).Text = (Double.Parse(tlpOverview_General[i].Cev.ToString()) / cntTourneys).ToString("F2");


                    //tlpOverview_3W_BTNvBBvREG
                    foreach (var month in tlpOverview_3W_BTNvBBvREG)
                    {
                        if (tlpOverview_General[i].Min_Date.ToString("MMMM.yy") == month.Min_Date.ToString("MMMM.yy"))
                        {
                            tlp_overview.GetControlFromPosition(4, currRow).Text = Double.Parse((month.aBB / month.Situations).ToString()).ToString("F2");
                            tlp_overview.GetControlFromPosition(4, currRow).MouseEnter += new System.EventHandler(this.tlp_MouseEnter);
                            tlp_overview.GetControlFromPosition(4, currRow).MouseLeave += new System.EventHandler(this.tlp_MouseLeave);
                            break;
                        }
                    }

                    //tlpOverview_3W_BTNvBBvFISH
                    foreach (var month in tlpOverview_3W_BTNvBBvFISH)
                    {
                        if (tlpOverview_General[i].Min_Date.ToString("MMMM.yy") == month.Min_Date.ToString("MMMM.yy"))
                        {
                            tlp_overview.GetControlFromPosition(5, currRow).Text = Double.Parse((month.aBB / month.Situations).ToString()).ToString("F2");
                            tlp_overview.GetControlFromPosition(5, currRow).MouseEnter += new System.EventHandler(this.tlp_MouseEnter);
                            tlp_overview.GetControlFromPosition(5, currRow).MouseLeave += new System.EventHandler(this.tlp_MouseLeave);
                            break;
                        }
                    }

                    //tlpOverview_3W_SBvBTNvREG
                    foreach (var month in tlpOverview_3W_SBvBTNvREG)
                    {
                        if (tlpOverview_General[i].Min_Date.ToString("MMMM.yy") == month.Min_Date.ToString("MMMM.yy"))
                        {
                            tlp_overview.GetControlFromPosition(6, currRow).Text = Double.Parse((month.aBB / month.Situations).ToString()).ToString("F2");
                            tlp_overview.GetControlFromPosition(6, currRow).MouseEnter += new System.EventHandler(this.tlp_MouseEnter);
                            tlp_overview.GetControlFromPosition(6, currRow).MouseLeave += new System.EventHandler(this.tlp_MouseLeave);
                            break;
                        }
                    }

                    //tlpOverview_3W_SBvBTNvFISH
                    foreach (var month in tlpOverview_3W_SBvBTNvFISH)
                    {
                        if (tlpOverview_General[i].Min_Date.ToString("MMMM.yy") == month.Min_Date.ToString("MMMM.yy"))
                        {
                            tlp_overview.GetControlFromPosition(7, currRow).Text = Double.Parse((month.aBB / month.Situations).ToString()).ToString("F2");
                            tlp_overview.GetControlFromPosition(7, currRow).MouseEnter += new System.EventHandler(this.tlp_MouseEnter);
                            tlp_overview.GetControlFromPosition(7, currRow).MouseLeave += new System.EventHandler(this.tlp_MouseLeave);
                            break;
                        }
                    }

                    //tlpOverview_3W_BBvBTNvREG
                    foreach (var month in tlpOverview_3W_BBvBTNvREG)
                    {
                        if (tlpOverview_General[i].Min_Date.ToString("MMMM.yy") == month.Min_Date.ToString("MMMM.yy"))
                        {
                            tlp_overview.GetControlFromPosition(8, currRow).Text = Double.Parse((month.aBB / month.Situations).ToString()).ToString("F2");
                            tlp_overview.GetControlFromPosition(8, currRow).MouseEnter += new System.EventHandler(this.tlp_MouseEnter);
                            tlp_overview.GetControlFromPosition(8, currRow).MouseLeave += new System.EventHandler(this.tlp_MouseLeave);
                            break;
                        }
                    }

                    //tlpOverview_3W_BBvBTNvFISH
                    foreach (var month in tlpOverview_3W_BBvBTNvFISH)
                    {
                        if (tlpOverview_General[i].Min_Date.ToString("MMMM.yy") == month.Min_Date.ToString("MMMM.yy"))
                        {
                            tlp_overview.GetControlFromPosition(9, currRow).Text = Double.Parse((month.aBB / month.Situations).ToString()).ToString("F2");
                            tlp_overview.GetControlFromPosition(9, currRow).MouseEnter += new System.EventHandler(this.tlp_MouseEnter);
                            tlp_overview.GetControlFromPosition(9, currRow).MouseLeave += new System.EventHandler(this.tlp_MouseLeave);
                            break;
                        }
                    }

                    //tlpOverview_3W_SBvBBvREG
                    foreach (var month in tlpOverview_3W_SBvBBvREG)
                    {
                        if (tlpOverview_General[i].Min_Date.ToString("MMMM.yy") == month.Min_Date.ToString("MMMM.yy"))
                        {
                            tlp_overview.GetControlFromPosition(10, currRow).Text = Double.Parse((month.aBB / month.Situations).ToString()).ToString("F2");
                            tlp_overview.GetControlFromPosition(10, currRow).MouseEnter += new System.EventHandler(this.tlp_MouseEnter);
                            tlp_overview.GetControlFromPosition(10, currRow).MouseLeave += new System.EventHandler(this.tlp_MouseLeave);
                            break;
                        }
                    }

                    //tlpOverview_3W_SBvBBvFISH
                    foreach (var month in tlpOverview_3W_SBvBBvFISH)
                    {
                        if (tlpOverview_General[i].Min_Date.ToString("MMMM.yy") == month.Min_Date.ToString("MMMM.yy"))
                        {
                            tlp_overview.GetControlFromPosition(11, currRow).Text = Double.Parse((month.aBB / month.Situations).ToString()).ToString("F2");
                            tlp_overview.GetControlFromPosition(11, currRow).MouseEnter += new System.EventHandler(this.tlp_MouseEnter);
                            tlp_overview.GetControlFromPosition(11, currRow).MouseLeave += new System.EventHandler(this.tlp_MouseLeave);
                            break;
                        }
                    }

                    //tlpOverview_3W_BBvSBvREG
                    foreach (var month in tlpOverview_3W_BBvSBvREG)
                    {
                        if (tlpOverview_General[i].Min_Date.ToString("MMMM.yy") == month.Min_Date.ToString("MMMM.yy"))
                        {
                            tlp_overview.GetControlFromPosition(12, currRow).Text = Double.Parse((month.aBB / month.Situations).ToString()).ToString("F2");
                            tlp_overview.GetControlFromPosition(12, currRow).MouseEnter += new System.EventHandler(this.tlp_MouseEnter);
                            tlp_overview.GetControlFromPosition(12, currRow).MouseLeave += new System.EventHandler(this.tlp_MouseLeave);
                            break;
                        }
                    }

                    //tlpOverview_3W_BBvSBvFISH
                    foreach (var month in tlpOverview_3W_BBvSBvFISH)
                    {
                        if (tlpOverview_General[i].Min_Date.ToString("MMMM.yy") == month.Min_Date.ToString("MMMM.yy"))
                        {
                            tlp_overview.GetControlFromPosition(13, currRow).Text = Double.Parse((month.aBB / month.Situations).ToString()).ToString("F2");
                            tlp_overview.GetControlFromPosition(13, currRow).MouseEnter += new System.EventHandler(this.tlp_MouseEnter);
                            tlp_overview.GetControlFromPosition(13, currRow).MouseLeave += new System.EventHandler(this.tlp_MouseLeave);
                            break;
                        }
                    }

                    //tlpOverview_HU_SBvBBvREG
                    foreach (var month in tlpOverview_HU_SBvBBvREG)
                    {
                        if (tlpOverview_General[i].Min_Date.ToString("MMMM.yy") == month.Min_Date.ToString("MMMM.yy"))
                        {
                            tlp_overview.GetControlFromPosition(14, currRow).Text = Double.Parse((month.aBB / month.Situations).ToString()).ToString("F2");
                            tlp_overview.GetControlFromPosition(14, currRow).MouseEnter += new System.EventHandler(this.tlp_MouseEnter);
                            tlp_overview.GetControlFromPosition(14, currRow).MouseLeave += new System.EventHandler(this.tlp_MouseLeave);
                            break;
                        }
                    }

                    //tlpOverview_HU_SBvBBvFISH
                    foreach (var month in tlpOverview_HU_SBvBBvFISH)
                    {
                        if (tlpOverview_General[i].Min_Date.ToString("MMMM.yy") == month.Min_Date.ToString("MMMM.yy"))
                        {
                            tlp_overview.GetControlFromPosition(15, currRow).Text = Double.Parse((month.aBB / month.Situations).ToString()).ToString("F2");
                            tlp_overview.GetControlFromPosition(15, currRow).MouseEnter += new System.EventHandler(this.tlp_MouseEnter);
                            tlp_overview.GetControlFromPosition(15, currRow).MouseLeave += new System.EventHandler(this.tlp_MouseLeave);
                            break;
                        }
                    }

                    //tlpOverview_HU_BBvSBvREG
                    foreach (var month in tlpOverview_HU_BBvSBvREG)
                    {
                        if (tlpOverview_General[i].Min_Date.ToString("MMMM.yy") == month.Min_Date.ToString("MMMM.yy"))
                        {
                            tlp_overview.GetControlFromPosition(16, currRow).Text = Double.Parse((month.aBB / month.Situations).ToString()).ToString("F2");
                            tlp_overview.GetControlFromPosition(16, currRow).MouseEnter += new System.EventHandler(this.tlp_MouseEnter);
                            tlp_overview.GetControlFromPosition(16, currRow).MouseLeave += new System.EventHandler(this.tlp_MouseLeave);
                            break;
                        }
                    }

                    //tlpOverview_HU_BBvSBvFISH
                    foreach (var month in tlpOverview_HU_BBvSBvFISH)
                    {
                        if (tlpOverview_General[i].Min_Date.ToString("MMMM.yy") == month.Min_Date.ToString("MMMM.yy"))
                        {
                            tlp_overview.GetControlFromPosition(17, currRow).Text = Double.Parse((month.aBB / month.Situations).ToString()).ToString("F2");
                            tlp_overview.GetControlFromPosition(17, currRow).MouseEnter += new System.EventHandler(this.tlp_MouseEnter);
                            tlp_overview.GetControlFromPosition(17, currRow).MouseLeave += new System.EventHandler(this.tlp_MouseLeave);
                            break;
                        }
                    }

                    //tlpOverview_F/R Ratio
                    foreach (var month_L1 in tlpOverview_HU_BBvSBvFISH)
                    {
                        if (tlpOverview_General[i].Min_Date.ToString("MMMM.yy") == month_L1.Min_Date.ToString("MMMM.yy"))
                        {

                            foreach (var month_L2 in tlpOverview_HU_BBvSBvREG)
                            {
                                if (tlpOverview_General[i].Min_Date.ToString("MMMM.yy") == month_L2.Min_Date.ToString("MMMM.yy"))
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
                    currRow++;
                }

            }
            TableLayoutRowStyleCollection rowStyles = tlp_overview.RowStyles;
            TableLayoutColumnStyleCollection columnStyles = tlp_overview.ColumnStyles;
            foreach (RowStyle style in rowStyles)
            {
                style.SizeType = SizeType.Absolute;
                style.Height = 50;
            }
            //foreach (ColumnStyle style in columnStyles)
            //{
            //    style.SizeType = SizeType.AutoSize;
            //}

            tlp_overview.ResumeLayout();
        }



        private (List<CevModel> , string) GetSatsListByCol(int _col)
        {
            List<CevModel> statsList = new List<CevModel>();
            string title = "";
            switch (_col)
            {
                case 0:
                    statsList = tlpOverview_General;
                    title = "Month";
                    break;
                case 1:
                    statsList = tlpOverview_General;
                    title = "Tourney Played";
                    break;
                case 2:
                    statsList = tlpOverview_General;
                    title = "av. BI";
                    break;
                case 3:
                    statsList = tlpOverview_General;
                    title = "cEV/t";
                    break;
                case 4:
                    statsList = tlpOverview_3W_BTNvBBvREG;
                    title = "3W BTNvBBvREG";
                    break;
                case 5:
                    statsList = tlpOverview_3W_BTNvBBvFISH;
                    title = "3W BTNvBBvFISH";
                    break;
                case 6:
                    statsList = tlpOverview_3W_SBvBTNvREG;
                    title = "3W SBvBTNvREG";
                    break;
                case 7:
                    statsList = tlpOverview_3W_SBvBTNvFISH;
                    title = "3W SBvBTNvFISH";
                    break;
                case 8:
                    statsList = tlpOverview_3W_BBvBTNvREG;
                    title = "3W BBvBTNvREG";
                    break;
                case 9:
                    statsList = tlpOverview_3W_BBvBTNvFISH;
                    title = "3W BBvBTNvFISH";
                    break;
                case 10:
                    statsList = tlpOverview_3W_SBvBBvREG;
                    title = "3W SBvBBvREG";
                    break;
                case 11:
                    statsList = tlpOverview_3W_SBvBBvFISH;
                    title = "3W SBvBBvFISH";
                    break;
                case 12:
                    statsList = tlpOverview_3W_BBvSBvREG;
                    title = "3W BBvSBvREG";
                    break;
                case 13:
                    statsList = tlpOverview_3W_BBvSBvFISH;
                    title = "3W BBvSBvFISH";
                    break;
                case 14:
                    statsList = tlpOverview_HU_SBvBBvREG;
                    title = "HU SBvBBvREG";
                    break;
                case 15:
                    statsList = tlpOverview_HU_SBvBBvFISH;
                    title = "HU SBvBBvFISH";
                    break;
                case 16:
                    statsList = tlpOverview_HU_BBvSBvREG;
                    title = "HU BBvSBvREG";
                    break;
                case 17:
                    statsList = tlpOverview_HU_BBvSBvFISH;
                    title = "HU BBvSBvFISH";
                    break;
                case 18:
                    statsList = tlpOverview_HU_BBvSBvREG;
                    title = "HU_BB SBvREG";
                    break;
                case 19:
                    statsList = tlpOverview_HU_BBvSBvFISH;
                    title = "HU BBvSBvFISH";
                    break;

                default:
                    statsList = tlpOverview_General;
                    title = "";
                    break;
            }
            return (statsList, title);
        }
        private void tlp_MouseEnter(object sender, EventArgs e)
        {
            Label l = (Label)sender;
            for (int row = 0; row < tlp_overview.RowCount; row++)
            {
                for (int col = 4; col < tlp_overview.ColumnCount; col++)
                {
                    if (l.TabIndex == tlp_overview.GetControlFromPosition(col, row).TabIndex)
                    {
                        List<CevModel> statsList = GetSatsListByCol(col).Item1;

                        foreach (var month in statsList)
                        {
                            if (tlp_overview.GetControlFromPosition(0, row).Text.ToString() == month.Min_Date.ToString("yy-MMM").ToUpper())
                            {
                                l.Text = Double.Parse(month.Situations.ToString()).ToString("F0");

                            }
                        }
                    }
                }
            }
        }



        private void tlp_MouseLeave(object sender, EventArgs e)
        {
            Label l = (Label)sender;

                for (int row = 0; row < tlp_overview.RowCount; row++)
            {
                for (int col = 4; col < tlp_overview.ColumnCount; col++)
                {
                    if (l.TabIndex == tlp_overview.GetControlFromPosition(col, row).TabIndex)
                    {
                        List<CevModel> statsList = GetSatsListByCol(col).Item1;

                        foreach (var month in statsList)
                        {
                            if (tlp_overview.GetControlFromPosition(0, row).Text.ToString() == month.Min_Date.ToString("yy-MMM").ToUpper())
                            {
                                l.Text = Double.Parse((month.aBB / month.Situations).ToString()).ToString("F2");

                            }
                        }
                    }
                }
            }
        }

        //private void tlp_MouseEnter(object sender, EventArgs e)
        //{

        //}



        //private void tlp_MouseLeave(object sender, EventArgs e)
        //{

        //}



        private void cBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox currCBox = (CheckBox)sender;
            int col = Int16.Parse(currCBox.Tag.ToString());
            (List<CevModel> currStats , string title) = GetSatsListByCol(col);   
            LineSeries item = PlotDataFromCevModelList(currStats, title, col);

            if (currCBox.Checked == true)
            {
                var secondaryAxis = new LinearAxis() { Position = AxisPosition.Right, Key = "YA" };
                pv.Model.Axes.Add(secondaryAxis);
                item.YAxisKey = "YA";

                pv.ActualModel.Series.Add(item);
                pv.Refresh();
                pv.Invalidate();
                pv.ActualModel.InvalidatePlot(true);
            }
            else
            {
                int index = 0;
                for (int i = 0; i < pv.ActualModel.Series.Count; i++)
                {
                    if ((int)pv.ActualModel.Series[i].Tag == col)
                    {
                        index = i;
                    }
                }
                pv.ActualModel.Series.RemoveAt(index);
                pv.Refresh();
                pv.Invalidate();
                pv.ActualModel.InvalidatePlot(true);
            }




        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void dashBoardToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void heatMapToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }   
}
