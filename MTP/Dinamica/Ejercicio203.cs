using System;
using System.Collections.Generic;
using System.Linq;

namespace MTP.Dinamica {
    /* 
     * Ejercicio 3
     * Sea un alfabeto S = {a, b, c} con la siguiente "tabla de multiplicación" (donde cada
     * fila corresponde al símbolo izquierdo y cada columna con el símbolo derecho):
     *     a b c
     *    -------
     * a | b b a
     * b | c b a
     * c | a c c
     * 
     * Nótese que dicha multiplicación no es asociativa ni conmutativa. Escribir un algoritmo
     * que dada una cadena x1, x2, ..., xn de caracteres de S, determine si es posible insertar
     * paréntesis en x de forma que el valor de la expresión resultante sea a. Por ejemplo, si
     * x = bbbba, al algoritmo debe devolver "cierto" dado que (b(bb))(ba) = (bb)c = bc = a.
     */
    public static class Ejercicio203 {
        static char Valor(char[,] tabla, char i, char j) {
            return tabla[(int)(i - 'a'), (int)(j - 'a')];
        }

        static char[,] Calcular(char[,] tabla, string lista, int tam) {
            char[,] r = new char[tam, tam];
            for(int i = 0; i < tam; i++) {
                for(int j = 0; j < tam; j++) {
                    if(i < j)
                        r[i, j] = ' ';
                    else if(i == j)
                        r[i, j] = lista[i];
                    else
                        r[i, j] = '-';
                }
            }

            for(int i = tam - 2; i >= 0; i--) {
                for(int j = i + 1; j < tam; j++) {
                    for(int k = i; k < j; k++) {
                        r[i, j] = Valor(tabla, r[i, k], r[k + 1, j]);
                        if(r[i, j] == 'a') break;
                    }
                }
            }
            return r;
        }

        static void MostrarFormula(char[,] r, char[,] tabla, string lista, int i, int j) {
            if(i + 1 < j) {
                for(int k = i; k < j; k++) {
                    if(r[i, j] == Valor(tabla, r[i, k], r[k + 1, j])) {
                        Console.Write("(");
                        MostrarFormula(r, tabla, lista, i, k);
                        Console.Write(" ");
                        MostrarFormula(r, tabla, lista, k + 1, j);
                        Console.Write(")");
                        return;
                    }
                }
            } else if(i < j) {
                Console.Write("(" + lista[i] + " " + lista[j] + ")");
            } else if(i == j) {
                Console.Write(lista[i]);
            }
        }

        static void Resolver(char[,] tabla, string lista) {
            char[,] r = Calcular(tabla, lista, lista.Length);
            Mostrar("Resultado:", r, lista.Length);

            if(r[0, lista.Length - 1] == 'a') {
                Console.WriteLine("Se ha encontrado solución.");
            } else {
                Console.WriteLine("No se ha encontrado solución.");
            }

            Console.Write("Formula: ");
            MostrarFormula(r, tabla, lista, 0, lista.Length - 1);
            Console.WriteLine("\n");
        }

        public static void Resolver() {
            char[,] tabla =
            {
                { 'b', 'b', 'a' },
                { 'c', 'b', 'a' },
                { 'a', 'c', 'c' },
            };
            Resolver(tabla, "bbbba");
            Resolver(tabla, "cbabca");
            Resolver(tabla, "abcacb");
            Resolver(tabla, "cbabcab");
        }

        static void Mostrar(string msj, char[,] tabla, int tam) {
            Console.WriteLine(msj);
            for(int i = 0; i < tam; i++) {
                for(int j = 0; j < tam; j++) {
                    Console.Write(tabla[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
