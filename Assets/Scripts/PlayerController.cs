using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10;
    public float rotateSpeed = 120;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(tranform.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        float dx = Input.GetAxis("Horizontal");
        float dy = Input.GetAxis("Vertical");
        // Vector3.forward je krajše za Vector3(0, 0, 1)
        transform.Translate(Vector3.forward * dy * Time.deltaTime * speed);
        transform.Rotate(Vector3.up * dx * Time.deltaTime * rotateSpeed);
    }
}
