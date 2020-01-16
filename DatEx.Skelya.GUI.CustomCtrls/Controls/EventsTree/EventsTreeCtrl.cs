using DatEx.Skelya.GUI.CustomCtrls.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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

namespace DatEx.Skelya.GUI.CustomCtrls
{
    #region ■■■■■ Base ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■

    [TemplatePart(Name = nameof(Part_ExpandLevel_Btn), Type = typeof(Button))]
    [TemplatePart(Name = nameof(Part_Level), Type = typeof(TextBlock))]
    [TemplatePart(Name = nameof(Part_CollapseLevel_Btn), Type = typeof(Button))]
    [TemplatePart(Name = nameof(Part_EventsCaption_Tree), Type = typeof(TreeView))]
    [TemplatePart(Name = nameof(Part_Events_Tree), Type = typeof(TreeView))]

    public partial class EventsTreeCtrl : Control
    {
        public EventsTreeCtrl()
        {

        }

        static EventsTreeCtrl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EventsTreeCtrl), new FrameworkPropertyMetadata(typeof(EventsTreeCtrl)));

            
            
        
        
            

            //#region ————— Dependency property registration ————————————————————————————————————————————————————————————

            //StringPropProperty = DependencyProperty.Register(nameof(StringProp), typeof(String), typeof(TagsTreeCtrl),
            //    new FrameworkPropertyMetadata(default(String), new PropertyChangedCallback(OnDependencyPropChanged_StringProp)));

            //#endregion ————— Dependency property registration

            //#region ————— Routed events registraiton ——————————————————————————————————————————————————————————————————

            //StringPropChangedEvent = EventManager.RegisterRoutedEvent(nameof(StringPropChanged), RoutingStrategy.Bubble,
            //    typeof(RoutedPropertyChangedEventArgs<String>), typeof(TagsTreeCtrl));

            //#endregion ————— Routed events registraiton

            //#region ————— Commands registration ———————————————————————————————————————————————————————————————————————

            //BindCommand(TagsTreeCtrlCommands.CollapseAllButThis, CollapseAllButThis_Executed, CollapseAllButThis_CanExecute);
            //BindCommand(TagsTreeCtrlCommands.CollapseAll, CollapseAll_Executed, CollapseAll_CanExecute);
            //BindCommand(TagsTreeCtrlCommands.ExpandAll, ExpandAll_Executed, ExpandAll_CanExecute);
            //// -----
            //BindCommand(TagsTreeCtrlCommands.AutoClearCheckMarks, AutoClearCheckMarks_Executed, AutoClearCheckMarks_CanExecute);
            //BindCommand(TagsTreeCtrlCommands.ClearAllCheckMarks, ClearAllCheckMark_Executed, ClearAllCheckMark_CanExecute);
            //BindCommand(TagsTreeCtrlCommands.InvertCheckMark, InvertCheckMark_Executed, InvertCheckMark_CanExecute);
            //BindCommand(TagsTreeCtrlCommands.SetCheckMark, SetCheckMark_Executed, SetCheckMark_CanExecute);
            //BindCommand(TagsTreeCtrlCommands.RemoveCheckMark, RemoveCheckMark_Executed, RemoveCheckMark_CanExecute);
            //// -----
            //BindCommand(TagsTreeCtrlCommands.InfiniteSearch, InfiniteSearch_Executed, InfiniteSearch_CanExecute);
            //BindCommand(TagsTreeCtrlCommands.MarkCheckedAsIncludable, MarkCheckedAsIncludable_Executed, MarkCheckedAsIncludable_CanExecute);
            //BindCommand(TagsTreeCtrlCommands.MarkCheckedAsExcludable, MarkCheckedAsExcludable_Executed, MarkCheckedAsExcludable_CanExecute);
            //BindCommand(TagsTreeCtrlCommands.MarkAllAsUndefined, MarkAllAsUndefined_Executed, MarkAllAsUndefined_CanExecute);
            //// -----
            //BindCommand(TagsTreeCtrlCommands.Bind, Bind_Executed, Bind_CanExecute);
            //BindCommand(TagsTreeCtrlCommands.Unbind, Unbind_Executed, Unbind_CanExecute);
            //// -----
            //BindCommand(TagsTreeCtrlCommands.AddTag, AddTag_Executed, AddTag_CanExecute);
            //BindCommand(TagsTreeCtrlCommands.AddDir, AddDir_Executed, AddDir_CanExecute);
            //BindCommand(TagsTreeCtrlCommands.RenameSelected, RenameSelected_Executed, RenameSelected_CanExecute);
            //BindCommand(TagsTreeCtrlCommands.DeleteSelected, DeleteSelected_Executed, DeleteSelected_CanExecute);
            //BindCommand(TagsTreeCtrlCommands.DeleteChecked, DeleteChecked_Executed, DeleteChecked_CanExecute);

            //#endregion ————— Commands registration
        }

        private static void BindCommand(RoutedCommand command, ExecutedRoutedEventHandler executedHandler, CanExecuteRoutedEventHandler canExecuteHandler)
        {
            CommandManager.RegisterClassCommandBinding(typeof(EventsTreeCtrl), new CommandBinding(command, executedHandler, canExecuteHandler));
        }


    }
    #endregion ■■■■■ Base


    #region ■■■■■ ControlParts ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
    public partial class EventsTreeCtrl
    {
        private Button Part_ExpandLevel_Btn;
        private TextBlock Part_Level;
        private Button Part_CollapseLevel_Btn;
        private TreeView Part_EventsCaption_Tree;
        private TreeView Part_Events_Tree;
        
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            Part_ExpandLevel_Btn = FindTemplatePart<Button>(nameof(Part_ExpandLevel_Btn));
            Part_Level = FindTemplatePart<TextBlock>(nameof(Part_Level));
            Part_CollapseLevel_Btn = FindTemplatePart<Button>(nameof(Part_CollapseLevel_Btn));
            Part_EventsCaption_Tree = FindTemplatePart<TreeView>(nameof(Part_EventsCaption_Tree));
            Part_Events_Tree = FindTemplatePart<TreeView>(nameof(Part_Events_Tree));
            //
            SetUpTemplateParts();
        }

        private T FindTemplatePart<T>(String templatePartName) where T : DependencyObject
            => (GetTemplateChild(templatePartName) as T) ?? throw new NullReferenceException(templatePartName);

        private String GetToolTipText(RoutedUICommand command)
        {
            KeyGesture keyGesture = command.InputGestures[0] as KeyGesture;
            return keyGesture == null ? command.Text : $"{command.Text} [{keyGesture.GetDisplayStringForCulture(CultureInfo.CurrentCulture)}]";
        }

        private void SetUpTemplateParts()
        {
            ObservableCollection<VM_EventTreeItem> items = new ObservableCollection<VM_EventTreeItem>();
            items.Add(VM_EventTreeItem.GetTestItems());

            EventManager.RegisterClassHandler(typeof(EventsTreeCtrl), TreeViewItem.ExpandedEvent, new RoutedEventHandler(OnTreeItemExpanded));
            EventManager.RegisterClassHandler(typeof(EventsTreeCtrl), TreeViewItem.CollapsedEvent, new RoutedEventHandler(OnTreeItemCollapsed));
            //Part_EventsCaption_Tree.Tre
            Part_Events_Tree.ItemsSource = items;
            Part_Events_Tree.Loaded += OntreeLoaded;
            Part_ExpandLevel_Btn.Click += Part_ExpandLevel_Btn_Click;
            Part_CollapseLevel_Btn.Click += Part_CollapseLevel_Btn_Click;
        }

    }
    #endregion ■■■■■ ControlParts


    public partial class EventsTreeCtrl : Control
    {
        

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
