using System;
using System.Collections.Generic;
using System.Linq;

namespace MTP.Voraz {
    /* 
     * Ejercicio 1:
     * Sean dos vectores A y B de n números naturales cada uno, el primero de los cuales, A, esta
     * ordenado de forma creciente. Implementa un algoritmo voraz que obtenga una reordenación del
     * vector B tal que maximice la suma de los elementos de A que no superan al elemento
     * correspondiente de B, es decir, maximiza: S = Sum(i=1..n) { A(i), si A(i)<=B(i) }
     * Ejemplo: A={1,4,6,7,9}, B={3,2,4,6,5} -> B={4,5,6,3,2}, S=11.
     */
    public static class Ejercicio101 {
        private static int[] Reordenación(int[] la, int[] lb) {
            LinkedList<int> r = new LinkedList<int>();
            bool[] usados = Enumerable.Range(0, lb.Length)
                                      .Select(x => false)
                                      .ToArray();
            int vmin, vmax, imin, imax, i;

            foreach(int val in la.Reverse()) {
                vmax = int.MinValue; imax = -1;
                vmin = int.MaxValue; imin = -1;

                for(i = 0; i < lb.Length; i++) {
                    if(!usados[i]) {
                        if(lb[i] < vmin) {
                            vmin = lb[i];
                            imin = i;
                        }
                        if(lb[i] > vmax) {
                            vmax = lb[i];
                            imax = i;
                        }
                    }
                }

                if(val > vmax) {
                    r.AddFirst(vmin);
                    usados[imin] = true;
                } else {
                    r.AddFirst(vmax);
                    usados[imax] = true;
                }
            }

            return r.ToArray();
        }

        static int Suma(int[] la, int[] lb) {
            int s = 0;

            for(int i = 0; i < la.Length; i++) {
                if(la[i] <= lb[i]) s += la[i];
            }

            return s;
        }

        public static void Resolver() {
            int[] la = { 1, 3, 4, 6, 7, 9, 10, 12, 14, 16 };
            int[] lb = { 6, 5, 8, 3, 2, 7, 15, 11, 10, 14 };

            int[] r = Reordenación(la, lb);

            Console.WriteLine("A: " +
                              la.Select(x => x.ToString())
                                .Aggregate((x, xs) => x + " " + xs));
            Console.WriteLine("B: " +
                              lb.Select(x => x.ToString())
                                .Aggregate((x, xs) => x + " " + xs));
            Console.WriteLine("R: " +
                              r.Select(x => x.ToString())
                               .Aggregate((x, xs) => x + " " + xs));
            Console.WriteLine("Sum: {0}", Suma(la, r));
        }
    }
}
