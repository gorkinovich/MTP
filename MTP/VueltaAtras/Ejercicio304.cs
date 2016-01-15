using System;
using System.Collections.Generic;
using System.Linq;

namespace MTP.VueltaAtras {
    /* 
     * La compañía discográfica NPI quiere sacar un LP con los grandes éxitos de uno de sus artistas principales.
     * Para ello dispone de M canciones a repartir entre las dos caras del LP. Se conocen tanto el tiempo de cada
     * canción como el tiempo de música que puede almacenar cada cara del LP. Se pide:
     * a) Encontrar mediante un algoritmo de vuelta atrás recursivo la composición de canciones del disco de tal
     *    forma que maximice el número de canciones.
     * b) Establecer el árbol de búsqueda (especificando el significado de las aristas y el contenido de cada nodo).
     * c) Indicar la primera llamada al procedimiento.
     */
    public static class Ejercicio304 {
        private static void Calcular(int[] l, int M, int lim, int i, LinkedList<int> r,
                                     ref int max, LinkedList<int> aux, int auxv, int auxt) {
            if(i < M) {
                if(auxt + l[i] <= lim) {
                    aux.AddLast(i);
                    Calcular(l, M, lim, i + 1, r, ref max, aux, auxv + 1, auxt + l[i]);
                    aux.RemoveLast();

                }
                Calcular(l, M, lim, i + 1, r, ref max, aux, auxv, auxt);
            } else if(auxv > max) {
                max = auxv;
                r.Clear();
                foreach(int n in aux) {
                    r.AddLast(n);
                }
            }
        }

        private static int[] Calcular(int[] l, int M, int lim) {
            LinkedList<int> r = new LinkedList<int>();
            LinkedList<int> aux = new LinkedList<int>();
            int max = 0;
            Calcular(l, M, lim, 0, r, ref max, aux, 0, 0);
            return r.ToArray();
        }

        public static void Resolver() {
            int[] l = { 1, 2, 3, 2, 3, 1, 2, 1, 3, 1, 2, 1 };
            int M = l.Length;
            int[] r = Calcular(l, M, 10);
            Console.WriteLine("Resultado: " + 
                              r.Select(x => x.ToString())
                               .Aggregate((x, xs) => x + " " + xs));
            Console.WriteLine("Tamaños: " +
                              r.Select(x => l[x].ToString())
                               .Aggregate((x, xs) => x + " " + xs));
            Console.WriteLine("Suma: " + r.Select(x => l[x])
                                          .Sum());
        }
    }
}
