using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace MaxBll
{
    public class clsIniciativas
    {

        MaxData.clsDatos objDatos;

        public clsIniciativas()
        {
            objDatos = new MaxData.clsDatos();
            objDatos.ConectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;
            objDatos.OpenConection();
        }



        public DataTable mtGetIniciativas(DateTime vFechaDesde, DateTime vFechaHasta, Int32 vAdmEstCodigo = 0)
        {
            DataTable dtReturn = new DataTable();
            try
            {
                objDatos.ClearParameter();
                objDatos.AddParameter("@FechaDesde", vFechaDesde);
                objDatos.AddParameter("@FechaHasta", vFechaHasta);
                if (vAdmEstCodigo > 0)
                    objDatos.AddParameter("@AdmEstCodigo", vAdmEstCodigo);
                dtReturn = objDatos.getDataTable("Spc_mIniIni_Get", CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
            return dtReturn;
        }

        public void mtSetIniciativaSGL(
            Int32 vIniIniCodigoSis
            , Int32 vIniIniVecesDev
            , String vIniIniNumArchivo
            , Boolean vIniIniPriorizada
            , DateTime vIniIniFechaPriorizada
            , Boolean vIniIniPerimida
            , DateTime vIniIniFechaPerimida
            , Boolean vIniIniConteoLeg
            , DateTime vIniIniFechaConteoLeg
            , Int32 vIniIniNumLegislaturaVigente
            , String vIniIniNumExpDiputados
            , Int32 vIniIniEnviadoComPor
            , Int32 vIniIniAnalizadoPor
            , Int32 vIniIniRevisadoPor
            , Int32 vIniIniDespachadopor
            , String vIniIniNumProm
            , String vIniIniAprobPresidida
            , Int32 vIniIniOficioEnvComis
            , String vIniIniMbrComisionesEsp
            , String vIniIniSecretarios)
        {
            try
            {
                objDatos.ClearParameter();
                objDatos.AddParameter("@IniIniCodigoSis", vIniIniCodigoSis);
                if (vIniIniVecesDev > 0) objDatos.AddParameter("@IniIniVecesDev", vIniIniVecesDev);
                if (!String.IsNullOrEmpty(vIniIniNumArchivo)) objDatos.AddParameter("@IniIniNumArchivo", vIniIniNumArchivo);
                objDatos.AddParameter("@IniIniPriorizada", vIniIniPriorizada);
                if (vIniIniPriorizada) objDatos.AddParameter("@IniIniFechaPriorizada", vIniIniFechaPriorizada);
                objDatos.AddParameter("@IniIniPerimida", vIniIniPerimida);
                if (vIniIniPerimida) objDatos.AddParameter("@IniIniFechaPerimida", vIniIniFechaPerimida);
                objDatos.AddParameter("@IniIniConteoLeg", vIniIniConteoLeg);
                if (vIniIniConteoLeg) objDatos.AddParameter("@IniIniFechaConteoLeg", vIniIniFechaConteoLeg);
                if (vIniIniNumLegislaturaVigente > 0) objDatos.AddParameter("@IniIniNumLegislaturaVigente", vIniIniNumLegislaturaVigente);
                if (!String.IsNullOrEmpty(vIniIniNumExpDiputados)) objDatos.AddParameter("@IniIniNumExpDiputados", vIniIniNumExpDiputados);
                if (vIniIniEnviadoComPor > 0) objDatos.AddParameter("@IniIniEnviadoComPor", vIniIniEnviadoComPor);
                if (vIniIniAnalizadoPor > 0) objDatos.AddParameter("@IniIniAnalizadoPor", vIniIniAnalizadoPor);
                if (vIniIniRevisadoPor > 0) objDatos.AddParameter("@IniIniRevisadoPor", vIniIniRevisadoPor);
                if (vIniIniDespachadopor > 0) objDatos.AddParameter("@IniIniDespachadopor", vIniIniDespachadopor);
                if (!String.IsNullOrEmpty(vIniIniNumProm)) objDatos.AddParameter("@IniIniNumProm", vIniIniNumProm);
                if (!String.IsNullOrEmpty(vIniIniAprobPresidida)) objDatos.AddParameter("@IniIniAprobPresidida", vIniIniAprobPresidida);
                if (vIniIniOficioEnvComis > 0) objDatos.AddParameter("@IniIniOficioEnvComis", vIniIniOficioEnvComis);
                if (!String.IsNullOrEmpty(vIniIniMbrComisionesEsp)) objDatos.AddParameter("@IniIniMbrComisionesEsp", vIniIniMbrComisionesEsp);
                if (!String.IsNullOrEmpty(vIniIniSecretarios)) objDatos.AddParameter("@IniIniSecretarios", vIniIniSecretarios);

                objDatos.ExecuteNonQuery("Spc_mIniIni_Put", CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }

        }

        public void mtSetIniciativaDTRL(
            Int32 vIniIniCodigoSis
            , Boolean vIniIniInformeAses
            , DateTime vIniIniFechaInformeAses
            , Boolean vIniIniInformeOpa
            , DateTime vIniIniFechaInformeOpa
            , Boolean vIniIniInformeDtrl
            , DateTime vIniIniFechaInformeDtrl
            , Boolean vIniIniInformeOtros
            , DateTime vIniIniFechaInformeOtros
            )
        {
            try
            {
                objDatos.ClearParameter();
                objDatos.AddParameter("@IniIniCodigoSis", vIniIniCodigoSis);
                objDatos.AddParameter("@IniIniInformeAses", vIniIniInformeAses);
                if (vIniIniInformeAses) objDatos.AddParameter("@IniIniFechaInformeAses", vIniIniFechaInformeAses);
                objDatos.AddParameter("@IniIniInformeOpa", vIniIniInformeOpa);
                if (vIniIniInformeOpa) objDatos.AddParameter("@IniIniFechaInformeOpa", vIniIniFechaInformeOpa);
                objDatos.AddParameter("@IniIniInformeDtrl", vIniIniInformeDtrl);
                if (vIniIniInformeDtrl) objDatos.AddParameter("@IniIniFechaInformeDtrl", vIniIniFechaInformeDtrl);
                objDatos.AddParameter("@IniIniInformeOtros", vIniIniInformeOtros);
                if (vIniIniInformeOtros) objDatos.AddParameter("@IniIniFechaInformeOtros", vIniIniFechaInformeOtros);

                objDatos.ExecuteNonQuery("Spc_mIniIni_Put", CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
        }

        public void mtSetIniciativaCORDCOM(
            Int32 vIniIniCodigoSis
            , Boolean vIniIniInformeComisiones
            , DateTime vIniIniFechaInformeComisiones
            , Int32 vIniIniInformeElaborado
            , Int32 vIniIniCorreccionEst
            , Int32 vIniIniCorreccionTec
            )
        {
            try
            {
                objDatos.ClearParameter();
                objDatos.AddParameter("@IniIniCodigoSis", vIniIniCodigoSis);
                objDatos.AddParameter("@IniIniInformeComisiones", vIniIniInformeComisiones);
                if (vIniIniInformeComisiones) objDatos.AddParameter("@IniIniFechaInformeComisiones", vIniIniFechaInformeComisiones);
                if (vIniIniInformeElaborado > 0) objDatos.AddParameter("@IniIniInformeElaborado", vIniIniInformeElaborado);
                if (vIniIniCorreccionEst > 0) objDatos.AddParameter("@IniIniCorreccionEst", vIniIniCorreccionEst);
                if (vIniIniCorreccionTec > 0) objDatos.AddParameter("@IniIniCorreccionTec", vIniIniCorreccionTec);

                objDatos.ExecuteNonQuery("Spc_mIniIni_Put", CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
        }

        public void mtSetIniciativaAUDLEG(
            Int32 vIniIniCodigoSis
             , Boolean vIniIniDespachada
             , DateTime vIniIniFechaDespachada
             , Int32 vIniIniDespachadoTrans
            )
        {
            try
            {
                objDatos.ClearParameter();
                objDatos.AddParameter("@IniIniCodigoSis", vIniIniCodigoSis);
                objDatos.AddParameter("@IniIniDespachada", vIniIniDespachada);
                if (vIniIniDespachada) objDatos.AddParameter("@IniIniFechaDespachada", vIniIniFechaDespachada);
                if (vIniIniDespachadoTrans > 0) objDatos.AddParameter("@IniIniDespachadoTrans", vIniIniDespachadoTrans);

                objDatos.ExecuteNonQuery("Spc_mIniIni_Put", CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
        }

        public void mtSetIniciativaTRANLEG(
            Int32 vIniIniCodigoSis
            , Int32 vIniIniRecibidoTrans
            , Int32 vIniIniTranscritoPor
            , Int32 vIniIniCorregidoTrans)
        {
            try
            {
                objDatos.ClearParameter();
                objDatos.AddParameter("@IniIniCodigoSis", vIniIniCodigoSis);

                if (vIniIniRecibidoTrans > 0) objDatos.AddParameter("@IniIniRecibidoTrans", vIniIniRecibidoTrans);
                if (vIniIniTranscritoPor > 0) objDatos.AddParameter("@IniIniTranscritoPor", vIniIniTranscritoPor);
                if (vIniIniCorregidoTrans > 0) objDatos.AddParameter("@IniIniCorregidoTrans", vIniIniCorregidoTrans);

                objDatos.ExecuteNonQuery("Spc_mIniIni_Put", CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
        }

        public void mtSetIniciativaDOCUM(
            Int32 vIniIniCodigoSis
            , Int32 vIniIniDespachadaHacia
            , String vIniIniNumOficioDesp
            , Boolean vIniIniPromulgada
            , DateTime vIniIniFechaPromulgada
            , String vIniIniNotasDespacho)
        {
            try
            {
                objDatos.ClearParameter();
                objDatos.AddParameter("@IniIniCodigoSis", vIniIniCodigoSis);

                if (vIniIniDespachadaHacia > 0) objDatos.AddParameter("@IniIniDespachadaHacia", vIniIniDespachadaHacia);
                if (!String.IsNullOrEmpty(vIniIniNumOficioDesp)) objDatos.AddParameter("@IniIniNumOficioDesp", vIniIniNumOficioDesp);
                objDatos.AddParameter("@IniIniPromulgada", vIniIniPromulgada);
                if (vIniIniPromulgada) objDatos.AddParameter("@IniIniFechaPromulgada", vIniIniFechaPromulgada);
                if (!String.IsNullOrEmpty(vIniIniNotasDespacho)) objDatos.AddParameter("@IniIniNotasDespacho", vIniIniNotasDespacho);

                objDatos.ExecuteNonQuery("Spc_mIniIni_Put", CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Método para modificar el Estado de una Iniciativa
        /// </summary>
        /// <param name="vIniIniCodigoSis">Código de la Iniciativa</param>
        /// <param name="vAdmEstCodigo">Código del Estado de la Iniciativa</param>
        public void mtSetIniciativas(Int32 vIniIniCodigoSis, Int32 vAdmEstCodigo)
        {
            try
            {
                objDatos.ClearParameter();
                objDatos.AddParameter("@IniIniCodigoSis", vIniIniCodigoSis);
                objDatos.AddParameter("@AdmEstCodigo", vAdmEstCodigo);
                objDatos.ExecuteNonQuery("Spc_mIniIni_Put", CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Método para insertar una iniciativa
        /// </summary>
        /// <param name="vIniIniCodigoSis"></param>
        /// <param name="vIniIniNumero"></param>
        /// <param name="vIniIniMateria"></param>
        /// <param name="vAdmpcoCodigo"></param>
        /// <param name="vIniIniPriorizada"></param>
        /// <param name="vIniIniFechaPriorizada"></param>
        /// <param name="vIniTipCodigo"></param>
        /// <param name="vIniStpCodigo"></param>
        /// <param name="vIniIniFecha"></param>
        /// <param name="vIniIniDescripcion"></param>
        /// <param name="vIniIniProponentes"></param>
        /// <param name="vIniIniObservaciones"></param>
        /// <param name="vComCfmId"></param>
        /// <param name="vAdmAnoCodigo"></param>
        /// <param name="vIniIniVecesDev"></param>
        /// <param name="vIniIniPoderOrigen"></param>
        /// <param name="vIniIniConteoLeg"></param>
        /// <param name="vIniIniFechaConteoLeg"></param>
        /// <param name="vIniIniPerimida"></param>
        /// <param name="vIniIniFechaPerimida"></param>
        /// <param name="vIniIniDigitadoPor"></param>
        /// <param name="vAdmCamCodigo"></param>
        /// <param name="vIniIniCreadoPor"></param>
        /// <param name="vIniIniNumOficioOrig"></param>
        /// <param name="vIniIniUsuario"></param>
        public void mtPutIniciativa(
            //Primer Tab
            Int32 vIniIniCodigoSis
            , String vIniIniNumero
            , Int32 vIniIniMateria
            , Int32 vAdmpcoCodigo
            , Boolean vIniIniPriorizada
            , DateTime vIniIniFechaPriorizada
            , Int32 vIniTipCodigo
            , Int32 vIniStpCodigo
            , DateTime vIniIniFecha
            , String vIniIniDescripcion
            , String vIniIniProponentes
            , String vIniIniObservaciones
            //Segundo Tab
            , String vComCfmId
            , Int32 vAdmAnoCodigo
            , Int32 vIniIniVecesDev
            , Int32 vIniIniPoderOrigen
            , Boolean vIniIniConteoLeg
            , DateTime vIniIniFechaConteoLeg
            , Boolean vIniIniPerimida
            , DateTime vIniIniFechaPerimida
            , Int32 vIniIniDigitadoPor
            , Int32 vAdmCamCodigo
            , Int32 vIniIniCreadoPor
            , String vIniIniNumOficioOrig
            , String vIniIniUsuario
            )
        {
            try
            {
                objDatos.ClearParameter();
                objDatos.AddParameter("@IniIniCodigoSis", vIniIniCodigoSis);
                objDatos.AddParameter("@IniIniNumero", vIniIniNumero);
                if (vIniIniMateria > 0)
                    objDatos.AddParameter("@IniIniMateria", vIniIniMateria);
                if (vAdmpcoCodigo > 0)
                    objDatos.AddParameter("@AdmpcoCodigo", vAdmpcoCodigo);
                objDatos.AddParameter("@IniIniPriorizada", vIniIniPriorizada);
                if (vIniIniPriorizada)
                    objDatos.AddParameter("@IniIniFechaPriorizada", vIniIniFechaPriorizada);
                else
                    objDatos.AddParameter("@IniIniFechaPriorizada", DBNull.Value);
                if (vIniTipCodigo > 0)
                    objDatos.AddParameter("@IniTipCodigo", vIniTipCodigo);
                if (vIniStpCodigo > 0)
                    objDatos.AddParameter("@IniStpCodigo", vIniStpCodigo);
                objDatos.AddParameter("@IniIniFecha", vIniIniFecha);
                objDatos.AddParameter("@IniIniDescripcion", vIniIniDescripcion);
                if (!String.IsNullOrEmpty(vIniIniProponentes))
                    objDatos.AddParameter("@IniIniProponentes", vIniIniProponentes);
                else
                    objDatos.AddParameter("@IniIniProponentes", DBNull.Value);
                if (!String.IsNullOrEmpty(vIniIniObservaciones))
                    objDatos.AddParameter("@IniIniObservaciones", vIniIniObservaciones);
                else
                    objDatos.AddParameter("@IniIniObservaciones", DBNull.Value);
                if (!String.IsNullOrEmpty(vComCfmId))
                    objDatos.AddParameter("@ComCfmId", vComCfmId);
                else
                    objDatos.AddParameter("@ComCfmId", DBNull.Value);

                if (vAdmAnoCodigo > 0)
                    objDatos.AddParameter("@AdmAnoCodigo", vAdmAnoCodigo);
                else
                    objDatos.AddParameter("@AdmAnoCodigo", DBNull.Value);

                if (vIniIniVecesDev > 0)
                    objDatos.AddParameter("@IniIniVecesDev", vIniIniVecesDev);
                else
                    objDatos.AddParameter("@IniIniVecesDev", DBNull.Value);
                if (vIniIniPoderOrigen > 0)
                    objDatos.AddParameter("@IniIniPoderOrigen", vIniIniPoderOrigen);
                else
                    objDatos.AddParameter("@IniIniPoderOrigen", DBNull.Value);
                objDatos.AddParameter("@IniIniConteoLeg", vIniIniConteoLeg);
                if (vIniIniConteoLeg)
                    objDatos.AddParameter("@IniIniFechaConteoLeg", vIniIniFechaConteoLeg);
                else
                    objDatos.AddParameter("@IniIniFechaConteoLeg", DBNull.Value);
                objDatos.AddParameter("@IniIniPerimida", vIniIniPerimida);
                if (vIniIniPerimida)
                    objDatos.AddParameter("@IniIniFechaPerimida", vIniIniFechaPerimida);
                else
                    objDatos.AddParameter("@IniIniFechaPerimida", DBNull.Value);
                if (vIniIniDigitadoPor > 0)
                    objDatos.AddParameter("@IniIniDigitadoPor", vIniIniDigitadoPor);
                else
                    objDatos.AddParameter("@IniIniDigitadoPor", DBNull.Value);
                if (vAdmCamCodigo > 0)
                    objDatos.AddParameter("@AdmCamCodigo", vAdmCamCodigo);
                else
                    objDatos.AddParameter("@AdmCamCodigo", DBNull.Value);
                if (vIniIniCreadoPor > 0)
                    objDatos.AddParameter("@IniIniCreadoPor", vIniIniCreadoPor);
                else
                    objDatos.AddParameter("@IniIniCreadoPor", DBNull.Value);
                if (String.IsNullOrEmpty(vIniIniNumOficioOrig))
                    objDatos.AddParameter("@IniIniNumOficioOrig", vIniIniNumOficioOrig);
                else
                    objDatos.AddParameter("@IniIniNumOficioOrig", DBNull.Value);
                objDatos.AddParameter("@IniIniUsuario", vIniIniUsuario);

                objDatos.ExecuteNonQuery("Spc_mIniIni_Put", CommandType.StoredProcedure);
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
