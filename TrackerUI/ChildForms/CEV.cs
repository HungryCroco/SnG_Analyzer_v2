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



        private List<CevModel> CalculateCevRequested()
        {
            List<CevModel> cevRequested = new List<CevModel>();
            cevRequested = NoSQL_Connector.GetCevByPosParallel("IPray2Buddha", "3-max", QueriesExtensionMethods.ConcatQueries(NoSQL_DashBoardQueries.sql_ExportCevPerTournamentAsJSON, NoSQL_DashBoardQueries.sql_cevRequestGeneral_test));
            return cevRequested;
        }

        public CEV()
        {

            ////w. Tasks:
            Task t1 = Task.Run(CalculateCevRequested);
            


            InitializeComponent();
            t1.Wait();

            cBox_3_3.Checked = true;


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

    }   
}
