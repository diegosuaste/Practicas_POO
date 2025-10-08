using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Practica : MonoBehaviour


{
    public float FuerzadeLanzamiento = 1000f;
    public float velocidadDeApuntado = 5f;
    public float limiteIzquierdo = 2f;
    public float limiteDerecho = 2f;

    private Rigidbody rb;
    private bool HazSidoLanzada = false;
    public bool CameraFollow;
    public bool ScoreManager; 

    // Start is called before the first frame update
    void Start()
    {
        rb = 
    }

    // Update is called once per frame
    void Update()
    {
        if (HazSidoLanzada)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                LanzarBola();
            }
        } 
    }
}
