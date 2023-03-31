using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticHazard : MonoBehaviour
{
    //the damage each individual hazard deals can be set in the editor
    [SerializeField] private float _damage = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if the player collided with the hazard, damage the player
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().Damage(_damage);
        }
    }
}
