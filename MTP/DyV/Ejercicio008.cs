using System;
using System.Collections.Generic;
using System.Linq;

namespace MTP.DyV {
    /*
     * Dada una matriz cuadrada M, cuya dimensión es potencia de 2, diseña un algoritmo que
     * calcule su traspuesta M^t, mediante la técnica Divide y Vencerás.
     * 
     * Ayuda:
     * Puede ser útil utilizar la siguiente convención: M[i..j, k..l] es la submatriz obtenida al
     * considerar las filas que van desde la i hasta la j y las columnas que van desde la k hasta la l.
     * 
     * Notas:
     * + Se puede considerar que la función que devuelve la parte entera de un número está implementada: ent(x).
     * + Se puede utilizar el operador asignación (<-) para realizar operaciones entre arrays y/o estructuras.
     */
    public static class Ejercicio008 {
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

        public static int[,] Traspuesta(int[,] matriz, Región r) {
            if(r.tamaño > 0) {
                int[,] result = new int[r.tamaño, r.tamaño];

                if(r.tamaño > 1) {
                    int mitad = r.tamaño / 2;
                    // Partición
                    int[,] r1 = Traspuesta(matriz, new Región(r.f, r.c, mitad));
                    int[,] r2 = Traspuesta(matriz, new Región(r.f, r.c + mitad, mitad));
                    int[,] r3 = Traspuesta(matriz, new Región(r.f + mitad, r.c, mitad));
                    int[,] r4 = Traspuesta(matriz, new Región(r.f + mitad, r.c + mitad, mitad));
                    // Combinación
                    for(int i = 0; i < mitad; i++) {
                        for(int j = 0; j < mitad; j++) {
                            result[i, j] = r1[i, j];
                            result[i, j + mitad] = r3[i, j];
                            result[i + mitad, j] = r2[i, j];
                            result[i + mitad, j + mitad] = r4[i, j];
                        }
                    }
                } else {
                    result[0, 0] = matriz[r.f, r.c];
                }

                return result;
            } else {
                return null;
            }
        }

        public static void Resolver() {
            const int tamaño = 8;
            int[,] matriz = new int[tamaño, tamaño]
            {
                { 1, 2, 3, 4, 5, 6, 7, 8 },
                { 1, 2, 3, 4, 5, 6, 7, 8 },
                { 1, 2, 3, 4, 5, 6, 7, 8 },
                { 1, 2, 3, 4, 5, 6, 7, 8 },
                { 1, 2, 3, 4, 5, 6, 7, 8 },
                { 1, 2, 3, 4, 5, 6, 7, 8 },
                { 1, 2, 3, 4, 5, 6, 7, 8 },
                { 1, 2, 3, 4, 5, 6, 7, 8 }
            };

            int[,] mtrasp = Traspuesta(matriz, new Región(0, 0, tamaño));

            for(int i = 0; i < tamaño; i++) {
                for(int j = 0; j < tamaño; j++) {
                    Console.Write(matriz[i, j].ToString() + " ");
                }
                Console.Write("\t");
                for(int j = 0; j < tamaño; j++) {
                    Console.Write(mtrasp[i, j].ToString() + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
