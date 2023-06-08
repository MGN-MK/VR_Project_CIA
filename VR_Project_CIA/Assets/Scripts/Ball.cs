using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float force;
    public Vector3 angle;

    public float lifeTime = 5f;
    public string batTag;
    public bool hitted = false;
    public bool pointsAdded = false;

    private float timer = 0f;
    private AreaType areaType;
    private Rigidbody rb;
    private PointsSystemManager pointsSystem;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        transform.rotation = Quaternion.Euler(angle);
        pointsSystem = FindAnyObjectByType<PointsSystemManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb.AddForce(transform.forward * 100 * force);
    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;

        if(timer >= lifeTime)
        {
            SelfDestroy();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == batTag && hitted == false)
        {
            hitted = true;
            pointsSystem.AddBall();
            SelfDestroy();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(hitted == true && pointsAdded == false)
        {
            pointsAdded = true;
            pointsSystem.AddPoints(other.GetComponent<HitAreaType>().hitType);
        }
    }

    private void SelfDestroy()
    {
        Debug.Log(name + "has been destroyed.");
    }
}
