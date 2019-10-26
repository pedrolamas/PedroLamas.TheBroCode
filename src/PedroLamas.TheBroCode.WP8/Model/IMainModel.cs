namespace PedroLamas.TheBroCode.Model
{
    public interface IMainModel
    {
        QuoteModel[] Quotes { get; }

        int SelectedIndex { get; set; }

        QuoteModel SelectedQuote { get; }

        int GetRandomQuoteIndex();

        int GetTodaysQuoteIndex();
    }
}