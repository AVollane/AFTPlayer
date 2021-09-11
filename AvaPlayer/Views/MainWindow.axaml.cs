using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using AvaPlayer.ViewModels;
using LibVLCSharp.Avalonia;
using LibVLCSharp.Shared;
using System;
using System.Threading;

namespace AvaPlayer.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OnOpened(object sender, EventArgs e)
        {
            var vm = DataContext as MainWindowViewModel;
            vm?.Play();
        }
    }
}
