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
    public class Bluetooth
    {
        // https://github.com/rostislav-nikitin/Plugin.BluetoothClassic

        public static readonly string MODULE_NAME_PROPERTY = "module_name";
        public static readonly string MODULE_NAME_DEFAULT = "HC-05";
        private readonly IBluetoothAdapter _bluetoothAdapter;
        private IBluetoothManagedConnection _connection;
        private BluetoothDeviceModel _device;
        public BluetoothDeviceModel Device
        {
            get 
            {
                if (_device == null)
                {
                    var deviceName = Storage.ModuleName;

                    _device = GetDevices().Where(device => device.Name == deviceName).FirstOrDefault();
                }
                return _device;
            }
        }

        public Bluetooth()
		{
            _bluetoothAdapter = DependencyService.Resolve<IBluetoothAdapter>();
        }

        public IBluetoothManagedConnection Connection { get { return _connection; } }

        public bool Enabled => _bluetoothAdapter.Enabled;

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

        public void Send(string command)
        {
            if (_connection == null) return;

            _connection.Transmit(new Memory<byte>(Encoding.ASCII.GetBytes(command)));
        }
    }
}
