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
    public class employeeService
    {
        #region private varaiable

        DalFunctions objDal;
        CommonFunctions objCom;
        ClsConnection objCon;
        ClsConState.clsConnectionState objState;
        #endregion


        #region Employee Info
        public DataTable emp_crud(empmdl md)
        {
            DataTable DtResult = new DataTable();
            objDal = new DalFunctions();
            objCon = new ClsConnection();
            objCom = new CommonFunctions();

            SqlParameter[] sqlParams = {
                 new SqlParameter("@action",SqlDbType.NVarChar){Value=md.action},
                 new SqlParameter("@Id",SqlDbType.NVarChar){Value=md.Id},
                 new SqlParameter("@name",SqlDbType.NVarChar){Value=md.name},
                 new SqlParameter("@dob",SqlDbType.NVarChar){Value=md.dob},
                 new SqlParameter("@qualification",SqlDbType.NVarChar){Value=md.qualification},
                 new SqlParameter("@experience",SqlDbType.NVarChar){Value=md.experience},
                 new SqlParameter("@joining_date",SqlDbType.NVarChar){Value=md.joiningdt},
                 new SqlParameter("@salary",SqlDbType.Int){Value=md.salary},
                 new SqlParameter("@designation",SqlDbType.NVarChar){Value=md.designation},
                 new SqlParameter("@hobbies",SqlDbType.NVarChar){Value=md.hobbies},
                 new SqlParameter("@recorddt",SqlDbType.NVarChar){Value=md.recorddt}

        };

            CommandType type = CommandType.StoredProcedure;
            SqlConnection con = objCon.myCon();
            objState = new ClsConState.clsConnectionState(con);
            try
            {
                using (con)
                {
                    DtResult = objDal.ExecuteReader(con, "tbl_employee_crud", type, sqlParams);
                }

                return DtResult;
            }

            catch (Exception ex)
            {
                throw;
            }

        }
        #endregion

        #region Employee Export
        public List<empmdl> emp_export()
        {
            DataTable DtResult = new DataTable();
            objDal = new DalFunctions();
            objCon = new ClsConnection();
            objCom = new CommonFunctions();

            SqlParameter[] sqlParams = {
                 new SqlParameter("@action",SqlDbType.NVarChar){Value="L"},

        };

            CommandType type = CommandType.StoredProcedure;
            SqlConnection con = objCon.myCon();
            objState = new ClsConState.clsConnectionState(con);
            try
            {
                using (con)
                {
                    DtResult = objDal.ExecuteReader(con, "tbl_employee_crud", type, sqlParams);
                }
                List<empmdl> objMbr = new List<empmdl>();
                if (DtResult.Rows.Count > 0)
                {
                    foreach (DataRow rw in DtResult.Rows)
                    {
                        empmdl md = new empmdl();
                        md.Id = Convert.ToInt32(objCom.HandleNull(rw["Id"]));
                        md.name = Convert.ToString(objCom.HandleNull(rw["Name"]));
                        md.qualification = Convert.ToString(objCom.HandleNull(rw["qualification"]));
                        md.joiningdt = Convert.ToString(objCom.HandleNull(rw["joining_date"]));
                        md.dob = Convert.ToString(objCom.HandleNull(rw["dob"]));
                        md.experience = Convert.ToString(objCom.HandleNull(rw["exprience"]));
                        md.salary = Convert.ToString(objCom.HandleNull(rw["salary"]));
                        md.designation = Convert.ToString(objCom.HandleNull(rw["designation"]));
                        md.hobbies = Convert.ToString(objCom.HandleNull(rw["hobbies"]));
                       
                        objMbr.Add(md);
                    }
                }
                return objMbr;
            }

            catch (Exception ex)
            {
                throw;
            }

        }
        #endregion
    }
}