# Additional level elements

In this chapter we will create further level elements in our game. You should start with a working level from the [last chapter](/docs/en/06-checkpoints.md). If necessary, you can use the sample project [here](https://github.com/JeanValjean80/UnityKidsWorkshop/releases/tag/0.6).


## Moving platforms
First, we create a small platform that moves horizontally from left to right and vice versa.
Open the Unity Editor and create a new object in the hierarchy by right-clicking > Create Empty. Name the new element “MovingPlatform”. Then take the existing prefab “SmallGround_1x5” from the Prefabs folder and drag it as an object under “MovingPlatform”. Create two more empty objects under the new object and name them “Start” and “Stop”. Set the position of the new objects on the X, Y and Z axes in the Inspector to 0. 

<p align="center">
<img src="https://user-images.githubusercontent.com/13068729/126306166-c663dac2-143a-4f85-8d7f-8d9dcfd73ec7.png" width="400">
</p>

Use the Move tool to move the empty object to a suitable position in the scene, preferably behind the last platform. So that we can see the start and end points in the scene later, we mark the points in color. You can do this by selecting the corresponding object in the hierarchy and clicking on the cube at the top of the Inspector:

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/126035665-2da2917c-b20e-48f6-926b-b61fa326f03d.png" width="400">
</p>

Select a green point for Start and a red point for Stop and drag the Stop point a little to the right so that we have a distance between the two points. In the scene, it will look like this:

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/126035848-dac96de6-1459-4dd0-96a7-a8353c92334f.png" width="800">
</p>

Now create a new C# script in the Scripts folder and name it “MovingObjects”. We can also use this script later for other moving platforms. Attach the script to the *MovingPlatform* object by selecting it in the hierarchy and selecting the script in the Inspector under “Add Component”. 

Open the script and create the following variables: 

```csharp
    public GameObject movingObject;
    public Transform startPoint;
    public Transform stopPoint;
    public float speed;


    private Vector3 _curTarget;
```

Initialize the start position of the object in the `Start()` method: 

```csharp
    _curTarget = stopPoint.position;
```

Implement the movement of the object in the `Update()` method:

```csharp
    movingObject.transform.position = Vector3.MoveTowards(movingObject.transform.position, _curTarget, speed * Time.deltaTime);

    if (movingObject.transform.position == stopPoint.position)
    {
        _curTarget = startPoint.position;
    }

    if (movingObject.transform.position == startPoint.position)
    {
        _curTarget = stopPoint.position;
    }
```


We now assign the variables in the Unity Editor. Select the *MovingPlatform* object in the hierarchy and drag the underlying platform into the *MovingObject* in the Inspector. Start becomes the Start Point and Stop becomes the Stop Point. Also set “Speed” to 3.

<p align="center">
<img src="https://user-images.githubusercontent.com/13068729/126306019-e35514f4-1d94-43f3-9ef1-1cb17d76484b.png" width="400">
</p>

When we start the game now, the new platform moves, but the *player* does not stay on the moving platform, but slides down. To fix this, we need to make some adjustments to the *Player* script.

First, create a new tag for the moving platform by selecting the object *SmallGround_1x5* (note: not *MovingPlatform*) in the hierarchy below *MovingPlatform* and choosing Tag > Add Tag... in the Inspector. Name the new tag “MovingPlatform” and select it for the object *SmallGround_1x5*.

Now go to the *Player* script and create two new methods. We use the methods `OnCollisionEnter()` and `OnCollisionExit()` here, because the *MovingPlatform* has no trigger. As long as the *Player* is on the platform, it assumes the position of the platform. If it leaves the platform, e.g. by jumping, we reset the “Transform” (position). The code looks like this.

```csharp
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
```

## Sorting Layers
To ensure that the objects are arranged correctly in the scene, we now define the “Sorting Layers” via the menu in the Inspector. Select Layers > Edit Layers above the Inspector and unfold the “Sorting Layers”. The order of the layers goes from top to bottom. What is at the top of the list is further in the background. What is furthest down in the list can be seen in the foreground of the scene.

Now create two new sorting layers. Name the first one *World Items* and the second one *Player*. Move *World Items* to the first position, *Default* remains in the second position and *Player* is moved to the third position.

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/126036858-010a4d79-8ecb-4743-a9b8-cf228cd3abf3.png" width="400">
</p>

Then assign the correct sorting layers to the objects and prefabs (*Snail*, *Spikes*, ...) by selecting them in the hierarchy and selecting the corresponding sorting layers in the Sprite Renderer. 

## Collect coins
Next, we want to collect the already prepared *Coins* (coins) and add them up. To do this, select all 5 *Coins* in the hierarchy and check the “Is Trigger” box in the Inspector for CircleCollider2D. Create a new script in the Scripts folder and name it “Coin”.

Open the new script and create a new variable for the *LevelManager* so that the collection of coins works centrally. Create a second variable for the value of the coin (if there are several coins with different values in the game).

```csharp
    private LevelManager _levelManager;
    public int coinValue;
```

Instantiate the level manager in the `Start()` method.

```csharp
    _levelManager = FindObjectOfType<LevelManager>();
```

Also add an `OnTriggerEnter2D()` method in which the coins are deactivated as soon as the *player* touches them.

```csharp
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            _levelManager.AddCoins(coinValue);
            gameObject.SetActive(false);
        }
    }
```

The `AddCoins` function must now be implemented in the *LevelManager*. To do this, open the *LevelManager* script and first create a variable for the number of coins collected. 

```csharp
    public int coinCount;
```

Also write the new function `AddCoin()`, in which the value of the coin just collected is added to the coins already collected. 

```csharp
    public void AddCoins(int addedCoins)
    {
        coinCount += addedCoins;
    }
```

Now go back to the Unity Editor, select all *Coins* in the hierarchy and add the *Coin* script to them in the Editor via “Add Component”. Set the “Coin Value” to 1. 

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/126137542-da758d2b-417a-4b8d-a40f-74e91ee0cc83.png" width="400">
</p>

When you start the game now, the coins disappear after you collect them and you can see how the “Coin Count” in the *LevelManager* increases. Chapter 7 is now complete.

[Here](https://github.com/JeanValjean80/UnityKidsWorkshop/releases/tag/0.7) you can download the sample solution and [here](/docs/en/08-ui_elements.md) you can continue to the next chapter.
