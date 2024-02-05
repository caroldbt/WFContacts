using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsContacts
{
    public class DataAccessLayer
    {
        //Crear conexion
        private readonly SqlConnection conn = new SqlConnection("Persist Security Info=False;User ID=sa;Password=Facultad1;Initial Catalog=WinFormsContacts;Data Source=LAPTOP-BU8OJO77");

        //INSERTAR DATOS
        public void InsertContact(Contact contact)
        {
            try
            {
                //Abrir conexion
                conn.Open();
                //Crear el insert
                string query = @"INSERT INTO Contacts(FirstName, LastName, Phone, Address)
                                VALUES (@FirstName, @LastName, @Phone, @Address)";
                SqlParameter firstName = new SqlParameter("@FirstName",contact.FirstName);
                //firstName.ParameterName = "@FirstName";
                //firstName.Value = contact.FirstName;
                //firstName.DbType = System.Data.DbType.String;

                SqlParameter lastName = new SqlParameter("@LastName",contact.LastName);
                SqlParameter phone = new SqlParameter("@Phone",contact.Phone);
                SqlParameter address = new SqlParameter("@Address", contact.Address);

                
                SqlCommand cmd = new SqlCommand(query, conn);
                //Agregar los parametros
                cmd.Parameters.Add(firstName);
                cmd.Parameters.Add(lastName);
                cmd.Parameters.Add(phone);
                cmd.Parameters.Add(address);

                //Ejecutar la consulta
                cmd.ExecuteNonQuery();

            }
            catch (Exception)
            {
                throw;
            }finally 
            {
                conn.Close();
            }
        }


        //Actualizar usuarios
        public void UpdateContact(Contact contact)
        {
            try
            {
                conn.Open();
                string query = @"UPDATE Contacts
                                SET  FirstName = @FirstName,
                                     LastName = @LastName,
                                     Phone = @Phone,
                                     Address = @Address
                                WHERE Id =@Id";
                SqlParameter id = new SqlParameter("@Id", contact.Id);
                SqlParameter firstName = new SqlParameter("@FirstName", contact.FirstName);
                SqlParameter lastName = new SqlParameter("@LastName", contact.LastName);
                SqlParameter phone = new SqlParameter("@Phone", contact.Phone);
                SqlParameter address = new SqlParameter("@address", contact.Address);

                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.Add(id);
                command.Parameters.Add(firstName);
                command.Parameters.Add(lastName);
                command.Parameters.Add(phone);
                command.Parameters.Add(address);

                command.ExecuteNonQuery();
            }catch (Exception)
            {
                throw;
            }finally { conn.Close(); }
        }

        //Eliminar Usuarios
        public void DeleteContact(int id)
        {
            try
            {
                conn.Open();
                string query = @"DELETE FROM Contacts WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.Add(new SqlParameter("@Id", id));
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally { conn.Close(); }

        }

        //Listar Usuarios
        public List<Contact> GetContacts(string search = null)
        {
            List<Contact> list = new List<Contact>();
            try
            {
                conn.Open();
                string query = @" SELECT Id, FirstName, LastName, Phone, Address
                                FROM Contacts";
                SqlCommand cmd = new SqlCommand();
                //cuando search no sea nulo o este vacio
                if (!string.IsNullOrEmpty(search))
                {
                    query += @" WHERE FirstName LIKE @Search OR LastName LIKE @Search OR Phone LIKE @Search OR Address LIKE @Search ";
                    cmd.Parameters.Add(new SqlParameter("@Search", $"%{search}%"));
                }
                cmd.CommandText = query;
                cmd.Connection = conn;
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Contact()
                    {
                        Id = int.Parse(reader["Id"].ToString()),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        Address = reader["address"].ToString(),
                    }); 
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }finally { conn.Close(); }  
            return list;
        }

       
    }
}
