using System;
using System.Collections.Generic;
using System.Linq;

namespace MTP.Voraz {
    /*
     * Minimización del tiempo total en un sistema. Tenemos una serie de procesos a ejecutar en un
     * ordenador, que ejecuta los procesos en orden secuencial. Queremos minimizar el tiempo medio
     * que los procesos permanecen en el sistema: la suma de los tiempos de espera más los tiempos
     * de ejecución.
     * 
     * Suponemos que los tiempos que va a tardar la ejecución de cada uno de los procesos son
     * conocidos. Se pide definir una estrategia voraz que determine en qué orden ejecutar los
     * procesos y demostrar que es óptima.
     */
    public static class Ejercicio112 {
        #region Algoritmo de vuelta atrás
        static int[] PlanifBack(int[] lista, int i, int tam,
                                bool[] marcado, int[] temp, ref int min) {
            if(i < tam) {
                int[] result = null;
                int[] aux = null;
                for(int j = 0; j < tam; j++) {
                    if(!marcado[j]) {
                        temp[i] = lista[j];
                        marcado[j] = true;
                        aux = PlanifBack(lista, i + 1, tam, marcado,
                                         temp, ref min);
                        marcado[j] = false;
                        if(aux != null) {
                            result = aux;
                        }
                    }
                }
                return result;
            } else {
                int sum = Sumatorio(temp);
                if(min > sum) {
                    min = sum;
                    return Enumerable.Range(0, tam)
                                     .Select(x => temp[x])
                                     .ToArray();
                } else {
                    return null;
                }
            }
        }

        static int[] PlanifBack(int[] lista) {
            int[] temp = Enumerable.Range(0, lista.Length)
                                   .Select(x => -1)
                                   .ToArray();
            bool[] marcado = Enumerable.Range(0, lista.Length)
                                       .Select(x => false)
                                       .ToArray();
            int min = int.MaxValue;
            return PlanifBack(lista, 0, lista.Length, marcado,
                              temp, ref min);
        }
        #endregion


        #region Algoritmo voraz
        private static int Menor(int[] lista, bool[] usado) {
            int min = int.MaxValue;
            int j = -1;
            for(int i = 0; i < lista.Length; i++) {
                if(!usado[i] && min > lista[i]) {
                    min = lista[i]; j = i;
                }
            }
            if(0 <= j && j < lista.Length) {
                usado[j] = true;
            }
            return min;
        }
        
        static int[] Planificación(int[] lista) {
            int[] result = new int[lista.Length];
            bool[] usado = Enumerable.Range(0, lista.Length)
                                     .Select(x => false)
                                     .ToArray();

            for(int i = 0; i < lista.Length; i++) {
                result[i] = Menor(lista, usado);
            }

            return result;
        }
        #endregion


        #region Función objetivo
        static int Sumatorio(int[] lista) {
            int r = 0;
            for(int i = 0; i < lista.Length; i++) {
                r += (lista.Length - i) * lista[i];
            }
            return r;
        }
        #endregion


        public static void Mostrar(int[] lista, int[] result) {
            Console.WriteLine("Procesos: " +
                              lista.Select(x => x.ToString())
                                   .Aggregate((x, xs) => x + " " + xs));
            if(result != null) {
                Console.WriteLine("Orden: " +
                                  result.Select(x => x.ToString())
                                        .Aggregate((x, xs) => x + " " + xs));
                Console.WriteLine("Suma: {0}", Sumatorio(result));
            } else {
                Console.WriteLine("Sin solución...");
            }
            Console.WriteLine();
        }

        public static void Resolver() {
            int[] lista = { 1, 2, 1, 3, 2, 3, 4, 10, 4, 8 };
            int[] result = PlanifBack(lista);
            Mostrar(lista, result);

            //lista.OrderBy(x => x).ToArray();
            int[] result2 = Planificación(lista);
            Mostrar(lista, result2);
        }
    }
}
