using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Web; 
 

namespace EHR.Model
{
    public class RequestModel
    {
        MiscModel _misc = new MiscModel();
        DBModel _db = new DBModel();
        PSModel _ps = new PSModel();

        public string GetTrainor(string requestid)
        {
            _db.GetConn();
            string sql = "SELECT APPEMPLID FROM APPROVALTRANSACTION WHERE APPROVERVALUE = 'HR_TRAINOR' " +
                    "AND REQID = '" + requestid + "'";
            string trainor;
            OleDbCommand cmd = new OleDbCommand(sql, _db.conn);
            OleDbDataReader rd = cmd.ExecuteReader();

            if (rd.Read())
            {
                trainor = Convert.ToString(rd["APPEMPLID"]);
            }
            else
            {
                trainor = "";
            }

            return trainor;
        }
    }
}
