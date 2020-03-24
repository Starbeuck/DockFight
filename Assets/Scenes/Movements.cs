using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movements : MonoBehaviour
{
  
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * 90.0f;
        transform.Rotate(Input.GetAxis("Vertical"), 0.0f, -Input.GetAxis("Horizontal"));   
    }
}
