using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public float timeToRespawn;
    public int coinCount;
    public Player player;

    public Image heart1;
    public Image heart2;
    public Image heart3;
    public Image heart4;
    public Image heart5;

    public Sprite heartFull;
    public Sprite heartHalf;
    public Sprite heartEmpty;

    public int maxHealth;
    public int countHealth;

    private bool _respawn;


    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        countHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (countHealth <= 0 && !_respawn)
        {
            Respawn();
            _respawn = true;
        }
    }

    public void AddCoins(int addedCoins)
    {
        coinCount += addedCoins;
    }

    public void Respawn()
    {
        StartCoroutine("RespawnCo");
    }

    public IEnumerator RespawnCo()
    {
        player.gameObject.SetActive(false);

        yield return new WaitForSeconds(timeToRespawn);

        countHealth = maxHealth;
        _respawn = false;

        player.transform.position = player.respawnPos;
        player.gameObject.SetActive(true);
    }

    public void PlayerHurt(int damage)
    {
        countHealth -= damage;
    }
}
