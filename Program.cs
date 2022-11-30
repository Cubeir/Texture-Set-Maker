using System;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;
using static System.Net.Mime.MediaTypeNames;

internal class TSMaker
{
    private static void Main(string[] args)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.BackgroundColor = ConsoleColor.Black;


        Console.Write("Input 0 or 1\n0 = Normals\n1 = Heightmaps\nType here: ");

        string heightmap_NormalMap_Switch = Console.ReadLine();
        if (Int32.TryParse(heightmap_NormalMap_Switch, out int answer))
        {
            if (answer == 0)
                Console.WriteLine(" ------- Normal jsons will be made ------- \nPlease type or copy the full directory of the folder where your images are located\nThis is usually the textures/blocks folder in your Minecraft resource pack\nIsolate the files that you want to generate jsons for\n");
            else if (answer == 1)
                Console.WriteLine(" ------- Heightmap jsons will be made ------- \nPlease type or copy the full directory of the folder where your images are located\nThis is usually the textures/blocks folder in your Minecraft resource pack\nIsolate the files that you want to generate jsons for\n");
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Input 0 or 1 only, now close the application");
            }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Input a number (0 or 1 only) now close the application");
        }
        



        string _ = Console.ReadLine();

        string folderPath = _ + @"\JSONS\";

        if (folderPath.EndsWith(@"\\")) folderPath.Replace(@"\\", @"\");

        Directory.CreateDirectory(folderPath);

        string[] image_Directories_Full = Directory.GetFiles(_);

        foreach (string listed_image_Directories_Full in image_Directories_Full)

        {
            string image_Name_Without_Extension = Path.GetFileNameWithoutExtension(listed_image_Directories_Full);

            string json_Fullpath = folderPath + image_Name_Without_Extension + ".texture_set.json";
            Console.WriteLine(json_Fullpath);

 //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
            string json_File_Content = " {\"format_version\":\"1.16.100\",\"minecraft:texture_set\":{\"color\":\"X\",\"metalness_emissive_roughness\":\"Y\",\"W\":\"Z\"}} "; //
                                                                                //------------------------------------------------------------------------------------------//
            string[] HN = { "normal", "heightmap" };                           //
            string MER = image_Name_Without_Extension + "_mer";               //
            string normal = image_Name_Without_Extension + "_normal";        //
            string heightmap = image_Name_Without_Extension + "_heightmap"; //
//-------------------------------------------------------------------------//

            json_File_Content = json_File_Content.Replace("X", image_Name_Without_Extension);
            json_File_Content = json_File_Content.Replace("Y", MER);

            if (answer == 0)
            {
                json_File_Content = json_File_Content.Replace("W", HN[0]);
                json_File_Content = json_File_Content.Replace("Z", normal);
            }
            else if (answer == 1)
            {
                json_File_Content = json_File_Content.Replace("W", HN[1]);
                json_File_Content = json_File_Content.Replace("Z", heightmap);
            }
            else Environment.Exit(1);


            File.WriteAllText(json_Fullpath, json_File_Content);
        }

        Console.WriteLine($"\nSUCCESSFUL! Find JSONS folder at:\n{folderPath}"); Console.ReadLine();
    }
}

// Planned:
// Exclude textures if the file name ends with _mer, _normal, _heightmap, this could be a bit tricky because of some special cases/exceptions.
// Re-Run the app whenever user types anything wrong, could be a few cases
// This app should not do anything with the files that aren't PNG, JPG, JPEG, TGA, etc... (or all other supported formats)
// Update readme.md with a more accurate description of the app, maybe a little "How To" too in case anyone has any problems.
// Reading subdirectories and placing the jsons in the directories with the same folder name inside of JSONS folder.
// e.g get files in /blocks/candles, and places candles jsons in JSONS/candles folders.
// Get files and create copies of the them with the same name + _mer/normal/heightmap in the same directory (Optional Feature, answer 0 0 == normal map, without file copy, 0 1 == normal map with file copy)
