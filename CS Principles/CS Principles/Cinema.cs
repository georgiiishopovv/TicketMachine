using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CS_Principles
{
    class Cinema
    {
        // Initializing the fields and enums of the class
        public enum Cities
        {
            Sofia,
            Plovdiv,
            Burgas,
            Varna
        }

        public enum Names
        {
            CinemaCity,
            KinoArena,
            ArenaDelux,
            CinemaCenter,
            CineGrand
        }

        private string name;
        private string address;
        private int seatCount;
        private Cities city;
        private List<string> movies = new List<string>();
        private static List<string> usedAddresses = new List<string>();
        private bool[][] availableSeats; 

        
        //Constructor
        public Cinema(Cities city, Names name)
        {
            this.name = $"{name} {city}";
            this.city = city;
            generateAddress(); 
            setMovies();

            availableSeats = new bool[10][];
            for (int i = 0; i < availableSeats.Length; i++)
            {
                availableSeats[i] = new bool[16];
            }

            ConstructSaloon();
        }
        
        //A method that fills up the movie list and is called in the constructor
        public void setMovies()
        {
            this.movies.Add("Spider-Man");
            this.movies.Add("Avengers");
            this.movies.Add("Need for Speed");
            this.movies.Add("The Joker");
            this.movies.Add("Fast and Furious");
            this.movies.Add("Mission Impossible");
            this.movies.Add("Divergent");
        }

        //Gets a random address based on given city
        public void generateAddress()
        {
            switch (city)
            {
                case Cities.Sofia:
                    generateSofiaAddress();
                    break;
                case Cities.Plovdiv:
                    generatePlovdivAddress();
                    break;
                case Cities.Burgas:
                    generateBourgasAddress();
                    break;
                case Cities.Varna:
                    generateVarnaAddress();
                    break;
                default:
                    break;
            }

        }

        //Gets a random address in Sofia
        public void generateSofiaAddress()
        {
            Random rand = new Random();
            string currentAddress = "";

            while (this.address == null)
            {
                int index = rand.Next(5);
                switch (index)
                {
                    case 0:
                        currentAddress = "12 Ivan Shishman Street";
                        break;
                    case 1:
                        currentAddress = "33 Pirotska Street";
                        break;
                    case 2:
                        currentAddress = "18 Bulgaria Boulevard";
                        break;
                    case 3:
                        currentAddress = "5 Aleko Konstantinov Street";
                        break;
                    case 4:
                        currentAddress = "14 Malinov Boulevard";
                        break;
                }
                if (IsAddressAvailable(currentAddress))
                {
                    this.address = currentAddress;
                    usedAddresses.Add(currentAddress);
                }
            }
        }

        //Gets a random address in Plovdiv
        public void generatePlovdivAddress()
        {
            Random rand = new Random();
            string currentAddress = "";
            while (this.address == null)
            {
                int index = rand.Next(5);
                switch (index)
                {
                    case 0:
                        currentAddress = "23 Hristo Botev Boulevard";
                        break;
                    case 1:
                        currentAddress = "8 Perushtitsa Street";
                        break;
                    case 2:
                        currentAddress = "9 Nikola Vaptsarov Boulevard";
                        break;
                    case 3:
                        currentAddress = "3 Dr. Georgi Stranski Street";
                        break;
                    case 4:
                        currentAddress = "2 Chereshovo Topche Street";
                        break;
                }
                if (IsAddressAvailable(currentAddress))
                {
                    this.address = currentAddress;
                    usedAddresses.Add(currentAddress);
                }
            }
        }
        
        //Gets a random address in Varna
        public void generateVarnaAddress()
        {
            Random rand = new Random();
            string currentAddress = "";
            while (this.address == null)
            {
                int index = rand.Next(5);
                switch (index)
                {
                    case 0:
                        currentAddress = "8 Primorski Polk boulevard";
                        break;
                    case 1:
                        currentAddress = "34 Knyaz Boris I boulevard";
                        break;
                    case 2:
                        currentAddress = "23 D-r Vasilaki Papadopolu street";
                        break;
                    case 3:
                        currentAddress = "12 Boris Ox street";
                        break;
                    case 4:
                        currentAddress = "4 Kokiche street";
                        break;
                }
                if (IsAddressAvailable(currentAddress))
                {
                    this.address = currentAddress;
                    usedAddresses.Add(currentAddress);
                }
            }
        }

        //Gets a random address in Bourgas
        public void generateBourgasAddress()
        {
            Random rand = new Random();
            string currentAddress = "";
            while (this.address == null)
            {
                int index = rand.Next(5);
                switch (index)
                {
                    case 0:
                        currentAddress = "27 Nikola Petkov Boulevard ";
                        break;
                    case 1:
                        currentAddress = "125 Dimitar Dimov Street ";
                        break;
                    case 2:
                        currentAddress = "143 Petko Zagorski Street";
                        break;
                    case 3:
                        currentAddress = "28 Izprev Street ";
                        break;
                    case 4:
                        currentAddress = "105 Aqua Calido Street";
                        break;
                }
                if (IsAddressAvailable(currentAddress))
                {
                    this.address = currentAddress;
                    usedAddresses.Add(currentAddress);
                }
            }
        }
 
        //A helper method for the address generator that allows us to see if a certain address has been assigned already
        public bool IsAddressAvailable(string address)
        {
            if (usedAddresses.Count == 0)
            {
                return true;
            }
            for (int i = 0; i < usedAddresses.Count; i++)
            {
                if (address == usedAddresses.ElementAt(i))
                {
                    return false;
                }
            }
            return true;
        }

        //Prints all the movies in the movie list so the user can choose their movie
        public void printMovies()
        {
            for (int i = 0; i < movies.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {movies.ElementAt(i)}");
            }
        }

        //Generates a cinema saloon with random taken seats (used instead of a database) so the user can choose where to sit
        public void ConstructSaloon()
        {

            for (int i = 0; i < availableSeats.Length; i++)
            {
                for (int j = 0; j < availableSeats[0].Length; j++)
                {
                    if (new Random().Next(2) == 0)
                    {
                        availableSeats[i][j] = true;
                        seatCount++;
                    }
                    else
                    {
                        availableSeats[i][j] = false;
                    }
                }
            }
        }
        
        //Prints the saloon generated by ConstructSaloon()
        public void PrintSaloon()
        {
            Console.Write("  ");
            for (int i = 1; i <= availableSeats[0].Length; i++)
            {
                if (i < 10)
                {
                    Console.Write($"  {i}  ");
                }
                else
                {
                    Console.Write($"  {i} ");
                }
            }
            Console.WriteLine();
            for (int i = 0; i < availableSeats.Length; i++)
            {
                if (i < 9)
                {
                    Console.Write((i + 1) + " ");
                }
                else
                {
                    Console.Write(i + 1);
                }
                for (int j = 0; j < availableSeats[0].Length; j++)
                {
                    if (availableSeats[i][j])
                    {
                        Console.Write("[   ]");
                    }
                    else
                    {
                        Console.Write("[ X ]");
                    }
                }
                Console.WriteLine();
            }
        }

        //Getters and Setter for the fields in the class
        public bool isSeatAvailable (int row, int column)
        {
            return availableSeats[row - 1][column - 1];
        }

        public string getMovie(int index)
        {
            return movies.ElementAt(index);
        }

        public void reserveSeat(int row, int column)
        {
            availableSeats[row - 1][column - 1] = false;
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public string Address
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
            }
        }

        public int SeatCount
        {
            get
            {
                return seatCount;
            }
            set
            {
                seatCount = value;
            }
        }

        public Cities City
        {
            get
            {
                return city;
            }
            set
            {
                city = value;
            }
        }
    }
}
