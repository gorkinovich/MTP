using System;
using System.Collections.Generic;
using System.Linq;

namespace MTP.VueltaAtras {
    /* 
     * En un tablero de ajedrez de dimensiones NxN consideramos el siguiente juego. En el tablero hay
     * colocados peones blancos y negros. Partiendo de una posición inicial y realizando movimientos
     * válidos deseamos saltar todos los peones blancos de acuerdo con las siguientes reglas:
     * + Sólo se permiten movimientos en cruz.
     * + Tipos de movimientos posibles:
     *   - Movimiento a una casilla vacía. Coste en longitud: 1.
     *   - Salto de un peón blanco. Coste en longitud: 2.
     *   - No se pueden saltar peones negros.
     * 
     * Diseñad un algoritmo de vuelta atrás que determine si dada una posición inicial y un número
     * máximo de movimientos es posible saltar todos los peones blancos. Es preciso detallar lo siguiente:
     * + El árbol de búsqueda utilizado en el algoritmo (1 punto).
     * + La primera llamada (1 punto).
     * + El algoritmo completo (3 puntos).
     */
    public static class Ejercicio303 {
        struct Punto {
            public Punto(int f, int c) {
                this.f = f;
                this.c = c;
            }
            public int f;
            public int c;
        }

        static bool Comprobar(int N, int f, int c) {
            return 0 <= f && f < N && 0 <= c && c < N;
        }

        static void Mover(char[,] mapa, int N, int f, int c, int j,
                          int k, out int f2, out int c2, out int j2) {
            int[] finc = { -1, 0, 1, 0 };
            int[] cinc = { 0, 1, 0, -1 };

            f2 = f + finc[k];
            c2 = c + cinc[k];
            j2 = j;

            if(Comprobar(N, f2, c2) && mapa[f2, c2] == 'B') {
                f2 += finc[k];
                c2 += cinc[k];
                j2--;
            }
        }
        
        static bool Calcular(char[,] mapa, int N, int f, int c, int M,
                             int i, int j, LinkedList<Punto> r) {
            if(i < M && Comprobar(N, f, c) && mapa[f, c] == ' ') {
                mapa[f, c] = '*';
                r.AddLast(new Punto(f, c));
                if(j > 0) {
                    int f2, c2, j2;
                    for(int k = 0; k < 4; k++) {
                        Mover(mapa, N, f, c, j, k, out f2, out c2, out j2);
                        if(Calcular(mapa, N, f2, c2, M, i + 1, j2, r)) {
                            return true;
                        }
                    }
                } else {
                    return true;
                }
                r.RemoveLast();
                mapa[f, c] = ' ';
            }
            return false;
        }

        static Punto[] Calcular(char[,] mapa, int N, int f, int c, int M) {
            LinkedList<Punto> r = new LinkedList<Punto>();
            char[,] aux = new char[N, N];
            int blancos = 0;

            for(int i = 0; i < N; i++) {
                for(int j = 0; j < N; j++) {
                    aux[i, j] = mapa[i, j];
                    if(mapa[i, j] == 'B') {
                        blancos++;
                    }
                }
            }

            if(Calcular(aux, N, f, c, M, 0, blancos, r)) {
                return r.ToArray();
            } else {
                return null;
            }
        }

        public static void Resolver() {
            const int N = 5;
            char[,] mapa =
            { //   0    1    2    3    4
                { ' ', ' ', ' ', 'N', ' ' }, // 0
                { 'B', ' ', 'B', 'N', 'B' }, // 1
                { ' ', 'N', ' ', 'B', ' ' }, // 2
                { 'B', 'N', 'N', 'N', ' ' }, // 3
                { ' ', ' ', 'B', ' ', ' ' }, // 4
            };
            Punto[] r = Calcular(mapa, N, 4, 4, 12);
            if(r == null) {
                Console.WriteLine("No hay solución...");
            } else {
                Console.WriteLine("Solución: " +
                                  r.Select(x => "(" + x.f + ", " + x.c + ")")
                                   .Aggregate((x, xs) => x + " " + xs));
            }
        }
    }
}
