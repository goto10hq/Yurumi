![Yurumi](https://raw.githubusercontent.com/goto10hq/Yurumi/master/yurumi-logo.png)

# Yurumi

[![Software License](https://img.shields.io/badge/license-MIT-brightgreen.svg?style=flat-square)](LICENSE.md)
[![Latest Version on NuGet](https://img.shields.io/nuget/v/Yurumi.svg?style=flat-square)](https://www.nuget.org/packages/Yurumi/)
[![NuGet](https://img.shields.io/nuget/dt/Yurumi.svg?style=flat-square)](https://www.nuget.org/packages/Yurumi/)
[![Visual Studio Team services](https://img.shields.io/vso/build/frohikey/c3964e53-4bf3-417a-a96e-661031ef862f/130.svg?style=flat-square)](https://github.com/goto10hq/Yurumi)
[![.NETStandard 2.0](https://img.shields.io/badge/.NETStandard-2.0-blue.svg)](https://github.com/dotnet/standard/blob/master/docs/versions/netstandard2.0.md)

## What Yurumi can do?

If you need just send a HTML e-mail with some basic tag replacements... Yurumi is here for you.

## Setup

```csharp
var connection = new Connections.SmtpConnection
(
     "smtp.yuru.mi",
     25,
     "user",
     "password",
     false // no ssl
);
            
var configuration = new Configurations.SendGridConfiguration("Yurumi", true);
var mailer = new Mailer(connection, configuration);

mailer.SendFromFile
( 
     "Template/index.html",
     new System.Net.Mail.MailAddress("noreply@yuru.mi"),
     new System.Net.Mail.MailAddressCollection { "me@me.com" },
     "[TEST] Yurumi",
     new Dictionary<string, object> { { "Salutation", "Hello my lovely robot," } }
);
```

## HTML template

Images are autoprocessed as ``linked resources``.

There is only a very simple ``tag replacements`` implemented. Use tags like that in HTML file: `{something}`. And then replace them this way: `new Dictionary<string, object> { { "something", "Aloha!" } }`

## Acknowledgement

Based on my lib [Drool](https://github.com/goto10hq/Drool) which becames too funky and not working in .NET Core because of too close ties with ASP.NET Mvc.

## License

MIT © [frohikey](http://frohikey.com) / [Goto10 s.r.o.](http://www.goto10.cz)
