using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Text;

using game.renderer.src.camera;

namespace game.renderer
{

    public class Game: IDisposable {
        private int __width;
        private int __height;

        public void createGame(int width, int height, string title)
        {
            using (GameLoad game = new GameLoad(width, height, title))
            {
                game.Run();

                __width = width;
                __height = height;
            }
        }

        public void initializeMenu(int width, int height, string title)
        {
            Console.WriteLine("Initializing game menu.");
            using (GameMenu gameMenu = new(width, height, title))
            {
                
                gameMenu.Run();

                __width = width;
                __height = height;
            }
        }

        public void Dispose()
        {
            
        }
    }
    public class GameMenu : GameWindow
    {
        private int ___width;
        private int ___height;
        private StringBuilder _inputText = new StringBuilder();

        public GameMenu(int width, int height, string title) : base(GameWindowSettings.Default,
        new NativeWindowSettings()
        {
            ClientSize = (width, height),
            MinimumClientSize = (width, height),
            Title = title,
        }) 
        {
            ___width = width;
            ___height = height;
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            if (!IsFocused)
            {
                return;
            }
            
            GL.Clear(ClearBufferMask.ColorBufferBit);

            SwapBuffers();
        }
    }

    public class GameLoad : GameWindow
    {
        private int _width;
        private int _height;

        public GameLoad(int width, int height, string title) : base(GameWindowSettings.Default,
            new NativeWindowSettings()
            {
                ClientSize = (width, height),
                MinimumClientSize = (width, height),
                Title = title,
            })
        {
            _width = width;
            _height = height;
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);

            Camera camera = new Camera();
            camera.CreateCamera(_width, _height);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            if (!IsFocused)
            {
                return;
            }
            
            GL.Clear(ClearBufferMask.ColorBufferBit);

            SwapBuffers();
        }

        protected override void OnFramebufferResize(FramebufferResizeEventArgs e)
        {
            base.OnFramebufferResize(e);
            
            GL.Viewport(0, 0, e.Width, e.Height);
        }

        void initializeMenu()
        {

        }
    }
}