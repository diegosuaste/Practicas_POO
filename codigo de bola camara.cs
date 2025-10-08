using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlBola : MonoBehaviour
{
    public Transform CamaraPrincipal;

    public Vector3 offsetCamara = new Vector3(0f, 3f, -6f); // por encima y detrás de la bola
    public float suavizadoCamara = 5f;

    public Rigidbody rb;

    //variable para apuntar
    public float VelocidadDeApuntado = 5f;

    public float LimiteIzquierdo = -2f;

    public float LimiteDerecho = 2f;
    public float fuerzadelanzamiento = 10000f;
    // conyroles de flijo, que controlan otros elemntos de flujo
    private bool HazSidoLanzada = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {// expresa mientras que hazsido lanzado sea falso sea falso para Diparar
        if (HazSidoLanzada == false)
        {



            Apuntar();
            if (Input.GetKeyDown(KeyCode.Space))
            {// no esta modificando el metodo solo se esta utilizando
                Lanzar();
            }
        }
    }

    void Apuntar()
    {// intup get te permite registrar entradas y salidas de A y D , Flecha izquierda y Flecha derecha
        float inputHorizontal = Input.GetAxis("Horizontal");

        // mover la bola hacia los lados
        transform.Translate(Vector3.right * inputHorizontal * VelocidadDeApuntado * Time.deltaTime);
        // delimitar el movimiento de la bola
        Vector3 posicionActual = transform.position;


        posicionActual.x = Mathf.Clamp(posicionActual.x, LimiteIzquierdo, LimiteDerecho);
        transform.position = posicionActual;
    }
    // TRANSFONR POSITION ME PERMITE SABER CUAL ES LA POSICION ACTUAL DE LA ESCENA
    void Lanzar()
    {
        HazSidoLanzada = true;
        rb.AddForce(Vector3.forward * fuerzadelanzamiento);
        if (CamaraPrincipal != null)
        {
            CamaraPrincipal.SetParent(transform);
        }
    }

    
        

         
         


}
// bienvenido la entrada al infierno
//el bool funciona como swicth 
//¡Perfecto! Estás muy cerca de lograr lo que quieres. Entiendo que deseas:

Que la cámara siga a la bola por encima, dando una vista en tercera persona.

Pero que no se mueva bruscamente, es decir, que tenga un movimiento más suave o que no gire loco si la bola gira, etc.

Y que el movimiento de la cámara comience después del lanzamiento (HazSidoLanzada = true), como ya lo estás manejando.

Vamos a ajustar un poco tu lógica, sin romper tu estructura actual, para que:

La cámara siga a la bola con un pequeño delay o suavizado (Lerp).

No se mueva en exceso ni gire con la bola (es decir, siga solo la posición pero no la rotación de la bola).

Solo la siga una vez lanzada.

✅ Paso 1: Cambiar CamaraPrincipal.SetParent(transform);

El problema con hacer esto directamente es que la cámara hereda toda la rotación de la bola cuando la haces hija de ella. Lo ideal sería no parentarla, sino moverla suavemente con código.

✅ Paso 2: Crear una posición de cámara deseada

Agrega esto al final de tu script, fuera de las funciones pero dentro de la clase:

public Vector3 offsetCamara = new Vector3(0f, 3f, -6f); // por encima y detrás de la bola
public float suavizadoCamara = 5f;

✅ Paso 3: Agregar el movimiento de cámara en LateUpdate()

LateUpdate() es mejor para cámaras porque se actualiza después de que se mueven todos los objetos (incluyendo la bola).

Agrega esta función a tu clase:

void LateUpdate()
{
    if (HazSidoLanzada && CamaraPrincipal != null)
    {
        Vector3 posicionDeseada = transform.position + offsetCamara;
        CamaraPrincipal.position = Vector3.Lerp(CamaraPrincipal.position, posicionDeseada, suavizadoCamara * Time.deltaTime);
        
        // Opcional: si no quieres que rote con la bola
        CamaraPrincipal.LookAt(transform.position + Vector3.forward * 5f); // mira un poco adelante de la bola
    }
}

✅ Opcional: Ajustar el Offset desde el Inspector

Como offsetCamara es public, puedes mover la cámara en la escena hasta encontrar una buena posición, y copiar esa diferencia entre la bola y la cámara como valores x, y, z.

✅ Resultado

Antes del lanzamiento, la cámara no se mueve.

Al presionar Espacio, la cámara comienza a seguir a la bola.

No se pega completamente a la bola (no se mueve bruscamente).

Siempre mira hacia adelante en la pista.

¿Quieres que la cámara siga más desde arriba, tipo dron? ¿O más como una vista en tercera persona, más cerca del suelo? Puedo ayudarte a afinar el offset dependiendo del estilo que buscas.