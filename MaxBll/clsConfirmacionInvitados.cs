using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxBll
{
    public class clsConfirmacionInvitados
    {
         MaxData.clsDatos objDatos;

         public clsConfirmacionInvitados()
        {
            objDatos = new MaxData.clsDatos();
            objDatos.ConectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;
            objDatos.OpenConection();
        }



         public DataTable mtGetConfirmaInvitados(Int32 vComActCodigoSis)
        {
            DataTable dtReturn = new DataTable();
            try
            {
                objDatos.ClearParameter();
                objDatos.AddParameter("@ComActCodigoSis", vComActCodigoSis);
                dtReturn = objDatos.getDataTable("Spc_dComInv_Get", CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
            return dtReturn;
        }
    }
}
