namespace api.rebel_wings.Models.Mermas;

public class MermasDto
{
    public string Description { get; set; }
    public double Unity { get; set; }
    public double Price { get; set; }
    public string UnitMeasure { get; set; }
}
public class ReporteDto
{
    public string cod { get; set; }
    public string Sucursal { get; set; }
    public string Articulo { get; set; }
    public string InvAyer { get; set; }
    public double TraspasoAyer { get; set; }
    public double ConsumoAyer { get; set; }
    public string InvHoy { get; set; }
    public double InvFormula { get; set; }
    public double Diferencia { get; set; }
    public DateTime Captura { get; set; }
    public string Seccion { get; set; }

}

public class AppsDto
{
    public string Reg { get; set; }
    public string Cod { get; set; }
    public string Sucursal { get; set; }
    public int Codcliente { get; set; }
    public string App { get; set; }
    public double Total { get; set; }
    public string Mes { get; set; }
    public string Marca { get; set; }

}

public class VendedorDto
{
    public string Sucursal { get; set; }
    public string Vendedores { get; set; }
    public string Articulo { get; set; }
    public string Departamento { get; set; }
    public double Uds { get; set; }
    public double Precio { get; set; }
    public string Cerveza { get; set; }
    public short? CodDpto { get; set; }
    public string Filtro { get; set; }
}
public class FiltroDto
{
    public string Name { get; set; }
    public bool Checked { get; set; }

}

