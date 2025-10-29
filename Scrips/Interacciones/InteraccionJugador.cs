using UnityEngine;
using Museo.Interfaces;   // ControlUI
using Museo.Objetos;      // InteractuableBase

namespace Museo.Interacciones
{
    /// <summary>
    /// Sistema de interacción del jugador en primera persona:
    /// - Raycast desde la cámara
    /// - Muestra prompt con el texto del objetivo
    /// - Llama a Interactuar(ui) al presionar E
    /// - Cierra la ficha al cambiar de objetivo o salir de rango
    /// </summary>
    public class InteraccionJugador : MonoBehaviour
    {
        [Header("Configuración de interacción")]
        [Tooltip("Cámara del jugador (desde donde sale el raycast).")]
        [SerializeField] private Camera camara;

        [Tooltip("Distancia máxima a la que se puede interactuar.")]
        [SerializeField] private float rangoInteraccion = 3f;

        [Tooltip("Tecla de interacción.")]
        [SerializeField] private KeyCode teclaInteraccion = KeyCode.E;

        [Tooltip("Capa(s) que contienen objetos interactuables. Asigna 'Interactuable'.")]
        [SerializeField] private LayerMask layerInteractuable = ~0;

        [Header("UI")]
        [Tooltip("Referencia al controlador de la UI.")]
        [SerializeField] private ControlUI controlUI;

        // Interactuable que se tiene actualmente en la mira
        private InteractuableBase objetivoActual;

        private void Update()
        {
            BuscarObjetivo();

            if (objetivoActual != null)
            {
                // Mostrar/actualizar prompt con el texto personalizado del objetivo
                controlUI?.MostrarPromptInteractuar(true, objetivoActual.TextoAyuda);

                // Interactuar
                if (Input.GetKeyDown(teclaInteraccion))
                {
                    objetivoActual.Interactuar(controlUI);
                }
            }
            else
            {
                // Sin objetivo a la vista: ocultar prompt y (por seguridad) la ficha
                controlUI?.MostrarPromptInteractuar(false);
            }
        }

        /// <summary>
        /// Realiza un raycast desde la cámara. Si cambia el objeto en mira, cierra la ficha anterior.
        /// </summary>
        private void BuscarObjetivo()
        {
            Ray rayo = new Ray(camara.transform.position, camara.transform.forward);

            if (Physics.Raycast(rayo, out RaycastHit hit, rangoInteraccion, layerInteractuable, QueryTriggerInteraction.Collide))
            {
                var nuevo = hit.collider.GetComponentInParent<InteractuableBase>();
                if (nuevo != objetivoActual)
                {
                    // Cambiaste de objetivo: cierra la ficha anterior para evitar que se quede pegada
                    controlUI?.OcultarFicha();
                    objetivoActual = nuevo;
                }
            }
            else
            {
                if (objetivoActual != null)
                {
                    // Saliste de rango: cierra ficha y limpia referencia
                    controlUI?.OcultarFicha();
                    objetivoActual = null;
                }
            }
        }

        private void OnDisable()
        {
            // Al desactivar el jugador, apaga prompt y ficha
            controlUI?.MostrarPromptInteractuar(false);
            controlUI?.OcultarFicha();
        }
    }
}
