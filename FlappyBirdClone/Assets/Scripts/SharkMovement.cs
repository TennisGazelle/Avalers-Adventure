using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class SharkMovement : MonoBehaviour {

    Transform player;
    Transform shark;
    UnityEngine.AI.NavMeshAgent agent;
    bool wander = false;
    float randTimer = 10;
    float wanderTimer = 0;
    float wanderingTimer = 0;

	// Use this for initialization
	void Awake () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        shark = gameObject.transform;
        float randTimer = Random.Range(5f, 15f);
    }
	
	// Update is called once per frame
	void Update () {
        wanderTimer += Time.deltaTime;

        if (wanderTimer > randTimer && !wander)
        {
            wander = true;
            agent.SetDestination(new Vector3(
                player.position.x + Random.Range(-15, 15)
                , player.position.y
                , player.position.z + Random.Range(-15, 15)));
            Debug.Log("Wandering");
        }
        
        if (wander)
        {
            Wander();
        }

        else
        {
            PursuitPlayer();
        }
    }

    void PursuitPlayer()
    {
        agent.SetDestination(player.position);
    }

    void Wander()
    {
        float dist = agent.remainingDistance;

        if (dist != Mathf.Infinity
            && agent.pathStatus == NavMeshPathStatus.PathComplete
            && agent.remainingDistance == 0)
        {
            wander = false;
            wanderingTimer = 0f;
            randTimer = Random.Range(10f, 15f);
            Debug.Log("Pursuiting player");
        } 
    }


}
