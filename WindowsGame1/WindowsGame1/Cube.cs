using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tad.Xna.Common.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1
{
    public class Cube : GameEntity
    {
        private BasicEffect effect;
        public float _sideLength;
        CubeFace[] faces;
        private int[] Indices;
        private VertexPositionColor[] Vertices;

        public Cube(Game game, Vector3 position, float sideLength) : base(game)
        {
            this._position = position;
            this._sideLength = sideLength;
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            VertexPositionColor[] vertices = new VertexPositionColor[]
            {
                new VertexPositionColor(new Vector3(-_sideLength,-_sideLength,-_sideLength), Color.White),
                new VertexPositionColor(new Vector3(_sideLength,-_sideLength,-_sideLength), Color.White),
                new VertexPositionColor(new Vector3(_sideLength,-_sideLength,_sideLength), Color.White),
                new VertexPositionColor(new Vector3(-_sideLength,-_sideLength,_sideLength), Color.White),
                new VertexPositionColor(new Vector3(-_sideLength,_sideLength,-_sideLength), Color.White),
                new VertexPositionColor(new Vector3(_sideLength,_sideLength,-_sideLength), Color.White),
                new VertexPositionColor(new Vector3(_sideLength,_sideLength,_sideLength), Color.White),
                new VertexPositionColor(new Vector3(-_sideLength,_sideLength,_sideLength), Color.White),
            };

            faces = new CubeFace[] {
                new CubeFace(this, vertices[0], vertices[1], vertices[2], vertices[3])/*,
                new CubeFace(this, vertices[4], vertices[0], vertices[3], vertices[7]),
                new CubeFace(this, vertices[5], vertices[6], vertices[7], vertices[5]),
                new CubeFace(this, vertices[1], vertices[2], vertices[6], vertices[5]),
                new CubeFace(this, vertices[6], vertices[2], vertices[3], vertices[7])*/
            };

            effect = new BasicEffect(GraphicsDevice);

            List<VertexPositionColor> verts = new List<VertexPositionColor>();
            List<int> indices = new List<int>();
            foreach(var face in faces)
            {
                foreach (var triangle in face.Triangles)
                {
                    if (!verts.Contains(triangle._v1))
                        verts.Add(triangle._v1);
                    if (!verts.Contains(triangle._v2))
                        verts.Add(triangle._v2);
                    if (!verts.Contains(triangle._v3))
                        verts.Add(triangle._v3);

                    indices.Add(verts.IndexOf(triangle._v1));
                    indices.Add(verts.IndexOf(triangle._v2));
                    indices.Add(verts.IndexOf(triangle._v3));
                }
            }

            Vertices = vertices.ToArray();
            Indices = indices.ToArray();

        }

        public override void Draw(GameTime gameTime)
        {
            foreach(var pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                
                GraphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList,
                                     Vertices, 0, Vertices.Length, Indices, 0, Indices.Length/3);
            }

            base.Draw(gameTime);
        }
    }

    public class CubeFace
    {
        Cube _cube;
        public CubeTriangle[] Triangles;

        public CubeFace(Cube cube, 
            VertexPositionColor v1, 
            VertexPositionColor v2, 
            VertexPositionColor v3, 
            VertexPositionColor v4)
        {
            _cube = cube;
            Triangles = new CubeTriangle[]
            {
                new CubeTriangle(cube, this, null, v1, v2, v3),
                new CubeTriangle(cube, this, null, v2, v3, v4)
            };
        }

        public void LoadContent()
        {
        }
    }

    public class CubeTriangle
    {
        private  Cube _cube;
        private  CubeFace _face;
        private CubeTriangle[] children;
        private  CubeTriangle _parent;
        public readonly VertexPositionColor _v1;
        public readonly VertexPositionColor _v2;
        public readonly VertexPositionColor _v3;

        public CubeTriangle(Cube cube, CubeFace face, CubeTriangle parent,
            VertexPositionColor v1,
            VertexPositionColor v2,
            VertexPositionColor v3)
        {
            _cube = cube;
            _face = face;
            _parent = parent;
            _v1 = v1;
            _v2 = v2;
            _v3 = v3;
        }
    }
}
