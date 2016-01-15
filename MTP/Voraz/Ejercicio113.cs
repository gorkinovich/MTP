using System;
using System.Collections.Generic;
using System.Linq;

namespace MTP.Voraz {
    /*
     * Las emisiones de los automóviles privados son una de las principales fuentes de contaminación
     * en las grandes ciudades. Las autoridades universitarias han decidido contribuir seriamente a
     * la mejora de la calidad del aire prohibiendo el paso de automóviles por el campus y creando
     * una serie de líneas de autobuses eléctricos que permitan acceder a distintos puntos de la
     * Ciudad Universitaria.
     * 
     * Para ello se han estudiado los principales flujos de vehículos entre distintos puntos
     * seleccionados (facultades, estación de metro, etc.) de la Ciudad Universitaria. Los
     * resultados del estudio contienen el número de vehículos que transitan al día entre estos
     * puntos seleccionados (sin tener en cuenta el sentido en el que circulan). El objetivo de
     * este estudio es localizar los trayectos entre puntos seleccionados que utilizan más
     * vehículos y que permiten conectar todos los puntos seleccionados.
     * 
     * Diseña mediante el método voraz un algoritmo que proporcione estos trayectos. Demuestra
     * que la solución que propones es óptima (utiliza los teoremas vistos en clase si lo crees
     * necesario).
     */
    public static class Ejercicio113 {
        #region Constantes y tipos
        const int I = int.MaxValue; // Infinito

        struct Arista {
            public int origen;
            public int destino;
            public int coste;

            public Arista(int o, int d, int c) {
                this.origen = o;
                this.destino = d;
                this.coste = c;
            }

            public override string ToString() {
                char aux = 'A';
                return "(" + (char)(aux + origen) + ", " +
                       (char)(aux + destino) + "; " +
                       coste.ToString() + ")";
            }
        }
        #endregion


        #region Kruskal
        static bool NoConectados(Arista a, int[] cc) {
            return cc[a.origen] != cc[a.destino];
        }

        static void Conectar(Arista a, int[] cc, int tam) {
            int identif = cc[a.origen];
            int víctima = cc[a.destino];
            for(int i = 0; i < tam; i++) {
                if(cc[i] == víctima) {
                    cc[i] = identif;
                }
            }
        }

        static bool Solución(int[] cc, int tam) {
            for(int i = 1; i < tam; i++) {
                if(cc[0] != cc[i]) return false;
            }
            return true;
        }

        static List<Arista> CalcKruskal(IEnumerable<Arista> lista, int tam) {
            List<Arista> arm = new List<Arista>();
            int[] cc = Enumerable.Range(0, tam).ToArray();

            foreach(Arista a in lista) {
                if(NoConectados(a, cc)) {
                    arm.Add(a);
                    Conectar(a, cc, tam);
                    if(Solución(cc, tam)) {
                        return arm;
                    }
                }
            }

            return arm;
        }
        #endregion


        static IEnumerable<Arista> ObtenerLista(int[,] grafo, int tam) {
            List<Arista> lista = new List<Arista>();

            for(int i = 0; i < tam; i++) {
                for(int j = i + 1; j < tam; j++) {
                    if(grafo[i, j] != I) {
                        lista.Add(new Arista(i, j, grafo[i, j]));
                    }
                }
            }

            return lista.OrderBy(x => x.coste);
        }

        public static void Resolver() {
            const int numNodos = 7;
            int[,] grafo =
            { // A  B  C   D   E   F   G
                {I, 7, I,  5,  I,  I,  I}, // A
                {7, I, 8,  9,  7,  I,  I}, // B
                {I, 8, I,  I,  5,  I,  I}, // C
                {5, 9, I,  I, 15,  6,  I}, // D
                {I, 7, 5, 15,  I,  8,  9}, // E
                {I, I, I,  6,  8,  I, 11}, // F
                {I, I, I,  I,  9, 11,  I}  // G
            };

            IEnumerable<Arista> lista = ObtenerLista(grafo, numNodos);
            List<Arista> arm = CalcKruskal(lista, numNodos);
            Console.WriteLine("Tramos: " + arm.Select(x => x.ToString())
                                              .Aggregate((x, xs) => x + " " + xs));
        }
    }
}
