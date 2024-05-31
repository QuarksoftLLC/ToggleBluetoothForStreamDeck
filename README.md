# Bluetooth Toggle Project

This project provides a simple console application written in C# that disables the Bluetooth radio, waits for 5 seconds, and then re-enables it using the `InTheHand.Net.Bluetooth` library.

## Prerequisites

- .NET Core SDK (version 3.1 or later)
- 32feet.NET library

## Installation

1. **Clone the repository**:
   ```sh
   git clone https://github.com/QuarksoftLLC/ToggleBluetoothForStreamDeck.git
   cd bluetooth-toggle
   ```

2. **Install the 32feet.NET library**:
   ```sh
   dotnet add package InTheHand.Net.Bluetooth
   ```

## Usage

1. **Build the project**:
   ```sh
   dotnet build
   ```

2. **Run the project**:
   ```sh
   dotnet run
   ```

The application will disable the Bluetooth radio, wait for 5 seconds, and then re-enable it. The current state of the Bluetooth radio will be printed to the console before and after the toggling.

## Code Overview

The main functionality is implemented in the `Program` class. Here is a brief explanation of the key parts:

- **Main Method**:
  ```csharp
  static async Task Main(string[] args)
  {
      await ToggleBluetoothRadioAsync();
  }
  ```
  The `Main` method calls `ToggleBluetoothRadioAsync` to perform the Bluetooth radio toggling.

- **ToggleBluetoothRadioAsync Method**:
  ```csharp
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
  ```

  This method retrieves the default Bluetooth radio, disables it, waits for 5 seconds, and then re-enables it.

- **SetBluetoothRadioState Method**:
  ```csharp
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
  ```

  This method sets the mode of the Bluetooth radio and returns whether the operation was successful.

## Troubleshooting

If the Bluetooth toggle button disappears from Windows 11 settings after running the program, follow these steps:

1. **Check Bluetooth in Device Manager**:
   - Press `Win + X` and select `Device Manager`.
   - Expand the `Bluetooth` section.
   - Right-click your Bluetooth adapter and select `Enable device` if it's disabled.

2. **Restart Bluetooth Services**:
   - Press `Win + R`, type `services.msc`, and press Enter.
   - Find `Bluetooth Support Service`, right-click it, and select `Restart`.

3. **Update Bluetooth Drivers**:
   - In `Device Manager`, right-click your Bluetooth adapter and select `Update driver`.

4. **Re-enable Bluetooth Radio Using PowerShell**:
   - Open PowerShell as an administrator.
   - Run the following command:
     ```powershell
     Get-PnpDevice -Class Bluetooth | Where-Object { $_.Status -eq "Error" } | Enable-PnpDevice -Confirm:$false
     ```

5. **Check Bluetooth Settings**:
   - Go to `Settings` > `Devices` > `Bluetooth & other devices`.
   - Check if the Bluetooth toggle reappears.

## Contributing

Contributions are welcome! Please fork the repository and submit a pull request with your changes.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE.txt) file for more details.

---

Feel free to modify the content as per your project specifics and repository details.
