using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player
{
    [Serializable]
    public struct PlayerStats
    {
        public float MovementSpeed;
        public float MaxHealth;

        public PlayerStats(float movementSpeed, float maxHealth)
        {
            MovementSpeed = movementSpeed;
            MaxHealth = maxHealth;
        }
    }
}
