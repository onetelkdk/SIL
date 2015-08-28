using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MaxBll
{
    public class clsDepartamentos
    {
        MaxData.clsDatos objDatos;

        public clsDepartamentos()
        {
            objDatos = new MaxData.clsDatos();
            objDatos.ConectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;
            objDatos.OpenConection();
        }


        public DataTable mtGetDepartamentos()
        {
            DataTable dtReturn = new DataTable();
            try
            {
                objDatos.ClearParameter();
                dtReturn = objDatos.getDataTable("Spc_mAdmDep_Get", CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
            return dtReturn;
        }

        public void mtDispose()
        {
            objDatos.CloseConection();
            objDatos = null;
        }
    }
}