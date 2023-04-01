# Texture Set Maker
A lightweight utility to speed up the process of creating a resource pack for Minecraft RTX by allowing you to quickly generate texture_set.json files.
Requires .NET +6.0

# How To Use
After running TextureSetMaker.exe, you will need to input the full directory of textures/blocks folder within your resource pack, everything will be automatically generated & placed in your textures/blocks folder.  
Note that jsons for textures in blocks folder's subdirectories (e.g. textures in textures/blocks/deepslate directory) are placed in their own folder (folder structure is preserved).
Another folder called TextureSets is also generated which contains copies of textures with _mer/_normal/_heightmap at the end of them, this naming scheme matches the naming conventions in the jsons generated using this app.
