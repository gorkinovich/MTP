using System;
using System.Collections.Generic;
using System.Linq;

namespace MTP.DyV {
    /*
     * Se trata de decidir si existe un elemento dado X en un vector de enteros. Plantear un algoritmo
     * con la siguiente estrategia: En primer lugar comparar el elemento dado X con el elemento que se
     * encuentra en la posición n/3 del vector. Si este es menor que el elemento X, entonces lo compara
     * con el elemento que se encuentra en la posición 2n/3, y si no coincide con X, busca recursivamente
     * en el correspondiente subvector de tamaño 1/3 del original.
     */
    public static class Ejercicio010 {
        public static int Busqueda(int x, int[] lista, int pri, int ult) {
            if(pri <= ult) {
                int tam = 1 + ult - pri;
                int terInf = pri + tam / 3;
                int terSup = pri + (tam * 2) / 3;

                if(lista[terInf] == x) {
                    return terInf;
                } else if(lista[terInf] > x) {
                    return Busqueda(x, lista, pri, terInf - 1);
                } else {
                    if(lista[terSup] == x) {
                        return terSup;
                    } else if(lista[terSup] < x) {
                        return Busqueda(x, lista, terSup + 1, ult);
                    } else {
                        return Busqueda(x, lista, terInf + 1, terSup - 1);
                    }
                }
            } else {
                return -1;
            }
        }

        public static int Busqueda(int x, int[] lista) {
            return Busqueda(x, lista, 0, lista.Length - 1);
        }

        public static void Resolver() {
            int[] lista = { 1, 3, 4, 6, 8, 9, 12, 14, 15, 16, 18, 20 };
            Console.WriteLine("Lista: " + lista.Select(x => x.ToString())
                                               .Aggregate((x, xs) => x + " " + xs));
            Array.ForEach(Enumerable.Range(0, 22)
                                    .Select(i => i.ToString() + "\t" + Busqueda(i, lista).ToString())
                                    .ToArray(), Console.WriteLine);
        }
    }
}
