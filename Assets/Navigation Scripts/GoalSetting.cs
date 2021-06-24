using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoalSetting : MonoBehaviour {

	//Create a slot in the inspector for the goal object.
	public Transform goal;
    
    //Call the NavMesh agent.
    private NavMeshAgent agent;

	//use this method for intialising
	void Start () {
        
        //Assign the navmesh agent.
        agent = GetComponent<NavMeshAgent>(); 

	}
	
	// Update is called once per frame
	void Update () {

    //make the agent move to the goal position
		agent.destination = goal.position; 

	}
}

//A script originally by Matt Cabanag, remade by Annabelle Macfarlene, and now redone by Cameron Edmond. Who shall be next to work on WMEC111? Let the legacy live on!
