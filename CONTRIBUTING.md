# Contributing

## Commit Message Format

This repository uses conventional commit messages. These are commits in the
format `<type>(<optional scope>): <commit message>`. Allowed types for this
project are:

- `feat` &mdash; a new feature
- `fix` &mdash; a bug fix
- `docs` &mdash; documentation only changes
- `style` &mdash; changes that do not affect the meaning of the code
  (whitespace, formatting, missing semi-colons etc)
- `refactor` &mdash; a code change that neither fixes a bug nor adds a feature
- `perf` &mdash; a code change that improves performance
- `test` &mdash; adding missing tests or correcting existing tests
- `build` &mdash; changes that affect the build system or external dependencies
- `ci` &mdash; changes to our CI configuration
- `chore` &mdash; other changes that don't modify source or test files
- `revert` &mdash; reverts a previous commit

## Tools

### Commitizen

You can automate the process of commiting by using commitizen. You can install
it using npm:

```bash
npm install -g commitizen
```

Then you can commit using:

- `git add .`
- `git cz`

### Commit Hooks

In order to make sure you are following standards it is highly recommended you
install our provided git hooks to your repository. In order to do so, run the
provided `install-hooks.sh` (unix) or `install-hooks.ps1` (windows) files.
Alternatively, simply paste the provided hooks from the `hooks` folder into
`.git/hooks`. Note that you will need python installed in order to run these
hooks.
