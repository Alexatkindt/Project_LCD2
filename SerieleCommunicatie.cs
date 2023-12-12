using System;
using System.IO.Ports;
using System.Windows;

namespace Project_LCD2
{
    
    public class SerieleCommunicatie
    {
        private SerialPort serialPort;

        
        public event EventHandler<string> DataReceived;

        
        public SerieleCommunicatie(string portName, int baudRate)
        {
            // Initialiseert poort met poortnaam en baudrate
            serialPort = new SerialPort(portName, baudRate);

            // wanneer de data toegekomen is roep de methode SerialPort_DataReceived.
            serialPort.DataReceived += SerialPort_DataReceived;
        }

        /// <summary>
        /// Opens the serial port for communication.
        /// </summary>
        public void Open()
        {
            try
            {
                //probeer seriele poort te openen.
                serialPort.Open();
            }
            catch (Exception ex)
            {
                // toon error als het openen van serielepoort fout loopt.
                MessageBox.Show($"Error opening serial port: {ex.Message}");
            }
        }

        /// <summary>
        /// Closes the serial port if it is open.
        /// </summary>
        public void Close()
        {
            if (serialPort.IsOpen)
            {
                // sluit poort als de poort open is
                serialPort.Close();
            }
        }

        public void SendData(string data)
        {
            try
            {
                // stuur data door naar de serieleport.
                serialPort.WriteLine(data);
            }
            catch (Exception ex)
            {
                // toon error als data failed.
                MessageBox.Show($"Error sending data: {ex.Message}");
            }
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                // Read a line of data from the serial port.
                string receivedData = serialPort.ReadLine();

                // Invoke the DataReceived event, passing the received data.
                DataReceived?.Invoke(this, receivedData);
            }
            catch (Exception ex)
            {
                // toon messagebox als een error optreedt
                MessageBox.Show($"Error processing received data: {ex.Message}");
            }
        }
    }
}