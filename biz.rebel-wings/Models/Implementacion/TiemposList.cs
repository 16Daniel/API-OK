namespace biz.rebel_wings.Models.Implementacion;



public class TiemposList
{
    public int Id { get; set; }
    public int IdComanda { get; set; }
    public int CodArticulo { get; set; }
    public int Orden { get; set; }
    public int Posicion { get; set; }
    public string Terminal { get; set; }
    public DateTime Hora { get; set; }
    public string Descripcion { get; set; }
    public double Unidades { get; set; }
    public double Minutos { get; set; }
    public string EnTiempo { get; set; }
    public string Sucursal { get; set; }

}
//public class BranchChartBarVerticalDto
//{
//    public string Name { get; set; }
//    public ICollection<SerieDto> Series { get; set; }
//}
//public class SerieDto
//{
//    public string Name { get; set; }
//    public decimal Value { get; set; }
//}
//public class TiemposGrafica 
//{
//    public virtual ICollection<BranchChartBarVerticalDto> Multi { get; set; }
//}

public class RangosList
{
    public string nomRango { get; set; }
    public int RangoValor { get; set; }
    
}

