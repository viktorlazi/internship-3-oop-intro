# internship-3-oop-intro

OOP C# Domaći:
Izradite .NET Core konzolnu aplikaciju za praćenje osoba koje dolaze na neki event.
Aplikacija treba sadržavati klasu koja predstavlja osobu koja dolazi na taj event i klasu koja predstavlja event.
Klasa koja predstavlja treba sadržavati sve osnovne informacije o toj osobi kao što su:
FirstName, LastName, OIB i broj mobitela.
Klasa koja predstavlja event treba sadržavati sljedeće:
Name, EventType, StartTime i EndTime.
Tip eventa je enum koji sadrži sljedeće vrijednosti:
-       Coffee
-       Lecture
-       Concert
-       StudySession
 
Za organizaciju podataka napravite u main dijelu programa dictionary koji će sadržavati event kao ključ i listu osoba kao vrijednost. Lista osoba predstavlja sve osobe koje dolaze na event
Koristiti se konceptima koje smo usvojili tijekom predavanja za implementaciju sljedećih zahtjeva:
1.    Dodavanje eventa
2.    Brisanje eventa
3.  Edit eventa
4.    Dodavanje osobe na event
5.    Uklanjanje osobe sa eventa
6.    Ispis detalja eventa. Ovaj dio je u odvojenom menu-u koji sadrži sljedeće:
1.    Ispis detalja eventa u formatu: name – event type – start time – end time – trajanje – ispis broja ljudi na eventu
2.    Ispis svih osoba na eventu u formatu: [Redni broj u listi]. name – last name – broj mobitela
3.    Ispis svih detalja. Kombinacija ispisa detalja eventa ( 6.1.) i ispisa svih osoba ( 6.2.)
4.    Izlazak iz podmenija.
      6.   Prekid rada
Osim zahtjeva definiranih gore navedenom listom treba implementirati sljedeće validacije:
-       Osoba sa istim OIB-om se ne smije moći dodati na event više od jedan put
-       Kraj eventa ne smije biti prije početka eventa.
-       Sve vrijednosti unutar objekta trebaju biti unesene.
-    Samo jedan event se može održavati u jednom vremenskom rasponu. Ili drugim riječima u jednom trenutku u vremenu samo jedan event može biti zakazan.
	
Za unos datuma koristite sljedeći tip: DateTime.
