using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationFloat : MonoBehaviour
{
    public Animator anim;
    public float speed = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal") ;
        float vertical = Input.GetAxis("Vertical") ;

        if (vertical > 0 || vertical < 0)
        {
            anim.SetFloat("Velocidad", vertical);
        }

    }
}
