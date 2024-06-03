# UI Elemente 
In this chapter we will add UI elements to the game so that we can see how many coins have been collected and how many lives the *player* has left. You should start here with a working state from the [last chapter](/docs/en/07-level_elements.md). If necessary, you can use the sample project [here](https://github.com/JeanValjean80/UnityKidsWorkshop/releases/tag/0.7).

## Count coins
First, we want our collected coins to be counted and displayed in the UI. To do this, we create a new script *CoinCounter* in the Scripts folder.

Open the new script in the editor and create a new variable for the *LevelManager*. 

```csharp
    private LevelManager _levelManager;
```

Instantiate the LevelManager again in the `Start()` method.

```csharp
    _levelManager = FindObjectOfType<LevelManager>();
```

As we want to connect the sprites for the counter to the script, we also create variables for this. We don't want to access the sprites from anywhere, so they should be declared private in the code. To make a private variable visible in the editor, we use the keyword `SerializeField`.
The keyword `Header(“Digit Coin Sprites”)` is used for ease of display in the editor and is the header of the fields.

```csharp
    [Header("Digit Coin Sprites")]
    [SerializeField]
    private Sprite[] _digitSprites;

    [SerializeField]
    private Sprite _coinSprite;

    [Header("Coin Digits")]
    [SerializeField]
    private Image _ones;

    [SerializeField]
    private Image _tens;

    [SerializeField]
    private Image _hundreds;

    [SerializeField]
    private Image _coin;
```

Presumably the variable type “Image” in Visual Studio is now highlighted in red. This is because the implementation of the Unity.UI classes is missing. Add the following to the beginning of the script.

```csharp
using UnityEngine.UI;
```

Create two more variables. With `maxPoints` the maximum achievable number of points is defined. In our case, this is 999 because our UI only has 3 digits. We need the variable `points` for the rest of the source code.

```csharp
    public int maxPoints;
    float points = 0f;
```

Initialize the coin sprite in the `Start()` method: 

```csharp
        _coin.sprite = _coinSprite;
```

Now implement the `Update()` method by setting `points` to the CoinCount value of the *LevelManager*. (Where we counted the number of coins in the field in the last chapter). We now initialize the UI with an if condition and set the three individual sprites to the first position of the array.

```csharp
    void Update()
    {
        points = _levelManager.coinCount;

        if (points <= 0)
        {
            _ones.sprite = _digitSprites[0];
            _tens.sprite = _digitSprites[0];
            _hundreds.sprite = _digitSprites[0];
            return;
        }
    }
```

As the individual digits of the number stand alone in the UI, there is no automatic transition (for example from 9 to 10). We solve this with the following addition in the Update() method under the if query:

```csharp
        int onesNumber = Mathf.RoundToInt(points % 10);
        int tensNumber = Mathf.RoundToInt((points - onesNumber) % 100) / 10;
        int hundredsNumber = Mathf.RoundToInt((points - tensNumber * 10 - onesNumber) % 1000 / 100);
```

The operation `%` is called modulo and calculates the remainder when dividing integers. 

Now assign the corresponding sprites to the UI. To do this, add the following to the `Update()` method:

```csharp
        _ones.sprite = _digitSprites[onesNumber];
        _tens.sprite = _digitSprites[tensNumber];
        _hundreds.sprite = _digitSprites[hundredsNumber];
```

Now go back to the Unity Editor and attach the new script to the “CoinCounter” object, which you can find in the hierarchy under the HUD. Set Max Points to 999. The images under “Coin Digits” are the elements that you can find in the hierarchy under the CoinCounter. Drag them into the fields. The sprites for the digits and the coin can be found in the Textures folder. Create a new element in the digit sprites for each number from 0 to 9 and drag the corresponding sprites from Textures > HUD into it.

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/126153841-c308a639-f9d1-4b29-a4ca-8ccbd0dd2a53.png" width="400">
</p>

If you wish, you can drag additional coins from the prefabs folder into the scene. Make sure that the coin prefab has the coin script attached. 

When you start the game now, you will see the counter for the coins at the top left of the screen.

## Show health
Now let's add a display for the health. To do this, create a new empty object in the hierarchy under HUD with right-click > Create Empty and name it “HealthMonitor”. Create five image objects under the Health Monitor by right-clicking > UI > Image and name the objects Heart1 to Heart5.

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/126155330-eb5af344-f571-4c28-9837-9d64801a64cd.png" width="400">
</p>

Align the “HealthMonitor” object so that it is at the top center of the screen. Select the object for this in the hierarchy and set the settings as shown in the following illustration.

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/126155735-343ae462-9fe5-4820-aaf0-fe72556e09f1.png" width="400">
</p>

Now select all five *Heart* objects under the *HealthMonitor* and set the following in the Inspector:

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/126156121-c68bfd92-30d0-48be-858b-7a7bc20335c3.png" width="400">
</p>

Now set the *Heart* objects next to each other by selecting each object individually in the hierarchy and setting the position X as follows: Heart1 = 0, Heart2 = 128, Heart3 = 256, Heart4 = 384, Heart5 = 512

Now select all *Heart* objects in the hierarchy again and assign the sprite Hud_10 (full heart) from the Textures folder to all of them in the Inspector under Source Image. Also activate the “Preserve Aspect” checkbox under “Raycast Padding”. This ensures that the sprite always scales the same in height and width when it is resized.

Now open the *LevelManager* script and create a variable for each of the heart images. 

```csharp
    public Image heart1;
    public Image heart2;
    public Image heart3;
    public Image heart4;
    public Image heart5;
```

If another error occurs, import the Unity.UI at the beginning of the script.

```csharp
using UnityEngine.UI;
```

We want to lose hearts each time we run into a spike or later encounter an *enemy* (opponent). To do this, we need the sprites with the half heart and the empty heart. We also declare these in the *LevelManager*.

```csharp
    public Sprite heartFull;  
    public Sprite heartHalf;
    public Sprite heartEmpty;
```

Create two more variables for maximum health and life counting.

```csharp
    public int maxHealth;
    public int countHealth;
```

Now go back to the Unity Editor, select the *LevelManager* in the hierarchy and assign the images and sprites. You will find the corresponding images for Heart1 to Heart5 in the hierarchy under the *HealthMonitor*. The sprites can be found in the Textures folder under HUD. Set Max Health to 10, as we have five hearts, each with a half heart option.

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/126158320-c9fd0f58-7aab-41a6-85e1-8a6dc33a87e7.png" width="400">
</p>

Now go back to the *LevelManager* script and initialize `countHealth` in the `Start()` method. 

```csharp
        countHealth = maxHealth;
```

Also create a new method to implement the damage to the *Player* by passing the damage and subtracting it from `countHealth`.

```csharp
    public void PlayerHurt(int damage)
    {
        countHealth -= damage;
    }
```

Now open the *PlayerHurt* script and create a new variable for the damage.

```csharp
    public int damage;
```

Now adjust the `OnTriggerEnter2D()` method. Comment out the respawning so that the player does not die immediately when injured. Instead, implement the `PlayerHurt()` method from the *LevelManager* and pass the value from `damage`.

```csharp
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // _levelManager.Respawn();
            _levelManager.PlayerHurt(damage);
        }
    }
```

Switch back to the Unity Editor and give the objects that should cause damage a value for Damage by selecting them in the hierarchy and setting the value in the Inspector. For example, set it to 1 for the *spikes* so that half a heart is deducted when they are touched. 

If we now start the game, we can observe how the Count Health in the *LevelManager* decreases when the *Player* runs into the *Spikes*. However, if the *Player* runs through the *Spikes* often enough, Count Health will still be in the minus range. Extend the `Update()` method in the *LevelManager* script so that respawning is executed when Count Health equals “0”.

```csharp
    void Update()
    {
        if (countHealth <= 0)
        {
            Respawn();
        }
    }
```

Extend the `RespawnCo()` method in the *LevelManager* script so that the *health* of the *player* is reset after respawning. To do this, write the following code under the yield statement:

```csharp
       countHealth = maxHealth;
```

So that the whole thing doesn't end in a continuous loop if the value is 0, we still have to make sure that a respawn really makes sense. To do this, go to the *LevelManager* script and create a new variable.

```csharp
    private bool _respawn;
```

Adjust the update method so that respwaning is only carried out if a respawn is not already taking place.

```csharp
    void Update()
    {
        if (countHealth <= 0 && !_respawn)
        {
            Respawn();
            _respawn = true;
        }
    }
```

Add another line to the `RespawnCo()` method to deactivate the new variable `_respawn`.

```csharp
    public IEnumerator RespawnCo()
    {
        player.gameObject.SetActive(false);
        yield return new WaitForSeconds(timeToRespawn);
        countHealth = maxHealth;
        _respawn = false;

        player.transform.position = player.respawnPos;
        player.gameObject.SetActive(true);
    }
```

Next, we want to add our new implementation to the UI so that the display of the hearts also changes with damage. To do this, create the function `UpdateHealth()` in the *LevelManager* script and implement the variants of the heart display using a switch statement.

```csharp
    public void UpdateHealth()
    {
        switch(countHealth)
        {
            case 10:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartFull;
                heart4.sprite = heartFull;
                heart5.sprite = heartFull;
                return;

            case 9:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartFull;
                heart4.sprite = heartFull;
                heart5.sprite = heartHalf;
                return;

            case 8:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartFull;
                heart4.sprite = heartFull;
                heart5.sprite = heartEmpty;
                return;

            case 7:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartFull;
                heart4.sprite = heartHalf;
                heart5.sprite = heartEmpty;
                return;

            case 6:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartFull;
                heart4.sprite = heartEmpty;
                heart5.sprite = heartEmpty;
                return;

            case 5:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartHalf;
                heart4.sprite = heartEmpty;
                heart5.sprite = heartEmpty;
                return;

            case 4:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartEmpty;
                heart4.sprite = heartEmpty;
                heart5.sprite = heartEmpty;
                return;

            case 3:
                heart1.sprite = heartFull;
                heart2.sprite = heartHalf;
                heart3.sprite = heartEmpty;
                heart4.sprite = heartEmpty;
                heart5.sprite = heartEmpty;
                return;

            case 2:
                heart1.sprite = heartFull;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                heart4.sprite = heartEmpty;
                heart5.sprite = heartEmpty;
                return;

            case 1:
                heart1.sprite = heartHalf;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                heart4.sprite = heartEmpty;
                heart5.sprite = heartEmpty;
                return;

            case 0:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                heart4.sprite = heartEmpty;
                heart5.sprite = heartEmpty;
                return;

            default:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                heart4.sprite = heartEmpty;
                heart5.sprite = heartEmpty;
                return;
        }
    }
```

This method must be called in the `RespawnCo()` function and in the `PlayerHurt()` function:

```csharp
    public IEnumerator RespawnCo()
    {
        player.gameObject.SetActive(false);
        yield return new WaitForSeconds(timeToRespawn);
        countHealth = maxHealth;
        _respawn = false;
        UpdateHealth();

        player.transform.position = player.respawnPos;
        player.gameObject.SetActive(true);
    }
    
    public void PlayerHurt(int damage)
    {
        countHealth -= damage;
        UpdateHealth();
    }
```

The display of the hearts now changes with the injury of the *Player* and the chapter is finished. :)

[Here](https://github.com/JeanValjean80/UnityKidsWorkshop/releases/tag/0.8) you can download the sample solution and [here](/docs/en/09-enemies.md) you can continue to the next chapter.
