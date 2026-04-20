Copyright: Jan Ochelski
Tytuł projektu: Przykład wzorca projektowego "Fasada" – zamówienia.
Opis projektu: Przykład wzorca projektowego polegającego na stworzeniu uproszczonego
interfejsu zajmującego się skomplikowanymi działaniami klas, frameworków lub bibliotek.
W tym przypadku FasadaZamowienia jest klasą zajmującą się obsługą zamówień. Wykonuje działania
na klasach takich jak SerwisMagazynu(), SerwisPlatnosci(), SerwisWysylki(). Dzięki temu interfejs użytkownika pozostaje 
prosty i nie jest obciążony szczegółami implementacyjnymi.
Metoda ZlozZamowienie(Instancja zamówienia) tej fasady wykonuje wszystkie potrzebne zadania obiektów Serwisów
dla danej instancji zamówienia bez konieczności ręcznego wywoływania wszystkich metod tych klas dla
tego zamówienia za każdym razem. Nie zastępuje ona tych serwisów, ale sama je wykorzystuje.
Jest ona jedynie przykładem niefunkcjonalnym
(nie wykonuje faktycznych działań), ma na celu ukazanie jedynie implementacji tego 
wzorca strukturalnego w danym frameworku.
