
using AutoMapper;
using BarberBoss.Domain.Repositories.Billings;
using System.Runtime.InteropServices;
using MigraDoc.Rendering;
using PdfSharp.Fonts;
using MigraDoc.DocumentObjectModel;
using BarberBoss.Domain.Reports;
using PdfSharp.Quality;
using BarberBoss.Application.UseCases.Billings.Reports.Pdf.Fonts;
using System.Reflection;
using MigraDoc.DocumentObjectModel.Tables;
using BarberBoss.Application.UseCases.Billings.Reports.Pdf.Colors;
using BarberBoss.Domain.Extensions;

namespace BarberBoss.Application.UseCases.Billings.Reports.Pdf
{
    public class GenerateBillingsReportPdfUseCase : IGenerateBillingsReportPdfUseCase
    {
        private const string CURRENCY_SYMBOL = "R$";
        private const int HEIGHT_ROW_BILLING_TABLE = 25;
        private readonly IBillingsReadOnlyRepository _repository;

        public GenerateBillingsReportPdfUseCase(IBillingsReadOnlyRepository repository )
        {
            _repository = repository;

            GlobalFontSettings.FontResolver = new BillingsReportFontResolver();
        }
        public async Task<byte[]> Execute(DateOnly month)
        {
            var billings = await _repository.FilterByMonth(month);

            if(billings.Count == 0)
            {
                return [];
            }

            var document = CreateDocument(month);
            var page = CreatePage(document);

            CreateHeaderWithProfilePhotoAndName(page);

            var totalBillings = billings.Sum(billing => billing.Amount);

            CreateTotalEarnedSection(page, month, totalBillings);

            foreach(var billing in billings)
            {
                var table = CreateBillingTable(page);

                var row = table.AddRow();
                row.Height = HEIGHT_ROW_BILLING_TABLE;

                AddingBillingServiceName(row.Cells[0], billing.ServiceName);
                AddHeaderForAmount(row.Cells[3]);

                row = table.AddRow();
                row.Height = HEIGHT_ROW_BILLING_TABLE;

                row.Cells[0].AddParagraph(billing.Date.ToString("D"));
                SetStyleBaseForBillingInformation(row.Cells[0]);
                row.Cells[0].Format.LeftIndent = 20;

                row.Cells[1].AddParagraph(billing.CreatedAt.ToString("t"));
                SetStyleBaseForBillingInformation(row.Cells[1]);

                row.Cells[2].AddParagraph(billing.PaymentMethod.PaymentMethodToString());
                SetStyleBaseForBillingInformation(row.Cells[2]);

                AddAmountForBilling(row.Cells[3], billing.Amount);

                if(string.IsNullOrWhiteSpace(billing.Notes) is false)
                {
                    var notesRow = table.AddRow();
                    notesRow.Height = HEIGHT_ROW_BILLING_TABLE;

                    notesRow.Cells[0].AddParagraph(billing.Notes);
                    notesRow.Cells[0].Format.Font = new Font { Name = FontsHelper.WORKSANS_REGULAR, Size = 10, Color = ColorsHelper.BLACK};
                    notesRow.Cells[0].Shading.Color = ColorsHelper.GREEN_LIGHT;
                    notesRow.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                    notesRow.Cells[0].MergeRight = 2;
                    notesRow.Cells[0].Format.LeftIndent = 20;

                    row.Cells[3].MergeDown = 1;
                }

                AddWhiteSpace(table);
            }

            return RenderDocument(document);
        }

        private Document CreateDocument(DateOnly month)
        {
            var document = new Document();

            document.Info.Title = $"{ResourceReportGenerationMessages.BILLINGS_FOR} {month:Y}";
            document.Info.Author = "Nathan Barbosa";

            var style = document.Styles["Normal"];
            style!.Font.Name = FontsHelper.RALEWAY_REGULAR;

            return document;
        }

        private Section CreatePage(Document document)
        {
            var section = document.AddSection();
            section.PageSetup = document.DefaultPageSetup.Clone();

            section.PageSetup.PageFormat = PageFormat.A4;
            section.PageSetup.LeftMargin = 40;
            section.PageSetup.RightMargin = 40;
            section.PageSetup.BottomMargin = 80;
            section.PageSetup.TopMargin = 80;

            return section;
        }
        private void CreateHeaderWithProfilePhotoAndName(Section page)
        {
            var table = page.AddTable();
            table.AddColumn();
            table.AddColumn(300);

            var row = table.AddRow();

            var assembly = Assembly.GetExecutingAssembly();
            var directoryName = Path.GetDirectoryName(assembly.Location);

            row.Cells[0].AddImage(Path.Combine(directoryName!, "Logo", "ProfilePhoto.jpg"));
            row.Cells[1].AddParagraph(ResourceReportGenerationMessages.NATHAN_BARBERSHOP);
            row.Cells[1].Format.Font = new Font { Name = FontsHelper.RALEWAY_BLACK, Size = 16 };
            row.Cells[1].VerticalAlignment = MigraDoc.DocumentObjectModel.Tables.VerticalAlignment.Center;

            row = table.AddRow();
            row.Height = 30;
            row.Borders.Visible = false;
        }

        private void CreateTotalEarnedSection(Section page, DateOnly month, decimal totalBillings)
        {
            var paragraph = page.AddParagraph();
            paragraph.Format.SpaceBefore = "40";
            paragraph.Format.SpaceAfter = "40";

            var title = string.Format(ResourceReportGenerationMessages.TOTAL_EARNED_IN, month.ToString("Y"));

            paragraph.AddFormattedText(title, new Font { Name = FontsHelper.RALEWAY_REGULAR, Size = 15 });

            paragraph.AddLineBreak();

            paragraph.AddFormattedText($"{CURRENCY_SYMBOL} {totalBillings}", new Font { Name = FontsHelper.WORKSANS_REGULAR, Size = 50 });
        }

        private Table CreateBillingTable(Section page)
        {
            var table = page.AddTable();

            table.AddColumn("195").Format.Alignment = ParagraphAlignment.Left;
            table.AddColumn("80").Format.Alignment = ParagraphAlignment.Center;
            table.AddColumn("120").Format.Alignment = ParagraphAlignment.Center;
            table.AddColumn("120").Format.Alignment = ParagraphAlignment.Right;

            return table;
        }

        private void AddingBillingServiceName(Cell cell, string billingServiceName)
        {
            cell.AddParagraph(billingServiceName);
            cell.Format.Font = new Font { Name = FontsHelper.RALEWAY_REGULAR, Size = 14, Color = ColorsHelper.BLACK };
            cell.Shading.Color = ColorsHelper.RED_LIGHT;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.MergeRight = 2;
            cell.Format.LeftIndent = 20;
        }
        
        private void AddHeaderForAmount(Cell cell)
        {
            cell.AddParagraph(ResourceReportGenerationMessages.AMOUNT);
            cell.Format.Font.Name = FontsHelper.RALEWAY_BLACK;
            cell.Format.Font.Size = 14;
            cell.Format.Font.Color = ColorsHelper.WHITE;
            cell.Shading.Color = ColorsHelper.RED_DARK;
            cell.VerticalAlignment = VerticalAlignment.Center;
        }

        private void SetStyleBaseForBillingInformation(Cell cell)
        {
            cell.Format.Font = new Font { Name = FontsHelper.WORKSANS_REGULAR, Size = 12, Color = ColorsHelper.BLACK };
            cell.Shading.Color = ColorsHelper.GREEN_DARK;
            cell.VerticalAlignment = VerticalAlignment.Center;

        }

        private void AddAmountForBilling(Cell cell, decimal billingAmount)
        {
            cell.AddParagraph($"{CURRENCY_SYMBOL} {billingAmount}");
            cell.Format.Font = new Font { Name = FontsHelper.WORKSANS_REGULAR, Size = 14, Color = ColorsHelper.BLACK };
            cell.Shading.Color = ColorsHelper.WHITE;
            cell.VerticalAlignment = VerticalAlignment.Center;
        }

        private void AddWhiteSpace(Table table)
        {
            var row = table.AddRow();
            row.Height = 30;
            row.Borders.Visible = false;
        }

        private byte[] RenderDocument(Document document)
        {
            var renderer = new PdfDocumentRenderer { Document = document };
            renderer.RenderDocument();

            using var file = new MemoryStream();
            renderer.PdfDocument.Save(file);

            return file.ToArray();
        }
        
    }
}
