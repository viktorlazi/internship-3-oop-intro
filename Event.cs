using System;

namespace Internship_3_oop_intro
{
    class Event
    {
        public Event(){

        }
        public Event(string name, EventTypeEnum eventType,
                    DateTime startTime, DateTime endTime){
            Name = name;
            EventType = eventType;
            StartTime = startTime;
            EndTime = endTime;
        }

        public string Name {get;set;}
       

        EventTypeEnum EventType{get;set;}
        public DateTime StartTime{get;set;}
        public DateTime EndTime{get;set;}

        public Person [] Attendants;
    }
}
