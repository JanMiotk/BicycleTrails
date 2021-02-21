# BicycleTrails

Used technologies: c#, .net core, openlayers, javascript

## Need to do

It is necessary to install all requirement libraries

You should create your own databases from SQL Server Express and MSQL.

Fill appsettings.json your data.
```
"ConnectionStrings": {
    "SQLServer": "Data Source =.\\SQLEXPRESS; Initial Catalog = catalog; Integrated Security = True;MultipleActiveResultSets=true",
    "Msql": "server=127.0.0.1;user id=root;password=password;port=port;database=database;"
  },
  "GoogleAuthentication": {
    "ClientId": "client",
    "ClientSecret": "secret"
  },
  "ClaimStamps": {
    "Admin": "stamp",
    "SuperAdmin": "stamp"
  },
  ```
  ## Files to load
  
  I put samples xml files to avoid download, so you can just input them from Admin panel
  
  ## Short Presentation
  
  * Login panel
  
  ![alt text](https://media.giphy.com/media/eNdavrK1Kf2FEyjVUj/giphy.gif)
  
  * Trails list
  
  ![alt text](https://media.giphy.com/media/XGgR0VztzrGTuq08QU/giphy.gif)
  
  * Detailed trail description
  
  ![alt text](https://media.giphy.com/media/YSeu2NZBKvSatLFFOO/giphy.gif)
  
  * Loading data in admin panel
  
  ![alt text](https://media.giphy.com/media/WOkXVt47pftSdM7IfR/giphy.gif)
  
  * Checking existing trails
  
  ![alt text](https://media.giphy.com/media/kH5vQipNArMjoInFwp/giphy.gif)
  
  * Super Admin can delete users after receiving properly claims from local server and google
  
  ![alt text](https://media.giphy.com/media/hvRqIm1fWTi4DDYdlU/giphy.gif)
  
  
  
