using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCounter : MonoBehaviour
{
    private LevelManager _levelManager;

    [Header("Digit Coin Sprites")]
    [SerializeField]
    private Sprite[] _digitSprites;

    [SerializeField]
    private Sprite _coinSprite;

    [Header("Coin Digits")]
    [SerializeField]
    private Image _ones;

    [SerializeField]
    private Image _tenth;

    [SerializeField]
    private Image _hundreds;

    [SerializeField]
    private Image _coin;

    public int maxPoints;
    float points = 0f;

    // Start is called before the first frame update
    void Start()
    {
        _levelManager = FindObjectOfType<LevelManager>();
        _coin.sprite = _coinSprite;
    }

    // Update is called once per frame
    void Update()
    {
        points = _levelManager.coinCount;

        if (points <= 0)
        {
            _ones.sprite = _digitSprites[0];
            _tenth.sprite = _digitSprites[0];
            _hundreds.sprite = _digitSprites[0];
            return;
        }

        int onesNumber = Mathf.RoundToInt(points % 10);
        int tenthNumber = Mathf.RoundToInt((points - onesNumber) % 100) / 10;
        int hundredsNumber = Mathf.RoundToInt((points - tenthNumber * 10 - onesNumber) % 1000 / 100);

        _ones.sprite = _digitSprites[onesNumber];
        _tenth.sprite = _digitSprites[tenthNumber];
        _hundreds.sprite = _digitSprites[hundredsNumber];
    }
}
