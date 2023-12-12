public class EditorGrafico
{
    static void Main(string[] args)
    {
        //pruebas de código
        var p1 = new Punto(1, 2);
        Console.WriteLine(p1.Dibujar());
        p1.Mover(2, 5);
        Console.WriteLine(p1.Dibujar());
        var r1 = new Rectangulo(3, 3, 2, 2);
        Console.WriteLine(r1.Dibujar());
        r1.Mover(23, 21);
        Console.WriteLine(r1.Dibujar());
        var c1 = new Circulo(12, 21, 4);
        Console.WriteLine(c1.Dibujar());
        c1.Mover(-2, 1);
        Console.WriteLine(c1.Dibujar());
        c1.Mover(-200, -10);
        Console.WriteLine(c1.Dibujar());
        //con un trycatch, ya que está hecho para fallar
        try
        {
            var p2 = new Punto(-2, 31);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al pintar el punto:Se sale de pantalla.");
        }

        var gC1 = new GraficoCompuesto(1, 2, 4, 21, 54, 21, 3, 6, 2);
        Console.WriteLine(gC1.Dibujar());
    }
}

//creamos una interfaz que incluya los métodos que después usaremos
public interface IGrafico
{
    public bool Mover(int x, int y);

    public string Dibujar();
}

//GraficoCompuesto en principio asume que será un punto, un rectángulo y un radio.
public class GraficoCompuesto : IGrafico
{
    public GraficoCompuesto(int xPunto, int yPunto, int xRect, int yRect, int ancho,
        int alto, int xCirc, int yCirc, int Radio)
    {
        //para que de un error en caso de que este se produca, lo mejor será aplicar tryCatch, ya que nos sacará
        //directamente de la construcción del objeto.
        try
        {
            var puntoCompuesto = new Punto(xPunto, yPunto);
            var rectCompuesto = new Rectangulo(xRect, yRect, ancho, alto);
            var circCompuesto = new Circulo(xCirc, yCirc, Radio);
            elements.Add(puntoCompuesto);
            elements.Add(rectCompuesto);
            elements.Add(circCompuesto);
            foreach (var el in elements)
            {
                el.Dibujar();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Error al crear Gráfico Compuesto");
        }
    }

    List<IGrafico> elements = new List<IGrafico>();
    //mover aplicará el Mover() de punto sobre cada elemento directamente pero con los mismos valores
    public bool Mover(int x, int y)
    {
        foreach (var el in elements)
        {
            bool mover;
            mover = el.Mover(x, y);
            if (!mover)
            {
                return false;
            }
        }

        Dibujar();
        return true;
    }
    //Dibujar dibujará simplemente cada elemento por separado.
    public string Dibujar()
    {
        var result = "Gráfico complejo compuesto de:\n";
        foreach (var el in elements)
        {
            result += el.Dibujar() + "\n";
        }

        return result;
    }
}

//La clase punto será la principal de este programa, de la que heredarán los demás
public class Punto : IGrafico
{
    //los he atribuido como protected por un tema de herencia, para que sean más accesibles pero sin hacerlos public
    protected int X;
    protected int Y;

    public Punto(int x, int y)
    {
        //esto soltará una excepción si cualquier gráfico sale del límite
        if (x > 600 || y > 800 || x < 0 || y < 0)
        {
            throw new Exception("El gráfico se sale de pantalla");
        }
        this.X = x;
        this.Y = y;
    }

    //un constructor vacío
    protected Punto()
    {
        X = 0;
        Y = 0;
    }

    //mover primero comprobará que el movimiento es legal y en caso de hacerlo lo realizará.
    //no envuelvo en else lo de abajo ya que al hacer un return abandonamos el método.
    public virtual bool Mover(int x, int y)
    {
        if ((this.X + x) > 600 || (this.X + x) < 0 || (this.Y + y) > 800 || ((this.X + x) < 0))
        {
            return false;
        }

        this.X += x;
        this.Y += y;
        return true;
    }

    //Dibujar simplemente mostará dónde se encuentra nuestro Punto.
    //Este método deberá ser sobrecargado más adelante para poder mostrar todos los atributos necesarios.
    public virtual string Dibujar()
    {
        return "Punto en (" + X + "," + Y + ")";
    }
}

//constructor principal
public class Rectangulo(int x, int y, int ancho, int alto)
    : Punto(x, y)
{
    private int ancho = ancho;

    private int alto = alto;

//constructor vacío
    public Rectangulo() : this(0, 0, 1, 1){}

//sobrecarga de Dibujar
    public override string Dibujar()
    {
        return "Rectángulo de (" + X + "," + Y + ") hasta " + "(" + (X + alto) + "," + (Y + ancho) + ")";
    }
}

//clase círculo, solo con radio como atributo y la herencia
public class Circulo : Punto
{
    //todo controlar también círculo y rectángulo con la excepción al igual que Punto
    private int radio;

//constructor base
    public Circulo(int x, int y, int radio) : base(x, y)
    {
        this.radio = radio;
    }

//y constructor vacío
    public Circulo()
    {
        radio = 1;
    }

    //sobrecarga de dibujar.
    public override string Dibujar()
    {
        return "Círculo de radio " + radio + " con centro en (" + X + "," + Y + ")";
    }
}

/*
Console.WriteLine("Programa de creación de gráfico compuesto");
Console.WriteLine("Introduzca X de su punto");
int xPunto = Convert.ToInt32(Console.ReadLine());
Console.WriteLine("Introduzca Y de su punto");
int yPunto = Convert.ToInt32(Console.ReadLine());
var puntoCompuesto = new Punto(xPunto, yPunto);
elements.Add(puntoCompuesto);
Console.WriteLine("Introduzca X del punto inicial de su rectángulo");
int xRect = Convert.ToInt32(Console.ReadLine());
Console.WriteLine("Introduzca Y del punto inicial de su rectángulo");
int yRect = Convert.ToInt32(Console.ReadLine());
Console.WriteLine("Introduzca ancho de su rectángulo");
int AnchoRect = Convert.ToInt32(Console.ReadLine());
Console.WriteLine("Introduzca alto de su rectángulo");
int AltoRect = Convert.ToInt32(Console.ReadLine());
var rectCompuesto = new Rectangulo(xRect, yRect, AnchoRect, AltoRect);
elements.Add(rectCompuesto);
Console.WriteLine("Introduzca X del punto inicial de su circunferencia");
int xCirc = Convert.ToInt32(Console.ReadLine());
Console.WriteLine("Introduzca Y del punto inicial de su circunferencia");
int yCirc = Convert.ToInt32(Console.ReadLine());
Console.WriteLine("Introduzca radio de su circunferencia");
var radioCirc = Convert.ToInt32(Console.ReadLine());
var circCompuesto = new Circulo(xCirc, yCirc, radioCirc);
elements.Add(circCompuesto);
Dibujar();
*/