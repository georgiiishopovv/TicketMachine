using System;
using System.Collections.Generic;
using System.Text;

namespace CS_Principles
{
    class Ticket
    {
        //Initializing all the fields in the class
        private string firstName;
        private string lastName;
        private string movie;
        private static double cost;
        private int row;
        private int column;
        private Cinema cinema;

        char[][] ticketTemplate = new char[10][];

        //Class Constructor
        public Ticket(string firstName, string lastName, Cinema cinema, string movie, int row, int column)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.cinema = cinema;
            this.movie = movie;
            cost = Math.Round(new Random().NextDouble() * 10 + 5, 2);
            this.row = row;
            this.column = column;
        }

        //Property of double Cost

        public static double Cost
        {
            get
            {
                return Ticket.cost;
            }
            set
            {
                Ticket.cost = value;
            }
        }

        //ToString of the ticket, which will be used for the final printing of the tickets.
        override
        public string ToString()
        {

            string output = "";
            for (int j = 0; j < 12; j++)
            {
                string row = "";
                switch (j)
                {
                    case 2:
                        row += $"Ticket Holder: {firstName} {lastName}";
                        break;
                    case 4:
                        row += $"Cinema: {cinema.Name}";
                        break;
                    case 5:
                        row += $"Address: {cinema.Address}";
                        break;
                    case 7:
                        row += $"Movie: {movie}";
                        break;
                    case 8:
                        row += $"Row: {this.row}, Seat: {column}";
                        break;
                    case 10:
                        row += $"                                      Cost: {cost}";
                        break;
                    default:
                        row = "";
                        break;
                }

                output += $"|{row}";
                for (int i = 0; i < 50 - row.Length; i++)
                {
                    if (j == 0 || j == 11)
                    {
                        output += "-";
                    }
                    else
                    {
                        output += " ";
                    }
                }
                output += "|\n";
            }
            
            return output;

        }
    }
}
