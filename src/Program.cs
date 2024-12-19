namespace rogalik
{
    class Weapon
    {
        public string name;
        public int damage;
    }

    class Aid_Kit
    {
        public string name;
        public int hp_healed;
    }
    class Enemy
    {
        public string name;
        public int hp;
        public Weapon weapon = new Weapon();
        public int points;
    }
    class Player
    {
        public string name;
        public int hp = 200;
        public int hp_max = 200;
        public Aid_Kit aid_kit = new Aid_Kit();
        public Weapon weapon = new Weapon();
        public int points = 0;

        public void heal()
        {
            hp += aid_kit.hp_healed;
            if (hp > hp_max)
            {
                hp = hp_max;
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> weapon_names = new List<string>() { "Экскалибур", "Меч-кладенец", "Меч Грааля", "Бальмунг", "Фламберг" };
            List<int> weapon_damage = new List<int>() { 10, 20, 30, 50, 80 };
            List<string> enemy_names = new List<string>() { "Обезображенный", "Сердитая толпа", "Странствующий рыцарь",
            "Туманоносец", "Древесный человек", "Клубок змей", "Искажённое дитя", "Могильный волк", "Лесной змей"};

            List<string> aid_names = new List<string>() { "Малая аптечка", "Средняя аптечка", "Большая аптечка" };
            List<int> aid_values = new List<int>() { 10, 20, 50 };


            bool player_is_dead = false;
            Player player = new Player();
            Enemy enemy = new Enemy();

            Console.WriteLine("Добро пожаловать!\nКак тебя зовут?");
            player.name = Console.ReadLine();
            Console.WriteLine($"\nВаше имя: {player.name}.\n");

            Random n1 = new Random();

            int weapon_choice_player = n1.Next(0, 5);
            player.weapon.name = weapon_names[weapon_choice_player];
            player.weapon.damage = weapon_damage[weapon_choice_player];

            int aid_choice = n1.Next(0, 3);
            player.aid_kit.name = aid_names[aid_choice];
            player.aid_kit.hp_healed = aid_values[aid_choice];

            Console.WriteLine($"Вы получили {player.weapon.name} ({player.weapon.damage}), а также {player.aid_kit.name} ({player.aid_kit.hp_healed}).\n");

            while (player_is_dead == false)
            {
                enemy.name = enemy_names[n1.Next(enemy_names.Count)];
                int weapon_choice_enemy = n1.Next(0, 5);
                enemy.weapon.name = weapon_names[weapon_choice_enemy];
                enemy.weapon.damage = weapon_damage[weapon_choice_enemy];
                enemy.hp = n1.Next(20, 151);
                enemy.points = n1.Next(10, 51);
                Console.WriteLine($"{player.name} встречает {enemy.name} ({enemy.hp} hp), его оружие: {enemy.weapon.name} ({enemy.weapon.damage}).");
                Console.WriteLine($"Ваше оружие: {player.weapon.name} ({player.weapon.damage})");
                Console.WriteLine($"За победу вы получите {enemy.points} очков.");
                Console.WriteLine($"У вас {player.hp} hp.");

                bool enemy_is_dead = false;
                while (enemy_is_dead == false && player_is_dead == false)
                {

                    Console.WriteLine("\nЧто вы будете делать?\n1. Ударить\n2. Пропустить ход\n3. Использовать аптечку\n");
                    int choice = Convert.ToInt32(Console.ReadLine());

                    if (choice == 1)
                    {
                        Console.WriteLine($"{player.name} ударил противника {enemy.name}\n");

                        enemy.hp -= player.weapon.damage;
                        player.hp -= enemy.weapon.damage;
                        if (player.hp <= 0 && enemy.hp <= 0)
                        {
                            Console.WriteLine($"Вы и {enemy.name} сразили друг друга.");
                            player_is_dead = true;
                            break;
                        }
                        else if (player.hp <= 0)
                        {
                            Console.WriteLine($"У противника остаётся {enemy.hp} hp.");
                            Console.WriteLine("Вы погибаете.");
                            player_is_dead = true;
                            break;
                        }
                        else if (enemy.hp <= 0)
                        {
                            Console.WriteLine("Враг побеждён!");
                            enemy_is_dead = true;
                            player.points += enemy.points;
                            Console.WriteLine($"У вас {player.points} очков.\n");
                            break;
                        }

                        Console.WriteLine($"У противника {enemy.hp} hp, у вас {player.hp} hp.");
                    }
                    else if (choice == 2)
                    {
                        player.hp -= enemy.weapon.damage;
                        if (player.hp <= 0)
                        {
                            Console.WriteLine($"У противника остаётся {enemy.hp} hp.");
                            Console.WriteLine("Вы погибаете.");
                            player_is_dead = true;
                            break;
                        }
                        Console.WriteLine($"У противника {enemy.hp} hp, у вас {player.hp} hp.");
                    }
                    else if (choice == 3)
                    {
                        Console.WriteLine($"{player.name} использовал аптечку.");
                        player.heal();
                        Console.WriteLine($"У противника {enemy.hp} hp, у вас {player.hp} hp.");
                    }
                    else
                    {
                        Console.WriteLine("Выбрано неверное число.");
                    }
                }
            }
        }
    }
}
