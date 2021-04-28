using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject expEffect;
    public float speed = 2000.0f;
    public string shooter;

    void Start()
    {
        GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * speed);        
    }

    void OnCollisionEnter(Collision coll)
    {
        GameObject obj = Instantiate(expEffect,
                                     transform.position,
                                     Quaternion.identity);
        Destroy(obj, 3.0f);
    }
}
