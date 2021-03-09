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
        private DateTime startTime;
        private DateTime endTime;
        private TimeSpan timeDifference;
        private bool isReset;
        private string timerDescription;

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
            
                TimerTextBlock.Text = Convert.ToString(DateTime.Now.Subtract(startTime));
 
        }

        private void TimerStartButton_Click(object sender, RoutedEventArgs e)
        {
            if(!DispatcherTimer.IsEnabled)
            {
                DispatcherTimer.Start();
                if (isReset)
                {
                    startTime = DateTime.Now;
                    isReset = false;
                }
                else
                {
                    startTime =  DateTime.Now.Add(timeDifference);
                }
            }
        }

        private void TimerStopButton_Click(object sender, RoutedEventArgs e)
        {
            if (DispatcherTimer.IsEnabled)
            {
                endTime = DateTime.Now;
                timeDifference = TimeSpan.FromTicks(startTime.Ticks) - TimeSpan.FromTicks(endTime.Ticks);
                DispatcherTimer.Stop();
            }
        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            timerDescription = TimerDescriptionTextBox.Text;
        }

        private void LogTimer_Click(object sender, RoutedEventArgs e)
        {
            if(DispatcherTimer.IsEnabled)
            {
                PastTimersLog.Items.Add($"{timerDescription} {Convert.ToString(DateTime.Now - startTime)}");
            }
            else
            {
                DateTime current = DateTime.Now;
                PastTimersLog.Items.Add($"{timerDescription} { Convert.ToString(current - current.Add(timeDifference) )}");
            }
        }

        private void TimerReset_Click(object sender, RoutedEventArgs e)
        {
            DispatcherTimer.Stop();
            timeDifference = TimeSpan.FromTicks(startTime.Ticks) - TimeSpan.FromTicks(startTime.Ticks);
            TimerTextBlock.Text = "00:00:00:00";
            isReset = true;

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
