using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace MaxBll
{
    public class clsrFunUser
    {
        MaxData.clsDatos objDatos;

        public clsrFunUser()
        {
            objDatos = new MaxData.clsDatos();
            objDatos.ConectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;
            objDatos.OpenConection();
        }

        public DataTable mtGetFunUser(Int16 vAdmFunCodigo, Int16 vAdmCatCodigo)
        {
            DataTable dtReturn = new DataTable();
            try
            {
                objDatos.ClearParameter();
                objDatos.AddParameter("@AdmFunCodigo", vAdmFunCodigo);
                if (vAdmCatCodigo > 0)
                    objDatos.AddParameter("@AdmCatCodigo", vAdmCatCodigo);
                dtReturn = objDatos.getDataTable("Spc_rFunUsr", CommandType.StoredProcedure);

            }
            catch
            {
                throw;
            }
            return dtReturn;
        }


        public DataTable mtGetCoordinadorTecnico()
        {
            DataTable dtReturn = new DataTable();
            try
            {
                int cod = 11;
                int cat = 3;
                objDatos.ClearParameter();
                objDatos.AddParameter("@AdmFunCodigo", cod);
                objDatos.AddParameter("@AdmCatCodigo", cat);
                dtReturn = objDatos.getDataTable("Spc_rFunUsr", CommandType.StoredProcedure);

            }
            catch
            {
                throw;
            }
            return dtReturn;
        }

        public DataTable mtGetSecretaria()
        {
            DataTable dtReturn = new DataTable();
            try
            {
                int cod = 10;
                int cat = 3;
                objDatos.ClearParameter();
                objDatos.AddParameter("@AdmFunCodigo", cod);
                objDatos.AddParameter("@AdmCatCodigo", cat);
                dtReturn = objDatos.getDataTable("Spc_rFunUsr", CommandType.StoredProcedure);

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
