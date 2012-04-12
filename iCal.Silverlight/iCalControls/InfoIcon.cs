// Copyright 2011 Miyako Komooka
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace iCalControls {

    public class InfoIcon : iCalControls.Icon {

        public InfoIcon() {
            PixelWidth = 14;
            PixelHeight = 14;
            
            // 0:Transparent, 1:Stroke 2:Fill 3:Gray
            Pixels = new int[]{
                0,0,0,0,0,3,3,3,3,0,0,0,0,0,
                0,0,0,3,3,2,2,2,2,3,3,0,0,0,
                0,0,3,2,2,2,2,2,2,2,2,3,0,0,
                0,3,2,2,2,2,2,2,2,2,2,2,3,0,
                0,3,2,2,2,2,1,1,2,2,2,2,3,0,
                3,2,2,2,2,2,1,1,2,2,2,2,2,3,
                3,2,2,2,2,2,2,2,2,2,2,2,2,3,
                3,2,2,2,2,2,1,1,2,2,2,2,2,3,
                3,2,2,2,2,2,1,1,2,2,2,2,2,3,
                0,3,2,2,2,2,1,1,2,2,2,2,3,0,
                0,3,2,2,2,2,2,2,2,2,2,2,3,0,
                0,0,3,2,2,2,2,2,2,2,2,3,0,0,
                0,0,0,3,3,2,2,2,2,3,3,0,0,0,
                0,0,0,0,0,3,3,3,3,0,0,0,0,0,
            };

            /*
            Pixels = new int[]{
                0,0,0,0,0,1,1,1,1,0,0,0,0,0,
                0,0,0,1,1,2,2,2,2,1,1,0,0,0,
                0,0,1,2,2,2,2,2,2,2,2,1,0,0,
                0,1,2,2,2,2,2,2,2,2,2,2,1,0,
                0,1,2,2,2,2,1,1,2,2,2,2,1,0,
                1,2,2,2,2,2,1,1,2,2,2,2,2,1,
                1,2,2,2,2,2,2,2,2,2,2,2,2,1,
                1,2,2,2,2,2,1,1,2,2,2,2,2,1,
                1,2,2,2,2,2,1,1,2,2,2,2,2,1,
                0,1,2,2,2,2,1,1,2,2,2,2,1,0,
                0,1,2,2,2,2,2,2,2,2,2,2,1,0,
                0,0,1,2,2,2,2,2,2,2,2,1,0,0,
                0,0,0,1,1,2,2,2,2,1,1,0,0,0,
                0,0,0,0,0,1,1,1,1,0,0,0,0,0,
            };
            */
        }

        protected override void UpdateBitmap()
        {
            if( Image == null ) {
                return;
            }

            int fill = ToArgb( FillColor );
            int stroke = ToArgb( StrokeColor );
            int gray = ToArgb( Colors.Gray );

            Bitmap = new WriteableBitmap( PixelWidth, PixelHeight );
            
            for( int i = 0; i < Bitmap.Pixels.Length; i++ ){
                int index = i;

                switch( this.Pixels[index] ){
                case 1:
                    Bitmap.Pixels[i] = stroke;
                    break;
                case 2:
                    Bitmap.Pixels[i] = fill;
                    break;
                case 3:
                    Bitmap.Pixels[i] = gray;
                    break;
                default:
                    Bitmap.Pixels[i] = 0;
                    break;
                }
            }
         
            if( Image != null ) {
                Image.Source = Bitmap;
            }
            // Bitmap.Invalidate();
        }

        /*
00000000,00000000,00000000,00000000,00000000,FF323232,FF323232,FF323232,FF323232,00000000,00000000,00000000,00000000,00000000,
00000000,00000000,00000000,FF323232,FF323232,FFBFBFBF,FFBFBFBF,FFBFBFBF,FFBFBFBF,FF323232,FF323232,00000000,00000000,00000000,
00000000,00000000,FF323232,FFBFBFBF,FFBFBFBF,FFBFBFBF,FF393939,FF393939,FFBFBFBF,FFBFBFBF,FFBFBFBF,FF323232,00000000,00000000,
00000000,FF323232,FFBFBFBF,FFBFBFBF,FF3F3F3F,FF474747,FF474747,FF474747,FF3F3F3F,FF393939,FFBFBFBF,FFBFBFBF,FF323232,00000000,
00000000,FF323232,FFBFBFBF,FF3F3F3F,FF474747,FF505050,FFFEFEFE,FFFEFEFE,FF474747,FF3F3F3F,FF393939,FFBFBFBF,FF323232,00000000,
FF323232,FFBFBFBF,FFBFBFBF,FF474747,FF505050,FF505050,FFFEFEFE,FFFEFEFE,FF505050,FF474747,FF393939,FFBFBFBF,FFBFBFBF,FF323232,
FF323232,FFBFBFBF,FF393939,FF474747,FF505050,FF5B5B5B,FF656565,FF5B5B5B,FF505050,FF474747,FF393939,FF323232,FFBFBFBF,FF323232,
FF323232,FFBFBFBF,FF393939,FF474747,FF505050,FF505050,FFFEFEFE,FFFEFEFE,FF505050,FF474747,FF393939,FF323232,FFBFBFBF,FF323232,
FF323232,FFBFBFBF,FFBFBFBF,FF3F3F3F,FF474747,FF505050,FFFEFEFE,FFFEFEFE,FF474747,FF3F3F3F,FF393939,FFBFBFBF,FFBFBFBF,FF323232,
00000000,FF323232,FFBFBFBF,FF393939,FF3F3F3F,FF3F3F3F,FFFEFEFE,FFFEFEFE,FF3F3F3F,FF393939,FF323232,FFBFBFBF,FF323232,00000000,
00000000,FF323232,FFBFBFBF,FFBFBFBF,FF393939,FF393939,FF393939,FF393939,FF393939,FF323232,FFBFBFBF,FFBFBFBF,FF323232,00000000,
00000000,00000000,FF323232,FFBFBFBF,FFBFBFBF,FFBFBFBF,FF323232,FF323232,FFBFBFBF,FFBFBFBF,FFBFBFBF,FF323232,00000000,00000000,
00000000,00000000,00000000,FF323232,FF323232,FFBFBFBF,FFBFBFBF,FFBFBFBF,FFBFBFBF,FF323232,FF323232,00000000,00000000,00000000,
00000000,00000000,00000000,00000000,00000000,FF323232,FF323232,FF323232,FF323232,00000000,00000000,00000000,00000000,00000000,
        */
        
    }
}


