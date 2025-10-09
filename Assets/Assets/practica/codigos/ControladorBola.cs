using UnityEngine;

/// <summary>
/// Controla el movimiento lateral y el lanzamiento de la bola.
/// También se comunica con la cámara y el sistema de puntuación.
/// </summary>
public class ControladorBola : MonoBehaviour
{
    // TODO: Fuerza con la que se lanza la bola
    public float fuerzaDeLanzamiento = 1000f;

    // TODO: Velocidad y límites para el apuntado
    public float velocidadDeApuntado = 5f;
    public float limiteIzquierdo = -2f;
    public float limiteDerecho = 2f;

    // TODO: Referencias internas
    private Rigidbody rb;
    private bool haSidoLanzada = false;

    // TODO: Referencia a la cámara y score
    public CameraFollow cameraFollow;
    public ScoreManager scoreManager;

    void Start()
    {
        // PISTA: Obtener el componente Rigidbody de esta bola
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!haSidoLanzada)
        {
            Apuntar();
            // PISTA: Si se presiona espacio, lanzar la bola
            if (Input.GetKeyDown(KeyCode.Space))
            {
                LanzarBola();
            }
        }
    }

    void Apuntar()
    {
        // PISTA: Obtener el movimiento horizontal (-1 a 1)
        float inputHorizontal = Input.GetAxis("Horizontal");

        // PISTA: Mover la bola lateralmente
        transform.Translate(Vector3.right * inputHorizontal * velocidadDeApuntado * Time.deltaTime);

        // PISTA: Limitar el movimiento dentro de los bordes del carril
        Vector3 posicionActual = transform.position;
        posicionActual.x = Mathf.Clamp(posicionActual.x, limiteIzquierdo, limiteDerecho);
        transform.position = posicionActual;
    }

    void LanzarBola()
    {
        haSidoLanzada = true;
        // Lanzar la bola aplicando una fuerza hacia adelante
        if (rb != null)
        {
            rb.AddForce(Vector3.forward * fuerzaDeLanzamiento);
        }
        else
        {
            Debug.LogWarning("El Rigidbody no está asignado en " + gameObject.name);
        }

        // PISTA: Iniciar seguimiento de la cámara (si existe)
        if (cameraFollow != null) cameraFollow.IniciarSeguimiento();
    }

    void OnCollisionEnter(Collision collision)
    {
        // PISTA: Si colisiona con un pino (solo se ejecuta una vez)
        if (collision.gameObject.CompareTag("Pin"))
        {
            // PISTA: Detener seguimiento de cámara (si no es null)
            if (cameraFollow != null) cameraFollow.DetenerSeguimiento();

            // PISTA: Calcular puntaje tras un pequeño retraso (2 segundos)
            if (scoreManager != null) Invoke("CalcularPuntaje", 2f);
        }
    }

    void CalcularPuntaje()
    {
        // PISTA: Llamar al ScoreManager para actualizar puntos
        if (scoreManager != null)
        {
            scoreManager.CalcularPuntaje();
        }
    }
}