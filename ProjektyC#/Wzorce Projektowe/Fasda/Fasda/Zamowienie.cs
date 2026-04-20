namespace Fasda
{
    public class Zamowienie
    {
        public string KodProduktu { get; }
        public int Ilosc { get; }
        public decimal Kwota { get; }
        public string AdresDostawy { get; }

        public Zamowienie(string kodProduktu, int ilosc, decimal kwota, string adresDostawy)
        {
            KodProduktu = kodProduktu;
            Ilosc = ilosc;
            Kwota = kwota;
            AdresDostawy = adresDostawy;
        }
    }
}
