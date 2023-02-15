using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Schizofascism.Desktop.Graphics;
using Schizofascism.Desktop.Graphics.Controls;
using System.Collections.Generic;

namespace Schizofascism.Desktop
{
    /// <summary>
    /// Start point of the game loop.
    /// </summary>
    public class Novel : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // Temporary variables for prototype only.
        MgPrimitiveBatcher _batcher;
        private SpriteFont t_sf_font;
        private Texture2D t_white;
        private Texture2D t_black;
        private Texture2D t_transparent;
        private Texture2D t_bg;
        private MouseState t_prewState;
        private Texture2D t_mc;
        private Screen t_screen;
        private Grid t_grid;
        private Image t_background;
        private Button t_exit;

        public Novel()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
            _graphics.PreferredDepthStencilFormat = DepthFormat.Depth24Stencil8;
#if !DEBUG
            _graphics.IsFullScreen = true;
            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
#endif
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            System.GC.Collect(0);

            // TODO: use this.Content to load your game content here
            t_sf_font = Content.Load<SpriteFont>("Fonts/LucidaSans");
            t_black = new Texture2D(_graphics.GraphicsDevice, 1, 1);
            t_black.SetData<Color>(new[] { Color.Black });
            t_white = new Texture2D(_graphics.GraphicsDevice, 1, 1);
            t_white.SetData<Color>(new[] { Color.White });
            t_transparent = new Texture2D(_graphics.GraphicsDevice, 1, 1);
            t_transparent.SetData<Color>(new[] { Color.Transparent });
            //t_windowSize = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            t_bg = Content.Load<Texture2D>("Backgrounds/Ray");
            t_mc = Content.Load<Texture2D>("Sprites/MainChar");

            _batcher = new MgPrimitiveBatcher(GraphicsDevice, t_sf_font);
            t_background = new Image(t_bg, _batcher, new Rectangle(0, 0, 50, 80));
            t_exit = new Button(_batcher, new Rectangle(120, 5, 30, 26))
            {
                Text = "Выход"
            };
            t_exit.Clicked += (s, a) => Exit();
            t_grid = new Grid(_batcher, new Rectangle(0, 0, 200, 200))
            {
                Columns = { new GridUnit(), new GridUnit() },
                Rows = { new GridUnit(), new GridUnit() },
                Children =
                {
                    //new GridChild() { Control = t_background, Column = 0 },
                    new GridChild() { Control = t_exit, Column = 1 },
                },
            };
            t_screen = new Screen(_batcher, new Rectangle(Point.Zero, Window.ClientBounds.Size))
            {
                Children = { t_background, t_grid },
            };
            Window.ClientSizeChanged += (s, e) =>
            {
                t_screen.Placement = new Rectangle(Point.Zero, Window.ClientBounds.Size);
                _batcher.TransformMatrix = Matrix.CreateOrthographicOffCenter(
                    0,
                    GraphicsDevice.Viewport.Width,
                    GraphicsDevice.Viewport.Height,
                    0,
                    0,
                    1);
            };
            t_screen.PlacementChanged += (s, e) =>
            {
                t_grid.Placement = t_background.Placement = t_screen.Placement;
            };
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            if (t_prewState != null && t_prewState.LeftButton == ButtonState.Released && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                //text = (++clicker_cnt).ToString();
            }
            t_prewState = Mouse.GetState();

            //t_exit.Update(gameTime);
            t_screen.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.Stencil, Color.CornflowerBlue, 0, 0);

            // TODO: Add your drawing code here

            t_screen.Draw(gameTime);
            _batcher.Flush();

            //this.Window.
            _spriteBatch.Begin(samplerState: SamplerState.PointWrap);
            _spriteBatch.Draw(t_mc, new Rectangle(t_prewState.Position, new Point(77 * 2, 220 * 2)), new Rectangle(Point.Zero, new Point(77, 220)), Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}