using System;
using System.Threading.Tasks;
using InTheHand.Net.Bluetooth;

class Program
{
    static async Task Main(string[] args)
    {
        await ToggleBluetoothRadioAsync();
    }

    static async Task ToggleBluetoothRadioAsync()
    {
        var radio = BluetoothRadio.Default;

        if (radio == null)
        {
            Console.WriteLine("No Bluetooth radio found.");
            return;
        }

        Console.WriteLine($"Radio: {radio.Name}, State: {radio.Mode}");

        // Disable the Bluetooth radio
        Console.WriteLine("Disabling Bluetooth Radio...");
        if (SetBluetoothRadioState(radio, RadioMode.PowerOff))
        {
            Console.WriteLine("Bluetooth radio disabled successfully.");
            await Task.Delay(5000); // Wait for 5 seconds

            // Re-enable the Bluetooth radio
            Console.WriteLine("Re-enabling Bluetooth Radio...");
            if (SetBluetoothRadioState(radio, RadioMode.Connectable))
            {
                Console.WriteLine("Bluetooth radio enabled successfully.");
            }
            else
            {
                Console.WriteLine("Failed to re-enable Bluetooth radio.");
            }
        }
        else
        {
            Console.WriteLine("Failed to disable Bluetooth radio.");
        }
    }

    static bool SetBluetoothRadioState(BluetoothRadio radio, RadioMode mode)
    {
        try
        {
            radio.Mode = mode;
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error changing Bluetooth radio state: {ex.Message}");
            return false;
        }
    }
}
