using System;
using System.Collections.Generic;
using System.Linq;

namespace MTP.VueltaAtras {
    public static class Caballo {
        const int N = 8;

        static bool Calcular(int m, int f, int c, int[,] tabla) {
            if(0 <= f && f < N && 0 <= c && c < N && tabla[f, c] == 0) {
                tabla[f, c] = m;
                if(m == 1) {
                    return true;
                } else if(Calcular(m - 1, f + 1, c + 2, tabla) ||
                          Calcular(m - 1, f + 2, c + 1, tabla) ||
                          Calcular(m - 1, f + 2, c - 1, tabla) ||
                          Calcular(m - 1, f + 1, c - 2, tabla) ||
                          Calcular(m - 1, f - 1, c - 2, tabla) ||
                          Calcular(m - 1, f - 2, c - 1, tabla) ||
                          Calcular(m - 1, f - 2, c + 1, tabla) ||
                          Calcular(m - 1, f - 1, c + 2, tabla)) {
                    return true;
                }
                tabla[f, c] = 0;
            }
            return false;
        }

        public static void Resolver() {
            int[,] tabla = new int[N, N];

            for(int i = 0; i < N; i++) {
                for(int j = 0; j < N; j++) {
                    tabla[i, j] = 0;
                }
            }

            if(Calcular(N * N, 0, 0, tabla)) {
                Mostrar(tabla);
            } else {
                Console.WriteLine("No hay solución...");
            }
        }

        static void Mostrar(int[,] tabla) {
            Console.WriteLine("Solución:");
            for(int i = 0; i < N; i++) {
                for(int j = 0; j < N; j++) {
                    Console.Write(tabla[i, j].ToString("00") + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
