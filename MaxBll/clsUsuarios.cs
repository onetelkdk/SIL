using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxBll
{
    public class clsUsuarios
    {
        MaxData.clsDatos objDatos;

        public clsUsuarios()
        {
            objDatos = new MaxData.clsDatos();
            objDatos.ConectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;
            objDatos.OpenConection();
        }

        /// <summary>
        /// Obtiene la información del Usuario
        /// </summary>
        /// <param name="vAdmusrCodigo">Código del Usuario</param>
        /// <returns></returns>
        public DataTable mtGetUsuario(String vAdmusrCodigo)
        {
            DataTable dtReturn = new DataTable();
            try
            {
                objDatos.ClearParameter();
                if (!String.IsNullOrEmpty(vAdmusrCodigo))
                    objDatos.AddParameter("@AdmusrCodigo", vAdmusrCodigo);
                dtReturn = objDatos.getDataTable("Spc_mAdmUsr_Get", CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
            return dtReturn;
        }

        /// <summary>
        /// Inserta o edita la información de un Usuario
        /// </summary>
        /// <param name="vId">Id del Registro</param>
        /// <param name="vAdmusrCodigo">Código del Usuario</param>
        /// <param name="vAdmusrNombre">Nombre del Usuario</param>
        /// <param name="vAdmusrPassword">Clave del Usuario</param>
        /// <param name="vAdmusrCorreo">Correo del Usuario</param>
        /// <param name="vAdmrolCodigo">Rol Asignado al Usuario</param>
        /// <param name="vAdmStsCodigo">Estado del Usuario [1=Activo    0=Inactivo]</param>
        /// <param name="vAdmDepCodigo">Codigo del Departamento</param>
        /// <param name="vAdmusrCambiarClave">Si el usuario tiene que cambiar la Clave [1=Si    0=No]</param>
        public void mtPutUsuarios(Int32 vId, String vAdmusrCodigo, String vAdmusrNombre, String vAdmusrPassword, String vAdmusrCorreo
            , String vAdmrolCodigo, Boolean vAdmStsCodigo, Int32 vAdmDepCodigo, Boolean vAdmusrCambiarClave)
        {

            try
            {
                objDatos.ClearParameter();
                objDatos.AddParameter("@Id", vId);
                objDatos.AddParameter("@AdmusrCodigo", vAdmusrCodigo);
                objDatos.AddParameter("@AdmusrNombre", vAdmusrNombre);
                objDatos.AddParameter("@AdmusrPassword ", vAdmusrPassword);
                objDatos.AddParameter("@AdmusrCorreo", vAdmusrCorreo);
                objDatos.AddParameter("@AdmrolCodigo", vAdmrolCodigo);
                objDatos.AddParameter("@AdmStsCodigo", vAdmStsCodigo);
                objDatos.AddParameter("@AdmDepCodigo", vAdmDepCodigo);
                objDatos.AddParameter("@AdmusrCambiarClave", vAdmusrCambiarClave);

                objDatos.ExecuteNonQuery("Spc_mAdmUsr_Put", CommandType.StoredProcedure);

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
