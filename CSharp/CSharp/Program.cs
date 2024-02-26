using CSharp;
using System.Collections;
using System.Diagnostics;

public delegate void FullName(string name, string lastname);
internal class Program
{
    //public delegate bool promosnyonDelegate(isci isci);

    public delegate void speedDelegate(int speedValue); // sayesinde eventlerde kullanabiliriz. Eğer eventlerde delegate kullanmazsak eventlerde sadece void kullanabiliriz.
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


        isci i1 = new isci { isim = "umut", soyisim = "oku", maas = 1000, tecrube = 1 };

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
        int topla = islem(2, 3, out carp, out cikar);
        //Console.WriteLine(topla +" "+ carp+""+cikar);


        //int a = 10;
        //Console.WriteLine("a değişkeninin değeri: "+a);

        //getData(ref a);
        //Console.WriteLine("method çalıştıktan sonra > a değişkeni: " + a);


        myclass mc = new myclass();
        Console.WriteLine(mc.ToString());


        int sayi = 10;
        sayi.ciftMi();

        urun urun = new(2, "selam");
        urun urun1 = new(2);
        urun urun2 = new();

        dikdortgen d1 = new dikdortgen { uzunluk = 10, yukseklik = 5 };
        dikdortgen d2 = new dikdortgen { uzunluk = 20, yukseklik = 10 };
        dikdortgen d3 = new dikdortgen { uzunluk = d1.uzunluk + d2.uzunluk, yukseklik = d1.yukseklik + d2.yukseklik };
        dikdortgen d4 = d1 + d2;

        int minVal = int.MinValue;
        int maxVal = int.MaxValue;

        checked // eğer checked kullanmazsak hata vermez
        {
            minVal--; // checked sayesinde hata verir
            maxVal--; // checked sayesinde hata verir
        }

        var student = new Student { Name = "umut", Surname = "oku" };
        var str = student.ToString(); // sayesinde student classının içindeki ToString methodunu çalıştırır. Eğer ToString methodu yoksa object classının ToString methodunu çalıştırır.



        var list = new List<int> { 1, 2, 3, 4, 5 }; // bu yöntemin adı collection initializer
        var list2 = new List<int>() // bu yöntemin adı object initializer
        {
            { 1 },
            { 2 },
            { 3 },
            { 4 },
            { 5 }

        };

        list.Add(6); // eğer add methodu var ise object initializer kullanabiliriz.

        var list3 = new ObjectInitializerTest<int> { 1, 2, 3, 4, 5 }; // bu yöntemin adı object initializer. ObjectInitializerTest classının içindeki Add methodunu çalıştırır.

         // this keywordü sayesinde index ve val değerlerini birleştirir.
    }

    public static void InterfaceImplement()
    {
        IUser user = new UserClass(); // burada name değeri umut olur. çünkü UserClass referans tip olduğu için user2 değişkeni user değişkenini etkiler.
        user.Name = "umut";
        user.Surname = "oku";
        IUser user2 = user;
        user2.Name = "berke";
        //Eğer bir struct iplemente edilirse referans gibi davranır. class gibi 
        IUser userS = new UserStruct(); // classlar referans tiptir. structlar değer tiptir. burada name değeri umut olur. çünkü UserStruct değer tip olduğu için userS2 değişkeni userS değişkenini etkilemez.
        userS.Name = "umut";
        userS.Surname = "oku";
        IUser userS2 = userS;
        userS2.Name = "berke";
    }


    public interface IUser
    {
        string Name { get; set; }
        string Surname { get; set; }
    }

    public class UserClass : IUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public UserClass UserClassImp { get; set; }
    }

    public struct UserStruct : IUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        //public UserStruct UserStructImp { get; set; } // Bunu yapamayız çünkü strutlar bir yer tuttuğu için sonsuz döngüye girer.
    }

    public abstract class BaseClass
    {
    }
    
    public class DerivedClass : BaseClass
    {
        public void BaseMethod()
        {
            Console.WriteLine("DerivedClass");
        }
    }

    public class DerivedClass2 : DerivedClass
    {
        public new void BaseMethod() // new keywordü sayesinde BaseMethod methodunu gizler.
        {
            Console.WriteLine("DerivedClass2");
        }
    }

    public class ObjectInitializerTest<T> : IEnumerable<T>
    {
        public void Add(T item)
        {

        }

        public IEnumerator GetEnumerator()
        {
            return null;
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return null;
        }

        public string this[int index, string val] // this keywordü sayesinde index ve val değerlerini birleştirir.
        {
            get => $"{index} {val}"; // index ve val değerlerini birleştirir.
        }
    }

    [DebuggerDisplay("{DebugDisplay}")] // sayesinde debug ekranında tostring değil DebugDisplay methodunun içeriğini gösterir.
    class Student
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public override string ToString()
        {
            return Name + " " + Surname;
        }

        public string DebugDisplay => Name + " " + Surname; // sayesinde debug modda bu methodu çalıştırır.
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


        public static void promosyon(List<isci> isciler,Func<isci,bool> promosnyonDelegate ) // sayesinde promosyonu istediğimiz şekilde yapabiliriz
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

    int minVal = int.MinValue;
    int maxVal = int.MaxValue;


}