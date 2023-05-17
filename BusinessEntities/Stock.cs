using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class Stock : IStock
    {
        public int StockID { get; set; }
        public string NameStock { get; set; }
        public string Type { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }
        public string Size { get; set; }
        public string Location { get; set; }

        public int GetStockID()
        {
            return this.StockID;
        }
        public string GetNameStock() { return this.NameStock; }
        public string GetTypeStock() { return this.Type; }
        public int GetAmount() { return this.Amount; }
        public double GetPrice() { return this.Price; }
        public string GetSize() { return this.Size; }
        public string GetLocation() { return this.Location; }

        public Stock()
        {
            this.NameStock = "Unknown";
            this.Type = "Unknown";
            this.Amount = 0;
            this.Price = 0.00;
            this.Size = "Unknown";
            this.Location = "Uknown";

        }
        public Stock(int ID, string Name, string type, int amount, string size, string location)
        {
            this.StockID = ID;
            this.NameStock = Name;
            this.Amount = amount;
            this.Price = 0.00;
            this.Type = type;
            this.Size = size;
            this.Location = location;


        }
        public Stock(int ID, string Name, string type, int amount, double price, string size, string location)
        {
            this.StockID = ID;
            this.NameStock = Name;
            this.Amount = amount;
            this.Price = price;
            this.Type = type;
            this.Size = size;
            this.Location = location;


        }

    }
}
