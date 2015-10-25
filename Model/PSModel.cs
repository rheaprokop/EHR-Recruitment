using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Web;

namespace EHR.Model
{
    public class PSModel
    {
        DBModel _db = new DBModel();

        //Get the name of employee
        public string GetEmplFirstName(string emplid)
        {
            string name;
            _db.GetConn();
            string sql = "SELECT FIRST_NAME_SRCH FROM PS.PS_SUB_WCZ_AT_VW_A WHERE EMPLID = '" + emplid + "'";
            OleDbCommand cmd = new OleDbCommand(sql, _db.conn);  
            OleDbDataReader rd = cmd.ExecuteReader();
            try
            {
               // if (_db.GetDataReader(sql).Read())
                if(rd.Read())
                {
                    name = Convert.ToString(rd["FIRST_NAME_SRCH"]);
                }
                else
                {
                    name = "";
                }
            }
            catch(Exception ex)
            {
                name = "";
            }
            finally
            {
                _db.conn.Close();
            }
            return name;
 
        }

        //Get the name of employee
        public string GetName(string emplid)
        {
            string name;
            _db.GetConn();
            string sql = @"SELECT INITCAP(NAME_A) NAME_A FROM PS.PS_SUB_WCZ_AT_VW_A WHERE EMPLID = '" + emplid + "'";
            OleDbCommand cmd = new OleDbCommand(sql, _db.conn); 
            OleDbDataReader rd = cmd.ExecuteReader();
            try
            {
                // if (_db.GetDataReader(sql).Read())
                if (rd.Read())
                {
                    name = Convert.ToString(rd["NAME_A"]);
                }
                else
                {
                    name = "";
                }
            }
            catch (Exception ex)
            {
                name = "";
            }
            finally
            {
                _db.conn.Close();
            }
            return name;

        }

        public string GetEmplIDFromName(string name_a)
        {
            string emplid;
            _db.GetConn();
            string sql = "SELECT EMPLID FROM PS.PS_SUB_WCZ_AT_VW_A WHERE NAME_A = '" + name_a + "'";
            OleDbCommand cmd = new OleDbCommand(sql, _db.conn);
            //cmd.Parameters.AddWithValue("':name_a'", name_a);
            OleDbDataReader rd = cmd.ExecuteReader();
            try
            {
                // if (_db.GetDataReader(sql).Read())
                if (rd.Read())
                {
                    emplid = Convert.ToString(rd["EMPLID"]);
                }
                else
                {
                    emplid = "";
                }
            }
            catch (Exception ex)
            {
                emplid = "";
            }
            finally
            {
                _db.conn.Close();
            }
            return emplid;
        }

        //Get the department of employee
        public string GetEmplDepartment(string emplid)
        {
            string deptid;
            _db.GetConn();
            string sql = "SELECT DEPTID FROM PS.PS_SUB_WCZ_AT_VW_A WHERE EMPLID = '" + emplid + "'";
            OleDbCommand cmd = new OleDbCommand(sql, _db.conn);
            OleDbDataReader rd = cmd.ExecuteReader();
            try
            {
                // if (_db.GetDataReader(sql).Read())
                if (rd.Read())
                {
                    deptid = Convert.ToString(rd["DEPTID"]);
                }
                else
                {
                    deptid = "";
                }
            }
            catch (Exception ex)
            {
                deptid = "";
            }
            finally
            {
                _db.conn.Close();
            }
            return deptid;

        }

        public string GetEmplEmailAdrress(string emplid)
        {
            string email = "";
            _db.GetConn();
            string sql = "SELECT EMAIL_ADDRESS_A FROM PS.PS_SUB_WCZ_AT_VW_A WHERE EMPLID = '" + emplid + "'";
            OleDbCommand cmd = new OleDbCommand(sql, _db.conn);
            OleDbDataReader rd = cmd.ExecuteReader();
            try
            {
                // if (_db.GetDataReader(sql).Read())
                if (rd.Read())
                {
                    email = Convert.ToString(rd["EMAIL_ADDRESS_A"]);
                }
                else
                {
                    email = "";
                }
            }
            catch (Exception ex)
            {
                email = "";
            }
            finally
            {
                _db.conn.Close();
            }

            return email;
        }

        public string GetJobBusiTitleFromCode(string jobcode)
        {
            _db.GetConn(); 
            string position;
            string sql = "Select JOB_BUSI_TITLE From PS.PS_JOB WHERE JOB_CODE = '" + jobcode + "'";
            OleDbCommand cmd = new OleDbCommand(sql, _db.conn);
            OleDbDataReader rd = cmd.ExecuteReader();

            if (rd.Read())
            {
                position = Convert.ToString(rd["JOB_BUSI_TITLE"]);
            }
            else
            {
                position = "";
            }

            return position;
        }

        public string GetContactNumber(string emplid)
        {
            _db.GetConn();
            string contact;
            string sql = "SELECT CNTCT_PHONE_A FROM PS.PS_SUB_WCZ_AT_VW_A WHERE EMPLID = '" + emplid + "'";
            OleDbCommand cmd = new OleDbCommand(sql, _db.conn);
            OleDbDataReader rd = cmd.ExecuteReader();

            if (rd.Read())
            {
                contact = Convert.ToString(rd["CNTCT_PHONE_A"]);
            }
            else
            {
                contact = "";
            }

            return contact;

        }
    }
}
