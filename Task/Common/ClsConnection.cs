using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Task.Common
{
    public class ClsConnection
    {
        public ClsConnection()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        public SqlConnection myCon()
        {
            string constr = Crypto.ConnectionString;
            //  string constr = Crypto.ConnectionString1;
            // string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString.ToString();
            SqlConnection con = new SqlConnection(constr);
            return con;
        }



    }
}