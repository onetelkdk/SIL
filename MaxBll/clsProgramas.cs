using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxBll
{
    public class clsProgramas
    {
    MaxData.clsDatos objDatos;

    public clsProgramas()
        {
            objDatos = new MaxData.clsDatos();
            objDatos.ConectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;
            objDatos.OpenConection();
        }

        /// <summary>
        /// Método para obtener los programas.
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable mtGetProgramas(String vAdmmodCodigo,String vPrgtipCodigo)
        {
            DataTable dtReturn = new DataTable();
            try
            {
                objDatos.ClearParameter();
                if (!String.IsNullOrEmpty(vAdmmodCodigo))
                objDatos.AddParameter("@AdmmodCodigo", vAdmmodCodigo);
                if (!String.IsNullOrEmpty(vPrgtipCodigo))
                objDatos.AddParameter("@PrgtipCodigo", vPrgtipCodigo);
                dtReturn = objDatos.getDataTable("Spc_mPrgPrg_Get", CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
            return dtReturn;
        }

        public void mtPutProgramas(Int32 vId, String vPrgprgNombre, String vPrgprgDescripcion, String vAdmmodCodigo, String vPrgtipCodigo,
            String vPrgprgIcono,String vPrgPrgColorFondo ,Int16 vPrgprgOrden,Boolean vAdmEstCodigo)
        {
            try
            {
                objDatos.ClearParameter();
                objDatos.AddParameter("@Id", vId);
                objDatos.AddParameter("@PrgprgNombre", vPrgprgNombre);
                objDatos.AddParameter("@PrgprgDescripcion", vPrgprgDescripcion);
                objDatos.AddParameter("@AdmmodCodigo ", vAdmmodCodigo);
                objDatos.AddParameter("@PrgtipCodigo", vPrgtipCodigo);
                objDatos.AddParameter("@PrgprgIcono", vPrgprgIcono);
                objDatos.AddParameter("@PrgPrgColorFondo", vPrgPrgColorFondo);
                objDatos.AddParameter("@PrgprgOrden", vPrgprgOrden);
                objDatos.AddParameter("@AdmEstCodigo", vAdmEstCodigo);

                objDatos.ExecuteNonQuery("Spc_mPrgPrg_Put", CommandType.StoredProcedure);

            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Método que retorna reporte de SSRS a visualizar
        /// </summary>
        /// <param name="vAdmParDescripcion">Descripción o título del reporte en SSRS</param>
        /// <returns></returns>
        public String mtGetAdmParReport(string vAdmParDescripcion)
        {
            try
            {
                objDatos.ClearParameter();
                objDatos.AddParameter("@vAdmParDescripcion", vAdmParDescripcion);
                DataTable dtReturn = objDatos.getDataTable(String.Concat("SELECT dbo.Fnc_AdmParReports_Get('",vAdmParDescripcion,"')"), CommandType.Text);
                return dtReturn.Rows[0][0].ToString();
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
