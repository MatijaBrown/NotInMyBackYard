using Silk.NET.GLFW;
using System;
using System.Numerics;

namespace NIMBY.Utils
{
    public class Camera
    {

        public const float NEAR_PLANE = 0.01f;
        
        public const float MIN_ZOOM = 0.0f;
        public const float MAX_ZOOM = 700.0f;

        public const float PER_PIXEL_DRAGGED = 1.0f;
        public const float PER_UNIT_ZOOMED = 50.0f;

        private float _maxXDistance, _maxYDistance;

        private Vector3 _position;
        private float _zoom;
        private float _scale;

        public Vector3 Position => _position;

        public float Zoom { get => _zoom; set => _zoom = value; }

        public float Scale => _scale;

        public float MaxXDistance { get => _maxXDistance; set => _maxXDistance = value; }

        public float MaxYDistance { get => _maxYDistance; set => _maxYDistance = value; }

        public bool CanZoom { get; set; } = false;

        public Camera(float startX, float startY, float maxXDistance, float maxYDistance)
        {
            _position = new Vector3(startX, startY, 0.0f);

            _maxXDistance = maxXDistance;
            _maxYDistance = maxYDistance;

            _zoom = MIN_ZOOM;
            _scale = 1.0f;
        }

        public void Update(float delta)
        {
            if (Input.Dragging)
            {
                _position.X -= Input.MouseXDelta * PER_PIXEL_DRAGGED;
                _position.Y += Input.MouseYDelta * PER_PIXEL_DRAGGED;
            }
            else
            {
                if (Input.IsKeyDown(Keys.W))
                {
                    _position.Y += PER_PIXEL_DRAGGED;
                }
                else if (Input.IsKeyDown(Keys.S))
                {
                    _position.Y -= PER_PIXEL_DRAGGED;
                }

                if (Input.IsKeyDown(Keys.A))
                {
                    _position.X -= PER_PIXEL_DRAGGED;
                }
                else if (Input.IsKeyDown(Keys.D))
                {
                    _position.X += PER_PIXEL_DRAGGED;
                }
            }

            _position.X = MathF.Max(_position.X, -_maxXDistance);
            _position.X = MathF.Min(_position.X, _maxXDistance);
            _position.Y = MathF.Max(_position.Y, -_maxYDistance);
            _position.Y = MathF.Min(_position.Y, _maxYDistance);

            if (CanZoom)
            {
                _zoom = Input.Scroll * PER_UNIT_ZOOMED;

                _zoom = Math.Min(_zoom, MAX_ZOOM);
                _zoom = Math.Max(_zoom, MIN_ZOOM);

                _position.Z = _zoom;
            }

            _scale = 1.0f + MathF.Abs(_zoom) / MathF.Abs(MAX_ZOOM - MIN_ZOOM);
        }

    }
}
