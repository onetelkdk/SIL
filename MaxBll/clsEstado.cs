using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace MaxBll
{
    public class clsEstado
    {
          MaxData.clsDatos objDatos;

          public clsEstado()
        {
            objDatos = new MaxData.clsDatos();
            objDatos.ConectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;
            objDatos.OpenConection();
        }



        public DataTable mtGetEstado()
        {
            DataTable dtReturn = new DataTable();
            try
            {
                objDatos.ClearParameter();
                dtReturn = objDatos.getDataTable("Spc_dAdmEst_Get", CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
            return dtReturn;
        }

        public DataTable mtGetEstado(Int32 vAdmCatCodigo)
        {
            DataTable dtReturn = new DataTable();
            try
            {
                objDatos.ClearParameter();
                objDatos.AddParameter("@AdmCatCodigo", vAdmCatCodigo);
                dtReturn = objDatos.getDataTable("Spc_dAdmEst_Get", CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
            return dtReturn;
        }

        public DataTable mtGetEstadoSiguientes(Int32 vAdmEstAnterior)
        {
            DataTable dtReturn = new DataTable();
            try
            {
                objDatos.ClearParameter();
                objDatos.AddParameter("@AdmEstAnterior", vAdmEstAnterior);
                dtReturn = objDatos.getDataTable("Spc_dAdmEst_Siguiente_Get", CommandType.StoredProcedure);
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
