using DatEx.Skelya.GUI.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DatEx.Skelya.GUI.UserControls
{
    public partial class EventsTreeCtrl : UserControl
    {
        public EventsTreeCtrl()
        {
            InitializeComponent();

            ObservableCollection<VM_EventTreeItem> items = new ObservableCollection<VM_EventTreeItem>();
            items.Add(VM_EventTreeItem.GetTestItems());
            Part_Events_Tree.ItemsSource = items;
            Part_Events_Tree.Loaded += OntreeLoaded;
            Part_ExpandLevel_Btn.Click += Part_ExpandLevel_Btn_Click;
            Part_CollapseLevel_Btn.Click += Part_CollapseLevel_Btn_Click;
        }

        private void Part_CollapseLevel_Btn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Action \"Collapse level\" not implemented.");
        }

        private void Part_ExpandLevel_Btn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Action \"Expand level\" not implemented.");
        }

        private void OntreeLoaded(object sender, RoutedEventArgs e)
        {
            FitHeadersWidth(Part_Events_Tree, Part_EventsCaption_Tree);
        }

        private void OnTreeItemExpanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem treeViewItem = e.OriginalSource as TreeViewItem;
            if (treeViewItem == null) return;
            treeViewItem.ItemContainerGenerator.StatusChanged += ItemContainerGenerator_StatusChanged;

            void ItemContainerGenerator_StatusChanged(object sender, EventArgs e)
            {
                ItemContainerGenerator generator = sender as ItemContainerGenerator;
                if (generator.Status != GeneratorStatus.ContainersGenerated) return;
                Dispatcher.BeginInvoke(new Action(() => FitHeadersWidth(Part_Events_Tree, Part_EventsCaption_Tree)));
                generator.StatusChanged -= ItemContainerGenerator_StatusChanged;
            }
        }

        private void OnTreeItemCollapsed(object sender, RoutedEventArgs e)
        {
            FitHeadersWidth(Part_Events_Tree, Part_EventsCaption_Tree);
        }

        private void FitHeadersWidth(TreeView itemsTree, TreeView captionTree)
        {
            if (itemsTree == null) return;

            List<TreeViewItemInfo> treeViewItemInfos = itemsTree.GetUnexpandedItemsInfo<VM_EventTreeItem>(x => x.Descendants);
            List<TextBlock> uiHeaders = new List<TextBlock>();

            Int32 levelOffsetPx = 19;
            Int32 maxRightBound = 0;

            foreach (var x in treeViewItemInfos)
            {
                TextBlock headerCtrl = x.Item.GetVisualChild<TextBlock>(0, 1, 0, 0, 0);
                uiHeaders.Add(headerCtrl);
                Int32 currentLevel = x.Level;
                AlignmentInfo tag = headerCtrl.Tag as AlignmentInfo;
                if (tag == null)
                {
                    tag = new AlignmentInfo() { Level = currentLevel, RightBound = levelOffsetPx * currentLevel + (Int32)Math.Ceiling(headerCtrl.DesiredSize.Width) };
                    headerCtrl.Tag = tag;
                }
                if (tag.Level != currentLevel)
                {
                    Int32 headerInitialWidth = tag.RightBound - levelOffsetPx * tag.Level;
                    tag.Level = currentLevel;
                    tag.RightBound = levelOffsetPx * currentLevel + headerInitialWidth;
                }
                if (tag.RightBound > maxRightBound) maxRightBound = tag.RightBound;
            }

            foreach (var item in uiHeaders)
            {
                AlignmentInfo info = item.Tag as AlignmentInfo;
                Double levelWidth = maxRightBound - info.Level * levelOffsetPx;

                item.SetValue(TextBlock.WidthProperty, levelWidth);
            }

            treeViewItemInfos = captionTree?.GetUnexpandedItemsInfo<VM_EventTreeItem>(x => x.Descendants);
            TextBlock caption = treeViewItemInfos.FirstOrDefault()?.Item?.GetVisualChild<TextBlock>(0, 1, 0, 0, 0);
            if (caption == null) return;
            caption.SetValue(TextBlock.WidthProperty, (Double)maxRightBound);
        }
    }

    public class AlignmentInfo
    {
        public Int32 RightBound { get; set; }

        public Int32 Level { get; set; }
    }

    public class TreeViewItemInfo
    {
        public TreeViewItem Item { get; }

        public Int32 Level { get; }

        public TreeViewItemInfo(TreeViewItem item, Int32 level)
        {
            Item = item;
            Level = level;
        }
    }

    public static class Extensions
    {
        public static List<TreeViewItemInfo> GetUnexpandedItemsInfo<T>(this TreeView treeView, Func<T, ICollection<T>> itemDescendantsSelector)
        {
            List<TreeViewItemInfo> unexpandedItemsInfo = new List<TreeViewItemInfo>();
            if (treeView == null) return unexpandedItemsInfo;
            Stack<T> dataItems = new Stack<T>();
            Stack<TreeViewItemInfo> treeViewItemsInfo = new Stack<TreeViewItemInfo>();
            foreach (var item in treeView.Items) dataItems.Push((T)item);
            //
            //treeView.UpdateLayout();
            while (dataItems.Count > 0)
            {
                T dataItem = dataItems.Pop();
                TreeViewItem treeViewItem = treeView.ItemContainerGenerator.ContainerFromItem(dataItem) as TreeViewItem;
                treeViewItemsInfo.Push(new TreeViewItemInfo(treeViewItem, 0));
            }
            while (treeViewItemsInfo.Count > 0)
            {
                TreeViewItemInfo treeViewItemInfo = treeViewItemsInfo.Pop();
                unexpandedItemsInfo.Add(treeViewItemInfo);
                TreeViewItem treeViewItem = treeViewItemInfo.Item;
                //treeViewItem.UpdateLayout();
                Int32 treeViewItemLevel = treeViewItemInfo.Level;
                if (!treeViewItem.HasItems || !treeViewItem.IsExpanded) continue;
                //
                T dataItem = (T)treeViewItem.DataContext;
                ICollection<T> dataItemDescendants = itemDescendantsSelector(dataItem);
                foreach (var dataItemDescendant in dataItemDescendants)
                {
                    TreeViewItem treeViewItemDescendant = treeViewItem.ItemContainerGenerator.ContainerFromItem(dataItemDescendant) as TreeViewItem;
                    if (treeViewItemDescendant is null) continue;
                    treeViewItemsInfo.Push(new TreeViewItemInfo(treeViewItemDescendant, treeViewItemLevel + 1));
                }
            }
            return unexpandedItemsInfo;
        }

        public static T GetVisualChild<T>(this DependencyObject parentUiItem, Int32 firstIndex, params Int32[] otherIndexes) where T : DependencyObject
        {
            if (parentUiItem == null) return null;
            List<Int32> childIndexes = new List<int> { firstIndex };
            childIndexes.AddRange(otherIndexes);
            DependencyObject visualChild = parentUiItem;
            for (int i = 0; i < childIndexes.Count; i++)
            {
                Int32 childIndex = childIndexes[i];
                Int32 childrenCount = VisualTreeHelper.GetChildrenCount(visualChild);                
                if (childIndex > childrenCount - 1)
                {
                    String exceptionMessage = "";
                    Int32 curItemIndex = 0;
                    foreach (var item in childIndexes)
                    {
                        exceptionMessage += (curItemIndex == i ? $"[{item}]" : item.ToString()) + (curItemIndex != childIndexes.Count - 1 ? ", " : "");
                        ++curItemIndex;
                    }
                    throw new IndexOutOfRangeException(exceptionMessage);
                }
                //
                visualChild = VisualTreeHelper.GetChild(visualChild, childIndex);
                if (visualChild == null) return null;
            }
            return visualChild as T;
        }
    }
}
