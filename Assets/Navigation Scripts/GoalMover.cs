using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoalMover : MonoBehaviour {
    /** this class moves the goal object when an object tagged enemy enters the space. 
     *  it uses a raycast, which is like a beam (if it gets interupted it's a hit) to detect if things are on the navmesh area
     *  To make the code less expensive to run it's recommended that you try a centre point then try multiple times 
     *  in a sphere in a radius around that centre point. The larger the radius is the more expensive this will be to run
     **/

     //by making this a public float it becomes a decimal point that you can access in the inspector
    public float range = 10.0f;
    //setup a public boolean so you can access it within the inspector
    public bool Invisible = false;
    
    //Creates a public slot for assigning the agent linked to this goal
    public Transform agent;


    // Use this for initialization
    void Start()
    {
        //if you've ticked the invisible option
        if (Invisible == true)
        {
            //hide the mesh component of this object
            GetComponentInChildren<Renderer>().enabled = false;
        }

    }

    //a boolean based function which turns true or false if conditions are met, it needs a centre of a sphere, range and has to return a result
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        //for 30 times in the sphere around the centre point
        for (int i = 0; i < 30; i++)
        {
            //use the centre point along with the range to generate a point within a spherical shape. 
            Vector3 randomPoint = center + Random.insideUnitSphere * range;

            //this is a boolean used to check if the navmesh is hit by the raycast
            NavMeshHit hit;

            //if the random point hits within the NavMesh area
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                //return the hit location has the result of this boolean
                result = hit.position;
                //return true for the boolean (it's been a succeuss) and leave the function
                return true;
            }
        }
        //we shouldn't ever get here, but just in case this returns 0
        result = Vector3.zero;
        //and sets the boolean to false, saying that it's not within the area. Also exiting the method
        return false;
    }


    // Update is called once per frame
    void Update()
    {
    float distance = Vector3.Distance(agent.position, transform.position);
    if (distance <= 1) {
        
        Debug.Log("Collision!");
        //place holder for the (x,y,z) position 
        Vector3 point;
        //if RandomPoint function is true and has returned a number(which gets assigned to the variable point)
        if (RandomPoint(transform.position, range, out point))
        {
            //change the transform of this object to the point
            transform.position = point;
        }
    }

}
}

//A script originally by Matt Cabanag, remade by Annabelle Macfarlene, and now redone by Cameron Edmond. Who shall be next to work on WMEC111? Let the legacy live on!
