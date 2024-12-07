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
        private UInt16 Vid = 0x1234;  // Vendor ID
        private UInt16 Pid = 0x0001;  // Product ID
        private byte reportId = 0x05;
        private int reportLength = 128;
        private HIDManager _hidManager;
        public MainWindow()
        {
            InitializeComponent();



            InfoBox.Text += "Hello.\n";
        }

        private void OpenDevice_Click(object sender, RoutedEventArgs e)
        {
            Vid = Convert.ToUInt16(VID.Text, 16);
            Pid = Convert.ToUInt16(PID.Text, 16);
            _hidManager = new HIDManager(Vid, Pid);
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

                reportId = Convert.ToByte(ReportID.Text, 16);
                reportLength = Convert.ToUInt16(ReportLength.Text);
                // data to be send
                byte[] data = new byte[reportLength];
                for (int i = 0; i < reportLength; i++)
                {
                    data[i] = (byte)i;
                }
                if (_hidManager.SendOutputReport(reportId, data))
                {
                    InfoBox.Text += "Output Report send successful!\nSuccessfully sent data:\n";
                    foreach (byte b in data)
                    {
                        InfoBox.Text += "0x" + b.ToString("X2") + " ";
                    }

                }
                else
                {
                    InfoBox.Text += $"Output Report send failed\n{_hidManager.getLastError()}\n";
                }
            }
            else
            {
                InfoBox.Text += $"The device is not opened, unable to send data\n";
            }
        }

        private void ClearInfoBox_Click(object sender, RoutedEventArgs e)
        {
            InfoBox.Text = string.Empty;
        }

        private void GetAllDeviceInfo_Click(object sender, RoutedEventArgs e)
        {
            if (_hidManager.IsDeviceOpened())
            {
                InfoBox.Text += _hidManager.GetAllDeviceInfo();
            }
            else
            {
                InfoBox.Text += $"The device is not opened, unable to send data\n";
            }
        }
    }
}