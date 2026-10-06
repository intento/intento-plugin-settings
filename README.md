# intento-plugin-settings

A library with a settings form used in Intento MemoQ plugin and Intento Trados plugin.

## About Intento
                                                             
Intento provides a single API to Cognitive AI services from many vendors.

To get more information check out [the site](https://inten.to/).

[API User Manual](https://github.com/intento/intento-api).

In case you don't have a key to use Intento API, please register here [console.inten.to](https://console.inten.to).

### Dependencies

- Intento.SDK 2.4.0

### Build

Run `SDK.build.bat`. It locates MSBuild via `vswhere` (any Visual Studio 2017+ edition or Build Tools),
restores NuGet packages, builds the solution, packs the nupkg into `build\` and signs it.
`Configuration`, `Version` and `DoSign` can be overridden through environment variables before running the script
(e.g. `set DoSign=0` to skip signing). `nuget.exe` is downloaded into `.tools\` if it is not on `PATH`.
The project targets .NET Framework 4.6.2, so the 4.6.2 Developer Pack must be installed (`choco install netfx-4.6.2-devpack`).

### Sign

The assembly and the NuGet package are signed with the Intento, Inc. code signing certificate stored in DigiCert KeyLocker
(SHA-1 thumbprint `0d1f66efbfc3f97c281800cbc3a91ab883fb1663`, SHA-256 fingerprint
`cfe2b6c33f7e79805a3e611c4aab1a67a13c6ce8f068b711a7b49bac69086481`, timestamp server `http://timestamp.digicert.com`),
the same setup as in the intento-csharp repository.

To sign locally you need the DigiCert ONE Signing Manager tools (`smctl`) and `signtool.exe` (Windows SDK) installed and configured:

1. `smctl healthcheck` must report `Status: Connected` with a valid client certificate.
2. Register the KSP once (admin shell): `smctl windows ksp register`.
3. Sync the certificate into the user store: `smctl windows certsync`.
4. Run `SDK.build.bat` (signing is on by default).

Fingerprint and timestamp server can be overridden with `CertificateFingerprint` and `Timestamper` environment variables.