using System;
using System.Numerics;

namespace NIMBY.Utils
{
    public static class Maths
    {

        public static Matrix4x4 CreateViewMatrix(Camera camera)
        {
            return Matrix4x4.CreateLookAt(camera.Position, new Vector3(camera.Position.X, camera.Position.Y, camera.Position.Z + 1.0f), new Vector3(0.0f, 1.0f, 0.0f));
        }

        public static Matrix4x4 CreatePerspective(float width, float height)
        {
            return Matrix4x4.CreatePerspectiveFieldOfView(90 * MathF.PI / 180.0f, width / height, Camera.NEAR_PLANE, Camera.MAX_ZOOM);
        }

        public static Matrix4x4 CreateProjectionMatrix(float width, float height)
        {
            return Matrix4x4.CreateOrthographicOffCenter(width / 2.0f, -width / 2.0f, -height / 2.0f, height / 2.0f, Camera.MIN_ZOOM, Camera.MAX_ZOOM);
        }

        public static Matrix4x4 CreateTransformationMatrix(float x, float y, float scale)
        {
            return Matrix4x4.CreateTranslation(new Vector3(x, y, 0.0f)) 
               * Matrix4x4.Identity
               * Matrix4x4.CreateScale(scale);
        }

    }
}
