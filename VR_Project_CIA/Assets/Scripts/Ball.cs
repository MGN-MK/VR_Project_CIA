using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float lifeTime;
    public string batTag;
    public bool hitted = false;
    public bool pointsAdded = false;
    public float applyForce;

    private float timer = 0f;
    private bool missed = false;
    private Rigidbody rb;
    private PointsSystemManager pointsSystem;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        pointsSystem = FindAnyObjectByType<PointsSystemManager>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        rb.AddForce(transform.forward * 100 * applyForce);
    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;

        if(timer >= lifeTime && hitted == false && missed == false)
        {
            missed = true;
            pointsSystem.AddPoints(AreaType.Miss, gameObject);
            StartCoroutine(SelfDestroy());
        }
    }

    private IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == batTag && hitted == false)
        {
            hitted = true;
            pointsSystem.AddBall();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(hitted == true && pointsAdded == false && missed == false)
        {
            pointsAdded = true;
            pointsSystem.AddPoints(other.GetComponent<HitAreaType>().hitType, gameObject);
            StartCoroutine(SelfDestroy());
        }
    }
}
