Little Thief Asset (Ver.1.1)

===================================
File Structure
===================================
[LittleThief]

    -> [Demo1]
        -> [Enemy_Data] : Enemy(slime) models and animations, materials and textures.
        -> [Environment_Data] : Floor and door&key models, materials and textures.
        -> [Prefabs] : Thief (player) prefabs for Demo1 scene.
        -> [Scripts] : Sample scripts for Demo1 scene.
        -> [TreasureBox&Items_Data] : Treasure box and item models, materials and textures.

    -> [Demo2]
        -> [Base_Data] : Base model, material and texture.
        -> [Scripts] : Sample scripts for Demo2 scene.
        -> Player : Thief (player) prefab for Demo2 scene.

    -> [Thief_Data] : Little Thief models and animations, materials and textures.
        -> [Prefabs] : 2 types of thief prefabs with weapon holders and sheaths.

    -> [Resources] : Prefabs for Instantiate function (for Demo1 and Demo2).

    -> [Weapons&Shields_Data] : Knife and sheath models, materials and textures.

    -> Demo1 scene : Sample scene of player controll on the field with enemies and treasure boxes, etc.

    -> Demo2 scene : Sample scene of 14 animations.


===================================
How to change weapons
===================================
Find WeaponHolder in Hierarchy.
  Player -> Thief -> Root -> spine  -> chest  -> R_arm ..... -> R_item -> WeaponHolder_R
  Player -> Thief -> Root -> spine  -> chest  -> L_arm ..... -> L_item -> WeaponHolder_L

The Holders has weapon prefabs.
You can remove them and attach your own items in Holders.
(*If you make 3D model, set the point of item handle to (0,0,0).)

===================================
How to change sheaths
===================================
Find WeaponHolder in Hierarchy.
  Player -> Thief -> Root -> spine  -> chest  -> Sheath_R
  Player -> Thief -> Root -> spine  -> chest  -> Sheath_L

The folder has KnifeSheath prefabs.
You can remove them and attach your own sheaths.
(*Change the position and rotation of your sheaths to be the same as the default sheaths.)

Publisher web page
http://miniascapeworks.blogspot.com

