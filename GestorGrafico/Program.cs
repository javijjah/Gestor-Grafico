

public class EditorGrafico
{
    static void Main(string[] args)
    {
        var p1 = new Punto(1, 2);
        p1.Dibujar();
        p1.Mover(2, 5);
        p1.Dibujar();
        var r1 = new Rectangulo(3, 3, 2, 2);
        r1.Dibujar();
        r1.Mover(23,21);
        r1.Dibujar();
        var c1 = new Circulo(12, 21, 4);
        c1.Dibujar();
        c1.Mover(-2, 1);
        c1.Dibujar();
        c1.Mover(-200, -10);
        c1.Dibujar();
        try
        {
            var p2 = new Punto(-2, 31);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
    }
public interface IGrafico
{
    public bool Mover(int x, int y);

    public void Dibujar();
}

public class GraficoCompuesto : IGrafico
{
    List<IGrafico> elements;

    public bool Mover(int x, int y)
    {
        return false;
    }

    public void Dibujar()
    {
    }
}

public class Punto : IGrafico
{
    protected int x;
    protected int y;

    public Punto(int x, int y)
    {
        if (x>600||y>800||x<0||y<0)
        {
            throw new Exception("El gráfico se sale de pantalla");
        }
        this.x = x;
        this.y = y;
    }

    public Punto()
    {
        x = 0;
        y = 0;
    }

    public bool Mover(int x, int y)
    {
            if ((this.x+x)>600||(this.x+x)<0 ||(this.y+y)>800||((this.x+x)<0))
            {
                return false;
            }
            this.x += x;
            this.y += y;
            return true;
    }


    public void Dibujar()
    {
        Console.WriteLine("Punto en (" + x + "," + y + ")");
    }
}

public class Rectangulo(int x, int y, int ancho, int alto)
    : Punto(x, y)
{
    private int ancho = ancho;
    private int alto = alto;

    public Rectangulo() : this(0, 0, 1, 1){}
    

    public void Dibujar()
    {
        Console.WriteLine("Rectángulo de (" + x + ","+ y + ") hasta " +  "(" + x+alto + ","+ y+ancho + ")" );
    }
}

public class Circulo : Punto
{
    private int radio;

    public Circulo(int x, int y, int radio) : base(x, y)
    {
        this.radio = radio;
    }

    public Circulo(int radio)
    {
        this.radio = radio;
    }

   
    public void Dibujar()
    {
        Console.WriteLine("Círculo de radio " + radio + " con centro en (" + x + "," + y + ")");
    }
    
    
}