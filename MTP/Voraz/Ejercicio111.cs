using System;
using System.Collections.Generic;
using System.Linq;

namespace MTP.Voraz {
    /*
     * La Universidad tiene que planificar un evento cultural que consiste en n conferencias. Para
     * cada conferencia se conoce la hora de comienzo y la de finalización fijadas por los ponentes.
     * Se ha pedido al Departamento de Informática que planifique las n conferencias distribuyéndolas
     * entre las distintas salas disponibles, de forma que no haya dos conferencias en una misma sala
     * al mismo tiempo. El objetivo es minimizar el número de salas utilizadas, para así causar el
     * menor trastorno al resto de las actividades académicas.
     */
    public static class Ejercicio111 {
        struct Conferencia {
            public Conferencia(int a, int h, int d) {
                this.aula = a;
                this.hora = h;
                this.duración = d;
            }
            public int aula;
            public int hora;
            public int duración;
        }

        static Conferencia[] Planificar(Conferencia[] lista) {
            List<Conferencia> result = new List<Conferencia>();
            int[] mins = Enumerable.Range(0, lista.Length)
                                   .Select(x => 0)
                                   .ToArray();
            int n = 1, imin, vmin;
            Conferencia aux;

            foreach(Conferencia c in lista.OrderBy(x => x.hora)) {
                aux = c; imin = -1; vmin = int.MaxValue;

                for(int i = 0; i < n; i++) {
                    if(mins[i] <= aux.hora && vmin > aux.hora - mins[i]) {
                        imin = i;
                        vmin = aux.hora - mins[i];
                    }
                }

                if(imin == -1) {
                    imin = n;
                    n++;
                }

                mins[imin] = aux.hora + aux.duración;
                aux.aula = imin;
                result.Add(aux);
            }

            return result.ToArray();
        }

        public static void Resolver() {
            Conferencia[] lista =
            {
                new Conferencia(-1, 0, 60), new Conferencia(-1, 30, 40),
                new Conferencia(-1, 60, 50), new Conferencia(-1, 70, 50),
                new Conferencia(-1, 110, 40), new Conferencia(-1, 120, 40),
                new Conferencia(-1, 150, 30), new Conferencia(-1, 160, 50),
                new Conferencia(-1, 180, 60), new Conferencia(-1, 210, 30),
                new Conferencia(-1, 10, 50), new Conferencia(-1, 20, 50),
                new Conferencia(-1, 60, 40), new Conferencia(-1, 70, 60),
                new Conferencia(-1, 100, 50), new Conferencia(-1, 130, 40),
                new Conferencia(-1, 150, 50), new Conferencia(-1, 170, 40),
                new Conferencia(-1, 200, 40), new Conferencia(-1, 210, 30)
            };

            Conferencia[] result = Planificar(lista);
            foreach(Conferencia c in result.OrderBy(x => x.aula)) {
                Console.WriteLine("Aula {0}\t hora {1} \tduración {2} -> {3}",
                                  c.aula, c.hora, c.duración, c.hora + c.duración);
            }
        }
    }
}
