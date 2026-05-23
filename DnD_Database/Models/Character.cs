using System;

namespace DnD_Database.Models
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ClassName { get; set; }
        public int Level { get; set; }
        public string Strength { get; set; }
        public string Agility { get; set; }
        public string Endurance { get; set; }
        public string Charisma { get; set; }
        public string Wit { get; set; }
        public string Trait { get; set; }
        public string Flaw { get; set; }
        public string Weapon { get; set; }
        public int Defense { get; set; }
        public int HP { get; set; }

        public override string ToString()
        {
            return $"[{Id}] {Name} | {ClassName} | Уровень: {Level} | HP: {HP} | Защита: {Defense} | {Weapon}";
        }
    }
}