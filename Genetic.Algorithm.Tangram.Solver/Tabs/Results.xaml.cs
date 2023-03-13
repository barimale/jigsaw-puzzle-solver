using Algorithm.Executor.WPF.Model;
using System.Collections.Generic;
using System.ComponentModel;
using System;
using System.Windows;
using System.Windows.Controls;

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

        public List<AlgorithmResult> ResultItems
        {
            get { return (List<AlgorithmResult>)GetValue(Results.ResultItemsProperty); }
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
