using System.Windows.Controls;

namespace Algorithm.Executor.WPF.Tabs
{
    /// <summary>
    /// Interaction logic for GamePartsOverview.xaml
    /// </summary>
    public partial class BlockDetails : UserControl
    {
        public BlockDetails()
        {
            InitializeComponent();

            // TODO: double check it
            DataContext = this;
        }
    }
}
