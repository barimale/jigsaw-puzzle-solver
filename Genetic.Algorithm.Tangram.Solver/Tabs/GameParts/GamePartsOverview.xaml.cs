using System.Windows.Controls;

namespace Algorithm.Executor.WPF.Tabs
{
    /// <summary>
    /// Interaction logic for GamePartsOverview.xaml
    /// </summary>
    public partial class GamePartsOverview : UserControl
    {
        public GamePartsOverview()
        {
            InitializeComponent();

            // TODO: double check it
            DataContext = this;
        }
    }
}
