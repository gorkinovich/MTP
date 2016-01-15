using System;
using System.Collections.Generic;
using System.Linq;

namespace MTP.Voraz {
    /*
     * Un camionero conduce desde Bilbao a Málaga siguiendo una ruta dada y llevando un camión que
     * le permite, con el tanque de gasolina lleno, recorrer n kilómetros sin parar. El camionero
     * dispone de un mapa de carreteras que le indica las distancias entre las gasolineras que hay
     * en su ruta. Como va con prisa, el camionero desea parar a repostar el menor número de veces
     * posible.
     * 
     * Deseamos diseñar un algoritmo voraz para determinar en qué gasolineras tiene que parar y
     * demostrar que el algoritmo encuentra siempre la solución óptima.
     */
    public static class Ejercicio109 {
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

            r.coste = Enumerable.Range(0, tam)
                                .Select(x => grafo[0, x])
                                .ToArray();
            r.anterior = Enumerable.Range(0, tam)
                                   .Select(x => 0)
                                   .ToArray();

            while(nodos.Count() > 1) {
                v = VérticeMínimo(nodos, r.coste);
                nodos = nodos.Where(x => x != v).ToList();
                foreach(int w in nodos) {
                    if(grafo[v, w] < I &&
                       r.coste[v] + grafo[v, w] < r.coste[w]) {
                        r.coste[w] = r.coste[v] + grafo[v, w];
                        r.anterior[w] = v;
                    }
                }
            }

            return r;
        }

        static int Suma(int[] lista, int ini, int fin) {
            return lista.Skip(ini).Take(fin - ini).Sum();
        }

        static int[,] Transformar(int[] lista, int max) {
            int tam = lista.Length + 1;
            int[,] grafo = new int[tam, tam];
            int val;

            for(int i = 0; i < tam; i++) {
                grafo[i, i] = I;
                for(int j = i + 1; j < tam; j++) {
                    val = Suma(lista, i, j);
                    grafo[i, j] = (val > max) ? I : max - val;
                }
            }

            for(int i = 0; i < tam; i++) {
                for(int j = i + 1; j < tam; j++) {
                    grafo[j, i] = I;
                }
            }

            return grafo;
        }

        static string Camino(int dest, Resultado r, int tam) {
            if(dest == 0) {
                return "";
            } else {
                return Camino(r.anterior[dest], r, tam) + " -> " + dest;
            }
        }

        public static void Resolver() {
            const int N = 100;//0  1   2   3   4   5   6   7   8   9   10
            int[] distancias = { 30, 50, 40, 30, 60, 40, 50, 30, 30, 40 };
            int tam = distancias.Length + 1;
            int[,] grafo = Transformar(distancias, N);

            Console.WriteLine("Grafo: ");
            for(int i = 0; i < tam; i++) {
                for(int j = 0; j < tam; j++) {
                    Console.Write("{0} ", (grafo[i, j] == I) ? " I " :
                                  grafo[i, j].ToString("000"));
                }
                Console.WriteLine();
            }
            Console.WriteLine();

            int dest = distancias.Length;
            Resultado r = CalcDijkstra(grafo, tam);
            Console.WriteLine("Camino: 0{0}", Camino(dest, r, tam));
            Console.WriteLine("Coste: {0}\n", r.coste[dest]);
        }
    }
}
