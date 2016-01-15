using System;
using System.Collections.Generic;
using System.Linq;

namespace MTP.Voraz {
    /*
     * http://es.wikipedia.org/wiki/Algoritmo_de_Dijkstra
     */
    public static class Dijkstra {
        const int I = int.MaxValue; // Infinito

        struct Resultado {
            public int[] coste;
            public int[] anterior;
        }

        static int VérticeMínimo(List<int> nodos, int[] coste) {
            int v = nodos.First();
            foreach(int w in nodos) {
                if(coste[w] < coste[v]) {
                    v = w;
                }
            }
            return v;
        }

        static Resultado CalcDijkstra(int[,] grafo, int tam) {
            List<int> nodos = Enumerable.Range(1, tam - 1).ToList();
            Resultado r = new Resultado();
            int v;

            r.coste = Enumerable.Range(0, tam).Select(x => grafo[0, x]).ToArray();
            r.anterior = Enumerable.Range(0, tam).Select(x => 0).ToArray();

            while(nodos.Count() > 1) {
                v = VérticeMínimo(nodos, r.coste);
                nodos = nodos.Where(x => x != v).ToList();
                foreach(int w in nodos) {
                    if(grafo[v, w] < I && r.coste[v] + grafo[v, w] < r.coste[w]) {
                        r.coste[w] = r.coste[v] + grafo[v, w];
                        r.anterior[w] = v;
                    }
                }
            }

            return r;
        }

        static string Camino(int dest, Resultado r, int tam) {
            if(dest == 0) {
                return "";
            } else {
                return Camino(r.anterior[dest], r, tam) +
                       " -> " + (char)('A' + dest);
            }
        }

        static void Mostrar(Resultado r, int tam) {
            for(int i = 1; i < tam; i++) {
                Console.WriteLine("Camino: A{0}", Camino(i, r, tam));
                Console.WriteLine("Coste: {0}\n", r.coste[i]);
            }
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

            Resultado r = CalcDijkstra(grafo, numNodos);
            Mostrar(r, numNodos);
        }
    }
}