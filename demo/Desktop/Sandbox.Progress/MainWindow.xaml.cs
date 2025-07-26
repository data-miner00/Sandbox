namespace Sandbox.Progress
{
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IDisposable
    {
        private CancellationTokenSource tokenSource;
        private bool isDisposed;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void btnLoop_Click(object sender, RoutedEventArgs e)
        {
            this.tokenSource = new();

            var progress = new Progress<int>(value =>
            {
                this.progressBar.Value = value;
                this.lblProgress.Content = $"{value}%";
            });

            await this.LoopThroughNumbersAsync(100, progress);

            this.tokenSource.Dispose();
        }

        private async Task LoopThroughNumbersAsync(int count, IProgress<int> progress)
        {
            for (int i = 0; i < count && !this.tokenSource.IsCancellationRequested; i++)
            {
                await Task.Delay(100);
                var percentComplete = i * 100 / count;
                progress.Report(percentComplete);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (this.tokenSource is not null && !this.tokenSource.IsCancellationRequested)
            {
                this.tokenSource.Cancel();
            }
        }

        public void Dispose()
        {
            if (this.isDisposed)
            {
                return;
            }

            this.tokenSource?.Dispose();
            this.isDisposed = true;
        }
    }
}
