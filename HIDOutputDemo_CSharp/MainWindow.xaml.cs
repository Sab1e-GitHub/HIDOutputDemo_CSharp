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
                if (_hidManager.SendOutputReport(0x03, [2, 1, 255, 0, 0, 0]))
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