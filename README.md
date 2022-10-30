# DivoomControl
This library simplifies communication with a Divoom device by making simple API calls.

**Not all interfaces are currently implemented, but this will be added over time**

## Supported Devices
- Divoom Pixoo64 (tested)
- Divoom Pixoo16 (not tested)

## Example
This example shows a random image from the favorites list on the display
```csharp
var items = await DivoomControl.Main.getAvailableDevices();
var random = new Random();
items.ForEach(async item =>
{
    Console.WriteLine(item);
    var images = await item.getLikedImagesAsync();
    if (images.Count > 0)
    {
        await item.setImageFromCloudAsync(images[random.Next(0, images.Count - 1)].FileId);
    }
});
```