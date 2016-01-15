using System;
using System.Collections.Generic;
using System.Linq;

namespace MTP.Dinamica {
    /* 
     * Un excéntrico nutricionista va a un restaurante. En la carta aparecen todos los platos disponibles
     * con el número de calorías. El nutricionista conoce el número mínimo de calorías que su cuerpo
     * necesita en esa comida. Su objetivo es encontrar el menú que cubra exactamente esa cantidad de
     * calorías o las supere de forma mínima. Además, no quiere repetir platos.
     * 
     * Diseña un algoritmo dinámico que determine qué platos forman parte del menú óptimo y el número de
     * calorías del menú óptimo. Detalla lo siguiente:
     * 
     * 1. Las estructuras y/o variables necesarias para representar la información del problema (0,5 puntos).
     * 2. La estructura de cálculos intermedios utilizada (1 punto).
     * 3. La relación recursiva entre subproblemas (1 punto).
     * 4. El procedimiento o función que implementa el algoritmo (2,5 puntos).
     */
    public static class Ejercicio207 {
        const int I = int.MaxValue; // Infinito
        
        static int Min(int a, int b) {
            return (b < a) ? b : a;
        }

        static int[,] Calcular(int[] lista, int N, int C) {
            int MaxElem = N + 1;
            int MaxCant = C + 1;
            int[,] tabla = new int[MaxElem, MaxCant];

            for(int i = 0; i < MaxElem; i++) {
                tabla[i, 0] = 0;
            }
            for(int i = 1; i < MaxCant; i++) {
                tabla[0, i] = I;
            }

            int a, b;
            for(int i = 1; i < MaxElem; i++) {
                for(int j = 1; j < MaxCant; j++) {
                    a = tabla[i - 1, j];
                    if(lista[i - 1] <= j) {
                        b = tabla[i - 1, j - lista[i - 1]];
                        if(b < I) b += lista[i - 1];
                    } else {
                        b = lista[i - 1];
                    }
                    if(b >= j) {
                        tabla[i, j] = Min(a, b);
                    } else {
                        tabla[i, j] = I;
                    }
                }
            }
            //Mostrar(tabla, MaxElem, MaxCant);
            return tabla;
        }

        private static int[] Resultado(int[,] t, int[] l, int N, int C) {
            List<int> r = new List<int>();
            int i = N;
            int j = C;

            while(i > 0 && j > 0) {
                if(t[i, j] == t[i - 1, j]) {
                    i--;
                } else {
                    r.Add(i - 1);
                    j -= l[i - 1];
                    i--;
                }
            }
            return r.ToArray();
        }

        static void Resolver(int[] lista, int C) {
            int[,] tabla = Calcular(lista, lista.Length, C);
            int[] r = Resultado(tabla, lista, lista.Length, C);

            Console.WriteLine("Lista: " +
                              lista.Select(x => x.ToString())
                                   .Aggregate((x, xs) => x + " " + xs));
            Console.WriteLine("Total a consumir: {0} calorías", C);
            Console.WriteLine("Total consumido: {0} calorías",
                              r.Select(x => lista[x]).Sum());
            foreach(int n in r) {
                Console.WriteLine("Plato {0} = {1} calorías", n + 1,
                                  lista[n]);
            }
            Console.WriteLine();
        }

        public static void Resolver() {
            int[] lista = { 7, 8, 9, 4, 5, 6 };
            Resolver(lista, 10);
            Resolver(lista, 37);

            //int[] listab = { 7, 4, 9, 5, 8, 6 };
            //Resolver(listab, 10);
            //Resolver(listab, 37);

            int[] lista2 = { 11, 9, 5, 3 };
            Resolver(lista2, 18);
            Resolver(lista2, 15);

            int[] lista3 = { 8, 16, 24, 32 };
            Resolver(lista3, 37);
            Resolver(lista3, 42);
        }

        static void Mostrar(int[,] tabla, int MaxElem, int MaxCant) {
            Console.WriteLine("Tabla:");
            for(int i = 0; i < MaxElem; i++) {
                for(int j = 0; j < MaxCant; j++) {
                    if(tabla[i, j] == I) {
                        Console.Write("IN ");
                    } else {
                        Console.Write(tabla[i, j].ToString("00") + " ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
