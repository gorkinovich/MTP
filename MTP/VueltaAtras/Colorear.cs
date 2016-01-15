using System;
using System.Collections.Generic;
using System.Linq;

namespace MTP.VueltaAtras {
    public static class Colorear {
        static bool Comprobar(bool[,] grafo, int N, int[] aux) {
            for(int i = 0; i < N; i++) {
                for(int j = 0; j < N; j++) {
                    if(grafo[i, j] && aux[i] == aux[j]) {
                        return false;
                    }
                }
            }
            return true;
        }

        static void Calcular(bool[,] grafo, int[] r, int N, int j,
                             ref int min, int auxv, int[] aux) {
            if(j < N) {
                for(int i = 0; i < N; i++) {
                    aux[j] = i + 1;
                    Calcular(grafo, r, N, j + 1, ref min, aux.Max(), aux);
                }
            } else if(min > auxv && Comprobar(grafo, N, aux)) {
                min = auxv;
                for(int i = 0; i < N; i++) {
                    r[i] = aux[i];
                }
            }
        }

        static int[] Colorea(bool[,] grafo, int N) {
            int[] r = Enumerable.Range(0, N).Select(x => 0).ToArray();
            int[] aux = Enumerable.Range(0, N).Select(x => 0).ToArray();
            int min = int.MaxValue;
            Calcular(grafo, r, N, 0, ref min, 0, aux);
            return r;
        }

        public static void Resolver() {
            const int N = 7;
            bool[,] grafo =
            { //   A      B      C      D      E      F      G
                {false, true,  false, true,  false, false, false}, // A
                {true,  false, true,  true,  true,  false, false}, // B
                {false, true,  false, false, true,  false, false}, // C
                {true,  true,  false, false, true,  true,  false}, // D
                {false, true,  true,  true,  false, true,  true},  // E
                {false, false, false, true,  true,  false, true},  // F
                {false, false, false, false, true,  true,  false}  // G
            };

            int[] r = Colorea(grafo, N);

            char aux = 'A';
            Console.WriteLine(r.Select(x => (aux++) + " " + x.ToString())
                               .Aggregate((x, xs) => x + ", " + xs));
            Console.WriteLine("Nº de colores: " + r.Max());
        }
    }
}
