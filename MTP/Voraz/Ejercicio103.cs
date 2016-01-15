using System;
using System.Collections.Generic;
using System.Linq;

namespace MTP.Voraz {
    /*
     * Ejercicio 3:
     * La Universidad tiene ha planificado un evento cultural que consiste en n conferencias que
     * se celebraran en 10 aulas diferentes. Cada conferencia se celebra una sola vez y se conoce
     * el aula donde se celebra, la hora de comienzo y su duración. Un alumno desea sacar el mayor
     * partido posible a este evento cultural, teniendo en cuenta que el único objetivo es asistir
     * al máximo número posible de conferencias, desarrolla un algoritmo voraz que solucione el
     * problema, indicando su complejidad.
     */
    public static class Ejercicio103 {
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
            List<Conferencia> r = new List<Conferencia>();
            List<Conferencia> aux = null;
            Conferencia candidato;
            int hora = 0;

            aux = lista.Where(x => x.hora >= hora)
                       .OrderBy(x => x.hora + x.duración)
                       .ToList();

            while(aux.Count() > 0) {
                candidato = aux.First();
                r.Add(candidato);
                hora = candidato.hora + candidato.duración;
                aux = aux.Where(x => x.hora >= hora).ToList();
            }

            return r.ToArray();
        }

        public static void Resolver() {
            Conferencia[] lista =
            {
                new Conferencia(1, 0, 60),
                new Conferencia(1, 60, 60),
                new Conferencia(1, 120, 60),
                new Conferencia(1, 180, 60),
                new Conferencia(1, 240, 60),

                new Conferencia(2, 10, 30),
                new Conferencia(2, 45, 30),
                new Conferencia(2, 80, 30),
                new Conferencia(2, 115, 45),
                new Conferencia(2, 165, 45),
                new Conferencia(2, 215, 45),

                new Conferencia(3, 0, 30),
                new Conferencia(3, 60, 90),
                new Conferencia(3, 160, 60),
                new Conferencia(3, 240, 40),
                
                new Conferencia(4, 0, 45),
                new Conferencia(4, 60, 30),
                
                new Conferencia(5, 20, 60),
                new Conferencia(5, 90, 30),
                
                new Conferencia(6, 120, 30),
                new Conferencia(6, 160, 60),
                new Conferencia(6, 230, 40),
                
                new Conferencia(7, 150, 30),
                new Conferencia(7, 180, 50),
                new Conferencia(7, 230, 40),
                
                new Conferencia(8, 0, 60),
                new Conferencia(8, 90, 90),
                new Conferencia(8, 190, 40),
                new Conferencia(8, 240, 30),
                
                new Conferencia(9, 0, 50),
                new Conferencia(9, 60, 50),
                new Conferencia(9, 120, 90),
                new Conferencia(9, 230, 30),
                new Conferencia(9, 260, 30),
                
                new Conferencia(10, 0, 90),
                new Conferencia(10, 90, 90),
                new Conferencia(10, 180, 90)
            };

            Conferencia[] result = Planificar(lista);
            foreach(Conferencia i in result) {
                Console.WriteLine("Aula {0} hora {1} duración {2}",
                                  i.aula, i.hora, i.duración);
            }
        }
    }
}
