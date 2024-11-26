using OpenTK.Mathematics;

namespace game.renderer.src.camera
{
    public class Camera
    {
        public void CreateCamera(int windowWidth, int windowHeight)
        {
            // Позиция камеры
            Vector3 position = new Vector3(0.0f, 0.0f, 3.0f);
            // Цель камеры (куда она смотрит)
            Vector3 target = Vector3.Zero;
            // Вектор "вверх"
            Vector3 up = Vector3.UnitY;

            // Направление камеры
            Vector3 cameraDirection = Vector3.Normalize(position - target);
            // Правый вектор камеры
            Vector3 cameraRight = Vector3.Normalize(Vector3.Cross(up, cameraDirection));
            // Вектор "вверх" камеры
            Vector3 cameraUp = Vector3.Cross(cameraDirection, cameraRight);

            // Матрица вида
            Matrix4 view = Matrix4.LookAt(position, target, up);
            // Матрица перспективы
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(
                MathHelper.DegreesToRadians(45.0f),
                (float)windowWidth / (float)windowHeight,
                0.1f,
                100.0f
            );

            // Здесь вы можете сохранить или использовать матрицы view и projection
            // например, передать их в шейдеры или сохранить в свойствах класса
        }
    }
}