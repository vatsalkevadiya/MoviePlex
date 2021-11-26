using System;
using System.Collections;
using System.Linq;

namespace MoviePlex
{
    internal class Main
    {
        const int MaxMovie = 10;
        Movie[] Movies = new Movie[MaxMovie];
        int count = 0;
        int num_of_movies { get; set; }
        public void Welcome()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n************************************************************************");
            Console.WriteLine("*                                                                      *");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("*                     Welcome to Movieplex Theatre                     *");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("*                                                                      *");
            Console.WriteLine("************************************************************************");

            int counter = 0;
            do
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nPlease select from the following option :");
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("\n1. Administrator");
                Console.WriteLine("2. Guest");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\nSelect the option : ");
                String userInput = Console.ReadLine();
                try
                {
                    int userEntry = 0;
                    if (int.TryParse(userInput, out userEntry))
                    {
                        if (userEntry == 1)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkMagenta;
                            Password password = new Password();
                            password.checkPassword();
                            admin();
                            break;
                        }
                        else if (userEntry == 2)
                        {
                            guest();
                            break;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            throw new Exception("\nYou have selected a wrong option. Please select from 1 or 2.");
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        UserException userException = new UserException("\nPlease enter numeric value. You have selected wrong option");
                        throw userException;
                    }
                }
                catch (UserException userException)
                {
                    Console.WriteLine(userException.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            } while (counter == 0);
        }
        public void admin()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n*************************************************************************************");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("**                         Welcome MoviePlex Administrator                         **");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("*************************************************************************************\n");
            Console.Write("How Many Movies are playing today? : ");
            num_of_movies = Convert.ToInt32(Console.ReadLine());
            addMovies();
        }
        public void guest()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n************************************************************************");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("**                          Welcome to Guest                          **");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("************************************************************************\n");
            showMovies();
            Console.WriteLine("Which Movie would you like to watch:");
        }

        public void addMovies()
        {
            string[] array1 = { "First", "Second", "Third", "Fourth", "Fifth", "Sixth", "Seventh", "Eighth", "Ninth", "Tenth" };
            string[] array2 = array1.Take(num_of_movies).ToArray();

            ArrayList myAL = new ArrayList() { "G", "PG", "PG-13", "R", "NC-17", "2", "4", "6", "8", "10", "12", "14", "16", "18", "20", "22", "24" };
            string userKeyPress; ;
            do
            {
                foreach (string s in array2)
                {
                    Movie movie = new Movie();
                    Console.WriteLine("");
                    Console.Write("Please Enter the " + s + " movie name : ");
                    movie.Name = Console.ReadLine();
                    do
                    {
                        Console.Write("Please Enter the Age Limit or Rating of the " + s + " Movie  : ");
                        userKeyPress = Console.ReadLine().ToUpper();
                    }
                    while (!myAL.Contains(userKeyPress));
                    movie.Rating = userKeyPress;
                    Movies[count] = movie;
                    count++;
                }

            } while (count != num_of_movies);
            Console.WriteLine("");
            showMovies();
            confirmMovies();
        }
        public void showMovies()
        {
            for (int i = 0; i < num_of_movies; i++)
            {

                Console.WriteLine((i + 1) + ".  " + Movies[i].Name + " {" + Movies[i].Rating + "}");
            }

        }
        private void confirmMovies()
        {
            Console.WriteLine("");
            char ch;
            Console.Write("Your Movies playing today are Listed above. Are you Satisfied? (Y/N})? ");
            ch = Convert.ToChar(Console.ReadLine());
            switch (Char.ToLower(ch))
            {
                case 'y':
                    Console.Clear();
                    Welcome();
                    break;
                case 'n':
                    Console.Clear();
                    Main main = new Main();
                    main.admin();
                    break;
                default:
                    Console.WriteLine("Enter either Y or N");
                    confirmMovies();
                    break;
            }
        }

        class UserException : ApplicationException
        {
            private string msgDetails;
            public UserException() { }
            public UserException(string message)
            {
                msgDetails = message;
            }
            public override string Message => $"{msgDetails}";
        }
    }
}