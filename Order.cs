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
	class Order:IComparable<Order>
	{
		public int OrderNo { get; set; }
		public DateTime OrderDateTime { get; set; }
		public double Amount { get; set; }
		public string Status { get; set; }
		public List<Ticket> TicketList { get; set; } = new List<Ticket>();
		public Order() { }
		public Order(int no, DateTime o)
		{
			OrderNo = no;
			OrderDateTime = o;
		}
		public void AddTicket(Ticket ticket)
		{
			TicketList.Add(ticket);
		}
		public override string ToString()
		{
			return "OrderNo: " + OrderNo + "OrderDateTime: " + OrderDateTime + "Amount: " + Amount + "Status" + Status;
		}
		public int CompareTo(Order o)
		{
			if (Amount > o.Amount)
				return -1;
			else if (Amount == o.Amount)
				return 0;
			else
				return 1;
		}
	}
}
