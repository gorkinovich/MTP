using System;
using System.Collections.Generic;
using System.Linq;

namespace MTP.Dinamica {
    /* 
     * Ejercicio 4
     * Tenemos n varillas de longitudes l1, ..., ln (números naturales) y precios c1, ..., cn
     * (números reales) que no se pueden cortar. Se desea soldar alguna de ellas para obtener
     * una varilla de longitud total L (número entero). Se pide escribir algoritmos de
     * programación dinámica y calcular sus complejidades, para:
     * + Obtener la varilla deseada minimizando el número de varillas utilizadas.
     * + Obtener la varilla deseada minimizando el precio total de las varillas utilizadas y
     *   devolver las varillas que componen la solución.
     */
    public static class Ejercicio204 {
        const int I = int.MaxValue; // Infinito

        static int Min(int a, int b) {
            return (b < a) ? b : a;
        }

        static int[,] CalcMinLon(int[] L, int N, int T) {
            int MaxElem = N + 1;
            int MaxCant = T + 1;
            int[,] tabla = new int[MaxElem, MaxCant];

            for(int i = 0; i < MaxElem; i++) { tabla[i, 0] = 0; }
            for(int i = 1; i < MaxCant; i++) { tabla[0, i] = I; }

            int a, b;
            for(int i = 1; i < MaxElem; i++) {
                for(int j = 1; j < MaxCant; j++) {
                    if(L[i - 1] <= j) {
                        a = tabla[i - 1, j];
                        b = tabla[i - 1, j - L[i - 1]];
                        if(b != I) b += 1;
                        tabla[i, j] = Min(a, b);
                    } else {
                        tabla[i, j] = tabla[i - 1, j];
                    }
                }
            }

            return tabla;
        }

        static int[] TransMinLon(int[] L, int N, int T, int[,] tabla) {
            int MaxElem = N + 1;
            int MaxCant = T + 1;
            List<int> r = new List<int>();

            Mostrar(tabla, MaxElem, MaxCant);

            int i = MaxElem - 1;
            int j = MaxCant - 1;

            while(i > 0 && j > 0) {
                if(tabla[i, j] == tabla[i - 1, j]) {
                    i--;
                } else if(j >= L[i - 1] && tabla[i - 1, j - L[i - 1]] == tabla[i, j] - 1) {
                    r.Add(i - 1);
                    j -= L[i - 1];
                    i--;
                } else if(j < L[i - 1]) {
                    j = 0;
                }
            }

            return r.ToArray();
        }

        static void Resolver1(int[] L, int N, int T) {
            int[,] tabla = CalcMinLon(L, N, T);
            int[] r = TransMinLon(L, N, T, tabla);

            Console.WriteLine("L: " +
                              L.Select(x => x.ToString())
                               .Aggregate((x, xs) => x + " " + xs));
            Console.Write("Lista: ");
            for(int i = 0; i < r.Length; i++) {
                Console.Write(L[r[i]] + " ");
            }
            Console.WriteLine("\n");
        }

        static int[,] CalcMinCos(int[] L, int[] C, int N, int T) {
            int MaxElem = N + 1;
            int MaxCant = T + 1;
            int[,] tabla = new int[MaxElem, MaxCant];

            for(int i = 0; i < MaxElem; i++) { tabla[i, 0] = 0; }
            for(int i = 1; i < MaxCant; i++) { tabla[0, i] = I; }

            int a, b;
            for(int i = 1; i < MaxElem; i++) {
                for(int j = 1; j < MaxCant; j++) {
                    if(L[i - 1] <= j) {
                        a = tabla[i - 1, j];
                        b = tabla[i - 1, j - L[i - 1]];
                        if(b != I) b += C[i - 1];
                        tabla[i, j] = Min(a, b);
                    } else {
                        tabla[i, j] = tabla[i - 1, j];
                    }
                }
            }

            return tabla;
        }

        static int[] TransMinCos(int[] L, int[] C, int N, int T, int[,] tabla) {
            int MaxElem = N + 1;
            int MaxCant = T + 1;
            List<int> r = new List<int>();

            Mostrar(tabla, MaxElem, MaxCant);

            int i = MaxElem - 1;
            int j = MaxCant - 1;

            while(i > 0 && j > 0) {
                if(tabla[i, j] == tabla[i - 1, j]) {
                    i--;
                } else if(j >= L[i - 1] && tabla[i - 1, j - L[i - 1]] == tabla[i, j] - C[i - 1]) {
                    r.Add(i - 1);
                    j -= L[i - 1];
                    i--;
                } else if(j < L[i - 1]) {
                    j = 0;
                }
            }

            return r.ToArray();
        }

        static void Resolver2(int[] L, int[] C, int N, int T) {
            int[,] tabla = CalcMinCos(L, C, N, T);
            int[] r = TransMinCos(L, C, N, T, tabla);

            Console.WriteLine("L: " +
                              L.Select(x => x.ToString())
                               .Aggregate((x, xs) => x + " " + xs));
            Console.WriteLine("C: " +
                              C.Select(x => x.ToString())
                               .Aggregate((x, xs) => x + " " + xs));
            Console.Write("Lista: ");
            for(int i = 0; i < r.Length; i++) {
                Console.Write(L[r[i]] + " ");
            }
            Console.WriteLine("\n");
        }

        public static void Resolver() {
            int[] L = { 1, 1, 2, 2, 3, 3, 4, 5, 6, 7 };
            int[] C = { 10, 10, 22, 22, 34, 34, 46, 58, 70, 82 };

            Resolver1(L, L.Length, 16);
            Resolver2(L, C, L.Length, 16);
        }

        static void Mostrar(int[,] tabla, int MaxElem, int MaxCant) {
            Console.WriteLine("Tabla:");
            for(int i = 0; i < MaxElem; i++) {
                for(int j = 0; j < MaxCant; j++) {
                    if(tabla[i, j] == I) {
                        Console.Write(" -  ");
                    } else {
                        Console.Write(tabla[i, j].ToString("000") + " ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
