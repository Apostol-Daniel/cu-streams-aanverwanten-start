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
        /// 

        public string[] KiesBestand(string filter = "Text documents(.txt)|*.txt|" +
                                                    "Comma separated values(.csv)|*.csv")
        {
            string[] gekozenBesnatndsInfo = new string[2];
            string gekozenBestandsPad;
            int lastBackslashIndex;
            OpenFileDialog kiesBestand = new OpenFileDialog();
            kiesBestand.Filter = filter;
            bool? result = kiesBestand.ShowDialog();
            //bool? beteknt dat de boolean naast true en false ook de waarde null kan bevatten

            gekozenBestandsPad = kiesBestand.FileName;

            if (string.IsNullOrEmpty(gekozenBestandsPad.Trim()))
            {
                throw new Exception("Er is geen bestand gekozen");
            }
            else
            {
                lastBackslashIndex = gekozenBestandsPad.LastIndexOf('\\');
                gekozenBesnatndsInfo[0] = gekozenBestandsPad.Substring(0, lastBackslashIndex);
                gekozenBesnatndsInfo[1] = gekozenBestandsPad.Substring(lastBackslashIndex + 1);
            }
            return gekozenBesnatndsInfo;
        }

        public bool StringToTextFile(string tekst, string bestandsMap, string bestandsNaam,
            Encoding encoding = null,bool overshrijfBestaandBestand = false)
        {
            bool isSuccesvolWeggeschreven;
            string bestandsPad;
            bestandsPad = bestandsMap + "\\" + bestandsNaam;

            if (encoding == null) 
            {
                encoding = Encoding.Default;
            }

            if(string.IsNullOrEmpty(bestandsPad.Trim())) 
            {
                throw new Exception("Er is geen bestand gekozen");
            }

            if (!Directory.Exists(bestandsMap)) 
            {
                try 
                {
                    Directory.CreateDirectory(bestandsMap);
                }
                catch (Exception)
                {
                    throw new Exception("De map is niet gevonden");
                }
            }

            if (File.Exists(bestandsPad) && !overshrijfBestaandBestand) 
            {
                throw new Exception("Het bestand bestaat reeds");
            }

            try
            {
                // Er wordt een instance aangemaakt van de StreamWriter-class
                using (StreamWriter sw = new StreamWriter(new FileStream(bestandsPad,FileMode.Create,FileAccess.ReadWrite),encoding))
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
