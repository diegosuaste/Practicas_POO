using UnityEngine;
using Museo.Sistemas; // Necesario para acceder al SistemaPuntosLlaves

namespace Museo.Objetos
{
    // Hereda de la clase ExhibicionBase
    public class ObjetoRecolectable : ExhibicionBase
    {
        [Header("Configuración Recolectable")]
        [Tooltip("Si es una llave, escribir el nombre de la llave que otorgará.")]
        [SerializeField] private string nombreLlaveADar = "";
        
        // Polimorfismo: Sobreescribe el comportamiento de la clase base
        public override void AlInteractuar()
        {
            // 1. Ejecutar el comportamiento base (muestra info si tiene, aunque para recolectable no es común)
            // base.AlInteractuar(); 

            // 2. Sumar puntos si aplica
            if (PuntosAlInteractuar > 0)
            {
                SistemaPuntosLlaves.Instancia.AñadirPuntos(PuntosAlInteractuar);
            }

            // 3. Otorgar llave si aplica
            // Si el campo nombreLlaveADar no está vacío, añade la llave al inventario.
            if (!string.IsNullOrEmpty(nombreLlaveADar))
            {
                SistemaPuntosLlaves.Instancia.AñadirLlave(nombreLlaveADar);
            }
            
            // 4. Destruir el objeto
            Destroy(gameObject);
        }
    }
}