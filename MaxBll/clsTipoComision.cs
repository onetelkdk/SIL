﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxBll
{
    public class clsTipoComision
    {
        MaxData.clsDatos objDatos;

        public clsTipoComision()
        {
            objDatos = new MaxData.clsDatos();
            objDatos.ConectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;
            objDatos.OpenConection();
        }

        /// <summary>
        /// Método para obtener los Tipos de Menú.
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable mtGetTipos()
        {
            DataTable dtReturn = new DataTable();
            try
            {
                objDatos.ClearParameter();
                dtReturn = objDatos.getDataTable("Spc_mComTip_Get", CommandType.StoredProcedure);
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
