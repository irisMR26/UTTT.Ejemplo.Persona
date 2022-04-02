using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using UTTT.Ejemplo.Persona.Control.Interface;


namespace UTTT.Ejemplo.Persona.Control.Ctrl
{
    public class CtrlNumeroSeguro : Conexion, IOperacion
    {
        public bool insertar(object _o)
        {
#pragma warning disable CS0168 // La variable '_e' se ha declarado pero nunca se usa
            try
            {
                UTTT.Ejemplo.Persona.Data.Entity.NumeroSeguro seguro = (UTTT.Ejemplo.Persona.Data.Entity.NumeroSeguro)_o;
                SqlConnection conn = base.sqlConnection();
                conn.Open();
                SqlCommand comm = new SqlCommand("INSERT INTO NumeroSeguro (idPersona, strNumeroSeguro, strNumAsegurados, strTipoSangre) VALUES( '"
                 + seguro.IdPersona + "','"
                 + seguro.StrNumeroSeguro + "','"
                 + seguro.StrNumAsegurados + "','"
                  + seguro.StrTipoSangre + "')", conn);
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
                UTTT.Ejemplo.Persona.Data.Entity.NumeroSeguro seguro = (UTTT.Ejemplo.Persona.Data.Entity.NumeroSeguro)_o;
                SqlConnection conn = base.sqlConnection();
                conn.Open();
                SqlCommand comm = new SqlCommand("Delete from NumeroSeguro where id=" + seguro.Id, conn);
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
                UTTT.Ejemplo.Persona.Data.Entity.NumeroSeguro seguro = (UTTT.Ejemplo.Persona.Data.Entity.NumeroSeguro)_o;
                SqlConnection conn = base.sqlConnection();
                conn.Open();
                SqlCommand comm = new SqlCommand("Update NumeroSeguro  set  strNumeroSeguro='" + seguro.StrNumeroSeguro +
                     "', strNumAsegurados ='" + seguro.StrNumAsegurados +
                         "', strTipoSangre ='" + seguro.StrTipoSangre +
                              "' where id=" + seguro.Id, conn);

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
                UTTT.Ejemplo.Persona.Data.Entity.NumeroSeguro seguro = (UTTT.Ejemplo.Persona.Data.Entity.NumeroSeguro)_o;
                SqlConnection conn = base.sqlConnection();
                conn.Open();

                SqlCommand comm = new SqlCommand("Select * from NumeroSeguro where IdPersona =" + seguro.IdPersona, conn);
                SqlDataReader reader = comm.ExecuteReader();

                List<Object> lista = new List<object>();
                while (reader.Read())
                {
                    UTTT.Ejemplo.Persona.Data.Entity.NumeroSeguro seguroTemp = new Data.Entity.NumeroSeguro();
                    seguroTemp.Id = int.Parse(reader["id"].ToString());
                    seguroTemp.IdPersona = int.Parse(reader["IdPersona"].ToString());
                    seguroTemp.StrNumeroSeguro = reader["strNumeroSeguro"].ToString();
                    seguroTemp.StrNumAsegurados = reader["strNumAsegurados"].ToString();
                    seguroTemp.StrTipoSangre = reader["strTipoSangre"].ToString();
                    Object objeto = seguroTemp;
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
                UTTT.Ejemplo.Persona.Data.Entity.NumeroSeguro seguro = (UTTT.Ejemplo.Persona.Data.Entity.NumeroSeguro)_o;
                SqlConnection conn = base.sqlConnection();
                conn.Open();
                SqlCommand comm = new SqlCommand("Select * from NumeroSeguro where id=" + seguro.Id + " ", conn);
                SqlDataReader reader = comm.ExecuteReader();
                UTTT.Ejemplo.Persona.Data.Entity.NumeroSeguro seguroTemp = new Data.Entity.NumeroSeguro();
                while (reader.Read())
                {
                    seguroTemp.Id = int.Parse(reader["id"].ToString());
                    seguroTemp.IdPersona = int.Parse(reader["IdPersona"].ToString());
                    seguroTemp.StrNumeroSeguro = reader["strNumeroSeguro"].ToString();
                    seguroTemp.StrNumAsegurados = reader["strNumAsegurados"].ToString();
                    seguroTemp.StrTipoSangre = reader["strTipoSangre"].ToString();
                }
                conn.Close();
                Object objeto = seguroTemp;
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
