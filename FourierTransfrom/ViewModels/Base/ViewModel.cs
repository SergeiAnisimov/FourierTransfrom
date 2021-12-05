using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FourierTransfrom.ViewModels.Base
{
    internal abstract class ViewModel : INotifyPropertyChanged
    {
        // pizdec
#pragma warning disable CS8612 // Nullability of reference types in type doesn't match implicitly implemented member.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning restore CS8612 // Nullability of reference types in type doesn't match implicitly implemented member.

        public void OnPropertyChanged([CallerMemberName] string property = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

        protected bool Set<T>(ref T field, T value, [CallerMemberName] string property = "")
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;

            field = value;
            OnPropertyChanged(property);
            return true;
        }
    }
}
