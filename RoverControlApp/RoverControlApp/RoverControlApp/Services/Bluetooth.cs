using Plugin.BluetoothClassic.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        private readonly IBluetoothAdapter bluetoothAdapter;
        private IBluetoothManagedConnection connection;

        public Bluetooth()
		{
            bluetoothAdapter = DependencyService.Resolve<IBluetoothAdapter>();
        }

        public IBluetoothManagedConnection Connection { get { return connection; } }

        public bool Enabled => bluetoothAdapter.Enabled;

        public void Enable() => bluetoothAdapter.Enable();

        public IEnumerable<BluetoothDeviceModel> GetDevices () => bluetoothAdapter.BondedDevices;

        public async Task<bool> Connect(BluetoothDeviceModel bluetoothDeviceModel)
        {
            try
            {
                connection = bluetoothAdapter.CreateManagedConnection(bluetoothDeviceModel);
                connection.Connect();
                return true;
            }
            catch (Exception exception)
            {
                await App.Current.MainPage.DisplayAlert("Connection error", 
                    $"Can not connect to the device: {bluetoothDeviceModel.Name} Exception: {exception.Message}",
                    "Close");

                return false;
            }
           
        }
	}
}
