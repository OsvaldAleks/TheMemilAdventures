using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 100;
    public float sprintSpeed = 200;
    public float rotateSpeed = 150;
    public float jumpForce = 2.0f;
    public Vector3 jump;
    public bool isGrounded;
    public int score = 0;
    public float movementRotation;
    public ArrayList milestone = new ArrayList();
    Rigidbody rb;
    GameObject cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera");
        rb = GetComponent<Rigidbody>();
        //at what scores should new pathes be layed
        milestone.Add(1);
        milestone.Add(3);
        //Debug.Log(tranform.ToString());
        movementRotation = cam.transform.eulerAngles.y;

        isGrounded = true;
        jump = new Vector3(0.0f, 2.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        float dx = Input.GetAxis("Horizontal");
        float dz = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        Vector3 vel;
        if (Input.GetKeyDown(KeyCode.LeftShift)){
            vel = new Vector3(dx, 0, dz) * sprintSpeed * Time.deltaTime;
        }
        else
            vel = new Vector3(dx, 0, dz) * speed * Time.deltaTime;
        if (vel != Vector3.zero)
        {
            //rotation of player movement is based on the roatation of the camera
            movementRotation = cam.transform.eulerAngles.y;

            //rotation of player model is dependent on direction of player movement
            float goal = cam.transform.eulerAngles.y;

            float turn = 0;
            if (dz >= 0)
            {
                if (dx > 0)
                    turn += 90;
                else if (dx < 0)
                    turn -= 90;
                if (dz != 0)
                    turn /= 2;
            }
            else
            {
                if (dx > 0)
                    turn += 135;
                else if (dx < 0)
                    turn -= 135;
                else
                    turn += 180;
            }

            goal = (goal + turn + 360) % 360;

            float offset1 = goal - transform.eulerAngles.y;
            float offset2 = goal + 360 - transform.eulerAngles.y;
            float offset3 = goal - 360 - transform.eulerAngles.y;

            if (Mathf.Abs(offset1) < Mathf.Abs(offset2) && Mathf.Abs(offset1) < Mathf.Abs(offset3))
                goal = goal - offset1 * 0.8f;
            else if (Mathf.Abs(offset2) < Mathf.Abs(offset3))
                goal = goal - offset2 * 0.8f;
            else
                goal = goal - offset3 * 0.8f;

            transform.eulerAngles = new Vector3(
                transform.eulerAngles.x,
                goal,
                transform.eulerAngles.z
            );
        }

        vel = Quaternion.Euler(0, movementRotation, 0) * vel;
        rb.velocity = vel + Vector3.up * rb.velocity.y;
        rb.angularVelocity = new Vector3(0, 0, 0);
        //Vector3.forward je krajše za Vector3(0, 0, 1)
        //transform.Translate(Vector3.forward * dy * Time.deltaTime * speed + Vector3.right * dx * Time.deltaTime * speed/2);

    }

    void OnCollisionEnter(Collision col)
    {
        //increase score if collision object is a cage
        if (col.gameObject.tag == "Cage")
        {
            score++;
            //
            if (col.gameObject.transform.Find("lock") != null && col.gameObject.transform.Find("lock").gameObject != null)
            {
                Destroy(col.gameObject.transform.Find("lock").gameObject);
            }
            if (milestone.Contains(score))
            {
                PathController pathScript = FindClosestPath().GetComponent<PathController>();
                pathScript.changePosition();
            }

            //untag cage, so it doesn't give any more points
            col.gameObject.tag = "Untagged";
        }
    }

    void OnCollisionStay()
    {
        if(rb.velocity[1] == 0)
            isGrounded = true;
    }

    public GameObject FindClosestPath()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Path");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

}
