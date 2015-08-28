using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace MaxBll
{
    public class clsPoderdeOrigen
    {
        MaxData.clsDatos objDatos;

        public clsPoderdeOrigen()
        {
            objDatos = new MaxData.clsDatos();
            objDatos.ConectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;
            objDatos.OpenConection();
        }



        /// <summary>
        /// Método para obtener las materias de la aplicación.
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable mtGetPoderdeOrigen()
        {
            DataTable dtReturn = new DataTable();
            try
            {
                dtReturn = objDatos.getDataTable("Spc_PoderOrigen_Get", CommandType.StoredProcedure);
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
