using Museo.Interfaces;

namespace Museo.Objetos
{
    /// <summary>
    /// Pintura de exhibición: hereda el comportamiento de pieza (sumar puntos + mostrar ficha).
    /// </summary>
    public class PinturaExhibicion : PiezaExhibicionBase
    {
        // Prompt específico para que se vea más claro qué pieza se está leyendo.
        public override string TextoAyuda => $"Presiona E para ver: {nombre}";
    }
}
