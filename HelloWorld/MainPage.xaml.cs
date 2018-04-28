using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Import;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using HelloWorld.DATA;
using System.ComponentModel;
using HelloWorld.services;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace HelloWorld
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
        public MainPage()
        {
            InitializeComponent();
            Loaded += MainPage_Loaded;
        }

        #region Delegates

        public event PropertyChangedEventHandler PropertyChanged;

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        #endregion

        public ToDoTask CurrentToDoTask
        {
            get
            {
                return _currentToDoTask;
            }
            set
            {
                _currentToDoTask = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentToDoTask)));
            }
        }

        private int _count;
        private ToDoTask _currentToDoTask;
        
        private async void Refresh()
        {
            var json = await ToDoTaskFileHelper.ReadToDoTaskJsonAsync();
            CurrentToDoTask = ToDoTask.FromJson(json);
        }

        private void UpdateBadge(object sender, RoutedEventArgs e)
        {
            _count++;
            TileService.SetBadgeCountOnTile(_count);
        }

        private void UpdatePrimaryTile(object sender, RoutedEventArgs e)
        {
            XmlDocument xmlDoc = TileService.CreateTiles(new PrimaryTile());

            TileUpdater updater = TileUpdateManager.CreateTileUpdaterForApplication();
            TileNotification notification = new TileNotification(xmlDoc);
            updater.Update(notification);
        }

        private void Notify(object sender, RoutedEventArgs e)
        {
            XmlDocument xmlDoc = ToastService.CreateToast();
            ToastNotifier notifier = ToastNotificationManager.CreateToastNotifier();
            ToastNotification toast = new ToastNotification(xmlDoc);
            notifier.Show(toast);
        }
    }
}