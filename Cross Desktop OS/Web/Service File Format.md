## Default format of creating a service file
```
[Unit]
Description=Sample Description

[Service]
WorkingDirectory=/var/www/html/PriSecFileStorage(Published)
ExecStart=/usr/bin/dotnet /var/www/html/publish/application_name.dll
Restart=always
RestartSec=10
KillSignal=SIGINT
SyslogIdentifier=dotnet-example
TimeoutStopSec=86400(24 hours)
User=name_of_user(any user that owns the application and its corresponding files/folders)
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=DOTNET_PRINT_TELEMETRY_MESSAGE=false

[Install]
WantedBy=multi-user.target
```

## Sample format of a created service file
```
[Unit]
Description=PriSecFileStorage API prototype

[Service]
WorkingDirectory=/var/www/html/PriSecFileStorage(Published)
ExecStart=/usr/bin/dotnet /var/www/html/PriSecFileStorage(Published)/PriSecFileStorageWeb
Restart=always
RestartSec=10
KillSignal=SIGINT
SyslogIdentifier=dotnet-prisecfilestorageexample
TimeoutStopSec=86400
User=kenny
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=DOTNET_PRINT_TELEMETRY_MESSAGE=false

[Install]
WantedBy=multi-user.target
```
