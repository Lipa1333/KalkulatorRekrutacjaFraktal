// Autor: Łukasz Miros
// Stworzone w ramach pracy domowej zadanej przez SKN FRAKTAL
//Uprzejmie proszę o NIE udostępnianie tej pracy do innych celów bez mojej zgody
//Program stworzylem przy pomocy Microsoft Visual Studio 2013 korzystajac z dostepnych tam narzędzi. Dlatego w kodzie jest wiele predefiniowanych rozwiązań.
//Program: Kalkulator uwzględniający pierwszeństwo mnożenia i dzielenia nad dodawaniem i odejmowaniem, zawierający funkcje + - * / ^ C,
//         działający dla liczb całkowitych jak i nie całkowitych, w zakresie 1.7E +/- 308 (w ramach zmiennej double),
//         obsługujący takie wyjątki jak np. wpisanie ciągu /* lub +* itp.  
//         w sytuacji typu 1+1+ lub +1+1 (czyli gdy działanie jest zakończone operatorem lub rozpoczęte operatorem +,/,*) wypisuje wynik tak jakby tego ostatniego(odpowiednio pierwszego) operatora nie bylo
//         zmienia takze wyrażenia typu 1+,5 na 1+0,5; nie można wprowadzić wiecej niz 10 znaków jednego elementu dzialania - przy próbie zostanie wywołany sygnał dźwiękowy
//         przycisk C usuwa poprzedni wprowadzony znak 
//
//
/*
 * Sposób działania: Użytkownik wprowadza ciąg znaków (możliwość wprowadzania klawiaturą celowo wyłączona). Do momentu naciśnięcia przycisku "=",
 * Użytkownik ma szanse poprawić wprowadzany przez siebie ciąg poprzez naciśnięcie przycisku "C" (który usuwa ostatni wprowadzony znak).
 * Po naciśnięciu przycisku "=" Wprowadzony ciąg zaczyna być poddawany obróbce. Po pierwsze - w ciągu znajdują się wyrazy (liczby) oraz operatory, 
 * przy czym znak "-" odczytywany jest jako modyfikator wyrazu a nie jako operator w zwyczajowym znaczeniu tego słowa.
 * Następnie ciąg jest sprawdzany pod kątem poprawności: W przypadku stwierdzenia że ciąg rozpoczyna się od operatora lub kończy się operatorem
 * to jest on (operator) zwyczajnie ignorowany; jeśli w ciągu znajdują się fragmenty typu "/*" albo "+*", zostaje wyświetlone okno z informacją o błędzie 
 * a algorytm wróci do stanu początkowego. Gdy ciąg zostanie ewentualnie poprawiony, rozpoczyna się jego dzielenie na dwie listy - wyrazów i operatorów.
 * Następnie algorytm znajduje wyrazy połączone operatorem ^ wykonuje działanie odpowiednie dla operatora, przy czym: na miejscu wyrazu poprzedzającego
 * operator, w wyniku działania algorytmu, znajdzie się wynik działania, a miejsce wyrazu znajdującego się po operatorze zostaje usunięte z listy, podobnie jak "wykonywany" operator.
 * Następnie algorytm analogiczne operacje dla mnożenia i dzielenia. Na koniec elementy które zostały zostają zsumowane, a ostateczny wynik (interpretowany jako ciąg znaków ) 
 * zostaje dodany do wprowadzonego działania dzięki czemu w polu tekstowym uzyskujemy formułę "działanie=wynik";


 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kalkulator1
{
    public partial class Form1 : Form
    {
        double Wynik=0;     //Pomocnicza uzywana w funkcji do obliczania ostatecznego wyniku
        double OstatecznyWynik=0;    // ostateczny wynik wprowadzonych dziala
        string WprowadzanyCiag="";   // przechowuje wprowadzany ciag
        string AktualniePobieranyWyraz = "";   // uzywany do rozdzielanina poszczegolnych wyrazow w ciagu
        int i = 0;       // zmienna pomocnicza
        int s=0;         // zmienna pomocnicza uzywana przy liczeniu powtarzajacych sie operatorow
        int liczbaszkodnikow = 0;     //zmienna pomocnicza: liczba powtorzonych operatorow po sobie
        string WprowadzanyPODCiag="0";   //ilosc wprowadzonych znkow jednego wyrazu
        string WyswietlanyCiag="";    // informacja wyswietlana uytkownikowi, o tym co wprowadzil
    
        

        List<double> Wyrazy = new List<double>();     // przechowuje wszystkie liczby
        List<char> Operatory = new List<char>();     // przechowuje wsystkie operatory oprocz -

        double Liczenie (string AktualnieBadanyCiag)
        {
            //funkcja ktora przyjmuje za argument poprawny ciag znakow i wykonuje wlasciwe obliczenia

            for (int p = 0; p < Operatory.Count; p++)
            {
                if (Operatory[p] == '^')
                {
                    Wyrazy[p + 1] = Math.Pow(Wyrazy[p], Wyrazy[p + 1]);

                    Wyrazy.RemoveAt(p);
                    Operatory.RemoveAt(p);
                    p--;
                }
            }
           

            for (int p = 0; p < Operatory.Count; p++)
            {
                if (Operatory[p] == '/')
                {

                    if (Wyrazy[p + 1] == 0)
                    {
                        MessageBox.Show("Wykryto próbę dzielenia przez 0");
                        return 0;
                    }
                    else
                    {
                        Wyrazy[p + 1] = Wyrazy[p] / Wyrazy[p + 1];
                        Wyrazy.RemoveAt(p);
                        Operatory.RemoveAt(p);
                        p--;
                    }
                }
            }

            for (int p = 0; p < Operatory.Count; p++)
            {
                if (Operatory[p] == '*')
                {
                    Wyrazy[p + 1] = Wyrazy[p] * Wyrazy[p + 1];
                    Wyrazy.RemoveAt(p);
                    Operatory.RemoveAt(p);
                    p--;
                }
            }

            for (int p = 0; p < Wyrazy.Count; p++)
            {
                Wynik += Wyrazy[p];
            }
            return Wynik;
            //koniec funkcji
        }

        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
          
        }

        private void Przycisk1_Click(object sender, EventArgs e)
        {
            if (WprowadzanyPODCiag.Length != 10)
            {
                WprowadzanyPODCiag += "1";
                WprowadzanyCiag += "1";
                WyswietlanyCiag += "1";
                textBox1.Text = WyswietlanyCiag;
            }
            else
            {
                System.Media.SystemSounds.Beep.Play();
            }
            
        }

        private void Przycisk2_Click(object sender, EventArgs e)
        {
            if (WprowadzanyPODCiag.Length != 10)
            {
                WprowadzanyPODCiag += "2";
                WprowadzanyCiag += "2";
                WyswietlanyCiag += "2";
                textBox1.Text = WyswietlanyCiag;
            }
            else
            {
                System.Media.SystemSounds.Beep.Play();
            }
        }

        private void Przycisk3_Click(object sender, EventArgs e)
        {
            if (WprowadzanyPODCiag.Length != 10)
            {
                WprowadzanyPODCiag += "3";
                WprowadzanyCiag += "3";
                WyswietlanyCiag += "3";
                textBox1.Text = WyswietlanyCiag;
            }
            else
            {
                System.Media.SystemSounds.Beep.Play();
            }
        }

        private void Przycisk4_Click(object sender, EventArgs e)
        {
            if (WprowadzanyPODCiag.Length != 10)
            {
                WprowadzanyPODCiag += "4";
                WprowadzanyCiag += "4";
                WyswietlanyCiag += "4";
                textBox1.Text = WyswietlanyCiag;
            }
            else
            {
                System.Media.SystemSounds.Beep.Play();
            }
        }

        private void Przycisk5_Click(object sender, EventArgs e)
        {
            if (WprowadzanyPODCiag.Length != 10)
            {
                WprowadzanyPODCiag += "5";
                WprowadzanyCiag += "5";
                WyswietlanyCiag += "5";
                textBox1.Text = WyswietlanyCiag;
            }
            else
            {
                System.Media.SystemSounds.Beep.Play();
            }
        }

        private void Przycisk6_Click(object sender, EventArgs e)
        {
            if (WprowadzanyPODCiag.Length != 10)
            {
                WprowadzanyPODCiag += "6";
                WprowadzanyCiag += "6";
                WyswietlanyCiag += "6";
                textBox1.Text = WyswietlanyCiag;
            }
            else
            {
                System.Media.SystemSounds.Beep.Play();
            }
        }

        private void Przycisk7_Click(object sender, EventArgs e)
        {
            if (WprowadzanyPODCiag.Length != 10)
            {
                WprowadzanyPODCiag += "7";
                WprowadzanyCiag += "7";
                WyswietlanyCiag += "7";
                textBox1.Text = WyswietlanyCiag;
            }
            else
            {
                System.Media.SystemSounds.Beep.Play();
            }
        }

        private void Przycisk8_Click(object sender, EventArgs e)
        {
            if (WprowadzanyPODCiag.Length != 10)
            {
                WprowadzanyPODCiag += "8";
                WprowadzanyCiag += "8";
                WyswietlanyCiag += "8";
                textBox1.Text = WyswietlanyCiag;
            }
            else
            {
                System.Media.SystemSounds.Beep.Play();
            }
        }

        private void Przycisk9_Click(object sender, EventArgs e)
        {
            if (WprowadzanyPODCiag.Length != 10)
            {
                WprowadzanyPODCiag += "9";
                WprowadzanyCiag += "9";
                WyswietlanyCiag += "9";
                textBox1.Text = WyswietlanyCiag;
            }
            else
            {
                System.Media.SystemSounds.Beep.Play();
            }
        }

        private void PrzyciskRownaSie_Click(object sender, EventArgs e)
        {
            if (WprowadzanyCiag == "")
            {
                ;   // jezeli nic nie wprowadzono to rowniez nic nie powinno sie wydarzyc po nacisnieciu "="
            }
            else
            {
                //usuwanie zbednych operatorow na koncu
                while (WprowadzanyCiag[WprowadzanyCiag.Length - 1] == '+' || WprowadzanyCiag[WprowadzanyCiag.Length - 1] == '-' || WprowadzanyCiag[WprowadzanyCiag.Length - 1] == '*' || WprowadzanyCiag[WprowadzanyCiag.Length - 1] == '/' || WprowadzanyCiag[WprowadzanyCiag.Length - 1] == '^' )
                {
                    WprowadzanyCiag = WprowadzanyCiag.Remove(WprowadzanyCiag.Length - 1, 1);
                }
                WprowadzanyCiag += "k";

                //najpierw trzeba poprawic caly ciag. 
                //usuwanie niepoprawnych poczatkow dzialan
                for (int l = 0; l < WprowadzanyCiag.Length-1; l++)
                {
                    if (l == 0 && (WprowadzanyCiag[l] == '+' || WprowadzanyCiag[l] == '*' || WprowadzanyCiag[l] == '/' || WprowadzanyCiag[l] == '^' ))                         // najpierw odddzielny warunek dla poczatku ciagu - aby nie bylo w nim czegos takiego jak +3+3= itp
                    {
                        WprowadzanyCiag = WprowadzanyCiag.Remove(0, 1);
                    }
                    //usuwanie podwojonych znakow
                    if (WprowadzanyCiag[l]=='+'&&WprowadzanyCiag[l+1]=='+')
                    {
                        s = l + 1;
                        liczbaszkodnikow = 0;
                        while (WprowadzanyCiag[s]=='+')
                        {
                            liczbaszkodnikow++;
                            s++;
                        }
                       WprowadzanyCiag =  WprowadzanyCiag.Remove(l + 1, liczbaszkodnikow);
                       
                    }
                    if (WprowadzanyCiag[l] == '-' && WprowadzanyCiag[l + 1] == '-')
                    {
                        s = l;
                        liczbaszkodnikow = 0;
                        while (WprowadzanyCiag[s] == '-')
                        {
                            
                            liczbaszkodnikow++;
                            s++;
                        }
                        if (liczbaszkodnikow%2==1)
                        {
                            WprowadzanyCiag = WprowadzanyCiag.Remove(l +1, liczbaszkodnikow-1);
                        }
                        if (liczbaszkodnikow % 2 == 0)
                        {
                            WprowadzanyCiag = WprowadzanyCiag.Remove(l , liczbaszkodnikow);
                        }
                        
                        
                    }
                    if (WprowadzanyCiag[l] == '*' && WprowadzanyCiag[l + 1] == '*')
                    {
                        s = l + 1;
                        liczbaszkodnikow = 0;
                        while (WprowadzanyCiag[s] == '*')
                        {

                            liczbaszkodnikow++;
                            s++;
                        }
                        WprowadzanyCiag = WprowadzanyCiag.Remove(l + 1, liczbaszkodnikow);

                    }
                    if (WprowadzanyCiag[l] == '/' && WprowadzanyCiag[l + 1] == '/')
                    {
                        s = l + 1;
                        liczbaszkodnikow = 0;
                        while (WprowadzanyCiag[s] == '/')
                        {
                            liczbaszkodnikow++;
                            s++;
                        }
                        WprowadzanyCiag = WprowadzanyCiag.Remove(l + 1, liczbaszkodnikow);

                    }
                    if (WprowadzanyCiag[l] == '^' && WprowadzanyCiag[l + 1] == '^')
                    {
                        s = l + 1;
                        liczbaszkodnikow = 0;
                        while (WprowadzanyCiag[s] == '^')
                        {
                            liczbaszkodnikow++;
                            s++;
                        }
                        WprowadzanyCiag = WprowadzanyCiag.Remove(l + 1, liczbaszkodnikow);

                    }
                   
                    //koniec usuwania podwojonych znakow
                    //usuwanie niedozwolonych znakow typu -+ albo +* /* */ +/ 
                    //tabela sluzy do sprawdzenia ktore kombinacje operatorow sa niepoprawne
                    //
                    //   +  |  -  |  *  |  /  |  
                    //+|    |     | +*  | +/  |       
                    //-| -+ |     | -*  | -/  |     
                    //*| *+ |     |     |*/   |     
                    //p| /+ |     | /*  |     |      
                    //
                    if (WprowadzanyCiag[l] == '+' && WprowadzanyCiag[l + 1] == '*')   
                    {
                        textBox1.Text = WprowadzanyCiag;
                        //wyswietlenie bledu
                        MessageBox.Show("Wprowadzono niepoprawny ciag znaków: +*");
                        Zakonczenie();
                    } if (WprowadzanyCiag[l] == '+' && WprowadzanyCiag[l + 1] == '/')  
                    {
                        textBox1.Text = WprowadzanyCiag;
                        //wyswietlenie bledu
                        MessageBox.Show("Wprowadzono niepoprawny ciag znaków: +/");
                        Zakonczenie();
                    }
                    if (WprowadzanyCiag[l] == '-' && WprowadzanyCiag[l + 1] == '+')   /////////
                    {
                        textBox1.Text = WprowadzanyCiag;
                        //wyswietlenie bledu
                        MessageBox.Show("Wprowadzono niepoprawny ciag znaków: -+");
                        Zakonczenie();
                    }
                    if (WprowadzanyCiag[l] == '-' && WprowadzanyCiag[l + 1] == '*')   /////////
                    {
                        textBox1.Text = WprowadzanyCiag;
                        //wyswietlenie bledu
                        MessageBox.Show("Wprowadzono niepoprawny ciag znaków: -*");
                        Zakonczenie();
                    }
                    if (WprowadzanyCiag[l] == '-' && WprowadzanyCiag[l + 1] == '/')   /////////
                    {
                        textBox1.Text = WprowadzanyCiag;
                        //wyswietlenie bledu
                        MessageBox.Show("Wprowadzono niepoprawny ciag znaków: -/");
                        Zakonczenie();
                    }
                    if (WprowadzanyCiag[l] == '*' && WprowadzanyCiag[l + 1] == '+')   /////////
                    {
                        textBox1.Text = WprowadzanyCiag;
                        //wyswietlenie bledu
                        MessageBox.Show("Wprowadzono niepoprawny ciag znaków: *+");
                        Zakonczenie();
                    }
                    if (WprowadzanyCiag[l] == '*' && WprowadzanyCiag[l + 1] == '/')   /////////
                    {
                        textBox1.Text = WprowadzanyCiag;
                        //wyswietlenie bledu
                        MessageBox.Show("Wprowadzono niepoprawny ciag znaków: */");
                        Zakonczenie();
                    }
                    if (WprowadzanyCiag[l] == '/' && WprowadzanyCiag[l + 1] == '+')   /////////
                    {
                        textBox1.Text = WprowadzanyCiag;
                        //wyswietlenie bledu
                        MessageBox.Show("Wprowadzono niepoprawny ciag znaków: /+");
                        Zakonczenie();
                    }
                    if (WprowadzanyCiag[l] == '/' && WprowadzanyCiag[l + 1] == '*')   /////////
                    {
                        textBox1.Text = WprowadzanyCiag;
                        //wyswietlenie bledu
                        MessageBox.Show("Wprowadzono niepoprawny ciag znaków: /*");
                        Zakonczenie();
                    }
                    //dodane funkcje : potega 
                    //   +  |  -  |  *  |  /  |  dod |^ |  
                    //+|    |     | +*  | +/  |      |+^|
                    //-| -+ |     | -*  | -/  |      |-^|
                    //*| *+ |     |     |*/   |      |*^|
                    //p| /+ |     | /*  |     |      |/^|
                    //_________________________      |  |
                    //^|  ^+|     |  ^* | ^/  |      |  |
                  
                    
                    if ((WprowadzanyCiag[l] == '+' || WprowadzanyCiag[l] == '-' || WprowadzanyCiag[l] == '*' || WprowadzanyCiag[l] == '/') && (WprowadzanyCiag[l + 1] == '^'))   /////////
                    {
                        textBox1.Text = WprowadzanyCiag;
                        //wyswietlenie bledu
                        MessageBox.Show("Wprowadzono niepoprawny ciag znaków przy potędze ");
                        Zakonczenie();
                    }
                    if ((WprowadzanyCiag[l] == '^') && (WprowadzanyCiag[l + 1] == '+' || WprowadzanyCiag[l + 1] == '*'||WprowadzanyCiag[l+1]=='/'))   /////////
                    {
                        textBox1.Text = WprowadzanyCiag;
                        //wyswietlenie bledu
                        MessageBox.Show("Wprowadzono niepoprawny ciag znaków przy potędze ");
                        Zakonczenie();
                    }
                    
                  
                }



                //koniec poprawianie ciagu. ciag jest w tym miejscu juz poprawny
                while (i<=WprowadzanyCiag.Length-1)// podzial miedzy operatory a elementy
                {
                    //dopoki nie napotka na operator kontynuuje odczytywanie jako wyraz
                    if (WprowadzanyCiag[i] != '+' && WprowadzanyCiag[i] != '*' && WprowadzanyCiag[i] != '/' && WprowadzanyCiag[i] != '^' )    // || != /*-
                    {
                        AktualniePobieranyWyraz += WprowadzanyCiag[i];
                        i++;
                    }
                    
                    if (WprowadzanyCiag[i]=='+')    //jezeli osiagnie operator lub jezeli osiagnie koniec
                    {
                        
                        Wyrazy.Add(Convert.ToDouble(AktualniePobieranyWyraz));  
                        Operatory.Add('+');
                        AktualniePobieranyWyraz = "";
                        i++;
                    }
                    if (WprowadzanyCiag[i] == '*')    //jezeli osiagnie operator lub jezeli osiagnie koniec
                    {

                        Wyrazy.Add(Convert.ToDouble(AktualniePobieranyWyraz));
                        Operatory.Add('*');
                        AktualniePobieranyWyraz = "";
                        i++;
                    }
                     if (WprowadzanyCiag[i] == '/')    //jezeli osiagnie operator lub jezeli osiagnie koniec
                    {
                           
                            Wyrazy.Add(Convert.ToDouble(AktualniePobieranyWyraz));
                            Operatory.Add('/');
                            AktualniePobieranyWyraz = "";
                            i++;
                    }
                     if (WprowadzanyCiag[i] == '^')    //jezeli osiagnie operator lub jezeli osiagnie koniec
                     {

                         Wyrazy.Add(Convert.ToDouble(AktualniePobieranyWyraz));
                         Operatory.Add('^');
                         AktualniePobieranyWyraz = "";
                         i++;
                     }
                     

                    if (WprowadzanyCiag[i] == 'k')
                    {
                        Wyrazy.Add(Convert.ToDouble(AktualniePobieranyWyraz));
                        AktualniePobieranyWyraz = "";
                        i++;
                    }
                }
                // obliczanie wyniku;
               


                OstatecznyWynik = Liczenie(WprowadzanyCiag);
                
                
                
                //wypisanie wyniku
                WprowadzanyCiag = WprowadzanyCiag.Remove(WprowadzanyCiag.Length-1,1);
                WyswietlanyCiag += "=";
                textBox1.Text =WyswietlanyCiag+ OstatecznyWynik.ToString();
                

                //sekwencja zakonczenia 
                Zakonczenie();
            }
        }
        private void Zakonczenie()
        {
            Wynik = 0;
            OstatecznyWynik = 0;
            WprowadzanyCiag = "";
            AktualniePobieranyWyraz = "";
            i = 0;
            s = 0;
            liczbaszkodnikow = 0;
            WprowadzanyPODCiag = "0";
            WyswietlanyCiag = "";
            Wyrazy.Clear();
            Operatory.Clear();
        }
        private void PrzyciskDodac_Click(object sender, EventArgs e)
        {
                WprowadzanyPODCiag = "";
                WprowadzanyCiag += "+";
                WyswietlanyCiag += "+";
                textBox1.Text = WyswietlanyCiag;
        }

        private void PrzyciskMinus_Click(object sender, EventArgs e)
        {
            //konieczne  warunki, potrzebne aby zachowac czytelnosc ciagu i odczytac "-" jako modyfikator
            WprowadzanyPODCiag = "";
            if (WprowadzanyCiag=="")
            {
                WprowadzanyCiag += "-";
                WyswietlanyCiag += "-";
            }
            else if (WprowadzanyCiag[WprowadzanyCiag.Length - 1] == '-' || WprowadzanyCiag[WprowadzanyCiag.Length - 1] == '+' || WprowadzanyCiag[WprowadzanyCiag.Length - 1] == '*' || WprowadzanyCiag[WprowadzanyCiag.Length - 1] == '/' || WprowadzanyCiag[WprowadzanyCiag.Length - 1] == '^')
            {
                WprowadzanyCiag += "-";
                WyswietlanyCiag += "-";
            }
            else
            {
                WprowadzanyCiag += "+-";
                WyswietlanyCiag += "-";
            }
            textBox1.Text = WyswietlanyCiag;
        }

        private void PrzyciskPomnoz_Click(object sender, EventArgs e)
        {
            WprowadzanyPODCiag = "";
            WprowadzanyCiag += "*";
            WyswietlanyCiag += "*";
            textBox1.Text = WyswietlanyCiag;
        }

        private void PrzyciskPodziel_Click(object sender, EventArgs e)
        {
            WprowadzanyPODCiag = "";
            WprowadzanyCiag += "/";
             WyswietlanyCiag += "/";
             textBox1.Text = WyswietlanyCiag;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Przycisk0_Click(object sender, EventArgs e)
        {
            if (WprowadzanyPODCiag.Length != 10)
            {
                WprowadzanyPODCiag += "0";
                WprowadzanyCiag += "0";
                WyswietlanyCiag += "0";
                textBox1.Text = WyswietlanyCiag;
             
            }
            else
            {
                System.Media.SystemSounds.Beep.Play();
            }
        }

        private void PrzyciskPrzecinek_Click(object sender, EventArgs e)
        {
            if (WprowadzanyPODCiag==""||WprowadzanyPODCiag=="-")
            {
           
                WprowadzanyCiag += "0,";
                WyswietlanyCiag += "0,";
            }

            else
            {
                WyswietlanyCiag += ",";
                WprowadzanyCiag += ",";
            }
            
            WprowadzanyPODCiag +=",";

            textBox1.Text = WyswietlanyCiag;
        }

        private void PrzyciskC_Click(object sender, EventArgs e)
        {
            if (WprowadzanyCiag == "")
            {
                ;
            }
            else
            {
                WyswietlanyCiag = WyswietlanyCiag.Remove(WyswietlanyCiag.Length - 1, 1);
                WprowadzanyCiag = WprowadzanyCiag.Remove(WprowadzanyCiag.Length - 1, 1);
                WprowadzanyPODCiag = WprowadzanyPODCiag.Remove(WprowadzanyPODCiag.Length - 1, 1);
                textBox1.Text = WyswietlanyCiag;
            }
        }

        private void PrzyciskDoPotegi_Click(object sender, EventArgs e)
        {
            WprowadzanyPODCiag = "";
            WprowadzanyCiag += "^";
            WyswietlanyCiag += "^";
            textBox1.Text = WyswietlanyCiag;
        }

       
       
    }
}
