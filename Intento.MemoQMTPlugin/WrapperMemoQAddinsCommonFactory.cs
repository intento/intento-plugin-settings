using IntentoMTPlugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IntentoMemoQMTPlugin
{
    public class WrapperMemoQAddinsCommonFactory
    {
        private WrapperMemoQAddinsCommon wrapper;

        public void Init(Assembly assembly)
        {
            wrapper ??= new WrapperMemoQAddinsCommon(assembly);
        }

        public WrapperMemoQAddinsCommon Wrapper => wrapper;

        private static WrapperMemoQAddinsCommonFactory _current;

        public static WrapperMemoQAddinsCommonFactory Current
        {
            get { return _current ??= new WrapperMemoQAddinsCommonFactory(); }
        }
    }
}
