using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10;
    public float rotateSpeed = 120;
    public int score = 0;
    public ArrayList milestone = new ArrayList(); 
    // Start is called before the first frame update
    void Start()
    {
        //at what scores should new pathes be layed
        milestone.Add(1);
        milestone.Add(3);
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

    void OnCollisionEnter(Collision col) {
        //increase score if collision object is a cage
        if (col.gameObject.tag == "Cage") { 
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
