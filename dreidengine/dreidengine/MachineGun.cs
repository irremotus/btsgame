﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using JigLibX.Physics;
using JigLibX.Geometry;
using JigLibX.Collision;
using System.Windows;

namespace dreidengine
{
    class MachineGun : Gun
    {
        public MachineGun(Game game, Vector3 pos)
            : base(game, "M9", pos, new Vector3(0, 0, 0), 50, 2000, 10, 10000, true, 999, 15, 100, 10)
        {

        }
    }
}
