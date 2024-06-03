# Enemies 
In this chapter we will implement the behavior and animation of the enemies in our game. You should start with a working version from the [last chapter](/docs/en/08-ui_elements.md). In an emergency, you can use the sample project [here](https://github.com/JeanValjean80/UnityKidsWorkshop/releases/tag/0.8).


## Snails
First we work with the snail, which we already had in the project at the beginning. To create the animation of the snail, select the object *Snail* in the hierarchy and click on “Create” in the Animation Window. Name the animation “SnailWalk” and save it in the Animations folder. Insert the sprites Enemies_28 and Enemies_24 from the Textures folder into the timeline alternately and with an interval of 10 seconds. The order should be Enemies_28 - Enemies_24 - Enemies_28 - Enemies_24 - Enemies_28.

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/126165637-3d572c67-941d-4b38-8a27-241069cac715.png" width="600">
</p>

Now the *Snail* gets some components. In the Inspector, add the “Ridigbody 2D” component once and the “Box Collider 2D” component twice. For “Rigidbody2D”, check “Freeze Rotation Z” under “Constraints” so that the *Snail* does not tip over in the event of a collision. 

The first BoxCollider2D is used for gravity and is not a trigger. Link PlayerMat here under “Material” so that the *Snail* does not get stuck on the side of a platform when falling. Edit the size of the collider with “Edit Collider” so that it has approximately the following size:

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/126167988-c13c709c-558f-4bb8-8709-537823946e0c.png" width="400">
</p>

The second BoxCollider2D is used to inflict damage on the *player* and is a trigger. Check the “Is Trigger” box for this. Edit the size again with “Edit Collider” so that it has approximately the following size. 

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/126168523-741dddc6-3454-4471-8235-ddb795c79100.png" width="400">
</p>

Now we need a separate component to implement the properties and functions of the *Snail*. To do this, create a new script in the Scripts folder and name it “Snail”. Open the script and create three variables. The variable `speed` regulates the speed of the *Snail*.
The private variable `_moving` ensures that the *Snail* may/can move. You also need a private variable for the `RigidBody2D`.

```csharp
    public float speed;

    private bool _moving;
    private Rigidbody2D _rb;
```

Initialize the Rigidbody2D in the `Start()` method.

```csharp
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
```

So that the *Snail* is only visible and “starts running” when the game is started, extend the `Update()` method with the following if query. Otherwise it might fall off the platform before the *Player* arrives at the location. 

```csharp
    void Update()
    {
        if (_moving)
        {
            _rb.velocity = new Vector3(-speed, _rb.velocity.y, 0f);
        }
    }
```

Implement a new method `OnBecameVisible()` on which you activate `_moving`. This means that the *Snail* only starts moving when it is detected by the camera.

```csharp
    void OnBecameVisible()
    {
        _moving = true;
    }
```

Switch back to the Unity Editor and add the new script to the *Snail* by selecting it in the hierarchy and then searching for the script in the Inspector via “Add Component”. For example, set Speed to 2. Also add the *PlayerHurt* script to the *Snail* so that it can add damage to the *Player*. Set Damage to 1, for example.

If the *Snail* now falls down, it will still remain active. We implemented the KillZone for such a case in a previous chapter. Insert the following code into the *Snail* script so that the *Snail* is destroyed when it falls down.

```csharp
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("KillZone"))
        {
            Destroy(gameObject);
        }
    }
```

## Pink Monster
Next, we will create another *Enemy* object with slightly different properties. To do this, first add more platforms to your game by dragging them from the Prefabs folder into the scene.

Now you can build a moving element on a new platform. Create a new *Enemy* (for example Enemies_38) by dragging it from the Textures > Enemies folder into the scene. Name the new object “PinkMonster”. Create a new animation for the *PinkMonster* and name it “PinkMonsterWalk”. Select the object, create the animation via “Create” and save it in the animation folder. Add the following sprites to the timeline at 10-second intervals: Enemies_38 - Enemies_43 - Enemies_38 - Enemies_43 - Enemies_38.

Add the “Rigidbody 2D” component to the *PinkMonster* in the Inspector and check the “Freeze Rotation Z” box under “Constraints”. Also add the “Box Collider 2D” component twice. The first “BoxCollider2D” is used for gravity and is not a trigger. Edit the size of the collider with “Edit Collider” so that it has approximately the following size:

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/126177668-c2d1aa33-5145-4a60-b5de-840989aace60.png" width="300">
</p>

The second “BoxCollider2D” is used to inflict damage on the *player* and is a trigger. Check the “Is Trigger” box for this. Edit the size of the second BoxCollider with “Edit Collider” so that it has approximately the following size. 

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/126177894-6e2940c0-af99-4cbc-989a-b50ec43873b3.png" width="300">
</p>

We want our *PinkMonster* to move back and forth between two points, similar to the *MovingPlatform*, so the object must be combined with the start and end point. To do this, create a new empty object in the hierarchy via right-click > Create Empty and name it “PinkEnemy”. Make PinkMonster a child of PinkEnemy by dragging it onto PinkEnemy in the hierarchy. Create two more empty objects under PinkEnemy and name them “Start” and “Stop”.

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/126178858-e76b94ea-1388-4f48-ad32-0b8038adcadc.png" width="400">
</p>

Give the two new objects icons again using the cube symbol in the Inspector. Start gets a green dot and Stop gets a red dot. 

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/126179261-346137d0-82e9-436e-a1d8-ff173de3d03e.png" width="200">
</p>

Arrange the start and stop points as shown in the following illustration. 

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/126179658-c851fa97-390b-44c9-8b51-6b3087b11477.png" width="600">
</p>

Now create a new script in the Scripts folder and name it “PinkEnemy”. Open the script and create the following variables, two variables for the start and end point, one for the speed, one for the Rigidbody2D and one variable to check in which direction the opponent is running. 

```csharp
    public Transform startPoint;
    public Transform endPoint;
    public float speed;

    private Rigidbody2D _rb;
    private bool _moveRight;
```

Initialize the Rigidbody2D in the `Start()` method.

```csharp
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
```

Adapt the `Update()` method so that it checks in which direction the *PinkEnemy* is running and the movement is started.

```csharp
    void Update()
    {
        if (_moveRight && transform.position.x > endPoint.position.x)
        {
            _moveRight = false;
        }
        if (!_moveRight && transform.position.x < startPoint.position.x)
        {
            _moveRight = true;
        }

        if (_moveRight)
        {
            _rb.velocity = new Vector3(speed, _rb.velocity.y, 0f);
        }
        else
        {
            _rb.velocity = new Vector3(-speed, _rb.velocity.y, 0f);
        }
    }
```

Switch back to the Unity Editor and attach the script to the object *PinkMonster* (Attention: Not to the parent element PinkEnemy!). Drag the start and stop points to the corresponding fields and set Speed to 2, for example. 

Now assign the *PlayerHurt* script to *PinkMonster* and set Damage to 1, for example. 


## Destroy enemies
As befits a good Jump&Run game, *Enemies* must of course also be destroyed. To do this, we edit the *Player*. Create an empty object below the *Player* and name it “StompBox”. Add a “Box Collider 2D” to the *StompBox* in the Inspector via “Add Component” and check the “Is Trigger” box. Place the BoxCollider2D at the feet of the *Player* as shown in the following image. 

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/126182826-ff722662-aefa-4674-b3f3-3974fd711f22.png" width="400">
</p>

Now create a new script in the Scripts folder and name it “StompBox”. Open the script and implement the `OnTriggerEnter2D()` method so that the object that collides with the *StompBox* is destroyed. 

```csharp
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
        }
    }
```

We do not yet have the “Enemy” tag. Create the new tag by selecting the *Snail* in the hierarchy in the Unity Editor and going to Tag > Add Tag... in the Inspector. in the Inspector. Create a new tag using the plus symbol and name it “Enemy”. Now select *Snail* in the hierarchy again and assign the new tag to it. Also assign the tag “Enemy” to the *PinkMonster* (note: not to the parent object). 

Now add the *StompBox* script to the *StompBox* by selecting it in the hierarchy and searching for the script in the Inspector via “Add Component”. The *Enemies* can now be destroyed when the *Player* jumps on them. However, the *StompBox* sometimes reacts without the *Player* jumping to the *Enemy*. This happens, for example, when the *StompBox* touches the “BoxCollider2D” of the other object. But we can easily adjust this.

Open the *Player* script and create a variable for the StompBox. 

```csharp
    public GameObject stompBox;
```

Insert the following code at the end of the `Update()` method so that the *StompBox* is only active when the *Player* jumps.

```csharp
        if (_rb.velocity.y < 0)
        {
            stompBox.SetActive(true);
        }
        else
        {
            stompBox.SetActive(false);
        }
```

Now open the Unity Editor, select the *Player* in the hierarchy so that it is displayed in the Inspector, and drag the *StompBox* object from the hierarchy into the “Stomp Box” variable.

We now have two enemies in our level that the *Player* can destroy. This concludes the chapter. :) 

[Here](https://github.com/JeanValjean80/UnityKidsWorkshop/releases/tag/0.9) you can download the sample solution and [here](/docs/en/10-bugfixes_export.md) you can continue to the next chapter.
