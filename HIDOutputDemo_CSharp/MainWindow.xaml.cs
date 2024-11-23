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

namespace HIDOutputDemo_CSharp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const UInt16 Vid = 0x1234;  // Vendor ID
        private const UInt16 Pid = 0x0065;  // Product ID
        private const int reportId = 0x03;
        private const int reportLength = 6;
        private HIDManager _hidManager;
        public MainWindow()
        {
            InitializeComponent();

            _hidManager = new HIDManager(Vid, Pid);

            InfoBox.Text += "Hello.\n";
        }

        private void OpenDevice_Click(object sender, RoutedEventArgs e)
        {
            if (_hidManager.OpenDevice())
            {
                InfoBox.Text += "Device successfully opened\n";
            }
            else
            {
                InfoBox.Text += "Unable to open device\n";
            }

        }

        private void SendTestData_Click(object sender, RoutedEventArgs e)
        {
            if (_hidManager.IsDeviceOpened())
            {
                // data to be send
                byte[] data = new byte[reportLength];
                for (int i = 0; i < reportLength; i++)
                {
                    data[i] = (byte)i;
                }
                if (_hidManager.SendOutputReport(reportId, data))
                {
                    InfoBox.Text += "Output Report send successful!\n";
                }
                else
                {
                    InfoBox.Text += "Output Report send failed\n";
                }
            }
            else
            {
                InfoBox.Text += "The device is not opened, unable to send data\n";
            }
        }
    }
}