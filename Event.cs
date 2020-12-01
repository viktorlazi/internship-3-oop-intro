using System;

namespace Internship_3_oop_intro
{
    class Event
    {
        public string Name {get;set;}
        public enum EventType{
            Coffee,
            Lecture,
            Concert,
            StudySession
        }
        public DateTime StartTime{get;set;}
        public DateTime EndTime{get;set;}
    }
}
