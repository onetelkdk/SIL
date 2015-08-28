using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace MaxBll
{
    public class clsComision
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

        public clsComision()
        {
            objDatos = new MaxData.clsDatos();
            objDatos.ConectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;
            objDatos.OpenConection();
        }

        /// <summary>
        /// Método para obtener las materias de la aplicación.
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable mtGetComision()
        {
            DataTable dtReturn = new DataTable();
            try
            {
                objDatos.ClearParameter();
                dtReturn = objDatos.getDataTable("Spc_mComCom_Get", CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
            return dtReturn;
        }

        public DataTable mtGetComision2()
        {
            DataTable dtReturn = new DataTable();
            try
            {
                objDatos.ClearParameter();
                dtReturn = objDatos.getDataTable("Spc_mComCom_Get2", CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
            return dtReturn;
        }

        public DataTable mtGetdAdmEst(int AdmCatCodigo)
        {
            DataTable dtReturn = new DataTable();
            try
            {
                objDatos.ClearParameter();
                objDatos.AddParameter("@AdmCatCodigo", AdmCatCodigo);
                dtReturn = objDatos.getDataTable("Spc_dAdmEst_Get", CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
            return dtReturn;
        }

        public DataTable mtGetdAdmEst()
        {
            DataTable dtReturn = new DataTable();
            try
            {
                dtReturn = objDatos.getDataTable("Spc_dAdmEst_Get", CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
            return dtReturn;
        }

        public DataTable mtGetmAdmPar(string AdmParCodigo)
        {
            DataTable dtReturn = new DataTable();
            try
            {
                objDatos.ClearParameter();
                objDatos.AddParameter("@AdmparCodigo", AdmParCodigo);
                dtReturn = objDatos.getDataTable("Spc_mAdmPar_Get", CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
            return dtReturn;
        }

        public DataTable mtGetmAdmSal()
        {
            DataTable dtReturn = new DataTable();
            try
            {
                objDatos.ClearParameter();
                dtReturn = objDatos.getDataTable("Spc_mAdmSal_Get", CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
            return dtReturn;
        }
        
        public string mtGetNextComActNumero()
        {
            DataTable dtReturn = new DataTable();
            string ComActNextNumber = default(string);

            try
            {
                objDatos.ClearParameter();
                dtReturn = objDatos.getDataTable("SELECT dbo.Fnc_mComActNextSequence() AS ComActNumero", CommandType.Text);
                ComActNextNumber = dtReturn.Rows[0]["ComActNumero"].ToString();
            }
            catch
            {
                throw;
            }
            return ComActNextNumber;
        }

        public DataTable mtGetmComAct(DateTime ComActFechaDesde, DateTime ComActFechaHasta, object ComActNumero = null)
        {
            DataTable dtReturn = new DataTable();
            try
            {
                objDatos.ClearParameter();
                objDatos.AddParameter("@ComActFechaDesde", ComActFechaDesde);
                objDatos.AddParameter("@ComActFechaHasta", ComActFechaHasta);
                objDatos.AddParameter("@ComActNumero", DBNull.Value);
                dtReturn = objDatos.getDataTable("Spc_mComAct_Get", CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
            return dtReturn;
        }

        public DataTable mtGetmComActAsistencia(Int32 vComActCodigoSis)
        {
            DataTable dtReturn = new DataTable();
            try
            {
                objDatos.ClearParameter();
                if (vComActCodigoSis > 0) objDatos.AddParameter("@ComActCodigoSis", vComActCodigoSis);
                dtReturn = objDatos.getDataTable("Spc_mComAct_Get_Actividad", CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
            return dtReturn;
        }

        public DataTable mtGetmComAct(int ComActCodigoSis)
        {
            DataTable dtReturn = new DataTable();
            try
            {
                objDatos.ClearParameter();
                objDatos.AddParameter("@ComActCodigoSis", ComActCodigoSis);
                dtReturn = objDatos.getDataTable("Spc_dComAct_Get", CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
            return dtReturn;
        }

        public DataTable mtGetrFunUsr()
        {
            DataTable dtReturn = new DataTable();
            try
            {
                objDatos.ClearParameter();
                dtReturn = objDatos.getDataTable("Spc_rFunUsr_Get", CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
            return dtReturn;
        }

        public void mtPutmComAct(TipoRegistro tipoRegistro, params object[] param)
        {
            objDatos.ClearParameter();
            try
            {
                switch (tipoRegistro)
                {
                    case TipoRegistro.Crear:
                        goto default;

                    case TipoRegistro.Actualizar:
                        goto default;

                    case TipoRegistro.Eliminar: objDatos.AddParameter("@IsDelete", 1);
                        objDatos.AddParameter("@ComActCodigoSis", int.Parse(param[20] as string));
                        objDatos.AddParameter("@ComActNumero", int.Parse(param[0] as string));
                        objDatos.ExecuteNonQuery("Spc_mAdmLeg_Put", CommandType.StoredProcedure);
                        break;

                    default:
                        objDatos.AddParameter("@ComActNumero", param[0] ?? DBNull.Value);
                        objDatos.AddParameter("@ComActTipo", int.Parse(param[1] as string));
                        objDatos.AddParameter("@ComCfmId ", param[2] ?? DBNull.Value);
                        objDatos.AddParameter("@ComActFecha", Convert.ToDateTime(param[3]));
                        objDatos.AddParameter("@AdmLetCodigo", int.Parse(param[4] as string));
                        objDatos.AddParameter("@IniIniCodigoSis", param[5] ?? DBNull.Value);
                        objDatos.AddParameter("@AdmPcoCodigo", param[6] ?? DBNull.Value);
                        objDatos.AddParameter("@ComActTipoReunion", int.Parse(param[8] as string));
                        objDatos.AddParameter("@ComActDescripcion", param[9] ?? DBNull.Value);
                        objDatos.AddParameter("@ComActResultados", param[10] ?? DBNull.Value);
                        objDatos.AddParameter("@ComActInvitados", param[11] ?? DBNull.Value);
                        objDatos.AddParameter("@AdmSalCodigo", int.Parse(param[12] as string));
                        objDatos.AddParameter("@ComActHoraConvoca", Convert.ToDateTime(param[13]));
                        objDatos.AddParameter("@ComActHoraInicio", Convert.ToDateTime(param[14]));
                        objDatos.AddParameter("@ComActHoraCierre", Convert.ToDateTime(param[15]));
                        objDatos.AddParameter("@ComActFuncionarioCom", param[16] ?? DBNull.Value);
                        objDatos.AddParameter("@AdmEstCodigo", int.Parse(param[17] as string));
                        objDatos.AddParameter("@ComActUsuario", param[18] ?? DBNull.Value);

                        objDatos.ExecuteNonQuery("Spc_mComAct_Put", CommandType.StoredProcedure);
                        break;
                }
            }
            catch
            {
                throw;
            }
        }

        public void mtPutdComAct(TipoRegistro tipoRegistro, params object[] param)
        {
            objDatos.ClearParameter();
            try
            {
                switch (tipoRegistro)
                {
                    case TipoRegistro.Crear:
                        goto default;

                    case TipoRegistro.Actualizar:
                        goto default;

                    case TipoRegistro.Eliminar: 
                        objDatos.AddParameter("@IsDelete", 1);
                        objDatos.AddParameter("@ComActNumero", param[0] as string);
                        objDatos.ExecuteNonQuery("Spc_dComAct_Put", CommandType.StoredProcedure);
                        break;

                    default:
                        objDatos.AddParameter("@ComActNumero", param[0] as string);
                        objDatos.AddParameter("@AdmLegCodigo", Convert.ToInt32(param[1]));
                        objDatos.AddParameter("@AdmFunCodigo", Convert.ToInt32(param[2]));
                        objDatos.AddParameter("@ComActHoraLLegada ", param[3] ?? DBNull.Value);
                        objDatos.AddParameter("@ComActHoraSalida", param[4] ?? DBNull.Value);
                        objDatos.AddParameter("@ComActPresente", param[5] ?? DBNull.Value);
                        objDatos.AddParameter("@ComActExcusa", param[6] ?? DBNull.Value);
                        objDatos.AddParameter("@AdmExcCodigo", param[7] ?? DBNull.Value);
                        objDatos.AddParameter("@ComActDefinicionExc", param[8] ?? DBNull.Value);
                        objDatos.AddParameter("@ComActPerteneceComision", param[9] ?? DBNull.Value);
                        objDatos.AddParameter("@ComActUsuario", param[10] ?? DBNull.Value);

                        objDatos.ExecuteNonQuery("Spc_dComAct_Put", CommandType.StoredProcedure);
                        break;
                }
            }
            catch
            {
                throw;
            }
        }

        public void mtPutComision(int ComComCodigo, string ComComNombre, string AdmStsCodigo)
         {
             try
             {
                 objDatos.ClearParameter();
                 objDatos.AddParameter("@ComComCodigo", ComComCodigo);
                 objDatos.AddParameter("@ComComNombre", ComComNombre);
                 objDatos.AddParameter("@AdmStsCodigo", AdmStsCodigo);

                 objDatos.ExecuteNonQuery("Spc_mComCom_Put", CommandType.StoredProcedure);

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