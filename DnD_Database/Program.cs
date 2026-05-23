using System;
using DnD_Database;
using DnD_Database.Models;

namespace DnD_Database
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new DatabaseHelper();
            db.InitializeDatabase();

            Console.WriteLine("=================================");
            Console.WriteLine("   БАЗА ДАННЫХ ПЕРСОНАЖЕЙ D&D");
            Console.WriteLine("=================================");
            Console.WriteLine();

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\nМеню:");
                Console.WriteLine("1. Заполнить базу данных 32 персонажами");
                Console.WriteLine("2. Показать всех персонажей");
                Console.WriteLine("3. Показать персонажей по классу");
                Console.WriteLine("4. Удалить персонажа по ID");
                Console.WriteLine("5. Выход");
                Console.Write("\nВыберите действие: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        FillWithSampleCharacters(db);
                        break;
                    case "2":
                        ShowAllCharacters(db);
                        break;
                    case "3":
                        ShowCharactersByClass(db);
                        break;
                    case "4":
                        DeleteCharacter(db);
                        break;
                    case "5":
                        exit = true;
                        Console.WriteLine("Программа завершена.");
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }
        }

        static void FillWithSampleCharacters(DatabaseHelper db)
        {
            Console.WriteLine("Заполнение базы данных...");
            db.ClearTable();
            var characters = GetSampleCharacters();
            foreach (var character in characters)
            {
                db.AddCharacter(character);
            }
            Console.WriteLine($"Добавлено {characters.Count} персонажей!");
        }

        static void ShowAllCharacters(DatabaseHelper db)
        {
            var characters = db.GetAllCharacters();
            if (characters.Count == 0)
            {
                Console.WriteLine("База данных пуста.");
                return;
            }

            Console.WriteLine("СПИСОК ВСЕХ ПЕРСОНАЖЕЙ:");
            Console.WriteLine("=========================================");
            foreach (var character in characters)
            {
                Console.WriteLine(character);
                Console.WriteLine($"   Класс: {character.ClassName} | Сила: {character.Strength} | Ловкость: {character.Agility} | Выносливость: {character.Endurance}");
                Console.WriteLine($"   Харизма: {character.Charisma} | Смекалка: {character.Wit} | Черта: {character.Trait} | Изъян: {character.Flaw}");
                Console.WriteLine($"   Оружие: {character.Weapon}");
                Console.WriteLine("-----------------------------------------");
            }
        }

        static void ShowCharactersByClass(DatabaseHelper db)
        {
            Console.Write("Введите класс (Воины/Маги/Плуты/Луки/Клирики/Барды): ");
            string className = Console.ReadLine();

            var characters = db.GetAllCharacters();
            var filtered = characters.FindAll(c => c.ClassName.Equals(className, StringComparison.OrdinalIgnoreCase));

            if (filtered.Count == 0)
            {
                Console.WriteLine("Персонажей с таким классом не найдено.");
                return;
            }

            Console.WriteLine($"\nПерсонажи класса '{className}':");
            foreach (var character in filtered)
            {
                Console.WriteLine(character);
            }
        }

        static void DeleteCharacter(DatabaseHelper db)
        {
            Console.Write("Введите ID персонажа для удаления: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                db.DeleteCharacter(id);
                Console.WriteLine($"Персонаж с ID {id} удален.");
            }
            else
            {
                Console.WriteLine("Неверный ID.");
            }
        }

        static List<Character> GetSampleCharacters()
        {
            return new List<Character>
            {
                new Character { Name = "Борис Могучий", ClassName = "Воины", Level = 3, Strength = "d10", Agility = "d6", Endurance = "d8", Charisma = "d4", Wit = "d5", Trait = "бесстрашный", Flaw = "упрямый", Weapon = "двуручный меч", Defense = 10, HP = 24 },
                new Character { Name = "Анна Стальная", ClassName = "Воины", Level = 4, Strength = "d8", Agility = "d7", Endurance = "d9", Charisma = "d5", Wit = "d6", Trait = "тактик", Flaw = "подозрительная", Weapon = "меч и щит", Defense = 12, HP = 27 },
                new Character { Name = "Гром Каменный", ClassName = "Воины", Level = 5, Strength = "d12", Agility = "d4", Endurance = "d10", Charisma = "d3", Wit = "d4", Trait = "неудержимый", Flaw = "медлительный", Weapon = "боевой топор", Defense = 9, HP = 30 },
                new Character { Name = "Виктор Храбрый", ClassName = "Воины", Level = 2, Strength = "d7", Agility = "d8", Endurance = "d8", Charisma = "d6", Wit = "d7", Trait = "лидер", Flaw = "импульсивный", Weapon = "копье", Defense = 8, HP = 24 },
                new Character { Name = "Ольга Северная", ClassName = "Воины", Level = 3, Strength = "d8", Agility = "d9", Endurance = "d7", Charisma = "d4", Wit = "d8", Trait = "хладнокровная", Flaw = "одиночка", Weapon = "алебарда", Defense = 9, HP = 21 },
                new Character { Name = "Дмитрий Щитоносец", ClassName = "Воины", Level = 4, Strength = "d6", Agility = "d6", Endurance = "d10", Charisma = "d7", Wit = "d7", Trait = "защитник", Flaw = "нерешительный", Weapon = "щит и булава", Defense = 14, HP = 30 },

                new Character { Name = "Архимаг Сергий", ClassName = "Маги", Level = 5, Strength = "d3", Agility = "d5", Endurance = "d4", Charisma = "d8", Wit = "d12", Trait = "мудрый", Flaw = "высокомерный", Weapon = "посох огня", Defense = 5, HP = 12 },
                new Character { Name = "Лилия Туманная", ClassName = "Маги", Level = 3, Strength = "d4", Agility = "d6", Endurance = "d5", Charisma = "d9", Wit = "d10", Trait = "загадочная", Flaw = "рассеянная", Weapon = "кристалл льда", Defense = 6, HP = 15 },
                new Character { Name = "Макс Громовой", ClassName = "Маги", Level = 4, Strength = "d5", Agility = "d5", Endurance = "d6", Charisma = "d7", Wit = "d11", Trait = "энергичный", Flaw = "вспыльчивый", Weapon = "жезл молний", Defense = 6, HP = 18 },
                new Character { Name = "Елена Звездочет", ClassName = "Маги", Level = 5, Strength = "d3", Agility = "d7", Endurance = "d4", Charisma = "d8", Wit = "d12", Trait = "провидица", Flaw = "замкнутая", Weapon = "астролябия", Defense = 5, HP = 12 },
                new Character { Name = "Валерий Теней", ClassName = "Маги", Level = 3, Strength = "d4", Agility = "d8", Endurance = "d5", Charisma = "d6", Wit = "d10", Trait = "скрытный", Flaw = "параноик", Weapon = "кинжал тьмы", Defense = 7, HP = 15 },
                new Character { Name = "Ирина Целитель", ClassName = "Маги", Level = 4, Strength = "d4", Agility = "d6", Endurance = "d6", Charisma = "d10", Wit = "d9", Trait = "сострадательная", Flaw = "доверчивая", Weapon = "жезл исцеления", Defense = 6, HP = 18 },

                new Character { Name = "Алекс Теневой", ClassName = "Плуты", Level = 3, Strength = "d5", Agility = "d10", Endurance = "d6", Charisma = "d7", Wit = "d8", Trait = "ловкий", Flaw = "жадный", Weapon = "парные кинжалы", Defense = 7, HP = 18 },
                new Character { Name = "Ксения Лиса", ClassName = "Плуты", Level = 4, Strength = "d4", Agility = "d11", Endurance = "d5", Charisma = "d9", Wit = "d9", Trait = "обаятельная", Flaw = "лгунья", Weapon = "рапира", Defense = 8, HP = 15 },
                new Character { Name = "Никита Бесшумный", ClassName = "Плуты", Level = 3, Strength = "d6", Agility = "d10", Endurance = "d7", Charisma = "d5", Wit = "d8", Trait = "незаметный", Flaw = "молчаливый", Weapon = "короткий лук", Defense = 7, HP = 21 },
                new Character { Name = "Мария Острая", ClassName = "Плуты", Level = 2, Strength = "d5", Agility = "d9", Endurance = "d6", Charisma = "d8", Wit = "d8", Trait = "остроумная", Flaw = "дерзкая", Weapon = "кинжалы-метатели", Defense = 8, HP = 18 },
                new Character { Name = "Петр Взломщик", ClassName = "Плуты", Level = 3, Strength = "d6", Agility = "d8", Endurance = "d7", Charisma = "d6", Wit = "d10", Trait = "изобретательный", Flaw = "любопытный", Weapon = "арбалет", Defense = 7, HP = 21 },
                new Character { Name = "София Трюкач", ClassName = "Плуты", Level = 4, Strength = "d4", Agility = "d10", Endurance = "d5", Charisma = "d10", Wit = "d9", Trait = "харизматичная", Flaw = "азартная", Weapon = "карты-лезвия", Defense = 6, HP = 15 },

                new Character { Name = "Иван Лесной", ClassName = "Луки", Level = 3, Strength = "d7", Agility = "d9", Endurance = "d8", Charisma = "d6", Wit = "d8", Trait = "выносливый", Flaw = "нелюдимый", Weapon = "длинный лук", Defense = 7, HP = 24 },
                new Character { Name = "Алиса Следопыт", ClassName = "Луки", Level = 4, Strength = "d6", Agility = "d10", Endurance = "d7", Charisma = "d7", Wit = "d9", Trait = "наблюдательная", Flaw = "упрямая", Weapon = "лук и меч", Defense = 8, HP = 21 },
                new Character { Name = "Олег Волчий", ClassName = "Луки", Level = 3, Strength = "d8", Agility = "d8", Endurance = "d8", Charisma = "d5", Wit = "d7", Trait = "дикий", Flaw = "агрессивный", Weapon = "топор и лук", Defense = 8, HP = 24 },
                new Character { Name = "Татьяна Точная", ClassName = "Луки", Level = 5, Strength = "d5", Agility = "d11", Endurance = "d6", Charisma = "d6", Wit = "d10", Trait = "меткая", Flaw = "перфекционистка", Weapon = "снайперский лук", Defense = 6, HP = 18 },
                new Character { Name = "Роман Охотник", ClassName = "Луки", Level = 4, Strength = "d7", Agility = "d8", Endurance = "d9", Charisma = "d7", Wit = "d8", Trait = "терпеливый", Flaw = "мстительный", Weapon = "копье и сеть", Defense = 8, HP = 27 },

                new Character { Name = "Отец Михаил", ClassName = "Клирики", Level = 4, Strength = "d6", Agility = "d5", Endurance = "d7", Charisma = "d10", Wit = "d9", Trait = "благочестивый", Flaw = "фанатичный", Weapon = "боевой молот", Defense = 10, HP = 21 },
                new Character { Name = "Сестра Анна", ClassName = "Клирики", Level = 3, Strength = "d5", Agility = "d6", Endurance = "d8", Charisma = "d11", Wit = "d8", Trait = "милосердная", Flaw = "наивная", Weapon = "жезл света", Defense = 9, HP = 24 },
                new Character { Name = "Паладин Виктор", ClassName = "Клирики", Level = 5, Strength = "d9", Agility = "d6", Endurance = "d8", Charisma = "d9", Wit = "d7", Trait = "благородный", Flaw = "самокритичный", Weapon = "меч света", Defense = 12, HP = 24 },
                new Character { Name = "Жрица Елена", ClassName = "Клирики", Level = 4, Strength = "d4", Agility = "d7", Endurance = "d7", Charisma = "d10", Wit = "d9", Trait = "мудрая", Flaw = "нерешительная", Weapon = "посох природы", Defense = 8, HP = 21 },
                new Character { Name = "Брат Дмитрий", ClassName = "Клирики", Level = 3, Strength = "d8", Agility = "d5", Endurance = "d9", Charisma = "d8", Wit = "d7", Trait = "стойкий", Flaw = "медлительный", Weapon = "щит веры", Defense = 13, HP = 27 },

                new Character { Name = "Шут Гороховый", ClassName = "Барды", Level = 1, Strength = "d4", Agility = "d3", Endurance = "d5", Charisma = "d6", Wit = "d7", Trait = "музыкант", Flaw = "сумасшедший", Weapon = "гитара", Defense = 8, HP = 15 },
                new Character { Name = "Бард Владимир", ClassName = "Барды", Level = 2, Strength = "d5", Agility = "d7", Endurance = "d6", Charisma = "d10", Wit = "d8", Trait = "вдохновляющий", Flaw = "хвастливый", Weapon = "лютня", Defense = 7, HP = 18 },
                new Character { Name = "Поэтесса Ольга", ClassName = "Барды", Level = 3, Strength = "d3", Agility = "d6", Endurance = "d4", Charisma = "d11", Wit = "d9", Trait = "красноречивая", Flaw = "драматичная", Weapon = "перо-кинжал", Defense = 5, HP = 12 },
                new Character { Name = "Скоморох Федор", ClassName = "Барды", Level = 2, Strength = "d6", Agility = "d8", Endurance = "d7", Charisma = "d9", Wit = "d8", Trait = "весельчак", Flaw = "несерьезный", Weapon = "трещотка-дубина", Defense = 7, HP = 21 }
            };
        }
    }
}