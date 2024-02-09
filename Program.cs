using System.Media;
using static System.Console;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.InteropServices;

public class TSMaker
{
    public static void Main(string[] args)
    {
    Restart:
        Clear();
        ForegroundColor = ConsoleColor.Cyan;
        BackgroundColor = ConsoleColor.Black;
        WriteLine(" ------- Let's Make A PBR Resource Pack ------- \nType or copy the full directory of the folder where your base textures are located\nThis is usually the textures/blocks folder in your Minecraft resource pack\n");
        string input = ReadLine();
        input.Trim('\"');

        if (Directory.Exists(input))
        {
            WriteLine("Pass.");

        }
        else
        {
            WriteLine("Directory does not exist.");
            Thread.Sleep(1000);
            Clear();
            goto Restart;
        }

        string[] blocksTextures_Subdirectories = Directory.GetDirectories(input, "*", SearchOption.AllDirectories);
        string[] blocksTextures_Folder = { input };
        string[] allBlocksTextures_Folders = blocksTextures_Folder.Concat(blocksTextures_Subdirectories).ToArray();

        for (int x = 0; x <= allBlocksTextures_Folders.Length - 1; x++)
        {

            string[] imageDirectories = Directory.GetFiles(allBlocksTextures_Folders[x]);

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

            // Folders
            string pbrMap_FolderPath_MER = Path.Combine(allBlocksTextures_Folders[x] + @"\MER-Extension\");

            string folderPath_Normal = Path.Combine(allBlocksTextures_Folders[x] + @"\Normal-Set\");

            string folderPath_Heightmap = Path.Combine(allBlocksTextures_Folders[x] + @"\Heightmap-Set\");

            string[] fileCount = Directory.GetFiles(allBlocksTextures_Folders[x]);
            if (fileCount.Length > 0)
            {
                Directory.CreateDirectory(folderPath_Heightmap);
                Directory.CreateDirectory(folderPath_Normal);
                Directory.CreateDirectory(pbrMap_FolderPath_MER);
            }
            else { ; }



            foreach (string listed_imageDirectory in imageDirectory_List)
            {
                string imageName = Path.GetFileNameWithoutExtension(listed_imageDirectory);
                string lowImageNmae = imageName.ToLower();

                // Discard these files (In case input folder already contains PBR files generated with this app).
                if (lowImageNmae.EndsWith("mer") || lowImageNmae.EndsWith("heightmap") || lowImageNmae.EndsWith("texture_set")) continue;
                else if (lowImageNmae.EndsWith("normal") && !(lowImageNmae.StartsWith("sandstone") || lowImageNmae.StartsWith("red_sandstone") || lowImageNmae.StartsWith("rail"))) continue;


                string NormalJsonPath = Path.Combine(folderPath_Normal, $"{imageName}.texture_set.json");
                string HeightmapJsonPath = Path.Combine(folderPath_Heightmap, $"{imageName}.texture_set.json");

                string MER = $"{imageName}_mer";
                string normal = $"{imageName}_normal";
                string heightmap = $"{imageName}_heightmap";

                JObject normalTextureSet = new JObject(
                    new JProperty("format_version", "1.16.100"),
                    new JProperty("minecraft:texture_set",
                        new JObject(
                            new JProperty("color", imageName),
                            new JProperty("metalness_emissive_roughness", MER),
                            new JProperty("normal", normal)
                        )
                    )
                );
                JObject heightmapTextureSet = new JObject(
                    new JProperty("format_version", "1.16.100"),
                    new JProperty("minecraft:texture_set",
                        new JObject(
                            new JProperty("color", imageName),
                            new JProperty("metalness_emissive_roughness", MER),
                            new JProperty("heightmap", heightmap)
                        )
                    )
                );

                string NormaljsonContent = normalTextureSet.ToString();
                string HeightmapjsonContent = heightmapTextureSet.ToString();

                File.WriteAllText(NormalJsonPath, NormaljsonContent);
                File.WriteAllText(HeightmapJsonPath, HeightmapjsonContent);

                WriteLine(NormalJsonPath);
                WriteLine(HeightmapJsonPath);

                // PBR Texture files copied to their directories
                string image_Extension = Path.GetExtension(listed_imageDirectory);
                try
                {
                    string MER_Destination_File = Path.Combine(pbrMap_FolderPath_MER + imageName + "_mer" + image_Extension);
                    File.Copy(listed_imageDirectory, MER_Destination_File, false);

                    string Normal_Destination_File = Path.Combine(folderPath_Normal + imageName + "_normal" + image_Extension);
                    File.Copy(listed_imageDirectory, Normal_Destination_File, false);

                    string Heightmap_Destination_File = Path.Combine(folderPath_Heightmap + imageName + "_heightmap" + image_Extension);
                    File.Copy(listed_imageDirectory, Heightmap_Destination_File, false);
                }
                catch
                {
                    WriteLine("One or more of PBR files already exists.");
                }
            }
        }

        WriteLine($"\nSuccessful.");
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            SoundPlayer finishSound = new SoundPlayer("finish.wav");
            finishSound.Load();
            finishSound.Play();
        }
        Thread.Sleep(2000);
        goto Restart;
    }
}