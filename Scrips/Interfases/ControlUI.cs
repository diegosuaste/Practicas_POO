using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Si usas TMP: using TMPro; y cambia Text -> TMP_Text
using Museo.Sistemas;

namespace Museo.Interfaces
{
    /// <summary>
    /// Control central de la UI del museo (Textos/TMP y paneles).
    /// Muestra/oculta ficha, prompt de interacción, puntos/llaves y mensajes temporales.
    /// </summary>
    public class ControlUI : MonoBehaviour
    {
        [Header("Bienvenida")]
        [Tooltip("GameObject del texto de bienvenida (puede estar desactivado al inicio).")]
        [SerializeField] private GameObject textoBienvenidaGO;
        [Tooltip("Componente de texto de bienvenida.")]
        [SerializeField] private Text textoBienvenida; // TMP_Text si usas TMP

        [Header("Objetivo")]
        [Tooltip("Texto que explica el objetivo (p. ej., 'Encuentra 7 llaves...').")]
        [SerializeField] private Text textoObjetivo; // TMP_Text

        [Header("Llaves")]
        [Tooltip("Texto que muestra el contador de llaves (formato: 'Llaves: X / 7').")]
        [SerializeField] private Text textoLlaves; // TMP_Text

        [Header("Panel de ficha")]
        [Tooltip("Panel que muestra la información de una pieza (título/descripcion).")]
        [SerializeField] private GameObject panelFicha;
        [Tooltip("Texto del título en la ficha.")]
        [SerializeField] private Text textoTituloFicha; // TMP_Text
        [Tooltip("Texto de la descripción en la ficha.")]
        [SerializeField] private Text textoDescripcionFicha; // TMP_Text

        [Header("Prompt de interacción")]
        [Tooltip("Panel/Texto que indica 'Presiona E para ...'.")]
        [SerializeField] private GameObject panelPrompt;
        [Tooltip("Texto dentro del prompt (mensaje dinámico).")]
        [SerializeField] private Text textoPrompt; // TMP_Text

        [Header("Mensajes temporales")]
        [Tooltip("Texto que aparece unos segundos para avisos (ej. 'Llave obtenida').")]
        [SerializeField] private Text textoMensajeTemporal; // TMP_Text
        [Tooltip("Duración por defecto del mensaje temporal en segundos.")]
        [SerializeField] private float duracionMensaje = 2f;

        private void Start()
        {
            // Estados iniciales seguros (evita que queden visibles si entras en Play varias veces).
            if (panelFicha       != null) panelFicha.SetActive(false);
            if (panelPrompt      != null) panelPrompt.SetActive(false);
            if (textoMensajeTemporal != null) textoMensajeTemporal.gameObject.SetActive(false);

            // Suscripciones a SistemaPuntosLlaves (si existe).
            if (SistemaPuntosLlaves.Instancia != null)
            {
                SistemaPuntosLlaves.Instancia.EventoPuntosActualizados       += ActualizarPuntos;
                SistemaPuntosLlaves.Instancia.EventoConteoLlavesActualizado  += ActualizarConteoLlaves;

                // Conteos iniciales
                ActualizarPuntos(SistemaPuntosLlaves.Instancia.ObtenerPuntos());
                ActualizarConteoLlaves(SistemaPuntosLlaves.Instancia.Llaves?.Count ?? 0);
            }
        }

        private void OnDestroy()
        {
            // Desuscribirse para evitar referencias inválidas al cerrar/jugar otra vez.
            if (SistemaPuntosLlaves.Instancia != null)
            {
                SistemaPuntosLlaves.Instancia.EventoPuntosActualizados      -= ActualizarPuntos;
                SistemaPuntosLlaves.Instancia.EventoConteoLlavesActualizado -= ActualizarConteoLlaves;
            }
        }

        /// <summary> Actualiza el texto de puntos (si decides mostrarlo). </summary>
        public void ActualizarPuntos(int total)
        {
            // Si tienes un texto de puntos, colócalo aquí. (Ejemplo:)
            // if (textoPuntos != null) textoPuntos.text = $"Puntos: {total}";
        }

        /// <summary> Muestra "Llaves: X / N" (N viene del diseño; X del sistema). </summary>
        public void ActualizarConteoLlaves(int total)
        {
            if (textoLlaves != null)
                textoLlaves.text = $"Llaves: {total} / 7"; // Cambia el 7 si el requisito cambia
        }

        /// <summary> Muestra el panel de ficha con el contenido indicado. </summary>
        public void MostrarFicha(string titulo, string descripcion)
        {
            if (panelFicha == null) return;
            textoTituloFicha.text     = titulo;
            textoDescripcionFicha.text= descripcion;
            panelFicha.SetActive(true);
        }

        /// <summary> Oculta el panel de ficha. </summary>
        public void OcultarFicha()
        {
            if (panelFicha == null) return;
            panelFicha.SetActive(false);
        }

        /// <summary> Muestra/Oculta el prompt de interacción con un texto opcional. </summary>
        public void MostrarPromptInteractuar(bool mostrar, string texto = "")
        {
            if (panelPrompt == null) return;
            panelPrompt.SetActive(mostrar);
            if (mostrar && !string.IsNullOrEmpty(texto))
                textoPrompt.text = texto;
        }

        /// <summary> Muestra un mensaje temporal durante 'duracionMensaje'. </summary>
        public void MostrarMensajeTemporal(string mensaje)
        {
            if (textoMensajeTemporal == null) return;
            StopAllCoroutines();
            StartCoroutine(MostrarMensajeCoroutine(mensaje));
        }

        private IEnumerator MostrarMensajeCoroutine(string mensaje)
        {
            textoMensajeTemporal.text = mensaje;
            textoMensajeTemporal.gameObject.SetActive(true);
            yield return new WaitForSeconds(duracionMensaje);
            textoMensajeTemporal.gameObject.SetActive(false);
        }
    }
}
