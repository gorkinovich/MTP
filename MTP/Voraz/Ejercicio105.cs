using System;
using System.Collections.Generic;
using System.Linq;

namespace MTP.Voraz {
    /*
     * Ejercicio 5:
     * Se dispone de un grafo no dirigido con n vértices. Se pretende colorear los vértices
     * utilizando el mínimo número de colores, teniendo en cuenta que dos vértices adyacentes
     * no pueden pintarse del mismo color. Diseña un algoritmo voraz que resuelva este problema.
     * ¿El algoritmo propuesto es óptimo? Si no es así, busca un ejemplo en el que el algoritmo
     * no proporcione la mejor solución.
     */
    public static class Ejercicio105 {
        const int I = int.MaxValue; // Infinito

        static int Candidato(int[,] grafo, int tam, int[] r, int[] c) {
            int vmax = 0, imax = -1;
            for(int i = 0; i < tam; i++) {
                if(r[i] == -1 && c[i] > vmax) {
                    vmax = c[i];
                    imax = i;
                }
            }
            return imax;
        }

        static int[] Conexiones(int[,] grafo, int tam) {
            int[] r = Enumerable.Range(0, tam).Select(x => 0).ToArray();

            for(int i = 0; i < tam; i++) {
                for(int j = 0; j < tam; j++) {
                    if(grafo[i, j] != I) {
                        r[i]++;
                    }
                }
            }

            return r;
        }

        static int[] Colorea(int[,] grafo, int tam) {
            int[] r = Enumerable.Range(0, tam).Select(x => -1).ToArray();
            int[] conex = Conexiones(grafo, tam);
            List<int> vecinos = new List<int>();
            int col, i;

            // Algoritmo de Welsh–Powell (1967)
            for(int k = 0; k < tam; k++) {
                i = Candidato(grafo, tam, r, conex);

                vecinos.Clear();
                for(int j = 0; j < tam; j++) {
                    if(grafo[i, j] != I) {
                        vecinos.Add(r[j]);
                    }
                }

                col = 1;
                foreach(int n in vecinos.OrderBy(x => x)) {
                    if(col == n) col++;
                }

                r[i] = col;
            }

            return r;
        }

        public static void Resolver() {
            const int numNodos = 7;
            int[,] grafo =
            { // A  B  C  D  E  F  G
                {I, 0, I, 0, I, I, I}, // A
                {0, I, 0, 0, 0, I, I}, // B
                {I, 0, I, I, 0, I, I}, // C
                {0, 0, I, I, 0, 0, I}, // D
                {I, 0, 0, 0, I, 0, 0}, // E
                {I, I, I, 0, 0, I, 0}, // F
                {I, I, I, I, 0, 0, I}  // G
            };

            int[] r = Colorea(grafo, numNodos);

            char aux = 'A';
            Console.WriteLine(r.Select(x => (aux++) + " " + x.ToString())
                               .Aggregate((x, xs) => x + ", " + xs));
            Console.WriteLine("Nº de colores: " + r.Max());
        }
    }
}
