// Copyright 2011 Miyako Komooka
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace iCalControls {

    public class Arrow : iCalControls.Icon {

        public Arrow() {
            PixelWidth = 11;
            PixelHeight = 12;
            
            // 0:Transparent, 1:Stroke 2:Fill
            Pixels = new int[]{
                0,0,0,0,0,1,0,0,0,0,0,
                0,0,0,0,1,1,0,0,0,0,0,
                0,0,0,1,2,1,0,0,0,0,0,
                0,0,1,2,2,1,1,1,1,1,1,
                0,1,2,2,2,2,2,2,2,2,1,
                1,2,2,2,2,2,2,2,2,2,1,
                1,2,2,2,2,2,2,2,2,2,1,
                0,1,2,2,2,2,2,2,2,2,1,
                0,0,1,2,2,1,1,1,1,1,1,
                0,0,0,1,2,1,0,0,0,0,0,
                0,0,0,0,1,1,0,0,0,0,0,
                0,0,0,0,0,1,0,0,0,0,0
            };
        }

        protected override void UpdateBitmap()
        {
            if( Image == null ) {
                return;
            }

            int fill = ToArgb( FillColor );
            int stroke = ToArgb( StrokeColor );

            Bitmap = new WriteableBitmap( PixelWidth, PixelHeight );
            
            for( int i = 0; i < Bitmap.Pixels.Length; i++ ){
                int index;

                if( ArrowDirection == FlowDirection.LeftToRight ){
                    int y = i / PixelWidth;
                    int x = i % PixelWidth;
                    index = y * PixelWidth + (PixelWidth - x - 1);
                } else {
                    index = i;
                }

                switch( this.Pixels[index] ){
                case 1:
                    Bitmap.Pixels[i] = stroke;
                    break;
                case 2:
                    Bitmap.Pixels[i] = fill;
                    break;
                default:
                    Bitmap.Pixels[i] = 0;
                    break;
                }
                // unchecked{ Bitmap.Pixels[i] = (int)this.Pixels[i]; }
            }
         
            if( Image != null ) {
                Image.Source = Bitmap;
            }
            // Bitmap.Invalidate();
        }

        public static readonly DependencyProperty ArrowDirectionProperty =
            DependencyProperty.Register(
                "ArrowDirection",
                typeof(FlowDirection),
                typeof(Arrow),
                new PropertyMetadata(new PropertyChangedCallback(PropertyChangedCallback)));

        public FlowDirection ArrowDirection {
            get { return (FlowDirection)GetValue( ArrowDirectionProperty ); }
            set { SetValue( ArrowDirectionProperty, value ); }
        }


        /*
        uint[] Pixels = {
0x00000000,0x00000000,0x00000000,0x00000000,0x00000000,0xFF7F7F7F,0x00000000,0x00000000,0x00000000,0x00000000,0x00000000,
0x00000000,0x00000000,0x00000000,0x00000000,0xFF7F7F7F,0xFF7F7F7F,0x00000000,0x00000000,0x00000000,0x00000000,0x00000000,
0x00000000,0x00000000,0x00000000,0xFF7F7F7F,0xFFFEFEFE,0xFF7F7F7F,0x00000000,0x00000000,0x00000000,0x00000000,0x00000000,
0x00000000,0x00000000,0xFF7F7F7F,0xFFFEFEFE,0xFFFEFEFE,0xFF7F7F7F,0xFF7F7F7F,0xFF7F7F7F,0xFF7F7F7F,0xFF7F7F7F,0xFF7F7F7F,
0x00000000,0xFF7F7F7F,0xFFFEFEFE,0xFFFEFEFE,0xFFFEFEFE,0xFFFEFEFE,0xFFFEFEFE,0xFFFEFEFE,0xFFFEFEFE,0xFFFEFEFE,0xFF7F7F7F,
0xFF7F7F7F,0xFFFEFEFE,0xFFFEFEFE,0xFFFEFEFE,0xFFFEFEFE,0xFFFEFEFE,0xFFFEFEFE,0xFFFEFEFE,0xFFFEFEFE,0xFFFEFEFE,0xFF7F7F7F,
0xFF7F7F7F,0xFFFEFEFE,0xFFFEFEFE,0xFFFEFEFE,0xFFFEFEFE,0xFFFEFEFE,0xFFFEFEFE,0xFFFEFEFE,0xFFFEFEFE,0xFFFEFEFE,0xFF7F7F7F,
0x00000000,0xFF7F7F7F,0xFFFEFEFE,0xFFFEFEFE,0xFFFEFEFE,0xFFFEFEFE,0xFFFEFEFE,0xFFFEFEFE,0xFFFEFEFE,0xFFFEFEFE,0xFF7F7F7F,
0x00000000,0x00000000,0xFF7F7F7F,0xFFFEFEFE,0xFFFEFEFE,0xFF7F7F7F,0xFF7F7F7F,0xFF7F7F7F,0xFF7F7F7F,0xFF7F7F7F,0xFF7F7F7F,
0x00000000,0x00000000,0x00000000,0xFF7F7F7F,0xFFBFBFBF,0xFF7F7F7F,0x00000000,0x00000000,0x00000000,0x00000000,0x00000000,
0x00000000,0x00000000,0x00000000,0x00000000,0xFF7F7F7F,0xFF7F7F7F,0x00000000,0x00000000,0x00000000,0x00000000,0x00000000,
0x00000000,0x00000000,0x00000000,0x00000000,0x00000000,0xFF7F7F7F,0x00000000,0x00000000,0x00000000,0x00000000,0x00000000
        };
        */
        
    }
}


