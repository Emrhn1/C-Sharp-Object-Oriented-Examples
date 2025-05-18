using System;
using System.Collections.Generic;
using System.Linq;
public enum Difficulty
{
    Training,
    Easy,
    NotSoEasy,
    Hard,
    VeryHard,
    HorriblyHard
}
public class Monster
{
    public string Name { get; set; }
    public int HealthPoints { get; set; }
    public int Damage { get; set; }

    public Monster(string name, int healthPoints, int damage)
    {
        Name = name;
        HealthPoints = healthPoints;
        Damage = damage;
    }

    public override string ToString()
    {
        return $"{Name} (HP: {HealthPoints}, Damage: {Damage})";
    }
}
public class Mercenary
{
    public string Name { get; set; }
    public int Level { get; set; }
    public int ExperiencePoints { get; set; }
    public int CurrentHealth { get; set; }
    public int MaxHealth { get; set; }
    public int Damage { get; set; }
    public int Gold { get; set; }

    public Mercenary(string name, int level, int experiencePoints, int currentHealth, int maxHealth, int damage, int gold)
    {
        Name = name;
        Level = level;
        ExperiencePoints = experiencePoints;
        CurrentHealth = currentHealth;
        MaxHealth = maxHealth;
        Damage = damage;
        Gold = gold;
    }

    public override string ToString()
    {
        return $"{Name} (Level: {Level}, HP: {CurrentHealth}/{MaxHealth}, Damage: {Damage}, Gold: {Gold})";
    }
}

public class Quest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public Difficulty Difficulty { get; set; }
    public Monster Monster { get; set; }
    public int ExperienceReward { get; set; }
    public int GoldReward { get; set; }

    public Quest(string name, string description, string location, Difficulty difficulty, Monster monster, int experienceReward, int goldReward)
    {
        Name = name;
        Description = description;
        Location = location;
        Difficulty = difficulty;
        Monster = monster;
        ExperienceReward = experienceReward;
        GoldReward = goldReward;
    }

    public override string ToString()
    {
        return $"{Name} (Location: {Location}, Difficulty: {Difficulty}, Monster: {Monster}, XP Reward: {ExperienceReward}, Gold Reward: {GoldReward})";
    }
}

public class Guild
{
    private List<Mercenary> mercenaries;
    private List<Quest> quests;

    public delegate void MercenaryEventHandler(Mercenary mercenary);
    public delegate void QuestEventHandler(Quest quest);
    public delegate void QuestCompletionEventHandler(Mercenary mercenary, Quest quest);

    public event QuestEventHandler OnQuestAdded;
    public event QuestCompletionEventHandler OnQuestCompleting;
    public event QuestCompletionEventHandler OnQuestCompleted;

    public Guild()
    {
        mercenaries = new List<Mercenary>();
        quests = new List<Quest>();
    }
    public void HireMercenary(Mercenary mercenary)
    {
        if (mercenaries.Any(m => m.Name == mercenary.Name))
        {
            throw new Exception("Mercenary with this name already exists.");
        }
        mercenaries.Add(mercenary);
        OnMercenaryHired?.Invoke(mercenary); 
        Console.WriteLine($"{mercenary.Name} has been hired!");
    }

    public void AddQuest(Quest quest)
    {
        if (quests.Any(q => q.Name == quest.Name))
        {
            throw new Exception("Quest with this name already exists.");
        }
        quests.Add(quest);
        OnQuestAdded?.Invoke(quest); 
        Console.WriteLine($"Quest '{quest.Name}' has been added!");
    }

   
    public void SendMercenaryOnQuest(string mercenaryName, string questName)
    {
        var mercenary = mercenaries.FirstOrDefault(m => m.Name == mercenaryName);
        var quest = quests.FirstOrDefault(q => q.Name == questName);

        if (mercenary == null)
        {
            Console.WriteLine("Mercenary not found.");
            return;
        }

        if (quest == null)
        {
            Console.WriteLine("Quest not found.");
            return;
        }

        OnQuestCompleting?.Invoke(mercenary, quest);
        Console.WriteLine($"{mercenary.Name} is going on the quest '{quest.Name}'!");
        mercenary.CurrentHealth -= quest.Monster.Damage; 

        if (mercenary.CurrentHealth > 0)
        {
            mercenary.ExperiencePoints += quest.ExperienceReward;
            mercenary.Gold += quest.GoldReward;
            OnQuestCompleted?.Invoke(mercenary, quest); 
            Console.WriteLine($"{mercenary.Name} completed the quest and earned {quest.ExperienceReward} XP and {quest.GoldReward} gold!");
        }
        else
        {
            Console.WriteLine($"{mercenary.Name} has fallen in battle!");
        }
    }
    public Mercenary FindMercenary(string name)
    {
        return mercenaries.FirstOrDefault(m => m.Name == name);
    }

    
    public Quest FindQuest(string name)
    {
        return quests.FirstOrDefault(q => q.Name == name);
    }
}
class Program
{
    static void Main(string[] args)
    {
      
        Guild guild = new Guild();

        Mercenary merc1 = new Mercenary("Archer", 1, 0, 100, 100, 15, 0);
        Mercenary merc2 = new Mercenary("Warrior", 1, 0, 120, 120, 20, 0);

        
        Monster monster1 = new Monster("Goblin", 30, 10);
        Quest quest1 = new Quest("Goblin Hunt", "Defeat the goblin", "Forest", Difficulty.Easy, monster1, 50, 20);

        
        guild.OnMercenaryHired += (merc) => Console.WriteLine($"Event: {merc.Name} has been hired!");
        guild.OnQuestAdded += (quest) => Console.WriteLine($"Event: Quest '{quest.Name}' has been added!");
        guild.OnQuestCompleting += (merc, quest) => Console.WriteLine($"Event: {merc.Name} is starting the quest '{quest.Name}'!");
        guild.OnQuestCompleted += (merc, quest) => Console.WriteLine($"Event: {merc.Name} has completed the quest '{quest.Name}'!");

       
        guild.HireMercenary(merc1);
        guild.HireMercenary(merc2);

       
        guild.AddQuest(quest1);

        
        guild.SendMercenaryOnQuest("Archer", "Goblin Hunt");
        guild.SendMercenaryOnQuest("Warrior", "Goblin Hunt");

        
        Console.WriteLine($"{merc1.Name} - HP: {merc1.CurrentHealth}, XP: {merc1.ExperiencePoints}, Gold: {merc1.Gold}");
        Console.WriteLine($"{merc2.Name} - HP: {merc2.CurrentHealth}, XP: {merc2.ExperiencePoints}, Gold: {merc2.Gold}");
    }
}
