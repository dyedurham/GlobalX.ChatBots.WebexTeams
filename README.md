# GlobalX.ChatBots.WebexTeams

<p align="center">
    <a href="https://www.nuget.org/packages/GlobalX.ChatBots.WebexTeams/"><img src="https://flat.badgen.net/nuget/v/globalx.chatbots.core" alt="GlobalX.ChatBots.WebexTeams nuget package" /></a>
    <a href="https://travis-ci.org/GlobalX/GlobalX.ChatBots.WebexTeams"><img src="https://flat.badgen.net/travis/GlobalX/GlobalX.ChatBots.WebexTeams" alt="GlobalX.ChatBots.WebexTeams on Travis CI" /></a>
    <img src="https://flat.badgen.net/github/commits/globalx/globalx.chatbots.webexteams" alt="commits" />
    <img src="https://flat.badgen.net/github/contributors/globalx/globalx.chatbots.webexteams" alt="contributors" />
    <img src="https://img.shields.io/badge/commitizen-friendly-brightgreen.svg" alt="commitizen friendly" />
</p>

A .NET Core library containing implementations of core interfaces of [GlobalX.ChatBots.Core](https://github.com/GlobalX/GlobalX.ChatBots.Core) for Webex Teams.

## Getting started

### Configuration

TODO

### Using Dependency Injection

In the `ConfigureServices` method of your `Startup.cs` file, add the following:

```cs
using GlobalX.ChatBots.WebexTeams;

public IServiceProvider ConfigureServices(IServiceCollection services)
{
    // Add other service registrations here
    services.ConfigureWebexTeamsBot();
    return services;
}
```

### Without Dependency Injection

You can get a webex teams implementation of the library by calling the `WebexTeamsChatHelperFactory.CreateWebexTeamsChatHelper` method.

```cs
using GlobalX.ChatBots.Core;
using GlobalX.ChatBots.WebexTeams;

// Some code here

IChatHelper webexTeamsChatHelper = WebexTeamsChatHelperFactory.CreateWebexTeamsChatHelper();
```

## Contributing

This repository uses conventional commit messages. You can automate the process of commiting by using commitizen. You can install it using npm:

```
npm install -g commitizen
npm install -g cz-conventional-changelog
```

Then you can commit using:

- `git add .`
- `git cz`
