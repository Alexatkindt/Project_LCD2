using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Project_LCD2
{
    public class SerieleCommunicatie
    {
        private SerialPort serialPort;

        public event EventHandler<string> DataReceived;

        public SerieleCommunicatie(string portName, int baudRate)
        {
            serialPort = new SerialPort(portName, baudRate);
            serialPort.DataReceived += SerialPort_DataReceived;
        }

        public void Open()
        {
            try
            {
                serialPort.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening serial port: {ex.Message}");
            }
        }

        public void Close()
        {
            if (serialPort.IsOpen)
            {
                serialPort.Close();
            }
        }

        public void SendData(string data)
        {
            try
            {
                serialPort.WriteLine(data);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error sending data: {ex.Message}");
            }
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string receivedData = serialPort.ReadLine();
                DataReceived?.Invoke(this, receivedData);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing received data: {ex.Message}");
            }
        }
    }
}

