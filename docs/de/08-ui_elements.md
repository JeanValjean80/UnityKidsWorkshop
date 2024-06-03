# UI Elemente 

In diesem Kapitel fügen wir dem Spiel UI Elemente hinzu, sodass wir sehen, wie viele Münzen gesammelt wurden und wie viele Leben der *Player* noch hat. Du solltest hier mit einem funktionsfähigen Stand aus dem [letzten Kapitel](/docs/07-level_elements.md) starten. Im Notfall kannst du das Musterprojekt [hier](https://github.com/FrankFlamme/UnityKidsWorkshop/releases/tag/0.7) nutzen.

## Münzen zählen

Zuerst möchten wir, dass unsere gesammelten Münzen nun auch in der UI gezählt und angezeigt werden. Dafür erstellen wir ein neues Script *CoinCounter* im Ordner Scripts.

Öffne das neue Script im Editor und erstelle eine neue Variable für den *LevelManager*. 

```csharp
    private LevelManager _levelManager;
```

Instanziiere den LevelManager wieder in der `Start()`-Methode.

```csharp
    _levelManager = FindObjectOfType<LevelManager>();
```

Da wir die Sprites für den Zähler mit dem Script verbinden wollen, erstellen wir hierfür auch Variablen. Wir wollen auf die Sprites nicht von überall zugreifen, deshalb sollten sie im Code auf private deklariert sein. Um eine private Variable im Editor sichtbar zu machen, nutzen wir das Keyword `SerializeField`.
Das Keyword `Header(„Digit Coin Sprites“)` dient der Anzeigefreundlichkeit im Editor und ist die Überschrift der Felder.

```csharp
    [Header("Digit Coin Sprites")]
    [SerializeField]
    private Sprite[] _digitSprites;

    [SerializeField]
    private Sprite _coinSprite;

    [Header("Coin Digits")]
    [SerializeField]
    private Image _ones;

    [SerializeField]
    private Image _tens;

    [SerializeField]
    private Image _hundreds;

    [SerializeField]
    private Image _coin;
```

Vermutlich ist der Variablen-Typ „Image“ in Visual Studio nun rot hinterlegt. Das liegt daran, dass die Implementierung der Unity.UI Klassen fehlt. Füge dafür folgendes an den Anfang des Scripts an.

```csharp
using UnityEngine.UI;
```

Lege zwei weitere Variablen an. Mit `maxPoints` wird die maximal erreichbare Punktzahl festgelegt. In unserem Fall ist das 999, weil unsere UI nur 3-stellig ist. Die Variable `points` benötigen wir für den weiteren Source Code.

```csharp
    public int maxPoints;
    float points = 0f;
```

Initialisiere das Coin-Sprite in der `Start()`-Methode: 

```csharp
        _coin.sprite = _coinSprite;
```

Implementiere nun die `Update()`-Methode, indem du `points` auf den CoinCount-Wert des *LevelManagers* setzt. (Dort, wo wir im letzten Kapitel die Anzahl der Coins im Feld gezählt haben). Mit einer if-Condition initialisieren wir jetzt die UI und setzen die drei einzelnen Sprites auf die erste Stelle des Arrays.

```csharp
    void Update()
    {
        points = _levelManager.coinCount;

        if (points <= 0)
        {
            _ones.sprite = _digitSprites[0];
            _tens.sprite = _digitSprites[0];
            _hundreds.sprite = _digitSprites[0];
            return;
        }
    }
```

Da in der UI die einzelnen Stellen der Zahl für sich stehen, passiert kein automatischer Übergang (zum Beispiel von 9 auf 10). Das lösen wir mit folgender Ergänzung in der Update()-Methode unter der if-Abfrage:

```csharp
        int onesNumber = Mathf.RoundToInt(points % 10);
        int tensNumber = Mathf.RoundToInt((points - onesNumber) % 100) / 10;
        int hundredsNumber = Mathf.RoundToInt((points - tensNumber * 10 - onesNumber) % 1000 / 100);
```

Die Operation `%` wird Modulo genannt und berechnet den Rest bei Division von Ganzzahlen. 

Weise der UI nun noch die entsprechenden Sprites zu. Ergänze dafür folgendes in der `Update()`-Methode:

```csharp
        _ones.sprite = _digitSprites[onesNumber];
        _tens.sprite = _digitSprites[tensNumber];
        _hundreds.sprite = _digitSprites[hundredsNumber];
```

Gehe nun wieder in den Unity Editor und hänge das neue Script an das Objekt "CoinCounter", das du in der Hierarchy unter dem HUD findest. Setze Max Points auf 999. Die Images unter "Coin Digits" sind die Elemente, die du in der Hierarchy unter dem CointCounter findest. Ziehe diese in die Felder. Die Sprites für die Digits und den Coin findest du im Ordner Textures. Lege in den Digit Sprites jeweils für jede Zahl von 0 bis 9 ein neues Element an und ziehe die entsprechenden Sprites aus Textures > HUD hinein.

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/126153841-c308a639-f9d1-4b29-a4ca-8ccbd0dd2a53.png" width="400">
</p>

Wenn du möchtest, kannst du dir noch weitere Münzen aus dem Prefabs-Ordner in die Szene ziehen. Achte dabei darauf, dass der Coin Prefab das Coin Script angefügt haben muss. 

Wenn du das Spiel nun startest, siehst du den Zähler für die Münzen links oben im Bild.

## Leben anzeigen

Nun fügen wir noch eine Anzeige für die Gesundheit ein. Lege hierfür in der Hierarchy unter HUD ein neues leeres Objekt mit Rechtsklick > Create Empty an und nenne es "HealthMonitor". Lege unter dem Health Monitor fünf Image Objekte an mit Rechtsklick > UI > Image und nenne die Objekte Heart1 bis Heart5.

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/126155330-eb5af344-f571-4c28-9837-9d64801a64cd.png" width="400">
</p>

Richte das Objekt "HealthMonitor" so aus, dass es oben in der Mitte des Screens ist. Wähle das Objekt dafür in der Hierarchy aus und setze die Einstellungen wie auf der folgenden Abbildung.

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/126155735-343ae462-9fe5-4820-aaf0-fe72556e09f1.png" width="400">
</p>

Markiere nun alle fünf *Heart* Objekte unter dem *HealthMonitor* und stelle im Inspector folgendes ein:

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/126156121-c68bfd92-30d0-48be-858b-7a7bc20335c3.png" width="400">
</p>

Setze die *Heart* Objekte nun nebeneinander, indem du jedes Objekt einzeln in der Hierarchy auswählst und die Position X folgendermaßen setzt: Heart1 = 0, Heart2 = 128, Heart3 = 256, Heart4 = 384, Heart5 = 512

Markiere nun noch einmal alle *Heart* Objekte in der Hierarchy und weise allen im Inspector unter Source Image das Sprite Hud_10 (volles Herz) aus dem Ordner Textures zu. Aktiviere außerdem den Haken bei „Preserve Aspect“ unter "Raycast Padding". Damit stellst du sicher, dass das Sprite bei einer Größenänderung immer gleich in Höhe und Breite skaliert.

Öffne nun das *LevelManager* Script und lege für jedes der Herz-Bilder eine Variable an. 

```csharp
    public Image heart1;
    public Image heart2;
    public Image heart3;
    public Image heart4;
    public Image heart5;
```

Sollte noch ein Fehler auftreten, importiere die Unity.UI am Anfang des Scripts.

```csharp
using UnityEngine.UI;
```

Wir möchten jeweils Herzen verlieren, wenn wir in einen Spike laufen oder später mit einem *Enemy* (Gegner) zusammentreffen. Dafür benötigen wir die Sprites mit dem halben und dem leeren Herzen. Diese deklarieren wir auch im *LevelManager*.

```csharp
    public Sprite heartFull;  
    public Sprite heartHalf;
    public Sprite heartEmpty;
```

Erstelle zwei weitere Variablen für die maximale Gesundheit und das Zählen der Leben.

```csharp
    public int maxHealth;
    public int countHealth;
```

Gehe nun wieder in den Unity Editor, wähle den *LevelManager* in der Hierarchy aus und weise die Images und Sprites zu. Die entsprechenden Images für Heart1 bis Heart5 findest du in der Hierarchy unter dem *HealthMonitor*. Die Sprites findest du im Ordner Textures unter HUD. Setze Max Health auf 10, da wir fünf Herzen mit jeweils einer Option als halbes Herz haben.

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/126158320-c9fd0f58-7aab-41a6-85e1-8a6dc33a87e7.png" width="400">
</p>

Gehe nun wieder zurück in das *LevelManager* Script und initialisiere `countHealth` in der `Start()`-Methode. 

```csharp
        countHealth = maxHealth;
```

Erstelle außerdem eine neue Methode, um den Schaden am *Player* zu implementieren, indem der Schaden übergeben und von `countHealth` abgezogen wird.

```csharp
    public void PlayerHurt(int damage)
    {
        countHealth -= damage;
    }
```

Öffne nun das *PlayerHurt* Script und erstelle dort eine neue Variable für den Schaden.

```csharp
    public int damage;
```

Passe nun die `OnTriggerEnter2D()`-Methode an. Kommentiere das Respawning erst einmal aus, sodass der Spieler bei einer Verletzung nicht sofort stirbt. Implementiere stattdessen die `PlayerHurt()`-Methode aus dem *LevelManager* und übergebe den Wert aus `damage`.

```csharp
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // _levelManager.Respawn();
            _levelManager.PlayerHurt(damage);
        }
    }
```

Wechsel wieder in den Unity Editor und gebe den Objekten, die Schaden verursachen sollen, einen Wert für Damage indem du sie in der Hierarchy auswählst und den Wert im Inspector setzt. Setze ihn bei den *Spikes* beispielsweise auf 1, sodass bei einer Berührung ein halbes Herz abgezogen wird. 

Wenn wir das Spiel nun starten, können wir beobachten, wie der Count Health im *LevelManager* sinkt, wenn der *Player* in die *Spikes* läuft. Allerdings geht Count Health gerade noch in den Minusbereich, wenn der *Player* oft genug durch die *Spikes* läuft. Erweitere die `Update()`-Mathode im *LevelManager* Script, sodass das Respawning ausgeführt wird, wenn Count Health gleich "0" ist.

```csharp
    void Update()
    {
        if (countHealth <= 0)
        {
            Respawn();
        }
    }
```

Erweitere im *LevelManager* Script die `RespawnCo()`-Methode, damit die *Health* (Gesundheit) des *Player* nach dem Respawning wieder zurückgesetzt wird. Schreibe dazu folgenden Code unter die yield-Anweisung:

```csharp
       countHealth = maxHealth;
```

Damit das ganze nicht in einer Dauerschleife endet, wenn der Wert 0 ist, müssen wir noch sicherstellen, dass ein Respawn wirklich sinnvoll ist. Gehe dafür in das *LevelManager* Script und erstelle eine neue Variable.

```csharp
    private bool _respawn;
```

Passe die Update Methode so an, dass das Respwaning nur ausgeführt wird, wenn nicht gerade schon ein Respawn stattfindet.

```csharp
    void Update()
    {
        if (countHealth <= 0 && !_respawn)
        {
            Respawn();
            _respawn = true;
        }
    }
```

Füge in der `RespawnCo()`-Methode eine weitere Zeile ein mit der die neue Variable `_respawn` deaktiviert wird.

```csharp
    public IEnumerator RespawnCo()
    {
        player.gameObject.SetActive(false);
        yield return new WaitForSeconds(timeToRespawn);
        countHealth = maxHealth;
        _respawn = false;

        player.transform.position = player.respawnPos;
        player.gameObject.SetActive(true);
    }
```

Als nächstes wollen wir unsere neue Implementierung in der UI einpflegen, sodass die Anzeige der Herzen sich mit einem Schaden auch verändert. Erstelle dafür die Funktion `UpdateHealth()` im *LevelManager* Script und implementiere mit Hilfe einer switch-Anweisung die Varianten der Herzen-Anzeige.

```csharp
    public void UpdateHealth()
    {
        switch(countHealth)
        {
            case 10:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartFull;
                heart4.sprite = heartFull;
                heart5.sprite = heartFull;
                return;

            case 9:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartFull;
                heart4.sprite = heartFull;
                heart5.sprite = heartHalf;
                return;

            case 8:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartFull;
                heart4.sprite = heartFull;
                heart5.sprite = heartEmpty;
                return;

            case 7:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartFull;
                heart4.sprite = heartHalf;
                heart5.sprite = heartEmpty;
                return;

            case 6:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartFull;
                heart4.sprite = heartEmpty;
                heart5.sprite = heartEmpty;
                return;

            case 5:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartHalf;
                heart4.sprite = heartEmpty;
                heart5.sprite = heartEmpty;
                return;

            case 4:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartEmpty;
                heart4.sprite = heartEmpty;
                heart5.sprite = heartEmpty;
                return;

            case 3:
                heart1.sprite = heartFull;
                heart2.sprite = heartHalf;
                heart3.sprite = heartEmpty;
                heart4.sprite = heartEmpty;
                heart5.sprite = heartEmpty;
                return;

            case 2:
                heart1.sprite = heartFull;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                heart4.sprite = heartEmpty;
                heart5.sprite = heartEmpty;
                return;

            case 1:
                heart1.sprite = heartHalf;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                heart4.sprite = heartEmpty;
                heart5.sprite = heartEmpty;
                return;

            case 0:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                heart4.sprite = heartEmpty;
                heart5.sprite = heartEmpty;
                return;

            default:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                heart4.sprite = heartEmpty;
                heart5.sprite = heartEmpty;
                return;
        }
    }
```

Diese Methode muss in der `RespawnCo()`-Funktion und in der `PlayerHurt()`-Funktion aufgerufen werden:

```csharp
    public IEnumerator RespawnCo()
    {
        player.gameObject.SetActive(false);
        yield return new WaitForSeconds(timeToRespawn);
        countHealth = maxHealth;
        _respawn = false;
        UpdateHealth();

        player.transform.position = player.respawnPos;
        player.gameObject.SetActive(true);
    }
    
    public void PlayerHurt(int damage)
    {
        countHealth -= damage;
        UpdateHealth();
    }
```

Die Anzeige der Herzen verändert sich nun mit der Verletzung des *Player* und das Kapitel ist damit beendet. :)

[Hier](https://github.com/FrankFlamme/UnityKidsWorkshop/releases/tag/0.8) findest du die Musterlösung zum Herunterladen und [hier](/docs/09-enemies.md) geht es weiter zum nächsten Kapitel.
