using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace webApiEmpresa.Models
{
    public class DataBase
    {

        private string conexionString;

        public DataBase() {

            this.conexionString = ConfigurationManager.ConnectionStrings["companiadb"].ConnectionString;

        }

        public List<Compania> allCompania() {

            SqlConnection objConn = new SqlConnection(conexionString);

            string sql = "select * from tbcompany";

            SqlCommand cmd = new SqlCommand(sql,objConn);

            cmd.CommandType = System.Data.CommandType.Text;

            List<Compania> listCompania = new List<Compania>();

            objConn.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            //Se valida si se obtuvieron resultados

            if (reader.HasRows) {

                // se recorre los valores obtenidos.
                while (reader.Read()) {

                    listCompania.Add(new Compania(reader.GetInt32(0), reader.GetString(1), reader.GetString(2)));
                } // Fin del While 

            } // Fin del If.

            // Se cierra la conexion
            objConn.Close();

            return listCompania;

        } // fin del  metodo allCompania


        public Compania getCompania(Int32 id) {


            SqlConnection connection = new SqlConnection(this.conexionString);
            string sql = "select * from tbcompany where tbcompany.id = @id;";
            SqlCommand command = new SqlCommand(sql, connection);

            command.CommandType = System.Data.CommandType.Text;

            Compania com = new Compania();

            SqlParameter idParam = command.Parameters.Add("@id",SqlDbType.Int);

            idParam.Direction = ParameterDirection.Input;

            idParam.Value = id;

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows) {


                while (reader.Read()) {


                    com.Id = reader.GetInt32(0);
                    com.Nombre = reader.GetString(1);
                    com.Url = reader.GetString(2);


                } // fin while

            } // fin del If


            connection.Close();

            return com;
        } // fin del Metodo findById


        public Int32 postCompania(Compania compania) {

            compania.Nombre = compania.Nombre.Replace("'", "''");
            compania.Url = compania.Url.Replace("'", "''");

            SqlConnection connection = new SqlConnection(this.conexionString);
            SqlCommand command = new SqlCommand("createcompany", connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter nombreParameter = command.Parameters.Add("@name", SqlDbType.VarChar, 100);
            nombreParameter.Direction = ParameterDirection.Input;
            nombreParameter.Value = compania.Nombre;

            SqlParameter urlParameter = command.Parameters.Add("@url", SqlDbType.VarChar, 200);
            urlParameter.Direction = ParameterDirection.Input;
            urlParameter.Value = compania.Url;

            SqlParameter stateParameter = command.Parameters.Add("@state", SqlDbType.Int);
            stateParameter.Direction = ParameterDirection.ReturnValue;

            connection.Open();

            command.ExecuteNonQuery();

            connection.Close();

            return int.Parse(stateParameter.Value.ToString());

        } // fin metodo PostComapny


        public Int32 putCompania(Compania compania) {

            compania.Nombre = compania.Nombre.Replace("'","''");
            compania.Url = compania.Url.Replace("'","''");

            SqlConnection connection = new SqlConnection(this.conexionString);
            SqlCommand cmd = new SqlCommand("updatecompany",connection);
            cmd.CommandType = CommandType.StoredProcedure;

            // Se asignan los parámetros.
            SqlParameter idParam = cmd.Parameters.Add("@id", SqlDbType.Int); // Se crea un parámetro SQL, se le pasa el nombre, el tipo.
            idParam.Direction = ParameterDirection.Input; // Se le indica la dirección.
            idParam.Value = compania.Id; // Se le asigna el valor.
            SqlParameter nameParam = cmd.Parameters.Add("@name", SqlDbType.VarChar, 100); // Se crea un parámetro SQL, se le pasa el nombre, el tipo y la longitud.
            nameParam.Direction = ParameterDirection.Input; // Se le indica la dirección.
            nameParam.Value = compania.Nombre; // Se le asigna el valor.
            SqlParameter urlParam = cmd.Parameters.Add("@url", SqlDbType.VarChar, 200); // Se crea un parámetro SQL, se le pasa el nombre, el tipo y la longitud.
            urlParam.Direction = ParameterDirection.Input;
            urlParam.Value = compania.Url; // Se le asigna el valor.
            SqlParameter stateParam = cmd.Parameters.Add("@state", SqlDbType.Int); // Se crea un parámetro SQL, se le pasa el nombre, el tipo.
            stateParam.Direction = ParameterDirection.ReturnValue;

            connection.Open(); // Se abre la conexión.
            cmd.ExecuteNonQuery(); // Se ejecuta el comando SQL.
            connection.Close(); // Se cierra la conexión.

            return int.Parse(stateParam.Value.ToString());

        } // fin metodo Actualzar

        public Int32 deleteCompany(Int32 id)
        {
            SqlConnection objConn = new SqlConnection(this.conexionString); // Se crea un objeto de tipo conexión SQL.
            SqlCommand cmd = new SqlCommand("deletecompany", objConn); // Se crea el comando SQL, se le pasa por parámetro el nombre del procedimiento almacenado y una conexión SQL.
            cmd.CommandType = CommandType.StoredProcedure;

            // Se asignan los parámetros.
            SqlParameter idParam = cmd.Parameters.Add("@id", SqlDbType.Int); // Se crea un parámetro SQL, se le pasa el nombre, el tipo.
            idParam.Direction = ParameterDirection.Input; // Se le indica la dirección.
            idParam.Value = id; // Se le asigna el valor.
            SqlParameter stateParam = cmd.Parameters.Add("@state", SqlDbType.Int); // Se crea un parámetro SQL, se le pasa el nombre, el tipo.
            stateParam.Direction = ParameterDirection.ReturnValue;

            objConn.Open(); // Se abre la conexión.
            cmd.ExecuteNonQuery(); // Se ejecuta el comando SQL.
            objConn.Close(); // Se cierra la conexión.

            return int.Parse(stateParam.Value.ToString());
        }// Fin del método.


    }
}