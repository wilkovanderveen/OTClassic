using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using OpenTemplater.Models.Interfaces;
using OpenTemplater.Models.Layout;
using OpenTemplater.Common.Measuring;

namespace OpenTemplater.Models
{
    public class Image : BasePageElement
    {
        private string _uri;
        private bool _keepHorizontalAspectRatio;
        private bool _keepVerticalAspectRatio;
        private System.Drawing.Image _image;
        
        /// <summary>
        /// Unified Resource to the image.
        /// </summary>
        public string Uri
        {
            get { return _uri; }
        }

        public Unit ContentHeight
        {
            get { throw new NotImplementedException(); }
        }

        public Image(Content container, string key, string uri)
            : base(key, container.Parent)
        {
            Key = key;
            Container = container;
            _uri = uri;
            _image = System.Drawing.Image.FromFile(_uri);

        }

        public void ConvertDPI()
        {
            int dpi = this.Container.Parent.Page.Document.Dpi;
            
            int destinationWidth = Convert.ToInt32(this.Layout.Width.Inches * dpi);
            int destinationHeight = Convert.ToInt32(this.Layout.Height.Inches * dpi);

            Bitmap result = new Bitmap(destinationWidth, destinationHeight);
            using (Graphics g = Graphics.FromImage(result))
                g.DrawImage(_image, 0, 0, destinationWidth, destinationHeight);

            result.SetResolution(dpi, dpi);

            _uri = Path.GetTempPath() + Guid.NewGuid().ToString();
            result.Save(_uri, ImageFormat.Jpeg);
        }

       
        public void ResizeToContent()
        {
            
        }

        public System.Drawing.Image GetImage()
        {
            return _image;
        }
    }
}