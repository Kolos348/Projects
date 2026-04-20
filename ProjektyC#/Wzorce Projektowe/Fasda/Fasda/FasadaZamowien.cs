using System;

namespace Fasda
{
    public class FasadaZamowien
    {
        private readonly SerwisMagazynu _magazyn;
        private readonly SerwisPlatnosci _platnosci;
        private readonly SerwisWysylki _wysylka;

        public FasadaZamowien(SerwisMagazynu magazyn, SerwisPlatnosci platnosci, SerwisWysylki wysylka)
        {
            _magazyn = magazyn;
            _platnosci = platnosci;
            _wysylka = wysylka;
        }

        public bool ZlozZamowienie(Zamowienie zamowienie)
        {
            if (!_magazyn.MaStan(zamowienie.KodProduktu, zamowienie.Ilosc))
            {
                Console.WriteLine("[Fasada] Brak towaru.");
                return false;
            }

            if (!_platnosci.Obciaz(zamowienie.Kwota))
            {
                Console.WriteLine("[Fasada] Płatność odrzucona.");
                return false;
            }

            _magazyn.Zarezerwuj(zamowienie.KodProduktu, zamowienie.Ilosc);
            var numer = _wysylka.UtworzPrzesylke(zamowienie.AdresDostawy);
            Console.WriteLine($"[Fasada] Zamówienie przyjęte. Numer przesyłki: {numer}");
            return true;
        }
    }
}
