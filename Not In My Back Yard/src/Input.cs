using Silk.NET.Input;
using System.Numerics;

namespace NIMBY
{
    public static class Input
    {

        private static readonly bool[] keys = new bool[1024];
        private static readonly bool[] lastKeys = new bool[1024];
        private static readonly bool[] mouseButtons = new bool[16];
        private static readonly bool[] lastMouseButtons = new bool[16];

        private static float mouseX, mouseY;
        private static float lastMouseX, lastMouseY;
        private static float scroll;

        private static bool dragging;
        private static bool lastDragging;

        public static float MouseX => mouseX;

        public static float MouseXDelta => mouseX - lastMouseX;

        public static float MouseY => mouseY;

        public static float MouseYDelta => mouseY - lastMouseY;

        public static float Scroll => scroll;

        public static bool Dragging => dragging;

        public static void Init(IInputContext context)
        {
            foreach (IKeyboard keyboard in context.Keyboards)
            {
                keyboard.KeyDown += OnKeyDown;
                keyboard.KeyUp += OnKeyUp;
            }
            foreach (IMouse mouse in context.Mice)
            {
                mouse.MouseDown += OnMouseDown;
                mouse.MouseUp += OnMouseUp;
                mouse.MouseMove += OnMouseMove;
                mouse.Scroll += OnMouseScroll;
            }
            scroll = 0;
        }

        public static void Update()
        {
            for (int i = 0; i < keys.Length; i++)
            {
                lastKeys[i] = keys[i];
            }
            for (int i = 0; i < mouseButtons.Length; i++)
            {
                lastMouseButtons[i] = mouseButtons[i];
            }

            lastMouseX = mouseX;
            lastMouseY = mouseY;

            lastDragging = dragging;
        }

        private static void OnKeyDown(IKeyboard _, Key key, int __)
        {
            if ((int)key < keys.Length)
            {
                keys[(int)key] = true;
            }
        }

        private static void OnKeyUp(IKeyboard _, Key key, int __)
        {
            if ((int)key < keys.Length)
            {
                keys[(int)key] = false;
            }
        }

        private static void OnMouseDown(IMouse _, MouseButton button)
        {
            if ((int)button < mouseButtons.Length)
            {
                mouseButtons[(int)button] = true;
            }
        }

        private static void OnMouseUp(IMouse _, MouseButton button)
        {
            if ((int)button < mouseButtons.Length)
            {
                mouseButtons[(int)button] = false;
            }

            if (button == MouseButton.Left)
            {
                dragging = false;
            }
        }

        private static void OnMouseMove(IMouse _, Vector2 pos)
        {
            mouseX = pos.X;
            mouseY = pos.Y;

            if (IsButtonDown(MouseButton.Left))
            {
                dragging = true;
            }
        }

        private static void OnMouseScroll(IMouse _, ScrollWheel wheel)
        {
            scroll += wheel.Y;
        }

        public static bool IsKeyDown(Key key)
        {
            return keys[(int)key];
        }

        public static bool WasKeyReleased(Key key)
        {
            return !keys[(int)key] && lastKeys[(int)key];
        }

        public static bool IsButtonDown(MouseButton button)
        {
            return mouseButtons[(int)button];
        }

        public static bool WasMouseButtonReleased(MouseButton button)
        {
            return !mouseButtons[(int)button] && lastMouseButtons[(int)button] && !lastDragging;
        }

    }
}
