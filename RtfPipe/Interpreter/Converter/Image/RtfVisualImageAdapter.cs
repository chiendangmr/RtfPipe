// -- FILE ------------------------------------------------------------------
// name       : RtfVisualImageAdapter.cs
// project    : RTF Framelet
// created    : Jani Giannoudis - 2008.06.05
// language   : c#
// environment: .NET 2.0
// copyright  : (c) 2004-2013 by Jani Giannoudis, Switzerland
// --------------------------------------------------------------------------
using System;
using System.Globalization;
#if DRAWING
using System.Drawing.Imaging;
#endif

namespace RtfPipe.Converter.Image
{
	// ------------------------------------------------------------------------
	public class RtfVisualImageAdapter : IRtfVisualImageAdapter
	{

		// ----------------------------------------------------------------------
		public const double DefaultDpi = 96.0;

		// ----------------------------------------------------------------------
		public RtfVisualImageAdapter() :
			this( defaultFileNamePattern )
		{
		} // RtfVisualImageAdapter

		// ----------------------------------------------------------------------
		public RtfVisualImageAdapter( string fileNamePattern )
		{
      if (fileNamePattern == null)
      {
        throw new ArgumentNullException("fileNamePattern");
      }

      this.fileNamePattern = fileNamePattern;
      this.dpiX = DefaultDpi;
      this.dpiY = DefaultDpi;
    } // RtfVisualImageAdapter

#if DRAWING
    // ----------------------------------------------------------------------
    public RtfVisualImageAdapter( ImageFormat targetFormat ) :
			this( defaultFileNamePattern, targetFormat )
		{
		} // RtfVisualImageAdapter

		// ----------------------------------------------------------------------
		public RtfVisualImageAdapter( string fileNamePattern, ImageFormat targetFormat ) :
			this( fileNamePattern, targetFormat, DefaultDpi, DefaultDpi )
		{
		} // RtfVisualImageAdapter

		// ----------------------------------------------------------------------
		public RtfVisualImageAdapter( string fileNamePattern, ImageFormat targetFormat, double dpiX, double dpiY )
		{
			if ( fileNamePattern == null )
			{
				throw new ArgumentNullException( "fileNamePattern" );
			}

			this.fileNamePattern = fileNamePattern;
			this.targetFormat = targetFormat;
			this.dpiX = dpiX;
			this.dpiY = dpiY;
		} // RtfVisualImageAdapter
#endif

		// ----------------------------------------------------------------------
		public string FileNamePattern
		{
			get { return fileNamePattern; }
		} // FileNamePattern

#if DRAWING
		// ----------------------------------------------------------------------
		public ImageFormat TargetFormat
		{
			get { return targetFormat; }
		} // TargetFormat
#endif

		// ----------------------------------------------------------------------
		public double DpiX
		{
			get { return dpiX; }
		} // DpiX

		// ----------------------------------------------------------------------
		public double DpiY
		{
			get { return dpiY; }
		} // DpiY

#if DRAWING
    // ----------------------------------------------------------------------
    public ImageFormat GetImageFormat( RtfVisualImageFormat rtfVisualImageFormat )
		{
			ImageFormat imageFormat = null;

			switch ( rtfVisualImageFormat )
			{
				case RtfVisualImageFormat.Emf:
					imageFormat = ImageFormat.Emf;
					break;
				case RtfVisualImageFormat.Png:
					imageFormat = ImageFormat.Png;
					break;
				case RtfVisualImageFormat.Jpg:
					imageFormat = ImageFormat.Jpeg;
					break;
				case RtfVisualImageFormat.Wmf:
					imageFormat = ImageFormat.Wmf;
					break;
				case RtfVisualImageFormat.Bmp:
					imageFormat = ImageFormat.Bmp;
					break;
			}

			return imageFormat;
		} // GetImageFormat
#endif

		// ----------------------------------------------------------------------
		public string ResolveFileName( int index, RtfVisualImageFormat rtfVisualImageFormat )
		{
#if DRAWING
      ImageFormat imageFormat = targetFormat ?? GetImageFormat( rtfVisualImageFormat );

			return string.Format(
				CultureInfo.InvariantCulture,
				fileNamePattern,
				index,
				GetFileImageExtension( imageFormat ) );
#else
      return string.Format(
        CultureInfo.InvariantCulture,
        fileNamePattern,
        index,
        rtfVisualImageFormat);
#endif
    } // ResolveFileName

		// ----------------------------------------------------------------------
		public int CalcImageWidth( RtfVisualImageFormat format, int width,
			int desiredWidth, int scaleWidthPercent )
		{
			float imgScaleX = scaleWidthPercent / 100.0f;
			return (int)Math.Round( (double)desiredWidth * imgScaleX / twipsPerInch * dpiX );
		} // CalcImageWidth

		// ----------------------------------------------------------------------
		public int CalcImageHeight( RtfVisualImageFormat format, int height,
			int desiredHeight, int scaleHeightPercent )
		{
			float imgScaleY = scaleHeightPercent / 100.0f;
			return (int)Math.Round( (double)desiredHeight * imgScaleY / twipsPerInch * dpiY );
		} // CalcImageHeight

#if DRAWING
    // ----------------------------------------------------------------------
    private static string GetFileImageExtension( ImageFormat imageFormat )
		{
			string imageExtension = null;

			if ( imageFormat == ImageFormat.Bmp )
			{
				imageExtension = ".bmp";
			}
			else if ( imageFormat == ImageFormat.Emf )
			{
				imageExtension = ".emf";
			}
			else if ( imageFormat == ImageFormat.Exif )
			{
				imageExtension = ".exif";
			}
			else if ( imageFormat == ImageFormat.Gif )
			{
				imageExtension = ".gif";
			}
			else if ( imageFormat == ImageFormat.Icon )
			{
				imageExtension = ".ico";
			}
			else if ( imageFormat == ImageFormat.Jpeg )
			{
				imageExtension = ".jpg";
			}
			else if ( imageFormat == ImageFormat.Png )
			{
				imageExtension = ".png";
			}
			else if ( imageFormat == ImageFormat.Tiff )
			{
				imageExtension = ".tiff";
			}
			else if ( imageFormat == ImageFormat.Wmf )
			{
				imageExtension = ".wmf";
			}

			return imageExtension;
		} // GetFileImageExtension
		private readonly ImageFormat targetFormat;
#endif

    // ----------------------------------------------------------------------
    // members
    private readonly string fileNamePattern;
		private readonly double dpiX;
		private readonly double dpiY;

		private const string defaultFileNamePattern = "{0}{1}";
		private const int twipsPerInch = 1440;

	} // class RtfVisualImageAdapter

} // namespace RtfPipe.Converter.Image
// -- EOF -------------------------------------------------------------------
