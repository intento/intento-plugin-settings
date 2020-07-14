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
		// 1.4.7: 2019-09-09
		// - Careful cleaning auth/model/glossary when provider changed
		// 1.5.0: 2019-09-15
		// - http(s) proxy support
		// 1.5.1: 2019-09-20
		// - Bug with single credential_id
		// 1.5.2: 2019-10-05
		// - Refactoring (using memoQ pull request) of AuthWizardForm to comply with different screen scale factors. 
		// - Refacoring of MOdels and Glossaries processing
		// 1.5.3: 2019-10-10
		// - Pull request from memoQ. Processed manually
		// 2.0.0: 2019-11-14
		// - New UI
		// 2.0.1: 2019-11-18
		// - Warning about the desirability of a new test settings when changing the ApiKey
		// - Improved combobox of language selection for test settings
		// - Changes in form of ignore test request errors 
		// 2.0.2: 2019-11-29
		// - Improved PluginHelper: handling some HTML character codes
		// 2.0.3: 2019-12-11
		// - bug with saving setting SmartRouting in memoQ
		// 2.0.4: 2019-12-14
		// - Version for memoQ 9.3
		// 2.1.0: 2020-02-09
		// - Version for plugins ver 2.1.0
		// - Splash form
		// 2.1.1: 2020-02-18
		// - Try-catch around splash form
		// 2.1.2: 2020-02-20
		// - Built-in icon on the splash form
		// 2.1.3: 2020-02-20
		// - Public version
		// 2.1.4: 2020-02-20
		// - Ability to copy ApiKey validation error to clipboard
		// - Control of the state of the "Test and save" button on options form.
		// 2.1.5: 2020-02-25
		// - Custom name support in Trados plugin
		// 2.1.6: 2020-04-02
		// - Asynchronous test request
		// 2.1.7: 2020-04-02
		// - Public memoQ version
		// 2.2.0: 2020-04-29
		// - Only delegated credentional
		// - Saving translation direction if a model is specified
		// - Ñhange settings for connected accounts
		// 2.2.1: 2020-05-13
		// - Setting a language pair for a test request from model/glossary data
		// 2.2.4: 2020-06-19
		// - Write logs to AWS
		// - Bug fix with required & delegated auth mode
		// - Use en-zh in testing of plugin settings if en-es unavailable
		// 2.2.5: 2020-07-02
		// - Added TraceEndTime to parameters of IntentoMTFormOptions
		// 2.2.6: 2020-07-03
		// - Test request marker added
		// - Fix: use of proxy for test translation request
        // 2.2.8: 2020-07-14
        // - Improved list of models
	}
}
