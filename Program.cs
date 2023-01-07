using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//============================================================
// Student Number : S10223281, S10204037
// Student Name : Chloe Lim Jiexi, Shernice Oh Yu En
// Module Group : T11
//============================================================


namespace PRG2_T11_Team8
{
    class Program
    {
        static int screeningNumber = 1001;
        static int orderNumber = 1;
        static void Main(string[] args)
        {
            List<Cinema> CinemaList = new List<Cinema>();
            List<Movie> MovieList = new List<Movie>();
            List<Screening> ScreeningList = new List<Screening>();
            List<Order> OrderList = new List<Order>();
            List<Ticket> TicketList = new List<Ticket>();

            //Part 1. Load Movie Data.
            InitMovieList(MovieList);

            //Part 1. Load Cinema Data.
            InitCinemaList(CinemaList);

            //Part 2. Load Screening Data.
            InitScreeningList(ScreeningList, CinemaList, MovieList);

            while (true)
            {
                DisplayMenu();
                int option = GetInt("Enter your option: ");
                if (option == 1)
                {
                    Console.WriteLine("Display Cinema Details");
                    //List all cinema details. 
                    ListCinemas(CinemaList);
                }
                else if (option == 2)
                {
                    Console.WriteLine("Display Screening Details");
                    //List all screenings. 
                    ListScreenings(ScreeningList);
                }
                else if (option == 3)
                {
                    Console.WriteLine("Display Movie Details");
                    //Part 3. List all movies. 
                    ListMovies(MovieList);
                }
                else if (option == 4)
                {
                    Console.WriteLine("List Movie Screenings");
                    //Part 4. List movie screenings. 
                    ListMovieScreenings(MovieList, ScreeningList, CinemaList);
                }
                else if (option == 5)
                {
                    Console.WriteLine("Add a Movie Screening Session");
                    //Part 5. Add a movie screening session. 
                   AddMovieScreenings(ScreeningList, MovieList, CinemaList);
                }
                else if (option == 6)
                {
                    Console.WriteLine("Delete Movie Screening Session");
                    //Part 6.  
                    DeleteMovieScreeningSession(ScreeningList);
                }
                else if (option == 7)
                {
                    Console.WriteLine("Order Movie Ticket");
                    //Part 7. 
                    OrderMovieTicket(ScreeningList, MovieList, CinemaList, OrderList);
                }
                else if (option == 8)
                {
                    Console.WriteLine("Cancel Order of Ticket");
                    //Part 8. 
                    int numOfTickets = 0;
                    double totalAmount = 0;
                   CancelOrderTicket(ScreeningList, MovieList, OrderList, CinemaList, TicketList, numOfTickets, totalAmount);
                }
                else if (option == 9)
                {
                    Console.WriteLine("Recommend Movie based on Sale of Tickets Sold");
                    //Part 3.1 (Advanced Features) - Shernice Oh Yu En(S10204037B)
                    RecommendedMovie(OrderList, TicketList);
                }
                else if (option == 10)
                {
                    Console.WriteLine("Display Available Seats of Screening Session in Descending Order");
                    //Part 3.2 (Advanced Features) - Chloe Lim Jiexi (S10223281K)
                    DisplayScreeningSeats(ScreeningList);
                }
                else if (option == 0)
                {
                    Console.Write("Bye");
                    break;
                }
            }
        }

        //Part 1. Load Movie Data.
        static void InitMovieList(List<Movie> MovieList)
        {
            //1. load data from given csv file and populate into a list
            string datafile = "Movie.csv";
            if (File.Exists(datafile))
            {
                string[] movieCsvLines = File.ReadAllLines(datafile);
                for (int i = 1; i < movieCsvLines.Length; i++)
                {
                    string[] dataTwo = movieCsvLines[i].Split(',');

                    //For the title, duration, classification, openingDate and genreList
                    string title = dataTwo[0];
                    int duration = Convert.ToInt32(dataTwo[1]);
                    string classification = dataTwo[3];
                    DateTime openingDate = Convert.ToDateTime(dataTwo[4]);
                    List<string> genreList = new List<string>(dataTwo[2].Split('/'));

                    //Adding of movies into MovieList
                    Movie m = new Movie(title, duration, classification, openingDate, genreList);
                    MovieList.Add(m);
                }
            }
            else
                Console.WriteLine("File does not exist");
        }

        //Part 1. Load Cinema Data.
        static void InitCinemaList(List<Cinema> CinemaList)
        {
            //1. load data from given csv file and populate into a list
            string datafile = "Cinema.csv";
            if (File.Exists(datafile))
            {
                string[] cinemaCsvLines = File.ReadAllLines(datafile);

                for (int i = 1; i < cinemaCsvLines.Length; i++)
                {
                    string[] data = cinemaCsvLines[i].Split(',');

                    //For the name, hallNo and capacity
                    string name = data[0];
                    int hallNo = Convert.ToInt32(data[1]);
                    int capacity = Convert.ToInt32(data[2]);

                    //Adding of cinemas into CinemaList
                    Cinema c = new Cinema(name, hallNo, capacity);
                    CinemaList.Add(c);
                }
            }
            else
                Console.WriteLine("File does not exist");
        }

        //Part 2. Load Screening Data.
        static void InitScreeningList(List<Screening> ScreeningList, List<Cinema> CinemaList, List<Movie> MovieList)
        {
            //1. load data from given csv file and populate into a list
            string datafile = "Screening.csv";
            if (File.Exists(datafile))
            {
                string[] screeningCsvLines = File.ReadAllLines(datafile);
                for (int i = 1; i < screeningCsvLines.Length; i++)
                {
                    string[] dataThree = screeningCsvLines[i].Split(',');
                    DateTime screeningDateTime = Convert.ToDateTime(dataThree[0]);
                    string screeningType = dataThree[1];
                    string cinemaName = dataThree[2];
                    int hallNo = Convert.ToInt32(dataThree[3]);
                    string movieTitle = dataThree[4];
                    Cinema cinema = SearchCinema(CinemaList, cinemaName, hallNo);
                    Movie movie = SearchMovie(MovieList, movieTitle);
                    int seatsRemaining = cinema.Capacity;
                    Screening s = new Screening(screeningNumber++, screeningDateTime, screeningType, cinema, movie);
                    ScreeningList.Add(s);
                    movie.AddScreening(s);
                }
            }
            else
                Console.WriteLine("File does not exist");
        }

        //Method to Search for a Cinema for Part 2
        static Cinema SearchCinema(List<Cinema> CinemaList, string name, int hallNo)
        {
            foreach (Cinema c in CinemaList)
            {
                if (c.Name == name && c.HallNo == hallNo)
                    return c;
            }
            return null;
        }

        //Method to Search for a Movie for Part 2
        static Movie SearchMovie(List<Movie> MovieList, string title)
        {
            foreach (Movie m in MovieList)
            {
                if (m.Title == title)
                    return m;
            }
            return null;
        }

        //Part 3. Display and List all movies.
        static void ListMovies(List<Movie> MovieList)
        {
            //1. display the information of all movies
            //This is for Title,Duration(mins),Genre,Classification,OpeningDate
            Console.WriteLine("{0,-7}{1,-25}{2,-15}{3,-25}{4,-20}{5,-15}", "S/No", "Title", "Duration", "Genre", "Classification", "Opening Date");
            for (int i = 0; i < MovieList.Count; i++)
            {
                Movie m = MovieList[i];
                string genre = string.Join("/", m.GenreList.ToArray());
                Console.WriteLine("{0,-7}{1,-25}{2,-15}{3,-25}{4,-20}{5,-15}", (i + 1), m.Title, m.Duration, genre, m.Classification, m.OpeningDate);
            }
        }

        //Method to Display and List all screenings.
        static void ListScreenings(List<Screening> ScreeningList)
        {
            Console.WriteLine("{0,-5}{1,-20}{2,-25}{3,-18}{4,-20}{5,-15}{6,-15}", "S/No", "ScreeningNumber", "DateTime", "Screening Type", "Cinema", "Hall Number", "Movie Title");
            for (int i = 0; i < ScreeningList.Count; i++)
            {
                Screening s = ScreeningList[i];
                Console.WriteLine("{0,-5}{1,-20}{2,-25}{3,-18}{4,-20}{5,-15}{6,-15}", (i + 1), s.ScreeningNo, s.ScreeningDateTime, s.ScreeningType, s.Cinema.Name, s.Cinema.HallNo, s.Movie.Title);
            }
        }

        //Part 4. List movie screenings.
        static void ListMovieScreenings(List<Movie> MovieList, List<Screening> ScreeningList, List<Cinema> CinemaList)
        {
            //1. list all movies
            ListMovies(MovieList);

            //2. prompt user to select a movie
            int movieNumber = GetInt("Please select a Movie: ");

            //3. retrieve movie onject
            Movie movie = MovieList[movieNumber - 1];
            int count = 0;

            //4. retrieve and display screening sessions for that movie
            Console.WriteLine("{0,-5}{1,-25}{2,-20}{3,-20}{4,-20}{5,-20}", "S/No", "Screening DateTime", "Screening Type", "Cinema", "Hall No", "Movie Title");
            foreach (Screening s in ScreeningList)
            {
                if (s.Movie.Title == movie.Title)
                {
                    count++;
                    Console.WriteLine("{0,-5}{1,-25}{2,-20}{3,-20}{4,-20}{5,-20}", count, s.ScreeningDateTime, s.ScreeningType, s.Cinema.Name, s.Cinema.HallNo, movie.Title);
                }
            }
        }

        //Method to Display and List all cinemas for Part 5.
        static void ListCinemas(List<Cinema> CinemaList)
        {
            Console.WriteLine("{0,-7}{1,-25}{2,-15}{3,-25}", "S/No", "Name", "Hall No", "Capacity");
            for (int i = 0; i < CinemaList.Count; i++)
            {
                Cinema c = CinemaList[i];
                Console.WriteLine("{0,-7}{1,-25}{2,-15}{3,-25}", (i + 1), c.Name, c.HallNo, c.Capacity);
            }
        }

        //Part 5. Add a movie screening session.
        static void AddMovieScreenings(List<Screening> ScreeningList, List<Movie> MovieList, List<Cinema> CinemaList)
        {
            //1. list all movies
            ListMovies(MovieList);

            //2. prompt user to select a movie
            int movieNumber = GetInt("Select a movie: ");
            Movie movie = MovieList[movieNumber - 1];

            //3. prompt user to enter a screening type [2D/3D]
            Console.Write("Enter a screening type [2D/3D]: ");
            string screeningType = Console.ReadLine();

            // 4. prompt user to enter a screening date and time 
            DateTime screeningDateTime = GetDateTime("Enter screening date and time: ");

            //checking for datetime entered is after the opening date of the movie
            if (screeningDateTime > movie.OpeningDate)
            {
                // 5. list all cinema halls 
                ListCinemas(CinemaList);

                //6. prompt user to select a cinema hall
                int hallNo = GetInt("Enter cinema hall: ");
                Cinema cinema = CinemaList[hallNo - 1];

                foreach (Screening s in ScreeningList.ToArray())
                {
                    if (SearchCinema(CinemaList, cinema.Name, cinema.HallNo) == cinema)
                    {
                        //check if the cinema hall is available at the datetime entered in point 4
                        DateTime screenTime = s.ScreeningDateTime;
                        int movieDur = movie.Duration;
                        screenTime.AddMinutes(30 + movieDur);   //30 minutes cleaning time
                        if (screenTime > screeningDateTime)
                        {
                            //7. create a Screening object with the information given
                            Screening newScreen = new Screening(screeningNumber++, screeningDateTime, screeningType, cinema, movie);

                            //7. add Screening object created to the relevant screening list
                            ScreeningList.Add(newScreen);
                            movie.AddScreening(newScreen);

                            ListScreenings(ScreeningList);

                            //8. display status if movie screening session creation is successful
                            Console.WriteLine("Added successfully...");
                            break;
                        }
                    }
                    else
                    {
                        //8. display status if movie screening session creation is unuccessful
                        Console.WriteLine("Movie creation is unsuccessful.");
                        break;
                    }
                }
            }
            else
            {
                //8. display status if movie screening session creation is unuccessful
                Console.WriteLine("Movie creation is unsuccessful.");
            }
        }

        //Part 6. Delete a movie screening session.
        static void DeleteMovieScreeningSession(List<Screening> ScreeningList)
        {
            foreach (Screening screens in ScreeningList.ToArray())
            {
                //check if screening seat remaining is equal to cinema capacity
                List<Screening> movieScreeningList = new List<Screening>();

                if (screens.SeatsRemaining == screens.Cinema.Capacity)
                {
                    //add movie screening session that have not sold any tickets to movieScreeningList
                    foreach (Screening s in ScreeningList)
                    {
                        movieScreeningList.Add(s);
                    }

                    int count = 0;
                    //1. list all movie screening sessions that have not sold any tickets
                    Console.WriteLine("{0,-5}{1,-20}{2,-25}{3,-18}{4,-20}{5,-15}{6,-15}", "S/No", "ScreeningNumber", "DateTime", "Screening Type", "Cinema", "Hall Number", "Movie Title");
                    foreach (Screening s in movieScreeningList)
                    {
                        count++;
                        Console.WriteLine("{0,-5}{1,-20}{2,-25}{3,-18}{4,-20}{5,-15}{6,-15}", count, s.ScreeningNo, s.ScreeningDateTime, s.ScreeningType, s.Cinema.Name, s.Cinema.HallNo, s.Movie.Title);
                    }

                    //2. prompt user to select a session
                    int number = GetInt("Enter movie screening to delete by S/No: ");
                    Screening screen = ScreeningList[number - 1];

                    if (SearchScreening(ScreeningList, screen.ScreeningNo) == screen)
                    {
                        //3. remove the movie screening from all screening lists
                        ScreeningList.Remove(screen);
                        ListScreenings(ScreeningList);

                        //4. display status if removal is successful
                        Console.WriteLine("Screening removed successfully.");
                    }
                    else
                    {
                        //4. display status if removal is unsuccessful
                        Console.WriteLine("Screening removed unsuccessfully.");
                    }
                }
                break;
            }
        }

        //Method to Search for a Screening for Part 6
        static Screening SearchScreening(List<Screening> ScreeningList, int screeningNo)
        {
            foreach (Screening s in ScreeningList)
            {
                if (s.ScreeningNo == screeningNo)
                    return s;
            }
            return null;
        }

        //Part 7. Order movie ticket/s 
        static void OrderMovieTicket(List<Screening> ScreeningList, List<Movie> MovieList, List<Cinema> CinemaList, List<Order> OrderList)
        {
            //1. list all movies
            ListMovies(MovieList);

            //2. prompt user to select a movie
            int movieNumber = GetInt("Please select a Movie S/No: ");
            Movie movie = MovieList[movieNumber - 1];

            //3. list all movie screenings of the selected movie
            int count = 0;
            Console.WriteLine("{0,-5}{1,-25}{2,-20}{3,-20}{4,-20}{5,-20}", "S/No", "Screening DateTime", "Screening Type", "Cinema", "Hall No", "Seats Remaining");
            foreach (Screening s in ScreeningList)
            {
                if (s.Movie.Title == movie.Title)
                {
                    count++;
                    Console.WriteLine("{0,-5}{1,-25}{2,-20}{3,-20}{4,-20}{5,-20}", count, s.ScreeningDateTime, s.ScreeningType, s.Cinema.Name, s.Cinema.HallNo, s.SeatsRemaining);
                }
            }

            List<Screening> movieScreeningList = new List<Screening>(movie.ScreeningList);

            //4. prompt user to select movie screening
            int movieScreenNumber = GetInt("Please select a movie screening S/No: ");

            //5. retrieve the selected movie screening
            Screening screening = movieScreeningList[movieScreenNumber - 1];

            //6. prompt user to enter total number of tickets to order
            int numOfTickets = GetInt("Enter total number of tickets to order: ");

            List<Ticket> TicketList = new List<Ticket>();

            //6. check if figure (numOfTickets) is more or less than the available seats for th screening
            if (numOfTickets <= screening.SeatsRemaining)
            {
                if (movie.Classification != "G")
                {
                    //7. prompt user if all ticket holders meet the movie classification requirements
                    char requirement = GetChar("Did all ticket holders meet the movie classification requirement [Y/N]?: ");

                    if (requirement == 'Y')
                    {
                        //8. create an order with the status "Unpaid"
                        orderNumber++;
                        Order order = new Order(orderNumber, DateTime.Now);
                        order.Status = "Unpaid";

                        double totalAmount = 0;

                        for (int i = 0; i < numOfTickets; i++)
                        {
                            //9a. prompt user for a response depending on the type of ticket ordered
                            int ticketType = GetInt("Enter ticket type [1-Student, 2-Senior Citizen, 3-Adult]: ");

                            //9ai. For Student Ticket
                            if (ticketType == 1)
                            {
                                Console.Write("Enter level of study [Primary, Secondary, Tertiary]: ");
                                string levelOfStudy = Console.ReadLine();
                                Student studentTicket = new Student(screening, levelOfStudy);
                                //b. create a Student Ticket object
                                Ticket ticket = studentTicket;
                                //c. add the Student ticket object to ticket list of the order
                                order.AddTicket(ticket);
                                double amount = ticket.CalculatePrice();
                                totalAmount += amount;
                            }
                            //9aii. For Senior Citizen Ticket
                            else if (ticketType == 2)
                            {
                                int yearOfBirth = GetInt("Enter year of birth: ");
                                if (DateTime.Now.Year - yearOfBirth >= 55)
                                {
                                    SeniorCitizen seniorCitizenTicket = new SeniorCitizen(screening, yearOfBirth);
                                    //b. create a Senior Citizen Ticket object
                                    Ticket ticket = seniorCitizenTicket;
                                    //c. add the Senior Citizen ticket object to ticket list of the order
                                    order.AddTicket(ticket);
                                    double amount = ticket.CalculatePrice();
                                    totalAmount += amount;
                                }
                                else
                                {
                                    Console.WriteLine("You are not at least 55 years old. Please try again.");
                                    break;
                                }
                            }
                            //9aiii. For Adult Ticket
                            else if (ticketType == 3)
                            {
                                Adult adultTicket = new Adult(screening, false);
                                Console.Write("Do you want PopcornOffer for $3 [Y/N]?: ");
                                string popcornOffer = Console.ReadLine();
                                if (popcornOffer.ToUpper() == "Y")
                                    adultTicket.PopcornOffer = true;
                                //b. create a Adult Ticket object
                                Ticket ticket = adultTicket;
                                //c. add the Adult ticket object to ticket list of the order
                                order.AddTicket(ticket);
                                if (popcornOffer.ToUpper() == "Y")
                                {
                                    double amount = ticket.CalculatePrice();
                                    totalAmount += amount;
                                    totalAmount += 3;
                                }
                                else
                                {
                                    double amount = ticket.CalculatePrice();
                                    totalAmount += amount;
                                }

                            }
                            else
                                Console.WriteLine("Ticket Type Entered Incorrectly.");
                        }

                        //9d. update seats remaining for the movie screening
                        screening.SeatsRemaining -= numOfTickets;

                        //10 . list amount payable
                        Console.WriteLine("\nAmount payable is: ${0} ",totalAmount.ToString("0.00"));


                        //11. prompt user to press any key to make payment
                        Console.WriteLine("Please press any key to to make payment.");
                        Console.ReadKey();

                        //12. fill in the necessary details to the new amount (e.g. amount)
                        order.Amount = totalAmount;

                        //13. change order status to "Paid"
                        order.Status = "Paid";

                        //add order to OrderList
                        OrderList.Add(order);
                    }
                    else
                        Console.WriteLine("You did not meet the movie classification requirements.");
                }

                else
                {
                    //8. create an order with the status "Unpaid"
                    orderNumber++;
                    Order order = new Order(orderNumber, DateTime.Now);
                    order.Status = "Unpaid";

                    double totalAmount = 0;

                    for (int i = 0; i < numOfTickets; i++)
                    {
                        //9a. prompt user for a response depending on the type of ticket ordered
                        int ticketType = GetInt("Enter ticket type [1-Student, 2-Senior Citizen, 3-Adult]: ");

                        //9ai. For Student Ticket
                        if (ticketType == 1)
                        {
                            Console.Write("Enter level of study [Primary, Secondary, Tertiary]: ");
                            string levelOfStudy = Console.ReadLine();
                            Student studentTicket = new Student(screening, levelOfStudy);
                            //b. create a Student Ticket object
                            Ticket ticket = studentTicket;
                            //c. add the Student ticket object to ticket list of the order
                            order.AddTicket(ticket);
                            double amount = ticket.CalculatePrice();
                            totalAmount += amount;
                        }
                        //9aii. For Senior Citizen Ticket
                        else if (ticketType == 2)
                        {
                            int yearOfBirth = GetInt("Enter year of birth: ");
                            if (DateTime.Now.Year - yearOfBirth >= 55)
                            {
                                SeniorCitizen seniorCitizenTicket = new SeniorCitizen(screening, yearOfBirth);
                                //b. create a Senior Citizen Ticket object
                                Ticket ticket = seniorCitizenTicket;
                                //c. add the Senior Citizen ticket object to ticket list of the order
                                order.AddTicket(ticket);
                                double amount = ticket.CalculatePrice();
                                totalAmount += amount;
                            }
                            else
                            {
                                Console.WriteLine("You are not at least 55 years old. Please try again.");
                                break;
                            }
                        }
                        //9aiii. For Adult Ticket
                        else if (ticketType == 3)
                        {
                            Adult adultTicket = new Adult(screening, false);
                            Console.Write("Do you want PopcornOffer for $3 [Y/N]?: ");
                            string popcornOffer = Console.ReadLine();
                            if (popcornOffer.ToUpper() == "Y")
                                adultTicket.PopcornOffer = true;
                            //b. create a Adult Ticket object
                            Ticket ticket = adultTicket;
                            //c. add the Adult ticket object to ticket list of the order
                            order.AddTicket(ticket);
                            if (popcornOffer.ToUpper() == "Y")
                            {
                                double amount = ticket.CalculatePrice();
                                totalAmount += amount;
                                totalAmount += 3;
                            }
                            else
                            {
                                double amount = ticket.CalculatePrice();
                                totalAmount += amount;
                            }
                        }
                        else
                            Console.WriteLine("Ticket Type Entered Incorrectly.");
                    }
                    //9d. update seats remaining for the movie screening
                    screening.SeatsRemaining -= numOfTickets;

                    //10 . list amount payable
                    Console.WriteLine("\nAmount payable is: ${0} ", totalAmount.ToString("0.00"));


                    //11. prompt user to press any key to make payment
                    Console.WriteLine("Please press any key to to make payment.");
                    Console.ReadKey();

                    //12. fill in the necessary details to the new amount (e.g. amount)
                    order.Amount = totalAmount;

                    //13. change order status to "Paid"
                    order.Status = "Paid";

                    //add order to OrderList
                    OrderList.Add(order);
                }
            }
            else
                Console.WriteLine("Not enough seats. Please try again.");
        }

        //Method to List Order List for Part 8.
        static void ListOrderList(List<Order> OrderList)
        {
            Console.WriteLine("{0,-10}{1,-25}{2,-15}{3,-20}", "OrderNo", "Order DateTime", "Amount", "Status");

            foreach (Order o in OrderList)
            {
                Console.WriteLine("{0,-10}{1,-25}${2,-15}{3,-20}", (o.OrderNo-1), o.OrderDateTime, o.Amount.ToString("0.00"), o.Status);
            }
        }

        // Method to Search for Order Number for Part 8.
        static Order SearchOrder(List<Order> OrderList, int orderNum)
        {
            foreach (Order o in OrderList)
            {
                if (orderNum == o.OrderNo)
                {
                    return o;
                }
            }
            return null;
        }

        //Part 8. Cancel order of ticket
        static void CancelOrderTicket(List<Screening> ScreeningList, List<Movie> MovieList, List<Order> OrderList, List<Cinema> CinemaList, List<Ticket> TicketList, int numOfTickets, double totalAmount)
        {
            //list order list 
            ListOrderList(OrderList);

            //1. prompt user for order number   
            int orderNumber = GetInt("Enter order number: ");

            //2. retrieve the selected order   
            Order o = OrderList[orderNumber - 1];

            foreach (Order order in OrderList)
            {
                foreach (Ticket ticket in order.TicketList)
                {
                    //3. check if the screening in the selected order is screened 
                    if (o.OrderDateTime <= ticket.Screening.ScreeningDateTime)
                    {
                        //4. update seat remaining for the movie screening based on the selected order 
                        ticket.Screening.SeatsRemaining += numOfTickets;

                        //5. change order status to "Cancelled" 
                        if (SearchOrder(OrderList, o.OrderNo) == order)
                        {
                            if (order.Status != "Cancelled")
                            {
                                order.Status = "Cancelled";
                                totalAmount -= totalAmount; // totalAmount == 0 

                                //6. display a message indicating that the amount is refunded 
                                Console.WriteLine("Amount is refunded.");
                                //7. display the status of the cancelation is successful
                                Console.WriteLine("Cancellation is successful.");

                                order.Amount = totalAmount;
                                break;
                            }
                            else
                            {
                                // 6.display a message indicating that the amount is not refunded 
                                Console.WriteLine("Amount is not refunded. Please try again.");
                                //7. display the status of the cancelation is unusuccessful
                                Console.WriteLine("Cancellation is unsuccessful.");
                                break;
                            }
                        }
                        else
                        {
                            //7. display the status of the cancelation is unusuccessful
                            Console.WriteLine("Cancellation is unsuccessful.");
                            break;
                        }
                    }
                }
            }
        }

        // Part 3.1 (Advanced Features). Method to recommend movie based on sale of tickets sold - Shernice Oh Yu En(S10204037B)
        static void RecommendedMovie(List<Order> OrderList, List<Ticket> TicketList)
        {
            Console.WriteLine("Movie sale of tickets sorted from highest to lowest.\n");
            Console.WriteLine("{0,-25}{1,-20}{2,-15}","Movie Title", "Amount/Sales", "Tickets Sold");
            OrderList.Sort();
            foreach (Order o in OrderList)
            {
                foreach (Ticket t in o.TicketList)
                {
                    Console.WriteLine("{0,-25}${1,-20}{2,-15}", t.Screening.Movie.Title, o.Amount.ToString("0.00"),o.TicketList.Count);
                    break;
                }
            }
        }

        // Part 3.2 (Advanced Features). Display Available Seats of Screening Session in Descending Order - Chloe Lim Jiexi (S10223281K)
        static void DisplayScreeningSeats(List<Screening> ScreeningList)
        {
            ScreeningList.Sort();
            Console.WriteLine("Screening seats in descending order");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("{0,-20} {1, -20} {2, -20}", "Screening N/O", "Movie Title", "Seats Remaining");

            foreach (Screening s in ScreeningList)
            {
                Console.WriteLine("{0,-20} {1, -20} {2, -20}", s.ScreeningNo, s.Movie.Title, s.SeatsRemaining);
            }
        }

        //Display Menu
        static void DisplayMenu()
        {
            Console.WriteLine("\n------------- MENU --------------");
            Console.WriteLine("[1] List all cinema");
            Console.WriteLine("[2] List all screenings");
            Console.WriteLine("[3] List all movies");
            Console.WriteLine("[4] List Movie Screenings");
            Console.WriteLine("[5] Add Movie Screening");
            Console.WriteLine("[6] Delete a Movie Screening");
            Console.WriteLine("[7] Order Movie Ticket");
            Console.WriteLine("[8] Cancel Order Of Ticket");
            Console.WriteLine("[9] Recommend Movie Based On Sale of Tickets Sold");
            Console.WriteLine("[10] Display Available Seats of Screening Session in Descending Order");
            Console.WriteLine("[0] Exit");
            Console.WriteLine("---------------------------------");
        }

        //Validations----------------------------------------------------------------------------------------------------------------
        static int GetInt(string input)
        {
            int n;
            while (true)
            {
                try
                {
                    Console.Write(input);
                    n = Convert.ToInt32(Console.ReadLine());
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Invalid Input! Please try again.");
                }
            }
            return n;
        }
        static double GetDouble(string input)
        {
            double n;
            while (true)
            {
                try
                {
                    Console.Write(input);
                    n = Convert.ToDouble(Console.ReadLine());
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Invalid Input! Please try again.");
                }
            }
            return n;
        }
        static DateTime GetDateTime(string input)
        {
            DateTime n;
            while (true)
            {
                try
                {
                    Console.Write(input);
                    n = Convert.ToDateTime(Console.ReadLine());
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Invalid Input! Please try again.");
                }
            }
            return n;
        }
        static char GetChar(string input)
        {
            char n;
            while (true)
            {
                try
                {
                    Console.Write(input);
                    n = Convert.ToChar(Console.ReadLine());
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Invalid Input! Please try again.");
                }
            }
            return n;
        }
    }
}
