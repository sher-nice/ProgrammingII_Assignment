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
    class Screening:IComparable<Screening>
    {
        public int ScreeningNo { get; set; }
        public DateTime ScreeningDateTime { get; set; }
        public string ScreeningType { get; set; }
        public int SeatsRemaining { get; set; }
        public Cinema Cinema { get; set; }
        public Movie Movie { get; set; }
        public Screening() { }
        public Screening(int no, DateTime dt, string t, Cinema c, Movie m)
        {
            ScreeningNo = no;
            ScreeningDateTime = dt;
            ScreeningType = t;
            SeatsRemaining = c.Capacity;
            Cinema = c;
            Movie = m;
        }
        public override string ToString()
        {
            return "ScreeningNo: " + ScreeningNo + "ScreeningDateTime: " + ScreeningDateTime +
                "ScreeningType: " + ScreeningType + "SeatsRemaining" + SeatsRemaining +
                "Cinema" + Cinema + "Movie" + Movie;
        }
        public int CompareTo(Screening s)
        {
            if (SeatsRemaining > s.SeatsRemaining)
            {
                return -1;
            }
            else if (SeatsRemaining == s.SeatsRemaining)
            {
                return 0;
            }
            else
                return 1;
        }
    }
}
