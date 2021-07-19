# UI Elemente 

In diesem Kapitel fügen wir dem Spiel UI Elemente hinzu, sodass wir sehen, wie viele Münzen gesammelt wurden und wie viele Leben der Spieler noch hat. Du solltest hier mit einem funktionsfähigen Stand aus dem [letzten Kapitel](/docs/07-level_elements.md) starten. Im Notfall kannst du das Musterprojekt [hier](https://github.com/FrankFlamme/UnityKidsWorkshop/releases/tag/0.7) nutzen.

## Münzen zählen

Zuerst möchten wir, dass unsere gesammelten Münzen nun auch in der UI gezählt und angezeigt werden. Dafür erstellen wir ein neues Script „CoinCounter“ im Ordner Scripts.

Öffne das neue Script in Visual Studio und erstelle eine neue Variable für den Level Manager. 

```csharp
    private LevelManager _levelManager;
```

Instanziiere den LevelManager wieder in der `Start()`-Methode.

```csharp
        _levelManager = FindObjectOfType<LevelManager>();
```

Da wir die Sprites für den Zähler auch mit dem Script verbinden wollen, erstellen wir hierfür auch Variablen. Nutze das Keyword `SerializeField`, um eine private Variable im Editor sichtbar zu machen. Wir wollen auf die Sprites nicht von überall zugreifen, deshalb sollten sie im Code auf private deklariert sein.
Da wir sie aber manuell verknüpfen, nutzen wir `SerializeField`. Das Keyword `Header(„Digit Coin Sprites“)` dient der Anzeigefreundlichkeit im Editor und ist die Überschrift der Felder.

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

Vermutlich sind die Typen „Image“ in Visual Studio nun rot hinterlegt. Das liegt daran, dass die Implementierung der Unity.UI Klassen fehlt. Füge dafür folgendes an den Anfang des Scripts an.

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

Implementiere nun die `Update()`-Methode, indem du `points` auf den CoinCount-Wert des LevelManagers setzt. (Dort, wo wir im letzten Kapitel die Anzahl der Coins im Feld gezählt haben). Mit einer if-Condition initialisieren wir jetzt die UI und setzen die drei einzelnen Sprites auf die erste Stelle des Arrays.

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

Markiere nun alle fünf Heart Objekte unter dem Health Monitor und stelle im Inspector die folgendes ein:

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/126156121-c68bfd92-30d0-48be-858b-7a7bc20335c3.png" width="400">
</p>

Setze die Objekte nun nebeneinander in der Health Monitor, indem du jedes Heart Objekt einzeln in der Hierarchy auswählst und die Position X folgendermaßen setzt: Heart1 = 0, Heart2 = 128, Heart3 = 256, Heart4 = 384, Heart5 = 512

Markiere nun noch einmal alle Heart Objekte in der Hierarchy und weise allen im Inspector unter Source Image das Sprite Hud_10 (volles Herz) aus dem Ordner Textures zu. Aktiviere außerdem den Haken bei „Preserve Aspect“ unter "Raycast Padding". Damit stellst du sicher, dass das Herz-Sprite bei einer Größenänderung immer gleich in Höhe und Breite skaliert.

Öffne nun das LevelManager Script in Visual Studio und lege für jedes der Herz-Bilder eine Variable an. 

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

Wir möchten jeweils Herzen verlieren, wenn wir einen Enemy treffen oder in einen Spike laufen. Dafür benötigen wir die Sprites mit dem halben und dem leeren Herzen. Weise diese wir auch im LevelManager zu.

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

Gehe nun wieder in den Unity Editor, wähle den Level Manager in der Hierarchy aus und weise die Images und Sprites zu. Die entsprechenden Images für Heart1 bis Heart5 findest du in der Hierarchy unter dem Health Monitor. Die Sprites findest du im Ordner Textures unter HUD. Setze Max Health auf 10, da wir fünf Herzen mit jeweils einer Option als halbes Herz haben.

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/126158320-c9fd0f58-7aab-41a6-85e1-8a6dc33a87e7.png" width="400">
</p>

Gehe nun wieder zurück in das LevelManager Script in Visual Studio und initialisiere `countHealth` in der `Start()`-Methode. 

```csharp
        countHealth = maxHealth;
```

Erstelle außerdem eine neue Methode, um den Schaden an dem Spieler zu implementieren, indem der Schaden übergeben und von `countHealth` abgezogen wird.

```csharp
    public void PlayerHurt(int damage)
    {
        countHealth -= damage;
    }
```

Öffne nun das PlayerHurt Script und erstelle dort eine neue Variable für den Schaden.

```csharp
    public int damage;
```

Passe nun die `OnTriggerEnter2D()`-Methode an. Kommentiere das Respawning erst einmal aus, sodass der Spieler bei einer Verletzung nicht sofort stirbt. Implementiere stattdessen die `PlayerHurt()`-Methode aus dem LevelManager und übergebe den Wert aus `damage`.

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

Wechsle wieder in der Unity Editor und gebe den Objekten, die Schaden verursachen sollen, einen Wert für Damage indem du sie in der Hierarchy auswählst und den Wert im Inspector setzt. Setze ihn bei den Spikes beispielsweise auf 1, sodass bei einer Berührung ein halbes Herz abgezogen wird. 

Wenn wir das Spiel nun starten, können wir beobachten, wie der Count Health im Level Manager sinkt, wenn der Spieler in die Spikes läuft. Allerdings geht Count Health gerade auch in den Minusbereich, wenn der Spieler oft genug durch die Spikes läuft. Erweitere die `Update()`-Mathode im PlayerHurt Script, sodass das Respawning ausgeführt wird, wenn Count Health auf 0 ist.

```csharp
    void Update()
    {
        if (countHealth <= 0)
        {
            Respawn();
        }
    }
```
