using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject player;
    public UnityEngine.Vector3 offset;
    public float rotateSpeed = 100;
    Vector3 rotation;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = player.transform.position + new Vector3(0, 1, -4);
    }
    void LateUpdate()
    {
        float rotate = Input.GetAxis("Mouse X");
        rotation = rotation + (Vector3.up * rotate * Time.deltaTime * rotateSpeed);

        Vector3 desiredPosition = (player.transform.position + (Quaternion.Euler(rotation) * offset));

        transform.position = desiredPosition;

        Vector3 interestPosition = player.transform.position - 3 * (Quaternion.Euler(rotation) * offset);
        interestPosition[1] = player.transform.position[1];
        transform.LookAt(interestPosition);
    }
}