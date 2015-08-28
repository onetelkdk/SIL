using MaxData;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxBll
{
    public class clsSourceBase : clsDatos, IDisposable
    {
        bool disposed = false;
        private static clsDatos objDatos;
        public static clsDatos ObjDatos
        {
            get 
            {
                return objDatos ?? (objDatos = new clsDatos());
            }
        }

        public clsSourceBase()
        {
            ObjDatos.ConectionString = ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;
            ObjDatos.OpenConection();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                objDatos.CloseConection();
                objDatos = null;
            }

            disposed = true;
        }
    }
}
