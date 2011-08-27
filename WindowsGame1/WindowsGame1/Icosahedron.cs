using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tad.Xna.Common.Cameras;
using Tad.Xna.Common.Entities;

namespace WindowsGame1
{
    public class Icosahedron : GameEntity
    {
        BasicEffect effect;
        private VertexPositionColor[] vertices;
        private int[] indices;

        public Icosahedron(Game game) : base(game)
        {
            IsStatic = true;
        }

        public override void Initialize()
        {
            

            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            float X = 30, Z = 30;
            vertices = new VertexPositionColor[]
            {
                new VertexPositionColor(new Vector3(-X, 0.0f, Z), Color.White),
                new VertexPositionColor(new Vector3(X, 0.0f, Z), Color.White),
                new VertexPositionColor(new Vector3(-X, 0.0f, -Z), Color.White),
                new VertexPositionColor(new Vector3(X, 0.0f, -Z), Color.White),   
                new VertexPositionColor(new Vector3(0.0f, Z, X), Color.White),
                new VertexPositionColor(new Vector3(0.0f, Z, -X), Color.White),
                new VertexPositionColor(new Vector3(0.0f, -Z, X), Color.White),
                new VertexPositionColor(new Vector3(0.0f, -Z, -X), Color.White),  
                new VertexPositionColor(new Vector3(Z, X, 0.0f), Color.White),
                new VertexPositionColor(new Vector3(-Z, X, 0.0f), Color.White),
                new VertexPositionColor(new Vector3(Z, -X, 0.0f), Color.White),
                new VertexPositionColor(new Vector3(-Z, -X, 0.0f), Color.White),
            };
            indices = new int[]
            {
                0,4,1, 
                0,9,4, 
                9,5,4, 
                4,5,8, 
                4,8,1,    
                8,10,1, 
                8,3,10, 
                5,3,8, 
                5,2,3, 
                2,7,3,    
                7,10,3, 
                7,6,10, 
                7,11,6, 
                11,0,6, 
                0,1,6, 
                6,1,10, 
                9,0,11, 
                9,11,2,
                9,2,5, 
                7,2,11            
            };

            effect = new BasicEffect(Game.GraphicsDevice);
            effect.EnableDefaultLighting();
            effect.LightingEnabled = false;
            effect.AmbientLightColor = new Vector3(255, 255, 255);

        }

        private static RasterizerState _wireframe = new RasterizerState() {FillMode = FillMode.WireFrame};
        private static RasterizerState _solid = new RasterizerState() { FillMode = FillMode.Solid};

        public override void Draw(GameTime gameTime)
        {
            effect.Projection = Camera.Default.ProjectionMatrix;
            effect.View = Camera.Default.ViewMatrix;
            effect.World = Matrix.Identity
                 * Matrix.CreateTranslation(this.Position);

            if (DrawWireframe)
                GraphicsDevice.RasterizerState = _wireframe;
            else
                GraphicsDevice.RasterizerState = _solid;

            foreach (var pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                GraphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList,
                    vertices, 0, vertices.Length, indices, 0, indices.Length/3);
            }

            base.Draw(gameTime);
        }
    }
}
