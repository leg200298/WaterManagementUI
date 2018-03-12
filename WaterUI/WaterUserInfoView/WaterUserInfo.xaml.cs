using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WaterUserInfoView.Model;

namespace WaterUserInfoView
{
    /// <summary>
    /// UserControl1.xaml 的互動邏輯
    /// </summary>
    public partial class WaterUserInfo : UserControl
    {
        ObservableCollection<UserSelectModel> _userSelectModel = new ObservableCollection<UserSelectModel>();
        public WaterUserInfo()
        {
            InitializeComponent();
            UserSelectDataGrid.DataContext = this;
            UserSelectDataGrid.ItemsSource = _userSelectModel;
            _userSelectModel.Add(new UserSelectModel() { Name = "Test", Phone = "0123456789" });
            _userSelectModel.Add(new UserSelectModel() { Name = "Test2", Phone = "1234567890" });
        }
    }
}
