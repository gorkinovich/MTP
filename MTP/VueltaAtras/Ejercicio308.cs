using System;
using System.Collections.Generic;
using System.Linq;

namespace MTP.VueltaAtras {
    /* 
     * El famoso juego del Sudoku consiste en rellenar un cubo de 9 x 9 celdas dispuestas en 9 subgrupos
     * de 3 x 3 celdas, con números del 1 al 9, atendiendo a la restricción de que no se debe repetir el
     * mismo número en la misma fila, columna o subgrupo de 9. Un Sudoku dispone de varias celdas con un
     * valor inicial, de modo que debemos empezar a resolver el problema a partir de esta solución parcial
     * sin modificar ninguna de las celdas iniciales.
     * + Características del árbol de expansión (Número de hijos, profundidad).
     * + Código del procedimiento.
     */
    public static class Ejercicio308 {
        const int N = 9;
        const int M = 3;

        struct Celda {
            public Celda(int i, int j, int[] l) {
                this.i = i;
                this.j = j;
                this.libres = l;
            }
            public int i;
            public int j;
            public int[] libres;
        }

        static int Zona(Celda x) {
            return M * (x.i / M) + x.j / M;
        }

        static bool Calcular(int[,] tabla, Celda[] l, int n, int[,] r) {
            if(l.Length == n) {
                return true;
            } else {
                bool aux; int k;
                foreach(int v in l[n].libres) {
                    //***************************************************
                    // Prueba del candidato
                    //***************************************************
                    aux = true;
                    for(k = n - 1; k >= 0 && aux; k--) {
                        if((l[k].i == l[n].i || l[k].j == l[n].j || Zona(l[k]) == Zona(l[n])) &&
                           r[l[k].i, l[k].j] == v) {
                               aux = false;
                        }
                    }

                    //***************************************************
                    // Prueba de los candidatos
                    //***************************************************
                    if(aux) {
                        // Meter número
                        r[l[n].i, l[n].j] = v;
                        // Siguiente casilla
                        if(Calcular(tabla, l, n + 1, r)) {
                            return true;
                        }
                    }
                }
                return false;
            }
        }

        static Celda[] CalcLibres(int[,] tabla) {
            List<Celda> lista = new List<Celda>();

            for(int i = 0; i < N; i++) {
                for(int j = 0; j < N; j++) {
                    if(tabla[i, j] == 0) {
                        List<int> libres = new List<int>();
                        for(int n = 1; n <= N; n++) {
                            bool aux = true;
                            for(int k = 0; k < N && aux; k++) {
                                if(tabla[i, k] == n || tabla[k, j] == n) {
                                    aux = false;
                                }
                            }
                            if(aux) libres.Add(n);
                        }
                        if(libres.Count > 0) {
                            lista.Add(new Celda(i, j, libres.ToArray()));
                        }
                    }
                }
            }
            return lista.OrderBy(x => x.libres.Length).ToArray();
        }

        static int[,] Calcular(int[,] tabla) {
            int[,] r = new int[N, N];
            Celda[] libres = CalcLibres(tabla);

            for(int i = 0; i < N; i++) {
                for(int j = 0; j < N; j++) {
                    r[i, j] = tabla[i, j];
                }
            }

            //foreach(Celda c in libres) {
            //    Console.WriteLine(c.i + " " + c.j + " -> " +
            //                      Zona(c) + " -> " +
            //                      c.libres.Select(x => x.ToString())
            //                       .Aggregate((x, xs) => x + " " + xs));
            //}
            //Console.WriteLine("Cantidad: " + libres.Length);
            
            if(Calcular(tabla, libres, 0, r)) {
                return r;
            } else {
                return null;
            }
        }

        public static void Resolver() {
            int[,] tabla =
            {
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            };
            Cargar(tabla);
            Mostrar(tabla, "         Sudoku\n", 0);

            int[,] result = Calcular(tabla);
            if(result != null) {
                Mostrar(result, "       Resultado\n", 40);
            } else {
                Console.WriteLine("No hay resultado...\n");
            }

            if(result != null && EsSolución(result)) {
                Console.WriteLine("El sudoku ha sido resuelto.\n");
            } else {
                Console.WriteLine("El sudoku no ha sido resuelto...\n");
            }
        }

        private static void Mostrar(int[,] tabla, string msj, int c) {
            Console.CursorTop = 0;
            Console.CursorLeft = c;
            Console.WriteLine(msj);
            for(int i = 0; i < N; i++) {
                Console.CursorLeft = c;
                Console.Write(" ");
                for(int j = 0; j < N; j++) {
                    if(tabla[i, j] == 0) {
                        Console.Write("  ");
                    } else {
                        Console.Write(tabla[i, j] + " ");
                    }
                    if(j == 2 || j == 5) {
                        Console.Write("| ");
                    }
                }
                Console.WriteLine();
                if(i == 2 || i == 5) {
                    Console.CursorLeft = c;
                    Console.Write("-");
                    for(int j = 0; j < N + 2; j++) {
                        Console.Write("--");
                    }
                    Console.WriteLine();
                }
            }
            Console.WriteLine();
        }

        static void Cargar(int[,] tabla) {
            /*
             * --3  ---  ---
             * 75-  3--  -86
             * 8-2  ---  --5
             * 
             * -36  28-  4-7
             * -4-  9--  ---
             * --5  43-  ---
             * 
             * 5-8  ---  76-
             * 6--  892  -5-
             * -2-  -5-  ---
             */
            // 0, 0
            tabla[0, 2] = 3; tabla[1, 0] = 7;
            tabla[1, 1] = 5; tabla[2, 0] = 8;
            tabla[2, 2] = 2;
            // 0, 3
            tabla[1, 3] = 3;
            // 0, 6
            tabla[1, 7] = 8; tabla[1, 8] = 6;
            tabla[2, 8] = 5;

            // 3, 0
            tabla[3, 1] = 3; tabla[3, 2] = 6;
            tabla[4, 1] = 4; tabla[5, 2] = 5;
            // 3, 3
            tabla[3, 3] = 2; tabla[3, 4] = 8;
            tabla[4, 3] = 9; tabla[5, 3] = 4;
            tabla[5, 4] = 3;
            // 3, 6
            tabla[3, 6] = 4; tabla[3, 8] = 7;

            // 6, 0
            tabla[6, 0] = 5; tabla[6, 2] = 8;
            tabla[7, 0] = 6; tabla[8, 1] = 2;
            // 6, 3
            tabla[7, 3] = 8; tabla[7, 4] = 9;
            tabla[7, 5] = 2; tabla[8, 4] = 5;
            // 6, 6
            tabla[6, 6] = 7; tabla[6, 7] = 6;
            tabla[7, 7] = 5;
        }

        static bool EsLínea(int[,] tabla, int n, bool horiz) {
            bool[] está = Enumerable.Range(0, N).Select(x => false).ToArray();

            if(horiz) {
                for(int j = 0; j < N; j++) {
                    if(tabla[n, j] > 0) {
                        está[tabla[n, j] - 1] = true;
                    }
                }
            } else {
                for(int i = 0; i < N; i++) {
                    if(tabla[i, n] > 0) {
                        está[tabla[i, n] - 1] = true;
                    }
                }
            }

            for(int k = 0; k < N; k++) {
                if(!está[k]) return false;
            }
            return true;
        }

        static bool EsCuadrado(int[,] tabla, int i, int j) {
            bool[] está = Enumerable.Range(0, N).Select(x => false).ToArray();

            for(int f = i; f < i + M; f++) {
                for(int c = j; c < j + M; c++) {
                    if(tabla[f, c] > 0) {
                        está[tabla[f, c] - 1] = true;
                    }
                }
            }

            for(int k = 0; k < N; k++) {
                if(!está[k]) return false;
            }
            return true;
        }

        static bool EsSolución(int[,] tabla) {
            for(int i = 0; i < N; i++) {
                if(!EsLínea(tabla, i, true)) return false;
            }
            for(int j = 0; j < N; j++) {
                if(!EsLínea(tabla, j, false)) return false;
            }
            for(int i = 0; i < N; i += M) {
                for(int j = 0; j < N; j += M) {
                    if(!EsCuadrado(tabla, i, j)) {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
