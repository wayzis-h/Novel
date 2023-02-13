using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
        private SpriteFont t_sf_font;
        private Texture2D t_texture;
        private Vector2 t_windowSize;
        private Texture2D t_bg;
        private string text = "Привет,\n Мир!";
        private MouseState t_prewState;
        private int clicker_cnt;
        private Texture2D t_mc;

        public Novel()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.IsFullScreen = true;
            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
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

            // TODO: use this.Content to load your game content here
            t_sf_font = Content.Load<SpriteFont>("Fonts/LucidaSans");
            t_texture = new Texture2D(_graphics.GraphicsDevice, 1, 1);
            t_texture.SetData<Color>(new[] { Color.White });
            t_windowSize = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            t_bg = Content.Load<Texture2D>("Backgrounds/Ray");
            t_mc = Content.Load<Texture2D>("Sprites/MainChar");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            if (t_prewState != null && t_prewState.LeftButton == ButtonState.Released && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                text = (++clicker_cnt).ToString();
            }
            t_prewState = Mouse.GetState();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin(samplerState: SamplerState.PointWrap);

            _spriteBatch.Draw(t_bg, new Rectangle(Point.Zero, t_windowSize.ToPoint()), Color.White);
            _spriteBatch.Draw(t_mc, new Rectangle(t_prewState.Position, new Point(77*2, 220*2)), new Rectangle(Point.Zero, new Point(77, 220)), Color.White);
            Vector2 ts;
            Vector2 pos = t_windowSize / 2;
            foreach (var line in text.Split(new[] {'\n'}))
            {
                ts = t_sf_font.MeasureString(line);
                pos.Y += ts.Y - 4;
                _spriteBatch.Draw(t_texture, new Rectangle(pos.ToPoint(), ts.ToPoint()), new Color(new Vector4(0.3f)));
                _spriteBatch.DrawString(t_sf_font, line, pos, Color.Black);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}