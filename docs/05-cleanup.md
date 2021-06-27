# Clean up 

In diesem Kapitel werden wir ein paar kleinere Aufräumarbeiten und Zusatzfunktionen in unser Projekt bringen, sodass wir im weiteren Verlauf einfacher weiterarbeiten können. Du solltest hier mit einem funktionsfähigen Stand aus dem [letzten Kapitel]("docs/04-camera.md") starten. Im Notfall kannst du das Musterprojekt [hier](https://github.com/FrankFlamme/UnityKidsWorkshop/releases/tag/0.4) nutzen.

## Player haftet an Wänden

Mit dem aktuellen Stand kann sich der Spieler an einer Wand halten und sich so wieder nach oben „kämpfen“. Das wollen wir natürlich nicht.
Erstelle im Project Fenster im Unity Editor einen neuen Ordner unter Assets und nenne ihn "Materials". Erstelle in diesem Ordner ein neue Material indem du rechtsklickst und dann unter Create > 2D > Physics Material 2D wählst. Nenne das Material "PlayerMat" und setze die Friction rechts im Inspector auf 0.

Wähle nun den Player in deiner Hierarchy aus. Du findest im Inspector unter dem Box Collider 2D ein Feld für ein Material. Ziehe dein neues Material "PlayerMat" in dieses Feld.

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/123544419-77f90d00-d753-11eb-840e-7d145706217f.png" width="400">
</p>

Es sollte nun nicht mehr möglich sein, dass dein Spieler sich an einer Wand "hochzieht".

## Killzone

Zur Zeit fällt der Player unendlich weit in die Tiefe, wenn er von einer Plattform fällt. Wir wollen natürlich, dass das Spiel endet, sobald der Spieler runterfällt. Dafür erstellen wir eine Killzone. 

Erstelle ein neues leeres GameObject in deiner Szene indem du in der Hierarchy rechtsklickst und "Create Empty" auswählst. Nenne das Objekt "KillZone" und wähle es mit einem Klick aus. Erstelle nun einen Box Collider 2D an der KillZone indem du rechts im Inspector auf "AddComponent" klickst und dort den Box Collider 2D aussuchst. Setze "Size" bei "X" auf 40.

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/123544699-c3f88180-d754-11eb-889e-444ff202a7a6.png" width="400">
</p>

Wir wollen, dass die KillZone sich mit der Kamera mitbewegt. Dafür ziehen wir die KillZone in der Hierarchy in die MainCamera rein, sodass ihre Position sich relativ zu der Kamera mit verändert. 

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/123544756-04f09600-d755-11eb-8ff9-1d27a7f0632e.png" width="350">
</p>

Ziehe die KillZone außerdem in der Szene mit dem Move Tool unter die Böden wie im folgenden Bild. 

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/123544853-99f38f00-d755-11eb-9d70-0b567b974d40.png" width="500">
</p>

Da der Spieler so gerade nur auf der KillZone "landen" würde, setzen wir hier noch einen Trigger. Setze im Box Collider der KillZone einen Haken bei "Is Trigger". Nun wollen wir noch die Funktion implementieren, die ausgeführt wird, wenn der Player diesen Trigger berührt. Öffne dafür wieder das Skript Player.cs und erstelle die neue Methode `OnTriggerEnter2D` mit folgendem Inhalt. 

```csharp
void OnTriggerEnter2D(Collider2D collision)
{
  if (collision.CompareTag("KillZone"))
  {
    gameObject.SetActive(false);
  }
}
```

Auf diese Art deaktivieren wir den Player erstmal, wenn er die KillZone berührt. Das wird später noch angepasst. 

Um die Killzone zu identifizieren, arbeiten wir mit Tags. Damit ist es sehr einfach, Objekte in bestimmte Gruppen zu ordnen. Markiere die Killzone in der Hierarchy und klicke im Inspector oben rechts auf Tags > Add Tag. Füge nun über das Plus-Symbol den Tag „KillZone“ hinzu. Wähle die KillZone wieder in der Hierarchy aus und setze im Inspector den Tag auf "KillZone".

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/123545059-8bf23e00-d756-11eb-8857-d6c3fc085d67.png" width="500">
</p>

Dein Player fällt nun nicht mehr ins bodenlose und damit ist auch dieses Kapitel geschafft! :) 

[Hier](https://github.com/FrankFlamme/UnityKidsWorkshop/releases/tag/0.5) findest du die Musterlösung zum Herunterladen und [hier]("docs/06-levelelements.md") geht es weiter zum nächsten Kapitel.
