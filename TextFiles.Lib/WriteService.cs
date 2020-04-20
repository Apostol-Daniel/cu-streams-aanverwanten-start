using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TextFiles.Lib
{
    public class WriteService
    {
        /// <summary>
        /// Schrijft een tekst weg naar een plaats op de harde schijf of in een netwerkmap
        /// </summary>
        /// <param name="tekst">De string-variabele die weggeschreven moet worden</param>
        /// <param name="bestandsMap">Plaats van het weg te schrijven bestand</param>
        /// <param name="bestandsNaam">Naam van het weg te schrijven bestand</param>
        /// <returns>boolean die aanduidt of het gelukt is om het bestand op te slaan</returns>
        public bool StringToTextFile(string tekst, string bestandsMap, string bestandsNaam)
        {
            bool isSuccesvolWeggeschreven;
            string bestandsPad;
            bestandsPad = bestandsMap + "\\" + bestandsNaam;

            try
            {
                // Er wordt een instance aangemaakt van de StreamWriter-class
                using (StreamWriter sw = new StreamWriter(bestandsPad))
                {
                    sw.Write(tekst);
                    sw.Close();
                }
                // na het using statement wordt de StreamWriter gesloten en wordt het geheugen vrijgegeven.
                isSuccesvolWeggeschreven = true;
            }
            catch (IOException)
            {
                //Soms zijn bestanden gelocked, met deze exception voor gevolg
                throw new IOException($"Het bestand {bestandsPad} kan niet weggeschreven worden.\n" +
                                $"Probeer het geopende bestand op die locatie te sluiten.");
            }
            catch (Exception e)
            {
                throw new Exception($"Er is een fout opgetreden. {e.Message}");
            }

            return isSuccesvolWeggeschreven;
        }
    }
}
