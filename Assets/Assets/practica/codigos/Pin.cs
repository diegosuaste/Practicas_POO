using UnityEngine;

/// <summary>
/// Detecta si el pino ha sido derribado.
/// </summary>
public class Pin : MonoBehaviour
{
    // TODO: Umbral de inclinación (por ejemplo, más de 5 grados de inclinación)
    [Tooltip("Ángulo de inclinación a partir del cual se considera caído.")]
    private float umbralCaida = 5f; 

    public bool EstaCaido()
    {
        // PISTA: Calcular ángulo entre la orientación "arriba" del pino (transform.up) y el eje vertical del mundo (Vector3.up)
        float angulo = Vector3.Angle(transform.up, Vector3.up);

        // PISTA: Retornar true si el ángulo supera el umbral de caída
        // Un ángulo mayor a 5 grados significa que ya no está perfectamente vertical.
        return angulo > umbralCaida;
    }
}