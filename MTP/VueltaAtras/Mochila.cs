using System;
using System.Collections.Generic;
using System.Linq;

namespace MTP.VueltaAtras {
    public static class Mochila03 {
        static void Calcular(int[] v, int[] t, int[] c, int N, int lim,
                             int auxp, int auxv, int[] aux, ref int max,
                             int[] r) {
            for(int i = 0; i < N; i++) {
                if(aux[i] < c[i] && auxp + t[i] <= lim) {
                    aux[i]++;
                    Calcular(v, t, c, N, lim, auxp + t[i],
                             auxv + v[i], aux, ref max, r);
                    aux[i]--;
                }
            }
            if(auxv > max) {
                for(int i = 0; i < N; i++) {
                    r[i] = aux[i];
                }
                max = auxv;
            }
        }

        static int[] Calcular(int[] v, int[] t, int[] c, int N, int lim) {
            int[] r = Enumerable.Range(0, N).Select(x => 0).ToArray();
            int[] aux = Enumerable.Range(0, N).Select(x => 0).ToArray();
            int max = 0;
            Calcular(v, t, c, N, lim, 0, 0, aux, ref max, r);
            return r;
        }

        public static void Resolver() {
            const int N = 7;
            int[] valor = { 1, 2, 10, 12, 10, 14, 10 };
            int[] tamaño = { 4, 4, 8, 8, 10, 10, 15 };
            int[] cantidad = { 2, 1, 1, 2, 1, 2, 1 };
            int[] r = null;

            Listar("Valor:    ", valor);
            Listar("Tamaño:   ", tamaño);
            Listar("Cantidad: ", cantidad);
            Console.WriteLine();

            r = Calcular(valor, tamaño, cantidad, N, 18);
            Mostar(valor, tamaño, r);

            r = Calcular(valor, tamaño, cantidad, N, 20);
            Mostar(valor, tamaño, r);

            r = Calcular(valor, tamaño, cantidad, N, 42);
            Mostar(valor, tamaño, r);
        }

        static void Mostar(int[] v, int[] t, int[] r) {
            int suma = 0;
            string aux;
            Console.Write("Resultado: ");
            for(int i = 0; i < r.Length; i++) {
                suma += v[i] * r[i];
                aux = "(" + v[i] + "; " + t[i] + ") ";
                for(int j = 0; j < r[i]; j++) {
                    Console.Write(aux);
                }
            }
            Console.WriteLine("\nSuma: {0}\n", suma);
        }

        static void Listar(string c, int[] l) {
            Console.WriteLine("{0}{1}", c,
                              l.Select(x => x.ToString("00"))
                               .Aggregate((x, xs) => x + " " + xs));
        }
    }
}
