using System;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/*\ 
 * Based on:
 * problem - https://community.monogame.net/t/rounded-rectangles-in-monogame/9855;
 * author - https://community.monogame.net/u/Jjagg;
 * code - https://gist.github.com/Jjagg/bd0540ded0d399e716f25e00641488e1.
\*/

namespace Schizofascism.Desktop.Graphics
{
    public class MgPrimitiveBatcher : PrimitiveBatcherBase<VertexPositionColorTexture, Matrix, Texture2D>
    {
        private readonly GraphicsDevice _graphicsDevice;
        private readonly SpriteFont _font;

        private readonly BasicEffect _basicEffect;
        private readonly DepthStencilState _maskStencilState;
        private readonly DepthStencilState _spriteStencilState;

        private readonly VertexBuffer _vb;
        private readonly IndexBuffer _ib;

        public SpriteBatch SpriteBatcher { get; init; }
        public Texture2D BlankTexture { get; }
        public Texture2D EmptyTexture { get; }
        public Point WindowSize { get; }

        public MgPrimitiveBatcher(GraphicsDevice gd, SpriteFont font)
        {
            _graphicsDevice = gd ?? throw new ArgumentNullException(nameof(gd));
            _font = font ?? throw new ArgumentNullException(nameof(font));

            SpriteBatcher = new SpriteBatch(gd);
            _basicEffect = new BasicEffect(gd)
            {
                VertexColorEnabled = true,
                LightingEnabled = false,
                TextureEnabled = true,
                FogEnabled = false,
            };
            _maskStencilState = new DepthStencilState
            {
                StencilEnable = true,
                StencilFunction = CompareFunction.Always,
                StencilPass = StencilOperation.Replace,
                ReferenceStencil = 1,
                DepthBufferEnable = false,
            };
            _spriteStencilState = new DepthStencilState
            {
                StencilEnable = true,
                StencilFunction = CompareFunction.LessEqual,
                StencilPass = StencilOperation.Keep,
                ReferenceStencil = 1,
                DepthBufferEnable = false,
            };

            BlankTexture = new Texture2D(gd, 1, 1);
            BlankTexture.SetData(new[] {Color.White.PackedValue});
            EmptyTexture = new Texture2D(gd, 1, 1);
            EmptyTexture.SetData(new[] { Color.Transparent.PackedValue });
            Texture = BlankTexture;

            FontTexture = _font.Texture;

            WindowSize = gd.Viewport.Bounds.Size;

            var viewport = gd.Viewport;
            TransformMatrix = Matrix.CreateOrthographicOffCenter(0, viewport.Width, viewport.Height, 0, 0, 1);

            _vb = new VertexBuffer(gd, VertexPositionColorTexture.VertexDeclaration, DefaultMaxVertices, BufferUsage.WriteOnly);
            _ib = new IndexBuffer(gd, IndexElementSize.ThirtyTwoBits, DefaultMaxVertices, BufferUsage.WriteOnly);
        }

        protected override VertexPositionColorTexture CreateVertex(Vector2 p, Vector2 uv, Color color)
        {
            return new VertexPositionColorTexture(new Vector3(p.X, p.Y, 0), new Color(color.PackedValue), new Vector2(uv.X, uv.Y));
        }

        protected override void TransformVertexPosition(ref VertexPositionColorTexture v, Matrix transform)
        {
            v.Position = Vector3.Transform(v.Position, transform);
        }

        protected override void SetTexture(Texture2D texture)
        {
            if (texture == null)
                throw new ArgumentNullException(nameof(texture));
            if (texture != _basicEffect.Texture)
                _basicEffect.Texture = texture;
        }

        protected override void BeginFlush(VertexPositionColorTexture[] vertices, int vertexCount, int[] indices, int indexCount)
        {
            _graphicsDevice.BlendState = BlendState.NonPremultiplied;
            _graphicsDevice.DepthStencilState = DepthStencilState.None;
            _graphicsDevice.RasterizerState = RasterizerState.CullNone;
            _graphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;

            _vb.SetData(vertices, 0, vertexCount);
            _ib.SetData(indices, 0, indexCount);
            _graphicsDevice.SetVertexBuffer(_vb);
            _graphicsDevice.Indices = _ib;
        }

        protected override void DrawBatch(int vertexCount, int indexOffset, int indexCount)
        {
            _basicEffect.CurrentTechnique.Passes[0].Apply();
            _graphicsDevice.DrawIndexedPrimitives(
                PrimitiveType.TriangleList,
                0, indexOffset, indexCount / 3);
        }

        public override void DrawString(string text, Vector2 position, float size, Color color)
        {
            SpriteBatcher.Begin();
            SpriteBatcher.DrawString(_font, text, new Vector2(position.X, position.Y), new Color(color.PackedValue));
            SpriteBatcher.End();
        }

        public override void DrawString(StringBuilder text, Vector2 position, float size, Color color)
        {
            SpriteBatcher.Begin();
            SpriteBatcher.DrawString(_font, text, new Vector2(position.X, position.Y), new Color(color.PackedValue));
            SpriteBatcher.End();
        }

        public override void DrawStringCropped(string text, Vector2 position, Rectangle area, float size, Color color)
        {
            SpriteBatcher.Begin(sortMode: SpriteSortMode.Immediate, depthStencilState: _maskStencilState);
            SpriteBatcher.Draw(EmptyTexture, area, Color.Transparent);
            SpriteBatcher.End();
            SpriteBatcher.Begin(sortMode: SpriteSortMode.Immediate, depthStencilState: _spriteStencilState);
            Vector2 ts;
            Vector2 pos = position /*t_windowSize / 2*/;
            foreach (var line in text.Split(new[] { '\n' }))
            {
                ts = _font.MeasureString(line);
                //pos.Y += ts.Y - 4;
                //_spriteBatch.Draw(BlankTexture, new Rectangle(pos.ToPoint(), ts.ToPoint()), new Color(new Vector4(0.3f)));
                SpriteBatcher.DrawString(_font, line, pos, Color.White);
            }
            SpriteBatcher.End();
            /*_spriteBatch.Begin();
            _spriteBatch.DrawString(_font, text, new Vector2(position.X, position.Y), new Color(color.PackedValue));
            _spriteBatch.End();*/
        }

        public override void DrawStringCropped(StringBuilder text, Vector2 position, Rectangle area, float size, Color color)
        {
            SpriteBatcher.Begin();
            SpriteBatcher.DrawString(_font, text, new Vector2(position.X, position.Y), new Color(color.PackedValue));
            SpriteBatcher.End();
        }
    }
}