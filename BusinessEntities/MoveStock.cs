using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class MoveStock :IMoveStock
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Size { get; set; }
        public int InBar { get; set; }
        public int InStorage { get; set; }
        public int Amount { get; set; }
        public int GetID() { return this.ID; }
        public string GetName() { return this.Name; }
        public string GetTypes() { return this.Type; }
        public string GetSize() { return this.Size; }
        public int GetInBar() { return this.InBar; }
        public int GetInStorage() { return this.InStorage; }
        public int GetAmount() { return this.Amount; }

        public MoveStock()
        {
            this.ID = 0;
            this.Name = "Uknown";
            this.Type = "Uknown";
            this.Size = "Uknown";
            this.InBar = 0;
            this.InStorage = 0;
            this.Amount = 0;


        }

        public MoveStock(int id, string name, string type, string size, int Bar, int storage, int amount)
        {
            this.ID = id;
            this.Name = name;
            this.Type = type;
            this.Size = size;
            this.InBar = Bar;
            this.InStorage = storage;
            this.Amount = amount;

        }

    }
}
