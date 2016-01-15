using System;
using System.Collections.Generic;
using System.Linq;

namespace MTP.Voraz {
    /*
     * Supongamos que disponemos de n ficheros f1, f2, ..., fn con tamaños l1, l2, ..., ln y un
     * disquete de capacidad d < l1+l2+...+ln.
     * 
     * a) Queremos maximizar el número de ficheros que ha de contener el disquete, y para eso
     *    ordenamos los ficheros por orden creciente de su tamaño y vamos metiendo ficheros en
     *    el disco hasta que no podamos meter más. Determinar si este algoritmo ávido encuentra
     *    solución óptima en todos los casos.
     * 
     * b) Queremos llenar el disquete tanto como podamos, y para eso ordenamos los ficheros por
     *    orden decreciente de su tamaño, y vamos metiendo ficheros en el disco hasta que no
     *    podamos meter más. Determinar si este algoritmo ávido encuentra solución óptima en
     *    todos los casos.
     */
    public static class Ejercicio110 {
        static int[] LlenarDisquete(int[] lista, int max) {
            List<int> r = new List<int>();
            int ocupación = 0;

            foreach(int n in lista) {
                if(ocupación + n <= max) {
                    ocupación += n;
                    r.Add(n);
                    if(ocupación >= max) {
                        return r.ToArray();
                    }
                }
            }

            return r.ToArray();
        }

        static int[] MaxNumFich(int[] lista, int max) {
            return LlenarDisquete(lista.OrderBy(x => x).ToArray(), max);
        }

        static int[] MaxOcupación(int[] lista, int max) {
            return LlenarDisquete(lista.OrderByDescending(x => x).ToArray(), max);
        }

        public static void Resolver() {
            const int max = 30;
            int[] lista = { 10, 15, 8, 2, 10 };
            int[] r1 = MaxNumFich(lista, max);
            int[] r2 = MaxOcupación(lista, max);

            Console.WriteLine("L: " + lista.Select(x => x.ToString())
                                           .Aggregate((x, xs) => x + " " + xs));
            Console.WriteLine("Max: {0}\n", max);

            Console.WriteLine("R1: " + r1.Select(x => x.ToString())
                                         .Aggregate((x, xs) => x + " " + xs));
            Console.WriteLine("Sum: {0}\n", r1.Sum());

            Console.WriteLine("R2: " + r2.Select(x => x.ToString())
                                         .Aggregate((x, xs) => x + " " + xs));
            Console.WriteLine("Sum: {0}\n", r2.Sum());
        }
    }
}
