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
    class Adult:Ticket
    {
        public bool PopcornOffer { get; set; }
        public Adult() : base() { }
        public Adult(Screening s, bool p) : base(s) { PopcornOffer = p; }
        public override double CalculatePrice()
        {
            int day = (int)Screening.ScreeningDateTime.DayOfWeek;
            if (Screening.ScreeningType == "2D")
            {
                if (day == 5 || day == 6 || day == 7)
                    return 12.50;
                else
                    return 8.50;
            }
            else
            {
                if (day == 5 || day == 6 || day == 7)
                    return 14.00;
                else
                    return 11.00;
            }
        }
        public override string ToString() { return base.ToString() + "PopcornOffer: " + PopcornOffer; }
    }
}
