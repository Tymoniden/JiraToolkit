namespace JiraToolkit.ViewModels
{
    public class OptionsViewModel
    {
        public bool ClearInputFieldAfterEnter { get; set; } = true;

        public bool OpenAtMouseCursorWhenOpenedViaGlobalHotkey { get; set; } = true;

        public bool StayOnTop { get; set; }
    }
}
