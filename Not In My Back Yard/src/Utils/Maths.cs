using System.Numerics;

namespace NIMBY.Utils
{
    public static class Maths
    {

        public static Matrix4x4 CreateViewMatrix()
        {
            return Matrix4x4.Identity;
        }

        public static Matrix4x4 CreateProjectionMatrix(float xOffset, float yOffset, float width, float height)
        {
            return Matrix4x4.CreateOrthographicOffCenter(xOffset, width + xOffset, yOffset, height + yOffset, -1.0f, 1.0f);
        }

        public static Matrix4x4 CreateTransformationMatrix(float x, float y, float scale)
        {
            return Matrix4x4.CreateTranslation(new Vector3(x, y, 0.0f)) * Matrix4x4.Identity * Matrix4x4.CreateScale(scale);
        }

    }
}
