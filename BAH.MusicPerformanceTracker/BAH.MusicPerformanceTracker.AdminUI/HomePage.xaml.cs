using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BAH.MusicPerformanceTracker.AdminUI
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Window
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void BtnPerformances_Click(object sender, RoutedEventArgs e)
        {
            ManagePerformance managePerformance = new ManagePerformance();
            managePerformance.ShowDialog();
        }

        private void BtnPieces_Click(object sender, RoutedEventArgs e)
        {
            ManagePiece managePiece = new ManagePiece();
            managePiece.ShowDialog();
        }

        private void BtnComposers_Click(object sender, RoutedEventArgs e)
        {
            ManageComposer manageComposer = new ManageComposer();
            manageComposer.ShowDialog();
        }

        private void BtnReporting_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.ShowDialog();
        }
    }
}
