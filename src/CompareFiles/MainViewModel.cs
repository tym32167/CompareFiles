using CompareFiles.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CompareFiles
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly StringsComparer _comparer = new StringsComparer();

        private string _left;
        private string _right;

        public string Left
        {
            get => _left;
            set
            {
                if (value == _left) return;
                _left = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CompareResult));
            }
        }

        public string Right
        {
            get => _right;
            set
            {
                if (value == _right) return;
                _right = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CompareResult));
            }
        }

        public IEnumerable<CompareStringResult> CompareResult => _comparer.Compare(
            Left?.Split(new[] { Environment.NewLine }, StringSplitOptions.None) ?? new string[0],
            Right?.Split(new[] { Environment.NewLine }, StringSplitOptions.None) ?? new string[0]
        );


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}