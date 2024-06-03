# Player Movement
In this chapter, we'll program the movement of the Player in the scene. You should have already completed [Chapter 1](/docs/en/01-start.md).

## Running
First, open the script that was created for the *Player* - Player.cs. Here, we need to create two variables: `speed` is the speed at which our Player will move through the scene and `_rb` is the “Rigidbody2D” that will allow the Player to react to physical influences.

```csharp
public float speed;    
private Rigidbody2D _rb;
```
In the `Start()` method, the `Rigidbody2D` will be initialized. We assign `_rb` to the `Rigidbody2D` component that was previously attached to the Player.

```csharp
_rb = GetComponent<Rigidbody2D>();
```

We implement the movement in the `Update()` method. This method is called once every frame. For the movement, we build a conditional statement (if-statement) where we address the movement of the `Rigidbody2D` provided by Unity.

```csharp
if (Input.GetAxisRaw("Horizontal") > 0f)
{
    _rb.velocity = new Vector3(speed, _rb.velocity.y, 0f);
}
else if (Input.GetAxisRaw("Horizontal") < 0f)
{
    _rb.velocity = new Vector3(-speed, _rb.velocity.y, 0f);
}
else
{
    _rb.velocity = new Vector3(0f, _rb.velocity.y, 0f);
}
```

Now, if you select your *Player* in the Unity Editor, you can set the desired `speed` in the Inspector under your Player script. For example, set it to 5. When you start the game, the Player can move left and right using the arrow keys on the keyboard.

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/122826402-5fb46880-d2e3-11eb-83d0-96a0bf2aa350.png" width="300">
</p>

## Jumping
For jumping, we need a third variable in the *Player* script, the jump force jump.

```csharp
public float jump;
```

In the `Update()` method, we add another if-statement where we address the vertical movement of the *Player* when the spacebar is pressed.

```csharp
if (Input.GetButtonDown("Jump"))
{
    _rb.velocity = new Vector3(_rb.velocity.x, jump, 0f);
}
```

Now, if you set the jump force in the Unity Editor, the *Player* can also jump. You can set the jump force just like the running speed by selecting your Player and setting the `jump` variable in the Inspector under your Player script, for example to 15.

However, the *Player* can now jump infinitely in the air. Next, we will ensure that the Player can only jump when on the ground. First, create two new layers in the Unity Editor. Select the `Layers` dropdown above the Inspector and click on `Edit Layers…`. Create a layer for Ground and a layer for Player in both the `Sorting Layers` and `Layers` lists. Ensure the Player is sorted under the *Ground* layer.

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/122826414-6511b300-d2e3-11eb-9ddc-e68373c2c766.png" width="300"> <img src="https://user-images.githubusercontent.com/75975986/122827711-00efee80-d2e5-11eb-8e23-ec234a2111e9.png" width="300">
</p>

Assign the *Player* to the Player layer by selecting the *Player* in the Hierarchy and choosing the `Layer` in the Inspector as shown in the next screenshot. Do the same for the *Grounds*. We have created Prefabs for the Grounds that need to be assigned to the Ground layer. In the Unity Editor, select the Prefabs folder in the Project window. There you will find two Grounds. Select the first Ground and set the layer to `Ground`. You may be asked if the changes should be applied to all child objects. Choose `Yes`.

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/122829276-182fdb80-d2e7-11eb-8b96-a3fddf0d8c69.png" width="300">
</p>

Now open the *Player* script again. Here, we will create three variables. `checkGround` is an empty object that will be placed at the feet of the Player. `checkGroundRadius` is a small circle object that will check if the Player is touching the Ground. `isGround` is used to identify the Ground.

```csharp
public Transform checkGround;
public float checkGroundRadius;
public LayerMask isGround; 
```

Go back to the Unity Editor and create an empty object under the *Player* by right-clicking the Player in the Hierarchy and selecting `Create Empty`. Name the empty object `GroundCheck` and move it to the feet of the Player using the Move tool.

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/122996587-5393de00-d3ab-11eb-94ea-cbfb2ce93db1.png" height="300"><img src="https://user-images.githubusercontent.com/75975986/122997372-3d3a5200-d3ac-11eb-876d-619e81f5058a.png" height="300">
</p>

Now we will fill in our new variables. Select the *Player* in the Hierarchy. In the Inspector, set the `Is Ground` variable to `Ground`, `Check Ground Radius` to <b>0.25</b>, and drag the new GroundCheck object to the `Check Ground` variable. Your variables should now look like this:

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/123169435-991edc80-d479-11eb-8e05-6098e3f7c90f.png" width="300">
</p>

Go back to the Player script once more. Add a new variable `grounded`.

```csharp
public bool grounded;
```

Extend the `Update()` method to set the variable. Here, we check if our previously created object (at the feet of the *Player*) touches an object that has the Ground layer set. Add the following code at the beginning of the `Update()` method:

```
grounded = Physics2D.OverlapCircle(checkGround.position, checkGroundRadius, isGround);
```

Also, change the if-condition for the `jump` so that it checks if `grounded` is true before jumping. The code should now look like this:

```csharp
if (Input.GetButtonDown("Jump") && grounded)
{
    _rb.velocity = new Vector3(_rb.velocity.x, jump, 0f);
}
```

The Player can now run and jump. You can find the sample solution [here](https://github.com/JeanValjean80/UnityKidsWorkshop/releases/tag/0.2) for download. Otherwise, you can continue with the [next chapter](/docs/en/03-animations.md). :)
