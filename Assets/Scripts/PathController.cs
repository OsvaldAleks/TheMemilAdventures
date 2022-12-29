using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathController : MonoBehaviour
{
    public float movementSteps = 100;
    public bool moving = false;
    public UnityEngine.Vector3 move;
    public UnityEngine.Vector3 endPos;

    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Vector3 startPos = transform.Find("start").position;
        endPos = transform.Find("end").position;
        move = (endPos - startPos)/movementSteps;
    }

    // Update is called once per frame
    void Update()
    {
        if (moving) {
            transform.position += move;
            movementSteps--;
            if (movementSteps == 0){
                moving = false;
                tag = "Untagged";
            }
        }
    }

    public void changePosition() {
        moving = true;
    }
}
