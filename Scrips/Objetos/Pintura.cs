using UnityEngine;

namespace Museo.Objetos
{
    public class Pintura : ExhibicionBase
    {
        [SerializeField] private string artista = "Artista desconocido";
        [SerializeField] private int anio = 0;

        public override void MostrarInformacion()
        {
            // Polimorfismo: muestra información específica de pintura
            Debug.Log($"Pintura: {Nombre} - {artista} ({anio})\n{Descripcion}");
            // Aquí podrías reproducir un audio, mostrar UI especializada, etc.
        }

        public override void AlInteractuar()
        {
            base.AlInteractuar();
            // Comportamiento extra al interactuar con una pintura (si necesario)
        }
    }
}
