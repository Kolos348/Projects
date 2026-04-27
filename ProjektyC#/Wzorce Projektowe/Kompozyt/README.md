# Kompozyt - Composite Design Pattern Example

--ENGLISH README--

1. Title - Composite Design Pattern - File Tree Structure

2. Description:
This is an example of the Composite design pattern that demonstrates the creation of a tree structure from objects of classes that are individual objects.

In this case, it is a composite of a file tree structure. It enables the creation of a tree structure with objects of file and directory types.

3. Key Components:
- **Directory** – An object that can contain other directories or files and be part of the tree (branch)
- **File** – An object that can only be part of the tree and does not contain other objects (leaf)

Information about the tree can be displayed using the methods of the `IFileManager` interface. The `File` and `Directory` classes implement the `IFileManager` interface, which makes them part of the tree.

4. Interface Methods:
- **GetInfo(a)** – Displays the tree in the console from a given object, where a is an argument indicating indentation with space characters
- **GetSize()** – Returns the size in bytes of a file, or if it is a directory, returns the size of all objects inside the directory
- **GetName()** – Returns the name of the file or directory

5. Creating Instances:
- **File** class requires two arguments: file name (string) and number of bytes (int)
- **Directory** class requires one argument: directory name (string)
- **Directory** objects have the `AddEntry(a)` method which adds an object a to the given directory in the tree structure

This is a recursive function typical for the Composite pattern.

--POLISH README--

6. Tytuł - Wzorzec projektowy Kompozyt – drzewo plików

7. Opis projektu:

Przykład wzorca projektowego "Kompozyt" polegającego na stworzeniu struktury drzewiastej z obiektów klas będących osobnymi obiektami. W tym przypadku jest to kompozyt drzewa plików. Umożliwia stworzenie struktury drzewa z obiektów typu plik i katalog.

8. Kluczowe komponenty:
- **Katalog** – Obiekt, który może zawierać inne katalogi lub pliki w sobie i być częścią drzewa (gałąź)
- **Plik** – Obiekt mogący być częścią drzewa i nie zawierający w sobie innych obiektów (liść)

Informacje o drzewie można wyświetlić za pomocą metod interfejsu `IFileManager`. Klasy Plik i Katalog implementują interfejs `IFileManager`, co powoduje że są częścią drzewa.

9. Metody interfejsu:
- **GetInfo(a)** – Wyświetla drzewo w konsoli od danego obiektu, gdzie a jest argumentem oznaczającym odstęp znakami spacji
- **GetSize()** – Zwraca rozmiar w bajtach pliku, a jeśli jest to katalog, zwraca rozmiar wszystkich obiektów wewnątrz katalogu
- **GetName()** – Zwraca nazwę pliku lub katalogu

10. Tworzenie instancji:
- Klasa **Plik** wymaga dwóch argumentów: nazwy pliku (string) i ilości bajtów (int)
- Klasa **Katalog** wymaga jednego argumentu: nazwy katalogu (string)
- Obiekty klasy **Katalog** mają metodę `AddEntry(a)`, która dodaje do danego katalogu obiekt a w strukturze drzewa

Jest to funkcja rekurencyjna typowa dla wzorca Kompozyt.