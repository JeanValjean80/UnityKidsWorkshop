# Bugfixes, Export und visuelle Anpassungen

In diesem Kapitel fügen wir dem Spiel Effekte für das Respawning hinzu, nehmen einige Fehlerkorrekturen vor und exportieren das Spiel so, dass du Unity nicht mehr zum Ausführen des Spiels benötigst. Du solltest hier mit einem funktionsfähigen Stand aus dem [letzten Kapitel](/docs/09-enemies.md) starten. Im Notfall kannst du das Musterprojekt [hier](https://github.com/FrankFlamme/UnityKidsWorkshop/releases/tag/0.9) nutzen.

## Hintergrund

Zuerst möchten wir, dass unser Hintergrund etwas mehr nach einem richtigen Spiel aussieht. Dafür nutzen wir den [Hintergrund](https://github.com/FrankFlamme/UnityKidsWorkshop/releases/download/1.0/backgroundEmpty.png), den du dir aus den Assets des Kapitels laden kannst.

Lade dir den Hintergrund auf deinen Desktop und wechsele im Unity Editor in den Ordner "Textures". Klicke mit der rechten Maustaste in den Ordner und wähle "Import New Asset". Wähle nun die Datei von deinem Desktop aus und klicke auf "Import".

Die Datei befindet sich nun in deinem "Textures" Ordner.
Klicke nun das Bild im Ordner an und nimm die Einstellungen im Inspector wie im Bild dargestellt vor:

<p align="center">
<img src="https://user-images.githubusercontent.com/13068729/126160733-42be8760-13bd-475d-9356-efdb4c61ef79.png">
</p>

Füge nun das Bild in die Szene ein, indem du es vom "Texture"-Ordner in die Szene ziehst. Vergrößere den Hintergrund so, dass du ihn etwas größer als die Größe der Kamera ziehst. Nenne das Objekt in der Hierarchy "BackgroundPicture". Setze das Flag "Order in Layer" im Inspector auf -1, damit das Bild wirklich im Hintergrund ist und keine anderen Items verdeckt.

Damit sich das Hintergrundbild mit der Kamera mitbewegt, legst du unterhalb der "Main Camera" ein neues, leeres Objekt mit dem Namen "CamBackground" an und ziehst "BackgroundPicture" als Child-Objekt darunter.

<p align="center">
<img src="https://user-images.githubusercontent.com/13068729/126161431-655eee95-eeb5-41eb-b8fc-0a87b54d364e.png">
</p>

## Partikel Effekt

Nun bauen wir noch einen realistischeren Effekt ein, wenn der Player alle Herzen verbraucht hat und respawned wird. Dafür nutzen wir einen „Particle Effect“.

Klicke in der Hierarchy mit der rechten Maustaste auf Level1 wähle GameObject > Effects > Particle System. Setze im Inspector "Duration" auf 1, "Start Lifetime" auf 1 und "Start Speed" auf 2.5. Unter "Start Color" kannst du die Farbe des Effektes aussuchen.
Setze im Reiter "Shape" die Einstellung "Shape" auf Circle und "Radius" auf 0.5.
Setze unter dem Reiter "Emission" die "Rate over Time" auf 0. Lege bei "Bursts" einen neuen Eintrag mit folgenden Einstellungen an. 

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/126282625-bb852c98-0031-4eb8-9df4-c07794d4079b.png" width="400"> 
</p>

Setze einen Haken bei "Size over Lifetime" und wähle bei "Size" die Curve aus. Stelle den Effekt folgendermaßen ein:

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/126284822-7ddfe30c-eed9-4d62-ab3c-52c22511bcee.png" width="400"> 
</p>

Setze nun unter Transform die "Rotation X" auf 0. Der Partikeleffekt sollte nun etwa so aussehen: 

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/126284837-5475e87c-a60f-4c0f-b073-df32fc906ce3.png" width="500"> 
</p>

Nehme nun noch den Haken bei "Looping" heraus, sodass der Effekt nur ein Mal ausgeführt und nicht ständig wiederholt wird. 

Nenne das Objekt in der Hierarchy um in "PlayerDied". Verschiebe es in den Ordner Prefabs und lösche es aus der Hierarchy. Wir brauchen es dort nicht mehr, weil wir es über ein Script aufrufen.

Öffne in Visual Studio das LevelManager Script und lege eine neue Variable für den Particle Effect an.

```csharp
    public GameObject playerDied;
```

Instanziiere den Particle Effect in der `RespawnCo()`-Methode nachdem der Player deaktiviert wurde: 

```csharp
    public IEnumerator RespawnCo()
    {
        player.gameObject.SetActive(false);
        Instantiate(playerDied, player.transform.position, player.transform.rotation);
        ...
```

Öffne den Unity Editor und wähle den Level Manager in der Hierarchy. Ziehe PlayerDied aus dem Ordner Prefabs in die Variable "Player Died" im Inspector des Level Managers. Wenn der Player nun stirbt, wird der Effekt ausgeführt. 

Allerdings wird gerade jedes Mal, wenn der Effekt ausgeführt wird, ein geklontes Objekt erstellt, das im Spiel zurück bleibt und Speicherplatz belegt. Das wollenw ir auf Dauer verhindern, da es unnötig Speicher belegt und das Spiel langsamer macht. Erstelle dafür ein neues Script im Ordner Scripts und nenne es "DestroyObjects".

Öffne das neue Script in Visual Studio und erstelle eine neue Variable für die Lebenszeit des Objektes. 

```csharp
    public float lifeTime;
```

Passe die `Update()`-Methode so an, dass das Objekt zerstört wird, wenn die Lebenszeit verstrichen ist. 

```csharp
    void Update()
    {
        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0f)
        {
            Destroy(gameObject);
        }
    }
```

Öffne den Unity Editor und wähle das PlayerDied Objekt im Ordner Prefabs aus. Hänge das neue Script an das Prefab an indem du im Inspector über "Add Component" nach dem Script suchst. Stelle "Life Time" auf 1.1, da das die Effektdauer unseres Effekts ist. 

Der Effekt wird nun nach jedem Ausführen auch wieder aus unserem Spiel gelöscht. 

## Prefabs

Um die Elemente später mehrfach zu verwenden, können wir aus einigen Objekten Prefabs machen, indem wir die Objekte aus der Hierarchy in den Prefabs-Ordner ziehen. Ziehe die folgenden Objekte aus der Hierarchy in den Ordner Prefabs:

* Player
* HUD
* LevelManager
* Spikes
* Snail
* MovingPlatform
* PinkEnemy

## Bugfixes

Möglicherweise bleibt der Player oder die Enemies beim laufen im Spiel hängen. Das kann daran liegen, dass zwei Box Collider miteinander kollidieren. Um das zu beheben bearbeiten wir erst einmal alle Enemies, indem wir die Prefabs anpassen. Markiere dafür die Enemies im Ordner Prefabs anstatt in der Hierarchy. 

Verkleinere den inneren Box Collider (ohne Trigger) und erstelle einen Circle Collider, der nur knapp den Boden berührt. Die Collider sollten etwa wie auf dem folgenden Bild aussehen. 

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/126289594-7fc68824-210e-488c-adb6-043e0e250145.png" width="300"> 
</p>

Bei dem Player können wir keinen Circle Collider nutzen, da dadurch weitere Fehler implementiert werden. Hier kannst du ein wenig mit dem Box Collider experimentieren und ihn beispielsweise am unteren Ende etwas nach oben schieben. 

## Das Spiel bauen

Zum Abschluss erstellen wir noch ein ausführbares Programm, das wir auf dem PC laufen lassen können, ohne es mit Unity zu starten. Klicke dafür in Unity auf File > Build Settings... . Setze den Haken bei Scenes/Level1. Falls dies noch nicht bei dir zu sehen ist, füge es über "Add Open Scenes" hinzu. Wähle unter "Platform" PC, Mac & Linux Standalone aus. Wähle unter "Target Platform" aus auf welchem Betriebssystem du das Spiel starten möchtest. 

Die restlichen Einstellungen müssen nicht verändert werden. Mit einem Klick auf „Build And Run“ kannst du das Spiel nun exportieren. Lege dir am besten einen Ordner auf dem Desktop an, in dem du das Spiel ablegen kannst. Das Exportieren kann je nach Computer und Leistung ein bisschen dauern.

Du hast das Tutorial damit beendet! :) [Hier](https://github.com/FrankFlamme/UnityKidsWorkshop/releases/tag/1.0) findest du die Musterlösung zum Herunterladen.
