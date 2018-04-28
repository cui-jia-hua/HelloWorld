using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace HelloWorld
{
    public static class ToDoTaskFileHelper
    {
        public static async Task<string> ReadToDoTaskJsonAsync()
        {
            // declare an empty variable to be filled later  
            string json = null;
            // define where the file resides  
            StorageFolder localfolder = ApplicationData.Current.LocalFolder;
            Debug.WriteLine(localfolder.Path);
            // see if the file exists  
            if (await localfolder.TryGetItemAsync(Filename) != null)
            {
                // open the file  
                StorageFile textfile = await localfolder.GetFileAsync(Filename);
                // read the file  
                json = await FileIO.ReadTextAsync(textfile);
            }
            // if the file doesn't exist, we'll copy the app copy to local storage  
            else
            {
                /*
                StorageFile storageFile =
                    await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/task.json"));

                await storageFile.CopyAsync(ApplicationData.Current.LocalFolder);
                json = await FileIO.ReadTextAsync(storageFile);*/
                json =
                    "{\r\n  \"Description\": \"A test task\",\r\n  \"Id\": \"9d6c3585-d0c2-4885-8fe0-f02727f8e483\",\r\n  \"IsComplete\": true\r\n}     ";
            }

            return json;
        }

        public static async Task SaveToDoTaskJson(string json)
        {
            StorageFolder localfolder = ApplicationData.Current.LocalFolder;
            StorageFile textfile = await localfolder.GetFileAsync(Filename);
            await FileIO.WriteTextAsync(textfile, json);
        }

        private static readonly string Filename = "task.json";
    }
}
