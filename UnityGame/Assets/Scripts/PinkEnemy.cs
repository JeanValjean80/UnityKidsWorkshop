using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkEnemy : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public float speed;

    private Rigidbody2D _rb;
    private bool _moveRight;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_moveRight && transform.position.x > endPoint.position.x)
        {
            _moveRight = false;
        }
        if (!_moveRight && transform.position.x < startPoint.position.x)
        {
            _moveRight = true;
        }

        if (_moveRight)
        {
            _rb.velocity = new Vector3(speed, _rb.velocity.y, 0f);
        }
        else
        {
            _rb.velocity = new Vector3(-speed, _rb.velocity.y, 0f);
        }
    }
}
