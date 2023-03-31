using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Instance Variables
    [SerializeField] private float maxHealth = 2f;
    private float _health;

    //public accessor for _health
    public float health
    {
        //health can be read publicly
        get
        {
            return _health;
        }
        //or set to a value within limits
        set
        {
            if (value > _health) Heal(value - _health);
            else Damage(_health - value);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        //TODO: Movement
    }

    ///<summary>Heals the player</summary>
    ///<param name="heal">The amount to heal</param>
    public void Heal(float heal)
    {
        _health += heal;
        _health = Mathf.Min(_health, maxHealth);
        //TODO: update Health UI
    }

    ///<summary>Damages the player</summary>
    ///<param name="dmg">The amount of health to remove</param>
    public void Damage(float dmg)
    {
        _health -= dmg;
        if (_health <= 0) Die();
        //TODO: update Health UI
    }

    private void Die()
    {
        //placeholder, simply freeze game
        Time.timeScale = 0;
    }
}
