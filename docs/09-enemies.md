# Gegner 

In diesem Kapitel implementieren wir das Verhalten und die Animation der Gegner ("Enemies") in unserem Spiel. Du solltest hier mit einem funktionsfähigen Stand aus dem [letzten Kapitel](/docs/08-ui_elements.md) starten. Im Notfall kannst du das Musterprojekt [hier](https://github.com/FrankFlamme/UnityKidsWorkshop/releases/tag/0.8) nutzen.

## Schnecke 

Als erstes arbeiten wir mit der Schnecke ("snail"), die wir bereits zu Beginn im Projekt hatten. Um die Animation der Schnecke zu erstellen, wähle das Objekt *Snail* in der Hierarchy aus und klicke im Animation Window auf "Create". Nenne die Animation "SnailWalk" und speichere Sie im Ordner Animations ab. Füge in der Timeline im Wechsel und mit einem Abstand von 10 Sekunden die Sprites Enemies_28 und Enemies_24 aus dem Ordner Textures ein. Die Reihenfolge sollte sein Enemies_28 - Enemies_24 - Enemies_28 - Enemies_24 - Enemies_28.

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/126165637-3d572c67-941d-4b38-8a27-241069cac715.png" width="600">
</p>

Nun bekommt die *Snail* einige Komponenten. Füge im Inspector einmal die Komponente "Ridigbody 2D" und zwei mal die Komponente "Box Collider 2D" hinzu. Setze beim "Rigidbody2D" unter "Constraints" den Haken bei "Freeze Rotation Z", damit die *Snail* bei einer Kollision nicht umkippt. 

Der erste BoxCollider2D dient der Schwerkraft und ist kein Trigger. Verknüpfe hier unter "Material" PlayerMat, sodass die *Snail* beim Herunterfallen nicht an der Seite einer Plattform hängen bleibt. Bearbeite die Größe des Colliders mit "Edit Collider", sodass er etwa folgende Größe hat:

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/126167988-c13c709c-558f-4bb8-8709-537823946e0c.png" width="400">
</p>

Der zweite BoxCollider2D dient dazu, dem *Player* einen Schaden zuzufügen und ist ein Trigger. Setze dafür den Haken bei "Is Trigger". Bearbeite die Größe wieder mit "Edit Collider", sodass er etwa folgende Größe hat. 

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/126168523-741dddc6-3454-4471-8235-ddb795c79100.png" width="400">
</p>

Nun benötigen wir noch ein eigene Komponente, um die Eigenschaften und Funktionen der *Snail* zu implementieren. Lege dazu ein neues Script im Ordner Scripts an und nenne es "Snail". Öffne das Script und lege drei Variablen an. Die Variable `speed` reguliert die Geschwindigkeit der *Snail*.
Die private Variable `_moving` stellt sicher, dass die *Snail* laufen darf/kann. Außerdem brauchst du noch eine private Variable für den `RigidBody2D`.

```csharp
    public float speed;

    private bool _moving;
    private Rigidbody2D _rb;
```

Initialisiere den Rigidbody2D in der `Start()`-Methode.

```csharp
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
```

Damit die *Snail* erst sichtbar ist und "losläuft", wenn das Spiel gestartet wird, erweitere die `Update()`-Methode mit folgender if-Abfrage. Andernfalls würde sie vielleicht schon von der Plattform stürzen, bevor der *Player* an der Stelle ankommt. 

```csharp
    void Update()
    {
        if (_moving)
        {
            _rb.velocity = new Vector3(-speed, _rb.velocity.y, 0f);
        }
    }
```

Implementiere eine neue Methode `OnBecameVisible()`, auf der du `_moving` aktivierst. Damit setzt sich die *Snail* erst in Bewegung, wenn sie von der Kamera erfasst wird.

```csharp
    void OnBecameVisible()
    {
        _moving = true;
    }
```

Wechsel wieder in den Unity Editor und füge der *Snail* das neue Script hinzu, indem du sie in der Hierarchy auswählst und dann im Inspector über "Add Component" das Script suchst. Setze Speed zum Beispiel auf 2. Füge der *Snail* außerdem das *PlayerHurt* Script an, sodass sie dem *Player* Schaden hinzufügen kann. Setze Damage zum Beispiel auf 1.

Fällt die *Snail* nun herunter, bleibt die trotzdem weiterhin aktiv. Für so einen Fall haben wir in einem vorhergehenden Kapitel die KillZone implementiert. Füge folgenden Code in das *Snail* Script ein, damit die *Snail* beim Herunterfallen zerstört wird.

```csharp
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("KillZone"))
        {
            Destroy(gameObject);
        }
    }
```

## Pinkes Monster

Als nächstes erstellen wir ein weiteres *Enemy* Objekt mit etwas anderen Eigenschaften. Dafür baust du zuerst weitere Plattformen in dein Spiel ein, indem du sie aus dem Prefabs Ordner in die Szene ziehst.

Nun kannst du auf einer neuen Plattform ein bewegliches Element aufbauen. Erstelle einen neuen *Enemy* (zum Beispiel Enemies_38) indem du es aus dem Ordner Textures > Enemies in die Szene ziehst. Nenne das neue Objekt "PinkMonster". Erstelle für das *PinkMonster* wieder eine neue Animation und nenne sie „PinkMonsterWalk“. Wähle dafür das Objekt aus, erstelle die Animation über "Create" und speichere sie im Animations Ordner. Füge im Abstand von 10 Sekunden folgende Sprites in die Timeline ein: Enemies_38 - Enemies_43 - Enemies_38 - Enemies_43 - Enemies_38.

Füge dem *PinkMonster* im Inspector die Komponente "Rigidbody 2D" hinzu und setze den Haken bei "Freeze Rotation Z" unter "Constraints". Füge außerdem wieder zwei Mal die Komponente "Box Collider 2D" hinzu. Der erste "BoxCollider2D" dient der Schwerkraft und ist kein Trigger. Bearbeite die Größe des Colliders mit "Edit Collider", sodass er etwa folgende Größe hat:

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/126177668-c2d1aa33-5145-4a60-b5de-840989aace60.png" width="300">
</p>

Der zweite "BoxCollider2D" dient dazu, dem *Player* einen Schaden zuzufügen und ist ein Trigger. Setze dafür den Haken bei "Is Trigger". Bearbeite die Größe des zweiten BoxColliders mit "Edit Collider", sodass er etwa folgende Größe hat. 

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/126177894-6e2940c0-af99-4cbc-989a-b50ec43873b3.png" width="300">
</p>

Wir wollen, dass unser *PinkMonster* sich zwischen zwei Punkten hin und her bewegt, ähnlich wie die *MovingPlatform*, daher muss das Objekt mit dem Start- und Endpunkt zusammengefasst werden. Erstelle dafür in der Hierarchy ein neues leeres Objekt über Rechtsklick > Create Empty und nenne es "PinkEnemy". Mache PinkMonster zu einem Child von PinkEnemy, indem du es in der Hierarchy auf PinkEnemy ziehst. Erstelle zwei weitere leere Objekte unter PinkEnemy und nenne sie "Start" und "Stop".

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/126178858-e76b94ea-1388-4f48-ad32-0b8038adcadc.png" width="400">
</p>

Gebe den zwei neuen Objekten wieder Icons über das Würfelsymbol im Inspector. Start bekommt einen grünen Punkt und Stop bekommt einen roten. 

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/126179261-346137d0-82e9-436e-a1d8-ff173de3d03e.png" width="200">
</p>

Ordne Start- und Stoppunkt etwa wie in folgender Abbildung an. 

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/126179658-c851fa97-390b-44c9-8b51-6b3087b11477.png" width="600">
</p>

Erstelle nun ein neues Script im Ordner Scripts und nenne es "PinkEnemy". Öffne das Script und erstelle folgende Variablen, zwei Variablen für den Start- und Endpunkt, eine für die Geschwindigkeit, eine für den Rigidbody2D und eine Variable um zu prüfen, in welche Richtung der Gegner läuft. 

```csharp
    public Transform startPoint;
    public Transform endPoint;
    public float speed;

    private Rigidbody2D _rb;
    private bool _moveRight;
```

Initialisiere den Rigidbody2D in der `Start()`-Methode.

```csharp
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
```

Passe die `Update()`-Methode so an, dass geprüft wird, in welche Richtung der *PinkEnemy* läuft und die Bewegung gestartet wird.

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

Wechsel wieder in den Unity Editor und hänge das Script an das Objekt *PinkMonster* an (Achtung: Nicht an das übergeordnete Element PinkEnemy!). Ziehe den Start- und Stoppunkt auf die entsprechenden Felder und setze Speed zum Beispiel auf 2. 

Weise *PinkMonster* nun auch noch das *PlayerHurt* Script zu und setze Damage zum Beispiel auf 1. 

## Gegner zerstören

Wie es sich für ein gutes Jump&Run Spiel gehört, müssen *Enemies* natürlich auch zerstört werden. Dafür bearbeiten wir den *Player*. Erstelle ein leeres Objekt unterhalb des *Player* und nenne es "StompBox". Füge der *StompBox* im Inspector über "Add Component" einen "Box Collider 2D" hinzu und setze einen Haken bei "Is Trigger". Platziere den BoxCollider2D an den Füßen des *Player* wie im folgenden Bild. 

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/126182826-ff722662-aefa-4674-b3f3-3974fd711f22.png" width="400">
</p>

Erstelle im Ordner Scripts nun ein neues Script und nenne es "StompBox". Öffne das Script und implementiere die `OnTriggerEnter2D()`-Methode, sodass das Objekt zerstört wird, das mit der *StompBox* kollidiert. 

```csharp
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
        }
    }
```

Den Tag "Enemy" haben wir bisher nicht. Lege den neuen Tag an indem du in der Hierarchy im Unity Editor die *Snail* auswählst und im Inspector auf Tag > Add Tag... gehst. Lege über das Plus-Symbol einen neuen Tag an und nenne ihn "Enemy". Wähle nun wieder *Snail* in der Hierarchy aus und weise ihr den neuen Tag zu. Weise auch dem *PinkMonster* (Achtung: Nicht dem übergeordneten Objekt) den Tag "Enemy" zu. 

Füge nun das *StompBox* Script der *StompBox* hinzu indem du es in der Hierarchy auswählst und im Inspector über "Add Component" das Script suchst. Die *Enemies* können nun zerstört werden, wenn der *Player* auf sie springt. Allerdings reagiert die *StompBox* manchmal, ohne dass der *Player* auf den *Enemy* springt. Das passiert, wenn die *StompBox* beispielsweise den "BoxCollider2D" des anderen Objektes berührt. Das können wir aber leicht anpassen.

Öffne das *Player* Script und erstelle eine Variable für die StompBox. 

```csharp
    public GameObject stompBox;
```

Füge folgenden Code am Ende der `Update()`-Methode ein, sodass die *StompBox* nur aktiv ist, wenn der *Player* springt.

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

Öffne nun den Unity Editor, wähle den *Player* in der Hierarchy aus, damit er im Inspector angezeigt wird, und ziehe das *StompBox* Objekt aus der Hierarchy in die Variable "Stomp Box".

Wir haben nun zwei Gegner in unserem Level, die der *Player* zerstören kann. Das Kapitel ist damit abgeschlossen. :) 

[Hier](https://github.com/FrankFlamme/UnityKidsWorkshop/releases/tag/0.9) findest du die Musterlösung zum Herunterladen und [hier](/docs/10-bugfixes_export.md) geht es weiter zum nächsten Kapitel.
