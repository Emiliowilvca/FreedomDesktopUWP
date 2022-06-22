namespace Freedom.Report.ReportInterface
{
    public interface IBankDepositReport
    {
        void PrintBankDepositReport(out string filePath);

        byte[] CreateReport();
    }
}