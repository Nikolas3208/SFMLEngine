using GameEngine.Core.Contents;
using GameEngine.Core.Contents.Assets;
using GameEngine.Core.GameObjects;
using GameEngine.Editor;
using GameEngine.Editor.EditorInterface;
using ImGuiNET;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Core.Graphics
{
    public enum GizmoType
    {
        None,
        Move,
        Rotate,
        Scale
    }

    public class Gizmo : Transformation, Drawable
    {
        private RectangleShape _rect;
        private RectangleShape arrowX;
        private RectangleShape arrowY;
        private GameObject _gameObject;

        private Vector2f mousePos = new Vector2f();
        private Vector2f oldMousePos = new Vector2f();
        private GizmoType _type = GizmoType.None;
        public Guid Id { get; }
        public GizmoType Type 
        { 
            get => _type;
            set
            {
                _type = value;

                UpdateSprite(value);
            }
        }

        private void UpdateSprite(GizmoType type)
        {
            switch (type)
            {
                case GizmoType.Move:
                    arrowX.Texture = AssetsMenager.GetAsset<ImageAsset>("ArrowMoveX").Texture;
                    arrowY.Texture = AssetsMenager.GetAsset<ImageAsset>("ArrowMoveY").Texture;
                    break;
                case GizmoType.Rotate:
                    break;
                case GizmoType.Scale:
                    arrowX.Texture = AssetsMenager.GetAsset<ImageAsset>("ArrowScaleX").Texture;
                    arrowY.Texture = AssetsMenager.GetAsset<ImageAsset>("ArrowScaleY").Texture;
                    break;
            }
        }

        public Gizmo()
        {
            _rect = new RectangleShape(new Vector2f(15, 15));
            _rect.Position = new Vector2f(-2, -2);
            _rect.FillColor = Color.White;
            arrowX = new RectangleShape(new Vector2f(40, 10));
            arrowX.Position = new Vector2f(10, 0);
            arrowX.FillColor = Color.Red;
            arrowY = new RectangleShape(new Vector2f(10, -40));
            arrowY.FillColor = Color.Green;
            Id = Guid.NewGuid();
        }

        public void SetGameObject(GameObject gameObject) => _gameObject = gameObject;

        public void Draw(RenderTarget target, RenderStates states)
        {
            if(_gameObject == null)
                return;

            if (Type == GizmoType.None)
                return;

            oldMousePos = mousePos;
            mousePos = (Vector2f)Mouse.GetPosition((RenderWindow)target);
            mousePos = new Vector2f(mousePos.X - target.GetView().Size.X / 2 - EditorInterface.sizeMin.X, mousePos.Y - target.GetView().Size.Y / 2 - EditorInterface.sizeMin.Y);
            states.Transform *= Transform;

            target.Draw(arrowX, states);
            target.Draw(arrowY, states);
            target.Draw(_rect, states);
        }

        private bool rectMove = false;
        private bool rectArrowX = false;
        private bool rectArrowY = false;
        public void Update()
        {
            if (_gameObject == null)
                return;

            if (Type == GizmoType.None)
                return;

            _rect.Position = _gameObject.Position + new Vector2f(-2, -2);
            arrowX.Position = _gameObject.Position + new Vector2f(10, 0);
            arrowY.Position = _gameObject.Position;

            var rect = new FloatRect(_rect.Position, _rect.Size);
            var arrowXRect = new FloatRect(arrowX.Position, arrowX.Size);
            var arrowYRect = new FloatRect(arrowY.Position, arrowY.Size);

            var mouseRect = new FloatRect(mousePos - new Vector2f(2, 2), new Vector2f(5, 5));

            if (rect.Intersects(mouseRect) && Mouse.IsButtonPressed(Mouse.Button.Left) && !rectArrowX && !rectArrowY)
            {
                rectMove = true;
            }
            else if(arrowXRect.Intersects(mouseRect) && Mouse.IsButtonPressed(Mouse.Button.Left) && !rectMove && !rectArrowY)
            {
                rectArrowX = true;
            }
            else if (arrowYRect.Intersects(mouseRect) && Mouse.IsButtonPressed(Mouse.Button.Left) && !rectMove && !rectArrowX)
            {
                rectArrowY = true;
            }

            if (rectMove)
            {
                if (Type == GizmoType.Move)
                    _gameObject.Position += mousePos - oldMousePos;
                else if(Type == GizmoType.Scale)
                    _gameObject.Scale += new Vector2f(mousePos.X - oldMousePos.X, -(mousePos.Y - oldMousePos.Y)) / 10;
            }
            else if (rectArrowX)
            {
                if (Type == GizmoType.Move)
                    _gameObject.Position += new Vector2f(mousePos.X - oldMousePos.X, 0);
                else if (Type == GizmoType.Scale)
                    _gameObject.Scale += new Vector2f(mousePos.X - oldMousePos.X, 0) / 10;
            }
            else if(rectArrowY)
            {
                if(Type == GizmoType.Move)
                    _gameObject.Position += new Vector2f(0, mousePos.Y - oldMousePos.Y);
                else if (Type == GizmoType.Scale)
                    _gameObject.Scale -= new Vector2f(0, mousePos.Y - oldMousePos.Y) / 10;
            }

            if (!Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                rectMove = false;
                rectArrowX = false;
                rectArrowY = false;
            }
        }
    }
}
