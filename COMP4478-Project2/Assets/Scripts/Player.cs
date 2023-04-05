using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;
using UnityEngine.AI;

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
    private bool gotKey;

    public int numOfHearts;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public TextMeshProUGUI BookCounter;

    public int levelScore;

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
        _health = Manager.health;
        rigidBody = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<Animator>();
        UpdateHealth();
        respawnPoint = transform.position;
        gotKey = false;
        levelScore = 0;
        BookCounter.text = Score.score.ToString();
    }

    private void UpdateHealth()
    {
        //healthUI.SetText("Health: " + _health);
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            } else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            } else
            {
                hearts[i].enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (health > 0)
        {
            //Movement
            isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundMask);
            float inputX = UnityEngine.Input.GetAxis("Horizontal");
            if (inputX > 0f)
            {
                rigidBody.velocity = new Vector2(inputX * speed, rigidBody.velocity.y);
                transform.localScale = new Vector2(1f, 1f);
            }
            else if (inputX < 0f)
            {
                rigidBody.velocity = new Vector2(inputX * speed, rigidBody.velocity.y);
                transform.localScale = new Vector2(-1f, 1f);
            }
            else if (inputX == 0f || Mathf.Abs(rigidBody.velocity.x) < 0.1f)
            {
                rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
            }

            if (UnityEngine.Input.GetButtonDown("Jump") && isTouchingGround)
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, speedJump);
            }
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
        UpdateHealth();
    }

    ///<summary>Damages the player</summary>
    ///<param name="dmg">The amount of health to remove</param>
    public void Damage(float dmg)
    {
        _health -= dmg;
        if (_health <= 0) StartCoroutine("Die");
        else { playerAnimation.SetTrigger("Hit"); }
        UpdateHealth();
    }

    private IEnumerator Die()
    {
        resetScore();
        NavMeshAgent agent = GameObject.Find("Professor").GetComponent<NavMeshAgent>();
        agent.isStopped = true;
        playerAnimation.SetTrigger("Death");
        yield return new WaitForSecondsRealtime(3f);
        agent.isStopped = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
;   }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FallDetector"))
        {
            Die();
        }
        if (collision.CompareTag("LevelEnd") && gotKey)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Manager.health = _health;
            transform.position = respawnPoint;
        }
        if (collision.CompareTag("Key"))
        {
            gotKey = true;
            GameObject.Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Book"))
        {
            addScore();
            GameObject.Destroy(collision.gameObject);
        }
    }
    public void addScore()
    {
        Score.AddScore();
        levelScore++;
        BookCounter.text = Score.score.ToString();
    }
    public void resetScore()
    {
        Score.RemoveScore(levelScore);
        levelScore = 0;
        BookCounter.text = Score.score.ToString();

    }
}
