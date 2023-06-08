using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AreaType
{
    Early, Perfect, Late
}

public class HitAreaType : MonoBehaviour
{
    public AreaType hitType;
    public BoxCollider colliderArea;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, colliderArea.size);
    }
}