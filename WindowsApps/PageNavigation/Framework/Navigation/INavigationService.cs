using System;

namespace Framework.Navigation
{
    public interface INavigationService
    {
        bool NavigateTo(Type type);
        bool NavigateTo(Type type, object parameter);

        bool CanGoBack { get; }
        void GoBack();
    }
}
