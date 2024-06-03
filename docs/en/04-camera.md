# Camera movement

In this chapter we will adjust the movement of the camera so that it moves with the *player*. You should start with a working version from the [last chapter](/docs/en/03-animations.md). If necessary, you can use the sample project [here](https://github.com/JeanValjean80/UnityKidsWorkshop/releases/tag/0.3).

## Let´s go
First, create a new script. To do this, go to the Scripts folder in the Project window of the Unity Editor, right-click and select Create > C# Script. Name the new script “CameraMovement”. It is important that you do not name the new script “Camera”, as Unity itself has a built-in function called “CameraMovement”. Open the script and create three variables to link the *player* and the camera. `target` is the object you want the camera to follow. `follow` is a value to determine the area around which the camera should move around the target. `_targetPos` is a private variable that should contain the position of the target object. 

```csharp
public GameObject target;
public float follow;
private Vector3 _targetPos;
```

Now attach the script to the camera. To do this, select the camera in the hierarchy and then click on “Add Component” in the Inspector. You can then enter “Camera Movement” in the search and select the script. You will then see your newly created variables in the Inspector. Set Target to the *Player* by simply dragging the *Player* from the hierarchy into the field behind “Target”. And set Follow to 5, for example, this value will later be added to the movement of the camera to move it a little further. 

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/123543452-cce65480-d74e-11eb-8ae2-3c6111375b4b.png" width="400">
</p>

Now adjust the `Update()` method in the camera script as follows:

```csharp
 _targetPos = new Vector3(target.transform.position.x, transform.position.y, transform.position.z);
	
if (target.transform.localScale.x > 0f)
  {
    _targetPos = new Vector3(_targetPos.x + follow, _targetPos.y, _targetPos.z);
  }
  else
  {
    _targetPos = new Vector3(_targetPos.x - follow, _targetPos.y, _targetPos.z);
  }

  transform.position = _targetPos;
```

When you start the game now, the camera already moves with the *player*. However, the transitions when changing direction are not particularly smooth. We solve this with the Lerp function, a mathematical function that sets the position of the camera more evenly over a period of time.

Create a new variable `smoothness` in the script “CameraMovement”.

```csharp
public float smoothness;
```

Replace the last line in the `Update()` method where we set `transform.position` with the code below. Where `Time.deltaTime` is the time that elapses between two frames. So 1/30 fps if the game is running at 30 fps.

```csharp
transform.position = Vector3.Lerp(transform.position, _targetPos, smoothness * Time.deltaTime);
```

If you now set the Smoothness variable in the Unity Editor to 1, for example, the camera will now move smoothly with the player.

You have now completed this chapter! :) 

[Here](https://github.com/JeanValjean80/UnityKidsWorkshop/releases/tag/0.4) you can download the sample solution. In the [next chapter](/docs/en/05-cleanup.md) we will do some small cleanup work and additional functions on our project. 
