## Készítők:
- **Fejlesztők**: [Dluhi Dániel](https://github.com/randomUSR56), [Luczi Alex](https://github.com/alexluczi)
- **Projektvezető**: Dluhi Dániel
- **Dizájn**: Luczi Alex
- **Tesztelő**: Luczi Alex, Dluhi Dániel

# Fejlesztői Dokumentáció
## Tartalomjegyzék
1. [Bevezetés](#bevezetés)
2. [Osztály struktúra](#osztály-struktúra)
  - [SeatReservationSystem](#seatreservationsystem)
  - [Program](#program)
3. [Belső működés](#belső-működés)
  - [Inicializálás](#inicializálás)
  - [Főmenüvel való interakció](#főmenüvel-való-interakció)
  - [Foglalási műveletek](#foglalási-műveletek)
  - [Adatállandóság](#adatállandóság)
4. [Összegzés](#összegzés)
5. [Felhasználói Dokumentáció](#felhasználói-dokumentáció)
  - [Bevezetés](#bevezetés-1)
  - [Hogyan kezdjünk](#hogyan-kezdjünk)
  - [Foglalás Készítése](#foglalás-készítése)
  - [Foglalás Törlése](#foglalás-törlése)
  - [Foglalás Módosítása](#foglalás-módosítása)

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

# Felhasználói Dokumentáció
# Színházi Székfoglalási Rendszer Felhasználói Útmutató

## Bevezetés

Köszöntünk a Színházi Székfoglalási Rendszer felhasználói útmutatójában! Ez a rendszer lehetővé teszi számodra, hogy kényelmesen foglalj jegyeket egy színházban. Legyen szó mozijegyekről, színházi előadásokról vagy bármilyen más eseményről, ez a rendszer segíteni fog neked hatékonyan kezelni a foglalásokat.

## Hogyan kezdjünk

A Színházi Székfoglalási Rendszer használatához kövesd ezeket a lépéseket:

1. **Fordítsd a Kódot**: Ha hozzáférésed van a C# kódhoz, fordítsd le azt egy C# fordítóval, például a Visual Studio, a JetBrains Rider vagy a .NET Core CLI segítségével.

2. **Indítsd el a Programot**: A kód lefordítása után futtasd a fordító által generált végrehajtható fájlt. Ez elindítja a foglalási rendszert.

3. **Kövesd a Képernyőn Megjelenő Utasításokat**: Miután a program fut, kövesd a képernyőn megjelenő utasításokat a foglalási rendszerrel való interakcióhoz.

## Foglalás Készítése

A foglalás készítéséhez kövesd ezeket a lépéseket:

1. **Írd Be A Neved**: Amikor arra kérik, add meg a nevedet. Ez azonosítani fogja a foglalásodat.

2. **Válassz Foglalási Módot**: Válaszd ki az alábbi foglalási módszerek egyikét:
   - **Kézi Székválasztás**: Válaszd ki kézzel azokat a székeket, amelyeket foglalni szeretnél.
   - **Véletlenszerű Székválasztás**: Hagyd, hogy a rendszer véletlenszerűen kiválassza a szabad székeket neked.

3. **Válassz Székeket**: Ha a kézi székválasztást választottad, kövesd az utasításokat a kívánt székek kiválasztásához. Ha a véletlenszerű székválasztást választottad, a rendszer automatikusan kiosztja a szabad székeket neked.

4. **Megerősítés**: Miután kiválasztottad a székeket, a rendszer megerősíti a foglalást, és megjeleníti a sikeres üzenetet.

## Foglalás Törlése

A foglalás törléséhez kövesd ezeket a lépéseket:

1. **Írd Be A Neved**: Amikor arra kérik, add meg a nevedet. Ez azonosítani fogja a foglalásodat.

2. **Megerősítés**: A rendszer megjeleníti a meglévő foglalás(ok)at. Válaszd ki azt a foglalást, amelyet törölni szeretnél.

3. **Törlés**: Erősítsd meg a törlést. A rendszer felszabadítja a foglalt székeket, így azok újra elérhetővé válnak mások számára.

## Foglalás Módosítása

A foglalás módosításához kövesd ezeket a lépéseket:

1. **Írd Be A Neved**: Amikor arra kérik, add meg a nevedet. Ez azonosítani fogja a foglalásodat.

2. **Válassz Foglalást**: A rendszer megjeleníti a meglévő foglalás(ok)at. Válaszd ki azt a foglalást, amelyet módosítani szeretnél.

3. **Válassz Új Székeket**: Kövesd az utasításokat az új székek kiválasztásához a foglalásodhoz.

4. **Megerősítés**: Miután kiválasztottad az új székeket, a rendszer megerősíti a módosítást, és frissíti a foglalásodat.

## Elérhető Székek Megtekintése

Bármikor megtekintheted a színházban elérhető székeket a főmenüből kiválasztható megfelelő opció segítségével.

## Foglalások Mentése és Betöltése

A rendszer automatikusan menti a foglalásokat egy fájlba (`szinhaz_foglalasok.txt`). Ez a fájl minden alkalommal betöltődik, amikor a program elindul, lehetővé téve az előző foglalások folytatását.

## Kilépés a Programból

A programból való kilépéshez válaszd ki a megfelelő opciót a főmenüből. A foglalásaid automatikusan mentésre kerülnek.

## Visszajelzés és Támogatás

Ha bármilyen kérdésed, visszajelzésed vagy problémád merül fel a Színházi Székfoglalási Rendszer használata során, kérjük, lépj kapcsolatba a támogatási csapatunkkal a [dluhid@kkszki.hu](mailto:support@example.com) e-mail címen.

Kellemes élményt kívánunk a Színházi Székfoglalási Rendszerrel!

### Pillanatképek a program működéséről

1. ![Image 1](https://i.postimg.cc/nrSYHzZW/temp-Imageaw-Wg-LT.avif)
2. ![Image 2](https://i.postimg.cc/tJ4NQP0P/temp-Image-BSZm-VB.avif)
3. ![Image 3](https://i.postimg.cc/nLK4XTpB/temp-Image-F5z-UUX.avif)
4. ![Image 4](https://i.postimg.cc/LsFk4GKm/temp-Image-Hcoc8-W.avif)
5. ![Image 5](https://i.postimg.cc/NGdx8NTx/temp-Imagem-UIf-F5.avif)
6. ![Image 6](https://i.postimg.cc/BQtBmxBf/temp-Imagev3gmut.avif)
7. ![Image 7](https://i.postimg.cc/SsTz9v52/temp-Imageyl1g52.avif)
