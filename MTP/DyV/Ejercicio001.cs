using System;
using System.Collections.Generic;
using System.Linq;

namespace MTP.DyV {
    /*
     * Ejercicio 1: Dado un vector V[1..n] de n elementos que se puedan comparar entre sí, diseña
     * un algoritmo que encuentre el máximo y el mínimo utilizando la técnica divide y vencerás,
     * y calcula su complejidad.
     */
    public static class Ejercicio001 {
        public static void BuscaMinMax(int[] lista, out int min, out int max) {
            min = max = lista[0];
            foreach(int num in lista) {
                if(num < min) min = num;
                if(num > max) max = num;
            }
        }

        public static void BuscaMinMax(int[] lista, int inf, int sup, out int min, out int max) {
            if((inf + 1) < sup) {
                int min1, max1, min2, max2;
                int mitad = (inf + sup) / 2;
                BuscaMinMax(lista, inf, mitad, out min1, out max1);
                BuscaMinMax(lista, mitad + 1, sup, out min2, out max2);
                min = (min1 > min2) ? min2 : min1;
                max = (max2 > max1) ? max2 : max1;
            } else {
                min = (lista[inf] > lista[sup]) ? lista[sup] : lista[inf];
                max = (lista[sup] > lista[inf]) ? lista[sup] : lista[inf];
            }
        }

        public static void Resolver() {
            int[] lista = { 9, 4, 8, 2, 7, 1, 5, 3, 6 };
            int min, max;

            BuscaMinMax(lista, 0, lista.Length - 1, out min, out max);

            Console.WriteLine("Lista: " + lista.Select(x => x.ToString())
                                               .Aggregate((x, xs) => x + " " + xs));
            Console.WriteLine("Min: " + min.ToString());
            Console.WriteLine("Max: " + max.ToString());

            BuscaMinMax(lista, out min, out max);
            Console.WriteLine();
            Console.WriteLine("Min: " + min.ToString());
            Console.WriteLine("Max: " + max.ToString());
        }
    }
}
