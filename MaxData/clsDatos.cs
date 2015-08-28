using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxData
{
    public class clsDatos
    {
            private SqlConnection oCon = new SqlConnection();
            private SqlCommand oCommand = null;
            private string _ConectionString = string.Empty;
            private string _Scalar = string.Empty;
            private Hashtable _parameter = new Hashtable();
            private SqlTransaction _transaction;
           
            /// <summary>
            /// Inicializa la cadena de conexion a base de datos
            /// </summary>
            public string ConectionString
            {
                get { return _ConectionString; }
                set { _ConectionString = value; }
            }

            /// <summary>
            /// Propiedad que contiene el Scalar devuelto del metodo ExecuteScalar.
            /// </summary>
            public string Scalar
            {
                get
                {
                    return _Scalar;
                }
            }

            /// <summary>
            /// Inicializa una Transaccion Sql.
            /// </summary>
            /// <returns></returns>
            public bool BeginTransaction()
            {
                bool returnValue = false;
                try
                {
                    _transaction = oCon.BeginTransaction();
                    returnValue = true;
                }
                catch 
                {
                    throw;
                }
                return returnValue;
            }

            /// <summary>
            /// Cancela una Transaccion sql iniciada.
            /// </summary>
            /// <returns></returns>
            public bool RollBackTransaction()
            {
                bool returnValue = false;
                try
                {
                    _transaction.Rollback();
                    returnValue = true;
                }
                catch 
                {
                    throw;
                }
                return returnValue;
            }

            /// <summary>
            /// Completa una transaccion sql iniciada.
            /// </summary>
            /// <returns></returns>
            public bool CommitTransaction()
            {
                bool returnValue = false;
                try
                {
                    _transaction.Commit();
                    returnValue = true;
                }
                catch 
                {
                    throw;
                }
                return returnValue;
            }

            /// <summary>
            /// Habre una coneccion sql.
            /// </summary>
            /// <returns></returns>
            public bool OpenConection()
            
            {
                bool returnValue = false;
                try
                {
                    // Se encontraba por fuera de la validación
                    if (oCon.State != ConnectionState.Open)
                    {
                        oCon.ConnectionString = _ConectionString;
                        oCon.Open();
                    }
                    returnValue = true;
                }
                catch 
                {
                    throw;
                }
                return returnValue;
            }

            /// <summary>
            /// Cierra una coneccion previamente abierta.
            /// </summary>
            /// <returns></returns>
            public bool CloseConection()
            {
                bool returnValue = false;
                try
                {
                    if (oCon.State == ConnectionState.Open)
                    {
                        oCon.Close();
                    }
                    returnValue = true;
                }
                catch 
                {
                    throw;
                }
                return returnValue;
            }

            /// <summary>
            /// Agrega los parametros a menejarse en las funciones de ejecucion.
            /// </summary>
            /// <param name="parameterName"></param>
            /// <param name="parameterValue"></param>
            /// <returns></returns>
            public bool AddParameter(string parameterName, object parameterValue)
            {
                bool returnValue = false;
                try
                {
                    _parameter.Add(parameterName, parameterValue);
                    returnValue = true;
                }
                catch 
                {
                    throw;
                }
                return returnValue;
            }

            /// <summary>
            /// Elimina la lista de parametros insertada.
            /// </summary>
            /// <returns></returns>
            public bool ClearParameter()
            {
                bool returnValue = false;
                try
                {
                    _parameter.Clear();
                    returnValue = true;

                }
                catch 
                {
                    throw;
                }
                return returnValue;
            }

            /// <summary>
            /// Elimina un parametro en especifico.
            /// </summary>
            /// <param name="parameterName"></param>
            /// <returns></returns>
            public bool RemoveParameter(object parameterName)
            {
                bool returnValue = false;
                try
                {
                    _parameter.Remove(parameterName);
                    returnValue = true;

                }
                catch 
                {
                    throw;
                }
                return returnValue;
            }

            /// <summary>
            /// Obtiene un datatable de la Sentencia o SP parado como parametro.
            /// </summary>
            /// <param name="SQL"></param>
            /// <param name="type"></param>
            /// <returns></returns>
            public DataTable getDataTable(string SQL, CommandType type)
            {
                DataTable dt = new DataTable();
                try
                {
                    oCommand = new SqlCommand(SQL, oCon, _transaction);
                    oCommand.CommandType = type;
                    foreach (DictionaryEntry oItem in _parameter)
                    {
                        oCommand.Parameters.AddWithValue(oItem.Key.ToString(), oItem.Value);
                    }
                    SqlDataReader reader = oCommand.ExecuteReader();
                    dt.Load(reader);
                    reader.Close();
                    return dt;
                }
                catch 
                {
                    throw;
                }

                return dt;
            }

            /// <summary>
            /// Ejecuta sentencia de sql que no devuelva valor.
            /// </summary>
            /// <param name="SQL"></param>
            /// <param name="type"></param>
            /// <returns></returns>
            public bool ExecuteNonQuery(string SQL, CommandType type)
            {
                bool returnValue = false;
                try
                {
                    oCommand = new SqlCommand(SQL, oCon, _transaction);
                    oCommand.CommandType = type;
                    foreach (DictionaryEntry oItem in _parameter)
                    {
                        oCommand.Parameters.AddWithValue(oItem.Key.ToString(), oItem.Value);
                    }
                    oCommand.ExecuteNonQuery();
                    returnValue = true;
                }
                catch 
                {
                    throw;
                }
                return returnValue;
            }

            /// <summary>
            /// Ejecuta sentencia devolviendo en la propiedad Scalar el valor.
            /// </summary>
            /// <param name="SQL"></param>
            /// <param name="type"></param>
            /// <returns></returns>
            public bool ExecuteScalar(string SQL, CommandType type)
            {
                bool returnValue = false;
                try
                {
                    oCommand = new SqlCommand(SQL, oCon, _transaction);
                    oCommand.CommandType = type;
                    foreach (DictionaryEntry oItem in _parameter)
                    {
                        oCommand.Parameters.AddWithValue(oItem.Key.ToString(), oItem.Value);
                    }
                    _Scalar = oCommand.ExecuteScalar().ToString();
                    returnValue = true;
                }
                catch 
                {
                    throw;
                }
                return returnValue;
            }
        }
    }
