using FolderSync;

Console.WriteLine("Copying process has been started.");

Operations operations = new Operations();
operations.CopyNetworkFolders();

Console.WriteLine("Operation completed successfully.");