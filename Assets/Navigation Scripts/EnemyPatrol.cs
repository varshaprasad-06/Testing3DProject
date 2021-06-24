using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour {
    //a list of transforms which can be assigned from GameObjects through the inspector
    public Transform[] points;
    // a number which contains how many seconds to wait for
    public float delay;
    //a copy of delay so we can change it
    private float timer;
    //current destination point
    private int destPoint = 0;
    //this retrieves the NavMeshAgent component from the enemy components
    private NavMeshAgent agent;
    //a boolean (true or false) which gives you the ability to reverse the order of the list
    public bool reverseOrder = false;
    //a boolean to turn off autobreaking (slowing down towards a point) by default this is on
    public bool braking = true;
    //Boolean (true or false) to check wether or not it's found the next point
    bool isFindingPath = false;



    void Start()
    {
        //if you've clicked reverse order in the inspector
        if (reverseOrder)
        {
            //reverse the list of points
            System.Array.Reverse(points);
        }

        //retrieve the navmeshagent component from this game object
        agent = GetComponent<NavMeshAgent>();

        //an if statement which says turn off the autobreaking if you clicked it off in the inspector
       if(!braking)
        {
            agent.autoBraking = false;
        }
      

        //runs the method below which makes it go to the next poitn
        GotoNextPoint();
    }


    void GotoNextPoint()
    {
        // Returns if no points have been set up, this is a good escape (limits mistakes breakings things)
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        // a % means the left over amount of destination point divided by the lenght of the list of points (this allows it to loop)
        destPoint = (destPoint + 1) % points.Length;
        //reset the timer to the delay
        timer = delay;
    }


    void Update()
    {
        /**Choose the next destination point when the agent gets less than half a unit away and it's no longer working out a path
         * start traveling to the next point 
         **/
        if (!agent.pathPending && agent.remainingDistance < 0.5f)

            //wait if there's still timer left
            if( timer > 0)
            {
                //stop the agent
                agent.isStopped = true;
                //minus time from the timer
                timer -= Time.deltaTime;
            }

        else
            {
                //allow the agent to move again
                agent.isStopped = false;
                //then go to next point
                GotoNextPoint();
            }
           
    }
}

//A script originally by Matt Cabanag, remade by Annabelle Macfarlene, and now redone by Cameron Edmond. Who shall be next to work on WMEC111? Let the legacy live on!
