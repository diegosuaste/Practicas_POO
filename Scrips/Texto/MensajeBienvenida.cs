using UnityEngine;
using TMPro;

public class MensajeBienvenida : MonoBehaviour
{
    public TextMeshProUGUI textoBienvenida;
    public float duracion = 3f; // duración en segundos

    void Start()
    {
        if (textoBienvenida != null)
        {
            textoBienvenida.gameObject.SetActive(true);
            Invoke("OcultarMensaje", duracion);
        }
    }

    void OcultarMensaje()
    {
        textoBienvenida.gameObject.SetActive(false);
    }
}
