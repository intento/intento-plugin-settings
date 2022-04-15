# Intento memoQ plugin

A plugin for memoQ to query Intento Machine Translation.

## About Intento

Intento provides a single API to Cognitive AI services from many vendors.

To get more information check out [the site](https://inten.to/).

[API User Manual](https://github.com/intento/intento-api).

In case you don't have a key to use Intento API, please register here [console.inten.to](https://console.inten.to).

## Direct plugin download

Get the latest plugin version [here](https://drive.google.com/drive/folders/11U4Tsp08VVixyCEZCuSqyFqiNU5qazh7).

## Development

To compile the memoQ IntentoMT plugin (Visual Studio 2017), you need to clone 3 repositories:

- [intento-csharp](https://github.com/intento/intento-csharp) - contains connector to the Intento API.
- [intento-plugin-settings](https://github.com/intento/intento-plugin-settings) - contains a settings form for the plugin.
- [intento-plugin-memoq](https://github.com/intento/intento-plugin-memoq) - current repository.

Clone repositories with original names with exact same roots as listed below (do not specify a directory name in gitclone, so directory will be created automatically with original repository name):

- `C:\repos\Intento\intento-csharp`
- `C:\repos\Intento\intento-plugin-settings`
- `C:\repos\Intento\intento-plugin-memoq`

Сompile repositories in the following order:

1. `intento-csharp`
2. `intento-plugin-settings`
3. `intento-plugin-memoq`

When compiled you may find plugin files here:

- `intento-plugin-memoq\IntentoMTPlugin\Intento.MemoQMTPlugin.dll`
- `intento-plugin-memoq\IntentoMTPlugin\Intento.MemoQMTPlugin.kgsign`
