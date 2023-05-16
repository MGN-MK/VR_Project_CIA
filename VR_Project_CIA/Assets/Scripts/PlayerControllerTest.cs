using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerControllerTest : MonoBehaviour
{
    public float grabDistance;
    public LayerMask capeTransitable;

    private NavMeshAgent playerAgent;
    private Ray lookRay;
    private RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        playerAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        // 0 para boton izquierdo, 1 para boton derecho, 2 para click central
        if (Input.GetMouseButtonDown(0))
        {
            lookRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(lookRay, out hit, maxDistance: grabDistance, (int)capeTransitable))
            {
                playerAgent.SetDestination(hit.point);
            }
        }
    }
}
