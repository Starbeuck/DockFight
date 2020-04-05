using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public ShipWeapon primaryWeapon;
    public ShipWeapon secondaryWeapon;

    // Update is called once per frame
    void Update()
    {
        //Shoot primary weapon
        if(Input.GetButton("Fire1") && primaryWeapon != null)
        {
            if(primaryWeapon.isPrecharged)
            {
                primaryWeapon.fire();
            }
            else if(Time.time >= primaryWeapon.nextTimeToFire)
            {
                primaryWeapon.nextTimeToFire = Time.time + 1f / primaryWeapon.fireRate;
                primaryWeapon.fire();
            }
        }

        //Shoot secondary weapon
        if(Input.GetButton("Fire2") && secondaryWeapon != null && Time.time >= secondaryWeapon.nextTimeToFire)
        {
            if (secondaryWeapon.isPrecharged)
            {
                secondaryWeapon.fire();
            }
            else if(Time.time >= secondaryWeapon.nextTimeToFire)
            {
                secondaryWeapon.nextTimeToFire = Time.time + 1f / secondaryWeapon.fireRate;
                secondaryWeapon.fire();
            }
        }

        //Reload precharded weapons
        if(Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Recharge");
            if(primaryWeapon.isPrecharged && !secondaryWeapon.readyToFire && primaryWeapon.prechargedProjectiles == null)
            {
                primaryWeapon.initiateProjectiles();
            }
            if(secondaryWeapon.isPrecharged && !secondaryWeapon.readyToFire)
            {
                secondaryWeapon.initiateProjectiles();
            }
        }
    }
}
