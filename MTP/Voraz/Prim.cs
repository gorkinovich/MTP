using System;
using System.Collections.Generic;
using System.Linq;

namespace MTP.Voraz {
    /*
     * http://es.wikipedia.org/wiki/Algoritmo_de_Prim
     */
    public static class Prim {
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
        

        #region Prim no eficiente
        static Arista Menor(int[,] grafo, bool[] nodos, int tam) {
            int coste = I;
            Arista result = new Arista(-1, -1, 0);

            for(int i = 0; i < tam; i++) {
                if(nodos[i]) {
                    for(int j = 0; j < tam; j++) {
                        if(!nodos[j] && grafo[i, j] < coste) {
                            coste = grafo[i, j];
                            result = new Arista(i, j, coste);
                        }
                    }
                }
            }
            return result;
        }

        static List<Arista> CalcPrim2(int[,] grafo, int tam) {
            bool[] nodos = Enumerable.Range(0, tam).Select(x => false).ToArray();
            List<Arista> arm = new List<Arista>();
            Arista aux;
            nodos[0] = true;

            for(int i = 1; i < tam; i++) {
                aux = Menor(grafo, nodos, tam);
                if(aux.destino > 0) {
                    arm.Add(aux);
                    nodos[aux.destino] = true;
                }
            }

            return arm;
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

            List<Arista> arm = CalcPrim(grafo, numNodos);
            Console.WriteLine("ARM: " + arm.Select(x => x.ToString())
                                           .Aggregate((x, xs) => x + " " + xs));
        }
    }
}
