using System;
using Newtonsoft.Json;
using System.IO;
using System.Media;
using static System.Console;
using Newtonsoft.Json.Linq;
using System.Runtime.CompilerServices;
using System.Diagnostics;

internal class TSMaker
{
    private static void Main(string[] args)
    {
        ForegroundColor = ConsoleColor.Yellow;
        BackgroundColor = ConsoleColor.Black;

        // User controls heightmap/normal json creation
        Write("Input 0 or 1\n0 = Normals\n1 = Heightmaps\nType here: ");

        string heightmap_NormalMap_Switch = ReadLine();
        if (Int32.TryParse(heightmap_NormalMap_Switch, out int answer))
        {
            if (answer == 0)
                WriteLine(" ------- Let's make some Normal Map jsons ------- \nPlease type or copy the full directory of the folder where your images are located\nThis is usually the textures/blocks folder in your Minecraft resource pack\nIsolate the files that you want to generate jsons for\n");
            else if (answer == 1)
                WriteLine(" ------- Let's make some Heightmap jsons ------- \nPlease type or copy the full directory of the folder where your images are located\nThis is usually the textures/blocks folder in your Minecraft resource pack\nIsolate the files that you want to generate jsons for\n");
            else
            {
                ForegroundColor = ConsoleColor.Red;
                WriteLine("Input 0 or 1 only, now close the application");
            }
        }
        else
        {
            ForegroundColor = ConsoleColor.DarkRed;
            WriteLine("Input a number (0 or 1 only) now close the application");
        }


        // Store all full file directories
        string _ = ReadLine();
        string[] image_Directories_Full = Directory.GetFiles(_);

        List<string> image_Directories_Full_List = new List<string>();
        image_Directories_Full_List = image_Directories_Full.ToList();


        // Define the folders
        string folderPath = _ + @"\Jsons\";
        if (folderPath.EndsWith(@"\\"))
            folderPath.Replace(@"\\", @"\");
        Directory.CreateDirectory(folderPath);

        string pbrMap_FolderPath_MER = _ + @"\TextureSets\MER\";
        if (pbrMap_FolderPath_MER.EndsWith(@"\\"))
            pbrMap_FolderPath_MER.Replace(@"\\", @"\");
        Directory.CreateDirectory(pbrMap_FolderPath_MER);

        string pbrMap_FolderPath_NH = _ + @"\TextureSets\Normal.Heightmap\";
        if (pbrMap_FolderPath_NH.EndsWith(@"\\"))
            pbrMap_FolderPath_NH.Replace(@"\\", @"\");
        Directory.CreateDirectory(pbrMap_FolderPath_NH);





        // Get rid of unsupported formats
        for (int i = image_Directories_Full_List.Count() - 1; i >= 0; i--)  // int i is -1 because indexes are zero-based, that means if there are 40 items in our list, we'll need to count from 39 to 0 (which is 40 items in total)
        {

            if (!(image_Directories_Full_List[i].EndsWith(".png") || image_Directories_Full_List[i].EndsWith(".jpg") || image_Directories_Full_List[i].EndsWith(".tga") || image_Directories_Full_List[i].EndsWith(".jpeg")))
            {
                image_Directories_Full_List.RemoveAt(i);
            }

        }



        // Factory
        foreach (string listed_image_Directories_Full in image_Directories_Full_List)

        {

            string image_Name_Without_Extension = Path.GetFileNameWithoutExtension(listed_image_Directories_Full);

            // Discard unneeded files
            if (image_Name_Without_Extension.EndsWith("mer")) continue;
            else if (image_Name_Without_Extension.EndsWith("heightmap")) continue;
            else if (image_Name_Without_Extension.EndsWith("texture_set")) continue;
            else if (image_Name_Without_Extension.EndsWith("normal") && !(image_Name_Without_Extension.StartsWith("sandstone") || image_Name_Without_Extension.StartsWith("red_sandstone") || image_Name_Without_Extension.StartsWith("rail"))) continue;

            string json_Fullpath = folderPath + image_Name_Without_Extension + ".texture_set.json";
            WriteLine(json_Fullpath);

            //------------------------------------------- Base information which we use to create the json's content
            string json_File_Content = "{\"format_version\":\"1.16.100\",\"minecraft:texture_set\":{\"color\":\"X\",\"metalness_emissive_roughness\":\"Y\",\"W\":\"Z\"}}";

            string[] NH = { "normal", "heightmap" };
            string MER = image_Name_Without_Extension + "_mer";
            string normal = image_Name_Without_Extension + "_normal";
            string heightmap = image_Name_Without_Extension + "_heightmap";
            //------------------------------------ Customize this if you have named your textures in a different way

            json_File_Content = json_File_Content.Replace("X", image_Name_Without_Extension);
            json_File_Content = json_File_Content.Replace("Y", MER);

            if (answer == 0)
            {
                json_File_Content = json_File_Content.Replace("W", NH[0]);
                json_File_Content = json_File_Content.Replace("Z", normal);
            }
            else if (answer == 1)
            {
                json_File_Content = json_File_Content.Replace("W", NH[1]);
                json_File_Content = json_File_Content.Replace("Z", heightmap);
            }
            else Environment.Exit(1);


            File.WriteAllText(json_Fullpath, json_File_Content);
            // JSON Creation Ends here, variables will be reused.





            // PBR Texture files (there will be more operations later to convert this into actual resource pack, manifest generation and many things will come even later)
            string image_Extension = Path.GetExtension(listed_image_Directories_Full);

            string MER_Destination_File = pbrMap_FolderPath_MER + image_Name_Without_Extension + "_mer" + image_Extension;
            File.Copy(listed_image_Directories_Full, MER_Destination_File, true);


            string NormalHeightmap = "";
            if (answer == 0) { NormalHeightmap = "normal"; }
            else if (answer == 1) { NormalHeightmap = "heightmap"; }

            string NormalHeightmap_Destination_File = pbrMap_FolderPath_NH + image_Name_Without_Extension + "_" + NormalHeightmap + image_Extension;
            File.Copy(listed_image_Directories_Full, NormalHeightmap_Destination_File, true);



        } // End of Foreach






        // Finish
        if (answer == 0 || answer == 1)
        {
            WriteLine($"\nSUCCESSFUL! Find JSONS folder at:\n{folderPath}");
            SoundPlayer finishSound = new SoundPlayer("finish.wav");
            finishSound.Load();
            finishSound.Play();
        }
        ReadLine();
    }
}

// ideas/plans:
// Re-Run the app whenever user types anything wrong, could be a few cases
// Reading subdirectories and placing the jsons in the directories with the same folder name inside of JSONS folder.
// e.g get files in /blocks/candles, and places candles jsons in JSONS/candles folders.
// Move it all to an actual Ui


// v3 so far:
// Get files and create copies of the them with the same name + _mer/normal/heightmap in the same directory (Optional Feature) [will add  optionlater]