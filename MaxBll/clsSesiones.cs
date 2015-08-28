using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxBll
{
    public class clsSesiones
    {
         MaxData.clsDatos objDatos;

         public clsSesiones()
        {
            objDatos = new MaxData.clsDatos();
            objDatos.ConectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;
            objDatos.OpenConection();
        }

        public DataTable mtGetSesiones()
        {
            DataTable dtReturn = new DataTable();
            try
            {
                dtReturn = objDatos.getDataTable("Spc_mIniSes_Get", CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
            return dtReturn;
        }

        public void mtPutSesiones(int IniSesCodigoSis, int IniSesSecuencia, string IniSesNumero, int IniSesTipo, DateTime IniSesFecha,
                                  int AdmLetCodigo, int AdmSalCodigo, int AdmPcoCodigo, int AdmEstCodigo, string IniSesUsuario)
        {
            try
            {
                objDatos.ClearParameter();
                objDatos.AddParameter("@IniSesCodigoSis", IniSesCodigoSis);
                objDatos.AddParameter("@IniSesSecuencia", IniSesSecuencia);
                objDatos.AddParameter("@IniSesNumero", IniSesNumero);
                objDatos.AddParameter("@IniSesTipo", IniSesTipo);
                objDatos.AddParameter("@IniSesFecha", IniSesFecha);
                objDatos.AddParameter("@AdmLetCodigo", AdmLetCodigo);
                objDatos.AddParameter("@AdmSalCodigo", AdmSalCodigo);
                objDatos.AddParameter("@AdmPcoCodigo", AdmPcoCodigo);
                objDatos.AddParameter("@AdmEstCodigo", AdmEstCodigo);
                objDatos.AddParameter("@IniSesUsuario", IniSesUsuario);

                objDatos.ExecuteNonQuery("Spc_mIniSes_Put", CommandType.StoredProcedure);

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
