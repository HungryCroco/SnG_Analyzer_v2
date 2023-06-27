using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Legends;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using OxyPlot.Annotations;
using System;
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
using TrackerLibrary.UIRequests;

namespace TrackerUI.ChildForms
{
    public partial class Dashboard : Form
    {
        
        public Dashboard()
        {
            InitializeComponent();
            chb_3_3.Checked = true;

            //LoadChart(PlotDataFromCevModelList(DashBoardRequest.CalculateCevRequested(),200,"CEV/t", 3) , PlotDataFromCevModelList(DashBoardRequest.CalculateCevRequested(), 200, "Chips/t", 2));



        }

        private void tlp_MouseEnter(object sender, EventArgs e)
        {

        }



        private void tlp_MouseLeave(object sender, EventArgs e)
        {

        }

        private void cBox_CheckedChanged(object sender, EventArgs e)
        {

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
            //CategoryAxis months = PlotDataFromCevModelList_CA(dashBoardData[1]);
            //pv.ActualModel.Axes.Add(months);
            pv.Refresh();
            pv.Invalidate();
            pv.ActualModel.InvalidatePlot(true);

        }

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
                        currPlot = cevList[i].Count_tourney;
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

                lineSeries.Points.Add(new DataPoint(i , currPlot));

            }
            return lineSeries;
        }

    }
}
