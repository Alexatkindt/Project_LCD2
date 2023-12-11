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
using System.IO.Ports;

namespace Project_LCD2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SerialCommunicationManager serialManager;

        public MainWindow()
        {
            InitializeComponent();
            InitializeSerialManager();
        }

        private void InitializeSerialManager()
        {
            serialManager = new SerieleCommunicatie("COM8", 9600); // Replace with your actual COM port and baud rate
            serialManager.DataReceived += SerialManager_DataReceived;
            serialManager.Open();
        }

        private void SerialManager_DataReceived(object sender, string receivedData)
        {
            Dispatcher.Invoke(() =>
            {
                // Update UI or perform actions based on received data
                // For example, display the received data in a TextBox
                textBoxReceivedData.Text = receivedData;
            });
        }

        private void Button_SendData_Click(object sender, RoutedEventArgs e)
        {
            // Send data to Arduino when the button is clicked
            serialManager.SendData("Hello, Arduino!");
        }

        protected override void OnClosed(EventArgs e)
        {
            // Close the serial port when the window is closed
            serialManager.Close();

            base.OnClosed(e);
        }

        private void delete(object sender, RoutedEventArgs e)
        {
            serialManager.SendData("reset");
        }
    }
}
