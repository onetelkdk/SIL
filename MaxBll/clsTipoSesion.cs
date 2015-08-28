using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxBll
{
    public class clsTipoSesion
    {
        MaxData.clsDatos objDatos;

        public clsTipoSesion()
        {
            objDatos = new MaxData.clsDatos();
            objDatos.ConectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;
            objDatos.OpenConection();
        }

        public DataTable mtGetTipoSesion()
        {
            DataTable dtReturn = new DataTable();
            try
            {
                dtReturn = objDatos.getDataTable("Spc_mIniTse_Get", CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
            return dtReturn;
        }

        public void mtPutTipoSesion(int IniTseCodigo, string IniTseDescripcion, bool AdmStsCodigo, string IniTseUsuario)
        {
            try
            {
                objDatos.ClearParameter();
                objDatos.AddParameter("@IniTseCodigo", IniTseCodigo);
                objDatos.AddParameter("@IniTseDescripcion", IniTseDescripcion);
                objDatos.AddParameter("@AdmStsCodigo", AdmStsCodigo);
                objDatos.AddParameter("@IniTseUsuario", IniTseUsuario);

                objDatos.ExecuteNonQuery("Spc_mIniTse_Put", CommandType.StoredProcedure);

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
