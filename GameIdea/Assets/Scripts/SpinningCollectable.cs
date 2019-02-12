using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningCollectable : MonoBehaviour

{
    public float amount = 50f;


    private Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }




    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal") * amount * Time.deltaTime;
        float v = Input.GetAxis("Vertical") * amount * Time.deltaTime;


        rb.AddTorque(transform.up * 10);
       // rb.AddTorque(transform.right * 10);
    }
}


