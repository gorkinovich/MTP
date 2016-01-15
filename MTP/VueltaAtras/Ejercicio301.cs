using System;
using System.Collections.Generic;
using System.Linq;

namespace MTP.VueltaAtras {
    /* 
     * Consideremos la siguiente ecuación:
     *         C(n)X(n) + C(n-1)X(n-1) + ... + C(1)X(1) + C(0) = 0
     *                 0 < X(i) < d(i), ∀i = 1..n
     * 
     * Donde C(i) y d(i) (C(i), d(i) ϵ R, ∀i = 1..n) son conocidos. Diseña un algoritmo de vuelta
     * atrás que muestre todas las soluciones sabiendo que X(i) ϵ Z, ∀i = 1..n. Detalla lo siguiente:
     * + La declaración de tipos y/o variables para representar la información del problema (0,5 puntos).
     * + El árbol de búsqueda: significado de las aristas y de los niveles (0,5 puntos).
     * + El código del procedimiento (1,5 puntos).
     * + El programa llamador (0,5 puntos).
     */
    public static class Ejercicio301 {
        static void Calcular(float[] C, float[] D, int[] X, int i, float v) {
            if(i < D.Length) {
                float aux;
                for(int n = 0; n < D[i]; n++) {
                    X[i] = n;
                    aux = v + (C[i] * (float)X[i]);
                    Calcular(C, D, X, i + 1, aux);
                }
            } else {
                if(v + C[i] == 0.0f) {
                    Mostrar(X);
                }
            }
        }

        static void Mostrar(int[] X) {
            Console.WriteLine("Solución: {0}",
                              X.Select(x => x.ToString())
                               .Aggregate((x, xs) => x + " " + xs));
        }

        public static void Resolver() {
            float[] C = { 1, -2, 3, -4, 5 };
            float[] D = { 10, 8, 6, 4 };
            int[] X = Enumerable.Range(0, D.Length).Select(x => 0).ToArray();
            Calcular(C, D, X, 0, 0.0f);
        }
    }
}
