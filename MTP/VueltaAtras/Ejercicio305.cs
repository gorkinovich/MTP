using System;
using System.Collections.Generic;
using System.Linq;

namespace MTP.VueltaAtras {
    /* 
     * Nicanor Cienfuegos tienes dos grandes pasiones: su pueblo y los kioscos. Tal es su pasión por su
     * pueblo que conoce perfectamente todas sus calles (con sus longitudes y sus intersecciones entre
     * ellas). Él vive en la intersección de dos calles y quiere ir a casa de un amigo que también vive
     * en la intersección de dos calles. Aunque podría ir directamente prefiere dar una vuelta y ojear
     * los kioscos. Sin embargo, como nunca compra nada desea pasar por cada kiosco como mucho una vez.
     * Nicanor sabe que los kioscos, en ese pueblo, solamente pueden estar en las intersecciones de dos
     * calles y quiere visitarlos todos.
     * + Establecer las variables y/o tipos de variables que permitan representar la información que
     *   conoce Nicanor y el árbol de búsqueda.
     * + Establecer el camino más corto desde casa de Nicanor a casa del amigo que pase exactamente una
     *   vez por cada kiosco mediante un algoritmo de vuelta atrás iterativo.
     */
    public static class Ejercicio305 {
        const int I = int.MaxValue; // Infinito
        
        static void Calcular(int[,] grafo, int[] r, int N, int o, int d, int i, ref int min, int auxv, int[] aux) {
            if(aux[o] == 0) {
                aux[o] = i + 1;
                if(o != d) {
                    for(int j = 0; j < N; j++) {
                        if(grafo[o, j] != I && aux[j] == 0) {
                            Calcular(grafo, r, N, j, d, i + 1, ref min, auxv + grafo[o, j], aux);
                        }
                    }
                } else if(aux[d] == N && auxv < min) {
                    min = auxv;
                    for(int j = 0; j < N; j++) {
                        r[j] = aux[j];
                    }
                }
                aux[o] = 0;
            }
        }

        static int[] Calcular(int[,] grafo, int N, int o, int d) {
            int[] r = Enumerable.Range(0, N).Select(x => 0).ToArray();
            int[] aux = Enumerable.Range(0, N).Select(x => 0).ToArray();
            int min = int.MaxValue;
            Calcular(grafo, r, N, o, d, 0, ref min, 0, aux);
            return r;
        }

        static void Resolver(int[,] grafo, int N, int o, int d) {
            int[] r = Calcular(grafo, N, o, d);

            char aux = 'A';
            Console.WriteLine((char)(aux + o) + " -> " + (char)(aux + d));
            Console.WriteLine(r.Select(x => (aux++) + " " + x.ToString())
                               .Aggregate((x, xs) => x + ", " + xs));
        }

        public static void Resolver() {
            const int N = 7;
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

            //Resolver(grafo, N, 0, 6);
            for(int i = 0; i < N; i++) {
                for(int j = i + 1; j < N; j++) {
                    Resolver(grafo, N, i, j);
                }
            }
        }
    }
}
