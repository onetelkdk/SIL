using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxBll
{
    /// <summary>
    /// Clase para la llamada de los Módulos.
    /// </summary>
    public class clsModulos
    {
        MaxData.clsDatos objDatos;

        public clsModulos()
        {
            objDatos = new MaxData.clsDatos();
            objDatos.ConectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;
            objDatos.OpenConection();
        }

        /// <summary>
        /// Método para obtener los módulos de la aplicación.
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable mtGetModulos(String vAdmmodCodigo)
        {
            DataTable dtReturn = new DataTable();
            try
            {
                objDatos.ClearParameter();
                if (!String.IsNullOrEmpty(vAdmmodCodigo))
                    objDatos.AddParameter("@AdmmodCodigo", vAdmmodCodigo);

               dtReturn= objDatos.getDataTable("Spc_mAdmMod_Get", CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
            return dtReturn;
        }

        public void mtPutModulos(Int32 vId, String vAdmmodCodigo, String vAdmmodNombre, Int16 vAdmmodOrden, String vAdmmodIcono
            , String vAdmModColorFondo, String vAdmModTamano, Boolean vAdmEstCodigo)
        {

            try
            {
                objDatos.ClearParameter();
                objDatos.AddParameter("@Id", vId);
                objDatos.AddParameter("@AdmmodCodigo", vAdmmodCodigo);
                objDatos.AddParameter("@AdmmodNombre", vAdmmodNombre);
                objDatos.AddParameter("@AdmmodOrden ", vAdmmodOrden);
                objDatos.AddParameter("@AdmmodIcono", vAdmmodIcono);
                objDatos.AddParameter("@AdmModColorFondo", vAdmModColorFondo);
                objDatos.AddParameter("@AdmModTamano", vAdmModTamano);
                objDatos.AddParameter("@AdmEstCodigo", vAdmEstCodigo);

                objDatos.ExecuteNonQuery("Spc_mAdmMod_Put", CommandType.StoredProcedure);

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
