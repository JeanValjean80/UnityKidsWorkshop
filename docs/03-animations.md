# Animation des Spielers

In diesem Kapitel werden wir die Bewegung des Spielers animieren, sodass es realistischer aussieht, wenn er läuft und springt. Du solltest hier mit einem funktionsfähigen Stand aus dem [letzten Kapitel](/docs/02-playermovement.md) starten. Im Notfall kannst du das Musterprojekt [hier](https://github.com/FrankFlamme/UnityKidsWorkshop/releases/tag/0.2) nutzen.

## Verbindungen im Unity Editor

Zuerst erstellen wir dafür die Bilder der Bewegung (Sprites) mit einer Animation. Öffne im Unity Editor dafür das Animation-Fenster indem du im Menü unter Window > Animation > Animation auswählst. Du kannst das Animation-Fenster in deinen Unity Editor integrieren, indem du den Animation Tab an eine Stelle deiner Wahl im Unity Editor ziehst. So könnte das zum Beispiel bei dir aussehen: 

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/123328585-0dba4f80-d53c-11eb-8340-6681b95036d6.png" width="700">
</p>

Erstelle im Project Tab unter Assets einen neuen Ordner und nenne ihn "Animations". Erstelle nun eine neue Animation indem du im Animations-Tab auf "Create" klickst. Nenne die Animation "PlayerWalk" und speichere sie in deinem neuen Ordner Animations ab. Wir fügen nun Sprites in die Animation ein. Gehe dafür im Project Tab auf den Ordner "Textures", dort findest du alle Sprite Sheets unseres Projektes. Klicke auf den Pfeil rechts am "MalePlayer". Du solltest nun, wie auf dem letzten Bild abgebildet, die einzelnen Sprites des Spielers sehen.

Füge 6 Sprites in deine Animation ein indem du sie einfach in die Zeitleiste der Animation ziehst. Damit die Bewegung realistisch aussieht, kannst du die Sprites mit einem Abstand von 10 Sekunden in folgender Reihenfolge einfügen: MalePlayer_10, MalePlayer_9, MalePlayer_0, MalePlayer_10, MalePlayer_9, MalePlayer_0. Fertig! Wenn du auf den Play-Button klickst, kannst du dir nun die Animation auf deinem Spieler ansehen. 

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/123330439-4eb36380-d53e-11eb-99f7-c093eb2aa24c.png" width="500">
</p>

Das selbe tun wir nun noch einmal mit der Animation für das Springen und für das Stehen. Erstelle eine neue Animation indem du im Animation-Tab auf den Pfeil nach unten neben "PlayerWalk" klickst und dort "Create New Clip..." auswählst. Nenne die Animation "PlayerJump" und speichere sie wieder im Ordner Animations. Ziehe nun das Sprite „MalePlayer_20“ in die Zeitleiste bei 0 Sekunden.

Erstelle eine weitere Animation auf dieselbe Art. Nenne diese "PlayerIdle" und speichere sie auch wieder im Animations Ordner. Ziehe das Sprite "MalePlayer_0" in die Zeitleiste bei 0 Sekunden.

Wir haben nun alle Animationen erstellt, die wir für den Player brauchen. Als nächstes müssen wir die Übergänge der Animationen zueinander verknüpfen. Öffne das für unter im Menü unter Window > Animation > Animator. Hier siehst du deine Animationen als Status. Ihre Anordnung sollte in etwa so aussehen: 

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/123332565-f29e0e80-d540-11eb-95d4-25aa842bb220.png" width="600">
</p>

Der Status "PlayerIdle" ist unser Standart State, er sollte orange eigefärbt sein. Falls er das nicht ist, setze ihn als Standard State, indem du mit der rechten Maustaste auf ihn klickst und "Set as Layer Default State" wählst. Erstelle nun zwei neue Parameter indem du im Animator auf Parameters und dann auf das Plus-Symbol klickst. Erstelle einen Float und nenne ihn "Speed". Erstelle dann einen Bool und nenne ihne "Grounded". Diese beiden Parameter nutzen wir, um zu bestimmen, wann ein Statusübergang passiert. 

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/123333241-c59e2b80-d541-11eb-8102-5e83862e67a2.png" width="500">
</p>

Um die erste Verbindung zwischen den Status herzustellen klicke im Animator mit der rechten Maustaste auf PlayerIdle, wähle "Make Transition" und klicke auf PlayerWalk. Es ist ein Verbindungspfeil zwischen PlayerIdle und PlayerWalk entstanden. Klicke auf den Pfeil, um die Eigenschaften des Statusübergangs zu bearbeiten. Klappe im Inspector die Settings auf. Entferne die Haken bei "Fixed Duration" und "Has Exit Time" und setze die "Transition Duration" auf 0. Füge unter Conditions eine neue Bedingung ein, indem du auf das Plus-Symbol unter der Tabelle klickst. Unsere gewünschte Bedingung ist, dass Speed größer als 0.1 ist. So sollte das Ganze aussehen: 

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/123334339-3db92100-d543-11eb-9849-52fe954b49d9.png" width="300">
</p>

Erstelle nun alle anderen Statusübergänge: 
- Von PlayerWalk auf PlayerIdle mit der Bedingung Speed ist kleiner als 0.1
- Von PlayerIdle auf PlayerJump mit der Bedingung Grounded ist false
- Von PlayerWalk auf PlayerJump mit der Bedingung Grounded ist false
- Von PlayerJump auf PlayerIdle mit der Bedingung Grounded ist true

Denke daran in allen Statusübergängen die Haken bei "Fixed Duration" und "Has Exit Time" zu entfernen und die "Transition Duration" auf 0 zu setzen. Der Animator sieht dann in etwa so aus:

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/123541076-b89c5a80-d742-11eb-96d8-887068d64335.png" width="500">
</p>

## Anpassungen im Code 

Wir wollen nun Anpassungen im Player Skript machen, sodass die Verbindungen, die wir zuvor im Unity Editor erstellt haben, auch aktiv werden können. Öffne wieder das Player-Skript und erstelle eine neue Variable `_animator`. 

```csharp
private Animator _animator;
```

Initialisiere den Animator mit folgenedem Code in der `Start()`-Methode:

```csharp
_animator = GetComponent<Animator>();
```

Wir erweitern die `Update()`-Methode, indem wir die beiden zuvor im Animator erstellten Parameter setzen. Für `Speed`nutzen wir hier die Absolut-Funktion, da wir im Animator unabhängig von dem Vorzeichen nur wissen wollen, ob die Geschwindigkeit unter oder über 0.1 liegt. So werden nun die Parameter gesetzt, die für unsere Statusübergänge überprüft werden.

```csharp
_animator.SetFloat("Speed", Mathf.Abs(_rb.velocity.x));
_animator.SetBool("Grounded", grounded);
```

Aktuell kann der Spieler beim laufen und Springen nur in eine Richtung schauen. Das können wir anpassen, indem wir in der `Update()`-Methode die ersten beiden if-Conditions um die Änderung der Skalierung anpassen. So sehen die if-Conditions nach der Erweiterung aus:

```csharp
if (Input.GetAxisRaw("Horizontal") > 0f)
  {
    _rb.velocity = new Vector2(speed, _rb.velocity.y);
    transform.localScale = new Vector3(1f, 1f, 1f);
  }
  else if (Input.GetAxisRaw("Horizontal") < 0f)
  {
    _rb.velocity = new Vector2(-speed, _rb.velocity.y);
    transform.localScale = new Vector3(-1f, 1f, 1f);
  }
```

Wenn du das Spiel nun in Unity startest, kann dein Player in beide Richtungen laufen und springen während seine Bewegung dementsprechend animiert wird. :)

[Hier](https://github.com/FrankFlamme/UnityKidsWorkshop/releases/tag/0.3) findest du die Musterlösung aus diesem Kapitel. Im [nächsten Kapitel](/docs/04-camera.md) kümmern wir uns um die Kameraführung während des Spiels. 
