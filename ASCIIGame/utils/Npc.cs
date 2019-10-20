using System;
using System.Collections.Generic;
using System.Text;

namespace ASCIIGame.utils
{
    class Npc : Entity
    {
        public enum Aggressiveness
        {
            Passive,
            Aggressive
        }
        public string Description { get; set; }
        public Aggressiveness AggressivenessType { get; set; }
        public Npc(string name, string examineDescription):base(name)
        {
            Description = examineDescription;
            InitializeDefault();
        }

        private void InitializeDefault()
        {
            AggressivenessType = Aggressiveness.Passive;
        }
    }
}
