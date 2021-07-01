# Checkpoints und neue Level Elemente

In diesem Kapitel werden wir Checkpoints in unser Spiel einbauen, sodass unser Spiel an bestimmten Stellen neu startet, sobald er runtergefallen ist. Du solltest hier mit einem funktionsfähigen Stand aus dem [letzten Kapitel]("docs/05-cleanup.md") starten. Im Notfall kannst du das Musterprojekt [hier](https://github.com/FrankFlamme/UnityKidsWorkshop/releases/tag/0.5) nutzen.

## Checkpoint in die Szene einfügen

Im letzten Kapitel haben wir eine KillZone unter den Plattformen erstellt und den Spieler deaktiviert, sobald er durch diese gefallen ist. Wir wollen das Spiel nun so umbauen, dass er an einer bestimmten Stelle im Spiel wieder erscheint. Dafür bauen wir uns einen Kontrollpunkt in Form einer Flagge. 

### Neues Objekt erstellen

Öffne den Ordner Textures in dem Project Fenster und klappe dort die Items aus. Ziehe eine der geschlossenen Fahnen (zum Beispiel Items_0) in die Szene und nenne das neue Objekt "CheckpointFlag". Gehe nun in den Ordner Scripts und erstelle ein neues Script (Rechtsklick > Create > C# Script), das du "Checkpoint" nennst. Hänge das neue Script nun an das Objekt CheckpointFlag an. Wähle dafür CheckpointFlag in der Hierarchy aus, klicke im Inspector auf "Add Component" und suche nach dem Checkpoint Script.

### Animation

Als nächstes animieren wir die Fahne so wie wir im ersten Kapitel die Spielerbewegung animiert haben. Die Fahne soll solange geschlossen bleiben, bis der Checkpoint erreicht wurde. Danach soll sie im Wind wehen.

Wähle in der Hierarchy CheckpointFlag aus und klicke im Animation Fenster auf "Create". Falls du das Animation Fenster noch nicht geöffnet hast, findest du es im Menü unter Window > Animation > Animation. Nenne deine neue Animation "FlagClosed" und achte darauf, dass du sie im Ordner Animations speicherst. Füge in der Timeline ein Mal die geschlossene Fahne (zum Beispiel Items_0) bei 0 Sekunden ein.

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/124025345-9f203a80-d9f0-11eb-8517-bccab368e50f.png" width="800">
</p>

Erstelle nun über "Create New Clip..." im Dropdown-Menü des Animation Fensters eine weitere Animation für die wehende Fahne. Nenne die neue Animation "FlagOpen" und achte auch hier darauf, dass du sie in den Animations Ordner ablegst. Füge hier in die Timeline fünf mal in einem Abstand von 10 Sekunden die Sprites der offenen Fahnen abwechselnd ein. Nutzt du die gelde Fahne wäre die Reihenfolge also zum Beipsiel: Items_17, Items_20, Items_17, Items_20, Items_17. Mit dem Play-Button kannst du dir in der Szene ansehen, wie die Animation aussehen würde.

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/124026406-f07cf980-d9f1-11eb-9f05-23b7ec9074b6.png" width="800">
</p>

Öffne im Unity Editor nun das Animator Fenster. Setze FlagClosed als Default State indem du darauf rechtsklickst und "Set as Layer Default State" wählst. Erstelle mit Rechtsklick und "Make Transition" sowohl eine Transition von FlagClosed nach FlagOpen, als auch in die andere Richtung.

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/124027177-dc85c780-d9f2-11eb-8017-1e224edbbb5e.png" width="400">
</p>

Erstelle nun im Animator Fenster über den Plus-Button einen neuen Parameter des Typen bool und nenne ihn "FlagOpen". Bearbeite nun die Transitions indem du auf die Verbindungen zwischen den States klickst. Öffne bei beiden Transitions im Inspector die Settings, entferne die Haken bei "Has Exit Time" und "Fixed Duration" und setze "Transition Duration" auf 0. Füge bei beiden Transitionen eine Condition hinzu. Bei der Transition von "FlagClosed" zu "FlagOpen" ist die Condition FlagOpen = true und bei der Transition "FlagOpen" zu "FlagClosed" ist die Condition FlagOpen = false. 

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/124029025-fcb68600-d9f4-11eb-895e-699df42f572f.png" width="250"> <img src="https://user-images.githubusercontent.com/75975986/124029028-fde7b300-d9f4-11eb-964d-f05b59af716a.png" width="250">
</p>

Als nächstes müssen wir einen Collider mit Trigger zum Objekt hinzufügen. In diesem Fall verwenden wir keinen BoxCollider2D, da dieser nicht zum Objekt passt. Stattdessen verwenden wir einen CircleCollider2D. Wähle das Objekt CheckpointFlag in der Hierarchy aus und füge im Inspector eine Komponente über "Add Component" hinzu. Wähle den CircleCollider2 aus und setze einen Haken bei "Is Trigger". Positioniere den Collider zentral um die Flagge indem du auf "Edit Collider" klickst und den Collider in der Szene anpasst.

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/124030257-4fdd0880-d9f6-11eb-90d4-35db59ba53d0.png" width="400">
</p>

Nun ergänzen wir unser Script. Öffne dafür das Checkpoint Skript in Visual Studio Code und erstelle darin fünf neue Variablen. Wir benötigen zwei Variablen vom Typ Sprite, um die beiden Bilder für die geöffnete und geschlossene Fahne festzulegen. Dann brauchen wir noch eine Variable vom Typ bool, um eine Verknüpfung mit dem Parameter unserer Animation herzustellen. Die anderen beiden Variablen sind aus unseren letzten Skripten bekannt. 

```csharp
public Sprite flagOpen;
public Sprite flagClosed;
public bool checkpointActive = false;

private SpriteRenderer _spriteRenderer;
private Animator _animator;
```

In der `Start()`-Methode initialisieren wir den SpriteRenderer für die Bilder der Flagge und den Animator.

```csharp
_spriteRenderer = GetComponent<SpriteRenderer>();
_animator = GetComponent<Animator>();
```

In der `Update()`-Methode setzen wir den Parameter „FlagOpen“ aus dem Animator auf den Wert des „checkpointActive“ Bools.

```csharp
_animator.SetBool("FlagOpen", checkpointActive);
```

Außerdem erstellen wir eine neue Methode `OnTriggerEnter2D` für das Öffnen der Fahne, wenn der Player auf die Fahne trifft.

```csharp
void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _spriteRenderer.sprite = flagOpen;
            checkpointActive = true;
        }
    }
```

Damit das Ganze funktioniert, muss der Player noch den richtigen Tag zugewiesen bekommen. Wechsle dafür in den Unity Editor und wähle den Player in der Hierarchy aus. Weise ihm im Inspector den Tag "Player" zu. Wähle nun in der Hierarchy CheckpointFlag aus und erstelle im Inspector unter Tag > Add Tag... und nenne den neuen Tag "Checkpoint". Weise CheckpointFlag den Tag "Checkpoint" zu. 

<p align="center">
<img src="https://user-images.githubusercontent.com/75975986/124184498-f0483100-dab9-11eb-9efd-ed5da91d2592.png" width="200">
</p>

Wenn du das Spiel nun startest, fängt die Fahne an zu wehen, sobald der Player hindurch läuft.

### Respawning

Im letzen Kapitel haben wir den Player einfach deaktiviert, wenn er heruntergefallen ist. Nun wollen wir ihn nach dem Herunterfallen am Checkopoint wiederherstellen, wenn er diesen bereits aktiviert hat.

Dafür bearbeiten wir das Player Script. Öffne das Script in Visual Studio Code und erstelle darin eine neue Variable, in der die Position der Fahne gespeichert wird.

```csharp
public Vector3 respawnPos;
```

In der `OnTriggerEnter2D()`-Methode fügen wir am Ende der Methode folgenden Code ein, sodass die Position an der der Spieler in die Fahne läuft in unserer neuen Variable gespeichert wird. 

```csharp
if (collision.CompareTag("Checkpoint"))
   {
       respawnPos = collision.transform.position;
   }
```

Die erste if-Condition in der `OnTriggerEnter2D()`-Methode passen wir auch an, sodass der Player nicht mehr deaktiviert wird, sondern seine Position auf den Wert unserer Variable `respawnPos` gesetzt wird. Die if-Condition sollte nun folgendermaßen aussehen: 

```csharp
if (collision.CompareTag("KillZone"))
   {
       transform.position = respawnPos;
   }
```

Erweitere die `Start()`-Methode um folgenden Code, sodass der Spieler an der Ausgangsposition wieder erscheint, falls er den Checkpoint noch nciht aktiviert hat.

```csharp
respawnPos = transform.position;
```

Der Spieler wird nach dem Herunterfallen nun am Checkpoint wiederhergestellt, falls er ihn schon aktiviert hat und sonst am Anfang des Spieles. Du kannst aus dem Checkpoint einen Prefab machen, sodass du das Element mehrmals wiederverwenden kannst. Ziehe dasfür einfach die CheckpointFlag aus der Hierarchy in den Ordner Prefabs.

## Level Manager

Damit wir uns später schnell und einfach eigene Level bauen können, implementieren wir nun einen Level Manager.

