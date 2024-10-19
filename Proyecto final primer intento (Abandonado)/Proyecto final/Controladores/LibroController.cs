using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_final.Controladores
{
    internal class LibroController
    {
        public class LibroControler
        {
            private ConexionDB conexion = new ConexionDB();

            public void AgregarLibro(Libro libro)
            {
                using (SqlConnection conn = conexion.GetConnection())
                {
                    conn.Open();
                    string query = "INSERT INTO Libros (ISBN, Titulo, Autor, Editorial, AnioPublicacion, Genero, NumeroCopias) VALUES (@ISBN, @Titulo, @Autor, @Editorial, @AnioPublicacion, @Genero, @NumeroCopias)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ISBN", libro.ISBN);
                        cmd.Parameters.AddWithValue("@Titulo", libro.Titulo);
                        cmd.Parameters.AddWithValue("@Autor", libro.Autor);
                        cmd.Parameters.AddWithValue("Editorial", libro.Editorial);
                        cmd.Parameters.AddWithValue("AnioPublicacion", libro.AnioPublicacion);
                        cmd.Parameters.AddWithValue("Genero", libro.Genero);
                        cmd.Parameters.AddWithValue("NumeroCopias", libro.NumeroCopias);
                        // Agrega los demás parámetros...
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            // Implementa editar, eliminar, buscar...
        }

    }
}
