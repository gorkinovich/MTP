using System;
using System.Collections.Generic;
using System.Linq;

namespace MTP.Dinamica {
    /* 
     * Supongamos que existen n bancos en los que podemos invertir, y disponemos de una cantidad
     * Cant para invertirla. Cada banco nos proporciona intereses según una función monótona
     * creciente, f(i,x), donde x es el importe a invertir e i el banco en el que se invierte.
     * 
     * Diseñad un algoritmo dinámico que encuentre la inversión óptima: la cantidad que se debe
     * invertir en cada banco para maximizar los intereses obtenidos.
     */
    public static class Ejercicio208 {
        static float[] L = { 8.0f, 6.0f, 7.0f, 9.0f, 5.0f };
        static float[] I = { 1.1f, 1.4f, 1.8f, 1.6f, 1.2f, 2.0f };

        static float f(int i, int x) {
            if(x >= L[i])
                return I[0] * (x - L[i]) + I[1 + i] * L[i];
            else
                return I[1 + i] * x;
        }
        
        static float[,] Calcular(int N, int C) {
            int MaxElem = N;
            int MaxCant = C + 1;
            float[,] tabla = new float[MaxElem, MaxCant];

            for(int i = 0; i < MaxElem; i++) {
                tabla[i, 0] = 0;
            }
            for(int i = 1; i < MaxCant; i++) { 
                tabla[0, i] = f(0, i);
            }

            float aux;
            for(int i = 1; i < MaxElem; i++) {
                for(int j = 1; j < MaxCant; j++) {
                    tabla[i, j] = tabla[i - 1, j];
                    for(int k = j; k >= 0; k--) {
                        aux = tabla[i - 1, j - k] + f(i, k);
                        if(aux > tabla[i, j]) {
                            tabla[i, j] = aux;
                        }
                    }
                }
            }
            //Mostrar(tabla, MaxElem, MaxCant);
            return tabla;
        }

        private static int[] Resultado(float[,] t, int N, int C) {
            int[] r = Enumerable.Range(0, N).Select(x => 0).ToArray();
            int i = N - 1;
            int j = C;
            const float alpha = 0.000001f;

            while(i > 0 && j > 0) {
                if(t[i, j] == t[i - 1, j]) {
                    i--;
                } else {
                    float aux;
                    for(int k = j; k >= 0; k--) {
                        aux = t[i, j] - (t[i - 1, j - k] + f(i, k));
                        if(aux < alpha) {
                            r[i] = k;
                            j -= k;
                            i--;
                            break;
                        }
                    }
                }
            }
            if(i == 0) {
                r[0] = j;
            }
            return r;
        }

        static void Resolver(int C) {
            const int N = 5;
            float[,] tabla = Calcular(N, C);
            int[] r = Resultado(tabla, N, C);
            Console.WriteLine("Cantidad: " + C);
            for(int i = 0; i < N; i++) {
                Console.WriteLine("Banco {0} = {1}", i + 1, r[i]);
            }
            Console.WriteLine();
        }

        public static void Resolver() {
            Resolver(10);
            Resolver(15);
            Resolver(20);
        }

        static void Mostrar(float[,] tabla, int MaxElem, int MaxCant) {
            Console.WriteLine("Tabla:");
            for(int i = 0; i < MaxElem; i++) {
                for(int j = 0; j < MaxCant; j++) {
                    Console.Write(tabla[i, j].ToString("00") + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
