using System;

namespace Fasda
{
    public class SerwisWysylki
    {
        public string UtworzPrzesylke(string adres) => $"PRZ-{Guid.NewGuid():N}".ToUpper();
    }
}
