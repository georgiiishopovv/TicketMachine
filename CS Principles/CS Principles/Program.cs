using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;



namespace CS_Principles
{
    class Program
    { 
        // Generates a random number of cinemas that is between 2 and 5 for each city.
        static List<Cinema> GenerateCinemas()
        {
            Random rand = new Random();
            List<Cinema> cinemas = new List<Cinema>();
            for (int i = 0; i < 4; i++)
            {
                int cinemaCount = rand.Next(4) + 2;
                for (int j = 0; j < cinemaCount; j++)
                {
                    cinemas.Add(new Cinema((Cinema.Cities)i, (Cinema.Names) j));
                }
            }
            return cinemas;
        }

        static void Main(string[] args)
        {
            
            List<Cinema> cinemas = GenerateCinemas();

            //Asks for the first and last name of the user
            Name_Input:
            Console.Write("Enter your first name: ");
            string firstName = Console.ReadLine();
            Console.Write("Enter your last name: ");
            string lastName = Console.ReadLine();
            if (firstName == "" || lastName == "") // Input Validation
            {
                Console.Clear();
                Console.WriteLine("Enter a valid name!");
                goto Name_Input;
            }


            //Asks for the city the user wants to watch a movie in 
            Console.Clear();
            City_Input:
            Console.WriteLine("Choose a city by writing the numerical index: \n 1. Sofia \n 2. Plovdiv\n 3. Burgas\n 4. Varna");
            int cityIndex = 0;
            try // Input Validation
            {
                cityIndex = int.Parse(Console.ReadLine()) - 1;
            }
            catch
            {
                Console.Clear();
                Console.WriteLine("Please input a valid index.");
                goto City_Input;
            }
            if (cityIndex < 0 || cityIndex > 3) // Input Validation
            {
                Console.Clear();
                Console.WriteLine("Please input a valid index.");
                goto City_Input;
            }


            //The Console prints a list of the cinemas in the city and prompts the user to choose one of them
            Console.Clear();
            Cinema_Input:
            Console.WriteLine($"Choose a location in {(Cinema.Cities) cityIndex} by writing the numerical index:");
            List<Cinema> cinemasSpecifiedCity = new List<Cinema>();
            int count = 1;
            for (int i = 0; i < cinemas.Count; i++)
            {
                if (cinemas.ElementAt(i).City.Equals((Cinema.Cities)cityIndex))
                {
                    Console.WriteLine($"{count}. {cinemas.ElementAt(i).Name} at {cinemas.ElementAt(i).Address}");
                    cinemasSpecifiedCity.Add(cinemas.ElementAt(i));
                    count++;
                }
            }


            int cinemaIndex = 0;
            try // Input Validation
            {
                cinemaIndex = int.Parse(Console.ReadLine()) - 1;
            }
            catch
            {
                Console.Clear();
                Console.WriteLine("Please input a valid index.");
                goto Cinema_Input;
            }

            if (cinemaIndex < 0 || cinemaIndex > count - 2) // Input Validation
            {
                Console.Clear();
                Console.WriteLine("Please input a valid index.");
                goto Cinema_Input;
            }

            Cinema chosenCinema = cinemasSpecifiedCity.ElementAt(cinemaIndex);


            //Asks the user to choose a movie
            Console.Clear();
            Movie_Input:
            Console.WriteLine($"Choose a movie at {chosenCinema.Name} by writing the numerical index:");
            chosenCinema.printMovies();
            int movieIndex = 0;
            try // Input Validation
            {
                movieIndex = int.Parse(Console.ReadLine()) - 1;
            }
            catch
            {
                Console.Clear();
                Console.WriteLine("Please input a valid index.");
                goto Movie_Input;
            }

            if (movieIndex < 0 || movieIndex > 6) // Input Validation
            {
                Console.Clear();
                Console.WriteLine("Please input a valid index.");
                goto Movie_Input;
            }


            //Asks the user to choose the amount of tickets that he or she wants to purchase
            Console.Clear();
            Ticket_Imput:
            Console.WriteLine("How many tickets are you going to purchase?");
            int ticketAmount = 0;
            try // Input Validation
            {
                ticketAmount = int.Parse(Console.ReadLine());
            }
            catch
            {
                Console.Clear();
                Console.WriteLine("Please input a valid number.");
                goto Ticket_Imput;
            }

            if (ticketAmount > 160) // Input Validation
            {
                Console.Clear();
                Console.WriteLine("There aren't that many seats in the cinema. Please enter a valid amount.");
                goto Ticket_Imput;
            }
            else if (ticketAmount > chosenCinema.SeatCount)
            {
                Console.Clear();
                Console.WriteLine("There aren't enough free seats.");
                goto Ticket_Imput;
            }
            else if (ticketAmount == 0)
            {
                Console.Clear();
                Console.WriteLine("Buy at least 1 ticket, please.");
                goto Ticket_Imput;
            }
            else if (ticketAmount < 0)
            {
                Console.Clear();
                Console.WriteLine("Enter a valid amount.");
                goto Ticket_Imput;
            }


            List<Ticket> tickets = new List<Ticket>();

            //The console prints out a seating chart and asks the user to choose their seats
            Console.Clear();
            for (int i = 0; i < ticketAmount; i++)
            {
                Seat_Selection:
                Console.WriteLine("Seating:");
                chosenCinema.PrintSaloon();
                Console.WriteLine($"Please select an available seat for ticket {i + 1} by writing \"row number, seat number\" (eg. 3, 5).");
                
                string seat = Console.ReadLine();
                int rowNumber = 0;
                int columnNumber = 0;
                try // Input Validation
                {
                    if (seat.Split(", ").Length > 2)
                    {
                        Console.Clear();
                        Console.WriteLine("Invalid seat input.");
                        goto Seat_Selection;
                    }
                    rowNumber = int.Parse(seat.Split(", ")[0]);
                    columnNumber = int.Parse(seat.Split(", ")[1]);
                }
                catch
                {
                    Console.Clear();
                    Console.WriteLine("Invalid seat input.");
                    goto Seat_Selection;
                }

                if (rowNumber < 1 || rowNumber > 10 || columnNumber < 1 || columnNumber > 16) // Input Validation
                {
                    Console.Clear();
                    Console.WriteLine("Invalid seat input.");
                    goto Seat_Selection;
                }


                // Check if the selected seat is available then creates a ticket based on the seat if it is. Asks the user for a new seat if it is taken.
                if (chosenCinema.isSeatAvailable(rowNumber, columnNumber))
                {
                    Console.Clear();
                    Console.WriteLine("Your seat has been reserved.");
                    tickets.Add(new Ticket(firstName, lastName, chosenCinema, chosenCinema.getMovie(movieIndex), rowNumber, columnNumber));
                    chosenCinema.reserveSeat(rowNumber, columnNumber);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("This seat is taken.");
                    goto Seat_Selection;
                }
            }

            // Outputs the tickets and their total cost.
            Console.Clear();
            Console.WriteLine("Your tickets:\n");
            for (int i = 0; i < tickets.Count; i++)
            {
                Console.WriteLine(tickets.ElementAt(i));
            }


            double totalCost = Ticket.Cost * ticketAmount;
            Console.WriteLine("Total cost: " + Math.Round(totalCost, 2));

        }
    }
}
