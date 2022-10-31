using Plugin.BluetoothClassic.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace RoverControlApp.Services
{
    // https://github.com/rostislav-nikitin/Plugin.BluetoothClassic
    public class Bluetooth
    {
        private static readonly Bluetooth instance = new Bluetooth();

        public Recived OnReceiveEvent { get; set; }
        public StateChanged OnStateChangedEvent { get; set; }

        private readonly IBluetoothAdapter bluetoothAdapter;
        private BluetoothDeviceModel device;
        private IBluetoothManagedConnection connection;

        public static Bluetooth Instance
        {
            get
            {
                return instance;
            }
        }

        static Bluetooth()
        {
        }

        private Bluetooth()
        {
            bluetoothAdapter = DependencyService.Resolve<IBluetoothAdapter>();
        }

        public BluetoothDeviceModel Device
        {
            get 
            {
                if (device == null) RefreshDevice();
                return device;
            }
        }

        public void RefreshDevice()
        {
            var deviceName = Storage.ModuleName;

            device = bluetoothAdapter.BondedDevices.Where(device => device.Name == deviceName).FirstOrDefault();
        }

        public bool Enabled => bluetoothAdapter.Enabled;

        public bool Connected => connection != null;

        public void Enable() => bluetoothAdapter.Enable();

        public void Connect(BluetoothDeviceModel bluetoothDeviceModel)
        {
            connection = bluetoothAdapter.CreateManagedConnection(bluetoothDeviceModel);
            connection.Connect();
            connection.OnRecived += OnReceiveEvent;
            connection.OnStateChanged += OnStateChangedEvent;
        }

        public void Disconnect()
        {
            if (connection != null) connection.Dispose();

            connection = null;
        } 

        public void Send(string command)
        {
            if (connection == null) return;

            connection.Transmit(new Memory<byte>(Encoding.ASCII.GetBytes(command)));
        }
    }
}
