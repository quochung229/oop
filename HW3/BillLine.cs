using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW3
{
    //phần mở rộng BillLine
    class BillLine
    {
        private Item item;
        private int quantity;

        public BillLine(Item item, int quantity)
        {
            this.item = item;
            this.quantity = quantity;
        }

        public void SetQuantity(int q) => quantity = q;
        public int GetQuantity() => quantity;

        public void SetItem(Item i) => item = i;
        public Item GetItem() => item;

        public double GetLineTotal() => item.GetPrice() * quantity;
        public double GetLineDiscount() => item.GetDiscount() * quantity;

        public override string ToString()
        {
            return $"{item} x {quantity} = {GetLineTotal():C}";
        }
    }

    class GroceryBillV2
    {
        protected Employee clerk;
        protected List<BillLine> lines;

        public GroceryBillV2(Employee clerk)
        {
            this.clerk = clerk;
            lines = new List<BillLine>();
        }

        public virtual void Add(BillLine line) => lines.Add(line);

        public virtual double GetTotal()
        {
            double total = 0;
            foreach (var line in lines) total += line.GetLineTotal();
            return total;
        }

        public virtual void PrintReceipt()
        {
            Console.WriteLine(clerk);
            Console.WriteLine("Hóa đơn mở rộng:");
            foreach (var line in lines) Console.WriteLine(line);
            Console.WriteLine($"Tổng: {GetTotal():C}");
        }
    }

    class DiscountBillV2 : GroceryBillV2
    {
        private bool preferred;
        private int discountCount;
        private double discountAmount;

        public DiscountBillV2(Employee clerk, bool preferred) : base(clerk)
        {
            this.preferred = preferred;
            discountCount = 0;
            discountAmount = 0;
        }

        public override void Add(BillLine line)
        {
            base.Add(line);
            if (preferred && line.GetItem().GetDiscount() > 0)
            {
                discountCount += line.GetQuantity();
                discountAmount += line.GetLineDiscount();
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