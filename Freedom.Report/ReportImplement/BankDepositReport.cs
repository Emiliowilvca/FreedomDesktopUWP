using Freedom.Report.BaseReport;
using Freedom.Report.ReportInterface;

namespace Freedom.Report.ReportImplement
{
    public class BankDepositReport : BaseReports, IBankDepositReport
    {
        private readonly string _fileName = "BankDepositReport.pdf";

        public void PrintBankDepositReport(out string filePath)
        {
            //try
            //{
            filePath = "";

            //    filePath = "";

            //    var ss = CreateReport();

            //    if(ss != null)
            //    {
            //    }

            //    //_fileService.ResetFilePath(_fileName);

            //    //string fullPath = _fileService.GetFullPath(_fileName);

            //    //filePath = fullPath;

            //    //PdfDocument pdfDoc = new(new PdfWriter(fullPath));

            //    //Document document = new(pdfDoc, iText.Kernel.Geom.PageSize.A4);

            //    //// Create a PdfFont
            //    //PdfFont font = PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN);
            //    //// Add a Paragraph
            //    //document.Add(new Paragraph("iText is:").SetFont(font));
            //    //// Create a List
            //    //List list = new List()
            //    //    .SetSymbolIndent(12)
            //    //    .SetListSymbol("\u2022")
            //    //    .SetFont(font);
            //    //// Add ListItem objects
            //    //list.Add(new ListItem("Never gonna give Emilio wil"))
            //    //    .Add(new ListItem("Never gonna let you down"))
            //    //    .Add(new ListItem("Never gonna run around and desert you"))
            //    //    .Add(new ListItem("Never gonna make you cry"))
            //    //    .Add(new ListItem("Never gonna say goodbye"))
            //    //    .Add(new ListItem("Never gonna tell a lie and hurt you"));
            //    //// Add the list
            //    //document.Add(list);

            //    ////Close document
            //    //document.Close();
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(ex.Message);
            //}
        }

        public byte[] CreateReport()
        {
            return null;
            //using (MemoryStream ms = new MemoryStream())
            //{
            //    using (PdfDocument pdfDoc = new PdfDocument(new PdfWriter(ms)))
            //    {
            //        Document document = new(pdfDoc, iText.Kernel.Geom.PageSize.A4);

            //        document.Add(new Paragraph("Lorem Ipsum ..."));

            //        document.Close();
            //    }
            //    return ms.ToArray();
            //}
        }
    }
}