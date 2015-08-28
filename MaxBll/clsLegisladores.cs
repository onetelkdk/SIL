using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxBll
{
    public class clsLegisladores
    {
        MaxData.clsDatos objDatos;

        public clsLegisladores()
        {
            objDatos = new MaxData.clsDatos();
            objDatos.ConectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;
            objDatos.OpenConection();
        }


         /// <summary>
         /// Método para obtener los tipos de legislatura de inicio de la aplicación.
         /// </summary>
         /// <returns>DataTable</returns>
         public DataTable mtGetLegisladores()
         {
             DataTable dtReturn = new DataTable();
             try
             {
                 dtReturn = objDatos.getDataTable("Spc_mAdmLeg_Get2", CommandType.StoredProcedure);
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
