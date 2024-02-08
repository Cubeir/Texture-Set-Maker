using System.Media;
using static System.Console;
using Newtonsoft.Json;

public class TSMaker
{
    public static void Main(string[] args)
    {
    ReSet:
        ForegroundColor = ConsoleColor.Cyan;
        BackgroundColor = ConsoleColor.Black;
        WriteLine(" ------- Let's Make A PBR Resource Pack ------- \nPlease type or copy the full directory of the folder where your base textures are located\nThis is always the textures/blocks folder in your Minecraft resource pack\n");
        string input = ReadLine();
        if (Directory.Exists(input))
        {
            WriteLine("Pass.");
        }
        else
        {
            WriteLine("Directory does not exist.");
            goto ReSet;
        }

        string[] blockTextures_Subdirectories = Directory.GetDirectories(input);
        string[] blocksTextures_Folder = { input };
        string[] blockTextures_Folders = blocksTextures_Folder.Concat(blockTextures_Subdirectories).ToArray();

        for (int x = 0; x <= blockTextures_Folders.Length - 1; x++)
        {

            string[] imageDirectories = Directory.GetFiles(blockTextures_Folders[x]);

            List<string> imageDirectory_List = new List<string>();
            imageDirectory_List = imageDirectories.ToList();

            // Get rid of unsupported formats
            for (int i = imageDirectory_List.Count() - 1; i >= 0; i--)
            {
                if (!(imageDirectory_List[i].EndsWith(".png") || imageDirectory_List[i].EndsWith(".jpg") || imageDirectory_List[i].EndsWith(".tga") || imageDirectory_List[i].EndsWith(".jpeg")))
                {
                    imageDirectory_List.RemoveAt(i);
                }
            }


            // Define folders
            string folderPath_Heightmap = Path.Combine(blockTextures_Folders[x] + @"\Heightmap-Set\");
            Directory.CreateDirectory(folderPath_Heightmap);

            string folderPath_Normal = Path.Combine(blockTextures_Folders[x] + @"\Normal-Set\");
            Directory.CreateDirectory(folderPath_Normal);

            string pbrMap_FolderPath_MER = Path.Combine(blockTextures_Folders[x] + @"\MERs\");
            Directory.CreateDirectory(pbrMap_FolderPath_MER);


            // Factory
            foreach (string listed_imageDirectory in imageDirectory_List)

            {
                string imageName = Path.GetFileNameWithoutExtension(listed_imageDirectory);
                string lowImageNmae = imageName.ToLower();

                // Discard unneeded files
                if (lowImageNmae.EndsWith("mer") || lowImageNmae.EndsWith("heightmap") || lowImageNmae.EndsWith("texture_set")) continue;
                else if (lowImageNmae.EndsWith("normal") && !(lowImageNmae.StartsWith("sandstone") || lowImageNmae.StartsWith("red_sandstone") || lowImageNmae.StartsWith("rail"))) continue;


                string Normal_json_path = Path.Combine(folderPath_Normal, $"{imageName}.texture_set.json");
                string Heightmap_json_path = Path.Combine(folderPath_Heightmap, $"{imageName}.texture_set.json");

                string MER = $"{imageName}_mer";
                string normal = $"{imageName}_normal";
                string heightmap = $"{imageName}_heightmap";

                var textureSetNormal = new
                {
                    format_version = "1.16.100",
                    minecraft = new
                    {
                        texture_set = new
                        {
                            color = imageName,
                            metalness_emissive_roughness = MER,
                            normal = normal
                        }
                    }
                };

                var textureSetHeightmap = new
                {
                    format_version = "1.16.100",
                    minecraft = new
                    {
                        texture_set = new
                        {
                            color = imageName,
                            metalness_emissive_roughness = MER,
                            heightmap = heightmap
                        }
                    }
                };

                string Normaljson_File_Content = JsonConvert.SerializeObject(textureSetNormal, Formatting.Indented);
                string Heightmapjson_File_Content = JsonConvert.SerializeObject(textureSetHeightmap, Formatting.Indented);

                File.WriteAllText(Normal_json_path, Normaljson_File_Content);
                File.WriteAllText(Heightmap_json_path, Heightmapjson_File_Content);

                // PBR Texture files copied to their directories
                string image_Extension = Path.GetExtension(listed_imageDirectory);

                string MER_Destination_File = Path.Combine(pbrMap_FolderPath_MER + imageName + "_mer" + image_Extension);
                File.Copy(listed_imageDirectory, MER_Destination_File, false);

                string Normal_Destination_File = Path.Combine(folderPath_Normal + imageName + "_normal" + image_Extension);
                File.Copy(listed_imageDirectory, Normal_Destination_File, false);

                string Heightmap_Destination_File = Path.Combine(folderPath_Heightmap + imageName + "_heightmap" + image_Extension);
                File.Copy(listed_imageDirectory, Heightmap_Destination_File, false);

            }
        }
        WriteLine($"\nSUCCESSFUL!");
        SoundPlayer finishSound = new SoundPlayer("finish.wav");
        finishSound.Load();
        finishSound.Play();
        ReadLine();
    }
}