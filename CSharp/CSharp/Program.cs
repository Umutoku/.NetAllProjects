using CSharp;
using System.Collections;

public delegate void FullName(string name, string lastname);
internal class Program
{
    //public delegate bool promosnyonDelegate(isci isci);

    public delegate void speedDelegate(int speedValue);
    public static void Main(string[] args)
    {
        //FullName full = new(FullNameMethod1);
        //FullName full2 = new(FullNameMethod2);
        //FullName full3 = new(FullNameMethod3);

        ////full("umut", "oku");

        //FullName Zincir = full + full2 + full3;

        //Zincir("umut", "oku");

        string[] isimler = new string[3];
        isimler[0] = "umut";
        isimler[1] = "umut";
        isimler[2] = "umut";

        ArrayList isimler1 = new ArrayList(); //başlangıç 4 sonra 8 sonra 16
        isimler1.Add("umut");
        isimler1.Add(4);
        //isimler1.Add(new StreamReader("stream"));


        isci i1 = new isci{ isim= "umut", soyisim= "oku", maas= 1000,tecrube = 1 };

        isci i2 = new isci { isim = "berke", soyisim = "yorul", maas = 2000, tecrube = 2 };

        isci i3 = new isci { isim = "tolga", soyisim = "demir", maas = 3000, tecrube = 3 };

        isci i4 = new isci { isim = "pelin", soyisim = "suse", maas = 4000, tecrube = 4 };

        List<isci> isciler = new List<isci>();
        isciler.Add(i1);
        isciler.Add(i2);
        isciler.Add(i3);
        isciler.Add(i4);


        //isci.maasilepro(isciler, 2000);
        //isci.tecrubeilepro(isciler, 3);


        //isci.promosyon(isciler,  (maas_promosyonu3000));
        //isci.promosyon(isciler,  (tecrube_promosyonu3000));
        //isci.promosyon(isciler, i => i.maas > 2000);








        //car c = new car();
        //c.Model = "renault";

        //c.speedEvent += C_speedEvent;
        //c.speedEvent += (i)=> { Console.WriteLine("Araba hızı aştı"+i); }; //yeni yöntem lambda


        //for (int i = 50; i < 100; i+=5)
        //{
        //  System.Threading.Thread.Sleep(300);
        //    c.Speed = i;
        //    Console.WriteLine("araç hızı"+i);

        //}


        int cikar;
        int carp;
        int topla = islem(2, 3,out carp,out cikar);
        //Console.WriteLine(topla +" "+ carp+""+cikar);


        //int a = 10;
        //Console.WriteLine("a değişkeninin değeri: "+a);

        //getData(ref a);
        //Console.WriteLine("method çalıştıktan sonra > a değişkeni: " + a);


        myclass mc = new myclass();
        Console.WriteLine(mc.ToString());


        int sayi = 10;
        sayi.ciftMi();

        urun urun = new(2,"selam");
        urun urun1 = new(2);
        urun urun2 = new();

        dikdortgen d1 = new dikdortgen{uzunluk = 10, yukseklik= 5 };
        dikdortgen d2 = new dikdortgen { uzunluk = 20, yukseklik = 10 };
        dikdortgen d3 = new dikdortgen { uzunluk = d1.uzunluk+d2.uzunluk, yukseklik = d1.yukseklik+d2.yukseklik };
        dikdortgen d4 = d1 + d2;

    }

    class dikdortgen
    {
        public int uzunluk { get; set; }
        public int yukseklik { get; set; }


        public override string ToString()
        {
            return uzunluk.ToString() + yukseklik.ToString();
        }

        public static dikdortgen operator +(dikdortgen d1, dikdortgen d2) // + operatörü için method
        {
            return new dikdortgen { uzunluk = d1.uzunluk + d2.uzunluk, yukseklik = d1.yukseklik + d2.yukseklik };
        }
    }

    internal class urun
    {
        public int id { get; set; }
        public string isim { get; set; }
        public int fiyat { get; set; }

        public urun()
        {

        }

        public urun(int id)
        {
            this.id = id;
            this.isim = null;
            this.fiyat = 0;
        }
        public urun(int id,string isim)
        {
            this.id = id;
            this.isim = isim;
            this.fiyat = 0;
        }

    }

    public class myclass()
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public override string ToString()
        {
            return (Name + " "+ Surname);
        }
    }


    public static int toplama(params int[] sayilar)
    {
        int sum = 0;
        foreach(var item in sayilar)
        {
            sum += item;
        }
        return sum;
    }

    public static void say()
    {
        for(int i = 0; i < 10; i++)
        {
            if (i == 10) return; // voidi durdurmak için return
        }
        
    }

    public static void getData(ref int a)
    {
        a = a + 10;
    }


    public static int islem(int a, int b,out int carp,out int cikar)
    {
        cikar = a - b;
        carp = a * b;
        return a + b;
    }



    private static void C_speedEvent(int speedValue)
    {
        Console.WriteLine("Araba hızını aştı."+speedValue);
    }

    public class car
    {
        public event speedDelegate speedEvent;
        public event Action<int> speedPredicat;


        private int _speed;
        public string Model { get; set; }
        public int Speed { get => _speed;
            set {
            if(value>80 && speedEvent!=null)
                {
                    speedEvent(value);
                }
                else
                {
                    _speed = value;
                }
            } }
    }

    public static bool maas_promosyonu3000(isci i)
    {
        if(i.maas>3000)
        {
            return true;
        }
        else
        { return false; }
    }

    public static bool tecrube_promosyonu3000(isci i)
    {
        if (i.tecrube > 3)
        {
            return true;
        }
        else
        { return false; }
    }

    public class isci
    {
        public string isim { get; set; }
        public string soyisim { get; set; }
        public int maas { get; set; }
        public int tecrube { get; set; }


        public static void promosyon(List<isci> isciler,Func<isci,bool> promosnyonDelegate )
        {
            foreach(var i in isciler)
            {
                if (promosnyonDelegate(i))
                {
                    Console.WriteLine(i.isim + i.soyisim);
                }
            }
        }

        //public static void maasilepro(List<isci> iscis, int maas)
        //{
        //    foreach (var isi in iscis)
        //    {
        //        if(isi.maas >= maas)
        //        {
        //            Console.WriteLine(isi.isim + " "+ isi.soyisim);
        //        }
        //    }
        //}

        //public static void tecrubeilepro(List<isci> iscis,int tecrube)
        //{
        //    foreach (var isi in iscis)
        //    {
        //        if (isi.tecrube >= tecrube)
        //        {
        //            Console.WriteLine(isi.isim + " " + isi.soyisim);
        //        }
        //    }
        //}
    }

    public static void FullNameMethod1(string name,string lastname)
    {
        Console.WriteLine(name + " " +lastname);
    }

    public static void FullNameMethod2(string name,string lastname)
    {
        Console.WriteLine(name.ToLower() + " " +lastname.ToUpper());
    }

    public static void FullNameMethod3(string name, string lastname)
    {
        Console.WriteLine(lastname.ToUpper() + " " + name.ToUpper());
    }
}