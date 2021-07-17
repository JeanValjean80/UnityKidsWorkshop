using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float jump;
    public float checkGroundRadius;
    public bool grounded;
    public Vector3 respawnPos;


    public Transform checkGround;
    public LayerMask isGround;
    public LevelManager levelManager;
    public GameObject stompBox;


    private Rigidbody2D _rb;
    private Animator _animator;


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        respawnPos = transform.position;
        levelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.OverlapCircle(checkGround.position, checkGroundRadius, isGround);

        if (Input.GetAxisRaw("Horizontal") > 0f)
        {
            _rb.velocity = new Vector3(speed, _rb.velocity.y, 0f);
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0f)
        {
            _rb.velocity = new Vector3(-speed, _rb.velocity.y, 0f);
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            _rb.velocity = new Vector3(0f, _rb.velocity.y, 0f);
        }

        if (Input.GetButtonDown("Jump") && grounded)
        {
            _rb.velocity = new Vector3(_rb.velocity.x, jump, 0f);
        }

        _animator.SetFloat("Speed", Mathf.Abs(_rb.velocity.x));
        _animator.SetBool("Grounded", grounded);

        if (_rb.velocity.y < 0)
        {
            stompBox.SetActive(true);
        }
        else
        {
            stompBox.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("KillZone"))
        {
            levelManager.Respawn();
        }

        if (collision.CompareTag("Checkpoint"))
        {
            respawnPos = collision.transform.position;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            transform.parent = collision.transform;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            transform.parent = null;
        }
    }
}
