using System;
using System.Collections.Generic;
using System.Linq;

namespace MTP.VueltaAtras {
    /* 
     * El laberinto
     * Una matriz bidimensional NxN puede representar un laberinto cuadrado. Cada posición contiene
     * un entero no negativo que indica si la casilla es transitable (0) o si no lo es (∞). Las
     * casillas (1, 1) y (n, n) corresponden a la entrada y salida del laberinto y siempre son
     * transitables.
     * 
     * Dada una matriz con un laberinto, el problema consiste en diseñar un algoritmo que encuentre
     * un camino, si existe, para ir de la entrada a la salida.
     * 
     * Por ejemplo, podemos considerar el siguiente laberinto:
     *     1   2   3   4   5
     *   |---|---|---|---|---|
     * 1 | E |   |   |   |   |
     *   |---|---|---|---|---|
     * 2 |   | ∞ | ∞ | ∞ |   |
     *   |---|---|---|---|---|
     * 3 |   |   |   |   |   |
     *   |---|---|---|---|---|
     * 4 |   | ∞ | ∞ | ∞ | ∞ |
     *   |---|---|---|---|---|
     * 5 |   |   |   |   | S |
     *   |---|---|---|---|---|
     *   
     * + ¿Cómo se generan los movimientos en el laberinto?
     * + ¿Cuál es el camino que va a seleccionar el algoritmo?
     */
    public static class Ejercicio307 {
        const int I = int.MaxValue; // Infinito

        struct Punto {
            public Punto(int f, int c) {
                this.f = f;
                this.c = c;
            }
            public int f;
            public int c;
        }

        static bool Camino(int[,] mapa, int N, int M, int x, int y,
                           LinkedList<Punto> r) {
            if(0 <= x && x < N && 0 <= y && y < M && mapa[y, x] == 0) {
                r.AddLast(new Punto(y, x));
                if(x == N - 1 && y == M - 1) {
                    return true;
                } else {
                    mapa[y, x] = 1;
                    if(Camino(mapa, N, M, x, y + 1, r)) return true;
                    if(Camino(mapa, N, M, x + 1, y, r)) return true;
                    if(Camino(mapa, N, M, x, y - 1, r)) return true;
                    if(Camino(mapa, N, M, x - 1, y, r)) return true;
                    mapa[y, x] = 0;
                    r.RemoveLast();
                    return false;
                }
            } else {
                return false;
            }
        }

        static Punto[] Camino(int[,] mapa, int N, int M) {
            LinkedList<Punto> r = new LinkedList<Punto>();
            if(Camino(mapa, N, M, 0, 0, r)) {
                return r.ToArray();
            } else {
                return null;
            }
        }

        static void Mostrar(int[,] mapa, int N, int M) {
            for(int j = 0; j < N + 1; j++) {
                Console.Write("***");
            }
            Console.WriteLine();
            for(int i = 0; i < M; i++) {
                Console.Write("* ");
                for(int j = 0; j < N; j++) {
                    if(mapa[i, j] == I) {
                        Console.Write("***");
                    } else {
                        Console.Write("   ");
                    }
                }
                Console.WriteLine("*");
            }
            for(int j = 0; j < N + 1; j++) {
                Console.Write("***");
            }
            Console.WriteLine("\n");
        }

        static void GotoXY(int x, int y) {
            Console.CursorLeft = x;
            Console.CursorTop = y;
        }

        static void Mostrar(Punto[] r, int Alto) {
            int n = 1;
            foreach(Punto p in r) {
                GotoXY(2 + p.c * 3, 1 + p.f);
                if(n < 10) {
                    Console.Write(n.ToString(" 0"));
                } else {
                    Console.Write(n.ToString("00"));
                }
                n++;
            }
            GotoXY(0, Alto + 3);
        }

        public static void Resolver() {
            const int Ancho = 5;
            const int Alto = 5;
            int[,] mapa =
            {
                { 0, 0, 0, 0, 0 },
                { 0, I, I, I, 0 },
                { 0, 0, 0, 0, 0 },
                { 0, I, I, I, I },
                { 0, 0, 0, 0, 0 },
            };
            Punto[] r = Camino(mapa, Ancho, Alto);
            Mostrar(mapa, Ancho, Alto);
            Mostrar(r, Alto);
        }
    }
}
