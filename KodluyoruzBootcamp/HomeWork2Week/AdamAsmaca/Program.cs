using System;

namespace AdamAsmaca
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /* 1. Kelime grubundan rastgele bir kelime seç
             * 2. Seçtiğin bu kelimenin her harfini * işaretine dönüştür ve ekranda göster
             * 3. Oyuncudan harf iste
             * 4. Harf kelimede var mı kontrol et
             * 5. a)Eğer varsa, o harfin bulunduğu * işaretlerini harfe çevir
             *    b) Yoksa bir hakkı azalt
             * 6. Oyuncudan kelimeyi tahmin etmesini iste
             *    Bilirse oyunu bitir
             *    Bilemezse 3.adıma dön
             */

            bool isGameOver = false; // koşul sağlanana kadar while döngüsü için kullanılacak
            string[] words = { "ayna", "masa", "tarantula", "endoplazmikretikulum" };
            while (!isGameOver)
            {

                string selectedWord = chooseWord(words);
                string puzzle = replaceToStar(selectedWord);// yıldızlara çevirme methodu
                Console.WriteLine(puzzle);
                bool isWordFinding = false;
                int life = 3;//kelime tahmin hakkı
                int heart = 10;// harf tahmin hakkı

                while (!isWordFinding && life >0 && heart >0)//tahmin hakları ve kelime bulunana kadar dönen döngü
                {
                    Console.Write("Bir harf giriniz: ");
                    string letter = Console.ReadLine();
                    bool isLetterExistInWord = checkLetterInWord(selectedWord, letter);// true false mantığında iki değişken eşit mi?
                    if (isLetterExistInWord)// aslında seçilen kelime için bu harf var mı methodunu kullanıyoruz
                    {
                        puzzle = replaceToStarToLetter(selectedWord, puzzle, letter);
                        Console.WriteLine(puzzle); //varsa seçilen kelimeden o indexi al puzzle üzerindeki indexe o harfi ata
                    }
                    else
                    {
                        heart--;// değilse hakkından düşecek :(
                        Console.WriteLine($"Harf doğru değil, tekrar dene. Kalan hak : {heart}");
                    }
                   
                    Console.WriteLine("Kelimeyi tahmin etmek ister misin? (E/H)");
                    string answerForGuess = Console.ReadLine();
                    if (answerForGuess.ToUpper() == "E")// kelimeyi tahmin etmek için harf tahmininden sonra girilen değere göre ilerleme
                    {
                        Console.WriteLine("Tahmininizi giriniz: ");
                        string guess = Console.ReadLine();
                        isWordFinding = compareGuessAndSelectedWord(guess, selectedWord);// girilen değer verilen otomatik değer ile aynı mı
                        if (isWordFinding)
                        {
                            Console.WriteLine("Tebrikler :)");
                            isWordFinding = true;
                        }
                        else
                        {
                            life--;
                            Console.WriteLine($"Tutturamadın, tekrar dene. Kalan hak : {life}");
                        }
                    }
                    else if(answerForGuess.ToUpper() != "H")
                    {
                        Console.WriteLine("Lütfen geçerli bir yanıt giriniz.");
                        ;
                    }
                   
                }
                Console.WriteLine("Oyuna devam mı? (E/H)");
                isGameOver = Console.ReadLine().ToUpper()=="H";
                
            }
        }

        private static bool compareGuessAndSelectedWord(string guess, string selectedWord)
        {
            return guess == selectedWord; // iki değişken birbirine eşit mi kontrol etmek için kullanılan method
        }

        private static string replaceToStarToLetter(string selectedWord, string puzzle, string letter)
        {
            int startindex = 0;
            char[] puzzleStars = puzzle.ToCharArray(); //puzzle yani * karakterlerinden oluşan string değerini char dizisine çevirdik
            while(selectedWord.IndexOf(letter,startindex)!=-1)//son indexin -1 olduğunu hatırlayalım. Oraya kadar dönen bir while
            {
                int findinfIndex = selectedWord.IndexOf(letter, startindex);//bulma koşulu ile index numarasını buluyoruz
                puzzleStars[findinfIndex] = Convert.ToChar(letter); // eğer varsa çeviriyoruz
                startindex = findinfIndex+1;// devam edebilmek için başlangıç indeksini arttırıyoruz ki aynı veriyi kontrol etmesin
            }
            string result = string.Empty;//boş string ürettikten sonra puzzlestars bize bir dizi veriyor
            foreach(var item in puzzleStars)
            {
                result+= item.ToString();//bunu stringe çevirmek için foreach mantığı kullanıyoruz.
            }
            return result;
        }

        private static bool checkLetterInWord(string selectedWord, string letter)// random ile seçilen kelimede eşleşen harf var mı kontrol
        {
            return selectedWord.Contains(letter); //Contains ile aramada olumlu sonuç dönerse true dönüş sağlayacak
        }

        private static string replaceToStar(string selectedWord)
        {
            string puzzleResult = string.Empty;
            for(int i=0; i<selectedWord.Length; i++) //random seçilen kelime uzunluğunda ekrana yıldız yazdıracak method
            {
                puzzleResult += "*";
            }
            return puzzleResult;
        }

        static string chooseWord(string[] words) // verilen değerler arasında rastgele bir veri seçme
        {
            Random random = new Random();
            string word = words[random.Next(0,words.Length)];
            return word;
        }

        

    }
}
