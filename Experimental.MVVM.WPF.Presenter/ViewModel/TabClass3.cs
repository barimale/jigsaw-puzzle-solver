using Solver.Tangram.Game.Logic;
using System;

namespace Demo.ViewModel
{
    public class TabClass3 : TabBase
    {
        private Game _gameInstance;

        public TabClass3(ref Game gameInstance)
        {
            this._gameInstance = gameInstance;
        }

        public string MyStringContent { get; set; }
        public Uri MyImageUrl { get; set; }
    }
}
