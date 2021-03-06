using System;
using System.Collections.Generic;

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
       

        public EventTypeEnum EventType{get;set;}

        public DateTime StartTime{get;set;}
        public DateTime EndTime{get;set;}


    }
}
