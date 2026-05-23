using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using DnD_Database.Models;

namespace DnD_Database
{
    public class DatabaseHelper
    {
        private string connectionString = "Data Source=characters.db";

        public void InitializeDatabase()
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string createTableQuery = @"
                    CREATE TABLE IF NOT EXISTS Characters (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT NOT NULL,
                        ClassName TEXT NOT NULL,
                        Level INTEGER NOT NULL,
                        Strength TEXT,
                        Agility TEXT,
                        Endurance TEXT,
                        Charisma TEXT,
                        Wit TEXT,
                        Trait TEXT,
                        Flaw TEXT,
                        Weapon TEXT,
                        Defense INTEGER,
                        HP INTEGER
                    )";

                using (var command = new SqliteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public void AddCharacter(Character character)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string insertQuery = @"
                    INSERT INTO Characters (Name, ClassName, Level, Strength, Agility, Endurance, Charisma, Wit, Trait, Flaw, Weapon, Defense, HP)
                    VALUES (@Name, @ClassName, @Level, @Strength, @Agility, @Endurance, @Charisma, @Wit, @Trait, @Flaw, @Weapon, @Defense, @HP)";

                using (var command = new SqliteCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@Name", character.Name);
                    command.Parameters.AddWithValue("@ClassName", character.ClassName);
                    command.Parameters.AddWithValue("@Level", character.Level);
                    command.Parameters.AddWithValue("@Strength", character.Strength);
                    command.Parameters.AddWithValue("@Agility", character.Agility);
                    command.Parameters.AddWithValue("@Endurance", character.Endurance);
                    command.Parameters.AddWithValue("@Charisma", character.Charisma);
                    command.Parameters.AddWithValue("@Wit", character.Wit);
                    command.Parameters.AddWithValue("@Trait", character.Trait);
                    command.Parameters.AddWithValue("@Flaw", character.Flaw);
                    command.Parameters.AddWithValue("@Weapon", character.Weapon);
                    command.Parameters.AddWithValue("@Defense", character.Defense);
                    command.Parameters.AddWithValue("@HP", character.HP);

                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Character> GetAllCharacters()
        {
            var characters = new List<Character>();
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string selectQuery = "SELECT * FROM Characters";

                using (var command = new SqliteCommand(selectQuery, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        characters.Add(new Character
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                            ClassName = reader["ClassName"].ToString(),
                            Level = Convert.ToInt32(reader["Level"]),
                            Strength = reader["Strength"].ToString(),
                            Agility = reader["Agility"].ToString(),
                            Endurance = reader["Endurance"].ToString(),
                            Charisma = reader["Charisma"].ToString(),
                            Wit = reader["Wit"].ToString(),
                            Trait = reader["Trait"].ToString(),
                            Flaw = reader["Flaw"].ToString(),
                            Weapon = reader["Weapon"].ToString(),
                            Defense = Convert.ToInt32(reader["Defense"]),
                            HP = Convert.ToInt32(reader["HP"])
                        });
                    }
                }
            }
            return characters;
        }

        public void DeleteCharacter(int id)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string deleteQuery = "DELETE FROM Characters WHERE Id = @Id";

                using (var command = new SqliteCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void ClearTable()
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string deleteQuery = "DELETE FROM Characters";

                using (var command = new SqliteCommand(deleteQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}