# Bewegung des Spielers
In diesem Kapitel werden wir die Bewegung des Spielers  in der Szene programmieren. 

## Laufen
Öffne zuerst das Skript, das für den Spieler angelegt wurde (Player.cs). Hier müssen zwei Variablen erstellt werden: `speed` ist die Geschwindigkeit, mit der unser Spieler sich durch die Szene bewegen wird und `_rb` ist der Rigidbody, mit dessen Hilfe der Spieler auf physikalische Einwirkungen reagieren kann. 

```
public float speed;    
private Rigidbody2D _rb;
```

In der `Start()`-Methode wird der Rigidbody initialisiert. Wir weisen `_rb` also die Komponente Rigidbody2D zu, die im Basisspiel in Unity Hub bereits an den Spieler gehängt wurde.

```
_rb = GetComponent<Rigidbody2D>();
```

Die Bewegung implementieren wir in der `Update()`-Methode. Diese Methode wird mit jeder Aktualisierung des Screens ein Mal aufgerufen. 
Für die Bewegung bauen wir eine bedingte Anweisung (if-Anweisung), in der wir die Bewegung des Rigidbody, die durch die Unity bereitgestellt wird, ansprechen.

```
if (Input.GetAxisRaw("Horizontal") > 0f)
{
    _rb.velocity = new Vector2(speed, _rb.velocity.y);
}
else if (Input.GetAxisRaw("Horizontal") < 0f)
{
    _rb.velocity = new Vector2(-speed, _rb.velocity.y);
}
else
{
    _rb.velocity = new Vector2(0f, 0f);
}
```

Wenn du nun im Unity Editor deinen Spieler auswählst, kannst du rechts im Inspector unter deinem Player-Skript die gewünschte Geschwindigkeit "speed" einstellen. Setze sie zum Beispiel auf 5. Wenn du das Spiel nun startest, kann sich dein Spieler mithilfe der Pfeiltasten nach links und rechts bewegen. 

![alt text](<img width="329" alt="Screenshot 2021-06-20 at 23 19 00" src="https://user-images.githubusercontent.com/75975986/122826402-5fb46880-d2e3-11eb-83d0-96a0bf2aa350.png"> "Edit player speed")

## Springen
Für das Springen brauchen wir eine dritte Variable im Player-Skript, die Sprunggeschwindigkeit jump.

```
public float jump;
```

In der Update()-Methode fügen wir eine weitere if-Anweisung hinzu, in der wir die vertikale Bewegung des Spielers ansprechen, wenn die Leertaste gedrückt wird.

```
if (Input.GetButtonDown("Jump"))
{
    _rb.velocity = new Vector2(_rb.velocity.x, jump);
}
```

Wenn du nun im Unity Editor die Sprunggeschwindigkeit einstellst, kann dein Spieler auch springen. Du kannst die Sprunggeschwindigkeit genau wie die Laufgeschwindigkeit einstellen, indem du deinen Spieler auswählst und im Inspector rechts unter deinem Player-Skript die Variable "jump" zum Beispiel auf 30 setzt. 

Der Spieler kann jetzt allerdings beliebig oft in der Luft springen. Wir wollen als nächstes dafür sorgen, dass er nur abspringen kann, wenn er sich auf dem Boden befindet. Dafür erstellen wir zuerst zwei neue Layer im Unity Editor.

![alt text](<img width="349" alt="Screenshot 2021-06-21 at 22 39 10" src="https://user-images.githubusercontent.com/75975986/122826414-6511b300-d2e3-11eb-9ddc-e68373c2c766.png"> "Edit player speed")