# Bewegung der Kamera

In diesem Kapitel werden wir die Bewegung der Kamera anpassen, sodass sie mit dem *Player* mitläuft. Du solltest hier mit einem funktionsfähigen Stand aus dem [letzten Kapitel](/docs/03-animations.md) starten. Im Notfall kannst du das Musterprojekt [hier](https://github.com/FrankFlamme/UnityKidsWorkshop/releases/tag/0.3) nutzen.

## Let´s go

Erstelle als erstes ein neues Script. Gehe dazu im Project-Fenster des Unity Editors auf den Ordner Scripts, mache einen Rechtsklick und wähle Create > C# Script. Nenne das neue Script "CameraMovement". Es ist wichtig, dass du das neue Script nicht "Camera" nennst, da Unity selbst eine integrierte Funktion hat, die so heißt. Öffne das Script und erzeuge drei Variablen, um den *Player* und die Kamera miteinander verknüpfen zu können. `target` ist das Objekt, dem die Kamera folgen soll. `follow` ist ein Wert, um den Bereich zu bestimmen, um welchen sich die Kamera um das Ziel ("target") herum bewegen soll. `_targetPos` ist eine private Variable, die die Position des Zielobjektes beinhalten soll. 

```csharp
public GameObject target;
public float follow;
private Vector3 _targetPos;
```

Hänge nun das Script an die Kamera an. Dafür wählst du die Kamera in der Hierarchy aus und klickst dann im Inspector auf "Add Component". Da kannst du dann in die Suche "Camera Movement" eingeben und das Script auswählen. Du siehst im Inspector dann deine gerade neu angelegten Variablen. Setze Target auf den *Player* indem du den *Player* aus der Hierarchy einfach in das Feld hinter "Target" ziehst. Und setze Follow zum Beispiel auf 5, dieser Wert wird später auf die Bewegung der Kamera addiert, um diese etwas weiter zu schieben. 

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/123543452-cce65480-d74e-11eb-8ae2-3c6111375b4b.png" width="400">
</p>

Passe nun die `Update()`-Methode im Kamera Script folgendermaßen an:

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

Wenn du das Spiel nun startest, bewegt sich die Kamera bereits mit dem *Player* mit. Allerdings sind die Übergänge bei einem Richtungswechsel nicht besonders sanft. Das lösen wir mit der Lerp-Funktion, eine mathematische Funktion, die die Position der Kamera über einen Zeitraum gleichmäßiger zu setzen.

Erstelle im Script "CameraMovement" eine neue Variable `smoothness`.

```csharp
public float smoothness;
```

Ersetze die letzte Zeile in der `Update()`-Methode, in der wir `transform.position` setzen durch den unten stehenden Code. Dabei ist `Time.deltaTime` die Zeit, die zwischen zwei Frames vergeht. Also 1/30 fps, wenn das Spiel mit 30 fps läuft.

```csharp
transform.position = Vector3.Lerp(transform.position, _targetPos, smoothness * Time.deltaTime);
```

Wenn du im Unity Editor nun die Variable Smoothness zum Beispiel auf 1 setzt, bewegt sich die Kamera nun gleichmäßig mit dem Spieler mit.

Damit hast du auch dieses Kapitel geschafft! :) 

[Hier](https://github.com/FrankFlamme/UnityKidsWorkshop/releases/tag/0.4) findest du die Musterlösung zum Herunterladen. Im [nächsten Kapitel](/docs/05-cleanup.md) machen wir ein paar kleine Aufräumarbeiten und Zusatzfunktionen an unserem Projekt. 
