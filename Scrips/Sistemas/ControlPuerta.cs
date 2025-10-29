using UnityEngine;
using Museo.Objetos;      // InteractuableBase
using Museo.Interfaces;   // ControlUI
using Museo.Sistemas;     // SistemaPuntosLlaves

namespace Museo.Sistemas
{
    /// <summary>
    /// Control de puerta:
    /// - Fuerza estado 'PuertaCerrada' al iniciar (evita aperturas al arrancar)
    /// - Abre con trigger 'Abrir' si se tienen suficientes llaves
    /// - Opcional: abrir automáticamente al llegar al requisito de llaves
    /// </summary>
    public class ControlPuerta : InteractuableBase
    {
        [Header("Animación")]
        [Tooltip("Animator de la puerta.")]
        [SerializeField] private Animator animador;

        [Tooltip("Nombre del estado de Animator que representa la puerta cerrada (Default State).")]
        [SerializeField] private string estadoCerrada = "PuertaCerrada";

        [Tooltip("Nombre del parámetro Trigger que dispara la animación de apertura.")]
        [SerializeField] private string triggerAbrir = "Abrir";

        [Header("Requisitos")]
        [Tooltip("Cuántas llaves se requieren para abrir.")]
        [SerializeField] private int llavesNecesarias = 7;

        [Tooltip("Si está activo, la puerta se abre en cuanto se alcance el número de llaves.")]
        [SerializeField] private bool abrirAutomaticoAlTenerLlaves = false;

        public override string TextoAyuda => "Presiona E para abrir la puerta";

        private void Awake()
        {
            // "Blindaje" del Animator para no iniciar en Abrir:
            if (animador)
            {
                animador.Rebind();      // Resetea parámetros/estados
                animador.Update(0f);    // Aplica reset ahora
                if (!string.IsNullOrEmpty(triggerAbrir))
                    animador.ResetTrigger(triggerAbrir);

                // Fuerza el estado inicial a "cerrada"
                if (!string.IsNullOrEmpty(estadoCerrada))
                    animador.Play(estadoCerrada, 0, 0f);
            }
        }

        private void OnEnable()
        {
            if (abrirAutomaticoAlTenerLlaves && SistemaPuntosLlaves.Instancia != null)
                SistemaPuntosLlaves.Instancia.EventoConteoLlavesActualizado += OnConteoLlaves;
        }

        private void OnDisable()
        {
            if (SistemaPuntosLlaves.Instancia != null)
                SistemaPuntosLlaves.Instancia.EventoConteoLlavesActualizado -= OnConteoLlaves;
        }

        private void OnConteoLlaves(int total)
        {
            if (!abrirAutomaticoAlTenerLlaves) return;
            if (total >= llavesNecesarias) AbrirPuerta();
        }

        public override void Interactuar(ControlUI ui)
        {
            int total = SistemaPuntosLlaves.Instancia?.Llaves?.Count ?? 0;

            if (total >= llavesNecesarias)
            {
                AbrirPuerta();
            }
            else
            {
                int faltan = Mathf.Max(0, llavesNecesarias - total);
                ui?.MostrarMensajeTemporal($"Te faltan {faltan} llaves para abrir.");
            }
        }

        private void AbrirPuerta()
        {
            if (animador && !string.IsNullOrEmpty(triggerAbrir))
                animador.SetTrigger(triggerAbrir);
        }
    }
}
