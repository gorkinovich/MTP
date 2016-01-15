using System;
using System.Collections.Generic;
using System.Linq;

namespace MTP.VueltaAtras {
    /* 
     * En el departamento de una empresa de traducciones se desea hacer traducciones de textos entre
     * varios idiomas. Se dispone de algunos diccionarios. Cada diccionario permite la traducción
     * (bidireccional) entre dos idiomas. En el caso más general, no se dispone de diccionarios para
     * cada par de idiomas por lo que es preciso realizar varias traducciones. Dados N idiomas y M
     * diccionarios, determina si es posible realizar la traducción entre dos idiomas dados y, en
     * caso de ser posible, determina la cadena de traducciones de longitud mínima.
     * 
     * Diseña un algoritmo de vuelta atrás que resuelva el problema detallando lo siguiente:
     * 1. El árbol de búsqueda: significado de las arista y de los niveles (0,5 puntos)
     * 2. El código del procedimiento (2 puntos)
     * 3. El programa llamador (0,5 puntos)
     */
    public static class Ejercicio302 {
        const int I = int.MaxValue; // Infinito

        static void Camino(bool[,] grafo, bool[] visitado, int N, int o,
                           int d, LinkedList<int> aux, ref int[] r) {
            if(o == d) {
                if(r == null || aux.Count < r.Length) {
                    r = aux.ToArray();
                }
            } else {
                visitado[o] = true;
                for(int i = 0; i < N; i++) {
                    if(!visitado[i] && grafo[o, i]) {
                        aux.AddLast(i);
                        Camino(grafo, visitado, N, i, d, aux, ref r);
                        aux.RemoveLast();
                    }
                }
                visitado[o] = false;
            }
        }

        static int[] Camino(bool[,] grafo, int N, int orig, int dest) {
            LinkedList<int> aux = new LinkedList<int>();
            bool[] v = Enumerable.Range(0, N).Select(x => false).ToArray();
            int[] r = null;
            Camino(grafo, v, N, orig, dest, aux, ref r);
            return r;
        }

        static void Resolver(bool[,] grafo, string[] l, int N,
                             int orig, int dest) {
            int[] r = Camino(grafo, N, orig, dest);
            Console.Write(l[orig]);
            foreach(int i in r) {
                Console.Write(" -> " + l[i]);
            }
            Console.WriteLine();
        }

        public static void Resolver() {
            const int numNodos = 7;
            string[] lenguaje = { "Aklo", "Elfo", "Klingon",
                                  "Parsel", "Troll", "D'ni",
                                  "Neolengua" };
            bool[,] grafo =
            { //   A      B      C      D      E      F      G
                {false, true,  false, true,  false, false, false}, // A
                {false, false, true,  true,  true,  false, false}, // B
                {false, false, false, false, true,  false, false}, // C
                {false, true,  false, false, true,  true,  false}, // D
                {false, false, true,  false, false, true,  true},  // E
                {false, false, false, false, true,  false, true},  // F
                {false, false, false, false, false, true,  false}  // G
            };
            Resolver(grafo, lenguaje, numNodos, 0, 6);
        }
    }
}
