using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxBll
{
    public class clsPartido
    {
        MaxData.clsDatos objDatos;

        public clsPartido()
        {
            objDatos = new MaxData.clsDatos();
            objDatos.ConectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;
            objDatos.OpenConection();
        }



        /// <summary>
        /// Método para obtener las materias de la aplicación.
        /// </summary>
        /// <returns>DataTable</returns>
        
        public DataTable mtGetPartido()
        {
            DataTable dtReturn = new DataTable();
            try
            {
                dtReturn = objDatos.getDataTable("Spc_mAdmPdo_Get2", CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
            return dtReturn;
        }

        public void mtPutPartido(int AdmPdoCodigo, string AdmPdoSiglas, string AdmPdoDescripcion, string AdmPdoLogo, bool AdmStsCodigo)
        {

            try
            {
                objDatos.ClearParameter();
                objDatos.AddParameter("@AdmPdoCodigo", AdmPdoCodigo);
                objDatos.AddParameter("@AdmPdoSiglas", AdmPdoSiglas);
                objDatos.AddParameter("@AdmPdoDescripcion", AdmPdoDescripcion);
                objDatos.AddParameter("@AdmPdoLogo", AdmPdoLogo);
                objDatos.AddParameter("@AdmStsCodigo", AdmStsCodigo);

                objDatos.ExecuteNonQuery("Spc_mAdmPdo_Put", CommandType.StoredProcedure);

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
