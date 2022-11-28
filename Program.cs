using System;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;
using static System.Net.Mime.MediaTypeNames;

internal class TSMaker
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Type 0 or 1\n0 for Heightmap texture sets\n1 for Normal map texture sets");
        string heightmap_NormalMap_Switch = Console.ReadLine();
        int answer = int.Parse(heightmap_NormalMap_Switch);
        Console.WriteLine("Selected:");


        // Heightmaps ----------------------------------------------- Heightmaps
        if (answer == 0)
        {
            Console.WriteLine("Heightmaps - Please type or copy the full directory of where your images are located\nIt is usually the /textures/blocks folder in your resource pack\n");
            string _ = Console.ReadLine();

            // Create a folder to place the jsons in it (and to direct jsons later on)
            string folderPath = _ + @"\JSONS\";
            if (folderPath.EndsWith(@"\\")) folderPath.Replace(@"\\", @"\");
            Directory.CreateDirectory(folderPath);


            string[] image_Directories_Full = Directory.GetFiles(_);
            foreach (string listed_image_Directories_Full in image_Directories_Full)

            {
                string image_Name_Without_Extension = Path.GetFileNameWithoutExtension(listed_image_Directories_Full);

                string json_Fullpath = folderPath + image_Name_Without_Extension + ".texture_set.json";                           // Defining the full path for the jsons we are going to make
                Console.WriteLine(json_Fullpath);


                string MER = image_Name_Without_Extension + "_mer";
                string heightmap = image_Name_Without_Extension + "_heightmap";


                string json_File_Content = " {\"format_version\":\"1.16.100\",\"minecraft:texture_set\":{\"color\":\"X\",\"metalness_emissive_roughness\":\"Y\",\"heightmap\":\"Z\"}} ";
                json_File_Content = json_File_Content.Replace("X", image_Name_Without_Extension);
                json_File_Content = json_File_Content.Replace("Y", MER);
                json_File_Content = json_File_Content.Replace("Z", heightmap);
                File.WriteAllText(json_Fullpath, json_File_Content);

            }

            Console.WriteLine("DONE! Find JSONS folder in your textures directory");
        }



        // Normal Maps ----------------------------------------------- Normal Maps
        else if (answer == 1)
        {
            Console.WriteLine("Normal maps - Please type or copy the full directory of where your images are located\nIt is usually the /textures/blocks folder in your resource pack\n");
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


                string MER = image_Name_Without_Extension + "_mer";
                string normal = image_Name_Without_Extension + "_normal";


                string json_File_Content = " {\"format_version\":\"1.16.100\",\"minecraft:texture_set\":{\"color\":\"X\",\"metalness_emissive_roughness\":\"Y\",\"normal\":\"Z\"}} ";
                json_File_Content = json_File_Content.Replace("X", image_Name_Without_Extension);
                json_File_Content = json_File_Content.Replace("Y", MER);
                json_File_Content = json_File_Content.Replace("Z", normal);
                File.WriteAllText(json_Fullpath, json_File_Content);

            }

            Console.WriteLine("DONE! Find JSONS folder in your textures directory");
        }

        else
        {
            Console.WriteLine("Type 0 or 1 Only, 0 for heightmaps, 1 for normals\nRe-Run the program");
        }

        Console.ReadLine();
    }
}
// They will be done in the numerical order.
// Planned 1: This app can use a lot less lines, I should also work on strings that are displayed to users/have clearer instructions.
// Planned 2: Exclude textures if the file name ends with _mer, _normal, _heightmap, this could be a bit tricky because of some special cases/exceptions.
// Planned 3: Re-Run the app whenever user types anything wrong, could be a few cases
// Planned 4: This app should do anything with the files aren't PNG, JPG, JPEG, TGA, etc... (all other supported formats)
// Planned 5: Update readme.md with a more accurate description of the app, maybe a little "How To" too in case anyone has any problems.
/* Planned for later: Reading subdirectories and placing the jsons in the directories with the same folder name inside of JSONS folder.
    e.g App reads files in /blocks/candles, and places candles jsons in JSONS/candles folder */
