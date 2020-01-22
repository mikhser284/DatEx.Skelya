using DatEx.Skelya.GUI.CustomCtrls.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DatEx.Skelya.GUI.CustomCtrls.Controls
{
    #region ■■■■■ Base ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
    [TemplatePart(Name = nameof(Part_CommentsList_lView), Type = typeof(ListView))]
    [TemplatePart(Name = nameof(Part_CommentsCount_tBlock), Type = typeof(TextBlock))]
    public partial class EventCommentsCtrl : Control
    {
        static EventCommentsCtrl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EventCommentsCtrl), new FrameworkPropertyMetadata(typeof(EventCommentsCtrl)));

            #region ————— Local methods ———————————————————————————————————————————————————————————————————————————————
            static DependencyProperty RegisterProperty<T>(String propName, T defaultValue, Action<DependencyObject, DependencyPropertyChangedEventArgs> propChangedCallback)
                => DependencyProperty.Register(propName, typeof(T), typeof(EventCommentsCtrl), new FrameworkPropertyMetadata(defaultValue, new PropertyChangedCallback(propChangedCallback)));

            static RoutedEvent RegisterEvent<T>(String handlerName)
                => EventManager.RegisterRoutedEvent(handlerName, RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<T>), typeof(EventCommentsCtrl));
            #endregion ————— Local methods

            #region ————— Dependency property & routed events registration ————————————————————————————————————————————
            //
            EventRecordProperty = RegisterProperty<VM_EventLogRecord>(nameof(EventRecord), default(VM_EventLogRecord), OnDependencyPropChanged_EventRecord);
            EventRecordChangedEvent = RegisterEvent<VM_EventLogRecord>(nameof(EventRecordChanged));
            //
            #endregion ————— Dependency property & routed events registration
        }
    }
    #endregion ■■■■■ Base


    #region ■■■■■ ControlParts ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
    public partial class EventCommentsCtrl
    {
        private ListView Part_CommentsList_lView;
        private TextBlock Part_CommentsCount_tBlock;

        private T FindTemplatePart<T>(String templatePartName) where T : DependencyObject
            => (GetTemplateChild(templatePartName) as T) ?? throw new NullReferenceException(templatePartName);

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            Part_CommentsList_lView = FindTemplatePart<ListView>(nameof(Part_CommentsList_lView));
            Part_CommentsCount_tBlock = FindTemplatePart<TextBlock>(nameof(Part_CommentsCount_tBlock));
            //
            SetUpTemplateParts();
        }

        private void SetUpTemplateParts()
        {
            Binding commentsListBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath($"{nameof(EventRecord)}.{nameof(EventRecord.Comments)}"),
                Mode = BindingMode.OneWay
            };
            BindingOperations.SetBinding(Part_CommentsList_lView, ListView.ItemsSourceProperty, commentsListBinding);
            //
            //
            //
            Binding commentsCountBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath($"{nameof(EventRecord)}.{nameof(EventRecord.Comments)}.{nameof(EventRecord.Comments.Count)}"),
                Converter = new ValConverter_Int32_AsCommentsCount(),
                Mode = BindingMode.OneWay
            };
            BindingOperations.SetBinding(Part_CommentsCount_tBlock, TextBlock.TextProperty, commentsCountBinding);
            //
        }
    }
    #endregion ■■■■■ ControlParts



    #region ■■■■■ Properties & Events ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
    public partial class EventCommentsCtrl
    {
        #region ————— EventRecord —————————————————————————————————————————————————————————————————————————————————————
        public static DependencyProperty EventRecordProperty;

        public VM_EventLogRecord EventRecord
        {
            get => (VM_EventLogRecord)GetValue(EventRecordProperty);
            set => SetValue(EventRecordProperty, value);
        }

        private static void OnDependencyPropChanged_EventRecord(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            EventCommentsCtrl ctrl = sender as EventCommentsCtrl;
            if(ctrl == null) return;
            VM_EventLogRecord oldValue = (VM_EventLogRecord)e.OldValue;
            VM_EventLogRecord newValue = (VM_EventLogRecord)e.NewValue;
            RoutedPropertyChangedEventArgs<VM_EventLogRecord> args = new RoutedPropertyChangedEventArgs<VM_EventLogRecord>(oldValue, newValue);
            args.RoutedEvent = EventCommentsCtrl.EventRecordChangedEvent;
            ctrl.RaiseEvent(args);
        }

        public static readonly RoutedEvent EventRecordChangedEvent;

        public event RoutedPropertyChangedEventHandler<VM_EventLogRecord> EventRecordChanged
        {
            add => AddHandler(EventRecordChangedEvent, value);
            remove => RemoveHandler(EventRecordChangedEvent, value);
        }
        #endregion ————— EventRecord
    }
    #endregion ■■■■■ Properties & Events
}
