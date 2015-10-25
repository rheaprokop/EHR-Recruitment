using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Web;
using System.Text;

namespace EHR.Model
{
    public class GlobalAgentModel : DBModel
    {
        //return true if this employee has proxy 
        public bool HasProxy(string appemplid)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                string[] emplid = appemplid.Split(',');

                foreach (string empl in emplid)
                {
                    sb.Append("'" + empl + "',");
                }
                string appemplids = sb.ToString().TrimEnd(',');
                string sql = "SELECT COUNT(*) FROM AGENTS WHERE APPEMPLID IN (" + appemplids + ") " +
                    "AND SYSDATE BETWEEN START_DATE_TIME AND END_DATE_TIME";
                GetConn();
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                int count = Convert.ToInt32(cmd.ExecuteScalar());

                if (count >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
                
             
        }

        //return the employee's proxy 
        public string GetAgent(string appemplid)
        {
            string agent;
            StringBuilder sb = new StringBuilder();
            string[] emplid = appemplid.Split(',');
            foreach (string empl in emplid)
            {
                sb.Append("'" + empl + "',");
            }
            string appemplids = sb.ToString().TrimEnd(',');

            string sql = "SELECT AGENTID FROM AGENTS WHERE APPEMPLID IN (" + appemplids + ") " +
                "AND SYSDATE BETWEEN START_DATE_TIME AND END_DATE_TIME";
            GetConn();
            OleDbCommand cmd = new OleDbCommand(sql, conn);
            OleDbDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                agent = Convert.ToString(rd["AGENTID"]);
            }
            else
            {
                agent = "";
            }
            return agent;

        }

        public string GetAgentsEmplid(string appemplid, string agent)
        {
            StringBuilder sb = new StringBuilder();
            string[] emplid = appemplid.Split(',');
            foreach (string empl in emplid)
            {
                sb.Append("'" + empl + "',");
            }
            string appemplids = sb.ToString().TrimEnd(',');

            string sql = " SELECT APPEMPLID FROM AGENTS WHERE APPEMPLID IN (" + appemplids + ") " + 
                        "AND AGENTID = '" + agent + "' " + 
                        "AND SYSDATE BETWEEN START_DATE_TIME AND END_DATE_TIME";

            GetConn();
            
            OleDbCommand _cmdAppemplid = new OleDbCommand(sql, conn);
            OleDbDataReader _rdAppemplid = _cmdAppemplid.ExecuteReader();

            if (_rdAppemplid.Read())
            {
                return Convert.ToString(_rdAppemplid["APPEMPLID"]);
            }
            else
            {
                return "";
            }

        }
 
    }
}
