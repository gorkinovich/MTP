using System;
using System.Collections.Generic;
using System.Linq;

namespace MTP.Ramificacion {
    /* 
     * En el departamento de una empresa de traducciones se desea hacer traducciones de textos entre
     * varios idiomas. Se dispone de algunos diccionarios. Cada diccionario permite la traducción
     * (bidireccional) entre dos idiomas. En el caso más general, no se dispone de diccionarios para
     * cada par de idiomas por lo que es preciso realizar varias traducciones. Dados N idiomas y M
     * diccionarios, determina si es posible realizar la traducción entre dos idiomas dados y, en
     * caso de ser posible, determina la cadena de traducciones de longitud mínima.
     * 
     * Diseña un algoritmo de ramificación y poda que resuelva el problema detallando lo siguiente:
     * 1. El árbol de búsqueda: significado de las aristas y de los niveles (0,5 puntos).
     * 2. El código del procedimiento (2,5 puntos).
     * 3. La función cota utilizada (1 punto).
     */
    public static class Ejercicio402 {
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
            public string[] lenguaje;
            public bool[,] grafo;
            public int orig;
            public int dest;
        }

        struct Nodo {
            public int[] solución;
            public bool[] marcado;
            public int lugar;
            public int Creal;
            public int Cestimado;
        }

        struct Resultado {
            public int[] solución;
            public int coste;
        }

        static int Min(int a, int b) {
            return (b < a) ? b : a;
        }

        static int Max(int a, int b) {
            return (b > a) ? b : a;
        }

        //*****************************************************************

        static int CosteEstimado(Nodo X, Datos d) {
            int coste = X.Creal;
            int k = X.lugar;
            if(k != d.dest) {
                int minFila = int.MaxValue;
                for(int i = 0; i < d.N; i++) {
                    if(d.grafo[k, i] && !X.marcado[i]) {
                        minFila = Min(minFila, 1);
                    }
                }
                coste += minFila;
            }
            return coste;
        }

        static Nodo Raíz(Datos d) {
            Nodo nodo = new Nodo();
            nodo.solución = new int[0];
            nodo.marcado = Enumerable.Range(0, d.N).Select(x => false).ToArray();
            nodo.marcado[d.orig] = true;
            nodo.lugar = d.orig;
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
            Console.WriteLine("(+) Se añade " + (char)(A + Y.lugar));
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
            Console.WriteLine("(-) Se saca " + (char)(A + Y.lugar));
            Mostrar(C);
            //************************************************************
            return Y;
        }

        static int[] NuevaLista(int[] solución, int k) {
            List<int> aux = new List<int>(solución);
            aux.Add(k);
            return aux.ToArray();
        }

        static IEnumerable<Nodo> Hijos(Nodo Y, Datos d) {
            List<Nodo> aux = new List<Nodo>();
            Nodo X;
            for(int k = 0; k < d.N; k++) {
                if(d.grafo[Y.lugar, k] && !Y.marcado[k]) {
                    X = new Nodo();
                    X.solución = NuevaLista(Y.solución, k);
                    X.marcado = (bool[])Y.marcado.Clone();
                    X.marcado[k] = true;
                    X.lugar = k;
                    X.Creal = Y.Creal + 1;
                    X.Cestimado = CosteEstimado(X, d);
                    aux.Add(X);
                }
            }
            return aux;
        }

        static bool EsSolución(Nodo X, Datos d) {
            return X.lugar == d.dest;
        }

        static bool EsCompletable(Nodo X, Datos d) {
            for(int k = 0; k < d.N; k++) {
                if(d.grafo[X.lugar, k] && !X.marcado[k]) {
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
            const int N = 7;

            Datos d = new Datos();
            d.N = N;
            d.lenguaje = new string[N]
            {
                "Aklo", "Elfo", "Klingon",
                "Parsel", "Troll", "D'ni",
                "Neolengua"
            };
            d.grafo = new bool[N, N]
            { //   A      B      C      D      E      F      G
                {false, true,  false, true,  false, false, false}, // A
                {false, false, true,  true,  true,  false, false}, // B
                {false, false, false, false, true,  false, false}, // C
                {false, true,  false, false, true,  true,  false}, // D
                {false, false, true,  false, false, true,  true},  // E
                {false, false, false, false, true,  false, true},  // F
                {false, false, false, false, false, true,  false}  // G
            };
            d.orig = 0;
            d.dest = 6;

            Resultado r = Calcular(d);
            Mostrar(d, r);
        }

        static void Mostrar(Datos d, Resultado r) {
            Console.Write("Resultado: " + d.lenguaje[d.orig]);
            foreach(int k in r.solución) {
                Console.Write(" -> " + d.lenguaje[k]);
            }
            Console.WriteLine();
        }

        static void Mostrar(LinkedList<Nodo> C) {
            if(C.Count > 0) {
                Console.WriteLine("Cola: " +
                                  C.Select(x => ((char)(A + x.lugar)).ToString())
                                   .Aggregate((x, xs) => x + " " + xs));
            } else {
                Console.WriteLine("Cola: ");
            }
        }
    }
}
