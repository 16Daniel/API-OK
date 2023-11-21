namespace api.rebel_wings.Models.Implementacion;

public class _25ptsDto
{
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
public class sucAuditaDto
{
    public int incidencias { get; set; }

}

