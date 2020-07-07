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

        public string [] GeefPadOmOpTeSlaan(string voorgesteldeNaam = "", 
            string filter = "Text documents (.txt)|*.txt|Comma separated values (.csv)|*.csv")
        {
            string[] bestandInfo;
            string pad, bestandsNaam, folder;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = filter;
            saveFileDialog.Title = "Kies een plaats om je bestand op te slaan";

            if (!string.IsNullOrEmpty(voorgesteldeNaam.Trim())) 
            {
                saveFileDialog.FileName = voorgesteldeNaam;
            }

            saveFileDialog.ShowDialog();

            pad = saveFileDialog.FileName;
            bestandsNaam = saveFileDialog.SafeFileName;
            folder = pad.Substring(0 , pad.Length - bestandsNaam.Length);
            bestandInfo = new string[]
            {
                folder , bestandsNaam
            };

            return bestandInfo;
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
