using System;
abstract class Phong
{
    protected int songay;
    public Phong(int songay)
    {
        this.songay = songay;
    }
    public abstract double TinhTien();
    public abstract void Hien();
}
class PhongA : Phong
{
    protected int tiendv;
    public PhongA(int songay) : base(songay)
    {
        Console.WriteLine("Nhap tien dich vu");
        tiendv = int.Parse(Console.ReadLine());
    }
    public override double TinhTien()
    {
        if (songay < 5) return songay * 80 + tiendv;
        else return 5 * 80 * (songay - 5) * 80 * 0.9 + tiendv;
    }
    public override void Hien()
    {
        Console.WriteLine("Dich vu phong A");
        Console.WriteLine("Tien dich vu:" + tiendv);
        Console.WriteLine("Tien phong:" + TinhTien());
    }
}
class PhongB : Phong
{
    public PhongB(int songay) : base(songay) { }
    public override double TinhTien()
    {
        if (songay < 5) return songay * 60;
        else return 5 * 60 * (songay - 5) * 60 * 0.9;

    }
    public override void Hien()
    {
        Console.WriteLine("Dich vu phong B");
        Console.WriteLine("Tien phong:" + TinhTien());
    }
}
class PhongC : Phong
{
    public PhongC(int songay) : base(songay) { }
    public override double TinhTien()
    {
        return songay * 40;
    }
    public override void Hien()
    {
        Console.WriteLine("Dich vu phong C");
        Console.WriteLine("Tien phong:" + TinhTien());
    }
}
class HoaDonKhach
{
    private string tenkhach;
    private int songay;
    private Phong loaiphong;
    public void Nhap()
    {
        Console.WriteLine("Nhap thong tin hoa don khach hang");
        Console.Write("Ho ten:");
        tenkhach = Console.ReadLine();
        Console.Write("So ngay o:");
        songay = int.Parse(Console.ReadLine());
        Console.WriteLine("Cho biet loai phong dinh o A,B,C");
        char ch = char.Parse(Console.ReadLine());
        switch (char.ToUpper(ch))
        {
            case 'A': loaiphong = new PhongA(songay); break;
            case 'B': loaiphong = new PhongB(songay); break;
            case 'C': loaiphong = new PhongC(songay); break;
        }
    }
    public void Hien()
    {
        Console.WriteLine("Thong tin hoa don khach hang");
        Console.WriteLine("Ho ten khach:" + tenkhach);
        Console.WriteLine("So ngay o:" + songay);
        Console.WriteLine("Khach hang da su dung");
        loaiphong.Hien();
    }
}
class Program
{
    static void Main()
    {
        HoaDonKhach t = new HoaDonKhach();
        t.Nhap();
        Console.Clear();
        t.Hien();
        Console.WriteLine();
    }
}