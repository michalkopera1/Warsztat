using System;
using Warsztat; 

namespace WarsztatAplikacja
{
    class Aplikacja
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Podaj czas w formacie hh:mm:ss:");
            string timeInput = Console.ReadLine();
            Time time;
            try
            {
                time = new Time(timeInput);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            Console.WriteLine("Podaj okres czasu w sekundach, który chcesz dodać:");
            long periodSeconds = long.Parse(Console.ReadLine());
            TimePeriod period = new TimePeriod(periodSeconds);

            Time newTime = time + period;
            Console.WriteLine($"Nowy czas po dodaniu okresu: {newTime}");

            Console.WriteLine("Podaj drugi czas do porównania (hh:mm:ss):");
            string secondTimeInput = Console.ReadLine();
            Time secondTime;
            try
            {
                secondTime = new Time(secondTimeInput);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            int comparisonResult = time.CompareTo(secondTime);
            if (comparisonResult < 0)
                Console.WriteLine("Pierwszy czas jest wcześniejszy.");
            else if (comparisonResult > 0)
                Console.WriteLine("Pierwszy czas jest późniejszy.");
            else
                Console.WriteLine("Czasy są równe.");
        }
    }
}
