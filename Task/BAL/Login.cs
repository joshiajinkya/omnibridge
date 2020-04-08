using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Common;
using Task.Models;

namespace Task.BAL
{
    public class Login
    {
        #region private varaiable

        DalFunctions objDal;
        CommonFunctions objCom;
        ClsConnection objCon;
        ClsConState.clsConnectionState objState;
        #endregion

        #region Login Member
        public DataTable MemberLogin(User md)
        {
            DataTable DtResult = new DataTable();
            objDal = new DalFunctions();
            objCon = new ClsConnection();
            objCom = new CommonFunctions();
            md.password = Crypto.EncryptToBase64String(md.password, "B&!m#vquxI");
            SqlParameter[] sqlParams = {
                
                 new SqlParameter("@userid",SqlDbType.NVarChar){Value=md.UserId},
                  new SqlParameter("@roleid",SqlDbType.Int){Value=md.roleid},
                 new SqlParameter("@pwd",SqlDbType.NVarChar){Value=md.password}
        };

            CommandType type = CommandType.StoredProcedure;
            SqlConnection con = objCon.myCon();
            objState = new ClsConState.clsConnectionState(con);
            try
            {
                using (con)
                {
                    DtResult = objDal.ExecuteReader(con, "api_Login", type, sqlParams);
                }

                return DtResult;
            }

            catch (Exception ex)
            {
                string msg = ex.Message;
                return DtResult;
            }

        }
        #endregion
    }
}