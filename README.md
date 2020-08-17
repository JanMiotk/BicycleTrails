# BicycleTrails
It is necessary to instal all requirement libraries

You should create your own databases from SQL Server Express and MSQL.

Fill appsettings.json your data.

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
  
  I put samples xml files to avoid download, so you can just input them from AdminController
