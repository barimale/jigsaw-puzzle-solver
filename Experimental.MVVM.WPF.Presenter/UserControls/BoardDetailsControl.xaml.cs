using System;
using System.Windows.Controls;

namespace Demo.UserControls
{
    /// <summary>
    /// Interaction logic for BoardDetailsControl.xaml
    /// </summary>
    public partial class BoardDetailsControl : UserControl
    {
        public BoardDetailsControl()
        {
            InitializeComponent();
            this.ID.Text = Guid.NewGuid().ToString();
        }
    }
}
