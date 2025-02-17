using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;

class Ciudadano
{
    public string Nombre { get; set; }
    public bool Vacunado { get; set; }
    public string TipoVacuna { get; set; } // Puede ser "Pfizer", "AstraZeneca" o "Ambas"
}

class Program
{
    static void Main()
    {
        // Generar ciudadanos
        List<Ciudadano> ciudadanos = GenerarCiudadanos(500, 75, 75);

        // Listados requeridos
        var noVacunados = ciudadanos.Where(c => !c.Vacunado).ToList();
        var vacunadosPfizer = ciudadanos.Where(c => c.TipoVacuna == "Pfizer").ToList();
        var vacunadosAstraZeneca = ciudadanos.Where(c => c.TipoVacuna == "AstraZeneca").ToList();
        var vacunadosAmbas = ciudadanos.Where(c => c.TipoVacuna == "Ambas").ToList();

        // Generar reporte en PDF
        GenerarReportePDF(noVacunados, vacunadosPfizer, vacunadosAstraZeneca, vacunadosAmbas);
    }

    static List<Ciudadano> GenerarCiudadanos(int total, int pfizerCount, int astrazenecaCount)
    {
        List<Ciudadano> ciudadanos = new List<Ciudadano>();
        Random rand = new Random();

        // Generar ciudadanos no vacunados
        for (int i = 1; i <= total; i++)
        {
            Ciudadano ciudadano = new Ciudadano
            {
                Nombre = "Ciudadano " + i,
                Vacunado = false,
                TipoVacuna = null
            };
            ciudadanos.Add(ciudadano);
        }

        // Vacunar a 75 ciudadanos con Pfizer
        for (int i = 0; i < pfizerCount; i++)
        {
            int index;
            do
            {
                index = rand.Next(0, total);
            } while (ciudadanos[index].Vacunado); // Asegurarse de que no esté ya vacunado

            ciudadanos[index].Vacunado = true;
            ciudadanos[index].TipoVacuna = "Pfizer";
        }

        // Vacunar a 75 ciudadanos con AstraZeneca
        for (int i = 0; i < astrazenecaCount; i++)
        {
            int index;
            do
            {
                index = rand.Next(0, total);
            } while (ciudadanos[index].Vacunado); // Asegurarse de que no esté ya vacunado

            ciudadanos[index].Vacunado = true;
            ciudadanos[index].TipoVacuna = "AstraZeneca";
        }

        // Opcional: Vacunar a algunos ciudadanos con ambas vacunas
        // Aquí puedes agregar lógica si deseas que algunos ciudadanos tengan ambas vacunas

        return ciudadanos;
    }

    static void GenerarReportePDF(List<Ciudadano> noVacunados, List<Ciudadano> vacunadosPfizer, List<Ciudadano> vacunadosAstraZeneca, List<Ciudadano> vacunadosAmbas)
    {
        Document doc = new Document();
        PdfWriter.GetInstance(doc, new FileStream("Reporte_Vacunacion.pdf", FileMode.Create));
        doc.Open();

        doc.Add(new Paragraph("Reporte de Vacunación"));
        doc.Add(new Paragraph(" ")); // Espacio en blanco

        doc.Add(new Paragraph("Listado de ciudadanos que no se han vacunado:"));
        foreach (var c in noVacunados)
        {
            doc.Add(new Paragraph(c.Nombre));
        }

        doc.Add(new Paragraph(" ")); // Espacio en blanco

        doc.Add(new Paragraph("Listado de ciudadanos que han recibido la vacuna de Pfizer:"));
        foreach (var c in vacunadosPfizer)
        {
            doc.Add(new Paragraph(c.Nombre));
        }

        doc.Add(new Paragraph(" ")); // Espacio en blanco

        doc.Add(new Paragraph("Listado de ciudadanos que han recibido la vacuna de AstraZeneca:"));
        foreach (var c in vacunadosAstraZeneca)
        {
            doc.Add(new Paragraph(c.Nombre));
        }

        doc.Add(new Paragraph(" ")); // Espacio en blanco

        doc.Add(new Paragraph("Listado de ciudadanos que han recibido ambas vacunas:"));
        foreach (var c in vacunadosAmbas)
        {
            doc.Add(new Paragraph(c.Nombre));
        }

        doc.Close();
        Console.WriteLine("Reporte generado: Reporte_V
