using System;

namespace Fasda
{
    class Program
    {
        static void Main()
        {
            var fasada = new FasadaZamowien
            (
                new SerwisMagazynu(),
                new SerwisPlatnosci(),
                new SerwisWysylki()
            );

            var zamowienie = new Zamowienie(kodProduktu: "GPU-4090", ilosc: 2, kwota: 899.99m, adresDostawy: "ul. Fraktalna 1, 43-300 Bielsko-Biała");
            var sukces = fasada.ZlozZamowienie(zamowienie);

            Console.WriteLine(sukces ? "Sukces." : "Niepowodzenie.");
        }
    }
}
