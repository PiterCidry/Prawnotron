﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;

namespace Prawnotron
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        List<Ustawa> _listaUst = new List<Ustawa>();
        readonly List<string> _listaTytulow = new List<string>();
        Stopwatch _stopwatch = new Stopwatch();
        Dictionary<string, string> _dic = new Dictionary<string, string>();

        /// <summary>
        /// Konstruktor okna głównego, ustawia ItemSource okienka listy ustaw
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            listBox_listaUstaw.ItemsSource = _listaTytulow;
        }

        async void button_szukajUstawy_Click(object sender, RoutedEventArgs e)
        {
            _stopwatch.Reset();
            _stopwatch.Start();
            try
            {
                _listaUst = await ApiClient.SzukajAsync(_dic);
                Debug.WriteLine(_listaUst.Count);
                foreach (Ustawa u in _listaUst)
                {
                    _listaTytulow.Add(u.Tytul);
                }
                listBox_listaUstaw.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                _stopwatch.Stop();
                ConditionsListView.Items.Refresh();
                Debug.WriteLine(_stopwatch.Elapsed);
            }
        }

        private void button_dodaj_click(object sender, RoutedEventArgs e)
        {
            try
            {
                WyborUstaw wybor = new WyborUstaw();
                if (wybor.ShowDialog() != null)
                {
                    _dic = WyborUstaw.Dic;
                }
                ConditionsListView.ItemsSource = _dic;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                ConditionsListView.Items.Refresh();
            }
        }
    }
}