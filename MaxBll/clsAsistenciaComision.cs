using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxBll
{
    public class clsAsistenciaComision
    {
         MaxData.clsDatos objDatos;

        public  clsAsistenciaComision()
        {
            objDatos = new MaxData.clsDatos();
            objDatos.ConectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;
            objDatos.OpenConection();
        }

        public DataTable mtGetAsistenciaComision(Int32 vComActCodigoSis)
        {
            DataTable dtReturn = new DataTable();
            try
            {
                objDatos.ClearParameter();
                objDatos.AddParameter("@ComActCodigoSis", vComActCodigoSis);
                dtReturn = objDatos.getDataTable("Spc_mComAsi_Get", CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
            return dtReturn;
        }

        public DataTable mtGetAsistenciaMiembros(Int32 vComAsiCodigo)
        {
            DataTable dtReturn = new DataTable();
            try
            {
                objDatos.ClearParameter();
                objDatos.AddParameter("@ComAsiCodigo", vComAsiCodigo);
                dtReturn = objDatos.getDataTable("Spc_dComAmbr_Get", CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
            return dtReturn;
        }

        public DataTable mtGetAsistenciaInvitados(Int32 vComAsiCodigo)
        {
            DataTable dtReturn = new DataTable();
            try
            {
                objDatos.ClearParameter();
                objDatos.AddParameter("@ComAsiCodigo", vComAsiCodigo);
                dtReturn = objDatos.getDataTable("Spc_dComAin_Get", CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
            return dtReturn;
        }

        public DataTable mtSetAsistenciaMiembros(Int32 vComAmbrCodigo, Int32 vAdmLegCodigo, Int32 vComAmbrLinea, String vComAmbrNombre
            , DateTime vComAmbrHoraLLegada, DateTime vComAmbrHoraSalida, Boolean vComAmbrPresente, Boolean vComAmbrExcusa, Int16 vAdmExcCodigo
            , String vComActDefinicionExc, Boolean vComAmbrDocAdjunto, String vComAmbrUsuario, Int32 vComAmbrMaster, Boolean vCtrl_L_Deleted=false) 
        {
            DataTable dtReturn = new DataTable();
            try
            {
                objDatos.ClearParameter();
                objDatos.AddParameter("@vComAmbrCodigo", vComAmbrCodigo);
                objDatos.AddParameter("@vAdmLegCodigo", vAdmLegCodigo);
                objDatos.AddParameter("@vComAmbrLinea", vComAmbrLinea);
                objDatos.AddParameter("@vComAmbrNombre", vComAmbrNombre);
                if (vComAmbrHoraLLegada != DateTime.MinValue) objDatos.AddParameter("@vComAmbrHoraLLegada", vComAmbrHoraLLegada); else objDatos.AddParameter("@vComAmbrHoraLLegada", DBNull.Value);
                if (vComAmbrHoraSalida != DateTime.MinValue) objDatos.AddParameter("@vComAmbrHoraSalida", vComAmbrHoraSalida); else objDatos.AddParameter("@vComAmbrHoraSalida", DBNull.Value);
                objDatos.AddParameter("@vComAmbrPresente", vComAmbrPresente);
                objDatos.AddParameter("@VComAmbrExcusa", vComAmbrExcusa);
                objDatos.AddParameter("@vAdmExcCodigo", vAdmExcCodigo);
                objDatos.AddParameter("@vComActDefinicionExc", vComActDefinicionExc);
                objDatos.AddParameter("@vComAmbrDocAdjunto", vComAmbrDocAdjunto);
                objDatos.AddParameter("@vComAmbrUsuario", vComAmbrUsuario);
                objDatos.AddParameter("@vComAmbrMaster", vComAmbrMaster);
                objDatos.AddParameter("@vCtrl_L_Deleted", vCtrl_L_Deleted);

                dtReturn = objDatos.getDataTable("Spc_dComAmbr_Put", CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
            return dtReturn;
        }

        public DataTable mtSetAsistenciaInvitados(Int32 vComAinCodigo, Int32 vComAinLinea, String vComAinNombre, DateTime vComAinHoraLLegada
            , DateTime vComAinHoraSalida, Boolean vComAinAsistio, String vComAinUsuario, Int32 vComAinMaster, Boolean vCtrl_L_Deleted=false) 
        {
            DataTable dtReturn = new DataTable();
            try
            {
                objDatos.ClearParameter();
                objDatos.AddParameter("@ComAinCodigo", vComAinCodigo);
                objDatos.AddParameter("@ComAinLinea", vComAinLinea);
                objDatos.AddParameter("@ComAinNombre", vComAinNombre);
                if (vComAinHoraLLegada != DateTime.MinValue) objDatos.AddParameter("@ComAinHoraLLegada", vComAinHoraLLegada); else objDatos.AddParameter("@ComAinHoraLLegada", DBNull.Value);
                if (vComAinHoraSalida != DateTime.MinValue) objDatos.AddParameter("@ComAinHoraSalida", vComAinHoraSalida); else objDatos.AddParameter("@ComAinHoraSalida", DBNull.Value);
                objDatos.AddParameter("@ComAinAsistio", vComAinAsistio);
                objDatos.AddParameter("@ComAinUsuario", vComAinUsuario);
                objDatos.AddParameter("@ComAinMaster", vComAinMaster);
                objDatos.AddParameter("@Ctrl_L_Deleted", vCtrl_L_Deleted);
                dtReturn = objDatos.getDataTable("Spc_dComAin_Put", CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
            return dtReturn;
        }

        public DataTable mtSetAsistenciaComision(Int32 vComActCodigoSis, Int32 vAdmLetCogido, Int32 vAdmPcoCogido, String vComAsiUsuario, Int32 vAdmstsCogido)
        {
            DataTable dtReturn = new DataTable();
            try
            {
                objDatos.ClearParameter();
                objDatos.AddParameter("@ComActCodigoSis", vComActCodigoSis);
                objDatos.AddParameter("@AdmLetCogido", vAdmLetCogido);
                objDatos.AddParameter("@AdmPcoCogido", vAdmPcoCogido);
                objDatos.AddParameter("@ComAsiUsuario", vComAsiUsuario);
                objDatos.AddParameter("@AdmstsCogido", vAdmstsCogido);
                dtReturn = objDatos.getDataTable("Spc_mComAsi_Put", CommandType.StoredProcedure);
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
    }
}
