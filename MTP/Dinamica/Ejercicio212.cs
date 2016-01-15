using System;
using System.Collections.Generic;
using System.Linq;

namespace MTP.Dinamica {
    /* 
     * Una Organización No Gubernamental dispone de un fondo para la financiación de proyectos de
     * ayuda, y se quiere financiar la realización del máximo número de proyectos posible. Para
     * ello, se dispone de una serie de tipos de proyecto diferentes, con la siguiente información
     * disponible:
     * + Presupuesto del tipo de proyecto, p(i)
     * + Número de regiones en las que se puede realizar, r(i)
     * 
     * Mediante la técnica de programación dinámica, diseña un algoritmo que permita determinar
     * cuántos proyectos se pueden realizar sin superar el importe total del fondo, indicando lo
     * siguiente:
     * + Estructura de datos intermedia (0,5 puntos)
     * + Relación recursiva entre casos y subcasos (1 punto)
     * + Código del algoritmo (3,5 puntos)
     */
    public static class Ejercicio212 {
        const int I = int.MaxValue; // Infinito

        static int Max(int a, int b) {
            return (b > a) ? b : a;
        }

        static int Min(int a, int b) {
            return (b < a) ? b : a;
        }

        static int Sumar(int a, int b) {
            if(a < I && b < I) {
                return a + b;
            } else {
                return I;
            }
        }

        //*****************************************************************

        static int[] Transformar(int[,] t, int[] p, int[] z, int ME, int MC) {
            int[] r = Enumerable.Range(0, z.Length).Select(x => 0).ToArray();
            int i = ME - 1, j = MC - 1;
            while(i > 0 && j > 0) {
                if(t[i, j] == t[i - 1, j]) {
                    i--;
                } else {
                    int aux, maxk = -1;
                    for(int k = 1; k <= z[i - 1]; k++) {
                        aux = p[i - 1] * k;
                        if(aux <= j) {
                            if(Sumar(k, t[i - 1, j - aux]) == t[i, j]) {
                                if(k > maxk) maxk = k;
                            }
                        } else {
                            if(t[i, j] == k) {
                                if(k > maxk) maxk = k;
                            }
                        }
                    }
                    if(maxk != -1) {
                        r[i - 1] += maxk;
                        j -= p[i - 1] * maxk;
                        i--;
                    }
                }
            }
            return r;
        }

        static int[] Calcular(int[] p, int[] z, int D) {
            int MaxElem = p.Length + 1;
            int MaxCant = D + 1;
            int[,] t = new int[MaxElem, MaxCant];

            for(int i = 0; i < MaxElem; i++) { t[i, 0] = 0; }
            for(int j = 1; j < MaxCant; j++) { t[0, j] = 0; }

            int maxk, vmin, aux;
            for(int i = 1; i < MaxElem; i++) {
                for(int j = 1; j < MaxCant; j++) {
                    maxk = Min(z[i - 1], j / p[i - 1]);
                    if(maxk > 0) {
                        vmin = t[i - 1, j];
                        for(int k = 1; k <= maxk; k++) {
                            aux = Sumar(k, t[i - 1, j - p[i - 1] * k]);
                            vmin = Max(aux, vmin);
                        }
                        t[i, j] = vmin;
                    } else {
                        t[i, j] = t[i - 1, j];
                    }
                }
            }
            Mostrar(t, MaxElem, MaxCant);
            return Transformar(t, p, z, MaxElem, MaxCant);
        }

        //*****************************************************************

        static void Resolver(int[] p, int[] z, int D) {
            int[] r = Calcular(p, z, D);
            Console.WriteLine("D: " + D);
            Listar("P", p);
            Listar("Z", z);
            Listar("R", r);
            Console.WriteLine();
        }

        public static void Resolver() {
            Resolver(new int[] { 1, 5, 10, 25 },
                     new int[] { 10, 2, 1, 1 }, 20);

            Resolver(new int[] { 1, 5, 10, 25 },
                     new int[] { 10, 4, 2, 1 }, 20);

            Resolver(new int[] { 1, 5, 10, 25 },
                     new int[] { 1, 1, 1, 1 }, 20);

            Resolver(new int[] { 4, 7, 10 },
                     new int[] { 2, 2, 1 }, 20);
        }

        static void Listar(string c, int[] l) {
            Console.WriteLine("{0}: {1}", c,
                              l.Select(x => x.ToString())
                               .Aggregate((x, xs) => x + " " + xs));
        }

        static void Mostrar(int[,] t, int MaxElem, int MaxCant) {
            for(int i = 0; i < MaxElem; i++) {
                for(int j = 0; j < MaxCant; j++) {
                    if(t[i, j] == I)
                        Console.Write(" - ");
                    else
                        Console.Write(t[i, j].ToString("00") + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
