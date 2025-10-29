using UnityEngine;

namespace Museo.Objetos
{
    public class Escultura : ExhibicionBase
    {
        [SerializeField] private string material = "Material desconocido";

        public override void MostrarInformacion()
        {
            Debug.Log($"Escultura: {Nombre} - Material: {material}\n{Descripcion}");
        }
    }
}
