using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Professor : MonoBehaviour
{

    private NavMeshAgent agent;
    private bool trackingPlayer = false;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GameObject target = GameObject.FindGameObjectWithTag("Player");
        
            StartCoroutine(TrackPlayer(target.transform));
    }

    private IEnumerator TrackPlayer(Transform target)
    {
        if (!trackingPlayer)
        {
            trackingPlayer = true;
            for (int i = 0; i < 4; i++)
            {
                agent.SetDestination(target.position);
                yield return new WaitForSeconds(0.5f);
            }
            trackingPlayer = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();  
        if (player != null)
        {
            player.Damage(3);
        }
    }
}
