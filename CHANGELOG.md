## [1.3.1](https://github.com/GlobalX/GlobalX.ChatBots.WebexTeams/compare/v1.3.0...v1.3.1) (2020-08-31)


### Bug Fixes

* add links to the list of allowed child nodes ([0e77cf7](https://github.com/GlobalX/GlobalX.ChatBots.WebexTeams/commit/0e77cf727325805b7fc2415f681267695e3cd3e9))

# [1.3.0](https://github.com/GlobalX/GlobalX.ChatBots.WebexTeams/compare/v1.2.1...v1.3.0) (2020-08-06)


### Features

* add retry policy handler ([bcc9864](https://github.com/GlobalX/GlobalX.ChatBots.WebexTeams/commit/bcc986450a91fd4b2f1bc7c190b9e7ef9f69a3a4))
* use the retry after header to determine the retry time ([dd4473e](https://github.com/GlobalX/GlobalX.ChatBots.WebexTeams/commit/dd4473eddb23c2ca5bee22bbd464ef9d779ad18f))

## [1.2.1](https://github.com/GlobalX/GlobalX.ChatBots.WebexTeams/compare/v1.2.0...v1.2.1) (2020-03-26)


### Bug Fixes

* update representation of phone numbers to match what's returned from the API ([4ba51a7](https://github.com/GlobalX/GlobalX.ChatBots.WebexTeams/commit/4ba51a72c8ec2043b2b80f7390f39e93273821e8))

# [1.2.0](https://github.com/GlobalX/GlobalX.ChatBots.WebexTeams/compare/v1.1.0...v1.2.0) (2020-01-30)


### Features

* implement support for lists ([005f433](https://github.com/GlobalX/GlobalX.ChatBots.WebexTeams/commit/005f433f726335813c9c69a9dbcb2762229aff12))

# [1.1.0](https://github.com/GlobalX/GlobalX.ChatBots.WebexTeams/compare/v1.0.0...v1.1.0) (2020-01-22)


### Features

* add full sender object to message ([34561da](https://github.com/GlobalX/GlobalX.ChatBots.WebexTeams/commit/34561da5bc935bff852099e38fced7c6d318439b))

# 1.0.0 (2019-09-23)


### Bug Fixes

* fix configuration code ([dd43fd9](https://github.com/GlobalX/GlobalX.ChatBots.WebexTeams/commit/dd43fd9))
* fix json serialization ([a131cfb](https://github.com/GlobalX/GlobalX.ChatBots.WebexTeams/commit/a131cfb))
* fix tests ([8332870](https://github.com/GlobalX/GlobalX.ChatBots.WebexTeams/commit/8332870))
* give library control over api version it uses ([a932bd0](https://github.com/GlobalX/GlobalX.ChatBots.WebexTeams/commit/a932bd0))
* **message-parser:** correctly parse br tags in xml ([3579df7](https://github.com/GlobalX/GlobalX.ChatBots.WebexTeams/commit/3579df7))
* **webhookservice:** do not attempt to register webhooks if webhooks are null ([0bb8e28](https://github.com/GlobalX/GlobalX.ChatBots.WebexTeams/commit/0bb8e28))
* make chat helper factory static ([6b3e085](https://github.com/GlobalX/GlobalX.ChatBots.WebexTeams/commit/6b3e085))
* pad base64 strings before attempting to decode ([a76f0e0](https://github.com/GlobalX/GlobalX.ChatBots.WebexTeams/commit/a76f0e0))
* properly subclass IMapper ([aaea666](https://github.com/GlobalX/GlobalX.ChatBots.WebexTeams/commit/aaea666))
* use dotnet core 2.1 dependencies ([ffc4a0b](https://github.com/GlobalX/GlobalX.ChatBots.WebexTeams/commit/ffc4a0b))


### Features

* add webhook registration to chat helper ([e76224c](https://github.com/GlobalX/GlobalX.ChatBots.WebexTeams/commit/e76224c))
* add webhook service ([057f073](https://github.com/GlobalX/GlobalX.ChatBots.WebexTeams/commit/057f073))
* begin to implement sending and getting messages ([44610eb](https://github.com/GlobalX/GlobalX.ChatBots.WebexTeams/commit/44610eb))
* implement parse create message request ([2c3f249](https://github.com/GlobalX/GlobalX.ChatBots.WebexTeams/commit/2c3f249))
* parse message XML ([6cf796a](https://github.com/GlobalX/GlobalX.ChatBots.WebexTeams/commit/6cf796a))
* **webhooks:** implement webhook api calls ([c2ea0f7](https://github.com/GlobalX/GlobalX.ChatBots.WebexTeams/commit/c2ea0f7))
* scaffold out main required classes ([a3a288c](https://github.com/GlobalX/GlobalX.ChatBots.WebexTeams/commit/a3a288c))
* **configuration:** add configuration for webhooks ([0b03ae2](https://github.com/GlobalX/GlobalX.ChatBots.WebexTeams/commit/0b03ae2))
* **configuration:** properly configure bot using appsettings ([881cc83](https://github.com/GlobalX/GlobalX.ChatBots.WebexTeams/commit/881cc83))
* **messages:** add sender name to create message response ([21c9e23](https://github.com/GlobalX/GlobalX.ChatBots.WebexTeams/commit/21c9e23))
* **people:** implement person handler ([da1c29c](https://github.com/GlobalX/GlobalX.ChatBots.WebexTeams/commit/da1c29c))
* **rooms:** implement webex room handler ([ba6e046](https://github.com/GlobalX/GlobalX.ChatBots.WebexTeams/commit/ba6e046))
* **webhooks:** process webhook message callbacks ([4b20022](https://github.com/GlobalX/GlobalX.ChatBots.WebexTeams/commit/4b20022))
* use .net standard 2.0 instead of .net core ([6e5bfd4](https://github.com/GlobalX/GlobalX.ChatBots.WebexTeams/commit/6e5bfd4))
* use transients because it doesn't matter if we get the same instance each time ([202b001](https://github.com/GlobalX/GlobalX.ChatBots.WebexTeams/commit/202b001))

# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

## [1.0.0]
### Added
- Implementations of all functionality from GlobalX.ChatBots.Core
- Webhook integration
- Options for configuring this library

[Unreleased]: https://github.com/GlobalX/GlobalX.ChatBots.WebexTeams/compare/feature/implement-functionality
[1.0.0]: https://github.com/GlobalX/GlobalX.ChatBots.WebexTeams/compare/feature/implement-functionality
