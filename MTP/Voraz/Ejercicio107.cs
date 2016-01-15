using System;
using System.Collections.Generic;
using System.Linq;

namespace MTP.Voraz {
    /*
     * En un lejano país los estudios de neuromagnetismo capilar están organizados de la siguiente
     * manera: existen N asignaturas, y cada asignatura proporciona una cualificación y un título,
     * es decir, no es necesario aprobar todas las asignaturas.
     * 
     * La relación entre las asignaturas es la siguiente: para dos asignaturas, A y B, puede
     * existir una relación de suficiencia de A hacia B si para matricularse en B es suficiente
     * haber aprobado A; o bien, pueden ser independientes: el aprobar A no da derecho a matricularse
     * en B. Son conocidas las relaciones de suficiencia entre las asignaturas. Diseña un algoritmo,
     * total o parcialmente voraz, que determine el mínimo número de años necesario para aprobar una
     * asignatura dada.
     */
    public static class Ejercicio107 {
        struct Relación {
            public Relación(int a, int n) {
                this.asignatura = a;
                this.necesita = n;
            }
            public int asignatura;
            public int necesita;
        }

        static IEnumerable<Relación> Conjunto(Relación[] lista, int v) {
            return lista.Where(x => x.asignatura == v);
        }

        static int NumAños(Relación[] lista, int N, int víctima) {
            LinkedList<Relación> cola = new LinkedList<Relación>();
            int[] veces = Enumerable.Range(0, N).Select(x => 0).ToArray();
            Relación aux;

            foreach(Relación x in Conjunto(lista, víctima)) {
                cola.AddLast(x);
            }
            veces[víctima - 1] = 1;
            //Console.WriteLine("V: " + veces.Select(x => x.ToString())
            //                               .Aggregate((x, xs) => x + " " + xs));

            while(cola.Count() > 0) {
                aux = cola.First();
                cola.RemoveFirst();
                veces[aux.necesita - 1] = veces[aux.asignatura - 1] + 1;
                //Console.WriteLine("Asig: " + aux.asignatura + " Nece: " + aux.necesita);
                //Console.WriteLine("V: " + veces.Select(x => x.ToString())
                //                               .Aggregate((x, xs) => x + " " + xs));
                foreach(Relación x in Conjunto(lista, aux.necesita)) {
                    cola.AddLast(x);
                }
            }

            return veces.Max();
        }

        public static void Resolver() {
            const int N = 10;
            Relación[] lista =
            {
                new Relación(10, 9), new Relación(10, 8),
                new Relación(9, 7), new Relación(8, 6),
                new Relación(7, 6), new Relación(7, 5),
                new Relación(4, 3), new Relación(4, 2),
                new Relación(3, 1), new Relación(2, 1)
            };

            //Console.WriteLine("Asignatura {0} = {1} años", 10,
            //                  NumAños(lista, N, 10));

            for(int i = 1; i <= N; i++) {
                Console.WriteLine("Asignatura {0} = {1} años", i,
                                  NumAños(lista, N, i));
            }
        }
    }
}
