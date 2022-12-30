using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject player;
    public UnityEngine.Vector3 offset;
    public float positionDamping = 0.5F;
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
        float currentAngle = transform.eulerAngles.y;
        float desiredAngle = player.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);

        Vector3 desiredPosition = player.transform.position + (rotation * offset);
        Vector3 position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * positionDamping);
        
        transform.position = position;

        Vector3 interestPosition = player.transform.position - 3*(rotation * offset);
        interestPosition[1] = player.transform.position[1];
        transform.LookAt(interestPosition);
    }
}
