using DatEx.Skelya.GUI.CustomCtrls.Commands;
using DatEx.Skelya.GUI.CustomCtrls.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;

namespace DatEx.Skelya.GUI.CustomCtrls.Controls
{
    #region ■■■■■ Base ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
    [TemplatePart(Type = typeof(TextBlock), Name = nameof(Part_EventStatus_tBlock))]
    [TemplatePart(Type = typeof(TextBlock), Name = nameof(Part_EventTime_tBlock))]
    [TemplatePart(Type = typeof(TextBlock), Name = nameof(Part_EventPlace_tBlock))]
    [TemplatePart(Type = typeof(TextBlock), Name = nameof(Part_EventDevice_tBlock))]
    [TemplatePart(Type = typeof(TextBlock), Name = nameof(Part_EventDescription_tBlock))]
    [TemplatePart(Type = typeof(Label),     Name = nameof(Part_SnapshotHeader_label))]
    [TemplatePart(Type = typeof(Label),     Name = nameof(Part_ImagePlaceholder_label))]
    [TemplatePart(Type = typeof(Image),     Name = nameof(Part_EventSnapshot_img))]
    [TemplatePart(Type = typeof(TextBlock), Name = nameof(Part_EventId_tBlock))]
    [TemplatePart(Type = typeof(TextBlock), Name = nameof(Part_EventTypeId_tBlock))]
    [TemplatePart(Type = typeof(TextBlock), Name = nameof(Part_EventType_tBlock))]
    [TemplatePart(Type = typeof(TextBlock), Name = nameof(Part_DeviceId_tBlock))]
    [TemplatePart(Type = typeof(TextBlock), Name = nameof(Part_DeviceType_tBlock))]
    [TemplatePart(Type = typeof(TextBlock), Name = nameof(Part_DeviceClass_tBlock))]
    [TemplatePart(Type = typeof(TextBlock), Name = nameof(Part_SnapshotId_tBlock))]
    [TemplatePart(Type = typeof(TextBlock), Name = nameof(Part_SnapshotTime_tBlock))]
    [TemplatePart(Type = typeof(TextBlock), Name = nameof(Part_ShapshotSizes_tBlock))]
    [TemplatePart(Type = typeof(TextBlock), Name = nameof(Part_SnapshotMimeType_tBlock))]
    [TemplatePart(Type = typeof(TextBlock), Name = nameof(Part_SnapshotName_tBlock))]
    [TemplatePart(Type = typeof(ListBox),   Name = nameof(Part_AllDevices_lBox))]
    [TemplatePart(Type = typeof(ListBox),   Name = nameof(Part_Triggers_lBox))]
    [TemplatePart(Type = typeof(TextBlock), Name = nameof(Part_edWeight_tBlock))]
    [TemplatePart(Type = typeof(TextBlock), Name = nameof(Part_edPrevWeight_tBlock))]
    [TemplatePart(Type = typeof(TextBlock), Name = nameof(Part_edTransaction_tBlock))]
    [TemplatePart(Type = typeof(TextBlock), Name = nameof(Part_edPrevTransaction_tBlock))]
    [TemplatePart(Type = typeof(TextBlock), Name = nameof(Part_edTransactionDataCount_tBlock))]
    [TemplatePart(Type = typeof(TextBlock), Name = nameof(Part_edStable_tBlock))]
    [TemplatePart(Type = typeof(TextBlock), Name = nameof(Part_edDuration_tBlock))]
    [TemplatePart(Type = typeof(TextBlock), Name = nameof(Part_edTransactionMaxWeight_tBlock))]
    [TemplatePart(Type = typeof(TextBlock), Name = nameof(Part_edPerimetr_tBlock))]
    [TemplatePart(Type = typeof(TextBlock), Name = nameof(Part_edDriverInCar_tBlock))]
    [TemplatePart(Type = typeof(TextBlock), Name = nameof(Part_edStatus_tBlock))]
    [TemplatePart(Type = typeof(TextBlock), Name = nameof(Part_edReader_tBlock))]
    [TemplatePart(Type = typeof(TextBlock), Name = nameof(Part_edTruck1_tBlock))]
    [TemplatePart(Type = typeof(TextBlock), Name = nameof(Part_edTruck2_tBlock))]
    [TemplatePart(Type = typeof(TextBlock), Name = nameof(Part_edItemName_tBlock))]
    [TemplatePart(Type = typeof(TextBlock), Name = nameof(Part_edOperationName_tBlock))]
    [TemplatePart(Type = typeof(TextBlock), Name = nameof(Part_edOparationType_tBlock))]
    [TemplatePart(Type = typeof(TextBlock), Name = nameof(Part_edOn_tBlock))]
    [TemplatePart(Type = typeof(TextBlock), Name = nameof(Part_edWaybill_tBlock))]
    [TemplatePart(Type = typeof(TextBlock), Name = nameof(Part_edTransactionWaybill_tBlock))]
    [TemplatePart(Type = typeof(TextBlock), Name = nameof(Part_edState_tBlock))]
    [TemplatePart(Type = typeof(TextBlock), Name = nameof(Part_edWeight1_tBlock))]
    [TemplatePart(Type = typeof(TextBlock), Name = nameof(Part_edWeight2_tBlock))]
    [TemplatePart(Type = typeof(TextBlock), Name = nameof(Part_edWeightCenter_tBlock))]
    [TemplatePart(Type = typeof(TextBlock), Name = nameof(Part_edCommand_tBlock))]
    [TemplatePart(Type = typeof(TextBlock), Name = nameof(Part_edUser_tBlock))]
    [TemplatePart(Type = typeof(TextBlock), Name = nameof(Part_edId_tBlock))]
    [TemplatePart(Type = typeof(TextBlock), Name = nameof(Part_edEPC_tBlock))]
    [TemplatePart(Type = typeof(TextBlock), Name = nameof(Part_edAccessLevel_tBlock))]
    [TemplatePart(Type = typeof(TextBlock), Name = nameof(Part_edAmbientTemperature_tBlock))]
    [TemplatePart(Type = typeof(TextBlock), Name = nameof(Part_edAnalysisCounter_tBlock))]
    [TemplatePart(Type = typeof(TextBlock), Name = nameof(Part_edSubsampleId_tBlock))]
    [TemplatePart(Type = typeof(TextBlock), Name = nameof(Part_edSubsampleNrOfSubsamples_tBlock))]
    [TemplatePart(Type = typeof(TextBlock), Name = nameof(Part_edSampleHash_tBlock))]
    [TemplatePart(Type = typeof(TextBlock), Name = nameof(Part_edProtDM_tBlock))]
    [TemplatePart(Type = typeof(TextBlock), Name = nameof(Part_edMoisture_tBlock))]
    [TemplatePart(Type = typeof(TextBlock), Name = nameof(Part_edOilDM_tBlock))]
    [TemplatePart(Type = typeof(TextBlock), Name = nameof(Part_edStarchDM_tBlock))]
    public partial class EventDetailsCtrl : Control
    {
        public EventDetailsCtrl() { }

        static EventDetailsCtrl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EventDetailsCtrl), new FrameworkPropertyMetadata(typeof(EventDetailsCtrl)));

            #region ————— Local methods ———————————————————————————————————————————————————————————————————————————————
            static DependencyProperty RegisterProperty<T>(String propName, T defaultValue, Action<DependencyObject, DependencyPropertyChangedEventArgs> propChangedCallback)
                => DependencyProperty.Register(propName, typeof(T), typeof(EventDetailsCtrl), new FrameworkPropertyMetadata(defaultValue, new PropertyChangedCallback(propChangedCallback)));

            static RoutedEvent RegisterEvent<T>(String handlerName)
                => EventManager.RegisterRoutedEvent(handlerName, RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<T>), typeof(EventDetailsCtrl));
            #endregion ————— Local methods

            #region ————— Dependency property & routed events registration ————————————————————————————————————————————
            //
            EventRecordProperty = RegisterProperty<VM_EventLogRecord>(nameof(EventRecord), default(VM_EventLogRecord), OnDependencyPropChanged_EventRecord);
            EventRecordChangedEvent = RegisterEvent<VM_EventLogRecord>(nameof(EventRecordChanged));
            //
            #endregion ————— Dependency property & routed events registration

            #region ————— Class handlers registraiton —————————————————————————————————————————————————————————————————

            EventManager.RegisterClassHandler(typeof(EventsTreeCtrl), TreeViewItem.ExpandedEvent, new RoutedEventHandler(OnTreeItemExpanded));

            #endregion ————— Class handlers registraiton
        }
    }
    #endregion ■■■■■ Base



    #region ■■■■■ ControlParts ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
    public partial class EventDetailsCtrl
    {
        private TextBlock Part_EventStatus_tBlock; //■
        private TextBlock Part_EventTime_tBlock; //■
        private TextBlock Part_EventPlace_tBlock; //■
        private TextBlock Part_EventDevice_tBlock; //■
        private TextBlock Part_EventDescription_tBlock; //■
        private Label Part_SnapshotHeader_label; //■
        private Label Part_ImagePlaceholder_label;
        private Image Part_EventSnapshot_img; //■
        private TextBlock Part_EventId_tBlock; //■
        private TextBlock Part_EventTypeId_tBlock; //■
        private TextBlock Part_EventType_tBlock;
        private TextBlock Part_DeviceId_tBlock; //■
        private TextBlock Part_DeviceType_tBlock; //■
        private TextBlock Part_DeviceClass_tBlock; //■
        private TextBlock Part_SnapshotId_tBlock; //■
        private TextBlock Part_SnapshotTime_tBlock; //■
        private TextBlock Part_ShapshotSizes_tBlock; //■
        private TextBlock Part_SnapshotMimeType_tBlock; //■
        private TextBlock Part_SnapshotName_tBlock; //■
        private ListBox Part_AllDevices_lBox; //■
        private ListBox Part_Triggers_lBox; //■
        private TextBlock Part_edWeight_tBlock; //■
        private TextBlock Part_edPrevWeight_tBlock; //■
        private TextBlock Part_edTransaction_tBlock; //■
        private TextBlock Part_edPrevTransaction_tBlock; //■
        private TextBlock Part_edTransactionDataCount_tBlock; //■
        private TextBlock Part_edStable_tBlock; //■
        private TextBlock Part_edDuration_tBlock; //■
        private TextBlock Part_edTransactionMaxWeight_tBlock; //■
        private TextBlock Part_edPerimetr_tBlock; //■
        private TextBlock Part_edDriverInCar_tBlock; //■
        private TextBlock Part_edStatus_tBlock; //■
        private TextBlock Part_edReader_tBlock; //■
        private TextBlock Part_edTruck1_tBlock; //■
        private TextBlock Part_edTruck2_tBlock; //■
        private TextBlock Part_edItemName_tBlock; //■
        private TextBlock Part_edOperationName_tBlock; //■
        private TextBlock Part_edOparationType_tBlock; //■
        private TextBlock Part_edOn_tBlock; //■
        private TextBlock Part_edWaybill_tBlock; //■
        private TextBlock Part_edTransactionWaybill_tBlock; //■
        private TextBlock Part_edState_tBlock; //■
        private TextBlock Part_edWeight1_tBlock; //■
        private TextBlock Part_edWeight2_tBlock; //■
        private TextBlock Part_edWeightCenter_tBlock; //■
        private TextBlock Part_edCommand_tBlock; //■
        private TextBlock Part_edUser_tBlock; //■
        private TextBlock Part_edId_tBlock; //■
        private TextBlock Part_edEPC_tBlock; //■
        private TextBlock Part_edAccessLevel_tBlock; //■
        private TextBlock Part_edAmbientTemperature_tBlock; //■
        private TextBlock Part_edAnalysisCounter_tBlock; //■
        private TextBlock Part_edSubsampleId_tBlock; //■
        private TextBlock Part_edSubsampleNrOfSubsamples_tBlock; //■
        private TextBlock Part_edSampleHash_tBlock; //■
        private TextBlock Part_edProtDM_tBlock; //■
        private TextBlock Part_edMoisture_tBlock; //■
        private TextBlock Part_edOilDM_tBlock; //■
        private TextBlock Part_edStarchDM_tBlock; //■

        private T FindTemplatePart<T>(String templatePartName) where T : DependencyObject
            => (GetTemplateChild(templatePartName) as T) ?? throw new NullReferenceException(templatePartName);

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            //
            Part_EventStatus_tBlock = FindTemplatePart<TextBlock>(nameof(Part_EventStatus_tBlock));
            Part_EventTime_tBlock = FindTemplatePart<TextBlock>(nameof(Part_EventTime_tBlock));
            Part_EventPlace_tBlock = FindTemplatePart<TextBlock>(nameof(Part_EventPlace_tBlock));
            Part_EventDevice_tBlock = FindTemplatePart<TextBlock>(nameof(Part_EventDevice_tBlock));
            Part_EventDescription_tBlock = FindTemplatePart<TextBlock>(nameof(Part_EventDescription_tBlock));
            Part_SnapshotHeader_label = FindTemplatePart<Label>(nameof(Part_SnapshotHeader_label));
            Part_ImagePlaceholder_label = FindTemplatePart<Label>(nameof(Part_ImagePlaceholder_label));
            Part_EventSnapshot_img = FindTemplatePart<Image>(nameof(Part_EventSnapshot_img));
            Part_EventId_tBlock = FindTemplatePart<TextBlock>(nameof(Part_EventId_tBlock));
            Part_EventTypeId_tBlock = FindTemplatePart<TextBlock>(nameof(Part_EventTypeId_tBlock));
            Part_EventType_tBlock = FindTemplatePart<TextBlock>(nameof(Part_EventType_tBlock));
            Part_DeviceId_tBlock = FindTemplatePart<TextBlock>(nameof(Part_DeviceId_tBlock));
            Part_DeviceType_tBlock = FindTemplatePart<TextBlock>(nameof(Part_DeviceType_tBlock));
            Part_DeviceClass_tBlock = FindTemplatePart<TextBlock>(nameof(Part_DeviceClass_tBlock));
            Part_SnapshotId_tBlock = FindTemplatePart<TextBlock>(nameof(Part_SnapshotId_tBlock));
            Part_SnapshotTime_tBlock = FindTemplatePart<TextBlock>(nameof(Part_SnapshotTime_tBlock));
            Part_ShapshotSizes_tBlock = FindTemplatePart<TextBlock>(nameof(Part_ShapshotSizes_tBlock));
            Part_SnapshotMimeType_tBlock = FindTemplatePart<TextBlock>(nameof(Part_SnapshotMimeType_tBlock));
            Part_SnapshotName_tBlock = FindTemplatePart<TextBlock>(nameof(Part_SnapshotName_tBlock));
            Part_AllDevices_lBox = FindTemplatePart<ListBox>(nameof(Part_AllDevices_lBox));
            Part_Triggers_lBox = FindTemplatePart<ListBox>(nameof(Part_Triggers_lBox));
            Part_edWeight_tBlock = FindTemplatePart<TextBlock>(nameof(Part_edWeight_tBlock));
            Part_edPrevWeight_tBlock = FindTemplatePart<TextBlock>(nameof(Part_edPrevWeight_tBlock));
            Part_edTransaction_tBlock = FindTemplatePart<TextBlock>(nameof(Part_edTransaction_tBlock));
            Part_edPrevTransaction_tBlock = FindTemplatePart<TextBlock>(nameof(Part_edPrevTransaction_tBlock));
            Part_edTransactionDataCount_tBlock = FindTemplatePart<TextBlock>(nameof(Part_edTransactionDataCount_tBlock));
            Part_edStable_tBlock = FindTemplatePart<TextBlock>(nameof(Part_edStable_tBlock));
            Part_edDuration_tBlock = FindTemplatePart<TextBlock>(nameof(Part_edDuration_tBlock));
            Part_edTransactionMaxWeight_tBlock = FindTemplatePart<TextBlock>(nameof(Part_edTransactionMaxWeight_tBlock));
            Part_edPerimetr_tBlock = FindTemplatePart<TextBlock>(nameof(Part_edPerimetr_tBlock));
            Part_edDriverInCar_tBlock = FindTemplatePart<TextBlock>(nameof(Part_edDriverInCar_tBlock));
            Part_edStatus_tBlock = FindTemplatePart<TextBlock>(nameof(Part_edStatus_tBlock));
            Part_edReader_tBlock = FindTemplatePart<TextBlock>(nameof(Part_edReader_tBlock));
            Part_edTruck1_tBlock = FindTemplatePart<TextBlock>(nameof(Part_edTruck1_tBlock));
            Part_edTruck2_tBlock = FindTemplatePart<TextBlock>(nameof(Part_edTruck2_tBlock));
            Part_edItemName_tBlock = FindTemplatePart<TextBlock>(nameof(Part_edItemName_tBlock));
            Part_edOperationName_tBlock = FindTemplatePart<TextBlock>(nameof(Part_edOperationName_tBlock));
            Part_edOparationType_tBlock = FindTemplatePart<TextBlock>(nameof(Part_edOparationType_tBlock));
            Part_edOn_tBlock = FindTemplatePart<TextBlock>(nameof(Part_edOn_tBlock));
            Part_edWaybill_tBlock = FindTemplatePart<TextBlock>(nameof(Part_edWaybill_tBlock));
            Part_edTransactionWaybill_tBlock = FindTemplatePart<TextBlock>(nameof(Part_edTransactionWaybill_tBlock));
            Part_edState_tBlock = FindTemplatePart<TextBlock>(nameof(Part_edState_tBlock));
            Part_edWeight1_tBlock = FindTemplatePart<TextBlock>(nameof(Part_edWeight1_tBlock));
            Part_edWeight2_tBlock = FindTemplatePart<TextBlock>(nameof(Part_edWeight2_tBlock));
            Part_edWeightCenter_tBlock = FindTemplatePart<TextBlock>(nameof(Part_edWeightCenter_tBlock));
            Part_edCommand_tBlock = FindTemplatePart<TextBlock>(nameof(Part_edCommand_tBlock));
            Part_edUser_tBlock = FindTemplatePart<TextBlock>(nameof(Part_edUser_tBlock));
            Part_edId_tBlock = FindTemplatePart<TextBlock>(nameof(Part_edId_tBlock));
            Part_edEPC_tBlock = FindTemplatePart<TextBlock>(nameof(Part_edEPC_tBlock));
            Part_edAccessLevel_tBlock = FindTemplatePart<TextBlock>(nameof(Part_edAccessLevel_tBlock));
            Part_edAmbientTemperature_tBlock = FindTemplatePart<TextBlock>(nameof(Part_edAmbientTemperature_tBlock));
            Part_edAnalysisCounter_tBlock = FindTemplatePart<TextBlock>(nameof(Part_edAnalysisCounter_tBlock));
            Part_edSubsampleId_tBlock = FindTemplatePart<TextBlock>(nameof(Part_edSubsampleId_tBlock));
            Part_edSubsampleNrOfSubsamples_tBlock = FindTemplatePart<TextBlock>(nameof(Part_edSubsampleNrOfSubsamples_tBlock));
            Part_edSampleHash_tBlock = FindTemplatePart<TextBlock>(nameof(Part_edSampleHash_tBlock));
            Part_edProtDM_tBlock = FindTemplatePart<TextBlock>(nameof(Part_edProtDM_tBlock));
            Part_edMoisture_tBlock = FindTemplatePart<TextBlock>(nameof(Part_edMoisture_tBlock));
            Part_edOilDM_tBlock = FindTemplatePart<TextBlock>(nameof(Part_edOilDM_tBlock));
            Part_edStarchDM_tBlock = FindTemplatePart<TextBlock>(nameof(Part_edStarchDM_tBlock));
            //
            SetUpTemplateParts();
        }

        private void SetUpTemplateParts()
        {
            Binding eventStatusBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath($"{nameof(EventRecord)}.{nameof(EventRecord.EventCriticality)}"),
                Mode = BindingMode.OneWay
                //TODO change EventCriticality type to Enum
                //TODO for view use diferent Icon, Label, and Color
            };
            BindingOperations.SetBinding(Part_EventStatus_tBlock, TextBlock.TextProperty, eventStatusBinding);
            //
            //
            //
            Binding eventTimeBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath($"{nameof(EventRecord)}.{nameof(EventRecord.EventTime)}"),
                Converter = new ValConverter_DateTime_String(),
                ConverterParameter = "yyyy.MM.dd-ddd HH:mm:ss",
                ConverterCulture = new CultureInfo("ru-RU"),
                Mode = BindingMode.OneWay
            };
            BindingOperations.SetBinding(Part_EventTime_tBlock, TextBlock.TextProperty, eventTimeBinding);
            //
            //
            //
            Binding eventPlaceBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath($"{nameof(EventRecord)}.{nameof(EventRecord.DataSectorName)}"),
                Mode = BindingMode.OneWay,
            };
            BindingOperations.SetBinding(Part_EventPlace_tBlock, TextBlock.TextProperty, eventPlaceBinding);
            //
            //
            //
            Binding eventDeviceBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath($"{nameof(EventRecord)}.{nameof(EventRecord.DeviceName)}"),
                Mode = BindingMode.OneWay,
            };
            BindingOperations.SetBinding(Part_EventDevice_tBlock, TextBlock.TextProperty, eventDeviceBinding);
            //
            //
            //            
            Binding eventDescriptionBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath($"{nameof(EventRecord)}.{nameof(EventRecord.EventDescription)}"),
                Mode = BindingMode.OneWay,
            };
            BindingOperations.SetBinding(Part_EventDescription_tBlock, TextBlock.TextProperty, eventDescriptionBinding);
            //
            //
            //
            Binding snapshotHeaderBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath($"{nameof(EventRecord)}.{nameof(EventRecord.EventHasSnapshot)}"),
                //TODO Converter = 
                Mode = BindingMode.OneWay,
            };
            //TODO BindingOperations.SetBinding(Part_SnapshotHeader_lbl, Label.ContentProperty, snapshotHeaderBinding);
            //
            //
            //
            Binding eventSnapshotBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath($"{nameof(EventRecord)}.{nameof(EventRecord.Snapshot)}.{nameof(EventRecord.Snapshot.File)}"),
                //TODO Converter = 
                Mode = BindingMode.OneWay,
            };
            //TODO BindingOperations.SetBinding(Part_EventSnapshot_img, Image.SourceProperty, eventSnapshotBinding);
            //
            //
            //
            Binding eventIdBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath($"{nameof(EventRecord)}.{nameof(EventRecord.EventId)}"),
                Mode = BindingMode.OneWay,
            };
            BindingOperations.SetBinding(Part_EventId_tBlock, TextBlock.TextProperty, eventIdBinding);
            //
            //
            //
            Binding eventTypeBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath($"{nameof(EventRecord)}.{nameof(EventRecord.EventTypeName)}"),
                Mode = BindingMode.OneWay,
            };
            BindingOperations.SetBinding(Part_EventType_tBlock, TextBlock.TextProperty, eventTypeBinding);
            //
            //
            //
            Binding eventTypeIdBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath($"{nameof(EventRecord)}.{nameof(EventRecord.EventTypeId)}"),
                Mode = BindingMode.OneWay,
            };
            BindingOperations.SetBinding(Part_EventTypeId_tBlock, TextBlock.TextProperty, eventTypeIdBinding);
            //
            //
            //
            Binding deviceIdBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath($"{nameof(EventRecord)}.{nameof(EventRecord.DeviceId)}"),
                Mode = BindingMode.OneWay,
            };
            BindingOperations.SetBinding(Part_DeviceId_tBlock, TextBlock.TextProperty, deviceIdBinding);
            //
            //
            //
            Binding deviceTypeBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath($"{nameof(EventRecord)}.{nameof(EventRecord.DeviceTypeName)}"),
                Mode = BindingMode.OneWay,
            };
            BindingOperations.SetBinding(Part_DeviceType_tBlock, TextBlock.TextProperty, deviceTypeBinding);
            //
            //
            //
            Binding deviceClassBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath($"{nameof(EventRecord)}.{nameof(EventRecord.DeviceTypeClassifier)}"),
                Mode = BindingMode.OneWay,
            };
            BindingOperations.SetBinding(Part_DeviceClass_tBlock, TextBlock.TextProperty, deviceClassBinding);
            //
            //
            //
            Binding snapshotIdBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath($"{nameof(EventRecord)}.{nameof(EventRecord.Snapshot)}.{nameof(EventRecord.Snapshot.Id)}"),
                Mode = BindingMode.OneWay,
            };
            BindingOperations.SetBinding(Part_SnapshotId_tBlock, TextBlock.TextProperty, snapshotIdBinding);
            //
            //
            //
            Binding snapshotTimeBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath($"{nameof(EventRecord)}.{nameof(EventRecord.Snapshot)}.{nameof(EventRecord.Snapshot.Time)}"),
                Mode = BindingMode.OneWay,
            };
            BindingOperations.SetBinding(Part_SnapshotTime_tBlock, TextBlock.TextProperty, snapshotTimeBinding);
            //
            //
            //
            Binding snapshotSizesBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath($"{nameof(EventRecord)}.{nameof(EventRecord.Snapshot)}.{nameof(EventRecord.Snapshot.SizePx)}"),
                //TODO Converter =                
                Mode = BindingMode.OneWay,
            };
            BindingOperations.SetBinding(Part_ShapshotSizes_tBlock, TextBlock.TextProperty, snapshotSizesBinding);
            //
            //
            //
            Binding snapshotMimeTypeBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath($"{nameof(EventRecord)}.{nameof(EventRecord.Snapshot)}.{nameof(EventRecord.Snapshot.MimeType)}"),
                Mode = BindingMode.OneWay,
            };
            BindingOperations.SetBinding(Part_SnapshotMimeType_tBlock, TextBlock.TextProperty, snapshotMimeTypeBinding);
            //
            //
            //
            Binding snapshotName = new Binding
            {
                Source = this,
                Path = new PropertyPath($"{nameof(EventRecord)}.{nameof(EventRecord.Snapshot)}.{nameof(EventRecord.Snapshot.Name)}"),
                Mode = BindingMode.OneWay,
            };
            BindingOperations.SetBinding(Part_SnapshotMimeType_tBlock, TextBlock.TextProperty, snapshotName);
            //
            //
            //
            Binding allDevicesBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath($"{nameof(EventRecord)}.{nameof(EventRecord.AllDevices)}"),
                Mode = BindingMode.OneWay,
            };
            BindingOperations.SetBinding(Part_AllDevices_lBox, ListBox.ItemsSourceProperty, allDevicesBinding);
            //
            //
            //
            Binding triggersBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath($"{nameof(EventRecord)}.{nameof(EventRecord.EventTypeTriggers)}"),
                Mode = BindingMode.OneWay,
            };
            BindingOperations.SetBinding(Part_Triggers_lBox, ListBox.ItemsSourceProperty, triggersBinding);
            //
            //
            //
            Binding edWeightBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath($"{nameof(EventRecord)}.{nameof(EventRecord.Data)}.{nameof(EventRecord.Data.Weight)}"),
                //TODO converter(double)
                Mode = BindingMode.OneWay,
            };
            BindingOperations.SetBinding(Part_edWeight_tBlock, TextBlock.TextProperty, edWeightBinding);
            //
            //
            //
            Binding edPrevWeightBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath($"{nameof(EventRecord)}.{nameof(EventRecord.Data)}.{nameof(EventRecord.Data.PrevWeight)}"),
                //TODO converter(double)
                Mode = BindingMode.OneWay,
            };
            BindingOperations.SetBinding(Part_edPrevWeight_tBlock, TextBlock.TextProperty, edPrevWeightBinding);
            //
            //
            //
            Binding edTransactionBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath($"{nameof(EventRecord)}.{nameof(EventRecord.Data)}.{nameof(EventRecord.Data.Transaction)}"),
                Mode = BindingMode.OneWay,
            };
            BindingOperations.SetBinding(Part_edTransaction_tBlock, TextBlock.TextProperty, edTransactionBinding);
            //
            //
            //
            Binding edPrevTransactionBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath($"{nameof(EventRecord)}.{nameof(EventRecord.Data)}.{nameof(EventRecord.Data.PrevTransaction)}"),
                Mode = BindingMode.OneWay,
            };
            BindingOperations.SetBinding(Part_edPrevTransaction_tBlock, TextBlock.TextProperty, edPrevTransactionBinding);
            //
            //
            //
            Binding edTransactionDataCount = new Binding
            {
                Source = this,
                Path = new PropertyPath($"{nameof(EventRecord)}.{nameof(EventRecord.Data)}.{nameof(EventRecord.Data.TransactionDataCount)}"),
                Mode = BindingMode.OneWay,
            };
            BindingOperations.SetBinding(Part_edTransactionDataCount_tBlock, TextBlock.TextProperty, edTransactionDataCount);
            //
            //
            //
            Binding edStableBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath($"{nameof(EventRecord)}.{nameof(EventRecord.Data)}.{nameof(EventRecord.Data.Stable)}"),
                Mode = BindingMode.OneWay,
            };
            BindingOperations.SetBinding(Part_edStable_tBlock, TextBlock.TextProperty, edStableBinding);
            //
            //
            //
            Binding edDurationBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath($"{nameof(EventRecord)}.{nameof(EventRecord.Data)}.{nameof(EventRecord.Data.Duration)}"),
                Mode = BindingMode.OneWay,
            };
            BindingOperations.SetBinding(Part_edDuration_tBlock, TextBlock.TextProperty, edDurationBinding);
            //
            //
            //
            Binding edTransactionMaxWeightBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath($"{nameof(EventRecord)}.{nameof(EventRecord.Data)}.{nameof(EventRecord.Data.TransactionMaxWeight)}"),
                Mode = BindingMode.OneWay,
            };
            BindingOperations.SetBinding(Part_edTransactionMaxWeight_tBlock, TextBlock.TextProperty, edTransactionMaxWeightBinding);
            //
            //
            //
            Binding edPerimeterBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath($"{nameof(EventRecord)}.{nameof(EventRecord.Data)}.{nameof(EventRecord.Data.Perimetr)}"),
                Mode = BindingMode.OneWay,
            };
            BindingOperations.SetBinding(Part_edPerimetr_tBlock, TextBlock.TextProperty, edPerimeterBinding);
            //
            //
            //
            Binding edDriverInCarBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath($"{nameof(EventRecord)}.{nameof(EventRecord.Data)}.{nameof(EventRecord.Data.DriverInCar)}"),
                Mode = BindingMode.OneWay,
            };
            BindingOperations.SetBinding(Part_edDriverInCar_tBlock, TextBlock.TextProperty, edDriverInCarBinding);
            //
            //
            //
            Binding edStatusBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath($"{nameof(EventRecord)}.{nameof(EventRecord.Data)}.{nameof(EventRecord.Data.Status)}"),
                Mode = BindingMode.OneWay,
            };
            BindingOperations.SetBinding(Part_edStatus_tBlock, TextBlock.TextProperty, edStatusBinding);
            //
            //
            //
            Binding edReaderBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath($"{nameof(EventRecord)}.{nameof(EventRecord.Data)}.{nameof(EventRecord.Data.Reader)}"),
                Mode = BindingMode.OneWay,
            };
            BindingOperations.SetBinding(Part_edReader_tBlock, TextBlock.TextProperty, edReaderBinding);
            //
            //
            //
            Binding edTruck1Binding = new Binding
            {
                Source = this,
                Path = new PropertyPath($"{nameof(EventRecord)}.{nameof(EventRecord.Data)}.{nameof(EventRecord.Data.Truck1)}"),
                Mode = BindingMode.OneWay,
            };
            BindingOperations.SetBinding(Part_edTruck1_tBlock, TextBlock.TextProperty, edTruck1Binding);
            //
            //
            //
            Binding edTruck2Binding = new Binding
            {
                Source = this,
                Path = new PropertyPath($"{nameof(EventRecord)}.{nameof(EventRecord.Data)}.{nameof(EventRecord.Data.Truck2)}"),
                Mode = BindingMode.OneWay,
            };
            BindingOperations.SetBinding(Part_edTruck2_tBlock, TextBlock.TextProperty, edTruck2Binding);
            //
            //
            //
            Binding edItemNameBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath($"{nameof(EventRecord)}.{nameof(EventRecord.Data)}.{nameof(EventRecord.Data.ItemName)}"),
                Mode = BindingMode.OneWay,
            };
            BindingOperations.SetBinding(Part_edItemName_tBlock, TextBlock.TextProperty, edItemNameBinding);
            //
            //
            //
            Binding edOperationNameBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath($"{nameof(EventRecord)}.{nameof(EventRecord.Data)}.{nameof(EventRecord.Data.OperationName)}"),
                Mode = BindingMode.OneWay,
            };
            BindingOperations.SetBinding(Part_edOperationName_tBlock, TextBlock.TextProperty, edOperationNameBinding);
            //
            //
            //
            Binding edOperationTypeBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath($"{nameof(EventRecord)}.{nameof(EventRecord.Data)}.{nameof(EventRecord.Data.OperationType)}"),
                Mode = BindingMode.OneWay,
            };
            BindingOperations.SetBinding(Part_edOparationType_tBlock, TextBlock.TextProperty, edOperationTypeBinding);
            //
            //
            //
            Binding edOnBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath($"{nameof(EventRecord)}.{nameof(EventRecord.Data)}.{nameof(EventRecord.Data.On)}"),
                Mode = BindingMode.OneWay,
            };
            BindingOperations.SetBinding(Part_edOn_tBlock, TextBlock.TextProperty, edOnBinding);
            //
            //
            //
            Binding edWayBillBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath($"{nameof(EventRecord)}.{nameof(EventRecord.Data)}.{nameof(EventRecord.Data.Waybill)}"),
                Mode = BindingMode.OneWay,
            };
            BindingOperations.SetBinding(Part_edWaybill_tBlock, TextBlock.TextProperty, edWayBillBinding);
            //
            //
            //
            Binding edTransactionWayBillBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath($"{nameof(EventRecord)}.{nameof(EventRecord.Data)}.{nameof(EventRecord.Data.TransactionWaybill)}"),
                Mode = BindingMode.OneWay,
            };
            BindingOperations.SetBinding(Part_edTransactionWaybill_tBlock, TextBlock.TextProperty, edTransactionWayBillBinding);
            //
            //
            //
            Binding edStateBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath($"{nameof(EventRecord)}.{nameof(EventRecord.Data)}.{nameof(EventRecord.Data.State)}"),
                Mode = BindingMode.OneWay,
            };
            BindingOperations.SetBinding(Part_edState_tBlock, TextBlock.TextProperty, edStateBinding);
            //
            //
            //
            Binding edWeight1Binding = new Binding
            {
                Source = this,
                Path = new PropertyPath($"{nameof(EventRecord)}.{nameof(EventRecord.Data)}.{nameof(EventRecord.Data.Weight1)}"),
                Mode = BindingMode.OneWay,
            };
            BindingOperations.SetBinding(Part_edWeight1_tBlock, TextBlock.TextProperty, edWeight1Binding);
            //
            //
            //
            Binding edWeight2Binding = new Binding
            {
                Source = this,
                Path = new PropertyPath($"{nameof(EventRecord)}.{nameof(EventRecord.Data)}.{nameof(EventRecord.Data.Weight2)}"),
                Mode = BindingMode.OneWay,
            };
            BindingOperations.SetBinding(Part_edWeight2_tBlock, TextBlock.TextProperty, edWeight2Binding);
            //
            //
            //
            Binding edWeightCenterBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath($"{nameof(EventRecord)}.{nameof(EventRecord.Data)}.{nameof(EventRecord.Data.WeightCenter)}"),
                Mode = BindingMode.OneWay,
            };
            BindingOperations.SetBinding(Part_edWeightCenter_tBlock, TextBlock.TextProperty, edWeightCenterBinding);
            //
            //
            //
            Binding edCommandBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath($"{nameof(EventRecord)}.{nameof(EventRecord.Data)}.{nameof(EventRecord.Data.Command)}"),
                Mode = BindingMode.OneWay,
            };
            BindingOperations.SetBinding(Part_edCommand_tBlock, TextBlock.TextProperty, edCommandBinding);
            //
            //
            //
            Binding edUserBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath($"{nameof(EventRecord)}.{nameof(EventRecord.Data)}.{nameof(EventRecord.Data.User)}"),
                Mode = BindingMode.OneWay,
            };
            BindingOperations.SetBinding(Part_edUser_tBlock, TextBlock.TextProperty, edUserBinding);
            //
            //
            //
            Binding edIdBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath($"{nameof(EventRecord)}.{nameof(EventRecord.Data)}.{nameof(EventRecord.Data.Id)}"),
                Mode = BindingMode.OneWay,
            };
            BindingOperations.SetBinding(Part_edId_tBlock, TextBlock.TextProperty, edIdBinding);
            //
            //
            //
            Binding edEPCBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath($"{nameof(EventRecord)}.{nameof(EventRecord.Data)}.{nameof(EventRecord.Data.EPC)}"),
                Mode = BindingMode.OneWay,
            };
            BindingOperations.SetBinding(Part_edEPC_tBlock, TextBlock.TextProperty, edEPCBinding);
            //
            //
            //
            Binding edAccessLevelBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath($"{nameof(EventRecord)}.{nameof(EventRecord.Data)}.{nameof(EventRecord.Data.Accesslevel)}"),
                Mode = BindingMode.OneWay,
            };
            BindingOperations.SetBinding(Part_edAccessLevel_tBlock, TextBlock.TextProperty, edAccessLevelBinding);
            //
            //
            //
            Binding edAmbientTemperatureBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath($"{nameof(EventRecord)}.{nameof(EventRecord.Data)}.{nameof(EventRecord.Data.AmbietTemperature)}"),
                Mode = BindingMode.OneWay,
            };
            BindingOperations.SetBinding(Part_edAmbientTemperature_tBlock, TextBlock.TextProperty, edAmbientTemperatureBinding);
            //
            //
            //
            Binding edAnalysisCounterBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath($"{nameof(EventRecord)}.{nameof(EventRecord.Data)}.{nameof(EventRecord.Data.AnalysisCounter)}"),
                Mode = BindingMode.OneWay,
            };
            BindingOperations.SetBinding(Part_edAnalysisCounter_tBlock, TextBlock.TextProperty, edAnalysisCounterBinding);
            //
            //
            //
            Binding edSubsampleIdBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath($"{nameof(EventRecord)}.{nameof(EventRecord.Data)}.{nameof(EventRecord.Data.Subsample_id)}"),
                Mode = BindingMode.OneWay,
            };
            BindingOperations.SetBinding(Part_edSubsampleId_tBlock, TextBlock.TextProperty, edSubsampleIdBinding);
            //
            //
            //
            Binding edSubsampleNrOfSubsamblesBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath($"{nameof(EventRecord)}.{nameof(EventRecord.Data)}.{nameof(EventRecord.Data.SubsampleNrOFSubSamples)}"),
                Mode = BindingMode.OneWay,
            };
            BindingOperations.SetBinding(Part_edSubsampleNrOfSubsamples_tBlock, TextBlock.TextProperty, edSubsampleNrOfSubsamblesBinding);
            //
            //
            //
            Binding edSampleHashBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath($"{nameof(EventRecord)}.{nameof(EventRecord.Data)}.{nameof(EventRecord.Data.SampleHash)}"),
                Mode = BindingMode.OneWay,
            };
            BindingOperations.SetBinding(Part_edSampleHash_tBlock, TextBlock.TextProperty, edSampleHashBinding);
            //
            //
            //
            Binding edProtDmBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath($"{nameof(EventRecord)}.{nameof(EventRecord.Data)}.{nameof(EventRecord.Data.ProtDM)}"),
                Mode = BindingMode.OneWay,
            };
            BindingOperations.SetBinding(Part_edProtDM_tBlock, TextBlock.TextProperty, edProtDmBinding);
            //
            //
            //
            Binding edMoistureBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath($"{nameof(EventRecord)}.{nameof(EventRecord.Data)}.{nameof(EventRecord.Data.Moisture)}"),
                Mode = BindingMode.OneWay,
            };
            BindingOperations.SetBinding(Part_edMoisture_tBlock, TextBlock.TextProperty, edMoistureBinding);
            //
            //
            //
            Binding edOilDMBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath($"{nameof(EventRecord)}.{nameof(EventRecord.Data)}.{nameof(EventRecord.Data.OilDM)}"),
                Mode = BindingMode.OneWay,
            };
            BindingOperations.SetBinding(Part_edOilDM_tBlock, TextBlock.TextProperty, edOilDMBinding);
            //
            Binding edStarchDMBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath($"{nameof(EventRecord)}.{nameof(EventRecord.Data)}.{nameof(EventRecord.Data.StarchDM)}"),
                Mode = BindingMode.OneWay,
            };
            BindingOperations.SetBinding(Part_edStarchDM_tBlock, TextBlock.TextProperty, edStarchDMBinding);
        }

        private String GetToolTipText(RoutedUICommand command)
        {
            KeyGesture keyGesture = command.InputGestures[0] as KeyGesture;
            return keyGesture == null ? command.Text : $"{command.Text} [{keyGesture.GetDisplayStringForCulture(CultureInfo.CurrentCulture)}]";
        }
    }
    #endregion ■■■■■ ControlParts



    #region ■■■■■ Properties & Events ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
    public partial class EventDetailsCtrl
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
            EventDetailsCtrl ctrl = sender as EventDetailsCtrl;
            if(ctrl == null) return;
            VM_EventLogRecord oldValue = (VM_EventLogRecord)e.OldValue;
            VM_EventLogRecord newValue = (VM_EventLogRecord)e.NewValue;
            RoutedPropertyChangedEventArgs<VM_EventLogRecord> args = new RoutedPropertyChangedEventArgs<VM_EventLogRecord>(oldValue, newValue);
            args.RoutedEvent = EventDetailsCtrl.EventRecordChangedEvent;
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



    #region ■■■■■ Class handlers ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
    public partial class EventDetailsCtrl
    {
        #region ————— ClassHandler ————————————————————————————————————————————————————————————————————————————————

        private static void OnTreeItemExpanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem treeViewItem = e.OriginalSource as TreeViewItem;
            if(treeViewItem == null) return;
            treeViewItem.ItemContainerGenerator.StatusChanged += ItemContainerGenerator_StatusChanged;

            void ItemContainerGenerator_StatusChanged(object sender, EventArgs e)
            {
                ItemContainerGenerator generator = sender as ItemContainerGenerator;
                if(generator.Status != GeneratorStatus.ContainersGenerated) return;
                Application.Current.Dispatcher.BeginInvoke(new Action(() => MessageBox.Show("Item expanded")));
                generator.StatusChanged -= ItemContainerGenerator_StatusChanged;
            }
        }

        #endregion ————— ClassHandler
    }
    #endregion ■■■■■ Class handlers



    #region ■■■■■ Commands ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
    public partial class EventDetailsCtrl
    {
        #region ————— ResetValue ——————————————————————————————————————————————————————————————————————————————————————

        private static void ResetValue_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show($"Command {((RoutedUICommand)e.Command).Text} not implemented");
        }

        private static void ResetValue_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = false;
        }

        #endregion ————— ResetValue
    }
    #endregion ■■■■■ Commands
}