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

        }
    }
}
