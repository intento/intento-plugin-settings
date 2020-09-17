using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntentoMemoQMTPlugin
{
    class RevisionHistory
    {
		// Version history:
		// 1.0.2: Switch translation to to async=true & wait_async=true
		// 1.0.3: - BatchSupported is always true because intento.async may process batch for any provider
		//        - UserAgent changed to Intento.MemoqPlugin
		//        - Providers filtered by integration and published flags
		// 1.0.4: 2018-11-29. Minor changes in UI
		// 1.0.5: 2018-12-01
		//        - Payload logging checkbox
		//        - auth us null in case no ProviderKey
		// 1.0.6: 2018-12-03: reference to registration on settings form
		// 1.0.7: 2018-12-06: 
		//        - Password style for apikey and auth fields
		//        - clickable links in text
		// 1.0.8: 2018-12-13: 
		//        - change name of plugin and signature to Intento.MemoQPlugin
		// 1.0.9: 2018-12-31: 
		//        - Bug with an application icon corrected
		// 1.0.10: 2019-01-24: 
		//        - Corrected language pairs with Serbian and Chinese languages
		// 1.0.11: 2019-01-29: 
		//        - Bug in Plugin package content
		//        - Showing version on form
		// 1.1.0: 2019-01-30: 
		//        - The previous version missed some files in the package, fixed. 
		//        - Package signing was incorrect, fixed
		//        - Plugin setting form now shows a plugin version, should help with bug reporting.
		// 1.1.1: 2019-02-15: 
		//        - Alternative method to get version/copyright/description from plugin
		// 1.1.2: 2019-02-25: 
		//        - Display information on getting verion data
		// 1.1.3: 2019-03-08: 
		//        - Problems with getting language pairs from Intento API (memoQ v.8.7)
		// 1.1.4: 2019-03-09: 
		//        - An indication of server/client source of the plugin
		// 1.2.0: 2019-04-14:
		//        - Smart Routing feature, which selects the best general-purpose MT engine for your language pair based on our regular benchmarks.
		//        - Support for new Google Translate API v3, including custom glossary support.
		//        - Simplified usage of MT providers on your own contracts.Just use Credentials Id from Intento Console instead of complex auth structures.
		// 1.2.2: 2019-05-14:
		//        - Translation of formatted documents
		// 1.2.5: 2019-05-23:
		//        - Simplify version number in bottom-right corner of the form
		// 1.3.0: 
		//        - Smart routing
		//        - Refactoring form processing
		// 1.3.1: 2019-06-10
		//        - Local logs
		// 1.3.2: 
		//        - Bug in Settgins form
		// 1.3.3: 2019-05-18
		//        - Using GitHub submodules to assemble the result
		// 1.3.4: 2019-06-25
		// - Bug with extracting version from dll
		// 1.3.5: 2019-06-26
		// - Bug with visiblity of credential_if list
		// 1.3.6: 2019-07-02
		// - waitAsyncDelay
		// 1.4.0: 2019-07-14
		// - Correct format of xml document for providers supported only xml
		// - Bug: Smart routing mode for Server version of plugin
		// 1.4.1: 2019-08-14
		// - Bug in glossaries: not possible to enter glossary manually
		// - Changed Newtonsoft.dll to be compatible with some other programs (downgrade to 10.0.3)
		// 1.4.2: 2019-08-22
		// - Remove of <meta> tag in result of Systran html translation
		// - Pre- and Post-processing moved to PluginHelper in Settings form because it is common for all plugins
		// - Generate skgsign file for server version of plugin
		// 1.4.3: 2019-08-23
		// - Internal refactoring: IntentoMT namespace changed to Intento.MT
		// 1.4.7: 2019-09-09
		// - Careful cleaning auth/model/glossary when provider changed
		// 1.5.0: 2019-09-15
		// - HTTP proxy
		// 1.5.1: 2019-09-20
		// - Name of plugin now (by request from memoQ): Intento MT Plugin (private)
		// - Bug with single credential_id
		// 1.5.2: 2019-10-08
		// - Refactoring (using memoQ pull request) of AuthWizardForm to comply with different screen scale factors. 
		// - Refacoring of MOdels and Glossaries processing
		// 1.5.3: 2019-10-10
		// - Pull request from memoQ. Processed manually
		// 2.0.0: 2019-11-17
		// - New UI
		// - Check form settings on exit
		// 2.0.2: 2019-12-10
		// - More accurate mapping of languages between memoQ and Intento. Now more chinese langs are supported as zh-hant
		// - smart routing table name changed to intento-tagged
		// 2.0.3: 2019-12-14
		// - Version for memoQ 9.3
		// 2.0.5: 2020-01-16
		// - Correct call of the settings from with splash form
		// 2.1.0: 2020-02-20
		// - splitting public and private version
		// - try-catch for showin intento logo on splash form
		// 2.1.1: 2020-03-04
		// - Correct processing of translate errors
		// 2.1.2: 2020-03-06
		// - Error handling in multi-segment translation
		// 2.1.7: 2020-04-02
		// - Public memoQ version
		// 2.2.0: 2020-04-29
		// - Only delegated credentional
		// - Saving translation direction if a model is specified
		// - Сhange settings for connected accounts
		// - Custom tag replacement
		// 2.2.1: 2020-05-01
		// - Support for setting custom html tag parser
		// 2.2.2: 2020-05-09
		// - Bug with updating supported language pairs when changing provider
		// 2.2.3: 2020-05-30
		// - Support of both versions of MemoQ.Addins.Common.dll - with 2 and 3 parameters in ConvertSegment2Html call
		// 2.2.4: 2020-06-19
		// - Write logs to AWS
		// - Correction of bug with checking version of server plugin
		// - Bug fix with required & delegated auth mode
		// - Use en-zh in testing of plugin settings if en-es unavailable
		// 2.2.5: 2020-06-24
		// - Send back additional information about the translation (providers name), to be presented to the user
		// 2.2.6: 2020-07-02
		// - Store the TraceEndTime parameter in the plugin settings
		// 2.2.7: 2020-07-11
		// - Fixed bug with the definition of the formats supported by the provider
		// 2.2.8: 2020-07-14
		// - Improved list of models
		// - Correction of bug with choosing html or xml format for provider for tagged segments
		// 2.2.11: 2020-07-29
		// - Corrected ILMerge sequence
		// 2.2.13
		// - Changed mapping of Chinese (Singapore) to zh (simplified)
		// 2.2.14 2020-09-09
		// - (Public version) Removed: Write logs to AWS 
		// 2.2.15 2020-09-09
		// - (Public version) Removed newtonsoft.dll from final signed library 
		// - (Public version) Default value for custom HTML tag parser setting is false 
		// - Handling hidden, implicit tags
	}
}
