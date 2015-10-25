using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Web;

namespace EHR.Model
{
    public class DBModel
    {
        string connStr = ConfigurationManager.ConnectionStrings["conn"].ToString();
        string psConStr = ConfigurationManager.ConnectionStrings["ps_conn"].ToString();
        
        public OleDbConnection conn = new OleDbConnection();
        public OleDbConnection ps_conn = new OleDbConnection();
        public OleDbDataAdapter adapter = new OleDbDataAdapter();
        public OleDbCommand command = new OleDbCommand();

        //rheaprokop (dont touch)
        public void GetConn()
        {
            if (conn.State == ConnectionState.Open) conn.Close();
            conn.ConnectionString = connStr;
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {

            }  
        }
         
        public void GetPSConn()
        {
            if (ps_conn.State == ConnectionState.Open) ps_conn.Close();
            ps_conn.ConnectionString = psConStr;
            ps_conn.Open();
        }

        //rheaprokop (dont touch)
        public OleDbDataReader GetDataReader(string sql)
        {
            GetConn();
            OleDbCommand cmd = new OleDbCommand(sql, conn);
            OleDbDataReader rd = cmd.ExecuteReader();
            return rd;
        }

        public OleDbDataReader GetDataReaderFromPS(string sql)
        {
            GetPSConn();
            command.Connection = ps_conn;
            command.CommandText = sql;
            OleDbDataReader dr = command.ExecuteReader();
            return dr;
        }

        public DataTable GetTableFromPS(string sql)
        {
            GetPSConn();
            adapter.SelectCommand = new OleDbCommand(sql, ps_conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }
        //rheaprokop (dont touch)
        public DataTable GetTable(string sql)
        {
            GetConn();
            adapter.SelectCommand = new OleDbCommand(sql, conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;

        }

        public DataSet GetTableSet(string sql, string tbl)
        {
            GetConn();
            adapter.SelectCommand = new OleDbCommand(sql, conn);
            DataSet set = new DataSet();
            adapter.Fill(set, tbl);
            return set;
        }

        //rheaprokop (dont touch)
        public void GetExecuteNonQuery(string sql)
        {
            GetConn();
            command.Connection = conn;
            command.CommandText = sql;
            try
            {

                command.ExecuteNonQuery();
            }
            catch
            {
            }
            finally
            {
                conn.Close();
            }

        }
        //rheaprokop (dont touch)
        public int GetExecuteScalar(string sql)
        {
            int usertype = 0;
            GetConn();
            OleDbCommand cmd = new OleDbCommand(sql, conn);
            usertype = Convert.ToInt32(cmd.ExecuteScalar());
            return usertype;

        }
    }
}
