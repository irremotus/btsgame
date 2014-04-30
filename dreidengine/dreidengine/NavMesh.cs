using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using JigLibX.Physics;
using JigLibX.Geometry;
using JigLibX.Collision;


namespace dreidengine
{
    public class NavMesh : DrawableGameComponent
    {
        Model navMeshModel;
        private string navMeshName;
        private HeightMapInfo heihgtMapInfo;
        public HeightMapInfo HMI { get { return heihgtMapInfo; } }

        public NavMesh(Game game, string name)
            : base(game)
        {
            navMeshName = name;
        }

        protected override void LoadContent()
        {
            navMeshModel = Game.Content.Load<Model>(navMeshName);
            heihgtMapInfo = navMeshModel.Tag as HeightMapInfo;
            base.LoadContent();
        }
    }
}
