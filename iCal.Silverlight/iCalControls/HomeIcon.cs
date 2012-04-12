// Copyright 2011 Miyako Komooka
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace iCalControls {

    public class HomeIcon : iCalControls.Icon {

        public HomeIcon() {
            PixelWidth = 14;
            PixelHeight = 13;
            
            // 0:Transparent, 1:Stroke 2:Fill, 3: White
            Pixels = new int[]{
                0,0,0,0,0,0,1,1,0,0,0,0,0,0,
                0,0,0,0,0,1,2,2,1,0,0,0,0,0,
                0,0,0,0,1,2,2,2,2,1,0,0,0,0,
                0,0,0,1,2,2,2,2,2,2,1,0,0,0,
                0,0,1,2,2,2,1,1,2,2,2,1,0,0,
                0,1,2,2,2,1,3,3,1,2,2,2,1,0,
                1,2,2,2,1,3,3,3,3,1,2,2,2,1,
                1,2,2,1,3,3,3,3,3,3,1,2,2,1,
                0,1,1,3,3,3,1,1,3,3,3,1,1,0,
                0,0,1,3,3,1,3,3,1,3,3,1,0,0,
                0,0,1,3,3,1,3,3,1,3,3,1,0,0,
                0,0,1,3,3,1,3,3,1,3,3,1,0,0,
                0,0,1,1,1,1,1,1,1,1,1,1,0,0,
            };
        }

        protected override void UpdateBitmap()
        {
            if( Image == null ) {
                return;
            }

            int fill = ToArgb( FillColor );
            int stroke = ToArgb( StrokeColor );
            int white = ToArgb( Colors.White );

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
                    Bitmap.Pixels[i] = white;
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
00000000,00000000,00000000,00000000,00000000,00000000,FF989898,FF989898,00000000,00000000,00000000,00000000,00000000,00000000,
00000000,00000000,00000000,00000000,00000000,FF989898,FFE6E6E6,FFE6E6E6,FF989898,00000000,00000000,00000000,00000000,00000000,
00000000,00000000,00000000,00000000,FF989898,FFE6E6E6,FFCBCBCB,FFCBCBCB,FFCBCBCB,FF989898,00000000,00000000,00000000,00000000,
00000000,00000000,00000000,FF989898,FFE6E6E6,FFCBCBCB,FFCBCBCB,FFCBCBCB,FFCBCBCB,FFCBCBCB,FF989898,00000000,00000000,00000000,
00000000,00000000,FF989898,FFE6E6E6,FFCBCBCB,FFCBCBCB,FF989898,FF989898,FFCBCBCB,FFCBCBCB,FFCBCBCB,FF989898,00000000,00000000,
00000000,FF989898,FFE6E6E6,FFCBCBCB,FFCBCBCB,FF989898,FFECECEC,FFECECEC,FF989898,FFCBCBCB,FFCBCBCB,FFCBCBCB,FF989898,00000000,
FF989898,FFE6E6E6,FFCBCBCB,FFCBCBCB,FF989898,FFECECEC,FFFEFEFE,FFFEFEFE,FFECECEC,FF989898,FFCBCBCB,FFCBCBCB,FFCBCBCB,FF989898,
FF989898,FFCBCBCB,FFCBCBCB,FF989898,FFECECEC,FFFEFEFE,FFFEFEFE,FFFEFEFE,FFFEFEFE,FFECECEC,FF989898,FFB9B9B9,FFB9B9B9,FF989898,
00000000,FF989898,FF989898,FFECECEC,FFFEFEFE,FFFEFEFE,FF767676,FF767676,FFFEFEFE,FFFEFEFE,FFF8F8F8,FF989898,FF989898,00000000,
00000000,00000000,FF767676,FFFEFEFE,FFFEFEFE,FF767676,FFFEFEFE,FFFEFEFE,FF767676,FFF8F8F8,FFECECEC,FF767676,00000000,00000000,
00000000,00000000,FF767676,FFFEFEFE,FFFEFEFE,FF767676,FFF8F8F8,FFD3D3D3,FF767676,FFE6E6E6,FFE0E0E0,FF767676,00000000,00000000,
00000000,00000000,FF767676,FFFEFEFE,FFF8F8F8,FF767676,FFECECEC,FFE6E6E6,FF767676,FFD3D3D3,FFD3D3D3,FF767676,00000000,00000000,
00000000,00000000,FF767676,FF767676,FF767676,FF767676,FF767676,FF767676,FF767676,FF767676,FF767676,FF767676,00000000,00000000,
        */
        
    }
}


