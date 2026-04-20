using System;

namespace Fasda
{
    public class SerwisMagazynu
    {
        public bool MaStan(string kodProduktu, int ilosc) => ilosc <= 5;
        public void Zarezerwuj(string kodProduktu, int ilosc) => Console.WriteLine($"[Magazyn] Rezerwuję {ilosc} × {kodProduktu}");
    }
}
