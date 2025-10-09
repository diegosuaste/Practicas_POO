using UnityEngine;
using UnityEngine.UI; // Necesario para la clase Text

/// <summary>
/// Calcula cuántos pinos están caídos y muestra el puntaje en pantalla.
/// </summary>
public class ScoreManager : MonoBehaviour
{
    // TODO: Texto UI - Asignar un componente Text (o TextMeshPro) en el Inspector
    public Text textoPuntaje;

    // TODO: Variables internas
    private int puntajeActual = 0;
    private Pin[] pinos; // Arreglo para almacenar todos los scripts de Pin

    void Start()
    {
        // PISTA: Buscar todos los objetos que tengan el componente Pin
        pinos = FindObjectsOfType<Pin>();
        
        // Inicializar el texto
        CalcularPuntaje();
    }

    public void CalcularPuntaje()
    {
        int puntaje = 0;
        
        // PISTA: Revisar cada pino si está caído
        foreach (Pin pin in pinos)
        {
            if (pin.EstaCaido())
            {
                puntaje++;
            }
        }
        
        puntajeActual = puntaje;
        
        // PISTA: Actualizar texto del puntaje (validar si textoPuntaje != null)
        if (textoPuntaje != null)
        {
            textoPuntaje.text = "Pinos Caídos: " + puntajeActual.ToString();
        }
        else
        {
             Debug.LogWarning("El textoPuntaje UI no está asignado en ScoreManager.");
        }
    }
}