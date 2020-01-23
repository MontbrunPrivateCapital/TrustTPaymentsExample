# Introduction 

This is the Trustt Payment API Developer SDK's official documentation.

Using this tool, you and your customers, being Trustt clients, could perform payments using current owned gold resources. Yes, is what are you thinking, old vintage style, payment using gold, but using twenty one century technology.

# Getting Started

- lack, define authenticacion's credential obtaining

First, instanciate Trustt SDK main object using a **TrusttAPI** instance. Use a 

```C#
var _api = 
    new TrusttAPI(new TrustTSettings
        { Host = "trustt-payments-api.azurewebsites.net",
        Bearer = "t0k3n3xample99777asdfipsumlorem" });
```

First, instantiate Trustt SDK main object using a **TrusttAPI** instance. Constructor expects a **TrustTSettings** as argument, this object contains all the API's client configuration. So, this mean, you could use in Dependency Injection like this.

```json appsetting.json
// this is the appsettings.json
"Trustt":{
    "Host":"trustt-payments-api.azurewebsites.net",
    "Bearer" : "t0k3n3xample99777asdfipsumlorem"
}
```

```C# 
// this is your «services» section in Startup.cs
services.AddTransient(p =>
    Configuration.GetSection("Trustt").Get<TrustTSettings>());

services.AddTransient<TrusttAPI>()
```

Once we have an instance of **TrusttAPI**, we can proceed to query the api as high level object. The following example, fetch trustt fees and gold price.

```C#
var goldPrice = fees.GoldPrice;
var serviceFees = fees.TrusttFee;
```




# Example

The following link, contain an example code. This app consumes the oficial API to transact a payment using you gold resources.