# Setup Database
```
Using the "HealthInsuranceERP.bak" file in Database backup folder, restore the database using Sql Server Management studio.

Once restored, change the Connection string values in the appsettings.json file under HealthInsuranceERP.Api project
    Server = your sql server name
    Password = your sa password
```

# Run Application

* Run the HealthInsuranceERP.Api project setting it as atartup project
