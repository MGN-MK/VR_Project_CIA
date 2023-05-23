using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float force;
    public Vector3 angle;
    public string batTag;

    public bool gotHit
    {
        get { return gotHit; }
    }

    private Rigidbody rb;
    private bool hitted;

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

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == batTag)
        {
            hitted = true;
        }
    }
}
