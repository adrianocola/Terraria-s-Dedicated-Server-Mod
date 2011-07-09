using System;
using System.Collections.Generic;
using Terraria_Server.Misc;
using Terraria_Server.Collections;
using Terraria_Server.Definitions;

namespace Terraria_Server
{
    public class Item : IRegisterableEntity
    {
        public const int POTION_DELAY = 720;

        public bool Accessory;
        public bool Active { get; set; }
        public int Alpha;
        public ProjectileType Ammo;
        public bool AutoReuse;
        public int Axe;
        public bool BeingGrabbed;
        public int BodySlot = -1;
        public int BuffTime;
        public int BuffType;
        public bool Buy;
        public bool Channel;
        public Color Color;
        public bool Consumable;
        public int CreateTile;
        public int CreateWall;
        public int Damage;
        public int Defense;
        public int Hammer;
        public int HeadSlot;
        public int HealLife;
        public int HealMana;
        public int Height;
        public int HoldStyle;
        public int KeepTime;
        public float KnockBack;
        public bool LavaWet;
        public int LegSlot;
        public int LifeRegen;
        public int Mana;
        public int ManaRegen;
        public bool Material;
        public int MaxStack;
        public bool Melee;
        public String Name { get; set; }
        public int NoGrabDelay;
        public bool NoMelee;
        public bool NoUseGraphic;
        public bool NoWet;
        public int Owner = 255;
        public int OwnIgnore = -1;
        public int OwnTime;
        public int Pick;
        public int PlaceStyle;
        public Vector2 Position;
        public bool Potion;
        public int Rare;
        public int Release;
        public float Scale;
        public ProjectileType Shoot;
        public float ShootSpeed;
        public int SpawnTime;
        public int Stack;
        public int TileBoost;
        public String ToolTip;
        public int Type { get; set; }
        public ProjectileType UseAmmo;
        public int UseAnimation;
        public int UseSound;
        public int UseStyle;
        public int UseTime;
        public bool UseTurn;
        public int Value;
        public bool Vanity;
        public Vector2 Velocity;
        public bool Wet;
        public byte WetCount;
        public int Width;
        public bool WornArmor;

        public Item()
        {
            BodySlot = -1;
            Color = default(Color);
            CreateTile = -1;
            CreateWall = -1;
            Damage = -1;
            LegSlot = -1;
            HeadSlot = -1;
            MaxStack = 1;
            Owner = 255;
            Scale = 1f;
            ToolTip = null;
            UseAnimation = 100;
            UseTime = 100;
        }

        public static String VersionName(String oldName, int release)
        {
            if (release <= 4)
            {
                switch(oldName)
                {
                    case "Cobalt Helmet":
                        return "Jungle Hat";
                    case "Cobalt Breastplate":
                        return "Jungle Shirt";
                    case "Cobalt Greaves":
                        return "Jungle Pants";
                }
            }
            return oldName;
        }

        public void UpdateItem(int i)
        {
            if (this.Active)
            {
                float num = 0.1f;
                float num2 = 7f;
                if (this.Wet)
                {
                    num2 = 5f;
                    num = 0.08f;
                }
                Vector2 value = this.Velocity * 0.5f;
                if (this.OwnTime > 0)
                {
                    this.OwnTime--;
                }
                else
                {
                    this.OwnIgnore = -1;
                }
                if (this.KeepTime > 0)
                {
                    this.KeepTime--;
                }
                if (!this.BeingGrabbed)
                {
                    this.Velocity.Y = this.Velocity.Y + num;
                    if (this.Velocity.Y > num2)
                    {
                        this.Velocity.Y = num2;
                    }
                    this.Velocity.X = this.Velocity.X * 0.95f;
                    if ((double)this.Velocity.X < 0.1 && (double)this.Velocity.X > -0.1)
                    {
                        this.Velocity.X = 0f;
                    }
                    bool flag = Collision.LavaCollision(this.Position, this.Width, this.Height);
                    if (flag)
                    {
                        this.LavaWet = true;
                    }
                    bool flag2 = Collision.WetCollision(this.Position, this.Width, this.Height);
                    if (flag2)
                    {
                        if (!this.Wet)
                        {
                            if (this.WetCount == 0)
                            {
                                this.WetCount = 20;
                                if (!flag)
                                {
                                    for (int j = 0; j < 10; j++)
                                    {
                                        int num3 = Dust.NewDust(new Vector2(this.Position.X - 6f, this.Position.Y + (float)(this.Height / 2) - 8f), this.Width + 12, 24, 33, 0f, 0f, 0, default(Color), 1f);
                                        Dust expr_1EC_cp_0 = Main.dust[num3];
                                        expr_1EC_cp_0.velocity.Y = expr_1EC_cp_0.velocity.Y - 4f;
                                        Dust expr_20A_cp_0 = Main.dust[num3];
                                        expr_20A_cp_0.velocity.X = expr_20A_cp_0.velocity.X * 2.5f;
                                        Main.dust[num3].scale = 1.3f;
                                        Main.dust[num3].alpha = 100;
                                        Main.dust[num3].noGravity = true;
                                    }
                                }
                                else
                                {
                                    for (int k = 0; k < 5; k++)
                                    {
                                        int num4 = Dust.NewDust(new Vector2(this.Position.X - 6f, this.Position.Y + (float)(this.Height / 2) - 8f), this.Width + 12, 24, 35, 0f, 0f, 0, default(Color), 1f);
                                        Dust expr_2F2_cp_0 = Main.dust[num4];
                                        expr_2F2_cp_0.velocity.Y = expr_2F2_cp_0.velocity.Y - 1.5f;
                                        Dust expr_310_cp_0 = Main.dust[num4];
                                        expr_310_cp_0.velocity.X = expr_310_cp_0.velocity.X * 2.5f;
                                        Main.dust[num4].scale = 1.3f;
                                        Main.dust[num4].alpha = 100;
                                        Main.dust[num4].noGravity = true;
                                    }
                                }
                            }
                            this.Wet = true;
                        }
                    }
                    else
                    {
                        if (this.Wet)
                        {
                            this.Wet = false;
                        }
                    }
                    if (!this.Wet)
                    {
                        this.LavaWet = false;
                    }
                    if (this.WetCount > 0)
                    {
                        this.WetCount -= 1;
                    }
                    if (this.Wet)
                    {
                        if (this.Wet)
                        {
                            Vector2 vector = this.Velocity;
                            this.Velocity = Collision.TileCollision(this.Position, this.Velocity, this.Width, this.Height, false, false);
                            if (this.Velocity.X != vector.X)
                            {
                                value.X = this.Velocity.X;
                            }
                            if (this.Velocity.Y != vector.Y)
                            {
                                value.Y = this.Velocity.Y;
                            }
                        }
                    }
                    else
                    {
                        this.Velocity = Collision.TileCollision(this.Position, this.Velocity, this.Width, this.Height, false, false);
                    }
                    if (this.Owner == Main.myPlayer && this.LavaWet && this.Type != 312 && this.Type != 318 && this.Type != 173 && this.Rare == 0)
                    {
                        if (this.Type == 267)
                        {
                            for (int l = 0; l < NPC.MAX_NPCS; l++)
                            {
                                if (Main.npcs[l].Active && Main.npcs[l].Type == 22)
                                {
                                    NetMessage.SendData(28, -1, -1, "", l, 9999f, 10f, (float)(-(float)Main.npcs[l].direction));
                                    Main.npcs[l].StrikeNPC(9999, 10f, -Main.npcs[l].direction);
                                }
                            }
                        }
                        this.Active = false;
                        this.Type = 0;
                        this.Name = "";
                        this.Stack = 0;
                        NetMessage.SendData(21, -1, -1, "", i);
                    }
                    if (this.Type == 75 && Main.dayTime)
                    {
                        for (int m = 0; m < 10; m++)
                        {
                            Dust.NewDust(this.Position, this.Width, this.Height, 15, this.Velocity.X, this.Velocity.Y, 150, default(Color), 1.2f);
                        }
                        for (int n = 0; n < 3; n++)
                        {
                            Gore.NewGore(this.Position, new Vector2(this.Velocity.X, this.Velocity.Y), Main.rand.Next(16, 18));
                        }
                        this.Active = false;
                        this.Type = 0;
                        this.Stack = 0;
                        NetMessage.SendData(21, -1, -1, "", i);
                    }
                }
                else
                {
                    this.BeingGrabbed = false;
                }

                if (this.Type == 75)
                {
                    if (Main.rand.Next(25) == 0)
                    {
                        Dust.NewDust(this.Position, this.Width, this.Height, 15, this.Velocity.X * 0.5f, this.Velocity.Y * 0.5f, 150, default(Color), 1.2f);
                    }
                    if (Main.rand.Next(50) == 0)
                    {
                        Gore.NewGore(this.Position, new Vector2(this.Velocity.X * 0.2f, this.Velocity.Y * 0.2f), Main.rand.Next(16, 18));
                    }
                }
                if (this.SpawnTime < 2147483646)
                {
                    this.SpawnTime++;
                }
                if (this.Owner != Main.myPlayer)
                {
                    this.Release++;
                    if (this.Release >= 300)
                    {
                        this.Release = 0;
                        NetMessage.SendData(39, this.Owner, -1, "", i);
                    }
                }
                if (this.Wet)
                {
                    this.Position += value;
                }
                else
                {
                    this.Position += this.Velocity;
                }
                if (this.NoGrabDelay > 0)
                {
                    this.NoGrabDelay--;
                }
            }
        }

        public static int NewItem(int X, int Y, int Width, int Height, int type, int stack = 1, bool noBroadcast = false)
        {
            if (WorldGen.gen)
            {
                return 0;
            }

            int itemIndex = 200;
            for (int i = 0; i < 200; i++)
            {
                if (!Main.item[i].Active)
                {
                    itemIndex = i;
                    break;
                }
            }


            if (itemIndex == 200)
            {
                int lastSpawned = 0;
                for (int j = 0; j < 200; j++)
                {
                    if (Main.item[j].SpawnTime > lastSpawned)
                    {
                        lastSpawned = Main.item[j].SpawnTime;
                        itemIndex = j;
                    }
                }
            }

            Item item = Registries.Item.Create(type, stack);
            item.Position.X = (float)(X + Width / 2 - Main.item[itemIndex].Width / 2);
            item.Position.Y = (float)(Y + Height / 2 - Main.item[itemIndex].Height / 2);
            item.Wet = Collision.WetCollision(Main.item[itemIndex].Position, Main.item[itemIndex].Width, Main.item[itemIndex].Height);
            item.Velocity.X = (float)Main.rand.Next(-20, 21) * 0.1f;
            item.Velocity.Y = (float)Main.rand.Next(-30, -10) * 0.1f;
            item.SpawnTime = 0;
            Main.item[itemIndex] = item;

            if (!noBroadcast)
            {
                NetMessage.SendData(21, -1, -1, "", itemIndex);
                item.FindOwner(itemIndex);
            }
            return itemIndex;
        }

        public void FindOwner(int whoAmI)
        {
            if (this.KeepTime > 0)
            {
                return;
            }
            int playerIndex = this.Owner;
            this.Owner = 255;
            float num2 = -1f;
            int count = 0;
            foreach(Player player in Main.players)
            {
                if (this.OwnIgnore != count && player.Active && player.ItemSpace(Main.item[whoAmI]))
                {
                    float num3 = Math.Abs(player.Position.X + (float)(player.width / 2) - this.Position.X - (float)(this.Width / 2)) + Math.Abs(player.Position.Y + (float)(player.height / 2) - this.Position.Y - (float)this.Height);
                    if (num3 < (float)(Main.screenWidth / 2 + Main.screenHeight / 2) && (num2 == -1f || num3 < num2))
                    {
                        num2 = num3;
                        this.Owner = count;
                    }
                }
                count++;
            }
            if (this.Owner != playerIndex && ((playerIndex == Main.myPlayer) || (playerIndex == 255) 
                || !Main.players[playerIndex].Active))
            {
                 NetMessage.SendData(21, -1, -1, "", whoAmI);
                if (this.Active)
                {
                    NetMessage.SendData(22, -1, -1, "", whoAmI);
                }
            }
        }

        public object Clone()
        {
            return base.MemberwiseClone();
        }

        public bool IsTheSameAs(Item compareItem)
        {
            return this.Name == compareItem.Name;
        }
    }
}
