// Copyright 2011 Miyako Komooka
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace iCalControls {

    [TemplatePart(Name = "Image", Type = typeof(Image))]    
    public class Icon : Control {

        public Icon() {

            this.DefaultStyleKey = typeof(Icon);

        }

        public override void OnApplyTemplate() {

            Image = GetTemplateChild("Image") as Image;
           
            /*
            if( Image != null ) {
                Image.Source = Bitmap;
            }
             */

            UpdateBitmap();
        }

        protected int ToArgb( Color color ){

            int r = color.R * color.A / 0xFF;
            int g = color.G * color.A / 0xFF;
            int b = color.B * color.A / 0xFF;

            int ret = (color.A << 24) | (r << 16) | (g << 8) | b;

            return( ret );
        }

        protected virtual void UpdateBitmap()
        {
            if( Image == null ) {
                return;
            }

            int fill = ToArgb( FillColor );
            int stroke = ToArgb( StrokeColor );

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

        protected static void
            PropertyChangedCallback( DependencyObject obj,
                                     DependencyPropertyChangedEventArgs args)
        {
            Icon ctl = (Icon)obj;
            ctl.UpdateBitmap();
        }

        public static readonly DependencyProperty FillColorProperty =
            DependencyProperty.Register(
                "FillColor",
                typeof(Color),
                typeof(Icon),
                new PropertyMetadata(Color.FromArgb( 0xFF, 0xFE, 0xFE, 0xFE ),
                                     new PropertyChangedCallback(PropertyChangedCallback)));

        public Color FillColor {
            get { return (Color)GetValue( FillColorProperty ); }
            set { SetValue( FillColorProperty, value ); }
        }

        public static readonly DependencyProperty StrokeColorProperty =
            DependencyProperty.Register(
                "StorokeColor",
                typeof(Color),
                typeof(Icon),
                new PropertyMetadata(Color.FromArgb( 0xFF, 0x7F, 0x7F, 0x7F ),
                                     new PropertyChangedCallback(PropertyChangedCallback)));

        public Color StrokeColor {
            get { return (Color)GetValue( StrokeColorProperty ); }
            set { SetValue( StrokeColorProperty, value ); }
        }

        /*
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
        */

        protected WriteableBitmap Bitmap = null;
        protected Image Image;

        protected int PixelWidth = 11;
        protected int PixelHeight = 12;

        // 0:Transparent, 1:Stroke 2:Fill
        protected int[] Pixels = {
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
}


