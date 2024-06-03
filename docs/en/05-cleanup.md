# Improve Game
In this chapter, we will introduce a few minor improvements and additional functions to our project so that we can continue working more easily in the future. You should start with a functional version from the [last chapter](/docs/en/04-camera.md). If necessary, you can use the sample project [here](https://github.com/JeanValjean80/UnityKidsWorkshop/releases/tag/0.4).


## Player's sticking to the walls
With the current status, the *player* can stick to a wall and “fight” his way back up. Of course we don't want that.
In the Project window in the Unity Editor, create a new folder under Assets and name it “Materials”. Create a new material in this folder by right-clicking and then selecting 2D under Create > 2D > Physics Material. Name the material “PlayerMat” and set the “Friction” on the right in the Inspector to 0.

Now select the *Player* in your hierarchy. You will find a field for a material in the Inspector under the “BoxCollider2D”. Drag your new material “PlayerMat” into this field.

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/123544419-77f90d00-d753-11eb-840e-7d145706217f.png" width="400">
</p>

It should no longer be possible for the *player* to “pull itself up” against a wall.

## Killzone
At the moment, the *player* falls infinitely far into the depths when he falls off a platform. Of course, we want the game to end as soon as the *player* falls down. Therefore we create a *killzone*. 

Create a new empty object in your scene by right-clicking in the hierarchy and selecting “Create Empty”. Name the object “KillZone” and select it with a click. Now create a “BoxCollider2D” at the *KillZone* by clicking on “AddComponent” in the Inspector on the right and selecting the “BoxCollider2D” there. Set “Size” at “X” to 40.

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/123544699-c3f88180-d754-11eb-889e-444ff202a7a6.png" width="400">
</p>

We want the *KillZone* to move with the camera. To do this, we drag the *KillZone* into the *MainCamera* in the hierarchy so that its position changes relative to the camera. 

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/123544756-04f09600-d755-11eb-8ff9-1d27a7f0632e.png" width="350">
</p>

Also drag the *KillZone* in the scene with the Move tool under the floors as shown in the following image. 

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/123544853-99f38f00-d755-11eb-9d70-0b567b974d40.png" width="500">
</p>

The *Player* would only “land” on the *KillZone*, which is why we check the “Is Trigger” box in the “BoxCollider2D” of the *KillZone*. Now we want to implement the function that is executed when the *Player* touches this trigger. To do this, open the *Player* script again and create the new method `OnTriggerEnter2D` with the following content. 

```csharp
void OnTriggerEnter2D(Collider2D collision)
{
  if (collision.CompareTag("KillZone"))
  {
    gameObject.SetActive(false);
  }
}
```

In this way, we deactivate the *player* when it touches the *kill zone*. This will be adjusted later. 

To identify the *kill zone*, we work with tags. This makes it very easy to organize objects into specific groups. Select the *Killzone* in the hierarchy and click on Tags > Add Tag in the Inspector at the top right. Now add the tag “KillZone” using the plus symbol. Select the *KillZone* again in the hierarchy and set the tag to “KillZone” in the Inspector.

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/123545059-8bf23e00-d756-11eb-8857-d6c3fc085d67.png" width="500">
</p>

The *player* no longer falls into the bottomless pit and so this chapter is also finished! :) 

[Here](https://github.com/JeanValjean80/UnityKidsWorkshop/releases/tag/0.5) you can download the sample solution and [here](/docs/en/06-checkpoints.md) you can continue to the next chapter.
