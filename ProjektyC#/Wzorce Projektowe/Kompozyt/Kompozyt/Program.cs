namespace Kompozyt
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Katalog root = new("root");

            Katalog katalogA = new("katalogA");
            root.AddEntry(katalogA);

            Katalog katalogB = new("katalogB");
            root.AddEntry(katalogB);

            ///////////////////////////////////////

            katalogA.AddEntry(new Plik("plikA1.txt", 1200));
            katalogA.AddEntry(new Plik("plikA2.txt", 800));

            katalogB.AddEntry(new Plik("plikB1.txt", 600));
            Katalog katalogB1 = new("katalogB1");
            katalogB.AddEntry(katalogB1);
            katalogB1.AddEntry(new Plik("plikB1_1.txt", 300));

            ///////////////////////////////////////

            Console.WriteLine(root.GetInfo(0));
        }
    }
}
