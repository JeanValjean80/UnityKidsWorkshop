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
