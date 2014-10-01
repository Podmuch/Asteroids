using System;
using System.Collections.Generic;
using System.Text;
using Asteroids.Model;

namespace Asteroids.Button 
{
    class ButtonModel : AbstractModel
    {
        private Action onClick;

        public ButtonModel()
        {
            onClick = () => { };
        }
        public ButtonModel(Action _onClick)
        {
            onClick = _onClick;
        }
        public override System.Object DrawParams
        {
            get
            {
                return onClick;
            }
        }
    }
}
