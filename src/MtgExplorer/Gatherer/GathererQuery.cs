using System;

namespace MtgExplorer.Gatherer
{
    public class GathererQuery
    {
        private const string _UrlBase = "http://gatherer.wizards.com/Pages/Search/Default.aspx?";

        public static Uri CreateStandardUri(string set)
        {
            string uri = string.Format("{0}output={1}&action=advanced&set=[\"{2}\"]", _UrlBase,
                                       Output.standard.ToString(), set);
            return new Uri(uri);
        }

        public static Uri CreateCompactUri(string set)
        {
            string uri = string.Format("{0}output={1}&action=advanced&set=[\"{2}\"]", _UrlBase,
                                       Output.compact.ToString(), set);
            return new Uri(uri);
        }

        public static Uri CreateChecklistUri(string set)
        {
            string uri = string.Format("{0}output={1}&action=advanced&set=[\"{2}\"]", _UrlBase,
                                       Output.checklist.ToString(), set);
            return new Uri(uri);
        }

        public static Uri CreateVisualSpoilerUri(string set)
        {
            string uri = string.Format("{0}output={1}&method=visual&action=advanced&set=[\"{2}\"]", _UrlBase,
                                       Output.spoiler.ToString(), set);
            return new Uri(uri);
        }

        public static Uri CreateTextSpoilerUri(string set)
        {
            string uri = string.Format("{0}output={1}&method=text&action=advanced&set=[\"{2}\"]", _UrlBase,
                                       Output.spoiler.ToString(), set);
            return new Uri(uri);
        }

        private enum Output
        {
            standard,
            compact,
            checklist,
            spoiler
        }
    }
}