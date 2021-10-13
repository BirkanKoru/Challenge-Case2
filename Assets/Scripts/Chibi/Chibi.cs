using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chibi : MonoBehaviour
{
    public enum States
    {
        idle,
        run,
        dance
    }

    private States state;
    public States State {  get { return state; } set { state = value; StateController(); } }
    private Animator anim;

    private ChibiMovement chibiMovement;

    void Start()
    {
        chibiMovement = GetComponent<ChibiMovement>();

        anim = GetComponent<Animator>();
        state = States.idle;
    }

    private void StateController()
    {
        switch (state)
        {
            case States.idle:
                anim.SetBool("run", false);
                break;
            case States.run:
                anim.SetBool("run", true);
                break;
            case States.dance:
                chibiMovement.SetSpeed(0f);
                anim.SetBool("run", false);
                anim.SetBool("dance", true);
                break;
        }
    }
}
