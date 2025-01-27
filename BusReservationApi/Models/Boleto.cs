namespace BusReservationApi.Models;

public class Boleto
{
    public int Id { get; set; } // Clave primaria
    public int ReservaId { get; set; } // Debe ser único
    public string NombrePasajero { get; set; }
    public string Asiento { get; set; }
    public DateTime FechaViaje { get; set; }
    public float Precio { get; set; }
    public string EstadoReserva { get; set; }
}