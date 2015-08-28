using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxBll
{
    public class clsRoles
    {
         MaxData.clsDatos objDatos;

         public clsRoles()
        {
            objDatos = new MaxData.clsDatos();
            objDatos.ConectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;
            objDatos.OpenConection();
        }

        
        public DataTable mtGetRoles()
        {
            DataTable dtReturn = new DataTable();
            try
            {
                objDatos.ClearParameter();
                dtReturn = objDatos.getDataTable("Spc_mAdmRol_Get", CommandType.StoredProcedure);
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
        public void mtPutRoles(Int32 vId, String vAdmRolDescripcion, String vAdmrolUsuario, Boolean vAdmEstCodigo)
        {

            try
            {
                objDatos.ClearParameter();
                objDatos.AddParameter("@AdmrolCodigo", vId);
                objDatos.AddParameter("@AdmRolDescripcion", vAdmRolDescripcion);
                objDatos.AddParameter("@AdmrolUsuario", vAdmrolUsuario);
                objDatos.AddParameter("@AdmEstCodigo", vAdmEstCodigo);          
                objDatos.ExecuteNonQuery("Spc_mAdmRol_Put", CommandType.StoredProcedure);

            }
            catch
            {
                throw;
            }
        }
    }
}
