namespace MauiApp1;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnCalculateClicked(object sender, EventArgs e)
    {
        if (!double.TryParse(LoanAmountEntry.Text, out double loanAmount) ||
            !double.TryParse(InterestRateEntry.Text, out double annualRate) ||
            !double.TryParse(LoanTermEntry.Text, out double years))
        {
            await DisplayAlertAsync("Invalid Input", "Please enter valid numbers in all fields.", "OK");
            return;
        }

        double monthlyRate = (annualRate / 100) / 12;
        int totalMonths = (int)(years * 12);

        double monthlyPayment;
        if (monthlyRate == 0)
        {
            monthlyPayment = loanAmount / totalMonths;
        }
        else
        {
            monthlyPayment = loanAmount *
                (monthlyRate * Math.Pow(1 + monthlyRate, totalMonths)) /
                (Math.Pow(1 + monthlyRate, totalMonths) - 1);
        }

        double totalPayment = monthlyPayment * totalMonths;
        double totalInterest = totalPayment - loanAmount;

        MonthlyPaymentLabel.Text = monthlyPayment.ToString("C2");
        TotalInterestLabel.Text = totalInterest.ToString("C2");
        TotalPaymentLabel.Text = totalPayment.ToString("C2");
    }
}