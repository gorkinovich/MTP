using System;
using System.Collections.Generic;
using System.Linq;

namespace MTP.Dinamica {
    /* 
     * A Nicanor Cienfuegos le han hecho un regalo. Como no le gusta ha decidido cambiarlo por otros
     * productos. Su cambio ideal es el siguiente: el valor de los productos tiene que ser igual al
     * valor del regalo o superarlo de forma mínima. No le importa tener varias copias del mismo
     * producto. Suponiendo conocidos los productos de la tienda, sus precios y el número de unidades
     * de cada producto:
     * a) (3 puntos) Diseña un algoritmo dinámico que determine el valor de los productos elegidos
     *    en el canje, detallando: La estructura de cálculos intermedios, la relación recursiva, y
     *    la función o procedimiento que implemente este algoritmo.
     * b) (1 punto) Proporciona una función con memoria que implemente este algoritmo.
     */
    public static class Ejercicio211 {
        const int I = int.MaxValue; // Infinito

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

        static int DivSup(int a, int b) {
            return (int)Math.Ceiling((float)a / (float)b);
        }

        //*****************************************************************

        static int[] Transformar(int[,] t, int[] p, int[] s, int ME, int MC) {
            int[] r = Enumerable.Range(0, ME).Select(x => 0).ToArray();
            int i = ME - 1, j = MC - 1;
            while(j > 0) {
                if(i == 0) {
                    if(t[i, j] != I) {
                        r[i] += t[i, j] / p[i];
                    }
                    j = 0;
                } else {
                    int aux, maxk = -1;
                    for(int k = 1; k <= s[i]; k++) {
                        aux = p[i] * k;
                        if(aux <= j) {
                            if(Sumar(aux, t[i - 1, j - aux]) == t[i, j]) {
                                if(k > maxk) maxk = k;
                            }
                        } else {
                            if(t[i, j] == aux) {
                                if(k > maxk) maxk = k;
                            }
                        }
                    }
                    if(maxk != -1) {
                        r[i] += maxk;
                        j -= p[i] * maxk;
                        i--;
                    } else if(t[i, j] == t[i - 1, j]) {
                        i--;
                    }
                }
            }
            return r;
        }

        static int[] Calcular(int[] p, int[] s, int D) {
            int MaxElem = p.Length;
            int MaxCant = D + 1;
            int[,] t = new int[MaxElem, MaxCant];

            for(int i = 0; i < MaxElem; i++) {
                t[i, 0] = 0;
            }
            for(int j = 1; j < MaxCant; j++) {
                if(p[0] * s[0] >= j) {
                    t[0, j] = DivSup(j, p[0]) * p[0];
                } else {
                    t[0, j] = I;
                }
            }

            int maxk, vmin, aux;
            for(int i = 1; i < MaxElem; i++) {
                for(int j = 1; j < MaxCant; j++) {
                    maxk = Min(s[i], j / p[i]);
                    if(maxk > 0) {
                        vmin = t[i - 1, j];
                        for(int k = 1; k <= maxk; k++) {
                            aux = Sumar(p[i] * k, t[i - 1, j - p[i] * k]);
                            vmin = Min(aux, vmin);
                        }
                        t[i, j] = vmin;
                    } else {
                        t[i, j] = Min(t[i - 1, j], p[i]);
                    }
                }
            }
            Mostrar(t, MaxElem, MaxCant);
            return Transformar(t, p, s, MaxElem, MaxCant);
        }

        //*****************************************************************

        static void Resolver(int[] p, int[] s, int D) {
            int[] r = Calcular(p, s, D);
            Console.WriteLine("D: " + D);
            Listar("P", p);
            Listar("S", s);
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
