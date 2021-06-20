# Bewegung des Spielers
In diesem Kapitel werden wir die Bewegung des Spielers  in der Szene programmieren. 

## Laufen
Öffne zuerst das Skript, das für den Spieler angelegt wurde (Player.cs). Hier müssen zwei Variablen erstellt werden: speed ist die Geschwindigkeit, mit der unser Spieler sich durch die Szene bewegen wird und _rb ist der Rigidbody, mit dessen Hilfe der Spieler auf physikalische Einwirkungen reagieren kann. 

```
public float speed;    
private Rigidbody2D _rb;
```

In der Start()-Methode wird der Rigidbody initialisiert. Wir weisen _rb also die Komponente Rigidbody2D zu, die im Basisspiel in Unity Hub bereits an den Spieler gehängt wurde.

```
_rb = GetComponent<Rigidbody2D>();
```

Die Bewegung implementieren wir in der Update()-Methode. Diese Methode wird mit jeder Aktualisierung des Screens ein Mal aufgerufen. 
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

Wenn du nun in Unity Hub deinen Spieler auswählst, kannst du rechts im Inspector unter deinem Player-Skript die gewünschte Geschwindigkeit (speed) einstellen. Setze sie zum Beispiel auf 5. Wenn du das Spiel nun startest, kann sich dein Spieler mithilfe der Pfeiltasten nach links und rechts bewegen. 

<Bild>

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

Wenn du nun in Unity Hub die Sprunggeschwindigkeit einstellst, kann dein Spieler auch springen. Du kannst die Sprunggeschwindigkeit genau wie die Laufgeschwindigkeit einstellen, indem du deinen Spieler auswählst und im Inspector rechts unter deinem Player-Skript die Variable jump zum Beispiel auf 30 setzt.