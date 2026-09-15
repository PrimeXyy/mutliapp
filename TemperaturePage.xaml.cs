namespace MauiApp1;

public partial class TemperaturePage : ContentPage
{
    public TemperaturePage()
    {
        InitializeComponent();
    }

    private void OnFtoC(object sender, EventArgs e)
    {
        if (!double.TryParse(TempEntry.Text, out double f))
        {
            ResultLabel.Text = "Enter a valid number";
            return;
        }
        double c = (f - 32) * 5.0 / 9.0;
        ResultLabel.Text = $"Result: {c:F2} °C";
    }

    private void OnCtoF(object sender, EventArgs e)
    {
        if (!double.TryParse(TempEntry.Text, out double c))
        {
            ResultLabel.Text = "Enter a valid number";
            return;
        }
        double f = (c * 9.0 / 5.0) + 32;
        ResultLabel.Text = $"Result: {f:F2} °F";
    }
}