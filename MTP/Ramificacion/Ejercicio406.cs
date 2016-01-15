using System;
using System.Collections.Generic;
using System.Linq;

namespace MTP.Ramificacion {
    /* 
     * Mochila [0,1] con múltiples elementos:
     * + Se dispone de n objetos y una mochila de capacidad C > 0.
     * + Para cada objeto, conocemos los siguientes datos:
     *   - El número de objetos del tipo i es m(i).
     *   - El peso del objeto i es w(i) > 0.
     *   - La inclusión del objeto i en la mochila produce un beneficio b(i) > 0.
     * + El objetivo consiste en llenar la mochila maximizando el valor de los objetos
     *   transportados sin sobrepasar la capacidad de la mochila.
     * + Los objetos no son fraccionables y suponemos que estan ordenados de mayor a menor
     *   en base a la relación beneficio/peso
     * + Esta es una variación del problema de la mochila [0,1] en la que se introduce la
     *   restricción de que existe un número limitado de cada tipo de objeto.
     */
    public static class Ejercicio406 {
        /*
        fun RpopMax (T : árbolDeEstados) dev <solución : tupla, coste : valor>
        var X, Y : nodo; C : colapr<nodo>
           Y := Raíz(T)
           C := CpVacía()
           Añadir(C, Y)
           coste := Y.CostePesimista
           mientras ¬EsCpVacía(C) y Máximo(C).CosteOptimista >= coste hacer
              Y := Máximo(C); EliminarMáximo(C)
              para todo hijo X de Y hacer
                 si EsSolución(X) entonces
                    si X.CosteReal >= coste entonces
                       coste := X.CosteReal
                       solución := X.Solución
                    fsi
                 sino si EsCompletable(X) y X.CosteOptimista >= coste entonces
                    Añadir(C, X)
                    coste := max {coste, X.CostePesimista}
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
