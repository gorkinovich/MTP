using System;
using System.Collections.Generic;
using System.Linq;

namespace MTP.Dinamica {
    /* 
     * Ejercicio 1:
     * Subsecuencia creciente de longitud máxima. Dada una secuencia A de n números naturales,
     * se debe obtener mediante un algoritmo dinámico la subsecuencia de longitud máxima de
     * este vector en la que los elementos están en orden creciente. Una subsecuencia creciente
     * de A = {a1, a2, ..., an} es una selección de elementos de A no necesariamente consecutivos
     * tales que sus elementos aparecen en orden creciente.
     * 
     * Por ejemplo, la secuencia A = {0, 7, 3, 8, 4, 2, 6} tiene como subsecuencia creciente de
     * longitud máxima la siguiente: {0, 3, 4, 6}.
     */
    public static class Ejercicio201 {
        static List<int> Max(List<int> a, List<int> b) {
            return (b.Count > a.Count) ? b : a;
        }

        static bool Validar(List<int> lista) {
            int ant = int.MinValue;
            foreach(int n in lista) {
                if(ant < n) {
                    ant = n;
                } else {
                    return false;
                }
            }
            return true;
        }

        static int[] Calcular(int[] lista, int N) {
            if(N > 0) {
                List<int>[,] tabla = new List<int>[N, N];
                for(int i = 0; i < N; i++) {
                    for(int j = 0; j < N; j++) {
                        if(i <= j) {
                            tabla[i, j] = new List<int>();
                            if(i == j) {
                                tabla[i, j].Add(lista[i]);
                            }
                        } else {
                            tabla[i, j] = null;
                        }
                    }
                }

                Mostrar(tabla, N);

                for(int i = N - 1; i >= 0; i--) {
                    for(int j = i; j < N; j++) {
                        for(int k = i + 1; k <= j; k++) {
                            // Construcción del candidato
                            List<int> aux = new List<int>();
                            aux.Add(lista[i]);
                            foreach(int n in tabla[k, j]) {
                                aux.Add(n);
                            }
                            // Validación y maximización
                            if(Validar(aux)) {
                                tabla[i, j] = Max(tabla[i, j], aux);
                            }
                        }
                    }
                }

                Mostrar(tabla, N);
                
                return tabla[0, N - 1].ToArray();
            } else {
                return null;
            }
        }

        static void Resolver(int[] lista) {
            int[] result = Calcular(lista, lista.Length);
            Console.WriteLine("Lista: " +
                              lista.Select(x => x.ToString())
                                   .Aggregate((x, xs) => x + " " + xs));
            Console.WriteLine("Resultado: " +
                              result.Select(x => x.ToString())
                                    .Aggregate((x, xs) => x + " " + xs));
            Console.WriteLine();
        }

        public static void Resolver() {
            int[] lista = { 0, 7, 3, 8, 4, 2, 6 };
            Resolver(lista);

            int[] lista2 = { 0, 7, 3, 8, 1, 2, 3, 6 };
            Resolver(lista2);
        }

        static void Mostrar(List<int>[,] tabla, int N) {
            Console.WriteLine("Tabla:");
            for(int i = 0; i < N; i++) {
                for(int j = 0; j < N; j++) {
                    if(tabla[i, j] == null) {
                        Console.Write(" - ");
                    } else {
                        Console.Write(tabla[i, j].Count.ToString("00") + " ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
