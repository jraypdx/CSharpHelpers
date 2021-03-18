using System;
using System.Collections.Generic;
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

namespace Example_App.UserControls
{
    /// <summary>
    /// Interaction logic for TimedPrompt.xaml
    /// </summary>
    public partial class TimedPrompt : UserControl
    {


        public TimedPrompt()
        {
            InitializeComponent();
        }
    }

	// Note: If ViewModelBase is not present in this project, MVVM property changed event stuff will need to be copied from the MVVM section in to here
    public class TimedPrompt_VM : ViewModels.ViewModelBase 
    {

        private string _Message;
        private string _Time;
        private BitmapImage _PausePlayImage;
        //private bool _DidUserSayYes;
        private Visibility _Vis;
        private Thickness _XY;
        private CancellationTokenSource _TokenSource;
        private CancellationToken _Token;

        public string Message { get => _Message; set { _Message = value; OnPropertyChanged(nameof(Message)); } }
        public string Time { get => _Time; set { _Time = value; OnPropertyChanged(nameof(Time)); } }
        //public bool DidUserSayYes { get => _DidUserSayYes; set { _DidUserSayYes = value; OnPropertyChanged(nameof(DidUserSayYes)); } }
        public Visibility Vis { get => _Vis; set { _Vis = value; OnPropertyChanged(nameof(Vis)); } }
        public Thickness XY { get => _XY; set { _XY = value; OnPropertyChanged(nameof(XY)); } }
        public BitmapImage PausePlayImage { get => _PausePlayImage; set { _PausePlayImage = value; OnPropertyChanged(nameof(PausePlayImage)); } }

        public ICommand Hide_Command { get; }
        public ICommand PlayPause_Command { get; }
        public ICommand StopAll_Command { get; }
        public ICommand KillCurrent_Command { get; }
        public ICommand KillAll_Command { get; }


        public TimedPrompt_VM()
        {
            Vis = Visibility.Collapsed;
            XY = new Thickness(0, 0, 0, 0);

			// Note: Functions had to be scrubbed and will need to be set up or replaced
            Hide_Command = new Tools.DelegateCommand(o => Hide());
            PlayPause_Command = new Tools.DelegateCommand(o => { PausePlay_Function(); Hide(); });
            StopAll_Command = new Tools.DelegateCommand(o => { CancelRunning_Function(); Hide(); });
            KillCurrent_Command = new Tools.DelegateCommand(o => { KillCurrentTest(); Hide(); });
            KillAll_Command = new Tools.DelegateCommand(o => { KillCurrentTest(); CancelRunning_Function(); Hide(); });

        }

        public async Task Start(int seconds, string message)
        {
            Vis = Visibility.Visible;
            PausePlayImage = PausePlayButtonImage;
            Message = message;
            var xy = MainWindowModel.GetMouseXY(); //Static function that finds the mouse XY coords relative to the UserControl or window you are displaying over (example at end)
            XY = new Thickness(xy.X, xy.Y, 0, 0);
            Time = "";
            _TokenSource = new CancellationTokenSource();
            _Token = _TokenSource.Token;

			// Runs for "approximately" the supplied seconds, has a countdown in the top right and then closes when the user selects something or when the time runs out
            await Task.Run(async () => { 
                for (int i = seconds; i > 0; i--)
                {
                    if (_Token.IsCancellationRequested)
                        break;

                    if (i <= 10) //Start a countdown for the last 5 seconds that it's shown
                        Application.Current.Dispatcher.Invoke(delegate { Time = i.ToString(); });

                    for (int t = 0; t < 10; t++) //Break the 1 second wait down in to 10 .1 second waits to make the buttons more responsive
                    {
                        if (_Token.IsCancellationRequested)
                            break;
                        await Task.Delay(100);
                    }
                }
            });

            Application.Current.Dispatcher.Invoke(delegate { Vis = Visibility.Collapsed; });
        }

        public void Hide()
        {
            _TokenSource.Cancel();
            Application.Current.Dispatcher.Invoke(delegate { Vis = Visibility.Collapsed; });
        }

    }

}

// EXAMPLE GetMouseXY() Functions
//
// It's a simple function, and where it gets put depends on what you are displaying the popup over
// In this instance my popup is displaying over a tab's content
//
//        public static System.Windows.Point GetMouseXY()
//        {
//            return Mouse.GetPosition(_items.First().Content);
//        }
//
//
