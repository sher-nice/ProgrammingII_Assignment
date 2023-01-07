using System;
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
    class Movie
    {
        public string Title { get; set; }
        public int Duration { get; set; }
        public string Classification { get; set; }
        public DateTime OpeningDate { get; set; }
        public List<string> GenreList { get; set; } = new List<string>();
        public List<Ticket> TicketList { get; set; } = new List<Ticket>();
        public List<Screening> ScreeningList { get; set; } = new List<Screening>();
        public Movie() { }
        public Movie(string t, int d, string c, DateTime o, List<string> g)
        {
            Title = t;
            Duration = d;
            Classification = c;
            OpeningDate = o;
            GenreList = g;
        }
        public void AddGenre(string g)
        {
            GenreList.Add(g);
        }
        public void AddScreening(Screening screening)
        {
            ScreeningList.Add(screening);
        }
        public override string ToString()
        {
            return "Title: " + Title + "\tDuration: " + Duration + "\tClassification: " + Classification + "\tOpeningDate: " + OpeningDate;
        }
    }
}
