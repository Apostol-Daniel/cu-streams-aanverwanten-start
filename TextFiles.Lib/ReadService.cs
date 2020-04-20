using System;
using System.IO;
using Microsoft.Win32;
using System.Text;


namespace TextFiles.Lib
{
    public class ReadService
    {
        public static string RootPad { get; } = AppDomain.CurrentDomain.BaseDirectory;
        public static string MyDocs { get; } = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public string TextFileToString(string bestandsMap, string bestandsNaam)
        {
            string bestandsInhoud = "";
            string bestandsPad = bestandsMap + "\\" + bestandsNaam;

            try
            {
                // Er wordt een instance aangemaakt van de StreamReader-class
                using (StreamReader sr = new StreamReader(bestandsPad))
                {
                    bestandsInhoud = sr.ReadToEnd();
                }
                // na het using statement wordt de StreamReader gesloten en wordt het geheugen vrijgegeven.
            }
            catch (IOException)
            {
                throw new IOException($"Het bestand {bestandsPad} kan niet geopend worden.\nProbeer het te sluiten.");
            }
            catch (Exception e)
            {
                throw new Exception($"Er is een fout opgetreden. {e.Message}");
            }

            return bestandsInhoud;
        }

    }
}
