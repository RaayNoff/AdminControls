using Microsoft.Build.Tasks;
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
        private enum TimerStatement
        {
            ENABLE_TIMER,
            PAUSE_TIMER,
            KILL_TIMER
        }

        private const string TimerStartingValue = "00:00:00:00";
        private Stopwatch _stopwatch;
        private Timer _timer;
        private bool IsPaused = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetTimerStartingValue();
            SetButtonsEnabled(start: true);
        }

        private void StartTimer_Click(object sender, RoutedEventArgs e)
        {
            SetTimerEnabled(TimerStatement.ENABLE_TIMER);
            SetButtonsEnabled(pause: true, stop: true);
        }
        private void PauseTimer_Click(object sender, RoutedEventArgs e)
        {
            SetTimerEnabled(TimerStatement.PAUSE_TIMER);
            SetButtonsEnabled(start: true, stop: true);
        }

        private void StopTimer_Click(object sender, RoutedEventArgs e)
        {
            SetTimerEnabled(TimerStatement.KILL_TIMER);
            SetButtonsEnabled(start: true);
        }
        private void MenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SetTimerEnabled(TimerStatement statement)
        {
            switch (statement)
            {
                case TimerStatement.ENABLE_TIMER:
                    {
                        CreateNewTimer();
                        _stopwatch.Start();
                        _timer.Start();
                        SetPaused(false);
                        break;
                    }
                case TimerStatement.PAUSE_TIMER:
                    {
                        _stopwatch.Stop();
                        _timer.Stop();
                        SetPaused(true);
                        break;
                    }
                case TimerStatement.KILL_TIMER:
                    {
                        _stopwatch.Stop();
                        _timer.Close();
                        SetTimerStartingValue();
                        SetPaused(false);
                        break;
                    }
                default:
                    break;
            }
        }

        private void CreateNewTimer()
        {
            if (StartTimer.IsEnabled && !PauseTimer.IsEnabled && !StopTimer.IsEnabled && !GetPaused()) NewInstance();
            else if (StartTimer.IsEnabled && !PauseTimer.IsEnabled && StartTimer.IsEnabled && !GetPaused()) NewInstance();

            void NewInstance()
            {
                _stopwatch = new Stopwatch();
                _timer = new Timer(interval: 5);
                _timer.Elapsed += OnTimerElapse;
            }
        }

        private void SetButtonsEnabled(bool start = false, bool pause = false, bool stop = false)
        {
            StartTimer.IsEnabled = start;
            PauseTimer.IsEnabled = pause;
            StopTimer.IsEnabled = stop;
        }

        private void SetTimerStartingValue()
        {
            TimerField.Text = TimerStartingValue;
        }

        private void OnTimerElapse(object sender, ElapsedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() => TimerField.Text = _stopwatch.Elapsed.ToString(format: @"hh\:mm\:ss\:ff"));
        }

        private bool GetPaused() => IsPaused;
        private void SetPaused(bool IsPaused) => this.IsPaused = IsPaused;
    }
}
