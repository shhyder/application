using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Web
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class MarkerIcon : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            String drawString = "";



            Bitmap bitmap; 


            Image image = null;
            Image thumbnailImage = null;
            Color waterMarkColor = Color.White;

            if (context.Request.QueryString["col"] != null)
            {
                waterMarkColor = Color.Black;
                bitmap = new Bitmap(System.Web.HttpContext.Current.Server.MapPath("ICon/yellowBlank.png"));
            }
            else
            {
                waterMarkColor = Color.White;
                bitmap = new Bitmap(System.Web.HttpContext.Current.Server.MapPath("ICon/redblank.png"));
            }


            using (MemoryStream memoryStream = new MemoryStream())
            {
                //string actionName = context.Request.QueryString["Image"];
                //string opacity = context.Request.QueryString["Opacity"];
                int opacityPercent = 255;// int.Parse(opacity);
                

                string myCompany = context.Request.QueryString["label"];
                Font font = new Font("Arial, Helvetica, sans-serif", 9f, FontStyle.Bold);
                context.Response.ContentType = "image/png";
                //bitmap = Resources.Resources.BlueHills;
                Graphics g = Graphics.FromImage(bitmap);
                Brush myBrush = new SolidBrush(Color.FromArgb(opacityPercent,waterMarkColor));
                SizeF sz = g.MeasureString(myCompany, font);
                int X = (int)(bitmap.Width - sz.Width) / 2;
                int Y = (int)(bitmap.Height - sz.Height - 3) / 2;
                g.DrawString(myCompany, font, myBrush, new Point(X, Y));
                bitmap.Save(memoryStream, ImageFormat.Png);



                context.Response.BinaryWrite(memoryStream.GetBuffer());
                memoryStream.Close();
                if (image != null) { image.Dispose(); }
                if (thumbnailImage != null) { thumbnailImage.Dispose(); }
                if (bitmap != null) { bitmap.Dispose(); }
            }

            //using (Bitmap b = bmap  )// new Bitmap(150, 40, PixelFormat.Format32bppArgb))
            //{
            //    using (Graphics g = Graphics.FromImage(b))
            //    {
            //        //Rectangle rect = new Rectangle(0, 0, 149, 39);
            //        //g.FillRectangle(Brushes.White, rect);

            //        //// Create string to draw.
            //        //Random r = new Random();
            //        //int startIndex = r.Next(1, 5);
            //        //int length = r.Next(5, 10);
            //        //drawString = Guid.NewGuid().ToString().Replace("-", "0").Substring(startIndex, length);

            //        //// Create font and brush.
            //        //Font drawFont = new Font("Arial", 16, FontStyle.Italic | FontStyle.Strikeout);
            //        //using (SolidBrush drawBrush = new SolidBrush(Color.Black))
            //        //{
            //        //    // Create point for upper-left corner of drawing.
            //        //    PointF drawPoint = new PointF(15, 10);

            //        //    // Draw string to screen.
            //        //    g.DrawRectangle(new Pen(Color.Red, 0), rect);
            //        //    g.DrawString(drawString, drawFont, drawBrush, drawPoint);
            //        //}


            //        //context.Session["RandomStr"] = drawString;

            //        //System.Web.HttpContext.Current.Session["RandomStr"] = drawString;
            //        //context.Session["RandomStr"] = drawString;
            //        b.Save(context.Response.OutputStream, ImageFormat.Jpeg);
            //        context.Response.ContentType = "image/jpeg";
            //        context.Response.End();
            //    }
            //}
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
