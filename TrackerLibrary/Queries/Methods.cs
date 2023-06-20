using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Queries
{
    public static  class QueryMethods
    {
        public static string ConcatQueries(string mainQuery, string whereClause)
        {
            if (mainQuery.Contains("@whereClause"))
            {
                string output = mainQuery.Replace("@whereClause", whereClause);
                return output;
            }
            else
            {
                return mainQuery;
            }
        }

        public static string ConcatQueries(string _mainQuery, string _HU_3W_Filter, string _regList, string _villianSAID)
        {
            if (_mainQuery.Contains("@HU_3W_Filter") & _mainQuery.Contains("@villianSAID") & _mainQuery.Contains("@villianSAID"))
            {
                if (_HU_3W_Filter == "3W")
                {
                    string output = _mainQuery.Replace("@regList", _regList).Replace("@villianSAID", _villianSAID).Replace("@HU_3W_Filter", "h.btnSeatActionId is not NULL AND");

                    return output;
                }
                else if (_HU_3W_Filter == "HU")
                {
                    string output = _mainQuery.Replace("@regList", _regList).Replace("@villianSAID", _villianSAID).Replace("@HU_3W_Filter", "h.btnSeatActionId is NULL AND");

                    return output;
                }
                else if (_HU_3W_Filter == "ALL")
                {
                    string output = _mainQuery.Replace("@regList", _regList).Replace("@villianSAID", _villianSAID).Replace("@HU_3W_Filter", "");

                    return output;
                }
                else return _mainQuery;

            }
            else
            {
                return _mainQuery;
            }
        }
    }
}
