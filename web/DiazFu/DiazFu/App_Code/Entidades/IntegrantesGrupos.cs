﻿using DiazFu.App_Code.Utilerias;
using SQLHelper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DiazFu.App_Code.Entidades
{
    public class IntegrantesGrupos
    {
        #region Propiedades
        private int? _Id;

        public int? Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        private int? _IdGrupo;

        public int? IdGrupo
        {
            get { return _IdGrupo; }
            set { _IdGrupo = value; }
        }

        private int? _IdCliente;

        public int? IdCliente
        {
            get { return _IdCliente; }
            set { _IdCliente = value; }
        }

        private int _IdEstatus;

        public int IdEstatus
        {
            get { return _IdEstatus; }
            set { _IdEstatus = value; }
        }

        private int _IdUsuario;

        public int IdUsuario
        {
            get { return _IdUsuario; }
            set { _IdUsuario = value; }
        }
        #endregion

        #region Constructor
        public IntegrantesGrupos()
        {

        }

        public IntegrantesGrupos(int IdUsuario)
        {
            this.IdEstatus = 1;
            this.IdUsuario = IdUsuario;
        }
        #endregion

        #region Métodos / Funciones
        /// <summary>
        /// Método para agregar un integrante al grupo.
        /// </summary>
        public DataSet Agregar()
        {
            DataSet Consulta = EjecutarSP(1);
            this.Id = int.Parse(Consulta.Tables[0].Rows[0]["Id"].ToString());
            return Consulta;
        }

        /// <summary>
        /// Método para actualizar un integrante al grupo.
        /// </summary>
        public DataSet Actualizar()
        {
            return EjecutarSP(2);
        }

        /// <summary>
        /// Función para consultar todos los integrantes del grupo activos.
        /// </summary>
        /// <returns>Data Set con todos los clientes activos.</returns>
        public DataSet ConsultarTodo()
        {
            this.Id = null;
            return EjecutarSP(3);
        }

        /// <summary>
        /// Método para consultar un integrante del grupo según su ID.
        /// </summary>
        public void ConsultarID()
        {
            DataSet Consulta = new DataSet();
            Consulta = EjecutarSP(3);
            if (Consulta.Tables[0].Rows.Count > 0)
            {
                DataRow Fila = Consulta.Tables[0].Rows[0];
                this.Id = int.Parse(Fila["Id"].ToString());
                this.IdGrupo = int.Parse(Fila["Nombre"].ToString());
                this.IdCliente = int.Parse(Fila["TelefonoCasa"].ToString());
                this.IdEstatus = int.Parse(Fila["IdEstatus"].ToString());
            }
            else
            {
                this.Id = null;
            }
        }

        /// <summary>
        /// Función para ejecutar el procedimiento almacenado seleccionado.
        /// </summary>
        /// <returns>Data Set con la consulta emitida por SQL</returns>
        public DataSet EjecutarSP(int Opcion)
        {
            List<SqlParameter> Parametros = new List<SqlParameter>();
            Parametros.Add(new SqlParameter("@Opcion", Opcion));
            Parametros.Add(new SqlParameter("@Id", Id));
            Parametros.Add(new SqlParameter("@IdGrupo", IdGrupo));
            Parametros.Add(new SqlParameter("@IdCliente", IdCliente));
            Parametros.Add(new SqlParameter("@IdEstatus", IdEstatus));
            Parametros.Add(new SqlParameter("@IdUsuarioActual", IdUsuario));

            return SqlHelper.ExecuteDataset(Conexion.CadenaConexion(), "[datos].[SPIntegrantesGrupos]", Parametros.ToArray());
        }
        #endregion
    }
}