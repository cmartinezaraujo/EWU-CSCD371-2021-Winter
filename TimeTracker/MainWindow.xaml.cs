using System;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace TimeTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        private DateTime _StartTime;
        private DateTime _EndTime;
        private TimeSpan _TimeDifference;
        private bool _IsReset;
        private string _TimerDescription;

        private DispatcherTimer DispatcherTimer { get; }
        public MainWindow()
        {
            InitializeComponent();

            DispatcherTimer = new();
            DispatcherTimer.Interval = new TimeSpan(1);
            DispatcherTimer.Tick += DispatcherTimer_Tick;
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            
                TimerTextBlock.Text = Convert.ToString(DateTime.Now.Subtract(_StartTime));
 
        }

        private void TimerStartButton_Click(object sender, RoutedEventArgs e)
        {
            if(!DispatcherTimer.IsEnabled)
            {
                DispatcherTimer.Start();
                if (_IsReset)
                {
                    _StartTime = DateTime.Now;
                    _IsReset = false;
                }
                else
                {
                    _StartTime =  DateTime.Now.Add(_TimeDifference);
                }
            }
        }

        private void TimerStopButton_Click(object sender, RoutedEventArgs e)
        {
            if (DispatcherTimer.IsEnabled)
            {
                _EndTime = DateTime.Now;
                _TimeDifference = TimeSpan.FromTicks(_StartTime.Ticks) - TimeSpan.FromTicks(_EndTime.Ticks);
                DispatcherTimer.Stop();
            }
        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            _TimerDescription = TimerDescriptionTextBox.Text;
        }

        private void LogTimer_Click(object sender, RoutedEventArgs e)
        {
            if(DispatcherTimer.IsEnabled)
            {
                PastTimersLog.Items.Add($"{_TimerDescription} {Convert.ToString(DateTime.Now - _StartTime)}");
            }
            else
            {
                DateTime current = DateTime.Now;
                PastTimersLog.Items.Add($"{_TimerDescription} { Convert.ToString(current - current.Add(_TimeDifference) )}");
            }
        }

        private void TimerReset_Click(object sender, RoutedEventArgs e)
        {
            DispatcherTimer.Stop();
            _TimeDifference = TimeSpan.FromTicks(_StartTime.Ticks) - TimeSpan.FromTicks(_StartTime.Ticks);
            TimerTextBlock.Text = "00:00:00:00";
            _IsReset = true;

        }

        private void PastTimersLog_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            if (e.Key == Key.Delete)
            {
                PastTimersLog.Items.Remove(PastTimersLog.Items.OfType<object>().Last());
            }
        }
    }
}
