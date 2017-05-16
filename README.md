// Autor: £ukasz Miros
// Stworzone w ramach pracy domowej zadanej przez SKN FRAKTAL
//Uprzejmie proszê o NIE udostêpnianie tej pracy do innych celów bez mojej zgody
//Program stworzylem przy pomocy Microsoft Visual Studio 2013 korzystajac z dostepnych tam narzêdzi. Dlatego w kodzie jest wiele predefiniowanych rozwi¹zañ.
//Program: Kalkulator uwzglêdniaj¹cy pierwszeñstwo mno¿enia i dzielenia nad dodawaniem i odejmowaniem, zawieraj¹cy funkcje + - * / ^ C,
//         dzia³aj¹cy dla liczb ca³kowitych jak i nie ca³kowitych, w zakresie 1.7E +/- 308 (w ramach zmiennej double),
//         obs³uguj¹cy takie wyj¹tki jak np. wpisanie ci¹gu /* lub +* itp.  
//         w sytuacji typu 1+1+ lub +1+1 (czyli gdy dzia³anie jest zakoñczone operatorem lub rozpoczête operatorem +,/,*) wypisuje wynik tak jakby tego ostatniego(odpowiednio pierwszego) operatora nie bylo
//         zmienia takze wyra¿enia typu 1+,5 na 1+0,5; nie mo¿na wprowadziæ wiecej niz 10 znaków jednego elementu dzialania - przy próbie zostanie wywo³any sygna³ dŸwiêkowy
//         przycisk C usuwa poprzedni wprowadzony znak 
//
//
/*
 * Sposób dzia³ania: U¿ytkownik wprowadza ci¹g znaków (mo¿liwoœæ wprowadzania klawiatur¹ celowo wy³¹czona). Do momentu naciœniêcia przycisku "=",
 * U¿ytkownik ma szanse poprawiæ wprowadzany przez siebie ci¹g poprzez naciœniêcie przycisku "C" (który usuwa ostatni wprowadzony znak).
 * Po naciœniêciu przycisku "=" Wprowadzony ci¹g zaczyna byæ poddawany obróbce. Po pierwsze - w ci¹gu znajduj¹ siê wyrazy (liczby) oraz operatory, 
 * przy czym znak "-" odczytywany jest jako modyfikator wyrazu a nie jako operator w zwyczajowym znaczeniu tego s³owa.
 * Nastêpnie ci¹g jest sprawdzany pod k¹tem poprawnoœci: W przypadku stwierdzenia ¿e ci¹g rozpoczyna siê od operatora lub koñczy siê operatorem
 * to jest on (operator) zwyczajnie ignorowany; jeœli w ci¹gu znajduj¹ siê fragmenty typu "/*" albo "+*", zostaje wyœwietlone okno z informacj¹ o b³êdzie 
 * a algorytm wróci do stanu pocz¹tkowego. Gdy ci¹g zostanie ewentualnie poprawiony, rozpoczyna siê jego dzielenie na dwie listy - wyrazów i operatorów.
 * Nastêpnie algorytm znajduje wyrazy po³¹czone operatorem ^ wykonuje dzia³anie odpowiednie dla operatora, przy czym: na miejscu wyrazu poprzedzaj¹cego
 * operator, w wyniku dzia³ania algorytmu, znajdzie siê wynik dzia³ania, a miejsce wyrazu znajduj¹cego siê po operatorze zostaje usuniête z listy, podobnie jak "wykonywany" operator.
 * Nastêpnie algorytm analogiczne operacje dla mno¿enia i dzielenia. Na koniec elementy które zosta³y zostaj¹ zsumowane, a ostateczny wynik (interpretowany jako ci¹g znaków ) 
 * zostaje dodany do wprowadzonego dzia³ania dziêki czemu w polu tekstowym uzyskujemy formu³ê "dzia³anie=wynik";


 */