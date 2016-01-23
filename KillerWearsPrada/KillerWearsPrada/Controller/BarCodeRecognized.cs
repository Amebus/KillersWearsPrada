using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillerWearsPrada.Controller
{
    class BarCodeRecognized
    {
        public event EventHandler<Arguments> BarCodeRecognizedEvent;

        public void RaiseEvent(string Code)
        {
            OnBarCodeRecognized(new Arguments(Code));
        }

        protected virtual void OnBarCodeRecognized(Arguments e)
        {
            if (BarCodeRecognizedEvent != null)
            {
                BarCodeRecognizedEvent(this, e);
            }
        }

        
        public class Arguments : EventArgs
        {

            public Arguments(string Code)
            {
                BarCode = Code;
            }

            public string BarCode { get; private set; }
        }
        
    }
}
