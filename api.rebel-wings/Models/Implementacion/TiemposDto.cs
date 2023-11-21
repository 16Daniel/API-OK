namespace api.rebel_wings.Models.Implementacion;

    public class TiemposDto
    {
    public int Id { get; set; }
    public int IdComanda { get; set; }
    public int CodArticulo { get; set; }
    public int Orden { get; set; }
    public int Posicion { get; set; }
    public string Terminal { get; set; } = null!;
    public DateTime Hora { get; set; }
    public string Descripcion { get; set; } = null!;
    public double Unidades { get; set; }
    public double Minutos { get; set; }
    public string EnTiempo { get; set; } = null!;
    public string Sucursal { get; set; } = null!;
    }
public class RangosDto
{
    public string nomRango { get; set; }
    public int RangoValor { get; set; }

}

