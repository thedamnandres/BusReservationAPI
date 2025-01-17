using System.ComponentModel.DataAnnotations;

namespace BusReservationApi.Models;

public class Ruta
{
    [Key]
    public int IdRuta { get; set; }

    [Required]
    [StringLength(100)]
    public string Origen { get; set; }

    [Required]
    [StringLength(100)]
    public string Destino { get; set; }

    [Required]
    [DataType(DataType.Time)]
    public TimeSpan Duracion { get; set; }
        
    [Required(ErrorMessage = "La fecha es obligatorio")]
    [DataType(DataType.Date)]
    public DateTime FechaSalida { get; set; }

    [Required]
    [DataType(DataType.Time)]
    public TimeSpan Hora { get; set; }
}