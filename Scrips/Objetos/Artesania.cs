using UnityEngine;

namespace Museo.Objetos
{
    public class Artesania : ExhibicionBase
    {
        [SerializeField] private string region = "Región";

        public override void MostrarInformacion()
        {
            Debug.Log($"Artesanía: {Nombre} - Región: {region}\n{Descripcion}");
        }
    }
}
