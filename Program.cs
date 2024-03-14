using System;
using System.Collections.Generic;
using System.IO;

namespace Szinhaz
{
    class SeatReservationSystem
    {
        private const int Rows = 16;
        private const int SeatsPerRow = 15;
        private const double ReservedPercentage = 0.1;
        private const string ReservationDataFile = "szinhaz_foglalasok.txt"; // A fájl neve egyértelműbb

        private bool[,] seats;

        public SeatReservationSystem()
        {
            seats = new bool[Rows, SeatsPerRow];
            InitializeSeats();
        }

        // Színház helyek inicializálása, bizonyos százalékban foglaltak
        private void InitializeSeats()
        {
            Random random = new Random();
            int reservedSeats = (int)(Rows * SeatsPerRow * ReservedPercentage);

            for (int i = 0; i < reservedSeats; i++)
            {
                int row = random.Next(0, Rows);
                int seat = random.Next(0, SeatsPerRow);
                if (!seats[row, seat])
                {
                    seats[row, seat] = true;
                }
                else
                {
                    i--; // Ha a hely már foglalt, próbáljunk újat választani
                }
            }
        }

        // Szabad és foglalt helyek megjelenítése
        private void PrintSeatMap()
        {
            Console.WriteLine("Szabad (O) / Foglalt (X) székek:");
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < SeatsPerRow; j++)
                {
                    if (seats[i, j])
                    {
                        Console.Write("X ");
                    }
                    else
                    {
                        Console.Write("O ");
                    }
                }
                Console.WriteLine();
            }
        }

        // Foglalások mentése fájlba
        private void SaveReservationData(Dictionary<string, List<Tuple<int, int>>> reservations)
        {
            using (StreamWriter writer = new StreamWriter(ReservationDataFile))
            {
                foreach (var reservation in reservations)
                {
                    writer.WriteLine(reservation.Key);
                    foreach (var seat in reservation.Value)
                    {
                        // Menti a foglalást a fájlba a sorszámhoz képest
                        writer.WriteLine($"Sor: {seat.Item1 + 1}, Szék: {seat.Item2 + 1}");
                    }
                    writer.WriteLine();
                }
            }
        }


        // Foglalások betöltése fájlból
        private Dictionary<string, List<Tuple<int, int>>> LoadReservationData()
        {
            Dictionary<string, List<Tuple<int, int>>> reservations = new Dictionary<string, List<Tuple<int, int>>>();

            if (File.Exists(ReservationDataFile))
            {
                using (StreamReader reader = new StreamReader(ReservationDataFile))
                {
                    string line;
                    string currentName = "";
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line != "")
                        {
                            if (!reservations.ContainsKey(line))
                            {
                                currentName = line;
                                reservations[currentName] = new List<Tuple<int, int>>();
                            }
                            else
                            {
                                string[] parts = line.Split(',');
                                int row = int.Parse(parts[0]);
                                int seat = int.Parse(parts[1]);
                                reservations[currentName].Add(new Tuple<int, int>(row, seat));
                            }
                        }
                    }
                }
            }

            return reservations;
        }

        // Főmenü megjelenítése és kezelése
        public void Run()
        {
            Dictionary<string, List<Tuple<int, int>>> reservations = LoadReservationData();
            int selectedOption = 1; // Alapértelmezett kiválasztott opció

            while (true)
            {
                Console.Clear(); // Töröljük a képernyőt

                // Főmenü megjelenítése
                Console.WriteLine("Válassz egy opciót nyilakkal:");
                for (int i = 1; i <= 4; i++)
                {
                    if (i == selectedOption)
                    {
                        Console.WriteLine($"-> {i}. ");
                    }
                    else
                    {
                        Console.WriteLine($"    {i}. ");
                    }
                }
                Console.WriteLine("1. Foglalás");
                Console.WriteLine("2. Foglalás törlése");
                Console.WriteLine("3. Foglalás módosítása");
                Console.WriteLine("4. Kilépés");

                // Billentyűlenyomás figyelése
                ConsoleKeyInfo key = Console.ReadKey(true);

                // Billentyűnek megfelelő művelet végrehajtása
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (selectedOption > 1)
                            selectedOption--; // Felfelé nyíl esetén az előző opció kiválasztása
                        break;
                    case ConsoleKey.DownArrow:
                        if (selectedOption < 4)
                            selectedOption++; // Lefelé nyíl esetén a következő opció kiválasztása
                        break;
                    case ConsoleKey.Enter:
                        // Enter lenyomásakor a kiválasztott opció végrehajtása
                        switch (selectedOption)
                        {
                            case 1:
                                MakeReservation(reservations);
                                break;
                            case 2:
                                CancelReservation(reservations);
                                break;
                            case 3:
                                ModifyReservation(reservations);
                                break;
                            case 4:
                                SaveReservationData(reservations);
                                return;
                        }
                        break;
                }
            }
        }

        // Foglalás készítése
        private void MakeReservation(Dictionary<string, List<Tuple<int, int>>> reservations)
        {
            // Felhasználótól név és foglalási módszer kérdezése
            Console.Clear();
            Console.WriteLine("Add meg a neved:");
            string name = Console.ReadLine();

            Console.WriteLine("Válassz egy opciót:");
            Console.WriteLine("1. Kézi székválasztás");
            Console.WriteLine("2. Véletlenszerű székválasztás");

            int option;
            while (!int.TryParse(Console.ReadLine(), out option) || (option != 1 && option != 2))
            {
                Console.WriteLine("Érvénytelen választás! Kérlek, válassz 1 vagy 2 közötti számot.");
            }

            List<Tuple<int, int>> selectedSeats = new List<Tuple<int, int>>();

            // Székválasztás a felhasználó választása alapján
            if (option == 1)
            {
                Console.WriteLine("Szabad helyek:");
                PrintSeatMap();

                Console.WriteLine("Add meg a foglalni kívánt sor és szék számát (pl.: 1 3), vagy írd be 'done' a foglalás befejezéséhez:");
                string input;
                while ((input = Console.ReadLine()) != "done")
                {
                    string[] parts = input.Split(' ');
                    int row = int.Parse(parts[0]) - 1;
                    int seat = int.Parse(parts[1]) - 1;

                    if (row < 0 || row >= Rows || seat < 0 || seat >= SeatsPerRow)
                    {
                        Console.WriteLine("Érvénytelen helyszám!");
                        continue;
                    }

                    if (seats[row, seat])
                    {
                        Console.WriteLine("Ez a hely már foglalt!");
                        continue;
                    }

                    selectedSeats.Add(new Tuple<int, int>(row, seat));
                }
            }
            else if (option == 2)
            {
                // Véletlenszerű székválasztás
                Random random = new Random();
                while (selectedSeats.Count < 1)
                {
                    int row = random.Next(0, Rows);
                    int seat = random.Next(0, SeatsPerRow);
                    if (!seats[row, seat])
                    {
                        selectedSeats.Add(new Tuple<int, int>(row, seat));
                    }
                }
            }

            // Ha nem választottak széket, visszatérés a főmenübe
            if (selectedSeats.Count == 0)
            {
                Console.WriteLine("Nem választottál széket a foglaláshoz!");
                Console.WriteLine("Nyomj meg egy gombot a főmenübe való visszatéréshez.");
                Console.ReadKey(true);
                return;
            }

            // Foglalások hozzáadása a rendszerhez
            if (!reservations.ContainsKey(name))
            {
                reservations[name] = new List<Tuple<int, int>>();
            }

            foreach (var seat in selectedSeats)
            {
                reservations[name].Add(seat);
                seats[seat.Item1, seat.Item2] = true;
            }

            Console.WriteLine("Sikeres foglalás!");
            Console.WriteLine("Nyomj meg egy gombot a főmenübe való visszatéréshez.");
            Console.ReadKey(true);
        }

        // Foglalás törlése
        private void CancelReservation(Dictionary<string, List<Tuple<int, int>>> reservations)
        {
            Console.Clear();
            Console.WriteLine("Add meg a neved a foglalás törléséhez:");
            string name = Console.ReadLine();

            if (!reservations.ContainsKey(name))
            {
                Console.WriteLine("Nincs foglalás ezen a néven!");
                Console.WriteLine("Nyomj meg egy gombot a főmenübe való visszatéréshez.");
                Console.ReadKey(true);
                return;
            }

            // Foglalások törlése
            List<Tuple<int, int>> reservedSeats = reservations[name];
            foreach (var seat in reservedSeats)
            {
                seats[seat.Item1, seat.Item2] = false;
            }

            reservations.Remove(name);

            Console.WriteLine("Foglalás törölve!");
            Console.WriteLine("Nyomj meg egy gombot a főmenübe való visszatéréshez.");
            Console.ReadKey(true);
        }

        // Foglalás módosítása
        private void ModifyReservation(Dictionary<string, List<Tuple<int, int>>> reservations)
        {
            Console.Clear();
            Console.WriteLine("Add meg a neved a foglalás módosításához:");
            string name = Console.ReadLine();

            if (!reservations.ContainsKey(name))
            {
                Console.WriteLine("Nincs foglalás ezen a néven!");
                Console.WriteLine("Nyomj meg egy gombot a főmenübe való visszatéréshez.");
                Console.ReadKey(true);
                return;
            }

            // Korábbi foglalások megjelenítése
            Console.WriteLine("Korábbi foglalásaid:");
            int index = 1;
            foreach (var seat in reservations[name])
            {
                Console.WriteLine($"{index}. Sor: {seat.Item1 + 1}, Szék: {seat.Item2 + 1}");
                index++;
            }

            Console.WriteLine("Add meg a módosítani kívánt foglalás sorszámát:");
            int reservationIndex;
            while (!int.TryParse(Console.ReadLine(), out reservationIndex) || reservationIndex < 1 || reservationIndex > reservations[name].Count)
            {
                Console.WriteLine($"Érvénytelen sorszám! Kérlek, válassz 1 és {reservations[name].Count} közötti számot.");
            }

            Console.WriteLine("Add meg az új sor és szék számát (pl.: 1 3):");
            string[] input = Console.ReadLine().Split(' ');
            int newRow = int.Parse(input[0]) - 1;
            int newSeat = int.Parse(input[1]) - 1;

            if (newRow < 0 || newRow >= Rows || newSeat < 0 || newSeat >= SeatsPerRow)
            {
                Console.WriteLine("Érvénytelen helyszám!");
                Console.WriteLine("Nyomj meg egy gombot a főmenübe való visszatéréshez.");
                Console.ReadKey(true);
                return;
            }

            if (seats[newRow, newSeat])
            {
                Console.WriteLine("Ez a hely már foglalt!");
                Console.WriteLine("Nyomj meg egy gombot a főmenübe való visszatéréshez.");
                Console.ReadKey(true);
                return;
            }

            // Foglalás módosítása
            Tuple<int, int> oldSeat = reservations[name][reservationIndex - 1];
            seats[oldSeat.Item1, oldSeat.Item2] = false;
            reservations[name][reservationIndex - 1] = new Tuple<int, int>(newRow, newSeat);
            seats[newRow, newSeat] = true;

            Console.WriteLine("Foglalás sikeresen módosítva!");
            Console.WriteLine("Nyomj meg egy gombot a főmenübe való visszatéréshez.");
            Console.ReadKey(true);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            SeatReservationSystem reservationSystem = new SeatReservationSystem();
            reservationSystem.Run();
        }
    }
}
