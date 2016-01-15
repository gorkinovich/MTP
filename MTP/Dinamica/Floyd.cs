using System;
using System.Collections.Generic;
using System.Linq;

namespace MTP.Dinamica {
    /*
     * http://es.wikipedia.org/wiki/Algoritmo_de_Floyd
     */
    public static class Floyd {
        const int I = int.MaxValue; // Infinito

        struct Resultado {
            public Resultado(int[,] cos, int[,] cam) {
                this.coste = cos;
                this.camino = cam;
            }
            public int[,] coste;
            public int[,] camino;
        }

        static int Suma(int[,] g, int i, int j, int k) {
            if(g[i, k] == I || g[k, j] == I) {
                return I;
            } else {
                return g[i, k] + g[k, j];
            }
        }

        static Resultado CalcFloyd(int[,] grafo, int tam) {
            int[,] coste = new int[tam, tam];
            int[,] camino = new int[tam, tam];
            for(int i = 0; i < tam; i++) {
                for(int j = 0; j < tam; j++) {
                    coste[i, j] = (i == j) ? 0 : grafo[i, j];
                    camino[i, j] = 0;
                }
            }
            int aux;
            for(int k = 0; k < tam; k++) {
                for(int i = 0; i < tam; i++) {
                    for(int j = 0; j < tam; j++) {
                        aux = Suma(coste, i, j, k);
                        if(aux < coste[i, j]) {
                            coste[i, j] = aux;
                            camino[i, j] = k;
                        }
                    }
                }
            }
            return new Resultado(coste, camino);
        }

        static void Camino(int[,] grafo, int i, int j) {
            if(grafo[i, j] != 0) {
                Camino(grafo, i, grafo[i, j]);
                Console.Write((char)('A' + grafo[i, j]) + " ");
                Camino(grafo, grafo[i, j], j);
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

        public static void Resolver() {
            const int numNodos = 7;
            int[,] grafo =
            { // A  B  C   D   E   F   G
                {I, 7, I,  5,  I,  I,  I}, // A
                {7, I, 8,  9,  7,  I,  I}, // B
                {I, 8, I,  I,  5,  I,  I}, // C
                {5, 9, I,  I, 15,  6,  I}, // D
                {I, 7, 5, 15,  I,  8,  9}, // E
                {I, I, I,  6,  8,  I, 11}, // F
                {I, I, I,  I,  9, 11,  I}  // G
            };
            Mostrar("Grafo:", grafo, numNodos);

            Resultado r = CalcFloyd(grafo, numNodos);
            Mostrar("Costes:", r.coste, numNodos);
            Mostrar("Caminos:", r.camino, numNodos);

            Console.WriteLine("Caminos desde A:");
            for(int i = 1; i < numNodos; i++) {
                Console.Write("Camino A -> {0}: A ", (char)('A' + i));
                Camino(r.camino, 0, i);
                Console.Write("{0}", (char)('A' + i));
                Console.WriteLine();
            }
        }
    }
}
