using System;
using System.Collections.Generic;
using System.Linq;

namespace MTP.Ramificacion {
    /* 
     * La compañía discográfica NPI quiere sacar un LP con los grandes éxitos de uno de sus artistas
     * principales. Para ello dispone de M canciones a repartir entre las dos caras del LP. Se conocen
     * tanto el tiempo de cada canción como el tiempo de música que puede almacenar cada cara del LP.
     * 
     * Se pide:
     * Encontrar mediante un algoritmo de ramificación y poda que utilice un montículo la composición
     * de canciones del disco de tal forma que maximice el número de canciones. Especificar el árbol
     * de búsqueda. Suponed conocida la cota. (3 puntos)
     */
    public static class Ejercicio405 {
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
