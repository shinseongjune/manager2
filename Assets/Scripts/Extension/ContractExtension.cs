public static class ContractExtension
{
    public static void DateProgress(this Contract contract)
    {
        contract.RemainingPeriod--;
    }
}
