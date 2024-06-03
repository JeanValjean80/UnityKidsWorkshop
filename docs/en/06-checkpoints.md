# Checkpoints and Level Manager

In this chapter we will add *checkpoints* to our game so that the game will restart at certain points if the *player* has fallen down. You should start here with a working state from the [last chapter](/docs/en/05-cleanup.md). In an emergency, you can use the sample project [here](https://github.com/JeanValjean80/UnityKidsWorkshop/releases/tag/0.5).

## Insert checkpoint into the scene
In the last chapter, we created a *KillZone* under the platforms and deactivated the *Player* as soon as it fell onto them. We now want to modify the game so that he reappears at a certain point in the game. To do this, we will create a control point in the form of a flag. 

Open the Textures folder in the Project window and unfold the items there. Drag one of the closed flags (for example Items_0) into the scene and name the new object “CheckpointFlag”. Now go to the Scripts folder and create a new script (right-click > Create > C# Script) and name it “Checkpoint”. Now attach the new script to the *CheckpointFlag* object. To do this, select *CheckpointFlag* in the hierarchy, click on “Add Component” in the Inspector and search for the *Checkpoint* script or drag the script directly onto the object in the hierarchy.


### Animation
Next, we animate the flag in the same way as we animated the movement of the *player* in the first chapter. The flag should remain closed until the *checkpoint* has been reached. Then it should wave in the wind.

Select *CheckpointFlag* in the hierarchy and click on “Create” in the animation window. If you have not yet opened the animation window, you can find it in the menu under Window > Animation > Animation. Name your new animation “FlagClosed” and make sure that you save it in the Animations folder. Insert the closed flag (for example Items_0) once at 0 seconds in the timeline.

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/124025345-9f203a80-d9f0-11eb-8517-bccab368e50f.png" width="800">
</p>

Now create another animation for the waving flag via “Create New Clip...” in the drop-down menu of the animation window. Name the new animation “FlagOpen” and make sure that you place it in the animations folder. Insert the sprites of the open flags into the timeline five times at intervals of 10 seconds. If you use the yellow flag, the sequence would be, for example: Items_17, Items_20, Items_17, Items_20, Items_17. You can use the play button to see what the animation looks like in the scene.

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/124026406-f07cf980-d9f1-11eb-9f05-23b7ec9074b6.png" width="800">
</p>

Now open the Animator window in the Unity Editor. Set FlagClosed as the default state by right-clicking on it and selecting “Set as Layer Default State”. Right-click and select “Make Transition” to create a transition from FlagClosed to FlagOpen as well as in the other direction.

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/124027177-dc85c780-d9f2-11eb-8017-1e224edbbb5e.png" width="400">
</p>

Now create a new parameter of the type Bool in the Animator window using the plus button and name it “FlagOpen”. Now edit the transitions by clicking on the arrows between the states. Open the settings for both transitions in the Inspector, uncheck “Has Exit Time” and “Fixed Duration” and set “Transition Duration” to 0. Add a condition to both transitions. For the transition from “FlagClosed” to “FlagOpen”, the condition FlagOpen = true and for the transition “FlagOpen” to “FlagClosed”, the condition FlagOpen = false. 

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/124029025-fcb68600-d9f4-11eb-895e-699df42f572f.png" width="250"> <img src="https://user-images.githubusercontent.com/75975986/124029028-fde7b300-d9f4-11eb-964d-f05b59af716a.png" width="250">
</p>

Next, we need to add a collider with trigger to the object. In this case, we do not use a BoxCollider2D as it does not fit the object. Instead, we will use a CircleCollider2D. Select the object *CheckpointFlag* in the hierarchy and add a component in the Inspector via “Add Component”. Select the CircleCollider2D and check the “Is Trigger” box. Position the collider centrally around the flag by clicking on “Edit Collider” and adjusting the collider in the scene.

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/124030257-4fdd0880-d9f6-11eb-90d4-35db59ba53d0.png" width="400">
</p>

Now we complete our script. To do this, open the *Checkpoint* script and create five variables in it. We need two variables of type sprite to define the two images for the open and closed flag. Then we need a variable of type bool to create a link to the parameter of our animation. The other two variables are known from our last scripts. 

```csharp
public Sprite flagOpen;
public Sprite flagClosed;
public bool checkpointActive = false;

private SpriteRenderer _spriteRenderer;
private Animator _animator;
```

In the `Start()` method, we initialize the SpriteRenderer for the images of the flag and the animator.

```csharp
_spriteRenderer = GetComponent<SpriteRenderer>();
_animator = GetComponent<Animator>();
```

In the `Update()` method, we set the “FlagOpen” parameter from the Animator to the value of the “checkpointActive” bool.

```csharp
_animator.SetBool("FlagOpen", checkpointActive);
```

We also create a new method `OnTriggerEnter2D` for opening the flag when the *Player* hits the flag.

```csharp
void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _spriteRenderer.sprite = flagOpen;
            checkpointActive = true;
        }
    }
```

For the whole thing to work, the *Player* still needs to be assigned the correct tag. To do this, switch back to the Unity Editor and select the *Player* in the hierarchy. Assign it the tag “Player” in the Inspector (not to be confused with the layer). Now select *CheckpointFlag* in the hierarchy and create a new tag in the Inspector under Tag > Add Tag... and name the new tag “Checkpoint”. Assign the tag “Checkpoint” to *CheckpointFlag*. 

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/124184498-f0483100-dab9-11eb-9efd-ed5da91d2592.png" width="200">
</p>

When you start the game now, the flag starts waving as soon as the *player* walks through it.

### Respawning

In the last chapter, we simply deactivated the *player* when it fell down. Now we want to restore it after dropping it at the *checkpoint* if it has already activated it.

To do this, we edit the *Player* script. Open the script and create a new variable in it in which the position of the flag is saved.

```csharp
public Vector3 respawnPos;
```

In the `OnTriggerEnter2D()` method, we insert the following code at the end of the method so that the position at which the *Player* runs into the flag is saved in the new variable `respawnPos`. 

```csharp
if (collision.CompareTag("Checkpoint"))
   {
       respawnPos = collision.transform.position;
   }
```

We also adjust the first if-condition in the `OnTriggerEnter2D()` method so that the *Player* is no longer deactivated, but its position is set to the value of our `respawnPos` variable. The if condition should now look like this: 

```csharp
if (collision.CompareTag("KillZone"))
   {
       transform.position = respawnPos;
   }
```

Extend the `Start()` method with the following code so that the *Player* reappears at the starting position if it has not yet activated the checkpoint.

```csharp
respawnPos = transform.position;
```

Now the *player* will be restored at the beginning of the game or at the *checkpoint* if it has activated it. You can turn the *checkpoint* into a prefab so that you can reuse the element several times. To do this, simply drag the *CheckpointFlag* from the hierarchy into the Prefabs folder.

## Level Manager

We will now implement a *LevelManager* so that we can build our own levels quickly and easily later on.
Create a new empty element in the hierarchy with a right click > “Create Empty”. Also create a new script in the Scripts folder and name both “LevelManager”. Attach the new script to the new object by selecting the *LevelManager* in the hierarchy, clicking “Add Component” in the Inspector and searching for the *LevelManager* script.

We have previously added a respawn function so that the *Player* reappears in the scene after it has fallen down. We will now add this function to the *LevelManager*. Open the *LevelManager* script and create two variables.

```csharp
    public float timeToRespawn;
    public Player player;
```

Initialize the *Player* in the `Start()` method. This time we do not use `GetComponent` for this, but `FindObjectOfType<Player>`, as we do not want to stay within an object with the *LevelManager*.

```csharp
    player = FindObjectOfType<Player>();
```

Also build a `Respawn()` method that can then be called from the *LevelManager*. 

```csharp
    public void Respawn()
    {
        player.gameObject.SetActive(false);
        player.transform.position = player.respawnPos;
        player.gameObject.SetActive(true);
    }
```

Now go back to the *Player* script and create a variable for the *LevelManager*.

```csharp
    public LevelManager levelManager;
```

Add a line in the `Start()` method with which you initialize the *LevelManager*.

```csharp
    levelManager = FindObjectOfType<LevelManager>();
```

Rewrite the `OnTriggerEnter2D()` method so that the position for recovery is no longer set manually, but via the *LevelManager*. The method should now look like this:

```csharp
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
```

Respawning should now work as before. However, the *player* currently respawns quite quickly if it falls down somewhere. The camera then reacts very “hectically”, so we want to delay the whole thing a little. We use a “CoRoutine” for this purpose.

We implement the CoRoutine in the *LevelManager* script. Insert a “CoRoutine” there and call it `RespawnCo()`. Then move the content from the `Respawn()` function into the “CoRoutine”. After deactivating the *Player*, add another line to the code in which the respawning delay is implemented. This is what the “CoRoutine” looks like: 

```csharp
    public IEnumerator RespawnCo()
    {
        player.gameObject.SetActive(false);
        yield return new WaitForSeconds(timeToRespawn);
        player.transform.position = player.respawnPos;
        player.gameObject.SetActive(true);
    }
```

Call the “CoRoutine” in the `Respawn()` method. 

```csharp
    public void Respawn()
    {
        //...
        StartCoroutine("RespawnCo");
    }
```

Go back to the Unity Editor, select the *LevelManager* in the hierarchy and set the value of “Time To Respawn” in the Inspector to 2. The camera movement now works a little more smoothly when restoring the *Player*. 

## Spikes

Now we create *spikes* that serve as an obstacle for the *player*. Open the “Textures” folder in the Unity Editor and open Tiles. Drag the sprite for the *spikes* (Tiles_69) into the scene and rename the object in the hierarchy to “Spikes”. Now create a new C# script in the Scripts folder and name it “PlayerHurt”. Select the object *Spikes* in the hierarchy and add the new script to it in the Inspector via “Add component”.

Open the new script and create a new variable for the *LevelManager* using the following code:

```csharp
    private LevelManager _levelManager;
```

Initialize the *LevelManager* in the `Start()` method: 

```csharp
    _levelManager = FindObjectOfType<LevelManager>();
```

If the *player* jumps onto the *spikes* or runs into them, it should die or later lose a life. To do this, we implement the method `OnTriggerEnter2D()`, which is activated when a BoxCollider2D marked as a trigger is touched. In this method, the respawning from the *LevelManager* is to be called when the *Player* touches the BoxCollider2D. 

```csharp
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _levelManager.Respawn();
        }
    }
```

Now we have to set the BoxCollider2D around the *Spikes*. Switch to the Unity Editor and select the *Spikes* object in the hierarchy. In the Inspector, add the new component “Box Collider 2D” via “Add Component” and check the “Is Trigger” box. Now edit the size of the collider by clicking on the button next to “Edit Collider”. Set the collider slightly inwards near the spikes so that the *player* does not die immediately when it is near the object. 

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/126035040-5cfa788b-c80d-4e5e-9b4f-db38b872db48.png" width="400">
</p>

The *player* now dies when he runs into the spikes - the chapter is now complete. 

[Here](https://github.com/JeanValjean80/UnityKidsWorkshop/releases/tag/0.6) you can download the sample solution and [here](/docs/en/07-level_elements.md) you can continue to the next chapter.
