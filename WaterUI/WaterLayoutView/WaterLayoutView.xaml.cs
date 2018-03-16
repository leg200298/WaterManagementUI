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
        /// <summary>
        /// 客戶管理子項目的按鈕觸發事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuUserButton_Click(object sender, RoutedEventArgs e)
        {
            Button menuItem = (Button)sender;

            var nameSpace = $"Water{menuItem.Name}View";
            var className = $"Water{menuItem.Name}";
            var dllFilePath = $@"{DynameicLoadingPath}Panel\UserPanel\{nameSpace}.dll";

            var content = AssemblyUserPanel(dllFilePath, nameSpace, className);

            //標籤顯示處理
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
        /// <summary>
        /// 系統管理子項目的按鈕觸發事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuSystemButton_Click(object sender, RoutedEventArgs e)
        {
            
        }
        /// <summary>
        /// 不受關注的Expand會收起來
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// 確定頁面檔案有存在在指定路徑
        /// </summary>
        /// <param name="dllFilePath"></param>
        /// <param name="nameSpace"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        private UserControl AssemblyUserPanel(string dllFilePath, string nameSpace, string className)
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
