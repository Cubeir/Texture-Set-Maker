using System;
using Newtonsoft.Json;
using System.IO;

Console.WriteLine("WeElcome to Texture_Set Maker, please type or copy the full directory of where your images are located\nit is usually the /textures/blocks folder\n");
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
    string normal = image_Name_Without_Extension + "_normal";
    string heightmap = image_Name_Without_Extension + "_heightmap";


    string json_File_Content = " {\"format_version\":\"1.16.100\",\"minecraft:texture_set\":{\"color\":\"X\",\"metalness_emissive_roughness\":\"Y\",\"heightmap\":\"Z\"}} ";
    json_File_Content = json_File_Content.Replace("X", image_Name_Without_Extension);
    json_File_Content = json_File_Content.Replace("Y", MER);
    json_File_Content = json_File_Content.Replace("Z", heightmap);
    File.WriteAllText(json_Fullpath, json_File_Content);

}

Console.WriteLine("DONE! Find JSONS folder in your textures directory");

// Planned: the ability to switch to normal maps and create normal map Jsons instead
// Planned: Excluding textures if the file name ends with _mer, _normal, _heightmap, this could be tricky as some of default resources end with _normal in their file name.