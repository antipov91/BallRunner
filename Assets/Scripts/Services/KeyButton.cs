using System;

namespace Systems.InputSystems
{
    public class KeyButton
    {
        public bool IsUp => upFunc.Invoke();
        public bool IsDown => downFunc.Invoke();
        public bool IsPressed => pressedFunc.Invoke();

        private Func<bool> upFunc;
        private Func<bool> downFunc;
        private Func<bool> pressedFunc;

        public KeyButton(Func<bool> upFunc, Func<bool> downFunc, Func<bool> pressedFunc)
        {
            this.upFunc = upFunc;
            this.downFunc = downFunc;
            this.pressedFunc = pressedFunc;
        }
    }
}