using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GameEngine.Core.GameObjects
{
    public class Transformation
    {
        private Transform _transform;
        private Vector2f _position;
        private Vector2f _scale = new Vector2f(1, 1);
        private Vector2f _origin;
        private float _rotation;

        [JsonIgnore]
        public Transform Transform { get => _transform; }

        [JsonInclude]
        public Vector2f Position { get => _position; set { _position = value; UpdateTransform(); } }
        [JsonInclude]
        public float Rotation { get => _rotation; set { _rotation = value; UpdateTransform(); } }
        [JsonInclude]
        public Vector2f Scale { get => _scale; set { _scale = value; UpdateTransform(); } }
        public Vector2f Origin { get => _origin; set {  _origin = value; UpdateTransform(); } }

        public Transformation()
        {
            _transform = new Transform();
            UpdateTransform();
        }

        private void UpdateTransform()
        {
            var angle = -_rotation * 3.141592654F / 180.0F;
            var cosine = (float)Math.Cos(angle);
            var sine = (float)Math.Sin(angle);
            var sxc = _scale.X * cosine;
            var syc = _scale.Y * cosine;
            var sxs = _scale.X * sine;
            var sys = _scale.Y * sine;
            var tx = (-_origin.X * sxc) - (_origin.Y * sys) + _position.X;
            var ty = (_origin.X * sxs) - (_origin.Y * syc) + _position.Y;

            _transform = new Transform(sxc, sys, tx,
                                        -sxs, syc, ty,
                                        0.0F, 0.0F, 1.0F);
        }
    }
}
