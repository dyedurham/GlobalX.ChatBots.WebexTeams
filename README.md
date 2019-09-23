# GlobalX.ChatBots.WebexTeams

<p align="center">
    <a href="https://www.nuget.org/packages/GlobalX.ChatBots.WebexTeams">
    	<img src="https://flat.badgen.net/nuget/v/globalx.chatbots.webexteams" alt="GlobalX.ChatBots.WebexTeams nuget package" />
    </a>
    <a href="https://travis-ci.org/GlobalX/GlobalX.ChatBots.WebexTeams">
    	<img src="https://flat.badgen.net/travis/GlobalX/GlobalX.ChatBots.WebexTeams" alt="GlobalX.ChatBots.WebexTeams on Travis CI" />
    </a>
    <a href="https://codecov.io/gh/GlobalX/GlobalX.ChatBots.WebexTeams">
    	<img src="https://flat.badgen.net/codecov/c/github/globalx/globalx.chatbots.webexteams" alt="GlobalX.ChatBots.WebexTeams on Codecov" />
    </a>
    <img src="https://flat.badgen.net/github/commits/globalx/globalx.chatbots.webexteams" alt="commits" />
    <img src="https://flat.badgen.net/github/contributors/globalx/globalx.chatbots.webexteams" alt="contributors" />
    <img src="https://flat.badgen.net/badge/commitizen/friendly/green" alt="commitizen friendly" />
</p>

A .NET Core library containing implementations of core interfaces of
[GlobalX.ChatBots.Core](https://github.com/GlobalX/GlobalX.ChatBots.Core) for
Webex Teams.

## Getting started

### Configuration

In order to use this bot, some configuration is required. This can either be done
through appsettings.json, or at the time of configuring the bot.

#### Example Configuration

```json
// In appsettings.json
{
    "GlobalX.ChatBots.WebexTeams": {
        "WebexTeamsApiUrl": "https://api.ciscospark.com",
        "BotAuthToken": "token",
        "Webhooks": [
            {
                "Name": "name",
                "TargetUrl": "https://fake-url.com",
                "Resource": "Messages",
                "Event": "Created",
                "Filter": "mentionedPeople=me"
            }
        ]
    }
}
```

### Using Dependency Injection

In the `ConfigureServices` method of your `Startup.cs` file, add the following:

```cs
using GlobalX.ChatBots.WebexTeams;

public IServiceProvider ConfigureServices(IServiceCollection services)
{
    // Add other service registrations here
    services.ConfigureWebexTeamsBot(Configuration);
    return services;
}
```

If you have not provided your configuration inside appsettings.json, you may do so
when you configure the bot:

```cs
using GlobalX.ChatBots.WebexTeams;
using GlobalX.ChatBots.WebexTeams.Configuration;

public IServiceProvider ConfigureServices(IServiceCollection services)
{
    // Add other service registrations here
    var settings = new WebexTeamsSettings
    {
        WebexTeamsApiUrl = "https://api.ciscospark.com",
        BotAuthToken = "token",
        Webhooks = new[]
        {
            new Webhook
            {
                Name = "name",
                TargetUrl = "https://fake-url.com",
                Resource = ResourceType.Messages,
                Event = EventType.Created,
                Filter = "mentionedPeople=me"
            }
        }
    };

    services.ConfigureWebexTeamsBot(settings);
}
```

To start the webhooks, put the following in your `Configure` method.

```cs
public void Configure (IApplicationBuilder app, IHostingEnvironment env)
{
    // other configuration code here
    app.ApplicationServices.GetService<IWebhookHelper>().Webhooks.RegisterWebhooksAsync();
}
```

### Without Dependency Injection

You can get a webex teams implementation of the library by calling the
`WebexTeamsChatHelperFactory.CreateWebexTeamsChatHelper` method.

```cs
using GlobalX.ChatBots.Core;
using GlobalX.ChatBots.WebexTeams;
using GlobalX.ChatBots.WebexTeams.Configuration;

// Some code here

var settings = new WebexTeamsSettings
{
    WebexTeamsApiUrl = "https://api.ciscospark.com",
    BotAuthToken = "token"
};

WebexTeamsChatHelper webexTeamsChatHelper = WebexTeamsChatHelperFactory.CreateWebexTeamsChatHelper(settings);
webexTeamsChatHelper.Webhooks.RegisterWebhooksAsync();
```
