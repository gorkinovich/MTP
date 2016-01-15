using System;
using System.Collections.Generic;
using System.Linq;

namespace MTP.Dinamica {
    /* 
     * En el departamento de una empresa de traducciones se desea hacer traducciones de textos entre
     * varios idiomas. Se dispone de algunos diccionarios. Cada diccionario permite la traducción
     * (bidireccional) entre dos idiomas. En el caso más general, no se dispone de diccionarios para
     * cada par de idiomas, por lo que es preciso realizar varias traducciones. Dados N idiomas y M
     * diccionarios, determina si es posible realizar la traducción entre dos idiomas dados y, en
     * caso de ser posible, determina la cadena de traducciones de longitud mínima.
     * 
     * Diseña un algoritmo dinámico que resuelva el problema detallando lo siguiente:
     * 1. La estructura de cálculos intermedios utilizada (0,5 puntos).
     * 2. La relación recursiva entre subproblemas (0,5 puntos).
     * 3. El procedimiento o función que implemente el algoritmo (2 puntos).
     * 
     * Ejemplo 1:
     *     Traducir del latín al arameo disponiendo de los siguientes diccionarios:
     *     latín-griego, griego-etrusco, griego-demótico, demótico-arameo
     *     Solución: Sí es posible realizando 3 traducciones: latín-griego-demótico-arameo.
     * Ejemplo 2:
     *     Traducir del latín al arameo disponiendo de los siguientes diccionarios:
     *     latín-griego, arameo-etrusco, griego-demótico, demótico-hebreo
     *     Solución: No es posible la traducción.
     */
    public static class Ejercicio206 {
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

        static void Camino(int[,] grafo, string[] leng, int i, int j) {
            if(grafo[i, j] != 0) {
                Camino(grafo, leng, i, grafo[i, j]);
                Console.Write(leng[grafo[i, j]] + " ");
                Camino(grafo, leng, grafo[i, j], j);
            }
        }

        static void Mostrar(string msj, int[,] tabla, int N) {
            Console.WriteLine(msj);
            for(int i = 0; i < N; i++) {
                for(int j = 0; j < N; j++) {
                    if(tabla[i, j] == I) {
                        Console.Write("- ");
                    } else {
                        Console.Write(tabla[i, j].ToString() + " ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        static bool ExisteCamino(Resultado r, int i, int j) {
            return r.coste[i, j] != I;
        }

        static void Caminos(Resultado r, string[] lng, int N, int i) {
            Console.WriteLine("Caminos desde {0}:", lng[i]);
            for(int j = 0; j < N; j++) {
                if(i != j) {
                    if(ExisteCamino(r, i, j)) {
                        Console.Write("Camino {0} -> {1}: {0} ",
                                      lng[i], lng[j]);
                        Camino(r.camino, lng, i, j);
                        Console.WriteLine(lng[j]);
                    } else {
                        Console.WriteLine("Camino " + lng[i] +
                                          " -> " + lng[j] +
                                          ": No existe camino...");
                    }
                }
            }
        }

        public static void Resolver() {
            const int numNodos = 7;
            string[] lenguaje = { "Aklo", "Elfo", "Klingon",
                                  "Parsel", "Troll", "D'ni",
                                  "Neolengua" };
            int[,] grafo =
            { // A  B  C  D  E  F  G
                {0, 1, I, 1, I, I, I}, // A
                {I, 0, 1, 1, 1, I, I}, // B
                {I, I, 0, I, 1, I, I}, // C
                {I, 1, I, 0, 1, 1, I}, // D
                {I, I, 1, I, 0, 1, 1}, // E
                {I, I, I, I, 1, 0, 1}, // F
                {I, I, I, I, I, 1, 0}  // G
            };
            /*
            int[,] grafo =
            { // A  B  C  D  E  F  G
                {0, 1, I, 1, I, I, I}, // A
                {1, 0, 1, 1, 1, I, I}, // B
                {I, 1, 0, I, 1, I, I}, // C
                {1, 1, I, 0, 1, 1, I}, // D
                {I, 1, 1, 1, 0, 1, 1}, // E
                {I, I, I, 1, 1, 0, 1}, // F
                {I, I, I, I, 1, 1, 0}  // G
            };
            //*/
            Mostrar("Grafo:", grafo, numNodos);

            Resultado r = CalcFloyd(grafo, numNodos);
            Mostrar("Costes:", r.coste, numNodos);
            Mostrar("Caminos:", r.camino, numNodos);

            for(int i = 0; i < numNodos; i++) {
                Caminos(r, lenguaje, numNodos, i);
                Console.WriteLine();
            }
        }
    }
}
