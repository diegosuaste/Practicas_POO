using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTest : MonoBehaviour
{
    public Animator anim;
    public KeyCode teclaCaminarEnfrente;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(teclaCaminarEnfrente))
        {
            anim.SetBool("caminarB", true);
        }
        else
        {
            anim.SetBool("caminarB", false);
        }
    }
}
