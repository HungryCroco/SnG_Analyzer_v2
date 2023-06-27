using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary;
using TrackerLibrary.Models;
using TrackerLibrary.Queries;
using TrackerLibrary.Queries.NoSQL;

namespace TrackerLibrary.UIRequests
{
    public class DashBoardRequest
    {

        public List<List<CevModel>> Data { get; }



        public DashBoardRequest(string _data)
        {
            Data = UIRequestsExtensionMethods.ReadEAFromFileAsJSON(GlobalConfig.FullFilePath(GlobalConfig.dashBoardList, GlobalConfig.temp));
        }


        






    }
}
