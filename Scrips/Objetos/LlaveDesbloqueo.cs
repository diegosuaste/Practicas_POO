using UnityEngine;
using Museo.Interfaces;
using Museo.Sistemas;

namespace Museo.Objetos
{
    /// <summary>
    /// Llave de desbloqueo: se añade al inventario y se muestra un mensaje temporal.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class LlaveDesbloqueo : InteractuableBase
    {
        [Header("Datos de la llave")]
        [Tooltip("Nombre único de esta llave (para evitar duplicados).")]
        [SerializeField] private string nombreLlave = "Llave";

        // Prompt personalizado para recoger llaves.
        public override string TextoAyuda => $"Presiona E para recoger: {nombreLlave}";

        public override void Interactuar(ControlUI ui)
        {
            // Si había una ficha abierta (de una pintura anterior), la ocultamos
            ui?.OcultarFicha();

            // Añadir la llave al sistema
            SistemaPuntosLlaves.Instancia?.AñadirLlave(nombreLlave);

            // Aviso al jugador
            ui?.MostrarMensajeTemporal($"Llave obtenida: {nombreLlave}");

            // Retirar la llave del mundo
            gameObject.SetActive(false);
            // O Destroy(gameObject) si prefieres destruirla.
        }
    }
}
