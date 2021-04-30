using Silk.NET.GLFW;
using System;

namespace NIMBY
{
    public static class Input
    {

        private static readonly bool[] keys = new bool[1024];
        private static readonly bool[] mouseButtons = new bool[16];

        private static float mouseX, mouseY;
        private static float lastMouseX, lastMouseY;
        private static float scroll;

        private static bool dragging;

        private static Action<Keys> onKeyReleased;
        private static Action<MouseButton> onMouseReleased;

        public static Action<Keys> OnKeyReleased { get => onKeyReleased; set => onKeyReleased = value; }

        public static Action<MouseButton> OnMouseReleased { get => onMouseReleased; set => onMouseReleased = value; }

        public static float MouseX => mouseX;

        public static float MouseXDelta => mouseX - lastMouseX;

        public static float MouseY => mouseY;

        public static float MouseYDelta => mouseY - lastMouseY;

        public static float Scroll => scroll;

        public static bool Dragging => dragging;

        public static unsafe void Init(Glfw glfw, WindowHandle* window)
        {
            glfw.SetKeyCallback(window, KeyCallback);
            glfw.SetMouseButtonCallback(window, MouseButtonCallback);
            glfw.SetCursorPosCallback(window, MousePosCallback);
            glfw.SetScrollCallback(window, MouseScrollCallback);
            scroll = 0;
        }

        public static void Update()
        {
            lastMouseX = mouseX;
            lastMouseY = mouseY;
        }

        private static unsafe void KeyCallback(WindowHandle* _, Keys key, int __, InputAction action, KeyModifiers ___)
        {
            if ((int)key < keys.Length)
            {
                if (action == InputAction.Press)
                    keys[(int)key] = true;
                else if (action == InputAction.Release)
                {
                    keys[(int)key] = false;
                    onKeyReleased?.Invoke(key);
                }
            }
        }

        private static unsafe void MouseButtonCallback(WindowHandle* _, MouseButton button, InputAction action, KeyModifiers ___)
        {
            if ((int)button < mouseButtons.Length)
            {
                if (action == InputAction.Press)
                    mouseButtons[(int)button] = true;
                else if (action == InputAction.Release)
                {
                    mouseButtons[(int)button] = false;
                    onMouseReleased?.Invoke(button);

                    if (button == MouseButton.Left)
                    {
                        dragging = false;
                    }
                }
            }
        }

        private static unsafe void MousePosCallback(WindowHandle* _, double x, double y)
        {
            mouseX = (float)x;
            mouseY = (float)y;

            if (IsButtonDown(MouseButton.Left))
            {
                dragging = true;
            }
        }

        private static unsafe void MouseScrollCallback(WindowHandle* _, double __, double y)
        {
            scroll += (float)y;
        }

        public static bool IsKeyDown(Keys key)
        {
            return keys[(int)key];
        }

        public static bool IsButtonDown(MouseButton button)
        {
            return mouseButtons[(int)button];
        }

    }
}
