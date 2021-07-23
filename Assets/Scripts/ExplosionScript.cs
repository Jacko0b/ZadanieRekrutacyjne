using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    [SerializeField] private GameObject explosionVFX;
    [SerializeField] private Vector3 explosionOffset;

    [SerializeField] private float minForce = 25;
    [SerializeField] private float maxForce = 150;
    [SerializeField] private float radius = 5;

    void Start()
    {
        Explode();
    }
    public void Explode()
    {
        if(explosionVFX != null)
        {
            GameObject explosion = Instantiate(explosionVFX, transform.position + explosionOffset, Quaternion.identity) as GameObject;
            Destroy(explosion, 5);
        }
        foreach(Transform t in transform)
        {
            var rb = t.GetComponent<Rigidbody>();

            if(rb != null)
            {
                rb.AddExplosionForce(Random.Range(minForce, maxForce),transform.position,radius);
            }
        }

        Destroy(gameObject, 10);
    }
   
}
