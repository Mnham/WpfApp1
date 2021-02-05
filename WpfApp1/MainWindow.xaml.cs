using FirstFloor.ModernUI.Presentation;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowVM();
        }
    }

    public class MainWindowVM : NotifyPropertyChanged
    {
        A selectedA;
        int selectedInt;
        int counter;
        bool checkBoxIsChecked;

        public MainWindowVM()
        {
            for (int i = 0; i < 10; i++)
                CollectionA.Add(new() { MyProperty1 = $"число {i}", MyProperty2 = $"число {i + 20}" });
        }

        public ObservableCollection<A> CollectionA { get; } = new();
        public ObservableCollection<B> CollectionB { get; } = new();
        public static ObservableCollection<C> CollectionC { get; } = new();
        public A SelectedA { get => selectedA; set => Set(ref selectedA, value); }
        public int SelectedInt { get => selectedInt; set => Set(ref selectedInt, value); }
        public int Counter { get => counter; set => Set(ref counter, value); }
        public bool CheckBoxIsChecked { get => checkBoxIsChecked; set => Set(ref checkBoxIsChecked, value); }

        public RelayCommand AddB => new(_ => CollectionB.Add(new(this)));
        public RelayCommand AddC => new(_ => CollectionC.Add(new()));
        public RelayCommand ClickCheckBox => new(_ => Counter++);
    }

    public class A : NotifyPropertyChanged
    {
        public string MyProperty1 { get; set; }
        public string MyProperty2 { get; set; }
    }

    public class B : NotifyPropertyChanged
    {
        readonly MainWindowVM mainWindowVM;

        public B(MainWindowVM mainWindowVM) => this.mainWindowVM = mainWindowVM;

        public RelayCommand Remove => new(_ => mainWindowVM.CollectionB.Remove(this));
    }

    public class C : NotifyPropertyChanged
    {
        public RelayCommand Remove => new(_ => MainWindowVM.CollectionC.Remove(this));
    }

    public class Provider
    {
        readonly IEnumerable<int> objects;

        public Provider() => objects = new[] { 1, 2, 3, 4 };

        public IEnumerable<int> GetObjects() => objects;
    }

    public class Static
    {
        public static IEnumerable<int> Collection { get; } = new[] { 1, 2, 3, 4, 5 };
    }
}
