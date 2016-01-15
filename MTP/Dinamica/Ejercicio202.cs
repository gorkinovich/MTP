using System;
using System.Collections.Generic;
using System.Linq;

namespace MTP.Dinamica {
    /* 
     * Ejercicio 2
     * El país de Fanfanisflán emite n sellos diferentes de valores naturales positivos s1, s2, ..., sn.
     * Se quiere enviar una carta y se sabe que la correspondiente tarifa postal es T. ¿De cuántas formas
     * diferentes se puede franquear exactamente la carta, si el orden de los sellos no importa?
     */
    public static class Ejercicio202 {
        const int I = int.MaxValue; // Infinito

        static int Min(int a, int b) {
            return (b < a) ? b : a;
        }

        static int[,] CalcTabla(int[] lista, int precio) {
            int MaxElem = lista.Length;
            int MaxCant = precio + 1;
            int[,] tabla = new int[MaxElem, MaxCant];
            int[,] total = new int[MaxElem, MaxCant];

            for(int i = 0; i < MaxElem; i++) {
                tabla[i, 0] = 0;
                total[i, 0] = 0;
            }
            for(int i = 1; i < MaxCant; i++) {
                tabla[0, i] = I;
                total[i, 0] = 0;
            }

            int a, b, aux;
            for(int i = 1; i < MaxElem; i++) {
                for(int j = 1; j < MaxCant; j++) {
                    if(lista[i] <= j) {
                        aux = total[i - 1, j - lista[i]];
                        if(aux != I) aux += lista[i];

                        if(aux == j) {
                            a = tabla[i - 1, j];
                            b = tabla[i - 1, j - lista[i]];
                            if(b != I) b += 1;
                            tabla[i, j] = Min(a, b);
                            total[i, j] = aux;
                        } else {
                            tabla[i, j] = I;
                            total[i, j] = I;
                        }
                    } else {
                        tabla[i, j] = tabla[i - 1, j];
                        total[i, j] = total[i - 1, j];
                    }
                }
            }

            //Mostrar("Tabla:", tabla, MaxElem, MaxCant);
            //Mostrar("Total:", total, MaxElem, MaxCant);

            return tabla;
        }

        static int[] Solución(int[] lista, int[,] tabla, int MaxElem, int MaxCant) {
            List<int> r = new List<int>();
            int i = MaxElem - 1;
            int j = MaxCant - 1;

            while(i > 0 && j > 0) {
                if(tabla[i, j] == tabla[i - 1, j]) {
                    i--;
                } else if(tabla[i - 1, j - lista[i]] == tabla[i, j] - 1) {
                    r.Add(lista[i]);
                    j -= lista[i]; i--;
                }
            }

            return r.ToArray();
        }

        static List<int[]> Soluciones(int[] lista, int[,] tabla, int precio) {
            int MaxElem = lista.Length;
            int MaxCant = precio + 1;
            int i = MaxElem - 1;
            int j = MaxCant - 1;
            List<int[]> r = new List<int[]>();

            while(tabla[i, j] != I) {
                if(tabla[i, j] != tabla[i - 1, j]) {
                    r.Add(Solución(lista, tabla, i + 1, MaxCant));
                }
                i--;
            }

            return r;
        }

        static List<int[]> Calcular(int[] lista, int precio) {
            List<int> sellos = new List<int>();
            int max;

            sellos.Add(0);
            for(int i = 0; i < lista.Length; i++) {
                max = precio / lista[i];
                for(int j = 0; j < max; j++) {
                    sellos.Add(lista[i]);
                }
            }

            //Console.WriteLine("Lista: " +
            //                  lista.Select(x => x.ToString())
            //                       .Aggregate((x, xs) => x + " " + xs));
            //Console.WriteLine("Sellos: " +
            //                  sellos.Select(x => x.ToString())
            //                        .Aggregate((x, xs) => x + " " + xs));

            int[,] tabla = CalcTabla(sellos.ToArray(), precio);
            return Soluciones(sellos.ToArray(), tabla, precio);
        }

        static void Resolver(int[] lista, int precio) {
            List<int[]> r = Calcular(lista, precio);

            Console.WriteLine("Nº de formas: " + r.Count + "\n");
            foreach(int[] l in r) {
                Console.WriteLine("Sellos: " +
                                  l.Select(x => x.ToString())
                                   .Aggregate((x, xs) => x + " " + xs));
            }
            Console.WriteLine();
        }

        public static void Resolver() {
            int[] lista = { 1, 5, 10, 20 };
            Resolver(lista, 22);

            int[] lista2 = { 1, 4, 6 };
            Resolver(lista2, 8);
            Resolver(lista2, 12);
        }

        static void Mostrar(string msj, int[,] tabla, int MaxElem, int MaxCant) {
            Console.WriteLine(msj);
            for(int i = 0; i < MaxElem; i++) {
                for(int j = 0; j < MaxCant; j++) {
                    if(tabla[i, j] == I) {
                        Console.Write(" - ");
                    } else {
                        Console.Write(tabla[i, j].ToString("00") + " ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
