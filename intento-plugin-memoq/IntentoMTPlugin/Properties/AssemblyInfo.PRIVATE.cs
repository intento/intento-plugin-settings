﻿using System.Reflection;
using System.Runtime.InteropServices;
using MemoQ.Addins.Common.Framework;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Intento MT Plugin (Private)")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Intento")]
[assembly: AssemblyProduct("IntentoMemoQMTPlugin")]
[assembly: AssemblyCopyright("Copyright © Intento 2018-2020")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("e80fb60f-85ed-4f18-bb5e-34d8865a4ce5")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("3.0.1.0")]
[assembly: AssemblyFileVersion("3.0.1.0")]
[assembly: IntentoSDK.AssemblyGitHash(IntentoMTPlugin.GitHash.hash)]
[assembly: Module(ModuleName = "Intento MT", ClassName = "IntentoMTPlugin.IntentoMTPluginDirector")]