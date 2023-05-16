using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float force;
    public Vector3 angle;

    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        transform.rotation = Quaternion.Euler(angle);
    }

    // Start is called before the first frame update
    void Start()
    {
        rb.AddForce(transform.forward * 100 * force);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
