

public class EditorGrafico
{
    static void Main(string[] args)
    {
        //pruebas de código
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
        //con un trycatch, ya que está hecho para fallar
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
//creamos una interfaz que incluya los métodos que después usaremos
public interface IGrafico
{
    public bool Mover(int x, int y);

    public void Dibujar();
}
//el gráficoCompuesto al final lo he dejado como una lista de gráficos que luego se procesarían sumándose, pero
//aún no estoy seguro de cómo hacerlo, por lo que lo he dejado en este estado.
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
//La clase punto será la principal de este programa, de la que heredarán los demás
public class Punto : IGrafico
{
    //los he atribuido como protected por un tema de herencia, para que sean más accesibles pero sin hacerlos public
    protected int x;
    protected int y;

    public Punto(int x, int y)
    {
        //esto soltará una excepción si cualquier gráfico sale del límite
        if (x>600||y>800||x<0||y<0)
        {
            throw new Exception("El gráfico se sale de pantalla");
        }
        this.x = x;
        this.y = y;
    }
    //un constructor vacío
    public Punto()
    {
        x = 0;
        y = 0;
    }
    //mover primero comprobará que el movimiento es legal y en caso de hacerlo lo realizará.
    //no envuelvo en else lo de abajo ya que al hacer un return abandonamos el método.
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

    //Dibujar simplemente mostará dónde se encuentra nuestro Punto.
    //Este método deberá ser sobrecargado más adelante para poder mostrar todos los atributos necesarios.
    public void Dibujar()
    {
        Console.WriteLine("Punto en (" + x + "," + y + ")");
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
    public void Dibujar()
    {
        Console.WriteLine("Rectángulo de (" + x + ","+ y + ") hasta " +  "(" + x+alto + ","+ y+ancho + ")" );
    }
}
//clase círculo, solo con radio como atributo y la herencia
public class Circulo : Punto
{
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
    public void Dibujar()
    {
        Console.WriteLine("Círculo de radio " + radio + " con centro en (" + x + "," + y + ")");
    }
    
    
}