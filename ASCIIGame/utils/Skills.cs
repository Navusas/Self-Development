using System;
using System.Collections.Generic;
using System.Text;

namespace ASCIIGame.utils
{
    class Skills
    {
        #region Properties
        public int Health { get; set; }
        public int Attack { get; set; }
        public int Strength { get; set; }
        public int Defence { get; set; }
        public int AttackSpeed { get; set; }
        public int Mage { get; set; }
        public int Range { get; set; }
        public int Prayer { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor, when no parameters are given, set every skill to -> 1
        /// </summary>
        public Skills()
        {
            Health = 1;
            Attack = 1;
            Strength = 1;
            Defence = 1;
            AttackSpeed = 1;
            Mage = 1;
            Range = 1;
            Prayer = 1;
        }

        /// <summary>
        /// Parameters are given
        /// </summary>
        public Skills(int health, int attack, int strength, int defence, int attackSpeed, int mage, int range, int prayer)
        {
            Health = health;
            Attack = attack;
            Strength = strength;
            Defence = defence;
            AttackSpeed = attackSpeed;
            Mage = mage;
            Range = range;
            Prayer = prayer;
        }
        #endregion
    }
}