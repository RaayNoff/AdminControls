using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AdminControls
{
    /// <summary>
    /// Логика взаимодействия для AdminControls
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string TimerStartedValue = "00:00:00";
        private Stopwatch _stopwatch;
        private Timer _timer;
        private string TimerValue
        {
            get
            {
                return TimerValue;
            }
            set
            {
                TimerValue = value;
                TimerField.Text = value;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnTimerElapse(object sender, ElapsedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() => TimerField.Text = _stopwatch.Elapsed.ToString(format: @"hh\:mm\:ss\:ff"));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ChangeButtonsStatments(stop: true);
            //TimerValue = TimerStartedValue;

            _stopwatch = new Stopwatch();
            _timer = new Timer(interval: 5);

            _timer.Elapsed += OnTimerElapse;
        }

        private void StartTimer_Click(object sender, RoutedEventArgs e)
        {
            _stopwatch.Start();
            _timer.Start();
            ChangeButtonsStatments(stop: false, start: true);
        }
        private void StopTimer_Click(object sender, RoutedEventArgs e)
        {
            _stopwatch.Stop();
            _timer.Stop();

            ChangeButtonsStatments(stop: true, start: false);
        }

        private void ChangeButtonsStatments(bool start = false, bool stop = false)
        {
            StartTimer.IsEnabled = !start;
            StopTimer.IsEnabled = !stop;
        }

        private void MenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
