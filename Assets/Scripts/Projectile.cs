using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage;
    public float bulletSpeed;
    public float rotationSpeed;

    public bool isHomming;
    public bool isLaunched;
    public bool isPrecharged;
    public Transform target;

    private bool isBeingDestroyed;

    //Associated objetcs
    public Rigidbody rb;
    public ParticleSystem[] particleEffects;
    public GameObject particleImpact;
    public ParticleSystem muzzleFlash;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (!isPrecharged)
        {
            rb.AddForce(transform.forward * bulletSpeed);
        } 
    }

    void FixedUpdate()
    {
        if (isHomming && isLaunched)
        {
            chaseTarget();
            for (int i = 0; i < particleEffects.Length; i++)
            {
                particleEffects[i].Play();
            }
        }
    }

    public void chaseTarget()
    {
        if (target == null)
        {
            return;
        }

        rb.velocity = transform.forward * bulletSpeed;
        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
        targetRotation = new Quaternion(targetRotation.x, targetRotation.y, targetRotation.z, targetRotation.w);
        rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed));
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.transform.tag.Equals("Target") || other.transform.tag.Equals("Obstacle")) && !isBeingDestroyed)
        {
            if(particleImpact != null)
            {
                isBeingDestroyed = true;
                GameObject explosionGO = Instantiate(particleImpact, transform.position, Quaternion.identity);
                Destroy(explosionGO, 2.0f);
            }
            Destroy(gameObject);
        }
    }
}
