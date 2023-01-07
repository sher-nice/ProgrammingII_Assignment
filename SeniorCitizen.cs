﻿using System;
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
    class SeniorCitizen:Ticket
    {
        public int YearOfBirth { get; set; }
        public SeniorCitizen() : base() { }
        public SeniorCitizen(Screening s, int y) : base(s) { YearOfBirth = y; }
        public override double CalculatePrice()
        {
            int day = (int)Screening.ScreeningDateTime.DayOfWeek;
            DateTime screeningDate = Screening.ScreeningDateTime;
            DateTime openingDate = Screening.Movie.OpeningDate;
            bool isWithin7Days = (screeningDate - openingDate).Days <= 7;

            if (Screening.ScreeningType == "2D")
            {
                if (day == 5 || day == 6 || day == 7 || isWithin7Days)
                    return 12.50;
                else if (day == 1 || day == 2 || day == 3 || day == 4)
                    return 5.00;
                else
                    return 8.50;
            }
            else
            {
                if (day == 5 || day == 6 || day == 7 || isWithin7Days)
                    return 14.00;
                else if (day == 1 || day == 2 || day == 3 || day == 4)
                    return 6.00;
                else
                    return 11.00;
            }
        }
        public override string ToString() { return base.ToString() + "YearOfBirth: " + YearOfBirth; }
    }
}
