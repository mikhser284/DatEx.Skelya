using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
using DatEx.Skelya;
using DatEx.Skelya.DataModel;
//using DatEx.Skelya.GUI.ViewModel;
using Newtonsoft.Json;

namespace DatEx.Skelya.GUI
{

    //delegate void UpdateProgressBarDelegate(DependencyProperty dp, object value);


    //public partial class MainWindow__ : Window
    //{
    //    public static AppSettings AppConfig = null;
    //    public static SkeliaClient Client = null;

    //    //public ObservableCollection<VM_DataSector> DataSectors = new ObservableCollection<VM_DataSector>();
    //    //public ObservableCollection<VM_DeviceType> DeviceTypes = new ObservableCollection<VM_DeviceType>();
    //    //public ObservableCollection<VM_EventLogRecord> EventLogRecords = new ObservableCollection<VM_EventLogRecord>();
    //    public ViewSnapshotDialog LargeSnapshotWindow = null;
    //    public MainWindow__()
    //    {
    //        InitializeComponent();

    //        //Part_DataGrid.SelectionChanged += Part_DataGrid_SelectionChanged;
    //        //Part_EventSnapshotImg.MouseLeftButtonUp += Part_EventSnapshotImg_MouseLeftButtonUp;
    //        //Closed += MainWindow_Closed;
    //        //Part_DataGrid.ItemsSource = EventLogRecords;
    //        //Part_DataSectorsTree.ItemsSource = DataSectors;
    //        //Part_DevTypesList.ItemsSource = DeviceTypes;
    //        // -----
    //        AppConfig = AppSettings.Load();
    //        Client = new SkeliaClient(AppConfig.HttpAddressOf.SkelyaServer);

    //        //DateTime today = DateTime.Now.TruncateToDay() - TimeSpan.FromHours(26);
    //        //DateTime yesterday = today + TimeSpan.FromDays(2);



    //        //List<EventLogRecord> eventLogRecords = Client.GetEventLogRecords(today, yesterday).Items;
    //        ////List<EventLogRecord> eventLogRecords = Client.GetEventLogRecords().Items;
    //        //Part_DataGrid.ItemsSource = eventLogRecords;
    //        LoadDataSectors();
    //        LoadData();


    //    }

    //    private void MainWindow_Closed(object sender, EventArgs e)
    //    {
    //        if (LargeSnapshotWindow != null) LargeSnapshotWindow.Close();
    //    }

    //    private void Part_EventSnapshotImg_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    //    {
    //        Image img = sender as Image;
    //        if (img == null) return;

    //        LargeSnapshotWindow = new ViewSnapshotDialog();

    //        LargeSnapshotWindow.Part_SnapshotImg.Source = img.Source;
    //        LargeSnapshotWindow.Show();
    //    }

    //    private async void LoadDataSectors()
    //    {
    //        await Task.Run(() =>
    //        {
    //            String dirPath = System.IO.Path.Combine(AppConfig.FolderPathOf.RootStorageFolder, AppConfig.FolderPathOf.DataSectorsFolder);
    //            if (!Directory.Exists(dirPath)) Directory.CreateDirectory(dirPath);
    //            String filePath = System.IO.Path.Combine(dirPath, "DataSectors.json");
    //            List<DataSector> dataSectors = Client.GetDataSectors().Items;

    //            using (StreamWriter stream = File.CreateText(filePath))
    //            {
    //                JsonSerializer serializer = JsonSerializer.Create(new JsonSerializerSettings() { Formatting = Formatting.Indented, NullValueHandling = NullValueHandling.Ignore });
    //                serializer.Serialize(stream, dataSectors);
    //            }

    //        });

    //        //
    //        await Task.Run(() =>
    //        {
    //            String dirPath = System.IO.Path.Combine(AppConfig.FolderPathOf.RootStorageFolder, AppConfig.FolderPathOf.DataSectorsFolder);
    //            if (!Directory.Exists(dirPath)) return;
    //            String filePath = System.IO.Path.Combine(dirPath, "DataSectors.json");

    //            List<DataSector> dataSectors = new List<DataSector>();
    //            using (StreamReader file = File.OpenText(filePath))
    //            {
    //                JsonSerializer serializer = new JsonSerializer();
    //                dataSectors.AddRange((List<DataSector>)serializer.Deserialize(file, typeof(List<DataSector>)));
    //            }
    //            Dictionary<Int32, List<VM_Device>> devices = LoadDevices();
    //            Dictionary<Int32, VM_DeviceType> devTypes = new Dictionary<int, VM_DeviceType>();
    //            foreach(var devs in devices.Values)
    //            {
    //                foreach(var dev in devs)
    //                {
    //                    if (devTypes.ContainsKey(dev.Type.Id)) continue;
    //                    devTypes.Add(dev.Type.Id, new VM_DeviceType(dev.Type));
    //                }
    //            }

    //            Dispatcher.Invoke(() =>
    //            {
    //                DataSectors.Clear();
    //                dataSectors.ForEach(x => DataSectors.Add(new VM_DataSector(x)));
    //                foreach (var x in DataSectors)
    //                {
    //                    if (!devices.ContainsKey(x.Id)) continue;
    //                    foreach(var dev in devices[x.Id])
    //                        x.Devices.Add(dev);
    //                }
    //                DeviceTypes.Clear();
    //                foreach (var devType in devTypes.Values) DeviceTypes.Add(devType);
    //            });
    //        });
    //    }

    //    private Dictionary<Int32, List<VM_Device>> LoadDevices()
    //    {
    //        Task<List<Device>> getDevices = new Task<List<Device>>(() =>
    //        {
    //            List<Device> devices = new List<Device>();
    //            String dirPath = System.IO.Path.Combine(AppConfig.FolderPathOf.RootStorageFolder, AppConfig.FolderPathOf.DevicesFolder);
    //            if (!Directory.Exists(dirPath)) Directory.CreateDirectory(dirPath);
    //            String filePath = System.IO.Path.Combine(dirPath, "Devices.json");
    //            devices = Client.GetDevices().Items.Select(x => x.Device).ToList();

    //            using (StreamWriter stream = File.CreateText(filePath))
    //            {
    //                JsonSerializer serializer = JsonSerializer.Create(new JsonSerializerSettings() { Formatting = Formatting.Indented, NullValueHandling = NullValueHandling.Ignore });
    //                serializer.Serialize(stream, devices);
    //            }
    //            return devices;
    //        });
    //        getDevices.Start();

    //        Dictionary<Int32, List<VM_Device>> devicesDict = new Dictionary<int, List<VM_Device>>();
    //        foreach(var dev in getDevices.Result)
    //        {
    //            if (!devicesDict.ContainsKey(dev.DataSector.Id))
    //                devicesDict.Add(dev.DataSector.Id, new List<VM_Device>());
    //            devicesDict[dev.DataSector.Id].Add(new VM_Device(dev));
    //        }
    //        return devicesDict;
    //    }



    //    private async void LoadData()
    //    {
    //        String eventsDir = System.IO.Path.Combine(AppConfig.FolderPathOf.RootStorageFolder, AppConfig.FolderPathOf.EventLogRecordsFolder);
    //        if (!Directory.Exists(eventsDir)) return;
    //        await DownLoadEventLogRecordsInBackground();
    //        await Task.Run(() =>
    //        {
    //            List<EventLogRecord> events = new List<EventLogRecord>();
    //            foreach (var fileName in Directory.GetFiles(eventsDir))
    //            {
    //                using (StreamReader file = File.OpenText(fileName))
    //                {
    //                    JsonSerializer serializer = new JsonSerializer();
    //                    events.AddRange((List<EventLogRecord>)serializer.Deserialize(file, typeof(List<EventLogRecord>)));
    //                }
    //            }
    //            events = events.OrderByDescending(x => x.DateTime).ToList();
    //            Dispatcher.Invoke(() => 
    //            { 
    //                EventLogRecords.Clear();
    //                events.ForEach(x => EventLogRecords.Add(new VM_EventLogRecord(x)));                
    //            });
    //        });
    //        ObtainImagesAndSaveToDisk(EventLogRecords);
    //    }

    //    private async Task DownLoadEventLogRecordsInBackground()
    //    {
    //        Part_OperationNameLbl.Content = "Загрузка событий";
    //        Part_OperationNameLbl.Visibility = Visibility.Visible;
    //        Part_OperationProgresPercentLbl.Visibility = Visibility.Visible;

    //        Part_OperationProgressProgBar.Visibility = Visibility.Visible;
    //        Part_OperationProgressProgBar.IsIndeterminate = false;
    //        Part_OperationProgressProgBar.Value = 0;
    //        Part_OperationProgressProgBar.Minimum = 0;

    //        String dir = System.IO.Path.Combine(AppConfig.FolderPathOf.RootStorageFolder, AppConfig.FolderPathOf.EventLogRecordsFolder);
    //        if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
    //        DateTime currentDate = DateTime.Now.TruncateToDay() - TimeSpan.FromHours(24);
    //        DateTime endDate = currentDate + TimeSpan.FromDays(2);
    //        DateTime? startDate = null;
    //        Int32? totalDays = null;
    //        var progress = new Progress<int>(value => Part_OperationProgressProgBar.Value = value);

    //        await Task.Run(() =>
    //        {
    //            List<EventLogRecord> eventLogRecords = Client.GetEventLogRecords(offset: 0, limit: 1, orderDate: "asc").Items;
    //            if (eventLogRecords != null && eventLogRecords.Count > 0)
    //            {
    //                startDate = eventLogRecords.First().DateTime;
    //                totalDays = (Int32)Math.Floor((endDate - (DateTime)startDate).TotalDays);                    
    //            }
    //            if (totalDays == null) return;
    //            for (int dayOffset = 0; dayOffset < totalDays;)
    //            {
    //                DateTime timeIntervalStart = (DateTime)startDate + TimeSpan.FromDays(dayOffset);
    //                LoadEventLogBy(timeIntervalStart, dir);
    //                ((IProgress<int>)progress).Report(++dayOffset);
    //                Dispatcher.Invoke(() =>
    //                {
    //                    Part_OperationProgressProgBar.Maximum = (Int32)totalDays;
    //                    //Part_OperationProgressProgBar.Value = ;
    //                    Part_OperationNameLbl.Content = $"Загрузка событий ({totalDays - dayOffset})";
    //                    Double percent = (Double)dayOffset * 100.00 / (Double)totalDays;
    //                    Part_OperationProgresPercentLbl.Content = $"{percent:00.00} %";
    //                });
    //            }
    //        });

    //        Part_OperationNameLbl.Content = "Загрузка событий завершена";
    //        Part_OperationNameLbl.Visibility = Visibility.Collapsed;
    //        Part_OperationProgressProgBar.Visibility = Visibility.Collapsed;
    //        Part_OperationProgresPercentLbl.Visibility = Visibility.Collapsed;

    //    }

    //    private void LoadEventLogBy(DateTime date, String directoryPath)
    //    {
    //        DateTime endTime = date + TimeSpan.FromDays(1);
    //        String fileName = System.IO.Path.Combine(directoryPath, $"{date:yyyy-MM-dd}.json");
    //        if (File.Exists(fileName)) return;
    //        List<EventLogRecord> eventLogRecords = Client.GetEventLogRecords(startDate: date, endDate: endTime).Items;
    //        using (StreamWriter stream = File.CreateText(fileName))
    //        {
    //            JsonSerializer serializer = JsonSerializer.Create(new JsonSerializerSettings() { Formatting = Formatting.Indented, NullValueHandling = NullValueHandling.Ignore });
    //            serializer.Serialize(stream, eventLogRecords);
    //        }
    //    }

    //    private void Part_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    //    {
    //        Part_EventSnapshotImg.Source = null;
    //        Part_EventSnapshotImg.Visibility = Visibility.Collapsed;
    //        Part_ImagePlaceholderLbl.Visibility = Visibility.Visible;

    //        VM_EventLogRecord selectedRecord = Part_DataGrid.SelectedItem as VM_EventLogRecord;
    //        if (selectedRecord == null) return;
    //        String pathToImageFile = System.IO.Path.Combine(AppConfig.FolderPathOf.RootStorageFolder, AppConfig.FolderPathOf.SnapshotsFolder, $"{selectedRecord.EventId}.json");
    //        if(File.Exists(pathToImageFile))
    //        {
    //            Snapshot eventSnapshot = null;
    //            using (StreamReader file = File.OpenText(pathToImageFile))
    //            {
    //                JsonSerializer serializer = new JsonSerializer();
    //                eventSnapshot = (Snapshot)serializer.Deserialize(file, typeof(Snapshot));
    //            }
    //            String imageInBase64 = eventSnapshot.File;
    //            if (String.IsNullOrEmpty(imageInBase64)) return;
    //            using (MemoryStream stream = FromBase64(imageInBase64))
    //            {
    //                BitmapImage img = ToBitmapImage(stream);
    //                Part_EventSnapshotImg.Source = img;
    //                if(LargeSnapshotWindow != null) LargeSnapshotWindow.Part_SnapshotImg.Source = img;
    //            }
    //        }
    //        Part_EventSnapshotImg.Visibility = Visibility.Visible;
    //        Part_ImagePlaceholderLbl.Visibility = Visibility.Collapsed;
    //    }


    //    private async void ObtainImagesAndSaveToDisk(ICollection<VM_EventLogRecord> eventLogRecords)
    //    {
    //        var progress = new Progress<int>(value => Part_OperationProgressProgBar.Value = value);
    //        await Task.Run(() =>
    //        {
    //            String dir = System.IO.Path.Combine(AppConfig.FolderPathOf.RootStorageFolder, AppConfig.FolderPathOf.SnapshotsFolder);
    //            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);


    //            Dispatcher.Invoke(() =>
    //            {
    //                Part_OperationNameLbl.Content = "Загрузка изображений";
    //                Part_OperationNameLbl.Visibility = Visibility.Visible;
    //                Part_OperationProgresPercentLbl.Visibility = Visibility.Visible;

    //                Part_OperationProgressProgBar.Visibility = Visibility.Visible;
    //                Part_OperationProgressProgBar.IsIndeterminate = false;
    //                Part_OperationProgressProgBar.Value = 0;
    //                Part_OperationProgressProgBar.Minimum = 0;
    //                Part_OperationProgressProgBar.Maximum = Part_DataGrid.Items.Count;
    //            });

    //            Int32 progressStatus = 0;
    //            foreach (var item in eventLogRecords)
    //            {
    //                LoadImage(item, dir);
    //                ((IProgress<int>)progress).Report(++progressStatus);
    //                Dispatcher.Invoke(() =>
    //                {
    //                    Part_OperationNameLbl.Content = $"Загрузка изображений ({eventLogRecords.Count - progressStatus})";
    //                    Double percent = (Double)progressStatus * 100.00 / (Double)eventLogRecords.Count;
    //                    Part_OperationProgresPercentLbl.Content = $"{percent:00.00} %";
    //                });
    //            }

    //            Dispatcher.Invoke(() =>
    //            {
    //                Part_OperationNameLbl.Content = "Загрузка изображений завершена";
    //                Part_OperationNameLbl.Visibility = Visibility.Collapsed;
    //                Part_OperationProgressProgBar.Visibility = Visibility.Collapsed;
    //                Part_OperationProgresPercentLbl.Visibility = Visibility.Collapsed;
    //            });
    //        });
    //    }

    //    private void LoadImage(VM_EventLogRecord eventLogRecord, String dir)
    //    {
    //        String snapshotFile = System.IO.Path.Combine(dir, $"{eventLogRecord.EventId}.json");
    //        if (File.Exists(snapshotFile))
    //        {
    //            using (StreamReader file = File.OpenText(snapshotFile))
    //            {
    //                JsonSerializer serializer = new JsonSerializer();
    //                Snapshot eventImg = (Snapshot)serializer.Deserialize(file, typeof(Snapshot));
    //                eventLogRecord.EventSnapshotId = eventImg.Id;
    //            }
    //            return;
    //        };
    //        Snapshot snapshot = null;
    //        var response = Client.GetSnapshot(logId: eventLogRecord.EventId)?.Items;
    //        if (response == null || response.Count == 0)
    //        {
    //            snapshot = new Snapshot
    //            {
    //                Date = eventLogRecord.EventTime,
    //                EventLogRecordId = eventLogRecord.EventId,
    //                File = null,
    //                Id = 0,
    //                MimeType = null,
    //                Name = null
    //            };
    //            eventLogRecord.EventSnapshotId = snapshot.Id;
    //        }
    //        else
    //        {
    //            eventLogRecord.EventSnapshotId = -1;
    //            snapshot = response.First();
    //            snapshot.EventLogRecordId = eventLogRecord.EventId;
    //        }

    //        using (StreamWriter stream = File.CreateText(snapshotFile))
    //        {
    //            JsonSerializer serializer = JsonSerializer.Create(new JsonSerializerSettings() { Formatting = Formatting.Indented, NullValueHandling = NullValueHandling.Ignore });
    //            serializer.Serialize(stream, snapshot);
    //        }
    //    }



    //    private void Part_GetEventImageBtn_Click(object sender, RoutedEventArgs e)
    //    {
    //        Part_EventSnapshotImg.Source = null;
    //        Part_EventSnapshotImg.Visibility = Visibility.Collapsed;
    //        Part_ImagePlaceholderLbl.Visibility = Visibility.Visible;
    //        var selectedRecord = Part_DataGrid.SelectedItem as EventLogRecord;
    //        if (selectedRecord == null) return;
    //        var response = Client.GetSnapshot(logId: selectedRecord.Id)?.Items;
    //        if (response == null || response.Count == 0) return;
    //        String imageInBase64 = response.First().File;
    //        if (String.IsNullOrEmpty(imageInBase64)) return;
    //        using (MemoryStream stream = FromBase64(imageInBase64))
    //        {
    //            Part_EventSnapshotImg.Source = ToBitmapImage(stream);
    //        }
    //        Part_EventSnapshotImg.Visibility = Visibility.Visible;
    //        Part_ImagePlaceholderLbl.Visibility = Visibility.Collapsed;
    //    }

    //    public static MemoryStream FromBase64(string content)
    //    {
    //        return new MemoryStream(Convert.FromBase64String(content));
    //    }

    //    public static BitmapImage ToBitmapImage(Stream stream)
    //    {
    //        try
    //        {
    //            var bitmap = new BitmapImage();
    //            bitmap.BeginInit();
    //            bitmap.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
    //            bitmap.CacheOption = BitmapCacheOption.OnLoad;
    //            bitmap.StreamSource = stream;
    //            bitmap.EndInit();
    //            return bitmap;
    //        }
    //        catch (Exception)
    //        {


    //        }

    //        return null;
    //    }
    //}

    //public static class Ext_DateTime
    //{
    //    public static DateTime TruncateToYear(this DateTime dt) => new DateTime(dt.Year, 1, 1);
    //    public static DateTime TruncateToMonth(this DateTime dt) => new DateTime(dt.Year, dt.Month, 1);

    //    public static DateTime TruncateToDay(this DateTime dt) => new DateTime(dt.Year, dt.Month, dt.Day);

    //    public static DateTime TruncateToHour(this DateTime dt) => new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, 0, 0);

    //    public static DateTime TruncateToMinute(this DateTime dt) => new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, 0);
    //}

}
