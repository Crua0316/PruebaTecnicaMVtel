using System;
using System.Collections.Generic;

public static class AnagramChecker
{
    public static bool SonAnagramas(string cadena1, string cadena2)
    {
        // Eliminar espacios y convertir a minúsculas
        cadena1 = cadena1.Replace(" ", "").ToLower();
        cadena2 = cadena2.Replace(" ", "").ToLower();

        // Si las longitudes son diferentes, no son anagramas
        if (cadena1.Length != cadena2.Length)
            return false;

        // Contar letras en la primera cadena
        Dictionary<char, int> conteo = new Dictionary<char, int>();
        foreach (char c in cadena1)
        {
            if (conteo.ContainsKey(c))
                conteo[c]++;
            else
                conteo[c] = 1;
        }

        // Verificar las letras de la segunda cadena
        foreach (char c in cadena2)
        {
            if (!conteo.ContainsKey(c) || conteo[c] == 0)
                return false;
            conteo[c]--;
        }

        return true;
    }
}
