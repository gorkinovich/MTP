using System;
using System.Collections.Generic;
using System.Linq;

namespace MTP.DyV {
    /*
     * Ejercicio 3: Diseña un algoritmo mediante la técnica divide y vencerás que encuentre el cuadrado
     * de unos más grande en una matriz cuadrada de bits (n x n, n potencia de 2). Calcula el orden de
     * complejidad del algoritmo desarrollado.
     */
    public static class Ejercicio003 {
        #region Tipos
        public struct Rect {
            public Rect(int f, int c, int an, int al) {
                this.f = f; this.alto = al;
                this.c = c; this.ancho = an;
            }
            public int f; public int alto;
            public int c; public int ancho;
        }

        public struct Región {
            public Región(int f, int c, int t) {
                this.f = f;
                this.c = c;
                this.tamaño = t;
            }
            public int f;
            public int c;
            public int tamaño;
        }
        #endregion


        #region Busqueda recursiva
        public static Rect Fusión(Rect a, Rect b) {
            if(a.f == b.f && a.alto == b.alto) {
                if(a.c + a.ancho == b.c) {
                    return new Rect(a.f, a.c, a.ancho + b.ancho, a.alto);
                } else if(b.c + b.ancho == a.c) {
                    return new Rect(b.f, b.c, a.ancho + b.ancho, a.alto);
                }
            } else if(a.c == b.c && a.ancho == b.ancho) {
                if(a.f + a.alto == b.f) {
                    return new Rect(a.f, a.c, a.ancho, a.alto + b.alto);
                } else if(b.f + b.alto == a.f) {
                    return new Rect(b.f, b.c, a.ancho, a.alto + b.alto);
                }
            }
            return new Rect(0, 0, 0, 0);
        }

        public static bool Esta(LinkedList<Rect> conj, Rect x) {
            foreach(Rect r in conj) {
                if(r.f == x.f && r.c == x.c &&
                   r.ancho == x.ancho && r.alto == x.alto) {
                    return true;
                }
            }
            return false;
        }

        public static void Combinación(int[,] matriz, LinkedList<Rect> conjunto) {
            Rect aux = new Rect();
            bool cambios;
            int sig, desp = 1;
            do {
                cambios = false;
                Rect[] lista = conjunto.ToArray();
                sig = lista.Length;

                for(int i = 0; i < lista.Length; i++) {
                    for(int j = desp; j < lista.Length; j++) {
                        if(matriz[lista[i].f, lista[i].c] == matriz[lista[j].f, lista[j].c]) {
                            aux = Fusión(lista[i], lista[j]);
                            if(aux.ancho != 0 && aux.alto != 0 && !Esta(conjunto, aux)) {
                                conjunto.AddLast(aux);
                                cambios = true;
                            }
                        }
                    }
                }

                desp = sig;
            } while(cambios);
        }

        public static LinkedList<Rect> Busqueda(int[,] matriz, Región r) {
            if(r.tamaño > 0) {
                LinkedList<Rect> result = new LinkedList<Rect>();
                if(r.tamaño > 1) {
                    int mitad = r.tamaño / 2;
                    // Partición
                    LinkedList<Rect> r1 = Busqueda(matriz, new Región(r.f, r.c, mitad));
                    LinkedList<Rect> r2 = Busqueda(matriz, new Región(r.f, r.c + mitad, mitad));
                    LinkedList<Rect> r3 = Busqueda(matriz, new Región(r.f + mitad, r.c, mitad));
                    LinkedList<Rect> r4 = Busqueda(matriz, new Región(r.f + mitad, r.c + mitad, mitad));
                    // Combinación
                    foreach(Rect item in r1.Concat(r2).Concat(r3).Concat(r4)) {
                        result.AddLast(item);
                    }
                    Combinación(matriz, result);
                } else {
                    result.AddLast(new Rect(r.f, r.c, 1, 1));
                }
                return result;
            } else {
                return null;
            }
        }

        public static Región Busqueda(int[,] matriz, int tam) {
            Región result = new Región(0, 0, 0);
            foreach(Rect r in Busqueda(matriz, new Región(0, 0, tam))) {
                //if(r.alto == r.ancho && r.alto > 3) {
                //    Console.WriteLine("f: {0}", r.f);
                //    Console.WriteLine("c: {0}", r.c);
                //    Console.WriteLine("t: {0}", r.alto);
                //    Console.WriteLine();
                //}
                if(r.alto == r.ancho && result.tamaño < r.alto) {
                    result.f = r.f;
                    result.c = r.c;
                    result.tamaño = r.alto;
                }
            }
            return result;
        }
        #endregion


        #region Busqueda iterativa
        public static Región BusqIter(int[,] matriz, int f, int c, int tam) {
            Región result = new Región(f, c, 1);
            int n = (f > c ? f : c);

            for(int i = 1; (n + i) < tam; i++) {
                if(matriz[f, c] != matriz[f + i, c + i]) {
                    return result;
                } else {
                    for(int j = i - 1; j >= 0; j--) {
                        if(matriz[f, c] != matriz[f + j, c + i]) {
                            return result;
                        }
                        if(matriz[f, c] != matriz[f + i, c + j]) {
                            return result;
                        }
                    }
                    result.tamaño++;
                }
            }
            return result;
        }

        public static Región BusqIter(int[,] matriz, int tam) {
            Región result = new Región();
            Región temp = new Región();

            for(int i = 0; i < tam; i++) {
                if(result.tamaño >= tam - i) break;
                for(int j = 0; j < tam; j++) {
                    if(result.tamaño >= tam - j) break;
                    temp = BusqIter(matriz, i, j, tam);
                    if(temp.tamaño > result.tamaño) {
                        result = temp;
                    }
                }
            }

            return result;
        }
        #endregion


        public static void Resolver() {
            const int tamaño = 8;
            int[,] matriz = new int[tamaño, tamaño]
            {
                #region Ejemplo 1
                /*
                { 1, 1, 1, 0, 0, 0, 0, 0 },
                { 1, 1, 1, 0, 0, 0, 0, 0 },
                { 1, 1, 1, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 1, 1, 1, 1, 0 },
                { 0, 0, 0, 1, 1, 1, 1, 0 },
                { 0, 0, 0, 1, 1, 1, 1, 0 },
                { 1, 1, 0, 1, 1, 1, 1, 0 },
                { 1, 1, 0, 0, 0, 0, 0, 0 }
                //*/
                #endregion

                #region Ejemplo 2
                /*
                { 1, 1, 1, 0, 0, 0, 0, 0 },
                { 1, 1, 1, 0, 0, 0, 0, 0 },
                { 1, 1, 1, 1, 1, 1, 1, 0 },
                { 0, 0, 1, 1, 1, 1, 1, 0 },
                { 0, 0, 1, 1, 1, 1, 1, 0 },
                { 0, 0, 1, 1, 1, 1, 1, 0 },
                { 1, 1, 1, 1, 1, 1, 1, 0 },
                { 1, 1, 0, 0, 0, 0, 0, 0 }
                //*/
                #endregion

                #region Ejemplo 3
                /*
                { 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 1, 1, 1, 1, 0 },
                { 0, 0, 0, 1, 1, 1, 1, 0 },
                { 0, 0, 0, 1, 1, 1, 1, 0 },
                { 0, 0, 0, 1, 1, 1, 1, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0 }
                //*/
                #endregion

                #region Ejemplo 4
                /*
                { 1, 1, 1, 0, 0, 0, 0, 0 },
                { 1, 1, 1, 1, 1, 1, 1, 0 },
                { 1, 1, 1, 1, 1, 1, 1, 0 },
                { 0, 0, 1, 1, 1, 1, 1, 0 },
                { 0, 0, 1, 1, 1, 1, 1, 0 },
                { 0, 0, 1, 1, 1, 1, 1, 0 },
                { 1, 1, 1, 1, 1, 1, 1, 0 },
                { 1, 1, 0, 0, 0, 0, 0, 0 }
                //*/
                #endregion

                #region Ejemplo 5
                //*
                { 1, 1, 1, 0, 0, 0, 0, 0 },
                { 1, 1, 1, 1, 1, 1, 1, 0 },
                { 1, 1, 1, 1, 1, 1, 1, 0 },
                { 0, 0, 1, 1, 1, 1, 1, 0 },
                { 0, 0, 1, 1, 1, 1, 1, 0 },
                { 0, 0, 1, 1, 1, 1, 1, 0 },
                { 1, 1, 0, 0, 0, 0, 0, 0 },
                { 1, 1, 0, 0, 0, 0, 0, 0 }
                //*/
                #endregion
            };

            Región result = BusqIter(matriz, tamaño);
            Console.WriteLine("f: {0}", result.f);
            Console.WriteLine("c: {0}", result.c);
            Console.WriteLine("t: {0}", result.tamaño);

            Console.WriteLine();
            result = Busqueda(matriz, tamaño);
            Console.WriteLine("f: {0}", result.f);
            Console.WriteLine("c: {0}", result.c);
            Console.WriteLine("t: {0}", result.tamaño);
        }
    }
}
