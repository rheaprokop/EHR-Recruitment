using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Web; 
 

namespace EHR.Model
{
    public class CandidateModel
    {
        MiscModel _misc = new MiscModel();
        DBModel _db = new DBModel();
        PSModel _ps = new PSModel(); 
         

        public string GetCandidateEmailAddress(string candidate)
        {
            _db.GetConn();
            string email;
            string sql = "SELECT EMAILADDRESS FROM EREC_CANDIDATES WHERE CANDIDATEID = '" + candidate + "'";
            OleDbCommand cmd = new OleDbCommand(sql, _db.conn);
            OleDbDataReader _rd = cmd.ExecuteReader();
            if (_rd.Read())
            {
                email = Convert.ToString(_rd["EMAILADDRESS"]); 
            }
            else
            {
                email = "";
            }
            _db.conn.Close(); 
            return email;
        }

        public string GetCandidateName(string candidate)
        {
            _db.GetConn();
            string name;
            string sql = "SELECT INITCAP(FIRSTNAME)|| ' '|| INITCAP(LASTNAME) FULLNAME FROM EREC_CANDIDATES " +
                        "WHERE CANDIDATEID = '" + candidate + "'";
            OleDbCommand cmd = new OleDbCommand(sql, _db.conn);
            OleDbDataReader _rd = cmd.ExecuteReader();
            if (_rd.Read())
            {
                name = Convert.ToString(_rd["FULLNAME"]);
            }
            else
            {
                name = ""; 
            }

            return name;
        }

        public string GetCandidateAppliedPosition(string requestID)
        {
            _db.GetConn();
            string position;
            string sql = "SELECT JOBTITLE From EREC_REQUESTS WHERE REQUESTID = '" + requestID + "'";
            OleDbCommand cmd = new OleDbCommand(sql, _db.conn);
            OleDbDataReader rd = cmd.ExecuteReader();

            if (rd.Read())
            {
                position = Convert.ToString(rd["JOBTITLE"]);
            }
            else
            {
                position = "";
            }

            return position;
        }

        public string GetCandidateRequestAcceptedFor(string candidate)
        {
            _db.GetConn();
            string requestID;
            string sql = "SELECT OFFERFORREQUESTID FROM EREC_CANDIDATES WHERE CANDIDATEID = '" + candidate + "'";
            OleDbCommand cmd = new OleDbCommand(sql, _db.conn);
            OleDbDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                requestID = Convert.ToString(rd["OFFERFORREQUESTID"]);
            }
            else
            {
                requestID = "";
            }

            return requestID;

        }

        public string GetOfferDept(string candidate)
        {
            _db.GetConn();
            string sql = "SELECT OFFERDEPT FROM EREC_CANDIDATES WHERE CANDIDATEID = '" + candidate + "'";
            OleDbCommand _cmd = new OleDbCommand(sql, _db.conn);
            OleDbDataReader _rd = _cmd.ExecuteReader();
            string dept;
            if (_rd.Read())
            {
                dept = Convert.ToString(_rd["OFFERDEPT"]);
            }
            else
            {
                dept = "";
            }

            return dept;
        }

        public string GetOfferJobPosition(string candidate)
        {
            _db.GetConn();
            string position;
            string sql = "SELECT OFFERPOSITION FROM EREC_CANDIDATES WHERE CANDIDATEID = '" + candidate + "'";
            OleDbCommand cmd = new OleDbCommand(sql, _db.conn);
            OleDbDataReader _rd = cmd.ExecuteReader();
            if (_rd.Read())
            {
                position = Convert.ToString(_rd["OFFERPOSITION"]);
            }
            else
            {
                position = "";
            }
            _db.conn.Close();
            return position;
        }

        public string GetCandidateEmplid(string candidate)
        {
            _db.GetConn();
            string email;
            string sql = "SELECT EMPLID FROM EREC_CANDIDATES WHERE CANDIDATEID = '" + candidate + "'";
            OleDbCommand cmd = new OleDbCommand(sql, _db.conn);
            OleDbDataReader _rd = cmd.ExecuteReader();
            if (_rd.Read())
            {
                email = Convert.ToString(_rd["EMPLID"]);
            }
            else
            {
                email = "";
            }
            _db.conn.Close();
            return email;
        }
    }
}
