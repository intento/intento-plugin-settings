using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intento.MT.Plugin.PropertiesForm
{
    class RevisionHistory
    {
        // Version history
        // 1.0.0: 2019-03-13
        //   - A separate solution has been created for the plugin configuration form.
        // 1.0.1: 2019-04-05
        //   - Supports Smart Routing setting
        // 1.2.4: 2019-05-21
        // - List of providers is requested now in sync mode to get real format options to send translate request with th best html or xml option
        // 1.3.0: 
        // - Smart routing
        // - Refactoring form processing
        // 1.3.1: 2019-06-10
        // - Local logs
        // 1.3.3: 2019-06-18
        // - Using GitHub submodules to assemble result
        // 1.3.4: 2019-06-25
        // - Bug with extracting version from dll
        // 1.3.5: 2019-06-26
        // - Bug with visiblity of credential_if list
        // 1.3.6: 2019-07-02
        // - waitAsyncDelay
        // 1.3.7: 2019-07-02
        // - The version in useragent now has a commit hash in git
        // 1.4.0: 2019-07-14
        // - Refactoring to support unit tests (not yet implemented)
        // 1.4.1: 2019-08-14
        // - SmartRouting generator for formatted docs (not finished yet)
        // - Bug in glossaries: not possible to enter glossary manually
        // - Changed Newtonsoft.dll to be compatible with some other programs (downgrade to 10.0.3)
        // 1.4.2: 2019-08-22
        // - Remove of <meta> tag in result of Systran html translation
        // - Pre- and Post-processing moved to PluginHelper in Settings form because it is common for all plugins
        // 1.4.3: 2019-08-23
        // - Internal refactoring: IntentoMT namespace changed to Intento.MT
        // 1.4.4: 2019-09-03
        // - http(s) proxy support

    }
}
