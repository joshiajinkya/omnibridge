using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Task.Common
{
    public class DalFunctions
    {
        /*ExcuteNonQuerySP is created by Neetu to execute all NonQuery type on date 29th April 2015*/
        public int ExcuteNonQuerySP(SqlConnection con, string spQuery, CommandType type, SqlParameter[] parms)
        {
            int rtn = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = spQuery;
            cmd.CommandType = type;
            using (cmd)
            {
                if (parms != null)
                {
                    foreach (SqlParameter p in parms)
                    {
                        cmd.Parameters.Add(p);
                    }
                }
                rtn = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return rtn;
            }
        }

        public DataTable ExcuteTableNonQuerySP(SqlConnection con, string spQuery, CommandType type, SqlParameter[] parms)
        {
            DataTable DtResult = new DataTable();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = spQuery;
            cmd.CommandType = type;
            using (cmd)
            {
                if (parms != null)
                {
                    foreach (SqlParameter p in parms)
                    {
                        cmd.Parameters.Add(p);
                    }
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(DtResult);

                cmd.Parameters.Clear();
                return DtResult;
            }
        }

        public DataTable ExecuteReader(SqlConnection con, string spQuery, CommandType type, SqlParameter[] parms)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = spQuery;
            cmd.CommandType = type;
            using (cmd)
            {
                if (parms != null)
                {
                    foreach (SqlParameter p in parms)
                    {
                        cmd.Parameters.Add(p);
                    }

                }
                DataTable dt = new DataTable();
                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                ad.Fill(dt);
                //SqlDataReader dr = cmd.ExecuteReader();
                //dt.Load(dr);
                cmd.Parameters.Clear();
                return dt;
            }
        }


        public DataSet ExecuteReaderDataset(SqlConnection con, string spQuery, CommandType type, SqlParameter[] parms)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = spQuery;
            cmd.CommandType = type;
            using (cmd)
            {
                if (parms != null)
                {
                    foreach (SqlParameter p in parms)
                    {
                        cmd.Parameters.Add(p);
                    }

                }
                DataSet dt = new DataSet();
                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                ad.Fill(dt);
                //SqlDataReader dr = cmd.ExecuteReader();
                //dt.Load(dr);
                cmd.Parameters.Clear();
                return dt;
            }
        }

        public List<DataTable> ExecuteDataSet(SqlConnection con, string spQuery, CommandType type, SqlParameter[] parms)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = spQuery;
            cmd.CommandType = type;
            using (cmd)
            {
                if (parms != null)
                {
                    foreach (SqlParameter p in parms)
                    {
                        cmd.Parameters.Add(p);
                    }

                }
                List<DataTable> dt = new List<DataTable>();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                foreach (DataTable tbl in ds.Tables)
                {
                    dt.Add(tbl);
                }
                cmd.Parameters.Clear();
                return dt;
            }
        }

        public DataTable ExecuteReaderFunction(SqlConnection con, string spQuery, SqlParameter[] parms)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = spQuery;
            using (cmd)
            {
                DataTable dt = new DataTable();
                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                ad.Fill(dt);
                return dt;
            }
        }
    }
}