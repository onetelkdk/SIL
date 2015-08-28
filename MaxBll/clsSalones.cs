using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxBll
{
    public class clsSalones
    {
        MaxData.clsDatos objDatos;

        public clsSalones()
        {
            objDatos = new MaxData.clsDatos();
            objDatos.ConectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;
            objDatos.OpenConection();
        }



        /// <summary>
        /// Método para obtener la lista de salones.
        /// </summary>
        /// <returns>DataTable</returns>
         public DataTable mtGetSalones()
        {
            DataTable dtReturn = new DataTable();
            try
            {
                dtReturn = objDatos.getDataTable("Spc_mAdmSal_Get", CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
            return dtReturn;
        }

         /// <summary>
         /// Método para obtener la lista de salones por tipo de codigo.
         /// </summary>
         /// <returns>DataTable</returns>
         public DataTable mtGetSalonesCodigo(int AdmCatCodigo)
         {
             DataTable dtReturn = new DataTable();
             try
             {
                 objDatos.ClearParameter();
                 objDatos.AddParameter("@AdmCatCodigo", AdmCatCodigo);
                 dtReturn = objDatos.getDataTable("Spc_mAdmSal_Get", CommandType.StoredProcedure);
             }
             catch
             {
                 throw;
             }
             return dtReturn;
         }


         public void mtPutSalones(int AdmSalCodigo, string AdmSalDescripcion, int AdmCatCodigo, bool AdmStsCodigo, string AdmSalUsuario)
         {
             try
             {
                 objDatos.ClearParameter();
                 objDatos.AddParameter("@AdmSalCodigo", AdmSalCodigo);
                 objDatos.AddParameter("@AdmSalDescripcion", AdmSalDescripcion);
                 objDatos.AddParameter("@AdmCatCodigo", AdmCatCodigo);
                 objDatos.AddParameter("@AdmStsCodigo", AdmStsCodigo);
                 objDatos.AddParameter("@AdmSalUsuario", AdmSalUsuario);

                 objDatos.ExecuteNonQuery("Spc_mAdmSal_Put", CommandType.StoredProcedure);

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
