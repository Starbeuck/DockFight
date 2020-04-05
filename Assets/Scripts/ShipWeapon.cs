using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipWeapon : MonoBehaviour
{
    [Header("Weapon stats")]
    public int damage;
    public float fireRate;
    public float speed;
    public float coolDown; //Can be 0 if no cooldown
    private float startCoolDown;
    public bool isFiring;
    public bool isPrecharged; //Case for missiles or bombs
    public bool readyToFire; //Case for weapons with cooldown
    [Space]
    [Header("Associated objets")]
    public GameObject projectilePrefab;
    public Transform[] shootingPoints;
    [Space]
    public Transform[] projectilesPositions; //For precharged weapons only
    public List<Projectile> prechargedProjectiles; //For precharged weapons only
    public Transform target;

    public float nextTimeToFire = 0f;

    //Coroutine
    public Coroutine firingCoroutine;

    private void Awake()
    {
        if(isPrecharged)
        {
            initiateProjectiles();
        }
    }

    private void Update()
    {
        
    }

    public void fire()
    {
        if(isPrecharged && !isFiring)
        {
            isFiring = true;
            firingCoroutine = StartCoroutine(LaunchAllMissiles());
        }
        else if(shootingPoints != null)
        {
            for (int i = 0; i < shootingPoints.Length; i++)
            {
                GameObject bulletGO = Instantiate(projectilePrefab, shootingPoints[i].position, shootingPoints[i].rotation);
                bulletGO.GetComponentInChildren<Projectile>().bulletSpeed = speed;
                bulletGO.GetComponentInChildren<Projectile>().damage = damage;
            }
        }
    }

    public void initiateProjectiles()
    {
        for (int i = 0; i < projectilesPositions.Length; i++)
        {
            GameObject missileGO = Instantiate(projectilePrefab, projectilesPositions[i]);
            missileGO.GetComponentInChildren<Projectile>().target = target;
            missileGO.GetComponentInChildren<Projectile>().bulletSpeed = speed;
            missileGO.GetComponentInChildren<Projectile>().damage = damage;
            prechargedProjectiles.Add(missileGO.GetComponentInChildren<Projectile>());
        }
        readyToFire = true;
    }

    //Lauch missiles
    IEnumerator LaunchAllMissiles()
    {
        readyToFire = false;

        for (int i = 0; i < prechargedProjectiles.Count; i++)
        {
            prechargedProjectiles[i].transform.parent = null;
            prechargedProjectiles[i].isLaunched = true;
            if (target != null)
            {
                prechargedProjectiles[i].target = target;
            }
            yield return new WaitForSeconds(0.1f); // wait till the next round
        }
        prechargedProjectiles.Clear();
        isFiring = false;
    }
}
