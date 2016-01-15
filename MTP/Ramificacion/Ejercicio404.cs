using System;
using System.Collections.Generic;
using System.Linq;

namespace MTP.Ramificacion {
    /* 
     * Dado el mapa de una región, deseamos colorearlo de tal manera que dos regiones adyacentes, con
     * frontera común, no tengan el mismo color. Diseñad un procedimiento, mediante la metodología de
     * Ramificación y Poda, que determine el mínimo número de colores necesario para colorear el mapa,
     * detallando lo siguiente:
     * + La declaración de tipos y/o variables para representar la información del problema (0,5 puntos).
     * + El arbol de búsqueda (0,5 puntos).
     * + El código del procedimiento (2 puntos).
     * + La función cota (1 punto).
     */
    public static class Ejercicio404 {
        /*
        fun RpopMin (T : árbolDeEstados) dev <solución : tupla, coste : valor>
        var X, Y : nodo; C : colapr<nodo>
           Y := Raíz(T)
           C := CpVacía()
           Añadir(C, Y)
           coste := Y.CostePesimista
           mientras ¬EsCpVacía(C) y Mínimo(C).CosteOptimista <= coste hacer
              Y := Mínimo(C); EliminarMínimo(C)
              para todo hijo X de Y hacer
                 si EsSolución(X) entonces
                    si X.CosteReal <= coste entonces
                       coste := X.CosteReal
                       solución := X.Solución
                    fsi
                 sino si EsCompletable(X) y X.CosteOptimista <= coste entonces
                    Añadir(C, X)
                    coste := min {coste, X.CostePesimista}
                 fsi
              fpara
           fmientras
        ffun
        */

        //*****************************************************************

        public static void Resolver() {
            //TODO: ...
        }
    }
}
