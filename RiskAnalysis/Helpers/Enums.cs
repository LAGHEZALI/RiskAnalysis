namespace RiskAnalysis.Helpers
{

    public enum OptionType
    {
        Call,
        Put
    }

    public enum Way
    {
        Buy,
        Sell
    }

    public enum GreekType
    {
       Delta,
        Vega,
        Gamma,
        Rho,
        Tetha,
    }

    public enum Underlying
    {
        Petrol,
        Elec,
        BaseMetal,
        PreciousMetal
    }
}
