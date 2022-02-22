namespace Intento.MT.Plugin.PropertiesForm
{
    public class ErrorInfo
    {
        public bool IsError { get; }
        public string VisibleErrorText { get; }
        public string ClipBoardContent { get; }

        public ErrorInfo(bool isError, string visibleErrorText, string clipBoardContent)
        {
            IsError = isError;
            VisibleErrorText = visibleErrorText;
            ClipBoardContent = clipBoardContent;
        }
    }
}