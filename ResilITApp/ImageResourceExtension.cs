using System;
//using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ResilITApp
{
    [ContentProperty (nameof(Source))]
    public class ImageResourceExtension : IMarkupExtension
    {
        public string Source { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            //var assembly = typeof(EmbeddedImages).GetTypeInfo().Assembly;
            //foreach(var res in assembly.GetManifestResourceNames())
            //{
            //    System.Diagnostics.Debug.WriteLine("found resource: " + res);
            //}

            if(Source == null)
            {
                return null;
            }

            // Do your translation lookup here, using whatever method you require.
            //var imageSource = ImageSource.FromResource(Source, typeof(ImageResourceExtension).GetTypeInfo().Assembly);
            var imageSource = ImageSource.FromResource(Source);

            return imageSource;
        }
    }
}
