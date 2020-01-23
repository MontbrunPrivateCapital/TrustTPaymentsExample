# Introduction 

This is the Trustt Payment API Developer SDK's official documentation.

Using this tool, you and your customers, being Trustt clients, could perform payments using current owned gold resources. Yes, is what are you thinking, old vintage payments with gold, but using twenty one century technology.

You may also see our swagger documentation, to test endpoints and see data format. Check out
[https://trustt-payments-api.azurewebsites.net/index.html](https://trustt-payments-api.azurewebsites.net/index.html)

# Getting Started

- lack, define authenticacion's credential obtaining

```C#
var _api = 
    new TrusttAPI(new TrustTSettings
        { Host = "trustt-payments-api.azurewebsites.net",
        Bearer = "t0k3n3xample99777asdfipsumlorem" });
```

First, instantiate Trustt SDK main object using a `TrusttAPI` instance. Constructor expects a `TrustTSettings` as argument, this object contains all the API's client configuration. So, this mean, you could use in Dependency Injection like this.

```json - "appsetting.json"
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

services.AddTransient<TrusttAPI>();
```

Once we have an instance of `TrusttAPI`, we can proceed to query the api as high level object. 

# Exceptions

Every method my fail with an exception class names `TrusttException`. Message would be a brief error code. We planning to provide a full description in the inner exception, but is not ready yet.

# Endpoints

From now, we assume **_api** as a valid `TrusttAPI` instance and we'll call it.

## Fees

The following example, fetch trustt fees and gold price.
```C#
var fees = _api.Fees();
var goldPrice = fees.GoldPrice; // string - "$1234.50"
var serviceFees = fees.TrusttFee; // string - "$0.1234"
```

Note the above numbers are a merely examples and their are returned as string in C# invariant culture currency.

## SendVerification

You can send verification request to you client. By now, just email. Notification via push, sms and/or otp will be implemented in near future.

```C#
var mailv = 
    new Verification { Email = "sombody@domain.com" };

try
{
    _api.SendVerification(mailv);
}
catch (Exception ex)
{
    var errorText =
        ex.Message == "not_email_found" ?
        "Not user belongs to the provided address." :
        "Unknown error while sending mail, please contact support"; 

    MessageBox.Show(
    errorText, "mail sending error",
    MessageBoxButtons.OK, MessageBoxIcon.Error);

    return;
}
```

## CardAttach

Before see any example we must understand the *Payload* field. We don't handle your clients, we just need a identification, so, in this endpoint, all we do is, associate a client with something of you own .

In the following example, we use a card number to associate a trustt customer. We identify trustt customers by their email address. So, we assume the Payload as whatever you want, but in the following example, will be card number plus some string, but may also be a hash of this card number, a json or whatever you want. Just store it and call it latter.

```C#
var card = new CardInfo
{
    Email = "some.body1988@domain.com",
    Payload = "1111-2222-3333-444+SomeBody"
};

_api.CardAttach(card);
```
In this example, you will store *1111-2222-3333-444+SomeBody* (not the id). 

Consider the following hypothetical implementation, where `_dbContext.Customers` is **your** entity framework and `Customer` entity has some "convenient columns". Then `card.Payload` is our client payment mechanism, so we store it in a field named *"TrusttIdentification"*.

```C#
var customer = new Customer
{
    Name = "Mr. Some Body",
    Category = CustomerCategories.Premium,
    Address = "Whenever he/she live #911",
    TrusttIdentification = card.Payload // 👈 payload we created in the card (se above example)
}
```

## Payments

Once you have attached cards (payloads), you are ready to proceed with a payment. All you need is send the payload (that whatever you want data already added in the above example) following currency (only USD supported by now, EUR comming zoom) and amount.

```C#
var payment = new Payment
{
    Payload = "1111-2222-3333-444+SomeBody",
    Currency = "USD",
    Amount = 15000
};

// perform payment and get back transaction
var transaction = _api.Payment(payment)
```

`Payment()` method will reply back a `PosTransaction` object. Yo can store the ID or other interest information. We strongly recommend to store transaction's ID, in order to solve eventually issues with some payment.