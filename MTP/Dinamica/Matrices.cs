using System;
using System.Collections.Generic;
using System.Linq;

namespace MTP.Dinamica {
    public static class Matrices {
        const int I = int.MaxValue; // Infinito

        struct Dim {
            public Dim(int al, int an) {
                this.alto = al;
                this.ancho = an;
            }
            public int alto;
            public int ancho;
        }

        static int Min(int a, int b) {
            return (b < a) ? b : a;
        }

        static int[,] Calcular(Dim[] d, int N) {
            int[,] r = new int[N, N];
            for(int i = 0; i < N; i++) {
                for(int j = 0; j < N; j++) {
                    if(i <= j)
                        r[i, j] = 0;
                    else
                        r[i, j] = I;
                }
            }

            for(int i = N - 2; i >= 0; i--) {
                for(int j = i + 1; j < N; j++) {
                    r[i, j] = I;
                    for(int k = i; k < j; k++) {
                        r[i, j] = Min(r[i, j], r[i, k] + r[k + 1, j] +
                                  d[i].alto * d[k].ancho * d[j].ancho);
                    }
                }
            }
            return r;
        }

        static void MostrarFormula(int[,] r, Dim[] d, int i, int j) {
            const char A = 'A';
            if(i + 1 < j) {
                for(int k = i; k < j; k++) {
                    if(r[i, j] == r[i, k] + r[k + 1, j] +
                       d[i].alto * d[k].ancho * d[j].ancho) {
                        Console.Write("(");
                        MostrarFormula(r, d, i, k);
                        Console.Write(" ");
                        MostrarFormula(r, d, k + 1, j);
                        Console.Write(")");
                        return;
                    }
                }
            } else if(i < j) {
                Console.Write("(" + (char)(A + i) +
                              " " + (char)(A + j) + ")");
            } else if(i == j) {
                Console.Write("(" + (char)(A + i) + ")");
            }
        }

        static void Mostrar(string msj, int[,] tabla, int N) {
            Console.WriteLine(msj);
            for(int i = 0; i < N; i++) {
                for(int j = 0; j < N; j++) {
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

        static void Resolver(Dim[] lista, int N) {
            int[,] r = Calcular(lista, N);
            Mostrar("Resultado:", r, N);

            Console.WriteLine("Nº de multiplicaciones: " + r[0, N - 1]);
            Console.Write("Formula: ");
            MostrarFormula(r, lista, 0, N - 1);
            Console.WriteLine("\n");
        }

        public static void Resolver() {
            Dim[] lista =
            {
                new Dim(2, 5), new Dim(5, 3),
                new Dim(3, 4), new Dim(4, 2),
                new Dim(2, 7), new Dim(7, 4),
                new Dim(4, 5)
            };
            Resolver(lista, lista.Length - 1);
            Resolver(lista, lista.Length);
        }
    }
}
