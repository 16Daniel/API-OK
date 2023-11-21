
using System.ComponentModel;

namespace api.rebel_wings.Models.Implementacion;
/// <summary>
/// Modelo Transferencias
/// </summary>
public class Envio25ptsDto
{
    [DefaultValue(0)]
    public int Id { get; set; }
    public DateTime FechaIni { get; set; }
    public int Sala { get; set; }
    public int Mesa { get; set; }
    public int TotalAyc { get; set; }
    public int Cobros { get; set; }
    public int CobrosMinimos { get; set; }
    public int Diferencia { get; set; }
    public string Justificacion { get; set; } = null!;
    public string Usuario { get; set; } = null!;
    public string Sucursal { get; set; } = null!;
}