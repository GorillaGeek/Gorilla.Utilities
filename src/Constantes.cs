namespace Gorilla.Utilities
{
    public class Constantes
    {
        /// <summary>
        /// Regex to validade email address
        /// </summary>
        public const string REGEX_EMAIL = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";

        /// <summary>
        /// Regex to validate brazilian CPF
        /// </summary>
        public const string REGEX_CPF = @"^\d{3}\.\d{3}\.\d{3}\-\d{2}$";

        // <summary>
        /// Regex to validade date
        /// </summary>
        public const string REGEX_DATE = @"(0?[1-9]|1[012])\/(0?[1-9]|[12][0-9]|3[01])\/\d{4}";

        // <summary>
        /// Regex to validade time
        /// </summary>
        public const string REGEX_TIME = @"(1[012]|[1-9]):[0-5][0-9](\s?)(am|AM|pm|PM)";

        /// <summary>
        /// Regexp for Time 24 hours
        /// </summary>
        public const string REGEX_TIME24 = @"^(([01][\d])|(2[0-3]))\:([0-5][\d])$";

        /// <summary>
        /// Regex to validade multiple email addresses separeted by comma
        /// </summary>
        public const string REGEX_EMAIL_MULTIPLE = @"^(\s*,?\s*[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})+\s*$";

        /// <summary>
        /// Regex for credit card expiration date validation
        /// </summary>
        public const string REGEX_CREDIT_CARD_EXPIRATION_DATE = @"(0[1-9]|1[0-2])\/[0-9]{2}";

        /// <summary>
        /// Regexp for Brazilian CEP
        /// </summary>
        public const string REGEX_CEP = @"^\d{5}(\-)(\d{3})?$";

        /// <summary>
        /// Mask for phone
        /// </summary>
        public const string MASK_PHONE = "(999)999-9999";

        /// <summary>
        /// Mask for money
        /// </summary>
        public const string MASK_MONEY = "money";

        /// <summary>
        /// Mask for Date
        /// </summary>
        public const string MASK_DATE = "99/99/9999";

        /// <summary>
        /// Default system time format - 10:00 AM
        /// </summary>
        public const string DEFAULT_TIME_FORMAT = "hh:mm tt";

        /// <summary>
        /// 
        /// </summary>
        public const string REGEX_PHONE_NUMBER = @"^(?:(?:\+?1\s*(?:[.-]\s*)?)?(?:\(\s*([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9])\s*\)|([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9]))\s*(?:[.-]\s*)?)?([2-9]1[02-9]|[2-9][02-9]1|[2-9][02-9]{2})\s*(?:[.-]\s*)?([0-9]{4})(?:\s*(?:#|x\.?|ext\.?|extension)\s*(\d+))?$";

        /// <summary>
        /// 
        /// </summary>
        public const string MASK_CPF = "999.999.999-99";

        /// <summary>
        /// 
        /// </summary>
        public const string MASK_RG = "99.999.999-*";

        /// <summary>
        /// 
        /// </summary>
        public const string MASK_CEP = "99999-999";

    }
}
