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
    abstract class Ticket
    {
        public Screening Screening { get; set; }
        public Ticket() { }
        public Ticket(Screening s)
        {
            Screening = s;
        }
        public abstract double CalculatePrice();
        public override string ToString()
        {
            return "Screening: " + Screening;
        }
    }
}
