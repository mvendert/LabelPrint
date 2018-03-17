/*
  2011 - This file is part of AcaLabelPrint 

  AcaLabelPrint is free Software: you can redistribute it and/or modify
  it under the terms of the GNU General Public License as published by
  the Free Software Foundation, either version 3 of the License, or
  (at your option) any later version.

  AcaLabelprint is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
  GNU General Public License for more details.

  You should have received a copy of the GNU General Public License
  along with AcaLabelPrint.  If not, see <http:www.gnu.org/licenses/>.

  We encourage you to use and extend the functionality of AcaLabelPrint,
  and send us an e-mail on the outlines of the extension you build. If
  it's generic, maybe we could add it to the project.
  Send your mail to the projectadmin at http:sourceforge.net/projects/labelprint/
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Globalization;
using System.Drawing.Printing;
using System.Runtime.InteropServices;

namespace ACA.LabelX.Tools
{
    public class Length
    {
        public float length = 0;
        public Tools.CoordinateSystem.Units units = null;
        public float DPI = 0;

        public Length(Tools.CoordinateSystem.Units Units)
            :this(0, Units)
        {
        }

        public Length(float Length, Tools.CoordinateSystem.Units Units)
        {
            this.length = Length;
            this.units = Units;
        }

        public Length(float Length, System.Drawing.GraphicsUnit Units)
        {
            this.length = Length;
            this.units = new CoordinateSystem.Units(Units);
        }


        public Length(Length newLength)
        {
            this.length = newLength.length;
            this.units = new CoordinateSystem.Units(newLength.units.UnitType);
        }

        public int InPixels(float DPI)
        {
            return (int)(InInch() * DPI);
        }

        public float InMM()
        {
            if (units.UnitType == System.Drawing.GraphicsUnit.Millimeter)
                return length;
            else if (units.UnitType == System.Drawing.GraphicsUnit.Inch)
                return length * (float)25.4;
            else if (units.UnitType == System.Drawing.GraphicsUnit.Pixel)
            {
                if (DPI == 0)
                {
                    throw new ApplicationException("Not supported when DPI not set.");
                }
                else
                {
                    float inch;
                    inch = length / DPI;
                    return inch * (float)25.4;
                }
            }
            return -1;
        }

        public float InInch()
        {
           
            if (units.UnitType == System.Drawing.GraphicsUnit.Millimeter)
                return length / (float)25.4;
            else if (units.UnitType == System.Drawing.GraphicsUnit.Inch)
                return length;
            else if (units.UnitType == System.Drawing.GraphicsUnit.Pixel)
            {
                throw new ApplicationException("Not supported.");
                //return -1;
            }
             
            return -1;
        }

        public static Length operator +(Length l1, Length l2)
        {
            Length length = new Length(new CoordinateSystem.Units(System.Drawing.GraphicsUnit.Millimeter));
            length.length = l1.InMM() + l2.InMM();
            return length;
        }

        public static Length operator - (Length l1, Length l2)
        {
            Length length = new Length(new CoordinateSystem.Units(System.Drawing.GraphicsUnit.Millimeter));
            length.length = l1.InMM() - l2.InMM();
            return length;
        }

        public static bool operator >(Length l1, Length l2)
        {
            return l1.InMM() > l2.InMM();
        }
        public static bool operator <(Length l1, Length l2)
        {
            return l1.InMM() < l2.InMM();
        }

        public static Length operator * (Length l1, Length l2)
        {
            Length length = new Length(new CoordinateSystem.Units(System.Drawing.GraphicsUnit.Millimeter));
            length.length = l1.InMM() * l2.InMM();
            return length;
        }

        public static Length operator /(Length l1, Length l2)
        {
            Length length = new Length(new CoordinateSystem.Units(System.Drawing.GraphicsUnit.Millimeter));
            length.length = l1.InMM() / l2.InMM();
            return length;
        }

        public static Length operator /(Length l1, int div)
        {
            Length length = new Length(l1);
            length.length = length.length / div;

            //Length length = new Length(new CoordinateSystem.Units(l1.units.UnitType));
            //length.length = l1.length / div;
            return length;
        }

        public static Length operator *(Length l1, int div)
        {
            Length length = new Length(l1);
            length.length = length.length * div ;
            return length;
        }

        public void Parse(XmlNode node)
        {
            length = (float)Convert.ToDouble(node.InnerText,CultureInfo.InvariantCulture);
            foreach (XmlAttribute attrib in node.Attributes)
            {
                if (attrib.Name.Equals("units", StringComparison.OrdinalIgnoreCase))
                {
                    units = new Tools.CoordinateSystem.Units(System.Drawing.GraphicsUnit.Millimeter);
                    units.Parse(attrib.Value);
                }
            }
        }
    }

    public class Size
    {
        public Length Width = null;
        public Length Height = null;

        public Tools.CoordinateSystem.Units DefaultUnits = null;

        public Size(float Width, float Height, Tools.CoordinateSystem.Units DefaultUnits)
            : this(new Length(Width, DefaultUnits), new Length(Height, DefaultUnits))
        {
        }

        public Size(float Width, float Height, System.Drawing.GraphicsUnit GraphicsUnit)
        {
            this.Width = new Length(Width, new Tools.CoordinateSystem.Units(GraphicsUnit));
            this.Height = new Length(Height, new Tools.CoordinateSystem.Units(GraphicsUnit));
        }

        public Size(Length Width, Length Height)
        {
            this.Width = new Length(Width);
            this.Height = new Length(Height);
        }

        public Size(Size oldSize)
        {
            this.Width = oldSize.Width;
            this.Height = oldSize.Height;
            this.DefaultUnits = oldSize.DefaultUnits;
        }

        public void Parse(XmlNode node)
        {
            Width = null;
            Height = null;

            foreach (XmlNode nodexxx in node.ChildNodes)
            {
                if (nodexxx.Name.Equals("width", StringComparison.OrdinalIgnoreCase))
                {
                    Width = new Length(DefaultUnits);
                    Width.Parse(nodexxx);
                }
                else if (nodexxx.Name.Equals("height", StringComparison.OrdinalIgnoreCase))
                {
                    Height = new Length(DefaultUnits);
                    Height.Parse(nodexxx);
                }
            }
        }
    }

    public class Point
    {
        public Length Left = null;
        public Length Top = null;
    }

    public class Rectangle
    {
        public Length Left = null;
        public Length Top = null;
        public Size Size = null;

        public Rectangle(float Left, float Top, float Width, float Height, Tools.CoordinateSystem.Units Units)
            : this(new Length(Left, Units), new Length(Top, Units), new Size(Width, Height, Units))
        {
        }

        public Rectangle(Length Left, Length Top, Size LabelSize)
        {
            this.Left = new Length(Left);
            this.Top = new Length(Top);
            this.Size = new Size(LabelSize);
        }

/*        public System.Drawing.Rectangle AsDrawingRectangle ()
        {
            return new System.Drawing.Rectangle(this.Left.GetLengthInMM(), this.Top.GetLengthInMM, this.Size.Width.GetLengthInMM(), this.Size.Height.GetLengthInMM());
        }
        */
    }

    public class CoordinateSystem
    {
        public DPIFactor dpiFactor = new DPIFactor();
        public Units units = null;

        public CoordinateSystem()
        {
            this.units = new Units(System.Drawing.GraphicsUnit.Millimeter);
        }

        public CoordinateSystem(uint DPIFactorX, uint DPIFactorY, string unit)
        {
            dpiFactor.X = DPIFactorX;
            dpiFactor.Y = DPIFactorY;
            if (unit.Equals("MILLIMETER", StringComparison.OrdinalIgnoreCase))
                this.units = new Units(System.Drawing.GraphicsUnit.Millimeter);
            else if (unit.Equals("INCH", StringComparison.OrdinalIgnoreCase))
                this.units = new Units(System.Drawing.GraphicsUnit.Inch);
            else if (unit.Equals("Millimeter", StringComparison.OrdinalIgnoreCase))
                this.units = new Units(System.Drawing.GraphicsUnit.Millimeter);
        }


        public class DPIFactor
        {
            public UInt32 X = 0;
            public UInt32 Y = 0;

            public DPIFactor(){}
            public DPIFactor(int x, int y)
            {
                this.X = (uint)x;
                this.Y = (uint)y;
            }

            public void Parse(XmlNode node)
            {
                foreach (XmlAttribute attrib in node.Attributes)
                {
                    if (attrib.Name.Equals("x", StringComparison.OrdinalIgnoreCase))
                    {
                        X = Convert.ToUInt32(attrib.Value);
                    }
                    else if (attrib.Name.Equals("y", StringComparison.OrdinalIgnoreCase))
                    {
                        Y = Convert.ToUInt32(attrib.Value);
                    }
                }
            }
        }
        public class Units
        {
            public System.Drawing.GraphicsUnit UnitType = System.Drawing.GraphicsUnit.Millimeter;

            public Units(System.Drawing.GraphicsUnit UnitType)
            {
                this.UnitType = UnitType;
            }

            public void Parse(String UnitsAsString)
            {
                if (UnitsAsString.Trim().Equals("MILLIMETER", StringComparison.OrdinalIgnoreCase))
                    UnitType = System.Drawing.GraphicsUnit.Millimeter;
                else if (UnitsAsString.Trim().Equals("PIXELS", StringComparison.OrdinalIgnoreCase))
                    throw new ApplicationException("Not allowed");
                else if (UnitsAsString.Trim().Equals("INCH", StringComparison.OrdinalIgnoreCase))
                    UnitType = System.Drawing.GraphicsUnit.Inch;
            }

            public void Parse(XmlNode node)
            {
                this.Parse(node.InnerText);
            }
        }

        public void Parse(XmlNode node)
        {
            foreach (XmlNode nodexxx in node.ChildNodes)
            {
                if (nodexxx.Name.Equals("dpifactor", StringComparison.OrdinalIgnoreCase))
                {
                    dpiFactor.Parse(nodexxx);
                }
                else if (nodexxx.Name.Equals("units", StringComparison.OrdinalIgnoreCase))
                {
                    units.Parse(nodexxx);
                }
            }
        }
    }

    /// <summary>
    /// Simple form of CultureInfo, for use in this
    /// label printing Software.
    /// </summary>
    /// <remarks>
    /// Intended usage:
    ///     Create instance
    ///     Set UseSystemCultureInfo somewhere
    ///     Use proterties to retrieve string values
    /// </remarks>
    /// <seealso cref="CultureInfo"/>
    public class CultureInfoSimple
    {
        private static bool Initialized;

        public bool UseSystemCultureInfo;
        private string currencySymbol;
        private string decimalPointSeperator;
        private string dateFieldSeperator;

        public string CurrencySymbol
        {
            get 
            {
                if (!Initialized)
                    Initialize();
                return currencySymbol; 
            }
            set { currencySymbol = value; }
        }
        public string DecimalPointSeperator
        {
            get 
            {
                if (!Initialized)
                    Initialize();
                return decimalPointSeperator; 
            }
            set { decimalPointSeperator = value; }
        }
        public string DateFieldSeperator
        {
            get 
            {
                if (!Initialized)
                    Initialize();
            
                return dateFieldSeperator; 
            }
            set { dateFieldSeperator = value; }
        }

        public void Initialize()
        {
            if (UseSystemCultureInfo)
            {
                LoadFromSystem();
            }
        }

        protected void LoadFromSystem()
        {
            CultureInfo ci;
            ci = CultureInfo.CurrentCulture;

            dateFieldSeperator = ci.DateTimeFormat.DateSeparator;
            currencySymbol = ci.NumberFormat.CurrencySymbol;
            decimalPointSeperator = ci.NumberFormat.NumberDecimalSeparator;
        }

        CultureInfoSimple()
        {
            Initialized = false;
        }

        CultureInfoSimple(bool useSystemCulture)
        {
            Initialized = false;
            UseSystemCultureInfo = useSystemCulture;
        }
    }

    /// <summary>
    /// The C# classes do never give out the real physical margings
    /// of the printer. If we want to be able to let them be entered
    /// we have to subtract this from the user given data.
    /// </summary>
    public class PrinterBounds
    {
        [DllImport("gdi32.dll")]
        private static extern Int32 GetDeviceCaps(IntPtr hdc, Int32 capindex);

        //Magical constants for the GetDeviceCaps function.
        private const int PHYSICALOFFSETX = 112;
        private const int PHYSICALOFFSETY = 113;

        public readonly System.Drawing.Rectangle Bounds;
        public readonly Length HardMarginLeft;
        public readonly Length HardMarginTop;
        
        public PrinterBounds(PrintPageEventArgs e)
        {
            int iHardMarginLeft;
            int iHardMarginTop;
           
            IntPtr hDC = e.Graphics.GetHdc();
            iHardMarginLeft = GetDeviceCaps(hDC, PHYSICALOFFSETX);
            iHardMarginTop = GetDeviceCaps(hDC, PHYSICALOFFSETY);
           
            e.Graphics.ReleaseHdc(hDC);

            HardMarginLeft = new Length(iHardMarginLeft, new CoordinateSystem.Units(System.Drawing.GraphicsUnit.Pixel));
            HardMarginTop = new Length(iHardMarginTop,new CoordinateSystem.Units(System.Drawing.GraphicsUnit.Pixel));
            //float test = HardMarginLeft.InInch();
            Bounds = e.MarginBounds;
            Bounds.Offset(-(int)(iHardMarginLeft * 100.0 / e.Graphics.DpiX), -(int)(iHardMarginTop * 100.0 / e.Graphics.DpiY));
            System.Diagnostics.Debug.WriteLine("MarginBounds X: " + Bounds.X + " Y: "  + Bounds.Y + " W: " + Bounds.Width + " H: " + Bounds.Height);
            HardMarginLeft.DPI = e.Graphics.DpiX;
            HardMarginTop.DPI = e.Graphics.DpiY;
        }
    }

}
/*
 AcaLabelPrint Copyright (C) 2011  Retailium Software Development BV.
 This program comes with ABSOLUTELY NO WARRANTY; 
 This is free Software, and you are welcome to redistribute it
 under certain conditions. See the License.txt file or 
 GNU GPL 3.0 License at <http://www.gnu.org/licenses>*/
