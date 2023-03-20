using System.Collections.Generic;
using System.ComponentModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using Solver.Tangram.AlgorithmDefinitions.Generics;

namespace Algorithm.Executor.WPF.Tabs
{
    /// <summary>
    /// Interaction logic for Results.xaml
    /// </summary>
    public partial class Results : UserControl
    {
        public static readonly DependencyProperty ResultItemsProperty = DependencyProperty
            .Register(
                "ResultItems",
                typeof(List<AlgorithmResult>),
                typeof(Results)
            );

        public ObservableCollection<AlgorithmResult> ResultItems
        {
            get { return (ObservableCollection<AlgorithmResult>)GetValue(Results.ResultItemsProperty); }
            set
            {
                SetValue(Results.ResultItemsProperty, value);
                OnPropertyChanged("ResultItems");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public Results()
        {
            InitializeComponent();

            DataContext = this;

            this.ResultsGrid.ItemsSource = ResultItems;
        }
    }
}
