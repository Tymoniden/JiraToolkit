using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace JiraToolkit.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        Dictionary<string, object> valueStore = new Dictionary<string, object>();

        public event PropertyChangedEventHandler PropertyChanged;

        protected void SetValue(object value, [CallerMemberName] string propertyName = null)
        {
            if (valueStore.ContainsKey(propertyName))
            {
                if (valueStore[propertyName] == value)
                {
                    return;
                }
            }

            valueStore[propertyName] = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected T GetValue<T>([CallerMemberName] string propertyName = null)
        {
            if (!valueStore.ContainsKey(propertyName))
            {
                return default(T);
            }

            return (T)valueStore[propertyName];
        }
    }
}
