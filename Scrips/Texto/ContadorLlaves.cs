using UnityEngine;
using TMPro;

public class ContadorLlaves : MonoBehaviour
{
    public TextMeshProUGUI textoLlaves;
    public int llavesActuales = 0;
    public int totalLlaves = 7;

    void Start()
    {
        ActualizarTexto();
    }

    public void SumarLlave()
    {
        llavesActuales++;
        ActualizarTexto();

        if (llavesActuales >= totalLlaves)
        {
            Debug.Log("¡Has encontrado todas las llaves, ahora se a desbloqueado la puerta!");
           
        }
    }

    void ActualizarTexto()
    {
        if (textoLlaves != null)
        {
            textoLlaves.text = "Llaves: " + llavesActuales + " / " + totalLlaves;
        }
    }
}
