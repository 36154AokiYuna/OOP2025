using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld {
    class ViewModel : BindableBase {

        public ViewModel() {
            ChangeMessageCommand = new DelegateCommand<string>(
                (par)=> GreetingMessage = par);
        }

        private string _greetingMessage = "Hello World!";
        public string GreetingMessage { 
            get=>_greetingMessage;
            set {
                if(SetProperty(ref _greetingMessage, value)) {
                    CanChangeMessage = false;
                }
            }
        }

        private bool _canChangeMessage = true;
        public bool CanChangeMessage {
            get => _canChangeMessage;
            private set => SetProperty(ref _canChangeMessage, value);
        }

        public string NewMessage1 { get; } = "Bye-bye world";
        public string NewMessage2 { get; } = "Long time no see, world";
        public DelegateCommand<string> ChangeMessageCommand { get; }
    }
}
