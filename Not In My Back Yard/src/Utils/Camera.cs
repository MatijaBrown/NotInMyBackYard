using System;

namespace NIMBY.Utils
{
    public class Camera
    {

        public const float MAX_ZOOM = 700;
        public const float MIN_ZOOM = 1;

        public const float PER_PIXEL_DRAGGED = 20.0f;
        public const float PER_UNIT_ZOOMED = 50.0f;

        private readonly float _maxXDistance, _maxYDistance;

        private float _xOffset, _yOffset;
        private float _zoom;

        public float XOffset => _xOffset;

        public float YOffset => _yOffset;

        public float Zoom => _zoom;

        public Camera(float startX, float startY, float maxXDistance, float maxYDistance)
        {
            _xOffset = startX;
            _yOffset = startY;

            _maxXDistance = maxXDistance;
            _maxYDistance = maxYDistance;

            _zoom = MIN_ZOOM;
        }

        public void Update(float delta)
        {
            if (Input.Dragging)
            {
                _xOffset -= Input.MouseXDelta * PER_PIXEL_DRAGGED * delta / _zoom;
                _xOffset = MathF.Max(_xOffset, -_maxXDistance);
                _xOffset = MathF.Min(_xOffset, _maxXDistance);
                _yOffset += Input.MouseYDelta * PER_PIXEL_DRAGGED * delta / _zoom;
                _yOffset = MathF.Max(_yOffset, -_maxYDistance);
                _yOffset = MathF.Min(_yOffset, _maxYDistance);
            }

            _zoom = Input.Scroll * PER_UNIT_ZOOMED * delta;

            _zoom = Math.Max(_zoom, MIN_ZOOM);
            _zoom = Math.Min(_zoom, MAX_ZOOM);
        }

    }
}
