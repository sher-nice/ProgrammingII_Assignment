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
    class Cinema
    {
        public string Name { get; set; }
        public int HallNo { get; set; }
        public int Capacity { get; set; }
        public Cinema() { }
        public Cinema(string n, int h, int c)
        {
            Name = n;
            HallNo = h;
            Capacity = c;
        }
        public override string ToString()
        {
            return "Name: " + Name + "\tHallNo: " + HallNo + "\tCapacity" + Capacity;
        }
    }
}
