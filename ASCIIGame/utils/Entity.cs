using System;
using System.Collections.Generic;
using System.Text;

namespace ASCIIGame.utils
{
    class Entity
    {
        #region Properties
        /// <summary>
        /// Determine differenet types of combat
        /// </summary>
        public enum CombatStyle
        {
            Melee,
            Range,
            Mage
        }

        #region Core
        public string Name { get; set; }
        public Skills Skills { get; set; }

        #endregion

        #region Gameplay related properties
        public bool IsDead { get; set; }
        public bool IsInCombat { get; set; }
        public CombatStyle CurrentCombatStyle { get; set; }

        #endregion

        #endregion

        #region Constructor
        public Entity(string name)
        {
            Name = name;
        }

        private void SetDefaultParams()
        {
            Skills = new Skills();      // set all skills to 1
            IsDead = false;             // not death (why would it be death during the start??) 
            IsInCombat = false;         // not in combat
            CurrentCombatStyle = CombatStyle.Melee; // default melee
        }

        #endregion
    }
}
