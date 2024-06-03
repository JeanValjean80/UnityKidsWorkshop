# Bugfixes, Export and visual changes

In this chapter we will add respawning effects to the game, make some bug fixes and export the game so that you no longer need Unity to run the game. You should start here with a working state from the [last chapter](/docs/en/09-enemies.md). In an emergency, you can use the sample project [here](https://github.com/JeanValjean80/UnityKidsWorkshop/releases/tag/0.9).


## Background
First of all, we want the background to look a bit more like a real game. To do this, we will use the [background](https://github.com/JeanValjean80/UnityKidsWorkshop/releases/download/1.0/backgroundEmpty.png), which you can download from the assets in the chapter.

Download the background to your desktop and go to the “Textures” folder in the Unity Editor. Right-click in the folder and select “Import New Asset”. Now select the file from your desktop and click on “Import”.

The file is now in the “Textures” folder.
Now click on the image in the folder and make the settings in the Inspector as shown in the image:

<p align="center">
<img src="https://user-images.githubusercontent.com/13068729/126160733-42be8760-13bd-475d-9356-efdb4c61ef79.png">
</p>

Now insert the image into the scene by dragging it from the “Texture” folder into the scene. Adjust the size of the background so that it is slightly larger than the size of the camera. Name the object “BackgroundPicture” in the hierarchy. Set the “Order in Layer” flag in the Inspector to -1 so that the image is really in the background and does not cover any other items.

To ensure that the background picture moves with the camera, create a new, empty object with the name “CamBackground” below the “Main Camera” and drag “BackgroundPicture” underneath it as a child object.

<p align="center">
<img src="https://user-images.githubusercontent.com/13068729/126161431-655eee95-eeb5-41eb-b8fc-0a87b54d364e.png">
</p>

## Partical Effects

Now we add a more realistic effect when the *player* has lost all hearts and is respawned. For this we use a “Particle Effect”.

Right-click on *Level1* in the hierarchy and select GameObject > Effects > Particle System. In the Inspector, set “Duration” to 1, “Start Lifetime” to 1 and “Start Speed” to 2.5. Under “Start Color” you can select the color of the effect.
In the “Shape” tab, set “Shape” to Circle and “Radius” to 0.5.
Under the “Emission” tab, set the “Rate over Time” to 0. Create a new entry for “Bursts” with the following settings. 

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/126282625-bb852c98-0031-4eb8-9df4-c07794d4079b.png" width="400"> 
</p>

Check the “Size over Lifetime” box and select the curve under “Size”. Set the effect as follows:

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/126284822-7ddfe30c-eed9-4d62-ab3c-52c22511bcee.png" width="400"> 
</p>

Now set the “Rotation X” under Transform to 0. The particle effect should now look something like this: 

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/126284837-5475e87c-a60f-4c0f-b073-df32fc906ce3.png" width="500"> 
</p>

Now uncheck the “Looping” box so that the effect is only executed once and is not constantly repeated. 

Rename the object in the hierarchy to “PlayerDied”. Move it to the Prefabs folder and delete it from the hierarchy. We no longer need it there because we call it via a script.

Open the *LevelManager* script in Visual Studio and create a variable for the Particle Effect.

```csharp
    public GameObject playerDied;
```

Instantiate the Particle Effect in the `RespawnCo()` method after the *Player* has been deactivated: 

```csharp
    public IEnumerator RespawnCo()
    {
        player.gameObject.SetActive(false);
        Instantiate(playerDied, player.transform.position, player.transform.rotation);
        ...
```

Open the Unity Editor and select the *LevelManager* in the hierarchy. Drag *PlayerDied* from the Prefabs folder into the variable “Player Died” in the Inspector of the *LevelManager*. If the *Player* now dies, the effect is executed. 

However, every time the effect is executed, a cloned object is created that remains in the game and takes up memory space. We want to prevent this in the long term, as it takes up unnecessary memory and slows down the game. To do this, create a new script in the Scripts folder and name it “DestroyObjects”.

Open the new script in Visual Studio and create a new variable for the lifetime of the object. 

```csharp
    public float lifeTime;
```

Customize the `Update()` method so that the object is destroyed when the lifetime has expired. 

```csharp
    void Update()
    {
        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0f)
        {
            Destroy(gameObject);
        }
    }
```

Open the Unity Editor and select the *PlayerDied* object in the Prefabs folder. Attach the new script to the prefab by searching for the script in the Inspector via “Add Component”. Set “Life Time” to 1.1, as this is the effect duration of our effect. 

The effect will now be deleted from our game after each execution. 

## Prefabs
In order to use the elements several times later, we can make prefabs from some objects by dragging the objects from the hierarchy into the prefabs folder. Drag the following objects from the hierarchy into the Prefabs folder:

* Player
* HUD
* LevelManager
* Spikes
* Snail
* MovingPlatform
* PinkEnemy

## Bugfixes

The *Player* or the Enemies may get stuck while running in the game. This may be due to two “BoxCollider2D” colliding with each other. To fix this, we first edit all *Enemies* by adjusting the prefabs. To do this, select the *Enemies* in the Prefabs folder instead of in the hierarchy. 

Reduce the inner “BoxCollider2D” (without trigger) and create a “Circle Collider 2D” that only just touches the ground. The “Collider” should look something like the following picture. 

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/126289594-7fc68824-210e-488c-adb6-043e0e250145.png" width="300"> 
</p>

We cannot use “CircleCollider2D” with the *Player*, as this would implement further errors. Here you can experiment a little with the “BoxCollider2D” and move it up a little at the bottom end, for example. 

## Building the game

Finally, we create an executable program that we can run on the PC without starting it with Unity. To do this, click on File > Build Settings in Unity... . Check the Scenes/Level1 box. If you don't see this yet, add it via “Add Open Scenes”. Under “Platform” select PC, Mac & Linux Standalone. Under “Target Platform”, select the operating system on which you want to start the game. 

The remaining settings do not need to be changed. You can now export the game by clicking on “Build And Run”. It is best to create a folder on your desktop where you can save the game. Depending on your computer and performance, exporting may take a little while.

You have now completed the tutorial! :) [Here](https://github.com/JeanValjean80/UnityKidsWorkshop/releases/tag/1.0) you can download the sample solution.
