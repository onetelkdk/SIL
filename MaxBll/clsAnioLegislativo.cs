using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace MaxBll
{
    public class clsAnioLegislativo
    {
        MaxData.clsDatos objDatos;

        public clsAnioLegislativo()
        {
            objDatos = new MaxData.clsDatos();
            objDatos.ConectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;
            objDatos.OpenConection();
        }



        /// <summary>
        /// Método para obtener los Años Legislativos.
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable mtGetAniosLegislativos()
        {
            DataTable dtReturn = new DataTable();
            try
            {
                dtReturn = objDatos.getDataTable("Spc_mAdmAno_Get", CommandType.StoredProcedure);
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
