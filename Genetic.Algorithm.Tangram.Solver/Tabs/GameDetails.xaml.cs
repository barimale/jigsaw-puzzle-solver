using System.Windows.Controls;

namespace Algorithm.Executor.WPF.Tabs
{
    /// <summary>
    /// Interaction logic for GameDetails.xaml
    /// </summary>
    public partial class GameDetails : UserControl
    {
        public GameDetails()
        {
            InitializeComponent();

            // TODO: double check it
            DataContext = this;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var i = 0;
        }
    }
}
