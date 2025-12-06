using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;

namespace AlienBloxTools.UIHelpers
{
    public abstract class UISliceElement : UIElement
    {
        private readonly Texture2D _texture;
        private readonly Rectangle _sliceRect;
        private readonly int _sliceWidth;
        private readonly int _sliceHeight;

        public UISliceElement(Texture2D texture)
        {
            _texture = texture;
            _sliceWidth = _texture.Width / 3;
            _sliceHeight = _texture.Height / 3;
            _sliceRect = new Rectangle(0, 0, _sliceWidth, _sliceHeight);
        }

        /// <summary>
        /// Draws the UI over the existing slice system
        /// </summary>
        /// <param name="spriteBatch">The connected <see cref="SpriteBatch"/></param>
        public virtual void DrawUI(SpriteBatch spriteBatch)
        {
            
        }

        public sealed override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            // Get position and size
            Vector2 position = GetDimensions().Position();
            float width = GetDimensions().Width;
            float height = GetDimensions().Height;

            // Top-left corner (no stretching)
            spriteBatch.Draw(_texture, position, _sliceRect, Color.White);

            // Top-middle (stretch horizontally)
            spriteBatch.Draw(_texture, new Rectangle((int)position.X + _sliceWidth, (int)position.Y, (int)(width - 2 * _sliceWidth), _sliceHeight), new Rectangle(_sliceWidth, 0, _sliceWidth, _sliceHeight), Color.White);

            // Top-right corner (no stretching)
            spriteBatch.Draw(_texture, new Rectangle((int)(position.X + width - _sliceWidth), (int)position.Y, _sliceWidth, _sliceHeight), new Rectangle(2 * _sliceWidth, 0, _sliceWidth, _sliceHeight), Color.White);

            // Middle-left (stretch vertically)
            spriteBatch.Draw(_texture, new Rectangle((int)position.X, (int)(position.Y + _sliceHeight), _sliceWidth, (int)(height - 2 * _sliceHeight)), new Rectangle(0, _sliceHeight, _sliceWidth, _sliceHeight), Color.White);

            // Center (stretch both horizontally and vertically)
            spriteBatch.Draw(_texture, new Rectangle((int)(position.X + _sliceWidth), (int)(position.Y + _sliceHeight), (int)(width - 2 * _sliceWidth), (int)(height - 2 * _sliceHeight)), new Rectangle(_sliceWidth, _sliceHeight, _sliceWidth, _sliceHeight), Color.White);

            // Middle-right (stretch vertically)
            spriteBatch.Draw(_texture, new Rectangle((int)(position.X + width - _sliceWidth), (int)(position.Y + _sliceHeight), _sliceWidth, (int)(height - 2 * _sliceHeight)), new Rectangle(2 * _sliceWidth, _sliceHeight, _sliceWidth, _sliceHeight), Color.White);

            // Bottom-left corner (no stretching)
            spriteBatch.Draw(_texture, new Rectangle((int)position.X, (int)(position.Y + height - _sliceHeight), _sliceWidth, _sliceHeight), new Rectangle(0, 2 * _sliceHeight, _sliceWidth, _sliceHeight), Color.White);

            // Bottom-middle (stretch horizontally)
            spriteBatch.Draw(_texture, new Rectangle((int)position.X + _sliceWidth, (int)(position.Y + height - _sliceHeight), (int)(width - 2 * _sliceWidth), _sliceHeight), new Rectangle(_sliceWidth, 2 * _sliceHeight, _sliceWidth, _sliceHeight), Color.White);

            // Bottom-right corner (no stretching)
            spriteBatch.Draw(_texture, new Rectangle((int)(position.X + width - _sliceWidth), (int)(position.Y + height - _sliceHeight), _sliceWidth, _sliceHeight), new Rectangle(2 * _sliceWidth, 2 * _sliceHeight, _sliceWidth, _sliceHeight), Color.White);

            DrawUI(spriteBatch);
        }
    }
}