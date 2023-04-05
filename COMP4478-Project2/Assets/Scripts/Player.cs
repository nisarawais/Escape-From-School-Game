using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;

public class Player : MonoBehaviour
{
    //Instance Variables
    [SerializeField] private float maxHealth = 2f;
    private float _health;

    private Rigidbody2D rigidBody;
    public float speed = 5f;
    public float speedJump = 10f;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundMask;
    private bool isTouchingGround;

    private Animator playerAnimation;

    private Vector3 respawnPoint;
    public GameObject fallDetector;

    public TextMeshProUGUI healthUI;

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
        rigidBody = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<Animator>();
        UpdateHealth();
        respawnPoint = transform.position;
    }

    private void UpdateHealth()
    {
        healthUI.SetText("Health: " + _health);
    }

    // Update is called once per frame
    void Update()
    {
        //Movement
        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundMask);
        float inputX = UnityEngine.Input.GetAxis("Horizontal");
        if (inputX > 0f)
        {
            rigidBody.velocity = new Vector2(inputX * speed, rigidBody.velocity.y);
            transform.localScale = new Vector2(1f, 1f);
        } else if(inputX < 0f)
        {
            rigidBody.velocity = new Vector2(inputX * speed, rigidBody.velocity.y);
            transform.localScale = new Vector2(-1f, 1f);
        } else if (inputX == 0f || Mathf.Abs(rigidBody.velocity.x) < 0.1f)
        {
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
        }

        if (UnityEngine.Input.GetButtonDown("Jump") && isTouchingGround)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, speedJump);
        }
        //Player Animations
        playerAnimation.SetFloat("Speed", Mathf.Abs(rigidBody.velocity.x));
        playerAnimation.SetFloat("SpeedY", rigidBody.velocity.y);
        playerAnimation.SetBool("OnGround", isTouchingGround);

        //Move the fall detector with the player
        fallDetector.transform.position = new Vector2(transform.position.x, fallDetector.transform.position.y);
    }

    private void FixedUpdate()
    {

    }

    ///<summary>Heals the player</summary>
    ///<param name="heal">The amount to heal</param>
    public void Heal(float heal)
    {
        _health += heal;
        _health = Mathf.Min(_health, maxHealth);
        //TODO: update Health UI
        UpdateHealth();
    }

    ///<summary>Damages the player</summary>
    ///<param name="dmg">The amount of health to remove</param>
    public void Damage(float dmg)
    {
        _health -= dmg;
        if (_health <= 0) Die();
        //TODO: update Health UI
        UpdateHealth();
    }

    private void Die()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
;   }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("FallDetector"))
        {
            Die();
        }
        if (collision.CompareTag("LevelEnd"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            transform.position = respawnPoint;
        }
    }
}
