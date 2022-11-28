using System;
/* using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Xml.Schema;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Collections.Concurrent; */
using System.Text;
using Newtonsoft.Json;
using System.IO;

Console.WriteLine("Welcome to Texture_Set Maker, please type or copy the full directory of where your images are located\nit is usually the /textures/blocks folder\n");
string _ = Console.ReadLine();

// create a folder to place the jsons in it in the future (and to direct the jsons to it)
string folderPath = _ + @"\JSONS\";
if (folderPath.EndsWith(@"\\")) folderPath.Replace(@"\\", @"\");
Directory.CreateDirectory(folderPath);


string[] image_Directories_Full = Directory.GetFiles(_);
foreach (string listed_image_Directories_Full in image_Directories_Full)                                // iterating through the array of our directory list for all images and files

{
    string image_Name_Without_Extension = Path.GetFileNameWithoutExtension(listed_image_Directories_Full);   //turning the full path of each file into just file name

    string json_Fullpath = folderPath + image_Name_Without_Extension + ".texture_set.json";                           // this line defines the path for jsons we are going to make
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

// you could make a heightmap/normal switchable code by having a user input, creating an if statement and executing a foreach based on user input
// could have exceptions, if the file name already has _mer _normal or _heightmap, fully ignore adding _mer,norm,heightmap to it. it'll require some IF ELSE statements, probably
