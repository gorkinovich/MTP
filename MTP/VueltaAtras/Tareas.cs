using System;
using System.Collections.Generic;
using System.Linq;

namespace MTP.VueltaAtras {
    public static class Tareas {
        static void Calcular(int[,] tabla, int[] r, int N, int j,
                             ref int min, int auxv, int[] aux) {
            if(j < N) {
                for(int i = 0; i < N; i++) {
                    if(auxv + tabla[i, j] < min) {
                        aux[j] = i;
                        Calcular(tabla, r, N, j + 1, ref min,
                                 auxv + tabla[i, j], aux);
                    }
                }
            } else if(min > auxv) {
                min = auxv;
                for(int i = 0; i < N; i++) {
                    r[i] = aux[i];
                }
            }
        }

        static int[] Calcular(int[,] tabla, int N) {
            int[] r = Enumerable.Range(0, N).Select(x => 0).ToArray();
            int[] aux = Enumerable.Range(0, N).Select(x => 0).ToArray();
            int max = int.MaxValue;
            Calcular(tabla, r, N, 0, ref max, 0, aux);
            return r;
        }

        public static void Resolver() {
            const int N = 5;
            int[,] tabla =
            {
                { 2, 2, 2, 2, 1 },
                { 2, 1, 2, 2, 2 },
                { 2, 2, 2, 1, 2 },
                { 2, 2, 1, 2, 2 },
                { 1, 2, 2, 2, 2 },
            };
            int[] r = Calcular(tabla, N);
            Console.WriteLine("Resultado: " +
                              r.Select(x => x.ToString())
                               .Aggregate((x, xs) => x + " " + xs));
            Console.WriteLine("Suma: " +
                              Enumerable.Range(0, N)
                                        .Select(x => tabla[r[x], x])
                                        .Sum());
        }
    }
}
