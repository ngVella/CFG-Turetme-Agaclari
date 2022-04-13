using System;
using System.Collections.Generic;
using System.Linq;


namespace CFG_Uretme_Agaclari
{
    class Program
    {
        static string[][] rules = new string[3][];
        static List<string> Exp = new();
        static List<string> tempExp = new();

        static public bool IsTerminal(string test) // Gelen string terminal mi bunu kontrol ediyoruz
        {
            char[] charArr = test.ToCharArray(); // stringi arraya dönüştürüyoruz
            for (int k = 0; k < charArr.Length; k++)
            {
                if (charArr[k] == 'X')// index X ise termal değil
                {
                    return false;
                }
            }
            return true; // Hiç X yok o halde terminal
        }

        static public void FirstReplace()//Ilk kelimelerimizi oluşturuyoruz
        {
            // ****************** İLK ADIM KELİMELERİ OLUŞTURUYORUZ
            string temp, buffer;
            char[] charArr;
            for (int i = 0; i < rules[1].Length; i++) // Rules[1] (yani S) sırayla dönüyoruz
            {
                temp = rules[1][i]; // S'nin string elemanını tempe atıyoruz
                charArr = temp.ToCharArray(); // S'nin string elemanını char dizisine dönüştrüyoruz

                for (int k = 0; k < temp.Length; k++) // dizinin indexlerini sırayla dönüyoruz
                {
                    if (charArr[k] == 'X') // index X ise
                    {
                        charArr[k] = 'L'; // Replace yapmak için X'i L'ye dönüştürüyoruz
                        buffer = new string(charArr); // Dönüştürmenin ardından buffera char dizisini string olarak atıyoruz ki replace yapabilelim
                        for (int x = 0; x < rules[2].Length; x++) // artık kelime değiştirme sırası
                        {
                            buffer = buffer.Replace("L", rules[2][x]); // Sırayla L'nin yerine X'de bulunan stringleri yerleştiriyoruz
                            tempExp.Add(buffer);    //oluşan kelimeyi tempe atıyoruz
                            buffer = new string(charArr); // bufferi X'in diger elemanlarıyle yeniden replace yapabilmek için tekrar string dönüştüryoruz

                        }
                        charArr[k] = 'X'; // İleride yeniden replace yapcagığımız için L yaptıgımızindexi tekrar X yapıyoruz
                    }
                }
            }
            //**********************
        }

        static public void ReplaceMore(List<string> nonTerminal) // yukardaki methodu tekrar kullanabilir halde düzenliyoruz
        {
            string temp, buffer;
            char[] charArr;
            for (int i = 0; i < nonTerminal.Count; i++) // gelen listenin uzunlupu kadar dönen for
            {
                temp = nonTerminal[i];      // tempe kelimemizi atıyoruz
                charArr = temp.ToCharArray();   // gelimeyei char dizisine dönüştürüyoruz

                for (int k = 0; k < temp.Length; k++)   // daha sonra kelimenin uzunluğu kadar bir for dmngüsü kullanıyoruz
                {
                    if (charArr[k] == 'X')  // eger k indexi X karakteriyse
                    {
                        charArr[k] = 'L';   // replace yapmak için X'i L' ye dönüştürüyoruz
                        buffer = new string(charArr);   // char dizisini stringe dönüştürüyoruz
                        for (int x = 0; x < rules[2].Length; x++)   // daha sonra terminal kümemizin uzunluğu kadar döenen bir for (Yani X--> ...)
                        {
                            buffer = buffer.Replace("L", rules[2][x]);  // L yerine kelimeleri koyuyoruz
                            tempExp.Add(buffer);        // lesteye atıyoruz
                            buffer = new string(charArr);   // bufferi X'in diger elemanlarıyle yeniden replace yapabilmek için tekrar string dönüştüryoruz

                        }
                        charArr[k] = 'X'; //L'yi 'Xe dönüştüyürouz
                        tempExp.RemoveAt(0);   // kelimeyi listeden çıkarıyoruz
                    }
                }
            }
        }

        static public void Check(string[] last, bool control) // tekrar eden kelimeleri arayıp eleyip ekrana bastıran method
        {
            for (int i = 0; i < last.Length; i++) // gelen dizinin uzunluğu kadar olan for
            {
                control = false;    // kontorol değişkenini false yapıyoruz
                for (int j = i + 1; j < last.Length; j++) // yukardaki for-un index olarak bir ilerisinden ilerleyen for
                {
                    if (last[i] == last[j]) // eger ard arda iki index birbirine eşitse
                    {
                        last[j] = "NO NEED";   // ikinci görüdüğümüz yerdeki kelimeyi NO NEED stringi ile değiştiriyoruz bu sayede dizimizde tekra reden kelime sayısı azalıyor
                        control = true;     // kontrolu true yapıyoruz
                    }
                }
                if (control == false) // Eger kontrol hiç true olmadıysa o kelime hiç tekrar etmemiştir dolayısıyla ekrana basamya gerek yoktur onuda NO NEED ile değiştiriyoruz
                    last[i] = "NO NEED";
            }
            for (int i = 0; i < last.Length; i++)
            {
                if (!last[i].Equals("NO NEED"))     // NO NEED' e eşit olmayan kelimeleler tekrar enden kelimelerdir ekrana bastırılabilir
                    Console.WriteLine(last[i]);
            }
        }

        static void Main(string[] args)
        {

            Console.Write("Lütfen Alfabeyi girin: {");
            string alphabete = Console.ReadLine();
            Console.Write("Lütfen S İfaseini girin S-->");
            string sExp = Console.ReadLine();
            Console.Write("Lütfen X İfaseini girin X-->");
            string xExp = Console.ReadLine();

            //-----SPLIT-----
            string[] _alphabete = alphabete.Split(','); //Splitlediğimiz elemanları global değişkenlere atıyoruz methodlarda da kullanabilmek için
            rules[0] = _alphabete;

            string[] _sExp = sExp.Split('|');
            rules[1] = _sExp;

            string[] _xExp = xExp.Split('|');
            rules[2] = _xExp;
            //-----SPLIT-----

            FirstReplace(); //ilk kelime oluşturma

            int count = 0, tempExpCount = tempExp.Count;
            string temp;

            foreach (var item in _sExp) // S--> elemanlarında terminal varsa direk Exp listesine ekrana yazdırıyoruz çünkü onlar ağacın ilk kelimeleri
            {
                if (IsTerminal(item))
                    Exp.Add(item);
            }

            while (tempExp.Count != 0) //geçici oalrak kullanıdığımız dizideki elemanlar bitene kadar devame den while
            {
                count = 0;
                tempExpCount = tempExp.Count;   // count sayısı eleman eklem sırasında değiştiğinde hata aldığım için bir değişkene atadım
                while (true)    // sonsuz while
                {
                    if (count == tempExpCount)  // eğer sayaç ile tempExpCount eşit olursa döngüyü kırıcaz
                        break;                  // bunun sebebi şudur. Count 0dan başlayıp artıyor, tempExpCount ise azalıyor. listeden her eleman çıkardığımızda tempExpCount azalıcak,
                                                //  count artıcak, eşit olduklarında listenin sonuna geldiğimiz anlaşılacak ve döngü kırılacak

                    temp = tempExp[count]; // kelimenin terminalliğini kontrol ediyoruz
                    if (IsTerminal(temp) == true) // eğer terminalse
                    {
                        Exp.Add(temp); // kelimeyi listeye atıyoruz ve temp listeden çıkartıyourz
                        tempExp.RemoveAt(count);
                        count--;    // listeden elemanı çıkardığımız için count var tempExpCount 1 azalıyor
                        tempExpCount--;
                    }
                    count++; // sonrakine lemana geçiyoruz
                }
                ReplaceMore(tempExp); // temp exp kontrol için tekrar çağırıyoruz
            }

            Console.WriteLine("--Uretilen Kelimeler--");
            string[] last = Exp.ToArray();
            last = last.Distinct().ToArray(); // ekrana bir kez bastırmak için elemanları ayırıyoruz
            Array.ForEach(last, v => Console.WriteLine(v));

            Console.WriteLine("\n--Tekrar Eden Kelimeler--");
            last = Exp.ToArray();
            bool control = false;
            Check(last, control); // tekrar eden kelimeleri kontrol eden method
        }
    }
}
