using System;
using System.Collections.Generic;
using System.Linq;

namespace MTP.Ramificacion {
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
     * Diseñad un algoritmo de ramificación y poda que determine el mínimo número de movimientos
     * necesario para saltar todos los peones blancos. Es preciso detallar lo siguiente:
     * + El árbol de búsqueda utilizado en el algoritmo (1 punto).
     * + El algoritmo completo (3 puntos).
     * + El algoritmo de la función de cota utilizada(1 puntos).
     */
    public static class Ejercicio403 {
        /*
        fun Minimización (T : árbolDeEstados) dev <solución : tupla, coste : valor>
        var X, Y : nodo; C : colapr<nodo>
           Y := raíz(T)
           C := CpVacía()
           Añadir(C, Y)
           coste := ∞
           mientras ¬EsCpVacía(C) y Mínimo(C).CosteEstimado < coste hacer
              Y := Mínimo(C); EliminarMínimo(C)
              para todo hijo X de Y hacer
                 si EsSolución(X) entonces
                    si X.CosteReal < coste entonces
                       coste := X.CosteReal
                       solución := X.Solución
                    fsi
                 sino si EsCompletable(X) y X.CosteEstimado < coste entonces
                    Añadir(C, X)
                 fsi
              fpara
           fmientras
        ffun
        */

        //*****************************************************************

        const char A = 'A';
        const int I = int.MaxValue; // Infinito

        struct Datos {
            public int N;
            public char[,] mapa;
            public int x;
            public int y;
        }

        struct Nodo {
            public int etapa;
            public Punto[] solución;
            public char[,] mapa;
            public int blancos;
            public int Creal;
            public int Cestimado;
        }

        struct Resultado {
            public Punto[] solución;
            public int coste;
        }

        static int Min(int a, int b) {
            return (b < a) ? b : a;
        }

        static int Max(int a, int b) {
            return (b > a) ? b : a;
        }
        
        //*****************************************************************

        const int MaxMov = 4;

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

        static void Mover(Nodo Y, Datos d, int k, out Punto p, out int b) {
            int[] finc = { -1, 0, 1, 0 };
            int[] cinc = { 0, 1, 0, -1 };

            p = new Punto();
            p.f = Y.solución[Y.etapa].f + finc[k];
            p.c = Y.solución[Y.etapa].c + cinc[k];
            b = Y.blancos;

            if(Comprobar(d.N, p.f, p.c) && Y.mapa[p.f, p.c] == 'B') {
                p.f += finc[k];
                p.c += cinc[k];
                b--;
            }
        }

        //*****************************************************************

        static int CosteEstimado(Nodo X, Datos d) {
            return X.Creal + X.blancos;
        }

        static Nodo Raíz(Datos d) {
            Nodo nodo = new Nodo();
            nodo.etapa = 0;
            nodo.solución = new Punto[1];
            nodo.solución[0] = new Punto(d.y, d.x);
            nodo.blancos = 0;
            nodo.mapa = new char[d.N, d.N];
            for(int i = 0; i < d.N; i++) {
                for(int j = 0; j < d.N; j++) {
                    nodo.mapa[i, j] = d.mapa[i, j];
                    if(d.mapa[i, j] == 'B') {
                        nodo.blancos++;
                    }
                }
            }
            nodo.mapa[d.y, d.x] = '*';
            nodo.Creal = 0;
            nodo.Cestimado = CosteEstimado(nodo, d);
            return nodo;
        }

        static void Añadir(LinkedList<Nodo> C, Nodo Y) {
            LinkedList<Nodo> aux = new LinkedList<Nodo>(C);
            aux.AddLast(Y);
            C.Clear();
            foreach(Nodo X in aux.OrderBy(x => x.Cestimado)) {
                C.AddLast(X);
            }
            //************************************************************
            Console.WriteLine("(+) Se añade " + (char)(A + Y.etapa));
            Mostrar(C);
            //************************************************************
        }

        static bool EsVacía(LinkedList<Nodo> C) {
            return C.Count == 0;
        }

        static Nodo Sacar(LinkedList<Nodo> C) {
            Nodo Y = C.First();
            C.RemoveFirst();
            //************************************************************
            Console.WriteLine("(-) Se saca " + (char)(A + Y.etapa));
            Mostrar(C);
            //************************************************************
            return Y;
        }

        static Punto[] NuevaLista(Punto[] solución, Punto k) {
            List<Punto> aux = new List<Punto>(solución);
            aux.Add(k);
            return aux.ToArray();
        }

        static IEnumerable<Nodo> Hijos(Nodo Y, Datos d) {
            List<Nodo> aux = new List<Nodo>();
            Nodo X; Punto p; int b;
            for(int k = 0; k < MaxMov; k++) {
                Mover(Y, d, k, out p, out b);
                if(Comprobar(d.N, p.f, p.c) && Y.mapa[p.f, p.c] == ' ') {
                    X = new Nodo();
                    X.etapa = Y.etapa + 1;
                    X.solución = NuevaLista(Y.solución, p);
                    X.blancos = b;
                    X.mapa = new char[d.N, d.N];
                    for(int i = 0; i < d.N; i++) {
                        for(int j = 0; j < d.N; j++) {
                            X.mapa[i, j] = Y.mapa[i, j];
                        }
                    }
                    X.mapa[p.f, p.c] = '*';
                    X.Creal = Y.Creal + 1;
                    X.Cestimado = CosteEstimado(X, d);
                    aux.Add(X);
                }
            }
            return aux;
        }

        static bool EsSolución(Nodo X, Datos d) {
            return X.blancos <= 0;
        }

        static bool EsCompletable(Nodo X, Datos d) {
            Punto p; int b;
            for(int k = 0; k < MaxMov; k++) {
                Mover(X, d, k, out p, out b);
                if(Comprobar(d.N, p.f, p.c) && X.mapa[p.f, p.c] == ' ') {
                    return true;
                }
            }
            return false;
        }

        static Resultado Calcular(Datos d) {
            LinkedList<Nodo> C = new LinkedList<Nodo>();
            Nodo Y = new Nodo();
            Resultado r = new Resultado();

            Y = Raíz(d);
            Añadir(C, Y);
            r.coste = int.MaxValue;

            while(!EsVacía(C)) {
                Y = Sacar(C);
                if(Y.Cestimado < r.coste) {
                    foreach(Nodo X in Hijos(Y, d)) {
                        if(EsSolución(X, d)) {
                            if(X.Creal < r.coste) {
                                r.coste = X.Creal;
                                r.solución = X.solución;
                            }
                        } else if(EsCompletable(X, d) && X.Cestimado < r.coste) {
                            Añadir(C, X);
                        }
                    }
                }
            }

            return r;
        }

        //*****************************************************************

        public static void Resolver() {
            const int N = 5;

            Datos d = new Datos();
            d.N = N;
            d.mapa = new char[N, N]
            { //   0    1    2    3    4
                { ' ', ' ', ' ', 'N', ' ' }, // 0
                { 'B', ' ', 'B', 'N', 'B' }, // 1
                { ' ', 'N', ' ', 'B', ' ' }, // 2
                { 'B', 'N', 'N', 'N', ' ' }, // 3
                { ' ', ' ', 'B', ' ', ' ' }, // 4
            };
            d.x = 4;
            d.y = 4;

            Resultado r = Calcular(d);
            Mostrar(d, r);
        }

        static void Mostrar(Datos d, Resultado r) {
            Console.Write("Resultado: ");
            foreach(Punto k in r.solución) {
                Console.Write("(" + k.f + ", " + k.c + ") ");
            }
            Console.WriteLine();
        }

        static void Mostrar(LinkedList<Nodo> C) {
            if(C.Count > 0) {
                Console.WriteLine("Cola: " +
                                  C.Select(x => ((char)(A + x.etapa)).ToString())
                                   .Aggregate((x, xs) => x + " " + xs));
            } else {
                Console.WriteLine("Cola: ");
            }
        }
    }
}
