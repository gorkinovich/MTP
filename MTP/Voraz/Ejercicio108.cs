using System;
using System.Collections.Generic;
using System.Linq;

namespace MTP.Voraz {
    /*
     * Consideremos un pueblo en el que conocemos completamente su mapa: conocemos todas las
     * intersecciones de las calles y todas sus longitudes. Supongamos que en cada intersección
     * de calles existe una plaza. El alcalde de este pueblo ha decidido asfaltar las calles.
     * Sin embargo, como el presupuesto es reducido ha decidido asfaltar aquellos tramos de calle
     * de tal forma que todas las plazas queden unidas por tramos de calle asfaltadas. Determina
     * qué tramos de calle son necesarios asfaltar para resolver el problema con un coste mínimo.
     * Nota: Podemos suponer que el gasto por unidad de longitud es g.
     */
    public static class Ejercicio108 {
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
        

        #region Prim normal
        static List<Arista> CalcPrim(int[,] grafo, int tam) {
            int[] costemín = Enumerable.Range(0, tam).Select(x => grafo[0, x]).ToArray();
            int[] conexión = Enumerable.Range(0, tam).Select(x => 0).ToArray();
            List<Arista> arm = new List<Arista>();
            Arista aux;
            int mínimo, elegido = 0;

            for(int i = 1; i < tam; i++) {
                mínimo = I;
                for(int j = 1; j < tam; j++) {
                    if(0 <= costemín[j] && costemín[j] < mínimo) {
                        mínimo = costemín[j]; elegido = j;
                    }
                }

                aux = new Arista();
                aux.origen = conexión[elegido];
                aux.destino = elegido;
                aux.coste = grafo[conexión[elegido], elegido];
                arm.Add(aux);
                costemín[elegido] = -1;

                for(int j = 1; j < tam; j++) {
                    if(grafo[elegido, j] < costemín[j]) {
                        costemín[j] = grafo[elegido, j];
                        conexión[j] = elegido;
                    }
                }
            }

            return arm;
        }
        #endregion


        #region Prim variación
        static List<Arista> Transformar(int[,] grafo, int[] conexión) {
            List<Arista> arm = new List<Arista>();
            Arista aux;
            for(int i = 1; i < conexión.Length; i++) {
                aux = new Arista();
                aux.origen = conexión[i];
                aux.destino = i;
                aux.coste = grafo[conexión[i], i];
                arm.Add(aux);
            }
            return arm;
        }

        static List<Arista> CalcPrim2(int[,] grafo, int tam) {
            int[] costemín = Enumerable.Range(0, tam).Select(x => grafo[0, x]).ToArray();
            int[] conexión = Enumerable.Range(0, tam).Select(x => 0).ToArray();
            int mínimo, elegido = 0;

            for(int i = 1; i < tam; i++) {
                mínimo = I;
                for(int j = 1; j < tam; j++) {
                    if(0 <= costemín[j] && costemín[j] < mínimo) {
                        mínimo = costemín[j]; elegido = j;
                    }
                }

                costemín[elegido] = -1;

                for(int j = 1; j < tam; j++) {
                    if(grafo[elegido, j] < costemín[j]) {
                        costemín[j] = grafo[elegido, j];
                        conexión[j] = elegido;
                    }
                }
            }

            return Transformar(grafo, conexión);
        }
        #endregion


        public static void Resolver() {
            const int numNodos = 7;
            const int g = 10;
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

            List<Arista> arm = CalcPrim(grafo, numNodos);
            Console.WriteLine("Tramos: " + arm.Select(x => x.ToString())
                                              .Aggregate((x, xs) => x + " " + xs));
            Console.WriteLine("Coste:  " + arm.Select(x => x.coste * g).Sum());
            Console.WriteLine();

            arm = CalcPrim2(grafo, numNodos);
            Console.WriteLine("Tramos: " + arm.Select(x => x.ToString())
                                              .Aggregate((x, xs) => x + " " + xs));
            Console.WriteLine("Coste:  " + arm.Select(x => x.coste * g).Sum());
            Console.WriteLine();
        }
    }
}
