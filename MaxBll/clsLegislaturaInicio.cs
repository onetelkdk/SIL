using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace MaxBll
{
    public class clsLegislaturaInicio
    {
         MaxData.clsDatos objDatos;

         public clsLegislaturaInicio()
        {
            objDatos = new MaxData.clsDatos();
            objDatos.ConectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;
            objDatos.OpenConection();
        }


         /// <summary>
         /// Método para obtener los tipos de legislatura de inicio de la aplicación.
         /// </summary>
         /// <returns>DataTable</returns>
         public DataTable mtGetclsLegislaturaInicio()
         {
             DataTable dtReturn = new DataTable();
             try
             {
                 dtReturn = objDatos.getDataTable("Spc_mAdmLet_Get", CommandType.StoredProcedure);
             }
             catch
             {
                 throw;
             }
             return dtReturn;
         }

         public DataTable mtGetLegislaturaActiva()
         {
             DataTable dtReturn = new DataTable();
             try
             {
                 objDatos.ClearParameter();
                 objDatos.AddParameter("@Abierto", "Y");
                 dtReturn = objDatos.getDataTable("Spc_mAdmLet_Get", CommandType.StoredProcedure);
             }
             catch
             {
                 throw;
             }
             return dtReturn;
         }

         public DataTable mtGetLegislatura(string AdmLetDescripcion)
         {
             DataTable dtReturn = new DataTable();
             try
             {
                 objDatos.ClearParameter();
                 objDatos.AddParameter("@AdmLetDescripcion", AdmLetDescripcion);
                 dtReturn = objDatos.getDataTable("Spc_mAdmLet_Get2", CommandType.StoredProcedure);
             }
             catch
             {
                 throw;
             }
             return dtReturn;
         }

         public DataTable mtGetLegislaturaActivaCombo()
         {
             DataTable dtReturn = new DataTable();
             try
             {
                 //objDatos.ClearParameter();
                 //objDatos.AddParameter("@AdmEstCodigo", 176);
                 dtReturn = objDatos.getDataTable("Spc_mAdmLet_Get2", CommandType.StoredProcedure);
             }
             catch
             {
                 throw;
             }
             return dtReturn;
         }

         public DataTable mtGetTipoLegislatura()
         {
             DataTable dtReturn = new DataTable();
             try
             {
                 dtReturn = objDatos.getDataTable("Spc_mAdmLtp_Get", CommandType.StoredProcedure);
             }
             catch
             {
                 throw;
             }
             return dtReturn;
         }

         public void mtPutLegislatura(int AdmLetCodigo, string AdmLetAno, string AdmLetDescripcion, DateTime AdmLetFechaDesde, DateTime AdmLetFechaHasta,
                                      int AdmPcoCodigo, int AdmLtpCodigo, int AdmEstCodigo, string AdmLetUsuario)
         {
             try
             {
                 objDatos.ClearParameter();
                 objDatos.AddParameter("@AdmLetCodigo", AdmLetCodigo);
                 objDatos.AddParameter("@AdmLetAno", AdmLetAno);
                 objDatos.AddParameter("@AdmLetDescripcion", AdmLetDescripcion);
                 objDatos.AddParameter("@AdmLetFechaDesde", AdmLetFechaDesde);
                 objDatos.AddParameter("@AdmLetFechaHasta", AdmLetFechaHasta);
                 objDatos.AddParameter("@AdmPcoCodigo", AdmPcoCodigo);
                 objDatos.AddParameter("@AdmLtpCodigo", AdmLtpCodigo);
                 objDatos.AddParameter("@AdmEstCodigo", AdmEstCodigo);
                 objDatos.AddParameter("@AdmLetUsuario", AdmLetUsuario);

                 objDatos.ExecuteNonQuery("Spc_mAdmLet_Put", CommandType.StoredProcedure);

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
