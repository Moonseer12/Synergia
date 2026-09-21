using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.Achievements;
using Synergia.Common;

namespace Synergia.Content.Achievements
{
    public class SaveAchieveIfCompleted
    {
        static string FilePath => Path.Combine(Main.SavePath, "SynergiaUtil", "Achievements.json");
        public static SaveAchieveIfCompleted SaveSystem { get; private set; } = new();

        [JsonProperty]
        public List<CompletedEntry> Completed { get; set; } = new();

        public static void Load()
        {
            if (File.Exists(FilePath))
            {
                try
                {
                    SaveSystem = JsonConvert.DeserializeObject<SaveAchieveIfCompleted>(File.ReadAllText(FilePath))
                                 ?? new SaveAchieveIfCompleted();

                    if (SaveSystem.Completed == null)
                    {
                        SaveSystem.Completed = new List<CompletedEntry>();
                    }
                }
                catch
                {
                    SaveSystem = new SaveAchieveIfCompleted();
                    Save();
                }
            }
            else
            {
                SaveSystem = new SaveAchieveIfCompleted();
                Save();
            }
        }

        public static void Save()
        {
            string directory = Path.GetDirectoryName(FilePath);
            if (!string.IsNullOrEmpty(directory))
            {
                Directory.CreateDirectory(directory);
            }
            File.WriteAllText(FilePath, JsonConvert.SerializeObject(SaveSystem, Formatting.Indented));
        }

        public static void MarkCompleted(string achievementName, string conditionName, bool isCompleted)
        {
            if (SaveSystem?.Completed == null) return;

            CompletedEntry entry = SaveSystem.Completed.Find(e => e.Achievement == achievementName && e.Condition == conditionName);

            if (entry == null)
            {
                SaveSystem.Completed.Add(new CompletedEntry { Achievement = achievementName, Condition = conditionName, Value = isCompleted });
            }
            else
            {
                entry.Value = isCompleted;
            }

            Save();
        }

        public static void Restore()
        {
            if (SaveSystem?.Completed == null) return;

            foreach (CompletedEntry completed in SaveSystem.Completed.ToArray())
            {
                if (completed == null || !completed.Value)
                {
                    continue;
                }

                if (Main.Achievements == null) continue;

                AchievementCondition cond = Main.Achievements.GetCondition(completed.Achievement, completed.Condition);

                if (cond == null)
                {
                    continue;
                }

                cond.Complete();
            }
        }
    }
}