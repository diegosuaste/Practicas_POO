using UnityEngine;
using Museo.Interfaces; // ControlUI

namespace Museo.Objetos
{
    /// <summary>
    /// Clase base para TODO objeto interactuable del museo.
    /// Define los campos comunes y la firma de interacción que utiliza la UI.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public abstract class InteractuableBase : MonoBehaviour
    {
        [Header("Datos comunes")]
        [Tooltip("Nombre que aparece como título en la ficha.")]
        [SerializeField] protected string nombre;

        [Tooltip("Descripción que aparece en la ficha.")]
        [TextArea(2, 6)] [SerializeField] protected string descripcion;

        [Tooltip("Texto que se muestra en el prompt de interacción.")]
        [SerializeField] protected string textoAyuda = "Presiona E para interactuar";

        /// <summary> Texto que verá el jugador en el prompt. </summary>
        public virtual string TextoAyuda => textoAyuda;

        /// <summary>
        /// Comportamiento por defecto al interactuar: mostrar la ficha con nombre/descripcion.
        /// Clases derivadas pueden sobrescribirlo para añadir lógica adicional.
        /// </summary>
        public virtual void Interactuar(ControlUI ui)
        {
            ui?.MostrarFicha(nombre, descripcion);
        }
    }
}
