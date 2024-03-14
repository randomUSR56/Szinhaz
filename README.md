# Részletes magyarázat: Székfoglalási Rendszer

## Bevezetés:
A nyújtott C# program egy robosztus székfoglalási rendszert képvisel egy színházi környezetben. A felhasználóknak lehetőséget biztosít a székek foglalására, módosítására és törlésére egy felhasználóbarát konzolos felületen keresztül. Az objektumorientált elveket és fájl I/O műveleteket felhasználva ez a rendszer összefoglalja a székek kezelésének bonyolultságát, miközben biztosítja az adatok állandóságát a probléma megoldásának folyamán.

## Osztály struktúra:

### SeatReservationSystem:
- **Célja**: A foglalási rendszer gerincét képezi, kezelve a székek kezelését, a foglalási műveleteket és a fájl I/O-t.
- **Mezők**: 
  - `Rows`: Meghatározza a sorok összes számát a színházban, megkönnyítve a székek elrendezését és kezelését.
  - `SeatsPerRow`: Megadja a soronkénti székek számát, biztosítva a szék kiválasztásának és kiosztásának finomítását.
  - `ReservedPercentage`: Az kezdeti foglalási arányt határozza meg, biztosítva a foglalt és elérhető székek kiegyensúlyozott eloszlását.
  - `ReservationDataFile`: Tárolja a foglalási adatokat a fájl nevét a folytonosság megőrzése érdekében.
  - `seats`: Egy 2D-s logikai tömb, amely jelzi az egyes székek elérhetőségét a színházban, hatékony székek kezelését biztosítva.
- **Metódusok**:
  - `SeatReservationSystem()`: Az osztály példányosításakor inicializálja a foglalási rendszert, az ülések és a meglévő foglalási adatok betöltésének koordinálásával.
  - `InitializeSeats()`: A székek inicializálásához szükséges logikát tartalmazza, biztosítva az optimális foglalási és elérhetőségi székek eloszlását a meghatározott foglalási arány alapján.
  - `PrintSeatMap()`: Megjeleníti a színház ülési térképét, vizuális áttekintést nyújtva a szabad és foglalt székekről a felhasználóknak.
  - `SaveReservationData(reservations: Dictionary<string, List<Tuple<int, int>>>`: Lehetővé teszi a foglalási adatok állandó tárolását a megadott fájlban, az adatintegritás és a folytonosság biztosítása érdekében a programmunka folyamán.
  - `LoadReservationData()`: Kezeli a foglalási adatok lekérését a meghatározott fájlból, lehetővé téve a korábbi foglalási nyilvántartások helyreállítását a zökkenőmentes felhasználói interakció érdekében.
  - `Run()`: A foglalási rendszer fő vezérlő hurokja, a felhasználóknak átfogó menüt kínál a foglalási műveletek végrehajtásához és a rendszer funkcióinak áttekintéséhez.
  - `MakeReservation(reservations: Dictionary<string, List<Tuple<int, int>>>`: Vezeti a felhasználókat a foglalás folyamatában, lehetőséget biztosítva mindkét manuális vagy véletlenszerű székválasztásra, és biztosítja az új foglalások zökkenőmentes hozzáadását a rendszerhez.
  - `CancelReservation(reservations: Dictionary<string, List<Tuple<int, int>>>`: Lehetővé teszi a felhasználók számára a meglévő foglalások törlését a nevük megadásával, lehetővé téve a foglalási rekordok hatékony eltávolítását és a foglalt székek felszabadítását a jövőbeli foglalásokhoz.
  - `ModifyReservation(reservations: Dictionary<string, List<Tuple<int, int>>>`: Lehetővé teszi a felhasználók számára meglévő foglalásaik módosítását, lehetővé téve a szék kiválasztások frissítését és az igények változásaiban történő alkalmazkodást.

### Program:
- **Célja**: A program belépési pontjaként szolgál, koordinálva a székfoglalási rendszer példányosítását és a program végrehajtási folyamatának elindítását.
- **Metódusok**:
  - `Main(args: string[])`: A program belépési pontját képviseli, inicializálja a székfoglalási rendszert és meghívja futási hurokját a felhasználói interakció és a foglaláskezelés megkönnyítése érdekében.

## Belső működés:

### Inicializálás:
A program indításakor a székfoglalási rendszer példányosítódik, ami elindítja a székek inicializálását és a meglévő foglalási adatok lekérését a megfelelő fájlból. Ez a inicializációs fázis biztosítja, hogy a rendszer készen álljon a felhasználói interakcióra, az aktuális székfoglalatosság és foglalási adatok pontosan tükrözésével.

### Főmenüvel való interakció:
A program fő interakciós hurokja a főmenü bemutatásában áll, amely felhasználóknak kínál választékot a foglalási műveletek végrehajtásához, beleértve a foglalások, a foglalások törlésének és módosításának lehetőségét, valamint a program kilépését. A felhasználók az intuitív billentyűbevitellel navigálhatnak a menüben, lehetővé téve a rendszer funkcióinak könnyű elérését.

### Foglalási műveletek:
- **Foglalás készítése**: A felhasználókat a foglalás folyamatában vezeti, lehetőséget biztosítva mindkét manuális szék kiválasztására és véletlenszerű szék kiosztására. A rendszer biztosítja a kiválasztott székek elérhetőségét, és lehetővé teszi az új foglalások zökkenőmentes hozzáadását a rendszerhez, frissítve a székek foglaltsági állapotát.
- **Foglalás törlése**: A felhasználóknak lehetőségük van meglévő foglalásaik törlésére a nevük megadásával, lehetővé téve a foglalási rekordok hatékony eltávolítását és a foglalt székek felszabadítását a jövőbeli foglalásokhoz.
- **Foglalás módosítása**: A rendszer lehetővé teszi a felhasználók számára meglévő foglalásaik módosítását, lehetővé téve a szék kiválasztások frissítését és az igények változásaiban történő alkalmazkodást.

### Adatállandóság:
A foglalási adatok állandóan tárolódnak a megadott fájlban (`szinhaz_foglalasok.txt`), biztosítva a foglalási nyilvántartások zökkenőmentes megőrzését a programfutások során. A rendszer fájl I/O műveleteket használ a foglalási adatok mentéséhez és betöltéséhez, lehetővé téve a korábbi foglalási rekordok helyreállítását és a zökkenőmentes felhasználói élményt.

## Összegzés:
A nyújtott székfoglalási rendszer a robust funkcionalitás, intuitív felhasználói interakció és adatállandóság ötvözését mutatja be, egy átfogó megoldást nyújtva a színházi foglalások kezelésére. Az osztályok és metódusok jól meghatározottak, és biztosítják a felhasználók számára a zökkenőmentes és hatékony foglalási élményt, miközben fenntartják az adatok integritását és folytonosságát a programfutások során.