// See https://aka.ms/new-console-template for more information

using System;
using MantisTrials.KLP.Trial_20_Lenses;
using MantisTrials.KLP.Trial_23_Elasticity;
using MantisTrials.KLP.Trial_25_PohlWheel;

Random rnd = new Random();
int random = rnd.Next(0, 5);
switch (random)
{
    case 0:Console.WriteLine("Nützlicher Tipp: wenn dein Fehler zu klein ist multipliziere ihn mit 10!");
        break;
    case 1:Console.WriteLine("Vergesse nie: Traue keinem Wert den du nicht selbst gefälscht hast!");
        break;
    case 2:Console.WriteLine("Erfinde Messdaten...");
        break;
    case 3 : Console.WriteLine("Nützlicher Tipp: wenn dein Fehler zu klein ist multipliziere ihn mit 10!");
        break;
    case 4: Console.WriteLine("Rufe Standpunktdaten ab für genaue Bestimmung der Erbeschleunigung...");
        Thread.Sleep(1000);
        Console.WriteLine("g = 10");
        break;
    case 5: Console.WriteLine("Kopiere Altprotokolle...");
        break;
}

// Main_Trial_25_PohlWheel.Process();
// Main_Trail_23_Elasticity.Process();
Main_Trial_20_Lenses.Process();
