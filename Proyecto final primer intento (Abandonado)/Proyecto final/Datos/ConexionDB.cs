using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_final.Datos
{
    internal class ConexionDB
    {
    }
}
public class ConexionDB
{
    private string connectionString = "Data Source=BRYAN-PC\\SQLEXPRESS;Initial Catalog=Biblioteca;Integrated Security=True";


    public SqlConnection GetConnection()
    {
        return new SqlConnection(connectionString);
    }
}

