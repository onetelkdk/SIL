using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxBll
{
    public class clsFuncion
    {
        #region Properties

        public enum TipoRegistro
        {
            Crear,
            Actualizar,
            Eliminar
        }

        MaxData.clsDatos objDatos;

        #endregion

        public clsFuncion()
        {
            objDatos = new MaxData.clsDatos();
            objDatos.ConectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;
            objDatos.OpenConection();
        }

        /// <summary>
        /// Método para obtener las materias de la aplicación.
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable mtGetFuncion()
        {
            DataTable dtReturn = new DataTable();
            try
            {
                dtReturn = objDatos.getDataTable("Spc_mAdmFun_Get2", CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
            return dtReturn;
        }

        public DataTable mtGetFuncion(short AdmFunCodigo = -1, char IsActive = '2')
        {
            DataTable dtReturn = new DataTable();
            try
            {
                objDatos.ClearParameter();
                if(AdmFunCodigo == -1)  
                {
                    objDatos.AddParameter("@AdmFunCodigo", DBNull.Value);
                }
                else
                { 
                    objDatos.AddParameter("@AdmFunCodigo", AdmFunCodigo);
                }
                objDatos.AddParameter("@IsActive", IsActive);
                dtReturn = objDatos.getDataTable("Spc_mAdmFun_Get", CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
            return dtReturn;
        }

        public Int32 mtGetNextmAdmFunCodigo()
        {
            int mAdmFunCodigoNextNumber = default(int);

            try
            {
                objDatos.ClearParameter();
                DataTable dtReturn = mtGetFuncion(-1);
                mAdmFunCodigoNextNumber = Convert.ToInt32(dtReturn.Compute("max(AdmFunCodigo)", string.Empty)) + 1; // Obtiene próximo AdmFunCódigo
            }
            catch
            {
                throw;
            }
            return mAdmFunCodigoNextNumber;
        }

        public DataTable mtGetmAdmCat(short AdmCatCargaDoc)
        {
            DataTable dtReturn = new DataTable();
            try
            {
                objDatos.ClearParameter();
                objDatos.AddParameter("@AdmCatCargaDoc", AdmCatCargaDoc);
                dtReturn = objDatos.getDataTable("Spc_mAdmCat_Get", CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
            return dtReturn;
        }

        public void mtPutFuncion(TipoRegistro tipoRegistro, params object[] param)
        {
            objDatos.ClearParameter();
            try
            {
                objDatos.AddParameter("@AdmFunCodigo", param[0] ?? DBNull.Value);
                switch (tipoRegistro)
                {
                    case TipoRegistro.Crear:
                        goto default;

                    case TipoRegistro.Actualizar:
                        goto default;

                    case TipoRegistro.Eliminar: 
                        objDatos.AddParameter("@IsDelete", 1);
                        objDatos.ExecuteNonQuery("Spc_mAdmLeg_Put", CommandType.StoredProcedure);
                        break;

                    default:
                        objDatos.AddParameter("@AdmFunDescripcion", param[1] ?? DBNull.Value);
                        objDatos.AddParameter("@AdmCatCodigo", param[2] ?? DBNull.Value);
                        objDatos.AddParameter("@AdmFunUserdata", param[3] ?? DBNull.Value);
                        objDatos.AddParameter("@AdmStsCodigo", param[4] ?? DBNull.Value);
                        objDatos.AddParameter("@AdmFunUsuario", param[5] ?? DBNull.Value);
                       break;
                }
                objDatos.getDataTable("Spc_AdmFun_Put", CommandType.StoredProcedure);
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
