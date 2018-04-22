using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Centipede
{
    class GameConstants
    {
        public const int WindowWidth = 720;
        public const int WindowHeight = 720;

        public const int PlayerClampHeight = WindowHeight - 24 * 7;
        public const float PlayerBulletSpeed = 0.4f;
    }
}
