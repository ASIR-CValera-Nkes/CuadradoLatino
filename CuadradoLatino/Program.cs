using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CuadradoLatino
{
    enum AppSection { LatinSquare, UniversityExercice }
    class Program
    {
        private static int tamano = 25, phase = -1;
        private static StringBuilder sb = new StringBuilder();
        private static bool skip = false;
        private static AppSection sec;
        static void Main(string[] args)
        {
            if (!skip)
            {
                string r = "";
                if (phase != 0)
                {
                    Console.Write("Introduzca el tamaño del cuadrado latino: ");
                    r = Console.ReadLine();
                }
                if (phase == 0 || !string.IsNullOrWhiteSpace(r) && int.TryParse(r, out tamano) && tamano > 0)
                {
                    Console.WriteLine();
                    Console.WriteLine("Seleccione la sección [1-2]:");
                    Console.WriteLine();
                    Console.WriteLine("1.- Cuadrado latino");
                    Console.WriteLine("2.- Ejercicio de la universidad");
                    Console.WriteLine();
                    string s = Console.ReadLine();
                    int isec = -1;
                    bool valid = true;
                    if (!int.TryParse(s, out isec)) valid = false;
                    else
                        if (!(Enumerable.Range(1, Enum.GetValues(typeof(AppSection)).Length).Contains(isec) && Enum.TryParse(s, out sec))) valid = false;
                    if (!string.IsNullOrWhiteSpace(s) && valid)
                    {
                        Console.WriteLine();
                        switch (sec - 1)
                        {
                            case AppSection.LatinSquare:
                                for (int x = 1; x <= tamano; ++x)
                                {
                                    for (int y = 1; y <= tamano; ++y)
                                    {
                                        int cur = y + x; //Hacemos que se vaya sumando uno...
                                        cur = cur > tamano ? cur - tamano : cur; //Jugamos con el desbordamiento, si el numero es mayor al tamaño, volvemos a empezar desde 1
                                        sb.Append((tamano >= 10 && cur.ToString().Length < 2 ? "0" : "") + cur + " "); //Guardamos el resultado, cuadrando el tamaño en todo caso...
                                    }
                                    sb.Append(Environment.NewLine);  
                                }
                                break;
                            case AppSection.UniversityExercice:
                                for (int x = 1; x <= tamano; ++x)
                                {
                                    for (int y = 1; y <= tamano; ++y)
                                    {
                                        int cur = y + 1; //Hacemos que se vaya sumando uno...
                                        cur -= x;
                                        cur = cur < 1 ? tamano - Math.Abs(cur) : cur; //Jugamos con el desbordamiento, si el numero es mayor al tamaño, volvemos a empezar desde 1
                                        sb.Append((tamano >= 10 && cur.ToString().Length < 2 ? "0" : "") + cur + " "); //Guardamos el resultado, cuadrando el tamaño en todo caso...
                                    }
                                    sb.Append(Environment.NewLine);
                                }
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Por favor, introduce un número de opción válido.");
                        Console.WriteLine();
                        phase = 0;
                        Main(null);
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("Por favor, introduce un número entero positivo y mayor que 0.");
                    Console.WriteLine();
                    Main(null);
                    return;
                }
                Console.Write(sb.ToString());
            }
            Console.WriteLine();
            Console.Write("¿Desea mostrar el resultado en un archivo de texto? [y/n]: ");
            string re = Console.ReadLine().ToUpper();
            if (!string.IsNullOrWhiteSpace(re) && re.Length == 1 && (re == "Y" || re == "N"))
            {
                switch (re)
                {
                    case "Y":
                        string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "cuadradolatino.txt");
                        File.WriteAllText(path, sb.ToString());
                        Process.Start(path);
                        break;
                    case "N":
                        break;
                }
            }
            else
            {
                Console.WriteLine("Por favor, introduce y/n.");
                skip = true;
                Main(null);
                return;
            }
            Console.WriteLine("Pulse cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}