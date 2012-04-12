// Copyright 2011 Miyako Komooka
using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace SilverlightGadgetUtilities
{
    public class SilverlightColors
    {
        public static Dictionary<string, ColorItem > Colors()
        {
            Dictionary<string, ColorItem> colorDict =
                new Dictionary<string,ColorItem>();

            ColorItem citem;


            citem = new ColorItem( "Transparent",                0x00,0xFF,0xFF,0xFF );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "Black",                      0xFF,0x00,0x00,0x00 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "White",                      0xFF,0xFF,0xFF,0xFF );
            colorDict.Add( citem.Name, citem );

            citem = new ColorItem( "AliceBlue",                  0xFF,0xF0,0xF8,0xFF );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "AntiqueWhite",               0xFF,0xFA,0xEB,0xD7 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "Aqua",                       0xFF,0x00,0xFF,0xFF );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "Aquamarine",                 0xFF,0x7F,0xFF,0xD4 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "Azure",                      0xFF,0xF0,0xFF,0xFF );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "Beige",                      0xFF,0xF5,0xF5,0xDC );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "Bisque",                     0xFF,0xFF,0xE4,0xC4 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "BlanchedAlmond",             0xFF,0xFF,0xEB,0xCD );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "Blue",                       0xFF,0x00,0x00,0xFF );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "BlueViolet",                 0xFF,0x8A,0x2B,0xE2 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "Brown",                      0xFF,0xA5,0x2A,0x2A );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "BurlyWood",                  0xFF,0xDE,0xB8,0x87 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "CadetBlue",                  0xFF,0x5F,0x9E,0xA0 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "Chartreuse",                 0xFF,0x7F,0xFF,0x00 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "Chocolate",                  0xFF,0xD2,0x69,0x1E );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "Coral",                      0xFF,0xFF,0x7F,0x50 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "ComflowerBlue",              0xFF,0x64,0x95,0xED );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "Comsilk",                    0xFF,0xFF,0xF8,0xDC );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "Crimson",                    0xFF,0xDC,0x14,0x3C );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "Cyan",                       0xFF,0x00,0xFF,0xFF );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "DarkBlue",                   0xFF,0x00,0x00,0x8B );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "DarkCyan",                   0xFF,0x00,0x8B,0x8B );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "DarkGoldenrod",              0xFF,0xB8,0x86,0x0B );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "DarkGray",                   0xFF,0xA9,0xA9,0xA9 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "DarkGreen",                  0xFF,0x00,0x64,0x00 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "DarkKhaki",                  0xFF,0xBD,0xB7,0x6B );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "DarkMagenta",                0xFF,0x8B,0x00,0x8B );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "DarkOliveGreen",             0xFF,0x55,0x6B,0x2F );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "DarkOrange",                 0xFF,0xFF,0x8C,0x00 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "DarkOrchid",                 0xFF,0x99,0x32,0xCC );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "DarkRed",                    0xFF,0x8B,0x00,0x00 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "DarkSalmon",                 0xFF,0xE9,0x96,0x7A );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "DarkSeaGreen",               0xFF,0x8F,0xBC,0x8F );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "DarkSlateBlue",              0xFF,0x48,0x3D,0x8B );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "DarkSlateGray",              0xFF,0x2F,0x4F,0x4F );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "DarkTurquoise",              0xFF,0x00,0xCE,0xD1 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "DarkViolet",                 0xFF,0x94,0x00,0xD3 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "DeepPink",                   0xFF,0xFF,0x14,0x93 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "DeepSkyBlue",                0xFF,0x00,0xBF,0xFF );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "DimGray",                    0xFF,0x69,0x69,0x69 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "DodgerBlue",                 0xFF,0x1E,0x90,0xFF );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "Firebrick",                  0xFF,0xB2,0x22,0x22 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "FloralWhite",                0xFF,0xFF,0xFA,0xF0 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "ForestGreen",                0xFF,0x22,0x8B,0x22 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "Fuchsia",                    0xFF,0xFF,0x00,0xFF );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "Gainsboro",                  0xFF,0xDC,0xDC,0xDC );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "GhostWhite",                 0xFF,0xF8,0xF8,0xFF );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "Gold",                       0xFF,0xFF,0xD7,0x00 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "Goldenrod",                  0xFF,0xDA,0xA5,0x20 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "Gray",                       0xFF,0x80,0x80,0x80 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "Green",                      0xFF,0x00,0x80,0x00 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "GreenYellow",                0xFF,0xAD,0xFF,0x2F );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "Honeydew",                   0xFF,0xF0,0xFF,0xF0 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "HotPink",                    0xFF,0xFF,0x69,0xB4 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "IndianRed",                  0xFF,0xCD,0x5C,0x5C );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "Indigo",                     0xFF,0x4B,0x00,0x82 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "Ivory",                      0xFF,0xFF,0xFF,0xF0 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "Khaki",                      0xFF,0xF0,0xE6,0x8C );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "Lavender",                   0xFF,0xE6,0xE6,0xFA );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "LavenderBlush",              0xFF,0xFF,0xF0,0xF5 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "LawnGreen",                  0xFF,0x7C,0xFC,0x00 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "LemonChiffon",               0xFF,0xFF,0xFA,0xCD );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "LightBlue",                  0xFF,0xAD,0xD8,0xE6 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "LightCoral",                 0xFF,0xF0,0x80,0x80 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "LightCyan",                  0xFF,0xE0,0xFF,0xFF );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "LightGoldenrodYellow",       0xFF,0xFA,0xFA,0xD2 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "LightGray",                  0xFF,0xD3,0xD3,0xD3 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "LightGreen",                 0xFF,0x90,0xEE,0x90 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "LightPink",                  0xFF,0xFF,0xB6,0xC1 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "LightSalmon",                0xFF,0xFF,0xA0,0x7A );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "LightSeaGreen",              0xFF,0x20,0xB2,0xAA );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "LightSkyBlue",               0xFF,0x87,0xCE,0xFA );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "LightSlateGray",             0xFF,0x77,0x88,0x99 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "LightSteelBlue",             0xFF,0xB0,0xC4,0xDE );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "LightYellow",                0xFF,0xFF,0xFF,0xE0 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "Lime",                       0xFF,0x00,0xFF,0x00 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "LimeGreen",                  0xFF,0x32,0xCD,0x32 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "linen",                      0xFF,0xFA,0xF0,0x36 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "Magenta",                    0xFF,0xFF,0x00,0xFF );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "Maroon",                     0xFF,0x80,0x00,0x00 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "MediumAquamarine",           0xFF,0x66,0xCD,0xAA );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "MediumBlue",                 0xFF,0x00,0x00,0xCD );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "MediumOrchid",               0xFF,0xBA,0x55,0xD3 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "MediumPurple",               0xFF,0x93,0x70,0xDB );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "MediumSeaGreen",             0xFF,0x3C,0xB3,0x71 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "MediumSlateBlue",            0xFF,0x7B,0x68,0xEE );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "MediumSpringGreen",          0xFF,0x00,0xFA,0x9A );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "MediumTurquoise",            0xFF,0x48,0xD1,0xCC );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "MediumVioletRed",            0xFF,0xC7,0x15,0x85 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "MidnightBlue",               0xFF,0x19,0x19,0x70 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "MintCream",                  0xFF,0xF5,0xFF,0xFA );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "MistyRose",                  0xFF,0xFF,0xE4,0xE1 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "Moccasin",                   0xFF,0xFF,0xE4,0xB5 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "NavajoWhite",                0xFF,0xFF,0xDE,0xAD );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "Navy",                       0xFF,0x00,0x00,0x80 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "OldLace",                    0xFF,0xFD,0xF5,0xE6 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "Olive",                      0xFF,0x80,0x80,0x00 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "OliveDrab",                  0xFF,0x6B,0x8E,0x23 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "Orange",                     0xFF,0xFF,0xA5,0x00 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "OrangeRed",                  0xFF,0xFF,0x45,0x00 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "Orchid",                     0xFF,0xDA,0x70,0xD6 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "PaleGoldenrod",              0xFF,0xEE,0xE8,0xAA );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "PaleGreen",                  0xFF,0x98,0xFB,0x98 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "PaleTurquoise",              0xFF,0xAF,0xEE,0xEE );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "PaleVioletRed",              0xFF,0xDB,0x70,0x93 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "PapayaWhip",                 0xFF,0xFF,0xEF,0xD5 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "PeachPuff",                  0xFF,0xFF,0xDA,0xB9 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "Peru",                       0xFF,0xCD,0x85,0x3F );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "Pink",                       0xFF,0xFF,0xC0,0xCB );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "Plum",                       0xFF,0xDD,0xA0,0xDD );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "PowderBlue",                 0xFF,0xB0,0xE0,0xE6 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "Purple",                     0xFF,0x80,0x00,0x80 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "Red",                        0xFF,0xFF,0x00,0x00 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "RosyBrown",                  0xFF,0xBC,0x8F,0x8F );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "RoyalBlue",                  0xFF,0x41,0x69,0xE1 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "SaddleBrown",                0xFF,0x8B,0x45,0x13 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "Salmon",                     0xFF,0xFA,0x80,0x72 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "SandyBrown",                 0xFF,0xF4,0xA4,0x60 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "SeaGreen",                   0xFF,0x2E,0x8B,0x57 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "SeaShell",                   0xFF,0xFF,0xF5,0xEE );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "Sienna",                     0xFF,0xA0,0x52,0x2D );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "Silver",                     0xFF,0xC0,0xC0,0xC0 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "SkyBlue",                    0xFF,0x87,0xCE,0xEB );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "SlateBlue",                  0xFF,0x6A,0x5A,0xCD );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "SlateGray",                  0xFF,0x70,0x80,0x90 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "Snow",                       0xFF,0xFF,0xFA,0xFA );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "SpringGreen",                0xFF,0x00,0xFF,0x7F );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "SteelBlue",                  0xFF,0x46,0x82,0xB4 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "Tan",                        0xFF,0xD2,0xB4,0x8C );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "Teal",                       0xFF,0x00,0x80,0x80 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "Thistle",                    0xFF,0xD8,0xBF,0xD8 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "Tomato",                     0xFF,0xFF,0x63,0x47 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "Turquoise",                  0xFF,0x40,0xE0,0xD0 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "Violet",                     0xFF,0xEE,0x82,0xEE );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "Wheat",                      0xFF,0xF5,0xDE,0xB3 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "WhiteSmoke",                 0xFF,0xF5,0xF5,0xF5 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "Yellow",                     0xFF,0xFF,0xFF,0x00 );
            colorDict.Add( citem.Name, citem );
            citem = new ColorItem( "YellowGreen",                0xFF,0x9A,0xCD,0x32 );
            colorDict.Add( citem.Name, citem );

            return colorDict;
        }
    }

    public class ColorItem : IEquatable<ColorItem>

    {
        public string Name { get; set; }
        public Color Color { get; set; }
        public Brush Solid {
            get {
                if( Name == null || Name == "" ){
                    return null;
                } else {
                    return new SolidColorBrush( Color );
                }
            }
        }

        public string Border { get; set; }

        public ColorItem( string name, byte a, byte r, byte g, byte b ){
            this.Name = name;
            this.Color = Color.FromArgb( a, r, g, b );

            this.Border = "Black";
        }

        public bool Equals( ColorItem other ){
            if( this.Name != other.Name ){
                return false;
            }
            if( this.Color != other.Color ){
                return false;
            }
            if( this.Border != other.Border ){
                return false;
            }

            return true;
        }

        public static ColorItem BlankColorItem() {
            ColorItem colorItem = new ColorItem( "", 0x00, 0xFF, 0xFF, 0xFF );
            colorItem.Border = "Transparent";

            return colorItem;
        }
    }
}
