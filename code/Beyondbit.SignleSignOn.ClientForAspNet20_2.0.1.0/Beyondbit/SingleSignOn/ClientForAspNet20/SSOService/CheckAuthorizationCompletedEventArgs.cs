namespace Beyondbit.SingleSignOn.ClientForAspNet20.SSOService
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;

    [DesignerCategory("code"), GeneratedCode("System.Web.Services", "2.0.50727.42"), DebuggerStepThrough]
    public class CheckAuthorizationCompletedEventArgs : AsyncCompletedEventArgs
    {
        private object[] results;

        internal CheckAuthorizationCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
        {
            this.results = results;
        }

        public string Result
        {
            get
            {
                base.RaiseExceptionIfNecessary();
                return (string) this.results[0];
            }
        }
    }
}

