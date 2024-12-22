using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FloatingFloor : MonoBehaviour
{
   [SerializeField]private GameObject[] waypoints;
   private int currentWayPointIndex;
   [SerializeField] private float speed;
    void Update()
    {
        if (Vector3.Distance(transform.position , waypoints[currentWayPointIndex].transform.position) < .1f)
        {
            currentWayPointIndex ++;

            if (currentWayPointIndex >= waypoints.Length)
            {
                currentWayPointIndex = 0;
            }
        }

    transform.position = Vector3.MoveTowards(transform.position , waypoints[currentWayPointIndex].transform.position , speed*Time.deltaTime);
    
    }
// sticking player to the floor when jumped on moving or floating floor
    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name=="Body")
        {
        other.gameObject.transform.SetParent(transform);
        }
    }
    public void OnCollisionExit(Collision other)
    {
        if (other.gameObject.name=="Body")
        {
            other.gameObject.transform.SetParent(null);
        }
    }



}
