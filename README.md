# M183 - Insecure App

## Datenbank aufsetzen
- Projekt im Visual Studio öffnen
- Paket-Manager-Konsole öffnen

![Screenshot: Paket-Manager-Konsole öffnen](/img/paket-manager-konsole.png?raw=true "Screenshot: Paket-Manager-Konsole öffnen")  

- Befehl `Update-Database` ausführen

## Datenbankzugriff im Visual Studio
- Server-Explorer öffnen

![Screenshot: Server-Explorer öffnen](/img/server-explorer.png?raw=true "Screenshot: Server-Explorer öffnen")

- "Mit Datenbank verbinden" klicken (1)
- Servername: (localdb)\mssqllocaldb (2)
- In der Box "Mit Datenbank verbinden": Datenbanknamen auswählen oder ausgeben auf den Pfeil klicken (3)
- Datenbank "M183InsecureApp" auswählen und verbinden

![Screenshot: Datenbankvrbindung hinzufügen](/img/datenbank-verbindung-hinzufuegen.png?raw=true "Screenshot: Datenbankvrbindung hinzufügen")

## Benutzer in der Applikation:
1. Benutzername: **administrator** Passwort: **8dminSec**
2. Benutzername: **user** Passwort: **uS3rP8ss**