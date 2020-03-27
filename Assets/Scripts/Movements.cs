using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movements : MonoBehaviour
{
  
    void Start() { }


    //this is our target velocity while decelerating
    float initialVelocity = 0.0f;

    //this is our target velocity while accelerating
    float finalVelocity = 10.0f;

    //this is our current velocity
    float currentVelocity = 0.0f;

    //this is the velocity we add each second while accelerating
    float accelerationRate = 1.20f;

    //this is the velocity we subtract each second while decelerating
    float decelerationRate = 0.7f;



    // Update is called once per frame
    void Update()
    {
        // Make the ship go forward at a given speed
        //transform.position += transform.forward * Time.deltaTime * 90.0f;
        //Make the ship pitch and roll 
        transform.Rotate(Input.GetAxis("Vertical"), 0.0f, -Input.GetAxis("Horizontal"));



        ///

        if (Input.GetKey(KeyCode.Space))
        {
            //add to the current velocity according while accelerating
            currentVelocity = currentVelocity + (accelerationRate * Time.deltaTime);
            transform.Translate(0, 0, currentVelocity);
        }
        else
        {
            //subtract from the current velocity while decelerating
            currentVelocity = currentVelocity - (decelerationRate * Time.deltaTime);
            if (currentVelocity > 0)
            {
                transform.Translate(0, 0, currentVelocity);
            }
            else
            {
                transform.Translate(0, 0, 0);
            }
        }

        //ensure the velocity never goes out of the initial/final boundaries
        currentVelocity = Mathf.Clamp(currentVelocity, initialVelocity, finalVelocity);

        //propel the object forward


        ///pitch = Input.GetAxis("Vertical");
        // roll = -Input.GetAxis("Horizontal");
        //yaw = Input.GetAxis("Yaw");
        // = Input.GetAxis("Throttle");




    }

}
