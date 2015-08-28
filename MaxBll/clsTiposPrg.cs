using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxBll
{
    public class clsTiposPrg
    {
     MaxData.clsDatos objDatos;

     public clsTiposPrg()
        {
            objDatos = new MaxData.clsDatos();
            objDatos.ConectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;
            objDatos.OpenConection();
        }

        /// <summary>
        /// Método para obtener los Tipos de Menú.
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable mtGetTipos()
        {
            DataTable dtReturn = new DataTable();
            try
            {
                objDatos.ClearParameter();
                dtReturn = objDatos.getDataTable("Spc_mPrgTip_Get", CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
            return dtReturn;
        }

        public void mtPutTipoPrg(Int32 vId, String vPrgtipCodigo, String vPrgtipNombre, String vPrgTipDescripcion, String vPrgtipOrden, String vPrgtipIcono, Boolean vAdmEstCodigo)
        {

            try
            {
                objDatos.ClearParameter();
                objDatos.AddParameter("@Id", vId);
                objDatos.AddParameter("@PrgtipCodigo", vPrgtipCodigo);
                objDatos.AddParameter("@PrgtipNombre", vPrgtipNombre);
                objDatos.AddParameter("@PrgTipDescripcion", vPrgTipDescripcion);
                objDatos.AddParameter("@PrgtipOrden ", vPrgtipOrden);
                objDatos.AddParameter("@PrgtipIcono", vPrgtipIcono);
                objDatos.AddParameter("@AdmEstCodigo", vAdmEstCodigo);

                objDatos.ExecuteNonQuery("Spc_mPrgTip_Put", CommandType.StoredProcedure);

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
