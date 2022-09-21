﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using Wireguard_FrontEnd.Backend;
using PowershellForCSharp;

namespace Wireguard_FrontEnd
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly VpnSelectionDetection _addToItem = new();
        public object? sent { get; set; }


        public MainWindow()
        {
            InitializeComponent();
            ConfAddItems();
        }

        private void ConfFileComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            sent = sender.ToString();
            StartTunnel.IsEnabled = sent != null;
        }

        private void ConfAddItems()
        {
            var filesFound = _addToItem.FilesListed();
            foreach (var t in filesFound)
                ConfFileComboBox.Items.Add(t);
        }

        private void Start_Tunnel_Click(object sender, RoutedEventArgs e)
        {
            PowershellHandler.RunCommand("& \'C:\\Program Files\\WireGuard\\wireguard.exe\' /installtunnelservice \'C:\\Program Files\\WireGuard\\Data\\Configurations\\" + sent + ".conf.dpapi\'", false, true, true, true);
            //Console.WriteLine(output);

            StartTunnel.Visibility = (Visibility)2;
            StopTunnel.Visibility = (Visibility)0;
        }

        private void StopTunnel_Click(object sender, RoutedEventArgs e)
        {
            PowershellHandler.RunCommand("& \'C:\\Program Files\\WireGuard\\wireguard.exe\' /uninstalltunnelservice \"" + sent + "\"", false, true, true, true);
            //Console.WriteLine(output);

            StartTunnel.Visibility = (Visibility)0;
            StopTunnel.Visibility = (Visibility)2;
        }
    }
}