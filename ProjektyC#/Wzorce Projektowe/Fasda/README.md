# Fasada - Facade Design Pattern Example

--ENGLISH README--

1. Facade Design Pattern - Orders Management

2. Description
This is an example of the Facade design pattern that demonstrates the creation of a simplified interface that handles complex operations of classes, frameworks, or libraries.

In this case, `FasadaZamowienia` is a class that manages orders. It performs operations on classes such as `SerwisMagazynu()`, `SerwisPlatnosci()`, `SerwisWysylki()`. Thanks to this, the user interface remains simple and is not burdened with implementation details.

The `ZlozZamowienie(OrderInstance)` method of this facade performs all necessary tasks of Service objects for a given order instance without the need to manually call all methods of these classes for each order every time. It does not replace these services, but uses them.

This is only a non-functional example that does not perform actual operations but is intended to show only the implementation of this structural pattern in the given framework.

--POLISH README--

3. Wzorzec projektowy Fasada – zamówienia

4. Opis projektu
Przykład wzorca projektowego polegającego na stworzeniu uproszczonego interfejsu zajmującego się skomplikowanymi działaniami klas, frameworków lub bibliotek.

W tym przypadku `FasadaZamowienia` jest klasą zajmującą się obsługą zamówień. Wykonuje działania na klasach takich jak `SerwisMagazynu()`, `SerwisPlatnosci()`, `SerwisWysylki()`. Dzięki temu interfejs użytkownika pozostaje prosty i nie jest obciążony szczegółami implementacyjnymi.

Metoda `ZlozZamowienie(Instancja zamówienia)` tej fasady wykonuje wszystkie potrzebne zadania obiektów Serwisów dla danej instancji zamówienia bez konieczności ręcznego wywoływania wszystkich metod tych klas dla tego zamówienia za każdym razem. Nie zastępuje ona tych serwisów, ale sama je wykorzystuje.

Jest ona jedynie przykładem niefunkcjonalnym (nie wykonuje faktycznych działań), ma na celu ukazanie jedynie implementacji tego wzorca strukturalnego w danym frameworku.
