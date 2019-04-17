# wellrested
Open-source REST API solution for Peloton&reg; WellView&reg;.  Add a self-managed, customizable web API to your well data management platform - for on-premises, or in the cloud! Uses Peloton&reg; IOEngine&reg;.

## Usage
1. Build solution
2. Update appsettings.json (just as you would initialize IOEngine)
```

{
  "Peloton": {
    "RootFolder": "C:\\Peloton\\WellView",
    "ProfileName": "All Data",
    "ProfilePassword": "",
    "UnitSetName": "US",
    "ConnectionDBMS": "SQLCompact",
    "ConnectionServer": "C:\\Peloton\\WellView\\user\\database\\wv10.0 sample.sdf",
    "ConnectionDatabase": "",
    "DatabaseUsername": "",
    "DatabasePassword": "",
    "Trusted": "true",
    "DataModel": "C:\\Peloton\\WellView\\admin\\sdk\\Peloton.DataModel.WellView102.dll"
  }, 
  "Logging": {
    "IncludeScopes": false,
    "Debug": {
      "LogLevel": {
        "Default": "Warning"
      }
    },
    "Console": {
      "LogLevel": {
        "Default": "Warning"
      }
    }
  }
}
```
3. Run application
