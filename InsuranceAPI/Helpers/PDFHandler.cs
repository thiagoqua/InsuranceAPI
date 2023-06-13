
using InsuranceAPI.Models;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using NPOI.HPSF;
using PdfSharp.Pdf.IO;
using System.Diagnostics.Eventing.Reader;

namespace InsuranceAPI.Helpers {
    public class PDFHandler {
        public PDFHandler() {}

        public async Task<byte[]?> export(List<Insured> insureds) {
            PdfDocumentRenderer renderer = new PdfDocumentRenderer(false);
            Document document = new Document();
            Table table = new Table();
            byte[] fileBytes = null;

            await Task.Run(() => {
                configurate(document);
                setColumnsNames(table);
            
                foreach(Insured insured in insureds)
                    setColumnData(table,insured);

                document.LastSection.Add(table);

                renderer.Document = document;
                renderer.RenderDocument();
                using(MemoryStream stream = new()) {
                    renderer.PdfDocument.Save(stream);
                    fileBytes = stream.ToArray();
                }
            });

            return fileBytes;
        }

        private void configurate(Document document) {
            document.AddSection();
            document.DefaultPageSetup.TopMargin = 0;
            document.DefaultPageSetup.BottomMargin = 0.5;
            document.DefaultPageSetup.LeftMargin = 0.5;
            document.DefaultPageSetup.RightMargin = 0;
            document.DefaultPageSetup.Orientation = Orientation.Landscape;
        }

        private void setColumnsNames(Table table) {
            Column column;
            Row row;
            Cell cell;

            for(int i = 0;i <= 13; ++i) {
                column = table.AddColumn(Unit.FromCentimeter(2.125));
                column.Format.Alignment = ParagraphAlignment.Center;
            }

            row = table.AddRow();
            row.Borders.Right.Width = 1.5;
            row.Borders.Bottom.Width = 1.5;
            row.Shading.Color = Colors.LightGreen;

            cell = row.Cells[0];
            cell.AddParagraph("MATRICULA");
            cell.Format.Font.Bold = true;
            cell.Format.Font.Color = Colors.DarkBlue;

            cell = row.Cells[1];
            cell.AddParagraph("CARPETA");
            cell.Format.Font.Bold = true;
            cell.Format.Font.Color = Colors.DarkBlue;

            cell = row.Cells[2];
            cell.AddParagraph("VIGENCIA");
            cell.Format.Font.Bold = true;
            cell.Format.Font.Color = Colors.DarkBlue;

            cell = row.Cells[3];
            cell.AddParagraph("CLIENTE");
            cell.Format.Font.Bold = true;
            cell.Format.Font.Color = Colors.DarkBlue;

            cell = row.Cells[4];
            cell.AddParagraph("FECHA NAC.");
            cell.Format.Font.Bold = true;
            cell.Format.Font.Color = Colors.DarkBlue;

            cell = row.Cells[5];
            cell.AddParagraph("DIRECCION");
            cell.Format.Font.Bold = true;
            cell.Format.Font.Color = Colors.DarkBlue;

            cell = row.Cells[6];
            cell.AddParagraph("ESTADO");
            cell.Format.Font.Bold = true;
            cell.Format.Font.Color = Colors.DarkBlue;

            cell = row.Cells[7];
            cell.AddParagraph("VTO. CUOTA");
            cell.Format.Font.Bold = true;
            cell.Format.Font.Color = Colors.DarkBlue;

            cell = row.Cells[8];
            cell.AddParagraph("LOCALIDAD");
            cell.Format.Font.Bold = true;
            cell.Format.Font.Color = Colors.DarkBlue;

            cell = row.Cells[9];
            cell.AddParagraph("DNI");
            cell.Format.Font.Bold = true;
            cell.Format.Font.Color = Colors.DarkBlue;

            cell = row.Cells[10];
            cell.AddParagraph("TEL.");
            cell.Format.Font.Bold = true;
            cell.Format.Font.Color = Colors.DarkBlue;

            cell = row.Cells[11];
            cell.AddParagraph("DESC.");
            cell.Format.Font.Bold = true;
            cell.Format.Font.Color = Colors.DarkBlue;

            cell = row.Cells[12];
            cell.AddParagraph("CUIT");
            cell.Format.Font.Bold = true;
            cell.Format.Font.Color = Colors.DarkBlue;

            cell = row.Cells[13];
            cell.AddParagraph("PROD.");
            cell.Format.Font.Bold = true;
            cell.Format.Font.Color = Colors.DarkBlue;

        }

        private void setColumnData(Table table,Insured insured) {
            Row row = table.AddRow();
            Cell cell;
            string born, address, phones, folder, namesAndPolicy;

            born = insured.Born.ToString("dd/MM/yyyy hh:mm:ss").Split(" ")[0];
            address = insured.AddressNavigation.Street + " " +
                      insured.AddressNavigation.Number;
            phones = "";
            folder = insured.Folder != 0 ? insured.Folder.ToString() : "SIN CARPETA";
            namesAndPolicy = insured.Firstname + " " + insured.Lastname;

            //completing address
            if(insured.AddressNavigation.Floor != null)
                address += " P " + insured.AddressNavigation.Floor.ToString();
            if(insured.AddressNavigation.Departament != null)
                address += " DTO " + insured.AddressNavigation.Departament;

            //completing phones
            for(int i = 0; i < insured.Phones.Count; ++i) {
                Phone phone = insured.Phones.ElementAt(i);
                phones += phone.Number;

                if(phone.Description != null)
                    phones += " (" + phone.Description + ")";

                if((i + 1) < insured.Phones.Count)
                    phones += "/";
            }

            //completing policy
            if(insured.InsurancePolicy != null)
                namesAndPolicy += " (" + insured.InsurancePolicy + ") ";

            //completing company 
            if(insured.Company == 1) {
                //company is cooperación, so the background
                //will be align to the producer
                switch(insured.Producer) {
                    case 1:
                        row.Shading.Color = Colors.HotPink;
                    break;
                    case 2:
                        row.Shading.Color = Colors.Yellow;
                    break;
                    case 3:
                        row.Shading.Color = Colors.Green;
                    break;
                    case 4:
                        row.Shading.Color = Colors.Orange;
                    break;
                }
            } else {
                //company is federación, so the background
                //will be white if it isn't property of a specific producer
                if(insured.ProducerNavigation.Id == 5)
                    row.Shading.Color = Colors.LightBlue;
                else
                    row.Shading.Color = Colors.White;
            }

            cell = row.Cells[0];
            cell.AddParagraph(insured.License);

            cell = row.Cells[0];
            cell.AddParagraph(folder);

            cell = row.Cells[2];
            cell.AddParagraph(insured.Life);

            cell = row.Cells[3];
            cell.AddParagraph(namesAndPolicy);

            cell = row.Cells[4];
            cell.AddParagraph(born);

            cell = row.Cells[5];
            cell.AddParagraph(address);

            cell = row.Cells[6];
            cell.AddParagraph(insured.Status);

            cell = row.Cells[7];
            cell.AddParagraph(insured.PaymentExpiration.ToString());

            cell = row.Cells[8];
            cell.AddParagraph(insured.AddressNavigation.City);

            cell = row.Cells[9];
            cell.AddParagraph(insured.Dni);

            cell = row.Cells[10];
            cell.AddParagraph(phones);

            cell = row.Cells[11];
            cell.AddParagraph(insured.Description);

            cell = row.Cells[12];
            cell.AddParagraph(insured.Cuit);

            cell = row.Cells[13];
            cell.AddParagraph(insured.ProducerNavigation.Firstname);
        }
    }
}
