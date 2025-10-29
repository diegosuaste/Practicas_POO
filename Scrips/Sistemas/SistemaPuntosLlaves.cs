using System;
using System.Collections.Generic;
using UnityEngine;

namespace Museo.Sistemas
{
    /// <summary>
    /// Sistema global (Singleton) que administra los puntos de aprendizaje y el inventario de llaves.
    /// Emite eventos cuando cambian los puntos o el conteo de llaves para que otros sistemas (UI/puertas) reaccionen.
    /// </summary>
    public class SistemaPuntosLlaves : MonoBehaviour
    {
        // Acceso global al sistema (Singleton).
        public static SistemaPuntosLlaves Instancia { get; private set; }

        [Header("Progreso")]
        [Tooltip("Puntos de aprendizaje acumulados por leer piezas (pinturas, etc.).")]
        [SerializeField] private int puntosAprendizaje = 0;

        [Tooltip("Permitir recoger llaves repetidas (útil para pruebas).")]
        [SerializeField] private bool permitirDuplicadas = false;

        // Inventario de llaves (privado por encapsulamiento).
        [SerializeField] private List<string> llaves = new List<string>();

        // Exposición de solo lectura del inventario (para UI / puerta).
        public List<string> Llaves => llaves;

        // Eventos hacia UI u otros sistemas.
        public event Action<int>    EventoPuntosActualizados;
        public event Action<string> EventoLlaveObtenida;
        public event Action<int>    EventoConteoLlavesActualizado;

        private void Awake()
        {
            // Patrón Singleton básico (uno por escena).
            if (Instancia != null && Instancia != this) { Destroy(gameObject); return; }
            Instancia = this;

            // Si quisieras persistir entre escenas:
            // DontDestroyOnLoad(gameObject);
        }

        /// <summary> Devuelve el total de puntos. </summary>
        public int ObtenerPuntos() => puntosAprendizaje;

        /// <summary> Suma puntos y notifica a quien esté suscrito (por ejemplo, la UI). </summary>
        public void AñadirPuntos(int cantidad)
        {
            puntosAprendizaje += Mathf.Max(0, cantidad);
            EventoPuntosActualizados?.Invoke(puntosAprendizaje);
            Debug.Log($"[SistemaPuntosLlaves] Puntos añadidos: {cantidad}. Total: {puntosAprendizaje}");
        }

        /// <summary> Intenta añadir una llave por nombre. Notifica y actualiza conteo. </summary>
        public void AñadirLlave(string nombreLlave)
        {
            if (string.IsNullOrWhiteSpace(nombreLlave)) return;

            if (permitirDuplicadas || !llaves.Contains(nombreLlave))
            {
                llaves.Add(nombreLlave);
                EventoLlaveObtenida?.Invoke(nombreLlave);
                EventoConteoLlavesActualizado?.Invoke(llaves.Count);
                Debug.Log($"[SistemaPuntosLlaves] Llave obtenida: {nombreLlave}. Total: {llaves.Count}");
            }
        }

        /// <summary> Consulta si se tiene una llave. </summary>
        public bool TieneLlave(string nombreLlave) => llaves.Contains(nombreLlave);

        /// <summary> Consume/remueve una llave. Devuelve true si se removió. </summary>
        public bool UsarLlave(string nombreLlave)
        {
            if (llaves.Remove(nombreLlave))
            {
                EventoConteoLlavesActualizado?.Invoke(llaves.Count);
                Debug.Log($"[SistemaPuntosLlaves] Llave usada: {nombreLlave}. Total: {llaves.Count}");
                return true;
            }
            return false;
        }
    }
}
