using DatEx.Skelya.GUI.CustomCtrls.Commands;
using DatEx.Skelya.GUI.CustomCtrls.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
    [TemplatePart(Name = nameof(Part_UpdateMode_Run), Type = typeof(Run))]
    [TemplatePart(Name = nameof(Part_UpdateIntervalInMin_Run), Type = typeof(Run))]
    [TemplatePart(Name = nameof(Part_EventsDisplayed_Run), Type = typeof(Run))]
    [TemplatePart(Name = nameof(Part_EventsLoaded_Run), Type = typeof(Run))]
    [TemplatePart(Name = nameof(Part_TimeOfFirstEvent_Run), Type = typeof(Run))]
    [TemplatePart(Name = nameof(Part_TimeOfLastEvent_Run), Type = typeof(Run))]
    [TemplatePart(Name = nameof(Part_DesiredStartTime_Run), Type = typeof(Run))]
    [TemplatePart(Name = nameof(Part_DesiredEndTime_Run), Type = typeof(Run))]
    [TemplatePart(Name = nameof(Part_DesiredTimeSpan_Run), Type = typeof(Run))]
    [TemplatePart(Name = nameof(Part_Progress_opProg), Type = typeof(OperationProgressCtrl))]
    public partial class AppMenuCtrl : Control
    {
        public AppMenuCtrl()
        {

        }

        static AppMenuCtrl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AppMenuCtrl), new FrameworkPropertyMetadata(typeof(AppMenuCtrl)));

            #region ————— Local methods ———————————————————————————————————————————————————————————————————————————————
            static DependencyProperty RegisterProperty<T>(String propName, T defaultValue) => DependencyProperty.Register(propName, typeof(T), typeof(AppMenuCtrl), new FrameworkPropertyMetadata(defaultValue));
            #endregion ————— Local methods

            #region ————— Dependency property & routed events registration ————————————————————————————————————————————
            //
            UpdateModeProperty = RegisterProperty<String>(nameof(UpdateMode), "【неизвестно】");
            UpdateIntervalInMinProperty = RegisterProperty<String>(nameof(UpdateIntervalInMin), "【0】");
            EventsDisplayedProperty = RegisterProperty<String>(nameof(EventsDisplayed), "【0】");
            EventsLoadedProperty = RegisterProperty<String>(nameof(EventsLoaded), "【0】");
            TimeOfFirstEventProperty = RegisterProperty<String>(nameof(TimeOfFirstEvent), "【0000.01.01-Пн   00:00】");
            TimeOfLastEventProperty = RegisterProperty<String>(nameof(TimeOfLastEvent), "【0000.01.01-Пн   00:00】");
            DesiredStartTimeProperty = RegisterProperty<String>(nameof(DesiredStartTime), "【0000.01.01-Пн】");
            DesiredEndTimeProperty = RegisterProperty<String>(nameof(DesiredEndTime), "【0000.01.01-Пн】");
            DesiredTimeSpanProperty = RegisterProperty<String>(nameof(DesiredTimeSpan), "【0 д. 0 ч.】");
            //
            #endregion ————— Dependency property & routed events registration
        }
    }
    #endregion ■■■■■ Base



    #region ■■■■■ ControlParts ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
    public partial class AppMenuCtrl
    {
        private Run Part_UpdateMode_Run;
        private Run Part_UpdateIntervalInMin_Run;
        private Run Part_EventsDisplayed_Run;
        private Run Part_EventsLoaded_Run;
        private Run Part_TimeOfFirstEvent_Run;
        private Run Part_TimeOfLastEvent_Run;
        private Run Part_DesiredStartTime_Run;
        private Run Part_DesiredEndTime_Run;
        private Run Part_DesiredTimeSpan_Run;
        private OperationProgressCtrl Part_Progress_opProg;

        private T FindTemplatePart<T>(String templatePartName) where T : DependencyObject
            => (GetTemplateChild(templatePartName) as T) ?? throw new NullReferenceException(templatePartName);

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            //
            Part_UpdateMode_Run = FindTemplatePart<Run>(nameof(Part_UpdateMode_Run));
            Part_UpdateIntervalInMin_Run = FindTemplatePart<Run>(nameof(Part_UpdateIntervalInMin_Run));
            Part_EventsDisplayed_Run = FindTemplatePart<Run>(nameof(Part_EventsDisplayed_Run));
            Part_EventsLoaded_Run = FindTemplatePart<Run>(nameof(Part_EventsLoaded_Run));
            Part_TimeOfFirstEvent_Run = FindTemplatePart<Run>(nameof(Part_TimeOfFirstEvent_Run));
            Part_TimeOfLastEvent_Run = FindTemplatePart<Run>(nameof(Part_TimeOfLastEvent_Run));
            Part_DesiredStartTime_Run = FindTemplatePart<Run>(nameof(Part_DesiredStartTime_Run));
            Part_DesiredEndTime_Run = FindTemplatePart<Run>(nameof(Part_DesiredEndTime_Run));
            Part_DesiredTimeSpan_Run = FindTemplatePart<Run>(nameof(Part_DesiredTimeSpan_Run));
            Part_Progress_opProg = FindTemplatePart<OperationProgressCtrl>(nameof(Part_Progress_opProg));
            //
            SetUpTemplateParts();
        }

        private void SetUpTemplateParts()
        {
            Binding updateModeBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath(nameof(UpdateMode)),
                Mode = BindingMode.OneWay
            };
            BindingOperations.SetBinding(Part_UpdateMode_Run, Run.TextProperty, updateModeBinding);
            //
            //
            Binding updateIntervalInMinBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath(nameof(UpdateIntervalInMin)),
                Mode = BindingMode.OneWay
            };
            BindingOperations.SetBinding(Part_UpdateIntervalInMin_Run, Run.TextProperty, updateIntervalInMinBinding);
            //
            //
            Binding eventsDisplayedBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath(nameof(EventsDisplayed)),
                Mode = BindingMode.OneWay
            };
            BindingOperations.SetBinding(Part_EventsDisplayed_Run, Run.TextProperty, eventsDisplayedBinding);
            //
            //
            Binding eventsLoadedBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath(nameof(EventsLoaded)),
                Mode = BindingMode.OneWay
            };
            BindingOperations.SetBinding(Part_EventsLoaded_Run, Run.TextProperty, eventsLoadedBinding);
            //
            //
            Binding timeOfFirstEventBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath(nameof(TimeOfFirstEvent)),
                Mode = BindingMode.OneWay
            };
            BindingOperations.SetBinding(Part_TimeOfFirstEvent_Run, Run.TextProperty, timeOfFirstEventBinding);
            //
            //
            Binding timeOfLastEventBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath(nameof(TimeOfLastEvent)),
                Mode = BindingMode.OneWay
            };
            BindingOperations.SetBinding(Part_TimeOfLastEvent_Run, Run.TextProperty, timeOfLastEventBinding);
            //
            //
            Binding desiredStartTimeBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath(nameof(DesiredStartTime)),
                Mode = BindingMode.OneWay
            };
            BindingOperations.SetBinding(Part_DesiredStartTime_Run, Run.TextProperty, desiredStartTimeBinding);
            //
            //
            Binding desiredEndTimeBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath(nameof(DesiredEndTime)),
                Mode = BindingMode.OneWay
            };
            BindingOperations.SetBinding(Part_DesiredEndTime_Run, Run.TextProperty, desiredEndTimeBinding);
            //
            //
            Binding desiredTimeSpanBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath(nameof(DesiredTimeSpan)),
                Mode = BindingMode.OneWay
            };
            BindingOperations.SetBinding(Part_DesiredTimeSpan_Run, Run.TextProperty, desiredTimeSpanBinding);
            //
            //
        }
    }
    #endregion ■■■■■ ControlParts



    #region ■■■■■ Properties & Events ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
    public partial class AppMenuCtrl
    {
        #region ————— UpdateMode ——————————————————————————————————————————————————————————————————————————————————————
        public static DependencyProperty UpdateModeProperty;

        public String UpdateMode
        {
            get => (String)GetValue(UpdateModeProperty);
            set => SetValue(UpdateModeProperty, value);
        }
        #endregion ————— UpdateMode

        #region ————— UpdateIntervalInMin —————————————————————————————————————————————————————————————————————————————
        public static DependencyProperty UpdateIntervalInMinProperty;

        public String UpdateIntervalInMin
        {
            get => (String)GetValue(UpdateIntervalInMinProperty);
            set => SetValue(UpdateIntervalInMinProperty, value);
        }
        #endregion ————— UpdateIntervalInMin

        #region ————— EventsDisplayed —————————————————————————————————————————————————————————————————————————————————
        public static DependencyProperty EventsDisplayedProperty;

        public String EventsDisplayed
        {
            get => (String)GetValue(EventsDisplayedProperty);
            set => SetValue(EventsDisplayedProperty, value);
        }
        #endregion ————— EventsDisplayed

        #region ————— EventsLoaded ————————————————————————————————————————————————————————————————————————————————————
        public static DependencyProperty EventsLoadedProperty;

        public String EventsLoaded
        {
            get => (String)GetValue(EventsLoadedProperty);
            set => SetValue(EventsLoadedProperty, value);
        }
        #endregion ————— EventsLoaded

        #region ————— TimeOfFirstEvent ————————————————————————————————————————————————————————————————————————————————
        public static DependencyProperty TimeOfFirstEventProperty;

        public String TimeOfFirstEvent
        {
            get => (String)GetValue(TimeOfFirstEventProperty);
            set => SetValue(TimeOfFirstEventProperty, value);
        }
        #endregion ————— TimeOfFirstEvent

        #region ————— TimeOfLastEvent —————————————————————————————————————————————————————————————————————————————————
        public static DependencyProperty TimeOfLastEventProperty;

        public String TimeOfLastEvent
        {
            get => (String)GetValue(TimeOfLastEventProperty);
            set => SetValue(TimeOfLastEventProperty, value);
        }
        #endregion ————— TimeOfLastEvent

        #region ————— DesiredStartTime ————————————————————————————————————————————————————————————————————————————————
        public static DependencyProperty DesiredStartTimeProperty;

        public String DesiredStartTime
        {
            get => (String)GetValue(DesiredStartTimeProperty);
            set => SetValue(DesiredStartTimeProperty, value);
        }
        #endregion ————— DesiredStartTime

        #region ————— DesiredEndTime ——————————————————————————————————————————————————————————————————————————————————
        public static DependencyProperty DesiredEndTimeProperty;

        public String DesiredEndTime
        {
            get => (String)GetValue(DesiredEndTimeProperty);
            set => SetValue(DesiredEndTimeProperty, value);
        }
        #endregion ————— DesiredEndTime

        #region ————— DesiredTimeSpan —————————————————————————————————————————————————————————————————————————————————
        public static DependencyProperty DesiredTimeSpanProperty;

        public String DesiredTimeSpan
        {
            get => (String)GetValue(DesiredTimeSpanProperty);
            set => SetValue(DesiredTimeSpanProperty, value);
        }
        #endregion ————— DesiredTimeSpan
    }
    #endregion ■■■■■ Properties & Events
}
