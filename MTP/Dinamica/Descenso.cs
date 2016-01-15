using System;
using System.Collections.Generic;
using System.Linq;

namespace MTP.Dinamica {
    public static class Descenso {
        const int I = int.MaxValue; // Infinito

        static int Min(int[,] r, int N, int orig, int dest) {
            int min = r[orig, dest];
            for(int i = orig + 1; i < dest; i++) {
                if(r[orig, i] + r[i, dest] < min) {
                    min = r[orig, i] + r[i, dest];
                }
            }
            return min;
        }

        static int[,] Calcular(int[,] grafo, int N) {
            int[,] r = new int[N, N];
            for(int i = 0; i < N; i++) {
                for(int j = 0; j < N; j++) {
                    r[i, j] = grafo[i, j];
                }
            }

            for(int i = N - 2; i >= 0; i--) {
                for(int j = N - 1; j > i; j--) {
                    r[i, j] = Min(r, N, i, j);
                }
            }
            return r;
        }

        static void Mostrar(string msj, int[,] tabla, int N) {
            Console.WriteLine(msj);
            for(int i = 0; i < N; i++) {
                for(int j = 0; j < N; j++) {
                    if(tabla[i, j] == I) {
                        Console.Write("INF ");
                    } else {
                        Console.Write(tabla[i, j].ToString("000") + " ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public static void Resolver() {
            const int numNodos = 10;
            int[,] grafo =
            { // A   B   C   D   E    F    G    H    I    J
                {0, 10, 25, 50, 80, 160, 240, 320, 480, 640}, // A
                {I,  0, 10, 30, 50,  80, 120, 200, 300, 400}, // B
                {I,  I,  0, 10, 20,  40,  90, 120, 200, 300}, // C
                {I,  I,  I,  0, 20,  30,  50,  80, 120, 200}, // D
                {I,  I,  I,  I,  0,  10,  30,  50,  80, 120}, // E
                {I,  I,  I,  I,  I,   0,  15,  30,  50,  80}, // F
                {I,  I,  I,  I,  I,   I,   0,  10,  20,  40}, // G
                {I,  I,  I,  I,  I,   I,   I,   0,  20,  30}, // H
                {I,  I,  I,  I,  I,   I,   I,   I,   0,  15}, // I
                {I,  I,  I,  I,  I,   I,   I,   I,   I,   0}, // J
            };
            Mostrar("Tarifas:", grafo, numNodos);

            int[,] result = Calcular(grafo, numNodos);
            Mostrar("Resultado:", result, numNodos);
        }
    }
}
