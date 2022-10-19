using Plugin.BluetoothClassic.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
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

        private readonly IBluetoothAdapter _bluetoothAdapter;
        private BluetoothDeviceModel _device;
        private IBluetoothManagedConnection _connection;

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
            _bluetoothAdapter = DependencyService.Resolve<IBluetoothAdapter>();
        }

        public BluetoothDeviceModel Device
        {
            get 
            {
                if (_device == null) RefreshDevice();
                return _device;
            }
        }

        public void RefreshDevice()
        {
            var deviceName = Storage.ModuleName;

            _device = GetDevices().Where(device => device.Name == deviceName).FirstOrDefault();
        }

        public bool Enabled => _bluetoothAdapter.Enabled;

        public bool Connected => _connection != null;

        public void Enable() => _bluetoothAdapter.Enable();

        public IEnumerable<BluetoothDeviceModel> GetDevices() => _bluetoothAdapter.BondedDevices;

        public async Task<bool> Connect(BluetoothDeviceModel bluetoothDeviceModel)
        {
            try
            {
                _connection = _bluetoothAdapter.CreateManagedConnection(bluetoothDeviceModel);
                _connection.Connect();
                return true;
            }
            catch (Exception exception)
            {
                await Application.Current.MainPage.DisplayAlert("Connection error", 
                    $"Can not connect to the device: {bluetoothDeviceModel.Name} Exception: {exception.Message}",
                    "Close");

                return false;
            }
        }

        public async void Disconnect()
        {
            if (_connection != null) _connection.Dispose();

            _connection = null;
        } 

        public void Send(string command)
        {
            if (_connection == null) return;

            _connection.Transmit(new Memory<byte>(Encoding.ASCII.GetBytes(command)));
        }
    }
}
