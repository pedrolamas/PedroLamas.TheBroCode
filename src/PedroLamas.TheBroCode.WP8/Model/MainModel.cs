using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Xml;
using Cimbalino.Phone.Toolkit.Extensions;
using Cimbalino.Phone.Toolkit.Services;

namespace PedroLamas.TheBroCode.Model
{
    public class MainModel : IMainModel
    {
        private const string QuotesFilename = "Quotes.txt";

        private readonly DateTime _referenceDateTime = new DateTime(2013, 1, 1);
        private readonly Random _random = new Random();

        private int _selectedIndex;

        #region Properties

        public QuoteModel[] Quotes { get; private set; }

        public int SelectedIndex
        {
            get
            {
                return _selectedIndex;
            }
            set
            {
                _selectedIndex = value.Clamp(0, Quotes.Length - 1);
            }
        }

        public QuoteModel SelectedQuote
        {
            get
            {
                return Quotes[SelectedIndex];
            }
        }

        #endregion

        public MainModel()
        {
            Quotes = ReadQuotesFromFile()
                .ToArray();

            _selectedIndex = GetTodaysQuoteIndex();
        }

        public int GetRandomQuoteIndex()
        {
            return _random.Next(0, Quotes.Length - 1);
        }

        public int GetTodaysQuoteIndex()
        {
            return (int)(DateTime.Today.Subtract(_referenceDateTime).TotalDays % Quotes.Length);
        }

        public IEnumerable<QuoteModel> ReadQuotesFromFile()
        {
            var quotesFileResource = Application.GetResourceStream(new Uri(QuotesFilename, UriKind.Relative));

            using (var reader = XmlReader.Create(quotesFileResource.Stream, new XmlReaderSettings() { IgnoreWhitespace = true, IgnoreComments = true }))
            {
                reader.ReadStartElement();

                while (reader.NodeType != XmlNodeType.EndElement)
                {
                    yield return new QuoteModel()
                    {
                        Title = reader.GetAttribute("t"),
                        Description = reader.ReadElementContentAsString()
                    };
                }
            }
        }
    }
}