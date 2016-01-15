using System;
using System.Collections.Generic;
using System.Linq;

namespace MTP.Dinamica {
    /* 
     * Nicanor Cienfuegos quiere impresionar a su novia. Ha decidido gastarse todo su dinero en flores.
     * Según los criterios estéticos de Nicanor el ramo ideal es aquel que minimiza el número de flores.
     * Dado el prestigio de la floristería, piensa que para cada tipo de flor pueden vender un número
     * infinito de copias. Ante la cara de asombro de su novia Nicanor recapacita, quizás no fue
     * correcta su suposición. Mediante programación dinámica, diseñad dos algoritmos que resuelvan
     * el problema detallando lo siguiente:
     * + Suposición 1: número infinito de copias (2 puntos):
     *   - Estructura de datos intermedia.
     *   - Relación recursiva entre casos y subcasos.
     *   - Implementación del código.
     * + Suposición 2: número finito de copias (2 puntos):
     *   - Estructura de datos intermedia.
     *   - Relación recursiva entre casos y subcasos.
     *   - Implementación del código.
     */
    public static class Ejercicio210 {
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

        static int[] Transformar(int[,] t, int[] p, int ME, int MC) {
            LinkedList<int> r = new LinkedList<int>();
            int i = ME - 1, j = MC - 1;
            while(i > 0 && j > 0) {
                if(t[i, j] == t[i - 1, j]) {
                    i--;
                } else {
                    r.AddFirst(i - 1);
                    j -= p[i - 1];
                }
            }
            return r.ToArray();
        }

        static int[] Calcular(int[] p, int D) {
            int MaxElem = p.Length + 1;
            int MaxCant = D + 1;
            int[,] t = new int[MaxElem, MaxCant];

            for(int i = 0; i < MaxElem; i++) { t[i, 0] = 0; }
            for(int j = 1; j < MaxCant; j++) { t[0, j] = I; }

            int a, b;
            for(int i = 1; i < MaxElem; i++) {
                for(int j = 1; j < MaxCant; j++) {
                    if(p[i - 1] <= j) {
                        a = t[i - 1, j];
                        b = Sumar(t[i, j - p[i - 1]], 1);
                        t[i, j] = Min(a, b);
                    } else {
                        t[i, j] = t[i - 1, j];
                    }
                }
            }
            //Mostrar(t, MaxElem, MaxCant);
            //Mostrar(s, MaxElem, MaxCant);
            return Transformar(t, p, MaxElem, MaxCant);
        }

        //*****************************************************************

        static int[] Transformar(int[,] t, int[] p, int[] st,
                                 int ME, int MC) {
            LinkedList<int> r = new LinkedList<int>();
            int[] s = (int[])st.Clone();
            int i = ME - 1, j = MC - 1;
            while(i > 0 && j > 0) {
                if(t[i, j] == t[i - 1, j] || s[i - 1] == 0) {
                    i--;
                } else {
                    r.AddFirst(i - 1);
                    j -= p[i - 1];
                    s[i - 1]--;
                }
            }
            return r.ToArray();
        }

        static int[] Calcular(int[] p, int[] st, int D) {
            int MaxElem = p.Length + 1;
            int MaxCant = D + 1;
            int[,] t = new int[MaxElem, MaxCant];
            int[,] s = new int[MaxElem, MaxCant];

            for(int i = 0; i < MaxElem; i++) {
                t[i, 0] = 0;
                s[i, 0] = 0;
            }
            for(int j = 1; j < MaxCant; j++) {
                t[0, j] = I;
                s[0, j] = 0;
            }

            int b, auxt, auxs;
            for(int i = 1; i < MaxElem; i++) {
                for(int j = 1; j < MaxCant; j++) {
                    auxt = t[i - 1, j];
                    auxs = 0;
                    if(p[i - 1] <= j) {
                        if(s[i, j - p[i - 1]] < st[i - 1]) {
                            b = Sumar(t[i, j - p[i - 1]], 1);
                            if(b < auxt) {
                                auxt = b;
                                auxs = s[i, j - p[i - 1]] + 1;
                            }
                        } else if(st[i - 1] * p[i - 1] <= j) {
                            b = Sumar(t[i, j - p[i - 1]],
                                t[i - 1, j - st[i - 1] * p[i - 1]]);
                            if(b < auxt) {
                                auxt = b;
                                auxs = s[i, j - p[i - 1]];
                            }
                        }
                    }
                    t[i, j] = auxt;
                    s[i, j] = auxs;
                }
            }
            //Mostrar(t, MaxElem, MaxCant);
            //Mostrar(s, MaxElem, MaxCant);
            return Transformar(t, p, st, MaxElem, MaxCant);
        }

        static void Resolver(int[] p, int[] s, int D) {
            int[] r1 = Calcular(p, D);
            int[] r2 = Calcular(p, s, D);
            Console.WriteLine("D: " + D);
            Listar("P", p);
            Listar("S", s);
            Console.WriteLine("R1: " +
                              r1.Select(x => p[x].ToString())
                                .Aggregate((x, xs) => x + " " + xs));
            Console.WriteLine("R2: " +
                              r2.Select(x => p[x].ToString())
                                .Aggregate((x, xs) => x + " " + xs));
            Console.WriteLine();
        }

        public static void Resolver() {
            int[] precio = { 1, 2, 5, 10, 25, 50 };
            int[] stock = { 24, 16, 8, 4, 2, 1,};
            Resolver(precio, stock, 101);

            int[] precio2 = { 1, 2, 10, 25 };
            int[] stock2 = { 5, 20, 1, 1 };
            Resolver(precio2, stock2, 42);
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
