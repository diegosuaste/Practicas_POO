using UnityEngine;
using Museo.Interfaces;
using Museo.Sistemas;

namespace Museo.Objetos
{
    /// <summary>
    /// Clase base para piezas de exhibición. Al interactuar, suma puntos y muestra la ficha.
    /// </summary>
    public abstract class PiezaExhibicionBase : InteractuableBase
    {
        [Header("Puntos por lectura")]
        [Tooltip("Puntos que otorga la pieza al leerla.")]
        [SerializeField] private int puntosPorLeer = 1;

        public override void Interactuar(ControlUI ui)
        {
            // 1) Sumar puntos
            SistemaPuntosLlaves.Instancia?.AñadirPuntos(puntosPorLeer);

            // 2) Mostrar ficha con los datos de la pieza
            base.Interactuar(ui);
        }
    }
}
