Copyright: Jan Ochelski
Tytuł projektu: Przykład wzorca projektowego "Kompozyt" – drzewo plików
Opis projektu: Przykład wzorca projektowego "Kompozyt" polegającego na stworzeniu struktury 
drzewiastej z obiektów klas będących osobnymi obiektami. 
W tym przypadku jest to kompozyt drzewa plików. 
Umożliwia stworzenie struktury drzewa z obiektów typu plik i katalog. 
Katalog jest obiektem, który może zawierać inne katalogi lub pliki w sobie i być częścią drzewa. 
Natomiast plik jest jedynie obiektem mogącym być częścią drzewa i nie zawiera w sobie innych obiektów. 
W analogii drzewa katalog byłby gałęzią, a plik liściem.
Informacje o drzewie można wyświetlić za pomocą metod interfejsu IFileManager. 
Klasy Plik i Katalog implementują interfejs IFileManager, co powoduje że są częścią drzewa.
Metody interfejsu: 
GetInfo(a) – wyświetla drzewo w konsoli od danego obiektu, gdzie a jest argumentem oznaczającym 
odstęp znakami spacji. 
GetSize() – zwraca rozmiar w bajtach pliku, a jeśli jest to katalog, zwraca rozmiar 
wszystkich obiektów wewnątrz katalogu. GetName() – zwraca nazwę pliku lub katalogu.
Stworzenie instancji klasy Plik wymaga dwóch argumentów: nazwy pliku typu string i ilości bajtów typu int. 
Stworzenie instancji klasy Katalog wymaga jednego argumentu: nazwy katalogu typu string. 
Obiekty klasy Katalog mają metodę AddEntry(a), która dodaje do danego katalogu obiekt a w strukturze drzewa. 
Jest to funkcja rekurencyjna typowa dla wzorca Kompozyt.