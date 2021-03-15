﻿using System;
using System.Drawing;
using Intento.MT.Plugin.PropertiesForm;
using MemoQ.MTInterfaces;

namespace IntentoMTPlugin
{
    /// <summary>
    /// Intento MT engine for a particular language combination.
    /// </summary>
    public class IntentoMTEngine : EngineBase
    {
        /// <summary>
        /// Access to Intento API
        /// </summary>
        IntentoMTServiceHelper serviceHelper;

        /// <summary>
        /// The source language.
        /// </summary>
        private readonly string srcLangCode;

        /// <summary>
        /// The target language.
        /// </summary>
        private readonly string trgLangCode;

        /// <summary>
        /// Plugin options
        /// </summary>
        private readonly IntentoMTOptions options;

        public IntentoMTEngine(IntentoMTServiceHelper serviceHelper, string srcLangCode, string trgLangCode, IntentoMTOptions options)
        {
            // using (new Logs.Pair("IntentoMTEngine.ctor"))
            {
                this.srcLangCode = srcLangCode;
                this.trgLangCode = trgLangCode;
                this.options = options;
                this.serviceHelper = serviceHelper;
				IntentoTranslationProviderOptionsForm.TraceEndTime = options.GeneralSettings.TraceEndTime;

			}
        }

        #region IEngine Members

        /// <summary>
        /// Creates a session for translating segments. Session will not be used in a multi-threaded way.
        /// </summary>
        public override ISession CreateLookupSession()
        {
            // using (new Logs.Pair("IntentoMTEngine.CreateLookupSession"))
            return new IntentoMTSession(serviceHelper, srcLangCode, trgLangCode, options);
        }

        /// <summary>
        /// Set an engine-specific custom property, e.g., subject matter area.
        /// </summary>
        public override void SetProperty(string name, string value)
        {
            // not needed
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns a small icon to be displayed under translation results when an MT hit is selected from this plugin.
        /// </summary>
        public override Image SmallIcon
        {
            get { return null; }
        }

        /// <summary>
        /// Indicates whether the engine supports the adjustment of fuzzy TM hits through machine translation.
        /// </summary>
        public override bool SupportsFuzzyCorrection
        {
            get { return false; }
        }

        /// <summary>
        /// Creates a session for translating segments. Session will not be used in a multi-threaded way.
        /// </summary>
        public override ISessionForStoringTranslations CreateStoreTranslationSession()
        {
            // return new IntentoMTSession(srcLangCode, trgLangCode, options);
            return null; // !!! Probably here we need to start async request
        }

        #endregion

        #region IDisposable Members

        public override void Dispose()
        {
            // dispose your resources if needed
        }

        #endregion
    }
}