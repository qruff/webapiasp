namespace webapiasp.Utility
{
    public class Constants
    {
        public enum GuideStatus
        {
            APPOINTED,
            PENDING,
            PROCESSING,
            DONE
        }

        public enum GuideTime
        {
            MORNING,
            AFTERNOON,
            EVENING,
            NIGHT,
            DEFAULT
        }

        public enum IsGuideAssigned
        {
            YES,
            NO
        }
        public static class GuideStatusValues
        {
            public const string APPOINTED = "Назначен";
            public const string PENDING = "В ожидании";
            public const string PROCESSING = "В процессе";
            public const string DONE = "Исполнено";
        }

        public static class GuideTimeValues
        {
            public const string MORNING = "Утро";
            public const string AFTERNOON = "День";
            public const string EVENING = "Вечер";
            public const string NIGHT = "Ночь";
            public const string DEFAULT = "";
        }

        public static class IsGuideAssignedValues
        {
            public const string YES = "Да";
            public const string NO = "Нет";
        }
    }
}
