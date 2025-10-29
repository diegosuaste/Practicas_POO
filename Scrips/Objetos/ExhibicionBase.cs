using UnityEngine;

namespace Museo.Objetos
{
    public abstract class ExhibicionBase : MonoBehaviour
    {
        [SerializeField] private string nombrePieza = "Pieza";
        [SerializeField, TextArea] private string descripcion = "Descripción de la pieza";
        [SerializeField] private bool visible = true; // estado interno
        [SerializeField] private int puntosAlInteractuar = 1; // puntos que otorga
        [SerializeField] private string llaveRequerida = ""; // nombre de llave requerida (si aplica)

        // Encapsulación: propiedades públicas de solo lectura
        public string Nombre => nombrePieza;
        public string Descripcion => descripcion;
        public bool EsVisible => visible;
        public int PuntosAlInteractuar => puntosAlInteractuar;
        public string LlaveRequerida => llaveRequerida;

        // Método polimórfico: cada derivada puede sobrescribirlo
        public virtual void MostrarInformacion()
        {
            Debug.Log($"Exhibición: {nombrePieza}\n{descripcion}");
        }

        // Método para ocultar/mostrar la pieza (controlado por sistema)
        public virtual void EstablecerVisibilidad(bool estado)
        {
            visible = estado;
            gameObject.SetActive(estado);
        }

        // Método que se ejecuta cuando el jugador interactúa
        public virtual void AlInteractuar()
        {
            // Por defecto solo muestra info; las clases derivadas pueden extender
            MostrarInformacion();
        }
    }
}
