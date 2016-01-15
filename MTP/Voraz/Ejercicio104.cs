using System;
using System.Collections.Generic;
using System.Linq;

namespace MTP.Voraz {
    /*
     * Ejercicio 4:
     * Un par de ladrones quieren robar en una mansión en la que se puede encontrar n objetos de
     * valor. El plan es que solo uno de ellos entre en la mansión mientras que el otro espera
     * fuera con un furgón en marcha para acelerar la huída. Los ladrones saben que solo disponen
     * de un tiempo T minutos para cargar objetos en el furgón antes de que la policía llegue a la
     * mansión, así que han de elegir muy bien los objetos a cargar. Teniendo en cuenta que los
     * ladrones conocen el valor v de los diferentes objetos y el tiempo t que emplean en cargar
     * cada uno de ellos en el camión, diseña un algoritmo que maximice el beneficio de la operación
     * sin ser detenidos. Demuestra la optimalidad del algoritmo o encuentra un contraejemplo.
     */
    public static class Ejercicio104 {
        struct Objeto {
            public int valor;
            public int tiempo;

            public Objeto(int v, int t) {
                this.valor = v;
                this.tiempo = t;
            }

            public override string ToString() {
                return "(" + this.valor + "; " + this.tiempo + ")";
            }

            public float Proporción {
                get { return (float)valor / (float)tiempo; }
            }
        }

        static Objeto[] Calcular(Objeto[] lista, int tiempo) {
            List<Objeto> r = new List<Objeto>();
            List<Objeto> l = lista.OrderByDescending(x => x.Proporción)
                                  .ToList();

            foreach(Objeto o in l) {
                if(tiempo >= o.tiempo) {
                    r.Add(o);
                    tiempo -= o.tiempo;
                    if(tiempo <= 0) {
                        return r.ToArray();
                    }
                }
            }

            return r.ToArray();
        }

        public static void Resolver() {
            int tiempo = 20;
            Objeto[] lista =
            {
                new Objeto(14, 10), new Objeto(12, 8), new Objeto(1, 4),
                new Objeto(14, 10), new Objeto(12, 8), new Objeto(2, 4),
                new Objeto(10, 10), new Objeto(10, 8), new Objeto(1, 4)
            };

            Objeto[] result = Calcular(lista, tiempo);
            Console.WriteLine("Resultado: " + 
                              result.Select(x => x.ToString())
                                    .Aggregate((x, xs) => x + " " + xs));
            Console.WriteLine("Suma: " + result.Select(x => x.valor)
                                               .Sum());
        }
    }
}
