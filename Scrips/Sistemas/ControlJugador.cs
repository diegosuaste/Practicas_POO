using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
//Movimiento
public class NewBehaviourScript : MonoBehaviour
{//Esta variable sirve para dar la velocidad de movimiento del jugador
    public float velocidad = 5f;
    public float gravedad = 9.8f;// esta variable sirve , para controlar la  gravedad de caidas
    private CharacterController controller;// es la pieza de lego que nos va permitir el movimiento en el juego
    private Vector3 velocidadVertical;//nos va permitir saber que tan rapido caemos

    //Variable Vista
    public Transform camara;//es para registrar que camara va a funcionar como ojos del jugador
    public float SensibilidadMouse = 200f;//es para la velocidad de la vista del jugador

    private float rotacionXVertical = 0f;//Es para indicar cuantosd grados va poder voltear hacia arriba o hacia abajo



    void Start()
    {
        controller = GetComponent<CharacterController>();// ESTO FUNCIONA PARA BUSCAR EL CHARACTER CONTROLLER
        Cursor.lockState = CursorLockMode.Locked;// ESTO SIRVE PARA BLOQUEAR EL PUNTERO 
        
            
        
    }

    // Update is called once per frame
    void Update()
    {
        ManejadorMovimiento();
        ManejadorVista();
    }


    void ManejadorVista()
    {
        //1 LEER EL INPUT DEL MOUSE
        float mouseX = Input.GetAxis("Mouse X") * SensibilidadMouse * Time.deltaTime;//REGISTRA EL DESPLAZAMIENTO CUANDO LOP MOVEMOS EN VERTICAL
        float mouseY = Input.GetAxis("Mouse Y") * SensibilidadMouse * Time.deltaTime;//REGISTRA EL DESPLAZAMIENTO CUANDO LOP MOVEMOS EN HORIZONTAL
        //2 CONSTRUIR LA ROTACION HORIZONTAL
        transform.Rotate(Vector3.up * mouseX);//registra desde un punto la cabeza del personaje para poder voltear 
        //3 REGISTRO DE LA ROTACION VERTICAL
        rotacionXVertical -= mouseY;
        //4 LIMPIAR LA ROTACION VERTICAL
        Mathf.Clamp(rotacionXVertical, -90f, 90f);//estoy delimitando el movimineto del cuello 
        //5 APLICAR LA ROTACIÒN
        //son los ejes x y
        camara.localRotation = Quaternion.Euler(rotacionXVertical, 0, 0);

    }

    void ManejadorMovimiento()
    {
        //1 leer el input de movimiento(WASD O LAS FLECHAS DE DIRECCION)
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");
        //2 crear el vector de movimiento
        //se almacena de forma local el registro de direccion de movimiento
        Vector3 direccion = transform.right * inputX + transform.forward * inputZ;
        //3Mover el CharacterController
        controller.Move(direccion * velocidad * Time.deltaTime);
        //4aplicar la gravedad 
        //Registro so estoy en el piso para un futiro cmportamiento de salto
        if (controller.isGrounded && velocidadVertical.y < 0)
        {
            velocidadVertical.y = -2f;//una pequeña fuerza hacia abajo para mantenerlo pegado al piso
        }
        velocidadVertical.y += gravedad * Time.deltaTime;// aplicamos la aceleracion de la gravedad

        controller.Move(velocidadVertical * Time.deltaTime);//Movemos el controlador hacia abajo
    }
}
