using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxBll
{
    public class clsAdmRolDetalles
    {
        MaxData.clsDatos objDatos;

        public clsAdmRolDetalles()
        {
            objDatos = new MaxData.clsDatos();
            objDatos.ConectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;
            objDatos.OpenConection();
        }

        /// <summary>
        /// Método para obtener los Tipos de Menú.
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable mtGetRolDetalles(Int32 vAdmCiaCodigo, Int32 vAdmRolCodigo)
        {
            DataTable dtReturn = new DataTable();
            try
            {
                objDatos.ClearParameter();
                if (vAdmCiaCodigo > 0)
                    objDatos.AddParameter("@AdmCiaCodigo", vAdmCiaCodigo);
                objDatos.AddParameter("@AdmRolCodigo", vAdmRolCodigo);
                dtReturn = objDatos.getDataTable("Spc_dAdmRol_Get", CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
            return dtReturn;
        }

        public DataTable mtGetRolDetalles(String vAdmRolCodigo, String vAdmmodCodigo, String vPrgtipCodigo)
        {
            DataTable dtReturn = new DataTable();
            try
            {
                objDatos.ClearParameter();
                objDatos.AddParameter("@AdmRolCodigo", vAdmRolCodigo);
                objDatos.AddParameter("@AdmmodCodigo", vAdmmodCodigo);
                objDatos.AddParameter("@PrgtipCodigo", vPrgtipCodigo);
                dtReturn = objDatos.getDataTable("Spc_dAdmRol_Get", CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
            return dtReturn;
        }

        public void mtBeginTran()
        {
            objDatos.BeginTransaction();
        }

        public void mtCommitTran()
        {
            objDatos.CommitTransaction();
        }

        public void mtRollBackTran()
        {
            objDatos.RollBackTransaction();
        }

        public void mtPutRolDetalle(Int32 vId, Int32 vAdmRolCodigo, String vPrgPrgNombre, String vAdmRolUsuario
            , Boolean vAdmEstCodigo, String vAdmmodCodigo, String vPrgtipCodigo, Boolean vCtrl_L_Deleted)
        {

            try
            {

                objDatos.ClearParameter();
                objDatos.AddParameter("@AdmCiaCodigo", vId);
                objDatos.AddParameter("@AdmRolCodigo", vAdmRolCodigo);
                objDatos.AddParameter("@PrgPrgNombre", vPrgPrgNombre);
                objDatos.AddParameter("@AdmRolUsuario ", vAdmRolUsuario);
                objDatos.AddParameter("@AdmEstCodigo", vAdmEstCodigo);
                objDatos.AddParameter("@AdmmodCodigo", vAdmmodCodigo);
                objDatos.AddParameter("@PrgtipCodigo", vPrgtipCodigo);
                objDatos.AddParameter("@Ctrl_L_Deleted", vCtrl_L_Deleted);

                objDatos.ExecuteNonQuery("Spc_dAdmRol_Put", CommandType.StoredProcedure);

            }
            catch
            {
                throw;
            }
        }

        public void mtDispose()
        {
            objDatos.CloseConection();
            objDatos = null;
        }
    }
}
