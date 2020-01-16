using DatEx.Skelya.DataModel;
//using DatEx.Skelya.GUI.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DatEx.Skelya.GUI.Dialogs
{
    /// <summary>
    /// Interaction logic for ViewSnapshotDialog.xaml
    /// </summary>
    public partial class ViewSnapshotDialog : Window
    {
        public ViewSnapshotDialog()
        {
            InitializeComponent();
        }

        public void InitializeImage(String pathToImageFile)
        {
            if (!File.Exists(pathToImageFile)) return;
            Snapshot eventSnapshot = null;
            using (StreamReader file = File.OpenText(pathToImageFile))
            {
                JsonSerializer serializer = new JsonSerializer();
                eventSnapshot = (Snapshot)serializer.Deserialize(file, typeof(Snapshot));
            }
            String imageInBase64 = eventSnapshot.File;
            if (String.IsNullOrEmpty(imageInBase64)) return;
            using (MemoryStream stream = FromBase64(imageInBase64))
            {
                //Part_SnapshotImg = new Image();
                Part_SnapshotImg.Source = ToBitmapImage(stream);
            }
        }


        private static MemoryStream FromBase64(string content)
        {
            return new MemoryStream(Convert.FromBase64String(content));
        }

        private static BitmapImage ToBitmapImage(Stream stream)
        {
            try
            {
                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.StreamSource = stream;
                bitmap.EndInit();
                return bitmap;
            }
            catch (Exception)
            {


            }

            return null;
        }
    }
}
