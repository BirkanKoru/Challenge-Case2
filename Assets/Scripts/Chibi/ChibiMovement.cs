using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChibiMovement : MonoBehaviour
{
    private Chibi chibi;
    private Rigidbody rb;
    [SerializeField] private float speed = 1f;
    
    private Vector3 dir;
    private bool canMove = false;

    private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        chibi = GetComponent<Chibi>();
        rb = GetComponent<Rigidbody>();

        gameController = GetComponentInParent<GameController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameController.GameStarted && !canMove)
        {
            chibi.State = Chibi.States.run;
            canMove = true;
        }

        if (canMove && speed > 0)
        {
            ForwardMovement();
        }
    }

    private void ForwardMovement()
    {
        dir = transform.forward * speed;
        dir.y = rb.velocity.y;
        rb.velocity = dir;
    }

    public float GetSpeed()
    {
        return this.speed;
    }

    public void SetSpeed(float val)
    {
        this.speed = val;
    }
}
