# Checkpoints und neue Level Elemente

In diesem Kapitel werden wir Checkpoints in unser Spiel einbauen, sodass unser Spiel an bestimmten Stellen neu startet, sobald er runtergefallen ist. Du solltest hier mit einem funktionsfähigen Stand aus dem [letzten Kapitel]("docs/05-cleanup.md") starten. Im Notfall kannst du das Musterprojekt [hier](https://github.com/FrankFlamme/UnityKidsWorkshop/releases/tag/0.5) nutzen.

## Checkpoint in die Szene einfügen

Im letzten Kapitel haben wir eine KillZone unter den Plattformen erstellt und den Spieler deaktiviert, sobald er durch diese gefallen ist. Wir wollen das Spiel nun so umbauen, dass er an einer bestimmten Stelle im Spiel wieder erscheint. Dafür bauen wir uns einen Kontrollpunkt in Form einer Flagge. 

Öffne den Ordner Textures in dem Project Fenster und klappe dort die Items aus. Ziehe eine der geschlossenen Fahnen (zum Beispiel Items_0) in die Szene und nenne das neue Objekt "CheckpointFlag". Gehe nun in den Ordner Scripts und erstelle ein neues Script (Rechtsklick > Create > C# Script), das du "Checkpoint" nennst. Hänge das neue Script nun an das Objekt CheckpointFlag an. Wähle dafür CheckpointFlag in der Hierarchy aus, klicke im Inspector auf "Add Component" und suche nach dem Checkpoint Script.

Als nächstes animieren wir die Fahne so wie wir im ersten Kapitel die Spielerbewegung animiert haben. Die Fahne soll solange geschlossen bleiben, bis der Checkpoint erreicht wurde. Danach soll sie im Wind wehen.

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/123544419-77f90d00-d753-11eb-840e-7d145706217f.png" width="400">
</p>
