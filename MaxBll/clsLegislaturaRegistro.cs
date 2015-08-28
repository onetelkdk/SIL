using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Custom references
using clsDatos = MaxData.clsDatos;

namespace MaxBll
{
    public class clsLegislaturaRegistro
    {
        #region Properties
        public enum TipoDatos
        {
            TipoId,
            PeriodoElecto,
            Estado,
            Sexo,
            PartidoPolitico
        }

        public enum TipoRegistro
        {
            Crear,
            Actualizar,
            Eliminar
        }

        clsDatos objDatos;

        #endregion

        public clsLegislaturaRegistro(bool isLoadGetDatos = true)
        {
            objDatos = new clsDatos();
            objDatos.ConectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;
            objDatos.OpenConection();
            if(isLoadGetDatos)
                dAdmEstGetDatos();
        }

        /// <summary>
        /// Método que retorna los legisladores registrados
        /// </summary>
        /// <returns>DataTable</returns>
        /// 
        public DataTable mAdmLegGetDatos()
        {
            DataTable dtReturn = new DataTable();
            try
            {
                objDatos.ClearParameter();
                dtReturn = objDatos.getDataTable("Spc_mAdmLeg_Get", CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
            return dtReturn;
        }

        /// <summary>
        /// Método que retorna el tipo de identificación
        /// </summary>
        /// <returns>DataTable</returns>
        /// 
        public DataTable mAdmParGetDatos(TipoDatos tipoDatos)
        {
            DataTable dtReturn = new DataTable();
            try
            {
                objDatos.ClearParameter();
                objDatos.AddParameter("@AdmParCodigo", tipoDatos.ToString());
                dtReturn = objDatos.getDataTable("Spc_mAdmPar_Get", CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
            return dtReturn;
        }

        /// <summary>
        ///  Método que retorna las posiciones/partidiarios
      /// </summary>
      /// <param name="AdmFunCodigo">Tipo de función</param>
      /// <returns>DataTable</returns>
        public DataTable mAdmFunGetDatos(short AdmFunCodigo)
        {
            DataTable dtReturn = new DataTable();
            try
            {
                objDatos.ClearParameter();
                objDatos.AddParameter("@AdmFunCodigo", AdmFunCodigo);
                dtReturn = objDatos.getDataTable("Spc_mAdmFun_Get", CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
            return dtReturn;
        }

        /// <summary>
        /// Método que retorna los partidos políticos
        /// </summary>
        /// <param name="AdmPdoCodigo">Identificador del partido político como parámetro.</param>
        /// <returns>DataTable</returns>
        public DataTable mAdmPodGetDatos(short AdmPdoCodigo)
        {
            DataTable dtReturn = new DataTable();
            try
            {
                objDatos.ClearParameter();
                objDatos.AddParameter("@AdmPdoCodigo", AdmPdoCodigo);
                dtReturn = objDatos.getDataTable("Spc_mAdmPdo_Get", CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
            return dtReturn;
        }

        /// <summary>
        /// Método que retorna los períodos de elección
        /// </summary>
        /// <param name="AdmPdoCodigo">Identificador del partido político como parámetro.</param>
        /// <returns>DataTable</returns>
        public DataTable mAdmPcoGetDatos()
        {
            DataTable dtReturn = new DataTable();
            try
            {
                objDatos.ClearParameter();
                dtReturn = objDatos.getDataTable("Spc_mAdmPco_Get", CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
            return dtReturn;
        }

        /// <summary>
        /// Método que retorna los estados de usuario
        /// </summary>
        /// <param name="AdmCatCodigo">Tipo de valores de retorno</param>
        /// <returns>DataTable</returns>
        public DataTable dAdmEstGetDatos(int AdmCatCodigo = 3)
        {
            DataTable dtReturn = new DataTable();
            try
            {
                objDatos.ClearParameter();
                objDatos.AddParameter("@AdmCatCodigo", AdmCatCodigo);
                dtReturn = objDatos.getDataTable("Spc_dAdmEst_Get", CommandType.StoredProcedure);
                return dtReturn;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Método que retorna los sectores municipales
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable mAdmSecGetDatos()
        {
            DataTable dtReturn = new DataTable();
            try
            {
                objDatos.ClearParameter();
                dtReturn = objDatos.getDataTable("Spc_mAdmSec_Get", CommandType.StoredProcedure);
                return dtReturn;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Método que retorna las ciudades
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable mAdmPrvGetDatos()
        {
            DataTable dtReturn = new DataTable();
            try
            {
                objDatos.ClearParameter();
                dtReturn = objDatos.getDataTable("Spc_mAdmPrv_Get", CommandType.StoredProcedure);
                return dtReturn;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Método que retorna los municipios
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable mAdmMunGetDatos()
        {
            DataTable dtReturn = new DataTable();
            try
            {
                objDatos.ClearParameter();
                dtReturn = objDatos.getDataTable("Spc_mAdmMun_Get", CommandType.StoredProcedure);
                return dtReturn;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Método que retorna las funciones
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable dComMbrGetDatos(string legislador)
        {
            DataTable dtReturn = new DataTable();
            try
            {
                objDatos.ClearParameter();
                objDatos.AddParameter("@Proponente", legislador);
                dtReturn = objDatos.getDataTable("Spc_dComMbr_Get", CommandType.StoredProcedure);
                return dtReturn;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Método que retorna las iniciativas
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable mIniIniGetDatos(DateTime fechaDesde, DateTime fechaHasta, string legislador)
        {
            DataTable dtReturn = new DataTable();
            try
            {
                objDatos.ClearParameter();
                objDatos.AddParameter("@FechaDesde", fechaDesde);
                objDatos.AddParameter("@FechaHasta", fechaHasta);
                objDatos.AddParameter("@PorLegislador", true);
                objDatos.AddParameter("@IniIniProponente", legislador);
                dtReturn = objDatos.getDataTable("Spc_mIniIni_Get", CommandType.StoredProcedure);
                return dtReturn;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Método que retorna las iniciativas por legislador y código de estado
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable mIniIniGetDatos(DateTime fechaDesde, DateTime fechaHasta, bool findLegislador, string legislador, string AdmEstCodigo)
        {
            DataTable dtReturn = new DataTable();
            try
            {
                
                objDatos.ClearParameter();
                objDatos.AddParameter("@FechaDesde", fechaDesde);
                objDatos.AddParameter("@FechaHasta", fechaHasta);
                objDatos.AddParameter("@PorLegislador", findLegislador);
                if(String.IsNullOrEmpty(legislador))
                    objDatos.AddParameter("@IniIniProponente", DBNull.Value);
                else
                    objDatos.AddParameter("@IniIniProponente", legislador);

                objDatos.AddParameter("@AdmEstCodigo", AdmEstCodigo);
                dtReturn = objDatos.getDataTable("Spc_mIniIni_Get", CommandType.StoredProcedure);
                return dtReturn;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Método que retorna las iniciativas por tipo de cierre
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable mIniIniGetDatos(DateTime fechaDesde, DateTime fechaHasta, bool findLegislador, bool EsCierre, string legislador, string AdmEstCodigo)
        {
            DataTable dtReturn = new DataTable();
            try
            {

                objDatos.ClearParameter();
                objDatos.AddParameter("@FechaDesde", fechaDesde);
                objDatos.AddParameter("@FechaHasta", fechaHasta);
                objDatos.AddParameter("@PorLegislador", findLegislador);
                objDatos.AddParameter("@EsCierre", EsCierre);
                objDatos.AddParameter("@IniIniProponente", legislador ?? (object)DBNull.Value);
                objDatos.AddParameter("@AdmEstCodigo", AdmEstCodigo ?? (object)DBNull.Value);

                dtReturn = objDatos.getDataTable("Spc_mIniIni_Get", CommandType.StoredProcedure);
                return dtReturn;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Método que retorna el estado de las iniciativas
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable mIniCacGetDatos()
        {
            DataTable dtReturn = new DataTable();
            try
            {
                objDatos.ClearParameter();
                dtReturn = objDatos.getDataTable("Spc_mIniCac_Get", CommandType.StoredProcedure);
                return dtReturn;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Método que retorna las excusas registradas por legislador --FALTA POR PARAMETRIZAR--
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable dComAleGetDatos(string AdmLegCedula)
        {
            DataTable dtReturn = new DataTable();
            try
            {
                objDatos.ClearParameter();
                objDatos.AddParameter("@AdmLegCedula", AdmLegCedula);
                dtReturn = objDatos.getDataTable("Spc_dComAle_Get", CommandType.StoredProcedure);
                return dtReturn;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Método para obtener las legislaturas
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable mtGetmAdmLet(string isOpen = null)
        {
            DataTable dtReturn = new DataTable();
            try
            {
                objDatos.ClearParameter();
                objDatos.AddParameter("@Abierto", isOpen ?? (object)DBNull.Value);
                dtReturn = objDatos.getDataTable("Spc_mAdmLet_Get", CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
            return dtReturn;
        }

        /// <summary>
        /// Método que retorna todas las iniciativas vigentes de Proyecto de Ley
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable mtGetmIniIniHist(DateTime FechaDesde, DateTime FechaHasta)
        {
            DataTable dtReturn = new DataTable();
            try
            {
                objDatos.ClearParameter();
                objDatos.AddParameter("@FechaDesde", FechaDesde);
                objDatos.AddParameter("@FechaHasta", FechaHasta);
                dtReturn = objDatos.getDataTable("Spc_mIniIniHist_Get", CommandType.StoredProcedure);
                return dtReturn;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Método para obtener IDPattern de la Legislatura abierta
        /// </summary>
        /// <returns>DataTable</returns>
        public Dictionary<String, string> mtGetmAdmOpenLetLast()
        {
            DataTable dt = new DataTable();

            try
            {
                objDatos.ClearParameter();
                objDatos.AddParameter("@Abierto", "Y" ?? (object)DBNull.Value);
                objDatos.AddParameter("@IsPreCierre", true);
                dt = objDatos.getDataTable("Spc_mAdmLet_Get", CommandType.StoredProcedure);

                Dictionary<String, string> listReturn = null;
                if (dt.Rows.Count > 0)
                {
                    listReturn = new Dictionary<string,string>()
                    {
                        { "AdmLetCodigo", dt.Rows[0]["AdmLetCodigo"].ToString() },
                        { "AdmEstCodigo", dt.Rows[0]["AdmEstCodigo"].ToString() },
                        { "AdmLetDescripcion", dt.Rows[0]["AdmLetDescripcion"] as string },
                        { "AdmLetFechaDesde", Convert.ToDateTime(dt.Rows[0]["AdmLetFechaDesde"]).ToShortDateString() },
                        { "AdmLetFechaHasta", Convert.ToDateTime(dt.Rows[0]["AdmLetFechaHasta"]).ToShortDateString() }
                    };
                }
                return listReturn;
            }
            catch
            {
                throw;
            }
        }

        public void mtPutmIniIniCierre(TipoRegistro tipoRegistro, bool IsPreCierre, short AdmLetCodigo, DateTime FechaDesde, DateTime FechaHasta)
        {
            objDatos.ClearParameter();
            try
            {
                switch (tipoRegistro)
                {
                    case TipoRegistro.Crear: 
                        objDatos.AddParameter("@Type", "I");
                        goto default;

                    case TipoRegistro.Actualizar: 
                        objDatos.AddParameter("@Type", "U");
                        goto default;

                    case TipoRegistro.Eliminar: 
                        break;

                    default: 
                        objDatos.AddParameter("@IsPreCierre", IsPreCierre);
                        objDatos.AddParameter("@AdmLetCodigo", AdmLetCodigo);
                        objDatos.AddParameter("@FechaDesde", FechaDesde);
                        objDatos.AddParameter("@FechaHasta", FechaHasta);
                        objDatos.getDataTable("Spc_mIniIniCierre_Put", CommandType.StoredProcedure);
                        break;
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Método CRUD para los legisladores
        /// </summary>
        /// <param name="isDelete">Manifiesta que se borrará el registro seleccionado.</param>
        /// <param name="param">Valores a insertar/actualizar un registro.</param>
        public void mtPutLegisladores(TipoRegistro tipoRegistro, params object[] param)
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
                        objDatos.AddParameter("@AdmLegCodigo", int.Parse(param[0] as string));
                        objDatos.ExecuteNonQuery("Spc_mAdmLeg_Put", CommandType.StoredProcedure);
                        break;

                    default:
                        objDatos.AddParameter("@AdmLegCodigo", int.Parse(param[0] as string));
                        objDatos.AddParameter("@AdmLegTipoId", int.Parse(param[1] as string));
                        objDatos.AddParameter("@AdmLegCedula ",     param[2] ?? DBNull.Value);
                        objDatos.AddParameter("@AdmlegNombres",     param[3] ?? DBNull.Value);
                        objDatos.AddParameter("@AdmlegApellido1",   param[4] ?? DBNull.Value);
                        objDatos.AddParameter("@AdmlegApellido2",   param[5] ?? DBNull.Value);
                        objDatos.AddParameter("@AdmlegSexo",        param[6] ?? DBNull.Value);
                        objDatos.AddParameter("@AdmFunCodigo",      param[7] ?? DBNull.Value);
                        objDatos.AddParameter("@AdmProProvincia",   param[8] ?? DBNull.Value);
                        objDatos.AddParameter("@AdmlegFoto",        param[9] ?? DBNull.Value);
                        objDatos.AddParameter("@AdmLegHuella",      param[10] ?? DBNull.Value);
                        objDatos.AddParameter("@AdmPdoCodigo",      param[11] ?? DBNull.Value);
                        objDatos.AddParameter("@AdmPcoCodigo",      param[12] ?? DBNull.Value);
                        objDatos.AddParameter("@AdmLegProfesion",   param[13] ?? DBNull.Value);
                        objDatos.AddParameter("@AdmLegFechaNac",    Convert.ToDateTime(param[14]));
                        objDatos.AddParameter("@AdmLegDireccion",          param[15] ?? DBNull.Value);
                        objDatos.AddParameter("@AdmPrvCodigo",             param[16] ?? DBNull.Value);
                        objDatos.AddParameter("@AdmMunCodigo",             param[17] ?? DBNull.Value);
                        objDatos.AddParameter("@AdmSecCodigo",             param[18] ?? DBNull.Value);
                        objDatos.AddParameter("@AdmLegCelular",            param[19] ?? DBNull.Value);
                        objDatos.AddParameter("@AdmLegTelefonoSenado",     param[20] ?? DBNull.Value);
                        objDatos.AddParameter("@AdmLegTelefonoProvincial", param[21] ?? DBNull.Value);
                        objDatos.AddParameter("@AdmLegCorreo",             param[22] ?? DBNull.Value);
                        objDatos.AddParameter("@AdmLegApartadoPostal",     param[23] ?? DBNull.Value);
                        objDatos.AddParameter("@AdmLegFax",                param[24] ?? DBNull.Value);
                        objDatos.AddParameter("@AdmLegTwitter",            param[25] ?? DBNull.Value);
                        objDatos.AddParameter("@AdmLegSitioWeb",           param[26] ?? DBNull.Value);
                        objDatos.AddParameter("@AdmLegLinkedlin",          param[27] ?? DBNull.Value);
                        objDatos.AddParameter("@AdmLegAreasInteres",       param[28] ?? DBNull.Value);
                        objDatos.AddParameter("@AdmLegPrioridad",          param[29] ?? DBNull.Value);
                        objDatos.AddParameter("@ExpExpNumero",             param[30] ?? DBNull.Value);
                        objDatos.AddParameter("@AdmLegUserdata",           param[31] ?? DBNull.Value);
                        objDatos.AddParameter("@AdmEstCodigo",             param[32] ?? DBNull.Value);
                        objDatos.AddParameter("@AdmLegUsuario",            param[33] ?? DBNull.Value);
                        objDatos.ExecuteNonQuery("Spc_mAdmLeg_Put", CommandType.StoredProcedure);
                        break;
                }
            }
            catch
            {
                throw;
            }
        }

        public void mAdmParDispose()
        {
            objDatos.CloseConection();
            objDatos = null;
        }
    }
}
