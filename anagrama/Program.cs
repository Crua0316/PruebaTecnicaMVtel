using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Verificar Anagramas");
        Console.WriteLine("Escribe la primera palabra:");
        string cadena1 = Console.ReadLine();

        Console.WriteLine("Escribe la segunda palabra:");
        string cadena2 = Console.ReadLine();

        bool resultado = AnagramChecker.SonAnagramas(cadena1, cadena2);

        if (resultado)
            Console.WriteLine($"{cadena1} y {cadena2} son anagramas.");
        else
            Console.WriteLine($"{cadena1} y {cadena2} no son anagramas.");
    }
}
