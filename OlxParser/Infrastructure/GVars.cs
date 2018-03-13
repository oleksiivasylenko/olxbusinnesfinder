namespace OlxParser
{
    public static class GVars
    {
        public static class Regex
        {
            public static string Page => @"\?page=(\d+)";
        }

        public static class Urls
        {
            public static string OrderUrl1 => "https://www.olx.ua";

            public static string OrderUrl2 => "obyavlenie";

        }

        public static class LabelsText
        {
            public static string LabelLinksLoaded (int loaded, int allCount) => $"{loaded} links to load. All - {allCount}";

            public static string LabelOrdersLoaded(int loaded, int allCount) => $"{loaded} orders to load. All - {allCount}";
        }

        public static class ProgramStatuses
        {
            public static string InProgress => "In progress!";

            public static string Stoped => "In progress!";
        }
    }
}
