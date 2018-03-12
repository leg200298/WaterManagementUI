using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using WaterLayoutView.Method;

namespace WaterLayoutView.Model
{
    public class TabItemModel
    {
        public string Header { get; set; }
        public UserControl Content { get; set; }

        public static TabControl ActionTabs { get; set; }
        public static ObservableCollection<TabItemModel> Tabs { get; set; }
        
        public ICommand AddTabPanel
        {
            get
            {
                return new RelayCommand(AddPanelExecute, CanAddTabPanelExecute);
            }
        }
        private bool CanAddTabPanelExecute(object obj)
        {
            return true;
        }
        private void AddPanelExecute(object obj)
        {
            var item = (TabItemModel)ActionTabs.SelectedItem;
            item.Content = null;

            Tabs.RemoveAt(ActionTabs.SelectedIndex);
            GC.Collect();
        }
    }
}
