using System;
using Newtonsoft.Json;
using System.IO;
using System.Media;
using static System.Console;
using Newtonsoft.Json.Linq;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using ImageProcessor.Imaging.Formats;
using ImageProcessor;
using static System.Net.WebRequestMethods;
using System.Drawing;
using File = System.IO.File;
using System.Drawing.Imaging;

internal class TSMaker
{
    private static void Main(string[] args)
    {
        ForegroundColor = ConsoleColor.Yellow;
        BackgroundColor = ConsoleColor.Black;
        WriteLine(" ------- Let's Make A PBR Resource Pack ------- \nPlease type or copy the full directory of the folder where your base textures are located\nThis is usually the textures/blocks folder in your Minecraft resource pack\n");


        // Store all file directories
        string _ = ReadLine();
        string[] image_Directories_Full = Directory.GetFiles(_, "*", SearchOption.AllDirectories);

        List<string> image_Directories_Full_List = new List<string>();
        image_Directories_Full_List = image_Directories_Full.ToList();


        // Define the folders
        string folderPath_Heightmap = _ + @"\Heightmap Jsons\";
        if (folderPath_Heightmap.EndsWith(@"\\"))
            folderPath_Heightmap.Replace(@"\\", @"\");
        Directory.CreateDirectory(folderPath_Heightmap);

        string folderPath_Normal = _ + @"\Normal Jsons\";
        if (folderPath_Normal.EndsWith(@"\\"))
            folderPath_Normal.Replace(@"\\", @"\");
        Directory.CreateDirectory(folderPath_Normal);

        string pbrMap_FolderPath_MER = _ + @"\TextureSets\MER\";
        if (pbrMap_FolderPath_MER.EndsWith(@"\\"))
            pbrMap_FolderPath_MER.Replace(@"\\", @"\");
        Directory.CreateDirectory(pbrMap_FolderPath_MER);

        string pbrMap_FolderPath_Heightmap = _ + @"\TextureSets\Heightmap\";
        if (pbrMap_FolderPath_Heightmap.EndsWith(@"\\"))
            pbrMap_FolderPath_Heightmap.Replace(@"\\", @"\");
        Directory.CreateDirectory(pbrMap_FolderPath_Heightmap);

        string pbrMap_FolderPath_Normal = _ + @"\TextureSets\Normal\";
        if (pbrMap_FolderPath_Normal.EndsWith(@"\\"))
            pbrMap_FolderPath_Normal.Replace(@"\\", @"\");
        Directory.CreateDirectory(pbrMap_FolderPath_Normal);


        // Get rid of unsupported formats
        for (int i = image_Directories_Full_List.Count() - 1; i >= 0; i--)  
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


            string Normal_json_Fullpath = folderPath_Normal + image_Name_Without_Extension + ".texture_set.json";
            string Heightmap_json_Fullpath = folderPath_Heightmap + image_Name_Without_Extension + ".texture_set.json";
            WriteLine(Normal_json_Fullpath + "\n" + Heightmap_json_Fullpath);
            //------------------------------------------- Base information which we use to create the json's content
            string Normaljson_File_Content = "{\"format_version\":\"1.16.100\",\"minecraft:texture_set\":{\"color\":\"X\",\"metalness_emissive_roughness\":\"Y\",\"W\":\"Z\"}}";
            string Heightmapjson_File_Content = "{\"format_version\":\"1.16.100\",\"minecraft:texture_set\":{\"color\":\"X\",\"metalness_emissive_roughness\":\"Y\",\"W\":\"Z\"}}";

            string MER = image_Name_Without_Extension + "_mer";
            string normal = image_Name_Without_Extension + "_normal";
            string heightmap = image_Name_Without_Extension + "_heightmap";

            // string[] NH = { "normal", "heightmap", normal, heightmap };
            //-------------------------------------------

            Normaljson_File_Content = Normaljson_File_Content.Replace("X", image_Name_Without_Extension);
            Normaljson_File_Content = Normaljson_File_Content.Replace("Y", MER);
            Normaljson_File_Content = Normaljson_File_Content.Replace("W", "normal");
            Normaljson_File_Content = Normaljson_File_Content.Replace("Z", normal);

            Heightmapjson_File_Content = Heightmapjson_File_Content.Replace("X", image_Name_Without_Extension);
            Heightmapjson_File_Content = Heightmapjson_File_Content.Replace("Y", MER);
            Heightmapjson_File_Content = Heightmapjson_File_Content.Replace("W", "heightmap");
            Heightmapjson_File_Content = Heightmapjson_File_Content.Replace("Z", heightmap);



            File.WriteAllText(Normal_json_Fullpath, Normaljson_File_Content);
            File.WriteAllText(Heightmap_json_Fullpath, Heightmapjson_File_Content);
            // JSON Creation Ends here. variables will be reused.





            // PBR Texture files copied to their directories (later processed)
            string image_Extension = Path.GetExtension(listed_image_Directories_Full);

            string MER_Destination_File = pbrMap_FolderPath_MER + image_Name_Without_Extension + "_mer" + image_Extension;
            File.Copy(listed_image_Directories_Full, MER_Destination_File, false);

            string Normal_Destination_File = pbrMap_FolderPath_Normal + image_Name_Without_Extension + "_normal" + image_Extension;
            File.Copy(listed_image_Directories_Full, Normal_Destination_File, false);

            string Heightmap_Destination_File = pbrMap_FolderPath_Heightmap + image_Name_Without_Extension + "_heightmap" + image_Extension;
            File.Copy(listed_image_Directories_Full, Heightmap_Destination_File, false);


        } //end of foreach




        // Finish
        WriteLine($"\nSUCCESSFUL!");
            SoundPlayer finishSound = new SoundPlayer("finish.wav");
            finishSound.Load();
            finishSound.Play();
            ReadLine();
    }
}

// ideas/plans:
// Re-Run the app whenever user types anything wrong, could be a few cases
// Move it all to an actual Ui
// MS-TD

// v3 so far:
// Get files and create copies of the them with the same name + _mer/normal/heightmap in the same directory (Optional Feature) [will add  option later]
// No more "Options" user will just input the pack directory, heightmap and normal jsons are both generated in seperate folders
//// And heightmaps and normals are both generated too, no more asking for 0 and 1, do it all at once. everything.
// Reading subdirectories and placing the jsons in the directories with the same folder name inside of JSONS folder.
//// e.g get files in /blocks/candles, and places candles jsons in JSONS/candles folders. (half done)