using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_final
{
    internal class Reserva
    {
    }
}
public class Reserva
{
    public int ReservaID { get; set; }
    public int UsuarioID { get; set; }
    public string LibroISBN { get; set; }
    public DateTime FechaReserva { get; set; }
    public DateTime FechaRetorno { get; set; }
}
