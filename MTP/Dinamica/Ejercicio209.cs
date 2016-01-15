using System;
using System.Collections.Generic;
using System.Linq;

namespace MTP.Dinamica {
    /* 
     * Subsecuencia común máxima. Dada una secuencia X = {x1 x2 ... xm}, se dice que Z = {z1 z2 ... zk}
     * (k <= m) es una subsecuencia de X si existe una secuencia creciente de índices de X {i1 i2 ... ik}
     * tales que para todo j = {1, 2, ..., k}, x(i(j)) = z(j).
     * 
     * Dadas dos secuencias X e Y, Z es una subsecuencia común de X e Y si es subsecuencia de X y
     * subsecuencia de Y. Determinar utilizando programación dinámica la subsecuencia de longitud
     * máxima común a X e Y.
     * 
     * Ejemplo: Dadas X = {A,B,C,B,D,A,B} e Y = {B,D,C,A,B,A}, la secuencia {B,C,A} es una subsecuencia
     * común de ambas. Las secuencias {B,C,B,A} y {B,D,A,B} son subsecuencias comunes de máxima longitud.
     */
    public static class Ejercicio209 {
        static bool Está(char x, char[] l, int j) {
            for(int i = j; i < l.Length; i++) {
                if(l[i] == x) return true;
            }
            return false;
        }

        static List<char> Fusión(List<char> li, List<char> lj) {
            List<char> r = new List<char>();
            foreach(char c in li) { r.Add(c); }
            foreach(char c in lj) { r.Add(c); }
            return r;
        }

        static bool Validar(char[] x, char[] l, int N) {
            int i = 0;
            for(int j = 0; j < N; j++) {
                if(i < x.Length && x[i] == l[j]) i++;
            }
            return (i == x.Length);
        }

        static char[] Calcular2(char[] lx, char[] ly) {
            int N = lx.Length;
            List<char>[,] tabla = new List<char>[N, N];

            for(int i = 0; i < N; i++) {
                for(int j = 0; j < N; j++) {
                    if(i <= j) {
                        tabla[i, j] = new List<char>();
                        if(i == j && Está(lx[j], ly, 0)) {
                            tabla[i, j].Add(lx[j]);
                        }
                    } else {
                        tabla[i, j] = null;
                    }
                }
            }

            List<char> aux;
            for(int i = N - 2; i >= 0; i--) {
                for(int j = i + 1; j < N; j++) {
                    tabla[i, j].AddRange(tabla[i + 1, j]);
                    if(i == 0 && tabla[i, j].Count < tabla[i, j - 1].Count) {
                        tabla[i, j].Clear();
                        tabla[i, j].AddRange(tabla[i, j - 1]);
                    }
                    for(int k = i; k < j; k++) {
                        aux = Fusión(tabla[i, k], tabla[k + 1, j]);
                        if(Validar(aux.ToArray(), ly, ly.Length) &&
                           aux.Count > tabla[i, j].Count) {
                            tabla[i, j] = aux;
                        }
                    }
                }
            }
            Mostrar(tabla, N);
            return tabla[0, N - 1].ToArray();
        }

        //*****************************************************************

        static int Max(int a, int b) {
            return (b > a) ? b : a;
        }

        static char[] Calcular(char[] lx, char[] ly) {
            int i, j;
            int N = lx.Length + 1;
            int M = ly.Length + 1;
            int[,] t = new int[N, M];

            for(i = 0; i < N; i++) { t[i, 0] = 0; }
            for(j = 1; j < M; j++) { t[0, j] = 0; }

            for(i = 1; i < N; i++) {
                for(j = 1; j < M; j++) {
                    if(lx[i - 1] == ly[j - 1]) {
                        t[i, j] = t[i - 1, j - 1] + 1;
                    } else {
                        t[i, j] = Max(t[i - 1, j], t[i, j - 1]);
                    }
                }
            }

            Mostrar(t, N, M);
            LinkedList<char> r = new LinkedList<char>();
            i = lx.Length; j = ly.Length;
            while(i > 0 && j > 0) {
                if(t[i, j] == t[i - 1, j - 1] + 1) {
                    r.AddFirst(lx[i - 1]);
                    i--;
                    j--;
                } else {
                    if(t[i, j] == t[i - 1, j]) {
                        i--;
                    } else {
                        j--;
                    }
                }
            }
            return r.ToArray();
        }

        public static void Resolver() {
            //char[] lx = { 'A', 'B', 'C', 'Z', 'Y', 'A', 'B' };
            char[] lx = { 'A', 'B', 'C', 'B', 'D', 'A', 'B' };
            char[] ly = { 'B', 'D', 'C', 'A', 'B', 'A' };

            char[] r = Calcular(lx, ly);
            Listar("X", lx);
            Listar("Y", ly);
            Listar("R", r);
            Console.WriteLine();

            r = Calcular(ly, lx);
            Listar("X", lx);
            Listar("Y", ly);
            Listar("R", r);
            Console.WriteLine();
        }

        static void Listar(string c, char[] l) {
            Console.WriteLine("{0}: {1}", c,
                              l.Select(x => x.ToString())
                               .Aggregate((x, xs) => x + " " + xs));
        }

        static void Mostrar(int[,] tabla, int N, int M) {
            for(int i = 0; i < N; i++) {
                for(int j = 0; j < M; j++) {
                    Console.Write(tabla[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        static void Mostrar(List<char>[,] tabla, int N) {
            for(int i = 0; i < N; i++) {
                for(int j = 0; j < N; j++) {
                    if(tabla[i, j] == null) {
                        Console.Write("- ");
                    } else {
                        Console.Write(tabla[i, j].Count + " ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
