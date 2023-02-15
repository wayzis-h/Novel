using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Schizofascism.Desktop.Graphics;
using Schizofascism.Desktop.Graphics.Controls;
using static System.Net.Mime.MediaTypeNames;

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
        private Vector2 t_windowSize;
        private Texture2D t_bg;
        private MouseState t_prewState;
        private int clicker_cnt;
        private Texture2D t_mc;
        private Button t_exit;

        public Novel()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
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
            clicker_cnt = 0;

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
            t_windowSize = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            t_bg = Content.Load<Texture2D>("Backgrounds/Ray");
            t_mc = Content.Load<Texture2D>("Sprites/MainChar");

            _batcher = new MgPrimitiveBatcher(GraphicsDevice, t_sf_font);
            t_exit = new Button(_batcher, new Rectangle(120, 5, 60, 90))
            {
                Text = "Выход"
            };
            t_exit.Clicked += (s, a) => Exit();
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

            t_exit.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.Stencil, Color.CornflowerBlue, 0, 0);

            // TODO: Add your drawing code here
            _spriteBatch.Begin(samplerState: SamplerState.PointWrap);
            _spriteBatch.Draw(t_bg, new Rectangle(Point.Zero, t_windowSize.ToPoint()), Color.White);
            //_spriteBatch.Draw(t_mc, new Rectangle(t_prewState.Position, new Point(77*2, 220*2)), new Rectangle(Point.Zero, new Point(77, 220)), Color.White);
            _spriteBatch.End();

            //_batcher.FillCircle(new Vector2(30), 20.23f, new Color(Color.LemonChiffon, 0.4f), 10);
            _batcher.FillRoundedRect(new System.Drawing.RectangleF(10, 10, 100, 100), 25f, ((int)System.Math.Sqrt(25)), new Color(Color.LemonChiffon, 0.4f));
            t_exit.Draw(gameTime);
            _batcher.Flush();


            _spriteBatch.Begin(samplerState: SamplerState.PointWrap);
            _spriteBatch.Draw(t_mc, new Rectangle(t_prewState.Position, new Point(77 * 2, 220 * 2)), new Rectangle(Point.Zero, new Point(77, 220)), Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}