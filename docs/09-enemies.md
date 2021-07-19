# Gegner 

In diesem Kapitel implementieren wir das Verhalten und die Animation der Gegner in unserem Spiel. Du solltest hier mit einem funktionsfähigen Stand aus dem [letzten Kapitel](/docs/08-ui_elements.md) starten. Im Notfall kannst du das Musterprojekt [hier](https://github.com/FrankFlamme/UnityKidsWorkshop/releases/tag/0.8) nutzen.

## Schnecke 

Als erstes arbeiten wir mit der Schnecke, die wir bereits zu Beginn im Projekt hatten. Um die Animation der Schnecke zu erstellen, wähle die das Objekt Snail in der Hierarchy aus und klicke im Animation Window auf "Create". Nenne die Animation „SnailWalk“ und speichere Sie im Ordner Animations ab. Füge in der Timeline im Wechsel und mit einem Abstand von 10 Sekunden die Sprites Enemies_28 und Enemies_24 aus dem Ordner Textures ein. Die Reihenfolge sollte sein Enemies_28 - Enemies_24 - Enemies_28 - Enemies_24 - Enemies_28.

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/126165637-3d572c67-941d-4b38-8a27-241069cac715.png" width="600">
</p>

Nun bekommt die Schnecke einige Komponenten. Füge im Inspector einmal die Komponente "Ridigbody 2D" und zwei mal die Komponente "Box Collider 2D" hinzu. Setze beim Rigidbody den Haken bei "Freeze Rotation Z" unter "Constraints", damit die Schnecke bei einer Kollision nicht umkippt. 

Der erste Box Collider dient der Schwerkraft und ist kein Trigger. Verknüpfe hier unter "Material" PlayerMat, sodass die Schnecke beim Herunterfallen nicht an der Seite einer Plattform hängen bleibt. Bearbeite die Größe des Colliders mit "Edit Collider", sodass er etwa folgende Größe hat:

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/126167988-c13c709c-558f-4bb8-8709-537823946e0c.png" width="400">
</p>

Der zweite Box Collider dient dazu, dem Player einen Schaden zuzufügen und ist ein Trigger. Setze dafür den Haken bei "Is Trigger". Bearbeite die Größe des zweiten Box Colliders mit "Edit Collider" so, dass er etwa folgende Größe hat. 

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/126168523-741dddc6-3454-4471-8235-ddb795c79100.png" width="400">
</p>

Nun benötigen wir noch ein eigenes Script, um die Eigenschaften und Funktionen der Schnecke zu implementieren. Lege dazu ein neues Script im Ordner Scripts an und nenne es „Snail“. Öffne das neue Script in Visual Studio und lege drei neue Variablen an. Die Variable `speed` reguliert die Geschwindigkeit der Schnecke.
Die private Variable `_moving` stellt sicher, dass die Schnecke laufen darf/kann. Außerdem brauchst du noch eine private Variable für den RigidBody.

```csharp
    public float speed;

    private bool _moving;
    private Rigidbody2D _rb;
```

Initialisiere den Rigidbody in der `Start()`-Methode.

```csharp
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
```

Erweitere die `Update()`-Methode, sodass die Bewegung ausgeführt wird, wenn `_moving` aktiv ist. Die Schnecke soll so erst loslaufen, wenn das Spiel gestartet und sie sichtbar ist. Andernfalls würde sie vielleicht schon von der Plattform stürzen, bevor der Spieler an der Stelle ankommt. 

```csharp
    void Update()
    {
        if (_moving)
        {
            _rb.velocity = new Vector3(-speed, _rb.velocity.y, 0f);
        }
    }
```

Implementiere eine neue Methode `OnBecameVisible()`, auf der du `_moving` aktivierst. Damit setzt sich die Schnecke erst in Bewegung, wenn sie von der Kamera erfasst wird.

```csharp
    void OnBecameVisible()
    {
        _moving = true;
    }
```

Wechsle wieder in den Unity Editor und füge der Schnecke das neue Script hinzu, indem du sie in der Hierarchy auswählst und dann im Inspector über "Add Component" das Script suchst. Setze Speed zum Beispiel auf 2. Füge der Schnecke außerdem das PlayerHurt Script an, sodass sie dem Spieler Schaden hinzufügen kann. Setze Damage zum Beispiel auf 1.

Fällt die Schnecke nun herunter, bleibt die trotzdem weiterhin aktiv. Für so einen Fall haben wir in einem vorhergehenden Kapitel die KillZone implementiert. Füge folgenden Code in das Snail Script ein, damit die Schnecke beim Herunterfallen zerstört wird.

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

Als nächstes erstellen wir ein weiteres Enemy Objekt mit etwas anderen Eigenschaften. Dafür kannst zuerst weitere Plattformen in dein Spiel einbauen, indem du sie aus dem Prefabs Ordner in die Szene ziehst.

Nun kannst du auf einer neuen Plattform ein bewegliches Element aufbauen. Erstelle einen neuen Enemy (zum Beispiel Enemies_38) indem du es aus dem Ordner Textures > Enemies in die Szene ziehst. Nenne das neue Objekt „PinkMonster“. Erstelle für das Monster wieder eine neue Animation und nenne sie „PinkMonsterWalk“. Wähle dafür das Objekt aus, erstelle die Animation über "Create" und speichere sie im Animations Ordner. Füge im Abstand von 10 Sekunden folgende Sprites in die Timeline ein: Enemies_38 - Enemies_43 - Enemies_38 - Enemies_43 - Enemies_38.

Füge dem Monster im Inspector die Komponente Rigidbody 2D hinzu und setze den Haken bei "Freeze Rotation Z" unter "Constraints". Füge außerdem wieder zwei Mal die Komponente Box Collider 2D hinzu. Der erste Box Collider dient der Schwerkraft und ist kein Trigger. Bearbeite die Größe des Colliders mit "Edit Collider", sodass er etwa folgende Größe hat:

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/126177668-c2d1aa33-5145-4a60-b5de-840989aace60.png" width="300">
</p>

Der zweite Box Collider dient dazu, dem Player einen Schaden zuzufügen und ist ein Trigger. Setze dafür den Haken bei "Is Trigger". Bearbeite die Größe des zweiten Box Colliders mit "Edit Collider" so, dass er etwa folgende Größe hat. 

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/126177894-6e2940c0-af99-4cbc-989a-b50ec43873b3.png" width="300">
</p>

Wir wollen, dass unser Pink Monster sich zwischen zwei Punkten hin und her bewegt, ähnlich wie die Moving Platform, daher muss das Objekt mit dem Start- und Endpunkt zusammengefasst werden. Erstelle dafür in der Hierarchy ein neues leeres Objekt über Rechtsklick > Create Empty und nenne es "PinkEnemy". Mache PinkMonster zu einem Child von PinkEnemy, indem du es in der Hierarchy auf PinkEnemy ziehst. Erstelle zwei weitere leere Objekte unter PinkEnemy und nenne sie "Start" und "Stop".

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

Erstelle nun ein neues Script im Ordner Scripts und nenne es "PinkEnemy". Öffne das neue Script in Visual Studio und erstelle fünf neue Variablen. Zwei Variablen sind für den Start- und Endpunkt, eine Variable für die Geschwindigkeit. Außerdem brauchen wir unseren Rigidbody und eine Variable um zu prüfen, in welche Richtung der Gegner läuft. 

```csharp
    public Transform startPoint;
    public Transform endPoint;
    public float speed;

    private Rigidbody2D _rb;
    private bool _moveRight;
```

Initialisiere den Rigidbody in der `Start()`-Methode.

```csharp
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
```

Passe die `Update()`-Methode so an, dass geprüft wird, in welche Richtung der Gegner läuft und die Bewegung gestartet wird.

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

Wechsle wieder in den Unity Editor und hänge das neue Script an das Objekt PinkMonster an (Achtung: Nicht an das übergeordnete Element PinkEnemy!). Ziehe den Start- und Stoppunkt auf die entsprechenden Felder und setze Speed zum Beispiel auf 2. 

Weise PinkMonster nun auch noch das PlayerHurt Script zu und setze Damage zum Beispiel auf 1. 

## Gegner zerstören

Wie es sich für ein gutes Jump&Run Game gehört, müssen Enemies natürlich auch zerstört werden. Dafür bearbeiten wir den Player. Erstelle ein leeres GameObject unterhalb des Players und nenne es „StompBox“. Füge der StompBox im Inspector über "Add Component" einen Box Collider 2D hinzu und setze einen Haken bei "Is Trigger". Platziere den Box Collider an den Füßen des Players wie im folgenden Bild. 

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/126182826-ff722662-aefa-4674-b3f3-3974fd711f22.png" width="400">
</p>

Erstelle im Ordner Scripts nun ein neues Script und nenne es "StompBox". Öffne das neue Script in Visual Studio und implementiere die `OnTriggerEnter2D()`-Methode, sodass das Objekt zerstört wird, das mit der StompBox kollidiert. 

```csharp
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
        }
    }
```

Den Tag Enemy haben wir bisher nicht. Lege den neuen Tag an indem du in der Hierarchy im Unity Editor die Snail auswählst und im Inspector auf Tag > Add Tag... gehst. Lege über das Plus-Symbol einen neuen tag an und nenne ihn "Enemy". Wähle nun wieder Snail in der Hierarchy aus und weise ihr den neuen Tag zu. Weise auch dem Gegner PinkMonster (Achtung: Nicht dem übergeordneten Objekt) den Tag Enemy zu. 

Füge nun das StompBox Script der StompBox hinzu indem du es in der Hierarchy auswählst und im Inspector über "Add Component" das Script suchst. Die Gegner können nun zerstört werden, wenn der Player auf sie springt. Allerdings reagiert die StompBox manchmal, ohne dass der Player auf den Gegner springt. Das passiert, wenn die StompBox beispielsweise den Box Collider des anderen Objektes berührt. Das können wir mit einem kleinen Fix anpassen.

Öffne das Player Script in Visual Studio und erstelle eine neue Variable für die StompBox. 

```csharp
    public GameObject stompBox;
```

Füge folgenden Code am Ende der `Update()`-Methode ein, sodass die StompBox nur aktiv ist, wenn der Player springt.

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

Öffne nun den Unity Editor, wähle den Spieler in der Hierarchy aus und ziehe das StompBox Objekt aus der Hierarchy in die Variable Stomp Box beim Inspector des Players.

Wir haben nun zwei neue Gegner in unserem Level, die der Spieler zerstören kann. Das Kapitel ist damit abgeschlossen. :) 

[Hier](https://github.com/FrankFlamme/UnityKidsWorkshop/releases/tag/0.9) findest du die Musterlösung zum Herunterladen und [hier](/docs/10-bugfixes_export.md) geht es weiter zum nächsten Kapitel.
