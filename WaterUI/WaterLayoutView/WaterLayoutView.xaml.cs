using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
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
using WaterLayoutView.Model;
using WaterStyle;

namespace WaterLayoutView
{
    /// <summary>
    /// UserControl1.xaml 的互動邏輯
    /// </summary>
    public partial class WaterLayoutView : UserControl
    {
        public ObservableCollection<TabItemModel> Tabs = new ObservableCollection<TabItemModel>();
        public string DynameicLoadingPath { get; set; }
        public WaterLayoutView()
        {
            InitializeComponent();
            DataContext = this;

            TabItemModel.ActionTabs = ActionTabs;
            TabItemModel.Tabs = Tabs;
            ActionTabs.ItemsSource = Tabs;
        }

        private void MenuUserButton_Click(object sender, RoutedEventArgs e)
        {
            Button menuItem = (Button)sender;

            var nameSpace = $"Water{menuItem.Name}View";
            var className = $"Water{menuItem.Name}";
            var dllFilePath = $@"{DynameicLoadingPath}Panel\UserPanel\{nameSpace}.dll";

            var content = AssemblyUserPanel(menuItem, dllFilePath, nameSpace, className);

            var tabsItem = Tabs.SingleOrDefault(item => item.Header == menuItem.Content.ToString());
            if (tabsItem == null)
            {
                Tabs.Add(new TabItemModel() { Header = menuItem.Content.ToString(), Content = content });
                ActionTabs.SelectedItem = Tabs.Last();
            }
            else
            {
                ActionTabs.SelectedItem = tabsItem;
            }
        }
        private void MenuSystemButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Menu_Expanded(object sender, RoutedEventArgs e)
        {
            foreach (var item in MenuPanel.Children)
            {
                var expanderObject = (Expander)item;

                if (expanderObject.Name != ((Expander)sender).Name)
                {
                    expanderObject.IsExpanded = false;
                }
            }
        }
        private UserControl AssemblyUserPanel(Button sender, string dllFilePath, string nameSpace, string className)
        {
            if (File.Exists(dllFilePath))
            {
                Assembly assembly = Assembly.LoadFrom(dllFilePath);
                Type type = assembly.GetType($"{nameSpace}.{className}");

                return (UserControl)assembly.CreateInstance(type.FullName, true);
            }
            return new UserControl();
        }
    }
}
