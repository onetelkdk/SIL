using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace MaxBll
{
    public class clsConfirmacionComision
    {
        MaxData.clsDatos objDatos;

        public clsConfirmacionComision()
        {
            objDatos = new MaxData.clsDatos();
            objDatos.ConectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;
            objDatos.OpenConection();
        }



        /// <summary>
        /// Método para obtener las materias de la aplicación.
        /// </summary>
        /// <returns>DataTable</returns>
        
        public DataTable mtGetConfirmaComision()
        {
            DataTable dtReturn = new DataTable();
            try
            {
                dtReturn = objDatos.getDataTable("Spc_mComCfm_Get", CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
            return dtReturn;
        }

        public void mtPutConfirmaComision(int ComCfmId, int ComComCodigo, int ComTipCodigo, int AdmPcoCodigo, DateTime ComCfmFecha, int ComCfmCoordinador, int ComCfmSecretaria, bool ComCfmDisuelta, string ComCfmAtribucion,int AdmEstCodigo)
        {

            try
            {
                objDatos.ClearParameter();
                objDatos.AddParameter("@ComCfmId", ComCfmId);
                objDatos.AddParameter("@ComComCodigo", ComComCodigo);
                objDatos.AddParameter("@ComTipCodigo", ComTipCodigo);
                objDatos.AddParameter("@AdmPcoCodigo", AdmPcoCodigo);
                objDatos.AddParameter("@ComCfmFecha", ComCfmFecha);
                objDatos.AddParameter("@ComCfmCoordinador", ComCfmCoordinador);
                objDatos.AddParameter("@ComCfmSecretaria", ComCfmSecretaria);
                objDatos.AddParameter("@ComCfmDisuelta", ComCfmDisuelta);
                objDatos.AddParameter("@ComCfmAtribucion", ComCfmAtribucion);
                objDatos.AddParameter("@AdmEstCodigo", AdmEstCodigo);

                objDatos.ExecuteNonQuery("Spc_mComCfm_Put", CommandType.StoredProcedure);

            }
            catch
            {
                throw;
            }
        }

        public DataTable mtGetConfirmaComisionMiembros(int ComCfmId)
        {
            DataTable dtReturn = new DataTable();
            try
            {
                objDatos.ClearParameter();
                objDatos.AddParameter("@ComCfmId", ComCfmId);
                dtReturn = objDatos.getDataTable("Spc_dComMbr_Get2", CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
            return dtReturn;
        }
        public void mtPutConfirmaComisionMiembros(int ComCfmId, int ComMbrSecuencia, string ComMbrNombre, int AdmFunCodigo,bool ComMbrInterno, int Ctrl_L_Deleted)
        {

            try
            {
                objDatos.ClearParameter();
                objDatos.AddParameter("@ComCfmId", ComCfmId);
                objDatos.AddParameter("@ComMbrSecuencia", ComMbrSecuencia);
                objDatos.AddParameter("@ComMbrNombre", ComMbrNombre);
                objDatos.AddParameter("@AdmFunCodigo", AdmFunCodigo);
                objDatos.AddParameter("@ComMbrInterno", ComMbrInterno);
                objDatos.AddParameter("@Ctrl_L_Deleted", Ctrl_L_Deleted);

                objDatos.ExecuteNonQuery("Spc_dComMbr_Put", CommandType.StoredProcedure);

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
