using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeScript : MonoBehaviour
{
    public ShapeAdjacentColliderScript[] adjacentShapeColliders; // [0] = x+1, [1] = x-1, [2] = y+1, [3] = y-1
    public GameObject selectedEffect;
    public bool isFalling;
    public bool ChainReactionCheck;
    public bool yCheck;
    public Rigidbody rb;
    [SerializeField] private float Yvelocity = 0;

    private State state;
    private enum State
    {
        Idle,
        Falling
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Yvelocity = rb.velocity.y;
        if (GameManager.instance.areShapesFalling)
        {
            if (rb.velocity.y < 0f) { state = State.Falling; }
            //if (yCheck && rb.velocity.y >= 0 /*&& rb.velocity.y < 0.05f*/) { GameManager.instance.CheckMatch(new ShapeScript[] { this }, true); }
        }
        else
        {
            state = State.Idle;
        }

        switch (state)
        {
            default:
            case State.Idle:
                break;
            case State.Falling:
                if (rb.velocity.y >= 0 && rb.velocity.y < 0.05f) { GameManager.instance.CheckMatch(new ShapeScript[] { this }, true); }
                break;
        }
    }

    void Swap(GameObject other)
    {
        bool check = false;
        
        foreach(ShapeAdjacentColliderScript col in adjacentShapeColliders)
        {
            if(col.shape == other) { check = true; }
        }

        if(check)
        {

        }
    }

    public void CheckMatch()
    {
        
    }

    public void OnCollisionEnter(Collision col)     // WORK ON THIS
    {
        //if (!GameManager.instance.areShapesFalling) { return; }
        //if (GetComponent<Rigidbody>().velocity.y < 0f) { return; }
        //if (col.gameObject.GetComponent<Rigidbody>().velocity.y != 0){ return; }
        //print("falling");

        //GameManager.instance.CheckMatch(new ShapeScript[] { this });
    }


}
