using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxBll
{
    public class clsContacto
    {
        MaxData.clsDatos objDatos;

        public clsContacto()
        {
            objDatos = new MaxData.clsDatos();
            objDatos.ConectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;
            objDatos.OpenConection();
        }

        public DataTable mtGetContacto()
        {
            DataTable dtReturn = new DataTable();
            try
            {
                dtReturn = objDatos.getDataTable("Spc_mAdmCnt_Get", CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
            return dtReturn;
        }

        public DataTable mtGetTipoContacto()
        {
            DataTable dtReturn = new DataTable();
            try
            {
                dtReturn = objDatos.getDataTable("Spc_mAdmTcn_Get", CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
            return dtReturn;
        }

        public void mtPutContacto(int AdmCntCodigo, string AdmCntNombre, int AdmTcnCodigo, string AdmCntEmail, string AdmCntEmpresa,
                                  string AdmCntWebsite, string AdmCntDireccion, string AdmCntMovil, string AdmCntTelOficina,
                                  string AdmCntTelCasa, string AdmCntUsuario, bool AdmStsCodigo)
        {
            try
            {
                objDatos.ClearParameter();
                objDatos.AddParameter("@AdmCntCodigo", AdmCntCodigo);
                objDatos.AddParameter("@AdmCntNombre", AdmCntNombre);
                objDatos.AddParameter("@AdmTcnCodigo", AdmTcnCodigo);
                objDatos.AddParameter("@AdmCntEmail", AdmCntEmail);
                objDatos.AddParameter("@AdmCntEmpresa", AdmCntEmpresa);
                objDatos.AddParameter("@AdmCntWebsite", AdmCntWebsite);
                objDatos.AddParameter("@AdmCntDireccion", AdmCntDireccion);
                objDatos.AddParameter("@AdmCntMovil", AdmCntMovil);
                objDatos.AddParameter("@AdmCntTelOficina", AdmCntTelOficina);
                objDatos.AddParameter("@AdmCntTelCasa", AdmCntTelCasa);
                objDatos.AddParameter("@AdmCntUsuario", AdmCntUsuario);
                objDatos.AddParameter("@AdmStsCodigo", AdmStsCodigo);

                objDatos.ExecuteNonQuery("Spc_mAdmCnt_Put", CommandType.StoredProcedure);

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
