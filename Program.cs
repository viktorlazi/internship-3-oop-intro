using System;
using System.Collections.Generic;

namespace Internship_3_oop_intro
{

    public enum EventTypeEnum{
            Coffee,
            Lecture,
            Concert,
            StudySession
    }
    class Program
    {
        
        static void Main(string[] args)
        {

            var exampleEvent1 = new Event("Kafa ujutro", EventTypeEnum.Coffee, new DateTime(2020, 12, 5, 9, 30, 0), 
                                                                               new DateTime(2020, 12, 5, 10, 30, 0));

            var exampleEvent2 = new Event("Ucenje", EventTypeEnum.StudySession, new DateTime(2020, 12, 5, 1, 30, 0),
                                                                                new DateTime(2020, 12, 5, 2, 30, 0));

            var eventList = new Dictionary<Event, List<Person>>(){
                {exampleEvent1, new List<Person>(){}},
                {exampleEvent2, new List<Person>(){}}
            };

            while(true){
                var userChoice = PrintMainMenuAndGetUserChoice();
                MainMenuHandleByChoice(userChoice, eventList);
            }

        }

        static int PrintMainMenuAndGetUserChoice(){
            Console.Clear();
            Console.WriteLine("Glavni menu");
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
        static int PrintSubMenuEventDetailsAndGetUserInput(){
            Console.Clear();
            Console.WriteLine("Detalji eventa");
            Console.WriteLine("1. Ispis detalja eventa u formatu: name – event type – start time – end time – trajanje – ispis broja ljudi na eventu");
            Console.WriteLine("2. Ispis svih osoba na eventu u formatu: [Redni broj u listi]. name – last name – broj mobitela");
            Console.WriteLine("3. Ispis svih detalja.");
            Console.WriteLine("4. Izlaz");
            var userChoice = Console.ReadLine();
            if(int.TryParse(userChoice, out int result) 
                            && result > 0 && result < 5){
                return result;
            }else{
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"{userChoice} nije valjan unos!");
                Console.ForegroundColor = ConsoleColor.White;

                PressEnterToContinue();
                return result;
            }
        }

        static void PressEnterToContinue(){
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\n");
            Console.WriteLine("--- Enter to contiunue ---");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadLine();
        }

        static void MainMenuHandleByChoice(int userChoice, Dictionary<Event, List <Person>> eventList){
            switch(userChoice){
                case 1:
                    AddingEvent(eventList);
                    break;
                case 2:
                    DeleteEvent(eventList);
                    break;
                case 3:
                    EditEvent(eventList);
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    while(PrintSubMenuEventDetailsAndGetUserInput()!= 4);
                    break;
                case 7:
                    Environment.Exit(0);    
                    break;           
            }
        }

        static bool UserConfirmation(string message = ""){ 

            Console.ForegroundColor = ConsoleColor.Yellow;

            if(message != ""){System.Console.WriteLine(message);} 

            while(true){
                System.Console.WriteLine("Jeste li sigurni? d/n: ");
                Console.ForegroundColor = ConsoleColor.White;
                var userInput = Console.ReadLine();

                switch(userInput){
                    case "d": return true;
                    case "da": return true;
                    case "ne": return false;
                    case "n": return false;
                    default:
                        System.Console.WriteLine("Pogresan unos.\n" +
                                        "Dozvoljeni unosi su:\n" +
                                        "da/ne/d/n \n");
                        break;
                }
            } 
        }

        static int ReadLineEventTypeEnum(){
            var userInput = Console.ReadLine();

            switch(userInput){
                case "Coffee": return 0;
                case "Lecture": return 1;
                case "Concert": return 2;
                case "StudySession": return 3;
                default: return 404;
            }
        }

        static bool ReadLineDateTime(out DateTime parsed){
            var userDateTime = Console.ReadLine();

            if(DateTime.TryParse(userDateTime, out DateTime parsedInput)){
                parsed = parsedInput;
                return true;
            }else{
                parsed = parsedInput;
                return false;
            }
            
        }

        static bool CheckDateTimeValidity(DateTime startTime, DateTime endTime, Dictionary<Event, List<Person>> eventList){
            if(startTime > endTime){
                System.Console.WriteLine("Event nemoze zavrsiti prije nego pocne");
                return false;
            }
            // ------1-------2---2---1-----
            var overlap = false;
            foreach(var _event in eventList.Keys){
                if(!(startTime > _event.EndTime || endTime < _event.StartTime)){
                    System.Console.WriteLine($"Event se preklapa sa \"{_event.Name}\"");
                    overlap = true;
                }
            }
            if(overlap){return false;}

            return true;
            
        }
        
        ////////////////////////////
        //       1 - 6 cases      //
        ////////////////////////////

        static void AddingEvent(Dictionary<Event, List<Person>> eventList){ 
            Console.Clear();
            Console.WriteLine(" -- Upisite detalje eventa -- ");
            Console.Write("Ime eventa: ");
            var eventName = Console.ReadLine();
            
            Console.WriteLine("Tip eventa (Coffee, Lecture, Concert, StudySession): ");
            var eventTypeInt = ReadLineEventTypeEnum();
            if(eventTypeInt == 404){
                Console.ForegroundColor = ConsoleColor.DarkRed;
                System.Console.WriteLine("Nepostojeci tip eventa!");
                Console.ForegroundColor = ConsoleColor.White;
                PressEnterToContinue();
                return;
            }
            var eventType = (EventTypeEnum) eventTypeInt;
            
            Console.WriteLine("Pocetak eventa (mm/dd/yyyy hh:mm)");
            var eventStartTime = new DateTime();
            if(!ReadLineDateTime(out eventStartTime)){
                Console.ForegroundColor = ConsoleColor.DarkRed;
                System.Console.WriteLine("Nevaljani datum \nIspravan oblik je mm/dd/yyyy hh:mm");
                Console.ForegroundColor = ConsoleColor.White;

                PressEnterToContinue();return;
            }

            Console.WriteLine("Kraj eventa (mm/dd/yyyy hh:mm)");
            var eventEndTime = new DateTime();
            if(!ReadLineDateTime(out eventEndTime)){
                Console.ForegroundColor = ConsoleColor.DarkRed;
                System.Console.WriteLine("Nevaljani datum \nIspravan oblik je yy/mm/dd hh:mm");
                Console.ForegroundColor = ConsoleColor.White;

                PressEnterToContinue();return;
            }


            if(CheckDateTimeValidity(eventStartTime, eventEndTime, eventList)){
                if(UserConfirmation("Dodati event?")){
                    eventList.Add(new Event(eventName, eventType, eventStartTime, eventEndTime), new List<Person>(){});
                    System.Console.WriteLine("Event je dodan!");
                }else{
                    Console.WriteLine("Vracam se u menu");
                }
            }else{
                Console.WriteLine("Promijeni vrijeme eventa");
            }


            PressEnterToContinue();
        }

        static void DeleteEvent(Dictionary<Event, List<Person>> eventList){
            Console.Clear();
            System.Console.WriteLine("Unesite naziv eventa koji zelite unijeti");
            var userInput = Console.ReadLine();

            var toDelete = new Event();
            foreach(var _event in eventList.Keys){
                if(userInput == _event.Name){
                    toDelete = _event;
                }
            }

            if(eventList.Remove(toDelete)){
                System.Console.WriteLine("Event deleted");
            }else{
                System.Console.WriteLine("Could not find {0}", userInput);
            }

            PressEnterToContinue();
        }

        static void EditEvent(Dictionary<Event, List<Person>> eventList){
            Console.Clear();

            
        }
    }
}
