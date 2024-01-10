namespace biz.bd2.Models;

public class Mermas
{
    public string Description { get; set; }
    public double Unity { get; set; }
    public double Price { get; set; }
    public string UnitMeasure { get; set; }
    public DateTime Fecha { get; set; }
}
public class Reporte
{
    public string cod { get; set; }
    public string Region { get; set; }
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

public class Apps
{
    public string Reg { get; set; }
    public string Cod { get; set; }
    public string Sucursal { get; set; }
    public int Codcliente { get; set; }
    public string App{ get; set; }
    public double Total { get; set; }
    public string Mes { get; set; }
    public string Marca { get; set; }
    public string Nom { get; set; }
}

public class SucursalesFront
{
    public string Idfront { get; set; }
    public string Titulo { get; set; }
}

public class Ranking
{
    public int Cod { get; set; }
    public string Descripcion { get; set; }
    public string Seccion { get; set; }
    public double Unidades { get; set; }
    public double Importe { get; set; }
    public double Porcentaje { get; set; }

}

public class Checadas { 
    public DateTime Fecha { get; set; }
    public string Reloj { get; set; }
    public string Empleado { get; set; }
}

public class Vendedor
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
public class Filtro
{
    public string Name { get; set; }
    public bool Checked { get; set; }

}