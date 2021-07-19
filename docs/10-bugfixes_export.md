# Bugfixes, Export und visuelle Anpassungen

In diesem Kapitel fügen wir dem Spiel Effekte für das Respawning hinzu, nehmen einige Fehlerkorrekturen vor und exportieren das Spiel so, dass du Unity nicht mehr zum Ausführen des Spiels benötigst. Du solltest hier mit einem funktionsfähigen Stand aus dem [letzten Kapitel](/docs/09-enemies.md) starten. Im Notfall kannst du das Musterprojekt [hier](https://github.com/FrankFlamme/UnityKidsWorkshop/releases/tag/0.9) nutzen.

## Hintergrund einfügen

Zuerst möchten wir, dass unser Hintergrund etwas mehr nach Spiel aussieht. Dafür nutzen wir den [Hintergrund](https://github.com/FrankFlamme/UnityKidsWorkshop/releases/download/1.0/backgroundEmpty.png), den du dir aus den Assets des Kapitels laden kannst.

Lade dir den Hintergrund auf deinen Desktop und wechsele im Unity Editor in den Ordner "Textures". Klicke mit der rechten Maustaste in den Ordner und wähle "Import New Asset". Wähle nun die Datei von deinem Desktop aus und klicke auf "Import".

Die Datei befindet sich nun in deinem "Textures" Ordner.
Klicke nun das Bild im Ordner an und nimm die Einstellungen im Inspector wie im Bild dargestellt vor:

<p align="center">
<img src="https://user-images.githubusercontent.com/13068729/126160733-42be8760-13bd-475d-9356-efdb4c61ef79.png">
</p>

Füge nun das Bild in die Szene ein, indem du es vom "Texture"-Ordner in die Szene ziehst. Nenne das Objekt "BackgroundPicture" und setze das Flag "Order in Layer" auf -1, damit das Bild wirklich im Hintergrund ist und keine anderen Items verdeckt.

Damit sich das Hintergrundbild mit der Kamera mitbewegt, legst du unterhalb der "Main Camera" ein neues, leeres Objekt mit dem Namen "CamBackground" an und ziehst "BackgroundPicture" als Child-Objekt darunter

<p align="center">
<img src="https://user-images.githubusercontent.com/13068729/126161431-655eee95-eeb5-41eb-b8fc-0a87b54d364e.png">
</p>