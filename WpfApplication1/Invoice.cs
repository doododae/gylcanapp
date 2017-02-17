using System;

namespace WpfApplication1
{
    internal class Invoice
    {
        public string ID
        {
            get;
            set;
        }
        public string Email
        {
            get;
            set;
        }
        public string MailAddress
        {
            get;
            set;
        }
        public string ShipAddress
        {
            get;
            set;
        }
        public int PID
        {
            get;
            set;
        }
        public double Stock
        {
            get;
            set;
        }
        public decimal Price
        {
            get;
            set;
        }
        public DateTime Updated
        {
            get;
            set;
        }
    }
}