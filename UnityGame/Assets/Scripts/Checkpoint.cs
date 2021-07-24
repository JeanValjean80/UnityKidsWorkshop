using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Sprite flagOpen;
    public Sprite flagClosed;
    public bool checkpointActive = false;

    private SpriteRenderer _spriteRenderer;
    private Animator _animator;


    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _animator.SetBool("FlagOpen", checkpointActive);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _spriteRenderer.sprite = flagOpen;
            checkpointActive = true;
        }
    }
}
