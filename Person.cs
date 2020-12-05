using System;

namespace Internship_3_oop_intro
{
    class Person
    {
        public Person(string firstName, string lastName, int oib, string phoneNumber){
            FirstName = firstName;
            LastName = lastName;
            OIB = oib;
            PhoneNumber = phoneNumber;
        }

        public Person(int oib){
            OIB = oib;
        }
        public string FirstName {get;set;}
        public string LastName{get;set;}
        public int OIB{get;set;}
        public string PhoneNumber{get;set;}

        public bool IsTheSamePersonAs(Person x){
            if(FirstName == x.FirstName && LastName == x.LastName &&
                OIB == x.OIB && PhoneNumber == x.PhoneNumber){
                    return true;
            }else{
                return false;
            }
        }
    }
}
