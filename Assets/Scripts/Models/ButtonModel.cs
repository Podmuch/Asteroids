using System;
using Asteroids.Model;

namespace Asteroids.Button 
{
    class ButtonModel : AbstractModel
    {
        public ButtonModel()
        {
            DrawParams=new Action(() => { });
        }
        public ButtonModel(Action _onClick)
        {
            DrawParams = _onClick;
        }
    }
}
