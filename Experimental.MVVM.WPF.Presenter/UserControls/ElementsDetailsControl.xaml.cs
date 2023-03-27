using System;
using System.Windows.Controls;

namespace Demo.UserControls
{
    /// <summary>
    /// Interaction logic for ElementsDetailsControl.xaml
    /// </summary>
    public partial class ElementsDetailsControl : UserControl
    {
        public ElementsDetailsControl()
        {
            InitializeComponent();
            this.ID.Text = Guid.NewGuid().ToString();
        }
    }
}
