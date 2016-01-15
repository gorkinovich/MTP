using System;
using System.Collections.Generic;
using System.Linq;

namespace MTP.VueltaAtras {
    /* 
     * Recorridos del rey de ajedrez:
     * En un tablero de ajedrez de tamaño NxN se coloca un rey en una casilla arbitraria (x[0], y[0]).
     * Cada casilla (x, y) del tablero tiene asignado un peso T(x, y), de tal forma que a cada recorrido
     * R = {(x[0], y[0]), ..., (x[k], y[k])} se le puede asignar un valor que viene determinado por la
     * siguiente expresión: P(R) = Sum(i=0..k) {i * T(x[i], y[i])
     * 
     * El problema consiste en diseñar un algoritmo que proporcione el recorrido de peso mínimo que
     * visite todas las casillas del tablero sin repetir ninguna.
     */
    public static class Ejercicio306 {
        const int N = 4;
        const int M = N * N;
        static int[] RX = { -1, 0, 1, 1, 1, 0, -1, -1 };
        static int[] RY = { -1, -1, -1, 0, 1, 1, 1, 0 };

        static void Calcular(int[,] t, int[,] r, int x, int y, int i,
                             ref int min, int auxv, int[,] aux) {
            if(0 <= x && x < N && 0 <= y && y < N && aux[y, x] == 0) {
                aux[y, x] = i + 1;
                int temp = auxv + t[y, x] * aux[y, x];
                if(aux[y, x] < M) {
                    if(temp < min) {
                        for(int k = 0; k < RX.Length; k++) {
                            Calcular(t, r, x + RX[k], y + RY[k], i + 1,
                                     ref min, temp, aux);
                        }
                    }
                } else if(min > temp) {
                    min = temp;
                    for(int f = 0; f < N; f++) {
                        for(int c = 0; c < N; c++) {
                            r[f, c] = aux[f, c];
                        }
                    }

                }
                aux[y, x] = 0;
            }
        }

        static int[,] Calcular(int[,] tabla) {
            int[,] r = new int[N, N];
            int[,] aux = new int[N, N];

            for(int i = 0; i < N; i++) {
                for(int j = 0; j < N; j++) {
                    r[i, j] = 0;
                    aux[i, j] = 0;
                }
            }

            int min = int.MaxValue;
            Calcular(tabla, r, 0, 0, 0, ref min, 0, aux);

            return r;
        }

        public static void Resolver() {
            int[,] tabla =
            {
                { 1, 2, 3, 4 },
                { 2, 3, 4, 5 },
                { 3, 4, 5, 6 },
                { 4, 5, 6, 7 }
            };

            int[,] r = Calcular(tabla);
            int suma = 0;

            for(int i = 0; i < N; i++) {
                for(int j = 0; j < N; j++) {
                    suma += (r[i, j] * tabla[i, j]);
                    Console.Write(r[i, j].ToString("00") + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("\nTotal = " + suma + "\n");
        }
    }
}
