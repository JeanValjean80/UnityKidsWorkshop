# Player animations
In this chapter we will animate the movement of the *player* so that it looks more realistic when it runs and jumps. You should start with a working version from the [last chapter](/docs/en/02-playermovement.md). If necessary, you can use the sample project [here](https://github.com/JeanValjean80/UnityKidsWorkshop/releases/tag/0.2).

## Connections in the Unity Editor
First we create the images of the movement (sprites) with an animation. Open the animation window in the Unity Editor by selecting Window > Animation > Animation in the menu. You can integrate the animation window into your Unity Editor by dragging the animation tab to a position of your choice in the Unity Editor. This is how it could look for you, for example: 

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/123328585-0dba4f80-d53c-11eb-8340-6681b95036d6.png" width="700">
</p>

Create a new folder in the Project tab under Assets and name it “Animations”. Now create a new animation by clicking on “Create” in the Animations tab. Name the animation “PlayerWalk” and save it in the new Animations folder. We will now add sprites to the animation. To do this, go to the “Textures” folder in the Project tab, where you will find all the sprite sheets in the project. Click on the arrow to the right of “MalePlayer”. You should now see the individual sprites of the *Player* as shown in the last picture.

Insert 6 sprites into your animation by simply dragging them into the animation timeline. To make the movement look realistic, you can insert the sprites 10 seconds apart in the following order: MalePlayer_10, MalePlayer_9, MalePlayer_0, MalePlayer_10, MalePlayer_9, MalePlayer_0. Done! If you click on the play button, you can now watch the animation on the *Player*. 

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/123330439-4eb36380-d53e-11eb-99f7-c093eb2aa24c.png" width="500">
</p>

We will now do the same again with the animation for jumping and standing. Create a new animation by clicking on the down arrow next to “PlayerWalk” in the Animations tab and selecting “Create New Clip...”. Name the animation “PlayerJump” and save it again in the Animations folder. Now drag the sprite “MalePlayer_20” into the timeline at 0 seconds.

Create another animation in the same way. Name it “PlayerIdle” and save it again in the Animations folder. Drag the sprite “MalePlayer_0” into the timeline at 0 seconds.

We have now created all the animations we need for the *Player*. Next, we need to link the transitions of the animations to each other. To do this, open the menu under Window > Animation > Animator. Here you can see your animations as a status. Their arrangement should look something like this: 

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/123332565-f29e0e80-d540-11eb-95d4-25aa842bb220.png" width="600">
</p>

The default state is “PlayerIdle”, it should be colored orange. If it is not, set it as the default state by right-clicking on it and selecting “Set as Layer Default State”. Now create two new parameters by clicking on Parameters in the Animator and then on the plus symbol. Create a float and name it “Speed”. Then create a bool and name it “Grounded”. We use these two parameters to determine when a status transition occurs. 

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/123333241-c59e2b80-d541-11eb-8102-5e83862e67a2.png" width="500">
</p>

To create the first connection between the statuses, right-click on PlayerIdle in the Animator, select “Make Transition” and click on PlayerWalk. A connecting arrow has appeared between PlayerIdle and PlayerWalk. Click on the arrow to edit the properties of the status transition. Open the settings in the Inspector. Remove the ticks for “Fixed Duration” and “Has Exit Time” and set the “Transition Duration” to 0. Add a new condition under Conditions by clicking on the plus symbol under the table. Our desired condition is that Speed is greater than 0.1. This is what it should look like: 

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/123334339-3db92100-d543-11eb-9849-52fe954b49d9.png" width="300">
</p>

Now create all other status transitions: 
- From PlayerWalk to PlayerIdle with the condition Speed is less than 0.1
- From PlayerIdle to PlayerJump with the condition Grounded is false
- From PlayerWalk to PlayerJump with the condition Grounded is false
- From PlayerJump to PlayerIdle with the condition Grounded is true

Remember to uncheck “Fixed Duration” and “Has Exit Time” in all status transitions and set the “Transition Duration” to 0. The animator will then look something like this:

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/123541076-b89c5a80-d742-11eb-96d8-887068d64335.png" width="500">
</p>

## Adjustments in code
We now want to make adjustments in the *Player* script so that the connections that we previously created in the Unity Editor can also become active. Open the *Player* script again and create a new variable `_animator`. 

```csharp
private Animator _animator;
```

Initialize the animator with the following code in the `Start()` method:

```csharp
_animator = GetComponent<Animator>();
```

We extend the `Update()` method by setting the two parameters previously created in the Animator. For `Speed` we use the absolute function here, as we only want to know in the Animator whether the speed is above or below 0.1, regardless of the sign. This sets the parameters that are checked for our status transitions.

```csharp
_animator.SetFloat("Speed", Mathf.Abs(_rb.velocity.x));
_animator.SetBool("Grounded", grounded);
```

Currently, the *player* can only look in one direction when running and jumping. We can adapt this by adjusting the first two if-conditions in the `Update()` method to change the scaling. This is what the if conditions look like after the extension:

```csharp
if (Input.GetAxisRaw("Horizontal") > 0f)
  {
    _rb.velocity = new Vector2(speed, _rb.velocity.y);
    transform.localScale = new Vector3(1f, 1f, 1f);
  }
  else if (Input.GetAxisRaw("Horizontal") < 0f)
  {
    _rb.velocity = new Vector2(-speed, _rb.velocity.y);
    transform.localScale = new Vector3(-1f, 1f, 1f);
  }
```

If you now start the game in Unity, the *player* can run and jump in both directions while his movement is animated accordingly. :)

[Here](https://github.com/JeanValjean80/UnityKidsWorkshop/releases/tag/0.3) you can find the sample solution from this chapter. In the [next chapter](/docs/en/04-camera.md) we will take care of the camera movement during the game. 
