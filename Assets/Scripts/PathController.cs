using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathController : MonoBehaviour
{
    public bool moving = false;
    public float speed;
    public UnityEngine.Vector3 direction;
    UnityEngine.Vector3 endPos;
    private double prevDistance;

    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Vector3 startPos = transform.position;
        endPos = transform.Find("end").position;
        direction = endPos - startPos;
        direction = Vector3.Normalize(direction);
        prevDistance = Mathf.Abs(Vector3.Distance(endPos, transform.position));
        speed = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (moving) {
            Quaternion rotation = transform.localRotation;
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            transform.Translate(direction * Time.deltaTime * speed);
            transform.localRotation = rotation;
            double distance = Mathf.Abs(Vector3.Distance(endPos, transform.position));
            if (prevDistance < distance) {
                moving = false;
                transform.position = endPos;
            }
            prevDistance = distance;
        }
    }

    public void changePosition() {
        moving = true;
    }
}
