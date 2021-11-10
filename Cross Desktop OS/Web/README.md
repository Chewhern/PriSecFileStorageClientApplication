# PriSecFileStorageClientApplicationWeb
This prisecfilestorageclientapplication will be running in web browser
format. It needs to be launched through good old fashion terminal.

The instruction below was just an example for **V1**.

## Requirements
1. OS: Linux(MacOS can run this application but it's not recommended to do so)
2. Components such as .Net 5.0 need to be installed on the Linux machine (https://docs.microsoft.com/en-us/dotnet/core/install/linux-ubuntu)

## Compiling Procedures
1. Download the whole repository.
2. Unzip the repository.
3. Get into **V1** then unzip the **Required_Files.zip**
4. Remove the **Required_Files.zip** in **V1**
5. Copy the whole **V1** folder into any place on your linux machine
6. Assuming you copy the **V1** into **/home**, the application directory was **/home/V1**.
7. Open the terminal/console/command prompt, use **cd** to change to the directory **/home/V1**.
8. Run the command of **dotnet run** on your linux machine.
9. If there's no flaw in performing the procedures, you will now see **/home/V1/bin/Debug/net5.0**
10. The **/home/V1/bin/Debug/net5.0** is now the compiled application.

## Changing launching setting
1. Go to **Program.cs** that's reside within **/home/V1**, go to line 44 and change the 4001 to whatever port number you want to use.

## Running application (Not 24/7)
1. Copy folder **/home/V1/bin/Debug/net5.0**.
2. Open terminal and change to the folder by using **cd**.
3. Type **dotnet application_name.dll** into the terminal and press **Enter** button on keyboard.

## Running 24/7 (ASP.Net Core Application will time out)
1. Assume that the application directory was **/home/V1**, open the terminal and change to the directory through using **cd**
2. Run **dotnet publish** on the terminal
3. Assume the application compiled directory was **/home/V1/bin/Debug/net5.0**, in the directory, user will found a **publish** folder.
4. The **publish** folder should reside within **/home/V1/bin/Debug/net5.0**, users need to copy that directory out.
5. Users may need to change the **publish** name to other name but this is completely optional.
6. Choose any destination to paste it. (Assume the destination was **/var/html**)
7. Refer to create service file in the repository.
8. Refer to the service file format in the repository.
9. Once you have created a service file, you can skip the step and do\
\
**Terminal**
```
sudo systemctl enable kestrel-prisecfilestorageweb.service

sudo systemctl start kestrel-prisecfilestorageweb.service

sudo systemctl status kestrel-prisecfilestorageweb.service
```
10. The application should now be running 24/7 if the status of the service file looks okay.

## Disabling the application running 24/7
1. You'll need to remember the service file's name.
2. Perform the first 2 opposite steps that was in 9th step in **Running 24/7** which could be shown below.
```
sudo systemctl stop kestrel-prisecfilestorageweb.service

sudo systemctl disable kestrel-prisecfilestorageweb.service
```

## Choices
1. If you decide to run the application 24/7, then you can't see the message that was being displayed to the terminal.
2. If you decide to not run the application 24/7, then the application will have a time out problem.

## Accessing Application
1. Assuming that you didn't change the default setttings, you can access the application via browser through this url (http://0.0.0.0:4001). If you do change the port number, then accessing the application can be done via browser through this url variant (http://0.0.0.0:port_number).
