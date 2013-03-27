using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;

namespace ValidationTest
{
    public class ViewModel : ReactiveValidatedObject 
    {
        string myValidatedProperty;
        readonly ViewModel2 nestedViewModel = new ViewModel2();

        [Required]
        [EmailAddress]
        public string MyValidatedProperty
        {
            get { return myValidatedProperty; }
            set { this.RaiseAndSetIfChanged(ref myValidatedProperty, value); }
        }

        public ViewModel2 NestedViewModel { get { return nestedViewModel; } }
    }

    public class ViewModel2 : INotifyDataErrorInfo
    {
        bool hasErrors;

        public string MyNotifyProperty { get; set; }

        public IEnumerable GetErrors(string propertyName)
        {
            return HasErrors ? new[] {"My Error"} : Enumerable.Empty<object>();
        }

        public bool HasErrors
        {
            get { return hasErrors; }
            set
            {
                if (hasErrors != value) {
                    hasErrors = value;
                    OnErrorsChanged(new DataErrorsChangedEventArgs("MyNotifyProperty"));
                }
            }
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        protected virtual void OnErrorsChanged(DataErrorsChangedEventArgs e)
        {
            var handler = ErrorsChanged;
            if (handler != null) handler(this, e);
        }
    }
}
