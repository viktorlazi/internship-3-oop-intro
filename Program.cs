using System;
using System.Collections.Generic;

namespace Internship_3_oop_intro
{
    class Program
    {
        static void Main(string[] args)
        {
            var exampleEvent1 = new Event();
            var exampleEvent2 = new Event();

            var peopleAndEventList = new Dictionary<Event, Person[]>(){
                {exampleEvent1, exampleEvent1.Attendants},
                {exampleEvent2, exampleEvent2.Attendants}
            };

            while(true){
                var userChoice = PrintMenuAndGetUserChoice();

            }

        }

        static int PrintMenuAndGetUserChoice(){
            Console.Clear();
            Console.WriteLine("1. Dodavanje eventa");
            Console.WriteLine("2. Brisanje eventa");
            Console.WriteLine("3. Edit eventa");
            Console.WriteLine("4. Dodavanje osobe na event");
            Console.WriteLine("5. Uklanjanje osobe sa eventa");
            Console.WriteLine("6. Ispis detalja eventa");
            Console.WriteLine("7. Prekid rada");
            
            var userChoice = Console.ReadLine();
            if(int.TryParse(userChoice, out int result) 
                            && result > 0 && result < 8){
                return result;
            }else{
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"{userChoice} nije valjan unos!");
                Console.ForegroundColor = ConsoleColor.White;

                PressEnterToContinue();
                return 0;
            }
        }

        static void PressEnterToContinue(){
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\n");
            Console.WriteLine("--- Enter to contiunue ---");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadLine();
        }
    }
}
