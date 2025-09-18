using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW3
{
    //lớp nhân viên
    class Employee
    {
        public string Name { get; set; }

        public Employee(string name) => Name = name;

        public override string ToString() => $"Clerk: {Name}";
    }
    //lớp item
    class Item
    {
        private string name;
        private double price;
        private double discount;

        public Item(string name, double price, double discount = 0)
        {
            this.name = name;
            this.price = price;
            this.discount = discount;
        }

        public double GetPrice() => price;
        public double GetDiscount() => discount;

        public override string ToString()
        {
            return $"{name} - Price: {price:C}, Discount: {discount:C}";
        }
    }

    //lớp grocery bill
    class GroceryBill
    {
        protected Employee clerk;
        protected List<Item> items;

        public GroceryBill(Employee clerk)
        {
            this.clerk = clerk;
            items = new List<Item>();
        }

        public virtual void Add(Item i) => items.Add(i);

        public virtual double GetTotal()
        {
            double total = 0;
            foreach (var item in items) total += item.GetPrice();
            return total;
        }

        public virtual void PrintReceipt()
        {
            Console.WriteLine(clerk);
            Console.WriteLine("Hóa đơn:");
            foreach (var item in items) Console.WriteLine(item);
            Console.WriteLine($"Tổng: {GetTotal():C}");
        }
    }

    //lớp hóa đơn chiết khấu, kế thừa từ GroceryBill
    class DiscountBill : GroceryBill
    {
        private bool preferred;
        private int discountCount;
        private double discountAmount;

        public DiscountBill(Employee clerk, bool preferred) : base(clerk)
        {
            this.preferred = preferred;
            discountCount = 0;
            discountAmount = 0;
        }

        public override void Add(Item i)
        {
            base.Add(i);
            if (preferred && i.GetDiscount() > 0)
            {
                discountCount++;
                discountAmount += i.GetDiscount();
            }
        }

        public override double GetTotal()
        {
            double total = base.GetTotal();
            if (preferred) total -= discountAmount;
            return total;
        }

        public int GetDiscountCount() => preferred ? discountCount : 0;
        public double GetDiscountAmount() => preferred ? discountAmount : 0;
        public double GetDiscountPercent()
        {
            double beforeDiscount = base.GetTotal();
            if (!preferred || beforeDiscount == 0) return 0;
            return (discountAmount / beforeDiscount) * 100;
        }

        public override void PrintReceipt()
        {
            base.PrintReceipt();
            if (preferred)
            {
                Console.WriteLine($"Số mặt hàng được giảm giá: {GetDiscountCount()}");
                Console.WriteLine($"Tổng chiết khấu: {GetDiscountAmount():C}");
                Console.WriteLine($"Phần trăm chiết khấu: {GetDiscountPercent():F2}%");
            }
        }
    }
}