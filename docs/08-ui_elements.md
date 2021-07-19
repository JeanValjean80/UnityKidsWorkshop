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
    private Image _tenth;

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

