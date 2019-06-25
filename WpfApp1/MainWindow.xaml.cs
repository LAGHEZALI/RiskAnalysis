using RiskAnalysis.Helpers;
using RiskAnalysis.Instruments;
using RiskAnalysis.MarketData;
using RiskAnalysis.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<GreekType> listGreeks;
        private Way way;
        private OptionType optionType;
        private Underlying sousJacent;
        private double vol;
        private double interestRate;
        private double strikeValue;
        private DateTime valueDate;
        private DateTime riskfacorDate;
        public DateTime maturityDate;


        public MainWindow()
        {
            InitializeComponent();
            
            
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Isdecimal(e.Text) == false && e.Text.Contains(",") == false)
                e.Handled = true;
        }

        public bool Isdecimal(string text)
        {
            return Double.TryParse(text, out double x);
        }

        private void TextBox_PreviewTextInput_1(object sender, TextCompositionEventArgs e)
        {
            if (Isdecimal(e.Text) == false && e.Text.Contains(",") == false)
                e.Handled = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            GetData(out vol, out strikeValue, out interestRate, out valueDate, out maturityDate, out riskfacorDate);

            way = (Way) System.Enum.Parse(typeof(Way), Way.SelectedItem.ToString());
            optionType = (OptionType) System.Enum.Parse(typeof(OptionType), TypeOption.SelectedItem.ToString());
            sousJacent = (Underlying) System.Enum.Parse(typeof(Underlying), sousjacent.SelectedItem.ToString());

            var x = MyGreeks.SelectedItems;
            var z = MyGreeks.SelectedItem;

            listGreeks = MyGreeks.SelectedItems.Cast<GreekType>().ToList();

            Data.InitMarketData(vol, interestRate, valueDate);
            var option = new OptionInstrument(strikeValue, maturityDate, optionType, way, sousJacent, riskfacorDate);

            var pricingcfg = new PricingConfiguration(listGreeks);
            var pricer = new Pricer();
            Result.Text = pricer.Results(option, pricingcfg).Mtm.ToString();

            var resultGreek = pricer.Results(option, pricingcfg).Results;
            if (resultGreek.Count != 0)
            {
                foreach (var result in resultGreek)
                {
                    switch (result.Key)
                    {
                        case GreekType.Delta:
                            ValueDelta.Text = result.Value.ToString("");
                            break;
                        case GreekType.Vega:
                            ValueVega.Text = result.Value.ToString();
                            break;
                        case GreekType.Gamma:
                            ValueGamma.Text = result.Value.ToString();
                            break;
                        case GreekType.Rho:
                            ValueRho.Text = result.Value.ToString();
                            break;
                        case GreekType.Tetha:
                            ValueTetha.Text = result.Value.ToString();
                            break;
                    }
                }
            }
        }

        void GetData(out double vol,out double strike, out double rate, out DateTime paramDate, out DateTime maturityDate,
            out DateTime riskFactorDate)
        {
            var volIsValid = double.TryParse(Volatility.Text, out vol);
            var rateIsValid = double.TryParse(Rate.Text, out rate);
            var strikeIsValid = double.TryParse(Strike.Text,out strike);
            paramDate = (DateTime)ParamDate.SelectedDate;
            maturityDate =(DateTime)Maturity.SelectedDate;
            riskFactorDate = (DateTime)RiskFactorDate.SelectedDate;
        }

        private void Result_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void ComboBoxItem_Selected_1(object sender, RoutedEventArgs e)
        {

        }

        private void lb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           // listGreeks = listTopics.SelectedItems.Cast<GreekType>().ToList(); 

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          

        }

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            //optionType = (OptionType)TypeOption.SelectionBoxItem;
        }

        private void ComboBox_SelectionChanged_2(object sender, SelectionChangedEventArgs e)
        {
            //sousJacent = (Underlying)TypeOption.SelectionBoxItem;
        }

        private void Volatility_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
