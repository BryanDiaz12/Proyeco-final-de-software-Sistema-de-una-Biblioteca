using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Proyecto_final.Controladores;

namespace Proyecto_final
{
    public partial class Form1 : Form
    {
        private string cadenaConexion = @"Server=BRYAN-PC\SQLEXPRESS;Database=Biblioteca;Trusted_Connection=True;";

        private List<Libro> listaLibros = new List<Libro>();
        public Form1()
        {
            InitializeComponent(); // Este es el método que se llama al iniciar el formulario
            ConfigurarDataGridView();
            CargarLibros();
        }
        private void ConfigurarDataGridView()
        {
            // Limpia las columnas previas si ya las había
            dataGridView1.Columns.Clear();

            // Añadir las columnas
            dataGridView1.Columns.Add("ISBN", "ISBN");
            dataGridView1.Columns.Add("Titulo", "Título");
            dataGridView1.Columns.Add("Autor", "Autor");
            dataGridView1.Columns.Add("Editorial", "Editorial");
            dataGridView1.Columns.Add("AnioPublicacion", "Año de Publicación");
            dataGridView1.Columns.Add("Genero", "Género");
            dataGridView1.Columns.Add("NumeroCopias", "Número de Copias");
        }
        private void CargarLibros()
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                try
                {
                    conexion.Open();

                    // Consulta para obtener los datos de la tabla 'Libros'
                    string consulta = "SELECT * FROM Libros";
                   

                    using (SqlCommand comando = new SqlCommand(consulta, conexion))
                    {
                        using (SqlDataReader reader = comando.ExecuteReader())
                        {
                            // Verifica si hay filas
                            if (reader.HasRows)
                            {
                                // Crear un DataTable y cargar los datos
                                DataTable dt = new DataTable();
                                dt.Load(reader);

                                // Asignar el DataTable como fuente de datos del DataGridView
                                dataGridView1.DataSource = dt;

                                // Refrescar para asegurarte de que los datos aparezcan
                                dataGridView1.Refresh();
                            }
                            else
                            {
                                MessageBox.Show("No se encontraron registros.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar los libros: " + ex.Message);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Verificar que los campos no estén vacíos
            if (string.IsNullOrWhiteSpace(txtISBN.Text) || string.IsNullOrWhiteSpace(txtTitulo.Text) || string.IsNullOrWhiteSpace(txtAutor.Text) ||
                string.IsNullOrWhiteSpace(txtEditorial.Text) || string.IsNullOrWhiteSpace(txtAnioPublicacion.Text) || string.IsNullOrWhiteSpace(txtGenero.Text) ||
                string.IsNullOrWhiteSpace(txtNumeroCopias.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos antes de agregar el libro.");
                return;
            }

            // Establecer la conexión con SQL Server
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                try
                {
                    conexion.Open();

                    // Crear el comando SQL para insertar el libro
                    string consulta = "INSERT INTO Libros (ISBN, Titulo, Autor, Editorial, AnioPublicacion, Genero, NumeroCopias) " +
                                      "VALUES (@ISBN, @Titulo, @Autor, @Editorial, @AnioPublicacion, @Genero, @NumeroCopias)";

                    using (SqlCommand comando = new SqlCommand(consulta, conexion))
                    {
                        // Añadir los parámetros al comando para evitar inyecciones SQL
                        comando.Parameters.AddWithValue("@ISBN", txtISBN.Text);
                        comando.Parameters.AddWithValue("@Titulo", txtTitulo.Text);
                        comando.Parameters.AddWithValue("@Autor", txtAutor.Text);
                        comando.Parameters.AddWithValue("@Editorial", txtEditorial.Text);
                        comando.Parameters.AddWithValue("@AnioPublicacion", int.Parse(txtAnioPublicacion.Text));
                        comando.Parameters.AddWithValue("@Genero", txtGenero.Text);
                        comando.Parameters.AddWithValue("@NumeroCopias", int.Parse(txtNumeroCopias.Text));

                        // Ejecutar el comando
                        comando.ExecuteNonQuery();
                    }

                    MessageBox.Show("Libro agregado exitosamente.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al agregar el libro: " + ex.Message);
                }
                finally
                {
                    conexion.Close();
                }
            }

            // Limpiar los cuadros de texto
            txtISBN.Clear();
            txtTitulo.Clear();
            txtAutor.Clear();
            txtEditorial.Clear();
            txtAnioPublicacion.Clear();
            txtGenero.Clear();
            txtNumeroCopias.Clear();

            // Actualizar la tabla (DataGridView)
            CargarLibros();
        }



        private void txtAutor_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)  // Verificar que haya una fila seleccionada
            {
                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    try
                    {
                        conexion.Open();

                        // Consulta SQL para actualizar el libro seleccionado
                        string consulta = "UPDATE Libros SET Titulo = @Titulo, Autor = @Autor, Editorial = @Editorial, " +
                                          "AnioPublicacion = @AnioPublicacion, Genero = @Genero, NumeroCopias = @NumeroCopias " +
                                          "WHERE ISBN = @ISBN";

                        using (SqlCommand comando = new SqlCommand(consulta, conexion))
                        {
                            // Asignar valores a los parámetros
                            comando.Parameters.AddWithValue("@ISBN", txtISBN.Text);
                            comando.Parameters.AddWithValue("@Titulo", txtTitulo.Text);
                            comando.Parameters.AddWithValue("@Autor", txtAutor.Text);
                            comando.Parameters.AddWithValue("@Editorial", txtEditorial.Text);
                            comando.Parameters.AddWithValue("@AnioPublicacion", txtAnioPublicacion.Text);
                            comando.Parameters.AddWithValue("@Genero", txtGenero.Text);
                            comando.Parameters.AddWithValue("@NumeroCopias", txtNumeroCopias.Text);

                            comando.ExecuteNonQuery();  // Ejecutar la actualización
                        }

                        // Recargar el DataGridView con los libros actualizados
                        CargarLibros();
                        MessageBox.Show("Libro modificado exitosamente.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al modificar el libro: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecciona un libro para modificar.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)  // Verificar que haya una fila seleccionada
            {
                DialogResult result = MessageBox.Show("¿Estás seguro de que deseas eliminar este libro?", "Confirmación", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                    {
                        try
                        {
                            conexion.Open();

                            // Consulta SQL para borrar el libro con el ISBN seleccionado
                            string consulta = "DELETE FROM Libros WHERE ISBN = @ISBN";

                            using (SqlCommand comando = new SqlCommand(consulta, conexion))
                            {
                                // Obtener el ISBN de la fila seleccionada en el DataGridView
                                string isbnSeleccionado = dataGridView1.SelectedRows[0].Cells["ISBN"].Value.ToString();
                                comando.Parameters.AddWithValue("@ISBN", isbnSeleccionado);

                                comando.ExecuteNonQuery();  // Ejecutar el borrado
                            }

                            // Recargar el DataGridView con los libros actualizados
                            CargarLibros();
                            MessageBox.Show("Libro borrado exitosamente.");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error al borrar el libro: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecciona un libro para borrar.");
            }
        }
    public class LibroControler
    {
        private ConexionDB conexion = new ConexionDB();


    }
}
