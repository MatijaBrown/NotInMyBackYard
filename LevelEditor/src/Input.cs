using Silk.NET.GLFW;
using System;

namespace Wind_Thing
{
    public static class Input
    {

        public static float MouseX = 0, MouseY = 0;

        public static Action<float, float> OnLeftDown;
        public static Action<Keys> OnKeyDown, OnKeyUp;
        public static Action<string> OnChar;

        public static unsafe void Init(Glfw glfw, WindowHandle* window)
        {
            glfw.SetCharCallback(window, CharCallback);
            glfw.SetKeyCallback(window, KeyCallback);
            glfw.SetMouseButtonCallback(window, MouseClick);
            glfw.SetCursorPosCallback(window, MouseMove);
        }

        private static unsafe void KeyCallback(WindowHandle* _, Keys key, int __, InputAction action, KeyModifiers ___)
        {
            if (action == InputAction.Press)
                OnKeyDown?.Invoke(key);
            else if (action == InputAction.Release)
                OnKeyUp?.Invoke(key);
        }

        private static unsafe void CharCallback(WindowHandle* _, uint c)
        {
            char ch = (char)c;
            OnChar?.Invoke(ch.ToString());
        }

        private static unsafe void MouseMove(WindowHandle* _, double x, double y)
        {
            MouseX = (float)x;
            MouseY = (float)y;
        }

        private static unsafe void MouseClick(WindowHandle* _, MouseButton button, InputAction action, KeyModifiers __)
        {
            if (action == InputAction.Press)
            {
                if (button == MouseButton.Left)
                {
                    OnLeftDown?.Invoke(MouseX, MouseY);
                }
            }
        }

    }
}
