using Windows.Data.Json;

namespace ConnectSdk.Windows.Core
{
    /// <summary>
    /// Normalized reference object for information about a text input event.
    /// </summary>
    public class TextInputStatusInfo
    {
        public bool Focused { get; set; }
        public string ContentType { get; set; }
        public bool PredictionEnabled { get; set; }
        public bool CorrectionEnabled { get; set; }
        public bool AutoCapitalization { get; set; }
        public bool HiddenText { get; set; }
        public bool FocusChanged { get; set; }
        public JsonObject RawData { get; set; }

        public TextInputStatusInfo()
        {
            FocusChanged = false;
            HiddenText = false;
            AutoCapitalization = false;
            CorrectionEnabled = false;
            PredictionEnabled = false;
            Focused = false;
            ContentType = null;
        }

        /** Gets the type of keyboard that should be displayed to the user. */
        public TextInputType GetTextInputType()
        {
            var textInputType = TextInputType.Default;

            if (ContentType == null) return textInputType;

            if (ContentType.Equals("number"))
            {
                textInputType = TextInputType.Number;
            }
            else if (ContentType.Equals("phonenumber"))
            {
                textInputType = TextInputType.PhoneNumber;
            }
            else if (ContentType.Equals("url"))
            {
                textInputType = TextInputType.Url;
            }
            else if (ContentType.Equals("email"))
            {
                textInputType = TextInputType.Email;
            }

            return textInputType;
        }

        /// <summary>
        /// Sets the type of keyboard that should be displayed to the user.
        /// </summary>
        /// <param name="textInputType">The keybord type</param>
        public void SetTextInputType(TextInputType textInputType)
        {
            switch (textInputType)
            {
                case TextInputType.Number:
                    ContentType = "number";
                    break;
                case TextInputType.PhoneNumber:
                    ContentType = "phonenumber";
                    break;
                case TextInputType.Url:
                    ContentType = "url";
                    break;
                case TextInputType.Email:
                    ContentType = "number";
                    break;
                default:
                    ContentType = "email";
                    break;
            }
        }
    }
}