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
using System.Windows.Markup;

namespace Project_LCD2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Instance of the class responsible for serial communication
        private SerieleCommunicatie serialManager;

        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            // Initialiseer seriele communicatie
            InitializeSerialManager();
        }

        
        private void InitializeSerialManager()
        {
            // maak een serielecommunicatie aan met com8 en baudrate 9600
            serialManager = new SerieleCommunicatie("COM8", 9600);

            //wanneer de data toegekomen is roep de methode SerialPort_DataReceived.
            serialManager.DataReceived += SerialManager_DataReceived;

            // Open de serial port
            serialManager.Open();
        }

        
        private void SerialManager_DataReceived(object sender, string receivedData)
        {
            // Update the UI met de ontvangen data
            Dispatcher.Invoke(() =>
            {
                textBoxReceivedData.Text = receivedData;
            });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Send "Hello, Arduino!" to the Arduino via de seriele poort
            serialManager.SendData("Hello, Arduino!");
        }

        protected override void OnClosed(EventArgs e)
        {
            // sluit poort wanneer het venster sluit
            serialManager.Close();

            // Call the base class implementation
            base.OnClosed(e);
        }

        
        private void delete(object sender, RoutedEventArgs e)
        {
            // stuur reset naar seeeduino via seriele poort.
            serialManager.SendData("reset");
        }
    }
}

