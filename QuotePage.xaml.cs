namespace MauiApp1;

public partial class QuotePage : ContentPage
{
    private readonly string[] horoscopes = new[]
    {
        "Your luck will be infinite! Expect great things today.",
        "A mysterious phone call will bring you unexpected fortune.",
        "Beware of dropped calls and low battery energy today.",
        "Your financial luck is ringing off the charts!",
        "Someone special is trying to reach you—keep your phone nearby.",
        "Signal strength is low, but your personal charisma is at 100%.",
        "A wrong number today might lead to a surprisingly great conversation.",
        "Your digital karma is exceptionally high right now."
    };

    public QuotePage()
    {
        InitializeComponent();
    }

    private void OnGetQuote(object sender, EventArgs e)
    {
        // Clean the input to remove any spaces or dashes users might type
        string input = NumberEntry.Text?.Trim() ?? string.Empty;

        // Use long.TryParse because phone numbers (like 089298900) can exceed int limits
        if (!long.TryParse(input, out long seed))
        {
            QuoteLabel.Text = "Please enter a valid phone number first!";
            return;
        }

        // Cast the seed safely to an int for the Random class constructor
        var random = new Random((int)(seed % int.MaxValue));
        int index = random.Next(horoscopes.Length);

        QuoteLabel.Text = horoscopes[index];
    }
}