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
                
                PressEnterToContinue();
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
            
            var userChoice = ReadLineColor();
            if(int.TryParse(userChoice, out int result) 
                            && result > 0 && result < 8){
                return result;
            }else{
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"{userChoice} nije valjan unos!");
                Console.ForegroundColor = ConsoleColor.White;
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
            var userChoice = ReadLineColor();
            if(int.TryParse(userChoice, out int result) 
                            && result > 0 && result < 5){
                return result;
            }else{
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"{userChoice} nije valjan unos!");
                Console.ForegroundColor = ConsoleColor.White;
                return result;
            }
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
                    AddPersonToEvent(eventList);
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
                var userInput = ReadLineColor();

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

        static bool ReadLineEventTypeEnum(out int result){
            var userInput = ReadLineColor();

            switch(userInput){
                case "Coffee": result = 0; return true;
                case "Lecture": result = 1; return true;
                case "Concert": result = 2; return true;
                case "StudySession": result = 3; return true;
                default: result = 404; return false;
            }
        }

        static bool ReadLineDateTime(out DateTime parsed){
            var userDateTime = ReadLineColor();

            if(DateTime.TryParse(userDateTime, out DateTime parsedInput)){
                parsed = parsedInput;
                return true;
            }else{
                parsed = parsedInput;
                return false;
            }
            
        }

        static bool CheckDateTimeValidity(DateTime startTime, DateTime endTime, Dictionary<Event, List<Person>> eventList, out string msg){
            
            if(startTime > endTime){
                msg = "Event ne moze zavrsiti prije nego pocne.\n";
                return false;
            }
            if(AreEventTimesOverlapping(startTime, endTime, eventList, out List<string> overlappingEventNames)){
                msg = "Event se preklapa sa drugim eventima: ";
                foreach(var name in overlappingEventNames){
                    msg = "- " + name + "\n";
                }
                return false;
            }
            msg = "";
            return true;
            
        }

        static bool AreEventTimesOverlapping(DateTime startTime, DateTime endTime, Dictionary<Event, List<Person>> eventList, out List<string> overlappingEventNames){
            var overlap = false;
            overlappingEventNames = new List<string>(){};
            foreach(var _event in eventList.Keys){
                if(!(startTime > _event.EndTime || endTime < _event.StartTime)){
                    overlappingEventNames.Add(_event.Name);
                    overlap = true;
                }
            }
            return overlap;
        }

        static bool GetEventKeyByName(string name, Dictionary<Event, List<Person>> eventList, out Event result){
            foreach(var _event in eventList.Keys){
                if(name == _event.Name){
                    result = _event;
                    return true;
                }
            }
            result = null;
            return false;
        }

        static bool IsPeronOIBValid(Person person, Dictionary<Event, List<Person>> eventList, out string msg){
            foreach(var attendents in eventList.Values){
                foreach(var attendent in attendents){
                    if(attendent.OIB == person.OIB && person != attendent){
                        msg = "Dvije osobe nemogu imati isti OIB!";
                        return false;
                    }
                }
            }
            msg = "";
            return true;
        }
        
        static void WarningMessage(string msg){
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            System.Console.WriteLine(msg);
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void InvalidInputMessage(string msg){
            Console.ForegroundColor = ConsoleColor.DarkRed;
            System.Console.WriteLine(msg);
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void PressEnterToContinue(){
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\n");
            Console.WriteLine("--- Enter to contiunue ---");
            Console.ForegroundColor = ConsoleColor.White;
            ReadLineColor();
        }




        static string ReadLineColor(){
            Console.ForegroundColor = ConsoleColor.Cyan;
            string input = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
            return input;
        }
        ////////////////////////////
        //       1 - 6 cases      //
        ////////////////////////////

        static void AddingEvent(Dictionary<Event, List<Person>> eventList){ 
            Console.Clear();
            Console.WriteLine(" -- Upisite detalje eventa -- ");
            Console.Write("Ime eventa: ");
            var eventName = ReadLineColor();
            
            Console.WriteLine("Tip eventa (Coffee, Lecture, Concert, StudySession): ");
                if(!ReadLineEventTypeEnum(out int eventTypeInt)){
                    InvalidInputMessage("Nepostojeci tip eventa");
                    return;
                }
                var eventType = (EventTypeEnum) eventTypeInt;
            
            Console.WriteLine("Pocetak eventa (mm/dd/yyyy hh:mm)");
            if(!ReadLineDateTime(out DateTime eventStartTime)){
                InvalidInputMessage("Nevaljani datum \nIspravan oblik je yy/mm/dd hh:mm");
                return;
            }

            Console.WriteLine("Kraj eventa (mm/dd/yyyy hh:mm)");
            if(!ReadLineDateTime(out DateTime eventEndTime)){
                InvalidInputMessage("Nevaljani datum \nIspravan oblik je yy/mm/dd hh:mm");
                return;
            }


            if(CheckDateTimeValidity(eventStartTime, eventEndTime, eventList, out string warningMessage)){
                if(UserConfirmation("Dodati event?")){
                    eventList.Add(new Event(eventName, eventType, eventStartTime, eventEndTime), new List<Person>(){});
                    System.Console.WriteLine("Event je dodan!");
                }else{
                    Console.WriteLine("Vracam se u menu");
                }
            }else{
                WarningMessage(warningMessage + "Promijeni vrijeme eventa");
            }


        }

        static void DeleteEvent(Dictionary<Event, List<Person>> eventList){
            Console.Clear();
            System.Console.WriteLine("Unesite naziv eventa koji zelite izbrisati");
            var userInput = ReadLineColor();

            if(GetEventKeyByName(userInput, eventList, out Event key)){
                eventList.Remove(key);
                System.Console.WriteLine("Event deleted");
            }else{
                InvalidInputMessage("Could not find " + userInput);
            }
        }



        static void EditEvent(Dictionary<Event, List<Person>> eventList){
            Console.Clear();
            System.Console.WriteLine("Unesite naziv eventa koji zelite editati");
            var userInput = ReadLineColor();
            
            if(GetEventKeyByName(userInput, eventList, out Event key)){
                System.Console.WriteLine("Unesite nove podatke za " + key.Name);
                
                Console.Write("Ime eventa: ");
                var eventName = ReadLineColor();
                
                Console.WriteLine("Tip eventa (Coffee, Lecture, Concert, StudySession): ");
                if(!ReadLineEventTypeEnum(out int eventTypeInt)){
                    InvalidInputMessage("Nepostojeci tip eventa");
                    return;
                }
                var eventType = (EventTypeEnum) eventTypeInt;
                
                Console.WriteLine("Pocetak eventa (mm/dd/yyyy hh:mm)");
                if(!ReadLineDateTime(out DateTime eventStartTime)){
                    InvalidInputMessage("Nevaljani datum \nIspravan oblik je yy/mm/dd hh:mm");
                    return;
                }

                Console.WriteLine("Kraj eventa (mm/dd/yyyy hh:mm)");
                if(!ReadLineDateTime(out DateTime eventEndTime)){
                    InvalidInputMessage("Nevaljani datum \nIspravan oblik je yy/mm/dd hh:mm");
                    return;
                }


                if(CheckDateTimeValidity(eventStartTime, eventEndTime, eventList, out string warningMessage)){
                    if(UserConfirmation("Promijeniti event?")){
                        eventList.Remove(key);
                        eventList.Add(new Event(eventName, eventType, eventStartTime, eventEndTime), new List<Person>(){});
                        System.Console.WriteLine("Event je promijenjen!");
                    }else{
                        Console.WriteLine("Vracam se u menu");
                    }
                }else{
                    WarningMessage(warningMessage + "Promijeni vrijeme eventa");
                }

            }else{
                InvalidInputMessage("Could not find " + userInput);
            }
        }

        static void AddPersonToEvent(Dictionary<Event, List<Person>> eventList){
            Console.Clear();
            System.Console.WriteLine("Zelite dodati osobu na event.");
            
            try{
                Console.Write("Ime?"); var personName = ReadLineColor();
                Console.Write("Prezime?"); var personSurname = ReadLineColor();
                Console.Write("OIB?"); var personOIB = int.Parse(ReadLineColor());
                Console.Write("Broj mobitela?"); var personPhoneNum = int.Parse(ReadLineColor());

                var person = new Person(personName, personSurname, personOIB, personPhoneNum);

                if(IsPeronOIBValid(person, eventList, out string msg)){
                    Console.WriteLine();
                    Console.Write("Na koji event covik ide?");
                    if(GetEventKeyByName(ReadLineColor(), eventList, out var eventKey)){
                        eventList[eventKey].Add(person);
                    }else{
                        InvalidInputMessage("Ne postoji uneseni event");
                    }
                }else{
                    InvalidInputMessage(msg);
                }

            }catch{
                InvalidInputMessage("Podaci covjeka nisu ispravni :(");
            }

            
        }
    }
}
