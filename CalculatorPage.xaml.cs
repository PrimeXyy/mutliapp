namespace MauiApp1;

public partial class CalculatorPage : ContentPage
{
    private double currentValue = 0;
    private double storedValue = 0;
    private string pendingOperator = "";
    private bool startNewEntry = true;

    public CalculatorPage()
    {
        InitializeComponent();
    }

    private void OnDigit(object sender, EventArgs e)
    {
        string digit = ((Button)sender).Text;

        if (startNewEntry || DisplayLabel.Text == "0")
        {
            DisplayLabel.Text = digit;
            startNewEntry = false;
        }
        else
        {
            DisplayLabel.Text += digit;
        }
    }

    private void OnDecimal(object sender, EventArgs e)
    {
        if (startNewEntry)
        {
            DisplayLabel.Text = "0.";
            startNewEntry = false;
            return;
        }

        if (!DisplayLabel.Text.Contains('.'))
            DisplayLabel.Text += ".";
    }

    private void OnOperator(object sender, EventArgs e)
    {
        string op = ((Button)sender).Text;

        if (!startNewEntry)
        {
            if (pendingOperator != "")
                CalculateResult();
            else
                storedValue = double.Parse(DisplayLabel.Text);
        }

        pendingOperator = op;
        ExpressionLabel.Text = $"{FormatNumber(storedValue)} {pendingOperator}";
        startNewEntry = true;
    }

    private void OnEquals(object sender, EventArgs e)
    {
        if (pendingOperator == "") return;

        ExpressionLabel.Text = $"{FormatNumber(storedValue)} {pendingOperator} {DisplayLabel.Text} =";
        CalculateResult();
        pendingOperator = "";
        startNewEntry = true;
    }

    private void CalculateResult()
    {
        double currentEntry = double.Parse(DisplayLabel.Text);

        currentValue = pendingOperator switch
        {
            "+" => storedValue + currentEntry,
            "−" => storedValue - currentEntry,
            "×" => storedValue * currentEntry,
            "÷" => currentEntry != 0 ? storedValue / currentEntry : 0,
            _ => currentEntry
        };

        DisplayLabel.Text = FormatNumber(currentValue);
        storedValue = currentValue;
    }

    private void OnClearAll(object sender, EventArgs e)
    {
        currentValue = 0;
        storedValue = 0;
        pendingOperator = "";
        DisplayLabel.Text = "0";
        ExpressionLabel.Text = "";
        startNewEntry = true;
    }

    private void OnClearEntry(object sender, EventArgs e)
    {
        DisplayLabel.Text = "0";
        startNewEntry = true;
    }

    private void OnBackspace(object sender, EventArgs e)
    {
        if (DisplayLabel.Text.Length > 1)
            DisplayLabel.Text = DisplayLabel.Text[..^1];
        else
            DisplayLabel.Text = "0";
    }

    private void OnToggleSign(object sender, EventArgs e)
    {
        if (double.TryParse(DisplayLabel.Text, out double val))
            DisplayLabel.Text = FormatNumber(val * -1);
    }

    private void OnPercent(object sender, EventArgs e)
    {
        if (double.TryParse(DisplayLabel.Text, out double val))
            DisplayLabel.Text = FormatNumber(val / 100);
    }

    private void OnReciprocal(object sender, EventArgs e)
    {
        if (double.TryParse(DisplayLabel.Text, out double val) && val != 0)
            DisplayLabel.Text = FormatNumber(1 / val);
    }

    private void OnSquare(object sender, EventArgs e)
    {
        if (double.TryParse(DisplayLabel.Text, out double val))
            DisplayLabel.Text = FormatNumber(val * val);
    }

    private void OnSquareRoot(object sender, EventArgs e)
    {
        if (double.TryParse(DisplayLabel.Text, out double val) && val >= 0)
            DisplayLabel.Text = FormatNumber(Math.Sqrt(val));
    }

    private string FormatNumber(double num)
    {
        return num % 1 == 0 ? num.ToString("0") : num.ToString("0.##########");
    }
}