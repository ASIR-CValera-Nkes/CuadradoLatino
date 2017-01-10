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
    class Program
    {
        private static int tamano = 25;
        private static StringBuilder sb = new StringBuilder();
        private static bool skip = false;
        static void Main(string[] args)
        {
            if (!skip)
            {
                Console.Write("Introduzca el tamaño del cuadrado latino: ");
                string r = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(r) && int.TryParse(r, out tamano))
                {
                    Console.WriteLine();
                    for (int x = 0; x < tamano; ++x)
                    {
                        for (int y = 0; y < tamano; ++y)
                        {
                            int cur = y + x + 1; //Hacemos que se vaya sumando uno...
                            cur = cur > tamano ? cur - tamano : cur; //Jugamos con el desbordamiento, si el numero es mayor al tamaño, volvemos a empezar desde 1
                            sb.Append((tamano >= 10 && cur.ToString().Length < 2 ? "0" : "") + cur + " "); //Guardamos el resultado, cuadrando el tamaño en todo caso...
                        }
                        sb.Append(Environment.NewLine);
                    }
                }
                else
                {
                    Console.WriteLine("Por favor, introduce un número entero.");
                    Main(null);
                }
                Console.Write(sb.ToString());
            }
            Console.WriteLine();
            Console.Write("¿Desea mostrar el resultado en un archivo de texto? [y/n]: ");
            string re = Console.ReadLine().ToUpper();
            if (!string.IsNullOrWhiteSpace(re) && re.Length == 1 && (re == "Y" || re == "N"))
            {
                switch(re)
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
            }
            Console.WriteLine("Pulse cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}
