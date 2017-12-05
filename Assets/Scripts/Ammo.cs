using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour {

    public Field field;
    public int rotateSpeed;

    void Start()
    {
        rotateSpeed = 50;
        field = transform.parent.GetComponent<Field>();
    }

     private void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime*rotateSpeed);
    } 

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            gameObject.SetActive(false);
        }
    }
}
