using System;
using System.Collections.Generic;
using System.Linq;

namespace MTP.Dinamica {
    public static class Monedas02 {
        const int I = int.MaxValue; // Infinito

        static int Min(int a, int b) {
            return (b < a) ? b : a;
        }

        static int Arriba(int[,] tabla, int i, int j) {
            return tabla[i - 1, j];
        }

        static int Actual(int[,] tabla, int i, int j) {
            return (tabla[i, j] == I) ? I : tabla[i, j] + 1;
        }

        static int[,] CalcCambio(int[] lista, int c) {
            int MaxMon = lista.Length + 1;
            int MaxCan = c + 1;
            int[,] tabla = new int[MaxMon, MaxCan];

            for(int i = 0; i < MaxMon; i++) {
                tabla[i, 0] = 0;
            }
            for(int i = 1; i < MaxCan; i++) {
                tabla[0, i] = I;
            }

            int a, b;
            for(int i = 1; i < MaxMon; i++) {
                for(int j = 1; j < MaxCan; j++) {
                    if(lista[i - 1] <= j) {
                        a = Arriba(tabla, i, j);
                        b = Actual(tabla, i, j - lista[i - 1]);
                        tabla[i, j] = Min(a, b);
                    } else {
                        tabla[i, j] = Arriba(tabla, i, j);
                    }
                }
            }

            return tabla;
        }

        static void Mostrar(int[,] tabla, int MaxMon, int MaxCan) {
            Console.WriteLine("Tabla:");
            for(int i = 0; i < MaxMon; i++) {
                for(int j = 0; j < MaxCan; j++) {
                    if(tabla[i, j] == I) {
                        Console.Write("I ");
                    } else {
                        Console.Write(tabla[i, j] + " ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        static int[] Transformar(int[] lista, int c, int[,] tabla) {
            int MaxMon = lista.Length + 1;
            int MaxCan = c + 1;
            List<int> r = new List<int>();

            Mostrar(tabla, MaxMon, MaxCan);

            int i = MaxMon - 1;
            int j = MaxCan - 1;

            while(i > 0 && j > 0) {
                if(tabla[i, j] == tabla[i - 1, j]) {
                    i--;
                } else if(tabla[i, j - lista[i - 1]] == tabla[i, j] - 1) {
                    r.Add(lista[i - 1]);
                    j -= lista[i - 1];
                }
            }

            return r.ToArray();
        }

        static int[] Cambio(int[] lista, int c) {
            int[,] tabla = CalcCambio(lista, c);
            return Transformar(lista, c, tabla);
        }

        static void Resolver(int[] lista, int c) {
            int[] cambio = Cambio(lista, c);
            Console.WriteLine("Cantidad: " + c);
            Console.WriteLine("Cambio: " +
                              cambio.Select(x => x.ToString())
                                    .Aggregate((x, xs) => x + " " + xs));
            Console.WriteLine();
        }

        public static void Resolver() {
            int[] lista = { 1, 4, 6 };
            Resolver(lista, 8);

            int[] lista2 = { 1, 2, 5 };
            Resolver(lista2, 10);
        }
    }
}
