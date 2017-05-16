// Autor: �ukasz Miros
// Stworzone w ramach pracy domowej zadanej przez SKN FRAKTAL
//Uprzejmie prosz� o NIE udost�pnianie tej pracy do innych cel�w bez mojej zgody
//Program stworzylem przy pomocy Microsoft Visual Studio 2013 korzystajac z dostepnych tam narz�dzi. Dlatego w kodzie jest wiele predefiniowanych rozwi�za�.
//Program: Kalkulator uwzgl�dniaj�cy pierwsze�stwo mno�enia i dzielenia nad dodawaniem i odejmowaniem, zawieraj�cy funkcje + - * / ^ C,
//         dzia�aj�cy dla liczb ca�kowitych jak i nie ca�kowitych, w zakresie 1.7E +/- 308 (w ramach zmiennej double),
//         obs�uguj�cy takie wyj�tki jak np. wpisanie ci�gu /* lub +* itp.  
//         w sytuacji typu 1+1+ lub +1+1 (czyli gdy dzia�anie jest zako�czone operatorem lub rozpocz�te operatorem +,/,*) wypisuje wynik tak jakby tego ostatniego(odpowiednio pierwszego) operatora nie bylo
//         zmienia takze wyra�enia typu 1+,5 na 1+0,5; nie mo�na wprowadzi� wiecej niz 10 znak�w jednego elementu dzialania - przy pr�bie zostanie wywo�any sygna� d�wi�kowy
//         przycisk C usuwa poprzedni wprowadzony znak 
//
//
/*
 * Spos�b dzia�ania: U�ytkownik wprowadza ci�g znak�w (mo�liwo�� wprowadzania klawiatur� celowo wy��czona). Do momentu naci�ni�cia przycisku "=",
 * U�ytkownik ma szanse poprawi� wprowadzany przez siebie ci�g poprzez naci�ni�cie przycisku "C" (kt�ry usuwa ostatni wprowadzony znak).
 * Po naci�ni�ciu przycisku "=" Wprowadzony ci�g zaczyna by� poddawany obr�bce. Po pierwsze - w ci�gu znajduj� si� wyrazy (liczby) oraz operatory, 
 * przy czym znak "-" odczytywany jest jako modyfikator wyrazu a nie jako operator w zwyczajowym znaczeniu tego s�owa.
 * Nast�pnie ci�g jest sprawdzany pod k�tem poprawno�ci: W przypadku stwierdzenia �e ci�g rozpoczyna si� od operatora lub ko�czy si� operatorem
 * to jest on (operator) zwyczajnie ignorowany; je�li w ci�gu znajduj� si� fragmenty typu "/*" albo "+*", zostaje wy�wietlone okno z informacj� o b��dzie 
 * a algorytm wr�ci do stanu pocz�tkowego. Gdy ci�g zostanie ewentualnie poprawiony, rozpoczyna si� jego dzielenie na dwie listy - wyraz�w i operator�w.
 * Nast�pnie algorytm znajduje wyrazy po��czone operatorem ^ wykonuje dzia�anie odpowiednie dla operatora, przy czym: na miejscu wyrazu poprzedzaj�cego
 * operator, w wyniku dzia�ania algorytmu, znajdzie si� wynik dzia�ania, a miejsce wyrazu znajduj�cego si� po operatorze zostaje usuni�te z listy, podobnie jak "wykonywany" operator.
 * Nast�pnie algorytm analogiczne operacje dla mno�enia i dzielenia. Na koniec elementy kt�re zosta�y zostaj� zsumowane, a ostateczny wynik (interpretowany jako ci�g znak�w ) 
 * zostaje dodany do wprowadzonego dzia�ania dzi�ki czemu w polu tekstowym uzyskujemy formu�� "dzia�anie=wynik";


 */