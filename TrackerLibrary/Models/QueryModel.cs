using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    public class QueryModel
    {
        public struct Query
        {
            public string MainQuery;
            public string DataViewQuery;
            public string WhereClauseHero;
            public string WhereClauseVillain;

            public Query(string mainQuery, string dataViewQuery, string whereClauseHero, string whereClauseVillain)
            {
                MainQuery = mainQuery;
                DataViewQuery = dataViewQuery;
                WhereClauseHero = whereClauseHero;
                WhereClauseVillain = whereClauseVillain;
            }

        }
    }
}
