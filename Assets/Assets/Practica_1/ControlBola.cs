using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlBola : MonoBehaviour
{
    public Transform CamaraPrincipal;
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