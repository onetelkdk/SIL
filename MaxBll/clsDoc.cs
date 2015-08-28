using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxBll
{
    public class clsDoc
    {
        MaxData.clsDatos objDatos;

        public clsDoc()
        {
            objDatos = new MaxData.clsDatos();
            objDatos.ConectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;
            objDatos.OpenConection();
        }

        public DataTable mtGetDoc(Int32 vAdmDocMaster)
        {
            DataTable dtReturn = new DataTable();
            try
            {
                objDatos.ClearParameter();
                objDatos.AddParameter("@AdmDocMaster", vAdmDocMaster);
                dtReturn = objDatos.getDataTable("Spc_mAdmDoc_Get", CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
            return dtReturn;
        }

        public DataTable mtGetCargaDocumentos()
        {
            DataTable dtReturn = new DataTable();
            try
            {
                objDatos.ClearParameter();
                objDatos.AddParameter("@AdmCatCargaDoc", 2);
                dtReturn = objDatos.getDataTable("Spc_mAdmCat_Get", CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
            return dtReturn;
        }

        public DataTable mtGetDocumentos(int Documento, DateTime FechaDesde, DateTime FechaHasta)
        {
            DataTable dtReturn = new DataTable();
            try
            {
                objDatos.ClearParameter();
                objDatos.AddParameter("@TipoDocumento", Documento);
                objDatos.AddParameter("@FechaDesde", FechaDesde);
                objDatos.AddParameter("@FechaHasta", FechaHasta);
                dtReturn = objDatos.getDataTable("Spc_mAdmDoc_Get2", CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
            return dtReturn;
        }

        public int mtPutDocumentos(int AdmDocNumero, int AdmDocMaster, int AdmCatCodigo, string AdmDocUsuario)
        {
            DataTable dtReturn = new DataTable();
            int id = 0;
            try
            {
                
                objDatos.ClearParameter();
                objDatos.AddParameter("@AdmDocNumero", AdmDocNumero);
                objDatos.AddParameter("@AdmDocMaster", AdmDocMaster);
                objDatos.AddParameter("@AdmCatCodigo", AdmCatCodigo);
                objDatos.AddParameter("@AdmDocUsuario", AdmDocUsuario);

                dtReturn = objDatos.getDataTable("Spc_mAdmDoc_Put", CommandType.StoredProcedure);

                if (dtReturn.Rows.Count > 0)
                {
                    foreach (DataRow d in dtReturn.Rows)
                    {
                        id = int.Parse(d[0].ToString());
                    }

                }
            }
            catch
            {
                throw;
            }
            return id;
        }

        public DataTable mtGetDocumentosDetalle(int AdmDocMaster, int AdmCatCodigo)
        {
            DataTable dtReturn = new DataTable();
            try
            {
                objDatos.ClearParameter();
                objDatos.AddParameter("@AdmDocMaster", AdmDocMaster);
                objDatos.AddParameter("@AdmCatCodigo", AdmCatCodigo);
                dtReturn = objDatos.getDataTable("Spc_dAdmDoc_Get", CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
            return dtReturn;
        }
        public void mtPutDocumentosDetalle(int AdmDocNumero, int AdmDocSecuencia, int AdmDclCodigo, int AdmDtpCodigo, string AdmDocTitulo,
                                           string AdmDocRuta, string AdmDocNombre, string AdmDocExtension,string AdmDocUsuario, int Ctrl_L_Deleted)
        {
            try
            {
                objDatos.ClearParameter();
                objDatos.AddParameter("@AdmDocNumero", AdmDocNumero);
                objDatos.AddParameter("@AdmDocSecuencia", AdmDocSecuencia);
                objDatos.AddParameter("@AdmDclCodigo", AdmDclCodigo);
                objDatos.AddParameter("@AdmDtpCodigo", AdmDtpCodigo);
                objDatos.AddParameter("@AdmDocTitulo", AdmDocTitulo);
                objDatos.AddParameter("@AdmDocRuta", AdmDocRuta);
                objDatos.AddParameter("@AdmDocNombre", AdmDocNombre);
                objDatos.AddParameter("@AdmDocExtension", AdmDocExtension);
                objDatos.AddParameter("@AdmDocUsuario", AdmDocUsuario);
                objDatos.AddParameter("@Ctrl_L_Deleted", Ctrl_L_Deleted);

                objDatos.ExecuteNonQuery("Spc_dAdmDoc_Put", CommandType.StoredProcedure);

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
