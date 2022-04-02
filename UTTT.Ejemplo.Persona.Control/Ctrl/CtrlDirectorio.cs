using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using UTTT.Ejemplo.Persona.Control.Interface;

namespace UTTT.Ejemplo.Persona.Control.Ctrl
{
    public class CtrlDirectorio : Conexion, IOperacion
    {
        public bool insertar(object _o)
        {
#pragma warning disable CS0168 // La variable '_e' se ha declarado pero nunca se usa
            try
            {
                UTTT.Ejemplo.Persona.Data.Entity.Directorio directorio = (UTTT.Ejemplo.Persona.Data.Entity.Directorio)_o;
                SqlConnection conn = base.sqlConnection();
                conn.Open();
                SqlCommand comm = new SqlCommand("INSERT INTO Directorio (idPersona, strTelefono) VALUES( '"
                 + directorio.IdPersona + "','"
                 + directorio.StrTelefono + "')", conn);
                comm.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            catch (Exception _e)
            {

            }
#pragma warning restore CS0168 // La variable '_e' se ha declarado pero nunca se usa
            return false;
        }

        public bool eliminar(object _o)
        {
#pragma warning disable CS0168 // La variable '_e' se ha declarado pero nunca se usa
            try
            {
                UTTT.Ejemplo.Persona.Data.Entity.Directorio directorio = (UTTT.Ejemplo.Persona.Data.Entity.Directorio)_o;
                SqlConnection conn = base.sqlConnection();
                conn.Open();
                SqlCommand comm = new SqlCommand("Delete from Directorio where id=" + directorio.Id, conn);
                comm.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            catch (Exception _e)
            {

            }
#pragma warning restore CS0168 // La variable '_e' se ha declarado pero nunca se usa
            return false;
        }

        public bool actualizar(object _o)
        {
#pragma warning disable CS0168 // La variable '_e' se ha declarado pero nunca se usa
            try
            {
                UTTT.Ejemplo.Persona.Data.Entity.Directorio directorio = (UTTT.Ejemplo.Persona.Data.Entity.Directorio)_o;
                SqlConnection conn = base.sqlConnection();
                conn.Open();
                SqlCommand comm = new SqlCommand("Update Directorio  set  strTelefono='" + directorio.StrTelefono +
                     "' where id=" + directorio.Id, conn);

                comm.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            catch (Exception _e)
            {

            }
#pragma warning restore CS0168 // La variable '_e' se ha declarado pero nunca se usa
            return false;
        }

        public List<object> consultarLista(object _o)
        {
#pragma warning disable CS0168 // La variable '_e' se ha declarado pero nunca se usa
            try
            {
                UTTT.Ejemplo.Persona.Data.Entity.Directorio directorio = (UTTT.Ejemplo.Persona.Data.Entity.Directorio)_o;
                SqlConnection conn = base.sqlConnection();
                conn.Open();

                SqlCommand comm = new SqlCommand("Select * from Directorio where IdPersona =" + directorio.IdPersona, conn);
                SqlDataReader reader = comm.ExecuteReader();

                List<Object> lista = new List<object>();
                while (reader.Read())
                {
                    UTTT.Ejemplo.Persona.Data.Entity.Directorio directorioTemp = new Data.Entity.Directorio();
                    directorioTemp.Id = int.Parse(reader["id"].ToString());
                    directorioTemp.IdPersona = int.Parse(reader["IdPersona"].ToString());
                    directorioTemp.StrTelefono = reader["strTelefono"].ToString();


                    Object objeto = directorioTemp;
                    lista.Add(objeto);
                }
                conn.Close();
                return lista;
            }
            catch (Exception _e)
            {

            }
#pragma warning restore CS0168 // La variable '_e' se ha declarado pero nunca se usa
            return null;
        }

        public object consultarItem(object _o)
        {
#pragma warning disable CS0168 // La variable '_e' se ha declarado pero nunca se usa
            try
            {
                UTTT.Ejemplo.Persona.Data.Entity.Directorio directorio = (UTTT.Ejemplo.Persona.Data.Entity.Directorio)_o;
                SqlConnection conn = base.sqlConnection();
                conn.Open();
                SqlCommand comm = new SqlCommand("Select * from Directorio where id=" + directorio.Id + " ", conn);
                SqlDataReader reader = comm.ExecuteReader();
                UTTT.Ejemplo.Persona.Data.Entity.Directorio directorioTemp = new Data.Entity.Directorio();
                while (reader.Read())
                {
                    directorioTemp.Id = int.Parse(reader["id"].ToString());
                    directorioTemp.IdPersona = int.Parse(reader["IdPersona"].ToString());
                    directorioTemp.StrTelefono = reader["strTelefono"].ToString();

                }
                conn.Close();
                Object objeto = directorioTemp;
                return objeto;
            }
            catch (Exception _e)
            {

            }
#pragma warning restore CS0168 // La variable '_e' se ha declarado pero nunca se usa
            return null;
        }
    }
}
