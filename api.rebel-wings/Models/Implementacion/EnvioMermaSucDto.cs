
using System.ComponentModel;

namespace api.rebel_wings.Models.Implementacion;
/// <summary>
/// Modelo Transferencias
/// </summary>
public class EnvioMermaSucDto
{
    [DefaultValue(0)]
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public string Serie { get; set; } = null!;
    public int Numero { get; set; }
    public int Codarticulo { get; set; }
    public string Referencia { get; set; } = null!;
    public string Descripcion { get; set; } = null!;
    public double unidades { get; set; }
    public double precio { get; set; }
    public string Justificacion { get; set; } = null!;
    public string Comentarios { get; set; } = null!;
    public string Usuario { get; set; } = null!;
    public string Sucursal { get; set; } = null!;

}
