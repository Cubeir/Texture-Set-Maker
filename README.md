# Texture Set Maker
A lightweight utility to speed up the process of creating a resource pack for Minecraft RTX by allowing you to quickly generate texture_set.json files.
Requires .NET +6.0

# How To Use
After opening TextureSetMaker.exe, you face two options (0 and 1)  
0 = Normal maps, generates jsons that should be used when you use normal maps for your resource pack. e.g.,  
```{"format_version":"1.16.100","minecraft:texture_set":{"color":"build_deny","metalness_emissive_roughness":"build_deny_mer","normal":"build_deny_normal"}}```

1 = Heightmaps, generates jsons that should be used when you use heightmaps maps for your resource pack. e.g.,  
```{"format_version":"1.16.100","minecraft:texture_set":{"color":"build_deny","metalness_emissive_roughness":"build_deny_mer","heightmap":"build_deny_heightmap"}}```

As seen in the example above, only the heightmap/normal map parameters are switched depending on your input.
It is vital to choose the correct parameter, otherwise you will end up with missing textures in-game.

Although you can name your textures in any way you like, as of now this app only generates your file names with _mer and _heightmap/_normal map added to the end as this is a known & standard of naming your textures. (An option to change customize this could be added in the future)

After choosing between heightmap/normal map, you will be asked to input the full directory of your textures.
Simply open the folder where your textures are located, click the file explorer address bar and copy the full directory.
Paste it inside of the console app and press enter. A JSON folder will be created at your textures folder which will contain all of the jsons, you can then copy paste them into your resource pack folder. (This is to give you a chance you to review and check jsons before proceeding to release them into another folder)

Note that as of now subdirectories are untouched, that means you will need to repeat the process a if you have multiple folders with textures located in them.
This will be addressed in the future.
