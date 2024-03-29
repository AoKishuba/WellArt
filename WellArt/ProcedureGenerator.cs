using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WellArt
{
    public class ProcedureGenerator
    {
        /// <summary>
        /// Generate a pipetting exercise procedure from a given set of colored wells
        /// </summary>
        /// <param name="colorList">List of used colors (should be a copy of colorList)</param>
        /// <param name="buttonList">List of wells used to create desired image</param>
        /// <param name="volumeSettingCountInput">Number of unique pipettor settings PER SIZE</param>
        public static void GenerateProcedure(List<Color> colorList, List<RoundButton> buttonList, int volumeSettingCountInput)
        {
            // Extract list of used wells from list of buttons representing all wells
            List<Well> wellList = [];
            foreach (RoundButton button in buttonList)
            {
                Well well = (Well)button.Tag;

                if (well.Color != Color.White)
                {
                    wellList.Add(well);
                }
            }

            // Sort wells by color
            Dictionary<Color, List<Well>> wellsByColorDict = [];
            foreach (Color color in colorList)
            {
                wellsByColorDict[color] = [];
            }
            foreach (Well well in wellList)
            {
                wellsByColorDict[well.Color].Add(well);
            }

            // Calculate distribution of pipette settings
            Dictionary<Color, int> settingCountDict = [];
            foreach (Color color in colorList)
            {
                settingCountDict[color] = 0;
            }

            // Distribute volume setting counts
            bool canAddMoreSettings = true; // Must be initialized as true in order for loop to start
            int totalSettingsAsssigned = 0;
            while (canAddMoreSettings)
            {
                canAddMoreSettings = false;
                // Distribute setting counts as evenly as possible
                foreach (KeyValuePair<Color, int> keyValuePair in settingCountDict)
                {
                    if (totalSettingsAsssigned == volumeSettingCountInput)
                    {
                        break;
                    }
                    // Cannot have more volume settings than wells of a given color!
                    if (keyValuePair.Value < wellsByColorDict[keyValuePair.Key].Count)
                    {
                        canAddMoreSettings = true;
                        settingCountDict[keyValuePair.Key]++;
                        totalSettingsAsssigned++;
                    }
                }
                if (totalSettingsAsssigned == volumeSettingCountInput)
                {
                    break;
                }
            }

            // Generate P20 and P200 settings for each color
            Dictionary<Color, List<int>> pTwentyVolumeSettingDict = [];
            Dictionary<Color, List<int>> pTwoHundredVolumeSettingDict = [];
            foreach (Color color in colorList)
            {
                List<int> pTwentySettingList = [];
                List<int> pTwoHundredSettingList = [];

                // Randomize P20 volumes from 2-20 uL
                List<int> pTwentyRange = Enumerable.Range(2, 19).ToList();
                pTwentyRange.Shuffle<int>();
                // Randomize P200 volumes from 50-150 uL
                List<int> pTwoHundredRange = Enumerable.Range(50, 101).ToList();
                pTwoHundredRange.Shuffle<int>();

                // Add correct number of random volume settings to dictionary for given color
                int settingCount = settingCountDict[color];
                for (int i = 0; i < settingCount; i++)
                {
                    pTwentySettingList.Add(pTwentyRange[0]);
                    pTwentyRange.RemoveAt(0);
                    pTwoHundredSettingList.Add(pTwoHundredRange[0]);
                    pTwoHundredRange.RemoveAt(0);
                }
                pTwentyVolumeSettingDict[color] = pTwentySettingList;
                pTwoHundredVolumeSettingDict[color] = pTwoHundredSettingList;
            }

            // Assign volumes to each well and count total volume (for checking pipetting accuracy)
            Random random = new();
            int totalVolume = 0;
            foreach (Color color in wellsByColorDict.Keys)
            {
                int settingCount = pTwentyVolumeSettingDict[color].Count;
                List<Well> coloredWellList = wellsByColorDict[color];

                // No need to randomize volume settings if only one possible setting
                if (settingCount == 1)
                {
                    foreach (Well well in coloredWellList)
                    {

                        well.PTwentyVolume = pTwentyVolumeSettingDict[color][0];
                        well.PTwoHundredVolume = pTwoHundredVolumeSettingDict[color][0];

                        totalVolume += well.PTwentyVolume + well.PTwoHundredVolume;
                    }
                }
                else
                {
                    // Assign each volume once, to ensure all are used
                    for (int i = 0; i < settingCount; i++)
                    {
                        Well well = coloredWellList[i];
                        well.PTwentyVolume = pTwentyVolumeSettingDict[color][i];
                        well.PTwoHundredVolume = pTwoHundredVolumeSettingDict[color][i];

                        totalVolume += well.PTwentyVolume + well.PTwoHundredVolume;
                    }
                    // Assign random settings to remaining wells of current color
                    for (int i = settingCount; i < wellsByColorDict[color].Count; i++)
                    {
                        Well well = coloredWellList[i];
                        // Random.Next upper bound is exclusive
                        int pTwentyVolumeIndex = random.Next(0, settingCount);
                        int pTwoHundredVolumeindex = random.Next(0, settingCount);
                        well.PTwentyVolume = pTwentyVolumeSettingDict[color][pTwentyVolumeIndex];
                        well.PTwoHundredVolume = pTwoHundredVolumeSettingDict[color][pTwoHundredVolumeindex];

                        totalVolume += well.PTwentyVolume + well.PTwoHundredVolume;
                    }
                }
            }

            // Write procedure to file
            // Generate filename from current date and time
            // Create filename from current time
            string fileName = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss-ff") + ".txt";

            // Initialize FileStream to create text file
            using var writer = new StreamWriter(fileName);
            FileStream fs = (FileStream)writer.BaseStream;

            // Group wells of each color by P20 and P200 volume
            foreach (Color color in colorList)
            {
                // Skip colors which were selected by not used
                if (wellsByColorDict[color].Count > 0)
                {
                    // P20
                    Dictionary<int, List<Well>> wellsByPTwentyDict = [];
                    foreach (int pTwentyVolume in pTwentyVolumeSettingDict[color])
                    {
                        wellsByPTwentyDict[pTwentyVolume] = [];
                        foreach (Well well in wellsByColorDict[color])
                        {
                            if (well.PTwentyVolume == pTwentyVolume)
                            {
                                wellsByPTwentyDict[pTwentyVolume].Add(well);
                            }
                        }
                        // Sort list by coördinates (leave volumes random)
                        wellsByPTwentyDict[pTwentyVolume] =
                            [.. wellsByPTwentyDict[pTwentyVolume].OrderBy(a => a.Y).ThenBy(a => a.X)];
                    }
                    // Write instructions to file
                    writer.WriteLine("Using the " + color.Name + " dye:");
                    // Spacing
                    writer.WriteLine("");

                    foreach (int volume in pTwentyVolumeSettingDict[color])
                    {
                        // Generate line of text for procedure
                        // Lines must be written as one string
                        string line = volume.ToString() + " uL: ";
                        line += wellsByPTwentyDict[volume][0].ToString();
                        for (int i = 0; i < wellsByPTwentyDict[volume].Count; i++)
                        {
                            line += ", " + wellsByPTwentyDict[volume][i].ToString();
                        }
                        writer.WriteLine(line);
                    }
                    // Spacing
                    writer.WriteLine("");
                }
            }

            foreach (Color color in colorList)
            {
                // Skip colors which were selected but not used
                if (wellsByColorDict[color].Count > 0)
                {
                    // P200
                    Dictionary<int, List<Well>> wellsByTwoHundredDict = [];
                    foreach (int pTwoHundredVolume in pTwoHundredVolumeSettingDict[color])
                    {
                        wellsByTwoHundredDict[pTwoHundredVolume] = [];
                        foreach (Well well in wellsByColorDict[color])
                        {
                            if (well.PTwoHundredVolume == pTwoHundredVolume)
                            {
                                wellsByTwoHundredDict[pTwoHundredVolume].Add(well);
                            }
                        }
                        // Sort list by coördinates (leave volumes random)
                        wellsByTwoHundredDict[pTwoHundredVolume] =
                            [.. wellsByTwoHundredDict[pTwoHundredVolume].OrderBy(a => a.Y).ThenBy(a => a.X)];
                    }
                    // Write instructions to file
                    writer.WriteLine("Using the " + color.Name + " dye:");
                    // Spacing
                    writer.WriteLine("");

                    foreach (int volume in pTwoHundredVolumeSettingDict[color])
                    {
                        // Generate line of text for procedure
                        // Lines must be written as one string
                        string line = volume.ToString() + " uL: ";
                        line += wellsByTwoHundredDict[volume][0].ToString();
                        for (int i = 0; i < wellsByTwoHundredDict[volume].Count; i++)
                        {
                            line += ", " + wellsByTwoHundredDict[volume][i].ToString();
                        }
                        writer.WriteLine(line);
                    }
                    // Spacing
                    writer.WriteLine("");
                }
            }

            // Spacing
            writer.WriteLine("");
            writer.WriteLine("Total volume: " + totalVolume.ToString() + " uL");

            // Create popup showing filename and location
            InputForm.ShowFilenameAndDirectory(fileName);
        }
    }
}
