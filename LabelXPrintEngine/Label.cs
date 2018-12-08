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
using System.IO;
using System.Reflection;
using System.Drawing;
using System.Xml;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Globalization;

namespace ACA.LabelX.Label
{
    
    public class Label
    {
        public class LabelCultureInfo
        {
            private string dateSeperator;

            public string DateSeperator
            {
                get
                {
                    if (!UseSystemCultureInfo)
                    {
                        return dateSeperator;
                    }
                    else
                    {
                        return CultureInfo.CurrentCulture.DateTimeFormat.DateSeparator.ToString();
                    }
                }
                set { dateSeperator = value; }
            }
            private string decimalpoint;

            public string Decimalpoint
            {
                get
                {
                    if (!UseSystemCultureInfo)
                    {
                        return decimalpoint;
                    }
                    else
                    {
                        decimalpoint = CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;
                        return CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;
                    }
                }
                set { decimalpoint = value; }
            }
            private string currencySymbol;

            public string CurrencySymbol
            {
                get
                {
                    if (!UseSystemCultureInfo)
                    {
                        return currencySymbol;
                    }
                    else
                    {
                        return CultureInfo.CurrentCulture.NumberFormat.CurrencySymbol;
                    }
                }
                set { currencySymbol = value; }
            }
            public string Language;
            public bool UseSystemCultureInfo;

            public LabelCultureInfo()
            {
                UseSystemCultureInfo = true; //overrules ALL!!!
                DateSeperator = "-";
                Decimalpoint = ",";
                CurrencySymbol = "�";
                Language = "ALL";
            }
            public void Parse(XmlNode node)
            {
                foreach (XmlAttribute attrib in node.Attributes)
                {
                    if (attrib.Name.Equals("dateseperator", StringComparison.OrdinalIgnoreCase))
                    {
                        DateSeperator = attrib.Value;
                    }
                    else if (attrib.Name.Equals("decimalpoint", StringComparison.OrdinalIgnoreCase))
                    {
                        Decimalpoint = attrib.Value;
                    }
                    else if (attrib.Name.Equals("currencysymbol", StringComparison.OrdinalIgnoreCase))
                    {
                        CurrencySymbol = attrib.Value;
                    }
                    else if (attrib.Name.Equals("language", StringComparison.OrdinalIgnoreCase))
                    {
                        Language = attrib.Value;
                        if (Language.Equals("system", StringComparison.OrdinalIgnoreCase))
                        {
                            UseSystemCultureInfo = true;
                        }
                        else
                        {
                            UseSystemCultureInfo = false;
                        }
                    }
                }
            }
        }
        public class LabelCultureInfos : Dictionary<string, LabelCultureInfo>
        {
            public void Parse(XmlNode node)
            {
                foreach (XmlNode nodex in node.ChildNodes)
                {
                    if (nodex.Name.Equals("cultureinfo", StringComparison.OrdinalIgnoreCase))
                    {
                        LabelCultureInfo theLabel;
                        theLabel = new LabelCultureInfo();
                        theLabel.Parse(nodex);
                        this.Add(theLabel.Language.ToString(), theLabel);
                    }
                }
            }

        }
        public class Value
        {
            public String Key = "";
            public String Type = "";
            public Int32 Language = 1043;//ACALabelPrint TODO
            public String Data = "";

            public Value()
            {
            }

            public Value(string sData)
            {
                Data = sData;
            }

            public void Parse(XmlNode node)
            {
                Key = "";
                Type = "";
                Language = 1043;
                Data = "";

                if (!node.Name.Equals("value", StringComparison.OrdinalIgnoreCase))
                    throw new ApplicationException("Not a values node");

                Data = node.InnerText;

                foreach (XmlAttribute attrib in node.Attributes)
                {
                    if (attrib.Name.Equals("key", StringComparison.OrdinalIgnoreCase))
                    {
                        Key = attrib.Value;
                        //Fix, Mve, Keynames worden aan elkaar geplakt met een . in parse van values in deze file
                        // zoek maar eens op "." : Values.Add(val.Key + "." + val.Language.ToString(), val);
                        // Als een key dus een . bevat moet dit eruit
                        Key = Key.Replace('.', '_');
                        // *
                    }
                    else if (attrib.Name.Equals("type", StringComparison.OrdinalIgnoreCase))
                    {
                        Type = attrib.Value;
                    }
                    else if (attrib.Name.Equals("language", StringComparison.OrdinalIgnoreCase))
                    {
                        Language = Convert.ToInt32(attrib.Value);
                    }
                    else if (attrib.Name.Equals("contents", StringComparison.OrdinalIgnoreCase))
                    {
                        if (Data.Equals(""))
                        {
                            Data = attrib.Value;
                        }
                    }
                }                
            }
        }

        public enum LabelType {DefaultLabel, Print};

        public LabelType Type = LabelType.Print;
        public UInt32 Quantity = 1;
        public IDictionary<string, Value> Values = new Dictionary<string, Value>();
        
        public LabelCultureInfos LblCultureInfos; 

        public Label()
        {
            LblCultureInfos = new LabelCultureInfos();
            //AddDefaultValues();
        }

        public void AddDefaultValues()
        {
            Values.Add("lbl_now",new Value(DateTime.Now.ToString(CultureInfo.InvariantCulture)));
            Values.Add("lbl_pcname",new Value(System.Windows.Forms.Application.CompanyName));
            Values.Add("lbl_sysculture",new Value(System.Windows.Forms.Application.CurrentCulture.EnglishName));
            Values.Add("lbl_product",new Value(System.Windows.Forms.Application.ProductName));
            Values.Add("lbl_version",new Value(System.Windows.Forms.Application.ProductVersion.ToString()));
            Values.Add("lbl_credits", new Value("Jamie Bosmans & Marc Van Endert & Peter Smolders"));
        }

        public void Draw(Graphics Graph, Rectangle Rect, LabelDef LabelDefinition, LabelSet Labels, int Language, bool DrawBorders)
        {            
            Rectangle LargerRect;
            Pen purplePen = new Pen(Color.Purple);
            LargerRect = new Rectangle(Rect.X - 1, Rect.Y - 1, Rect.Width + 2, Rect.Height + 2);
            System.Diagnostics.Debug.WriteLine("Label drawn at: X: " + Rect.X + " Y: " + Rect.Y + " W: " + Rect.Width + " H: " + Rect.Height + " DPI: " + Graph.DpiX);
            Graph.SetClip(LargerRect);
            
            if (DrawBorders)
                Graph.DrawRectangle(purplePen, Rect);
            
            Graph.SetClip(Rect);
            LabelDefinition.Draw(Graph, Rect, Labels, Language,false);

            purplePen.Dispose();
            
        }

        public void Parse(XmlNode node)
        {
            Type = LabelType.Print;
            Quantity = 1;

            foreach (XmlAttribute attrib in node.Attributes)
            {
                if (attrib.Name.Equals("type", StringComparison.OrdinalIgnoreCase))
                {
                    if (attrib.Value.Equals("default", StringComparison.OrdinalIgnoreCase))
                    {
                        Type = LabelType.DefaultLabel;
                    }
                    else if (attrib.Value.Equals("print", StringComparison.OrdinalIgnoreCase))
                    {
                        Type = LabelType.Print;
                    }
                }
            }

            foreach (XmlNode nodex in node.ChildNodes)
            {
                if (nodex.Name.Equals("options", StringComparison.OrdinalIgnoreCase))
                {
                    foreach (XmlNode nodexx in nodex.ChildNodes)
                    {
                        if (nodexx.Name.Equals("quantity", StringComparison.OrdinalIgnoreCase))
                        {
                            Quantity = Convert.ToUInt32(nodexx.InnerText);
                        }
                        else if (nodexx.Name.Equals("cultureinfos", StringComparison.OrdinalIgnoreCase))
                        {
                            LblCultureInfos.Parse(nodexx);
                        }
                    }
                } else if (nodex.Name.Equals("values", StringComparison.OrdinalIgnoreCase))
                {
                    Values.Clear();
                    foreach (XmlNode nodexx in nodex.ChildNodes)
                    {
                        if (nodexx.Name.Equals("value", StringComparison.OrdinalIgnoreCase))
                        {
                            Value val = new Value();
                            val.Parse(nodexx);
                            try
                            {
                                Values.Add(val.Key + "." + val.Language.ToString(), val);
                            }
                            catch (Exception ex)
                            {
                                GlobalDataStore.Logger.Info(string.Format("{0} : {1}",val.Key + "." + val.Language.ToString(),ex.Message));
                                System.Diagnostics.Debug.WriteLine(ex.Message);
                            }
                        }
                    }
                }
            }
        }
    }

    public class LabelSet
    {
        public Label CurrentLabel;
        public Label DefaultLabel;
        public Label BaseLabel;
        public Label StaticVarsLabel;

        public Label.Value TryGetValue(string sFieldId, int language)
        {
            
            Label.Value value;
            value = null;

            if (sFieldId.Equals("lbl_syscurrency"))
            {
                CultureInfo ci;
                ci = CultureInfo.CurrentCulture;
                value = new Label.Value(ci.NumberFormat.CurrencySymbol);
                return value;
            }

            if (!CurrentLabel.Values.TryGetValue(string.Format("{0}.{1}", sFieldId, language), out value))
                if (!DefaultLabel.Values.TryGetValue(string.Format("{0}.{1}", sFieldId, language), out value))
                    if (!BaseLabel.Values.TryGetValue(string.Format("{0}.{1}", sFieldId, language), out value))
                        StaticVarsLabel.Values.TryGetValue(string.Format("{0}.{1}",sFieldId, language), out value);
            return value;
        }

        internal ACA.LabelX.Label.Label.LabelCultureInfo GetCiForLanguage(int language)
        {
            ACA.LabelX.Label.Label.LabelCultureInfo val;
            val = null;
            if (!CurrentLabel.LblCultureInfos.TryGetValue(language.ToString(), out val))
            {
                if (!DefaultLabel.LblCultureInfos.TryGetValue(language.ToString(), out val))
                {
                    if (!BaseLabel.LblCultureInfos.TryGetValue(language.ToString(), out val))
                        val = null;
                }
            }
            return val;
        }

        public string DecimalPoint(int language)
        {   
            ACA.LabelX.Label.Label.LabelCultureInfo val;
            val = GetCiForLanguage(language);
            if (val != null)
            {
                return val.Decimalpoint;
            }
            else
            {
                return ",";
            }
        }

        public string DateSeperator(int language)
        {
            ACA.LabelX.Label.Label.LabelCultureInfo val;
            val = GetCiForLanguage(language);
            if (val != null)
            {
                return val.DateSeperator;
            }
            else
            {
                return "-";
            }
        }

        public string CurrencySymbol(int language)
        {
            ACA.LabelX.Label.Label.LabelCultureInfo val;
            val = GetCiForLanguage(language);
            if (val != null)
            {
                return val.CurrencySymbol;
            }
            else
            {
                return "�";
            }
        }
    }

    public class LabelDef
    {        
        public class FontX : IDisposable
        {
            public String ID;
            public Boolean InversePrint = false;
            public Font Font = null;
            public FontStyle Style;
            private bool disposed = false;

            public FontX()
            {
                Style = FontStyle.Regular;
            }

            public FontX(string ID, Boolean inversePrint, Font font, FontStyle fontStyle)
            {
                this.ID = ID;
                this.InversePrint = inversePrint;
                this.Font = font;
                this.Style = fontStyle;
            }                        

            public void Parse(XmlNode node)
            {
                InversePrint = false;

                foreach (XmlAttribute attrib in node.Attributes)
                {
                    if (attrib.Name.Equals("id", StringComparison.OrdinalIgnoreCase))
                    {
                        ID = attrib.Value;
                    }
                }

                String TypeFace = null;
                double Size = 0;

                foreach (XmlNode nodex in node.ChildNodes)
                {
                    if (nodex.Name.Equals("typeface", StringComparison.OrdinalIgnoreCase))
                    {
                        TypeFace = nodex.InnerText;
                    }
                    else if (nodex.Name.Equals("size", StringComparison.OrdinalIgnoreCase))
                    {
                        Size = Convert.ToDouble(nodex.InnerText, CultureInfo.InvariantCulture);
                    }
                    else if (nodex.Name.Equals("inverseprint", StringComparison.OrdinalIgnoreCase))
                    {
                        InversePrint = nodex.InnerText.Trim().Equals("true", StringComparison.OrdinalIgnoreCase);
                    } else if (nodex.Name.Equals("style",StringComparison.OrdinalIgnoreCase))
                    {
                        string stylestring;
                        stylestring = nodex.InnerText.Trim();
                        if (stylestring.Contains("bold"))
                        {
                            Style |=  FontStyle.Bold;
                        }
                        if (stylestring.Contains("italic"))
                        {
                            Style |= FontStyle.Italic;
                        }
                         if (stylestring.Contains("strikeout"))
                        {
                            Style |= FontStyle.Strikeout;
                        }
                        if (stylestring.Contains("underline"))
                        {
                            Style |= FontStyle.Underline;
                        }
                    }

                }
                if (this.Font != null)
                {
                    this.Font.Dispose();
                }
                this.Font = new Font(TypeFace, (float)Size,Style);               
            }
            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);                                
            }
            protected virtual void Dispose(bool disposing)
            {
                if (!disposed)
                {
                    if (disposing)
                    {
                        Font.Dispose();
                    }
                    disposed = true;
                }
            }
            ~FontX()
            {
                Dispose(false);
            }
        }

        public class PaperType
        {
            public String ID;
            public Boolean Default;

            public PaperType() { }
            public PaperType(string ID,bool IsDefault)
            {
                this.ID = ID;
                Default = IsDefault;
            }

            public void Parse(XmlNode node)
            {
                foreach (XmlAttribute attrib in node.Attributes)
                {
                    if (attrib.Name.Equals("type", StringComparison.OrdinalIgnoreCase))
                    {
                        ID = attrib.Value;
                    } else if (attrib.Name.Equals("default", StringComparison.OrdinalIgnoreCase))
                    {
                        Default = attrib.Value.Equals("true", StringComparison.OrdinalIgnoreCase);
                    }
                }
            }
        }

        public class FieldFormat
        {
            public enum Alignment { Left, Center, Right }
            public Alignment Align = Alignment.Left;
            public String FormatString;

            public StringAlignment GetAsStringAlignment()
            {
                switch (Align)
                {
                    case Alignment.Left: return StringAlignment.Near;
                    case Alignment.Center: return StringAlignment.Center;
                    case Alignment.Right: return StringAlignment.Far;
                }
                return StringAlignment.Near;
            }

            public virtual void Parse(XmlNode node)
            {
                Align = Alignment.Left;
                foreach (XmlAttribute attrib in node.Attributes)
                {
                    if (attrib.Name.Equals("align", StringComparison.OrdinalIgnoreCase))
                    {
                        if (attrib.Value.Equals("right", StringComparison.OrdinalIgnoreCase))
                            Align = Alignment.Right;
                        else if (attrib.Value.Equals("center", StringComparison.OrdinalIgnoreCase))
                            Align = Alignment.Center;
                        else if (attrib.Value.Equals("left", StringComparison.OrdinalIgnoreCase))
                            Align = Alignment.Left;
                    } 
                    else if (attrib.Name.Equals("format",StringComparison.OrdinalIgnoreCase))
                    {
                        FormatString = attrib.Value;
                    }
                }
            }
            public virtual string Format(string sInput, LabelSet Labels, int language)
            {
                return sInput;
            }
        }

        public class StringFormat : FieldFormat
        {
            public bool IsBarcode = false;

            public StringFormat(){}
            public StringFormat(bool isBarCode)
            {
                this.IsBarcode = isBarCode;
            }


            public override void Parse(XmlNode node)
            {
                base.Parse(node);

                foreach (XmlAttribute attrib in node.Attributes)
                {
                    if (attrib.Name.Equals("isbarcode", StringComparison.OrdinalIgnoreCase))
                    {
                        IsBarcode = attrib.Value.Equals("true", StringComparison.OrdinalIgnoreCase);
                    }
                }
            }
        }

        public class DateTimeFormat : FieldFormat
        {
            public String format = "dd/MM/yyyy";

            public override void Parse(XmlNode node)
            {
                base.Parse(node);

                foreach (XmlAttribute attrib in node.Attributes)
                {
                    if (attrib.Name.Equals("dateformat", StringComparison.OrdinalIgnoreCase))
                    {
                        this.format = attrib.Value;
                    }
                }
            }
            public override string Format(string sInput, LabelSet Labels, int language)
            {
                DateTime theDate;
                CultureInfo ci;
                string sOutput = string.Empty;
                ci = CultureInfo.InvariantCulture;
                // input should always be : yyyy/mm/dd
                if (DateTime.TryParse(sInput, ci, DateTimeStyles.AssumeLocal, out theDate))
                {
                    if (FormatString != null && FormatString.Length > 0)
                    {
                        sOutput = theDate.ToString(FormatString);
                    }
                    else
                    {
                        sOutput = theDate.ToString(format);
                        sOutput = sOutput.Replace("/", Labels.DateSeperator(language));
                    }
                }
                else
                {
                    sOutput = "Format Error";
                }
                return sOutput;
            }
        }

        public class DecimalFormat : FieldFormat
        {
            public enum DecimalPortion { Entire, Integer, Fraction }

            public DecimalPortion Portion = DecimalPortion.Entire;

            public override void Parse(XmlNode node)
            {
                base.Parse(node);
                this.Portion = DecimalPortion.Entire;

                foreach (XmlAttribute attrib in node.Attributes)
                {
                    if (attrib.Name.Equals("macro", StringComparison.OrdinalIgnoreCase))
                    {
                        if (attrib.Value.Equals("onlyfraction", StringComparison.OrdinalIgnoreCase))
                            this.Portion = DecimalPortion.Fraction;
                        else if (attrib.Value.Equals("onlywhole", StringComparison.OrdinalIgnoreCase))
                            this.Portion = DecimalPortion.Integer;
                        else
                            this.Portion = DecimalPortion.Entire;
                    }
                }
            }

            public override string Format(string sInput, LabelSet labels, int language)
            {                
                CultureInfo info = CultureInfo.InvariantCulture;
                string sDecimalPoint = labels.DecimalPoint(language);
                string sThousandSeperator;
                if (sDecimalPoint.Equals("."))
                    sThousandSeperator = ",";
                else
                    sThousandSeperator = ".";
                Decimal getal = Decimal.Parse(sInput, info.NumberFormat);
                string sHelp = "";
                switch (Portion)
                {
                    case DecimalPortion.Entire:
                        {
                            if (String.IsNullOrEmpty(FormatString))
                            {
                                sHelp = getal.ToString(CultureInfo.InvariantCulture);
                            }
                            else
                            {
                                sHelp = getal.ToString(FormatString, CultureInfo.InvariantCulture);
                            }
                            break;
                        }
                    case DecimalPortion.Integer:
                        {                                                        
                            int getal2 = (int)Decimal.Truncate(getal);
                            if (string.IsNullOrEmpty(FormatString))
                                sHelp = getal2.ToString(CultureInfo.InvariantCulture);                            
                            else
                                sHelp = getal2.ToString(FormatString, CultureInfo.InvariantCulture);
                            break;
                        }
                    case DecimalPortion.Fraction:
                        {                                                        
                            Decimal getal2 = Decimal.Truncate(getal);
                            getal -= getal2;
                            if (getal < 0) getal *= -1;

                            if ((FormatString == string.Empty) | (FormatString == null))
                                sHelp = getal.ToString(".0",CultureInfo.InvariantCulture);
                            else
                                sHelp = getal.ToString(FormatString, CultureInfo.InvariantCulture);

                            try
                            {
                                if (sHelp.StartsWith("."))
                                    sHelp = sHelp.Substring(1);
                            }
                            catch { }
                            break;
                        }                    
                }
                string InvariantSeperator = CultureInfo.InstalledUICulture.NumberFormat.NumberDecimalSeparator;
                if (sDecimalPoint.Equals(",")){
                    sHelp = sHelp.Replace(",","-");
                    sHelp = sHelp.Replace(".", sDecimalPoint);
                    sHelp = sHelp.Replace("-", sThousandSeperator);
                }
                               
                return sHelp;
            }            
        }

        public class PrintFormat 
        {
            public FieldFormat format = null;

            public PrintFormat() { }
            public PrintFormat(FieldFormat fieldFormat)
            {
                this.format = fieldFormat;
            }
            public void Parse(XmlNode node)
            {
                format = null;
                foreach (XmlNode nodex in node.ChildNodes)
                {
                    if (nodex.Name.Equals("format", StringComparison.OrdinalIgnoreCase))
                    {
                        foreach (XmlAttribute attrib in nodex.Attributes)
                        {
                            if (attrib.Name.Equals("type", StringComparison.OrdinalIgnoreCase))
                            {
                                if (attrib.Value.Equals("xs:string", StringComparison.OrdinalIgnoreCase))
                                {
                                    format = new StringFormat();
                                    format.Parse(nodex);
                                }
                                else if (attrib.Value.Equals("xs:decimal", StringComparison.OrdinalIgnoreCase))
                                {
                                    format = new DecimalFormat();
                                    format.Parse(nodex);
                                }
                                else if (attrib.Value.Equals("xs:datetime", StringComparison.OrdinalIgnoreCase))
                                {
                                    format = new DateTimeFormat();
                                    format.Parse(nodex);
                                }
                            }                            
                        }
                    }
                }
            }
        }

        public abstract class Field
        {
            public String ID = "";
            public string ValueRef = null;
            public Tools.CoordinateSystem coordinateSystem = null;
            public Tools.Length PositionX = null;
            public Tools.Length PositionY = null;
            public Tools.Length Width = null;
            public Tools.Length Height = null;
            public float Rotation = 0;
            protected System.Drawing.Drawing2D.Matrix baseTransformation;

            public System.Drawing.Drawing2D.Matrix BaseTransformation
            {
                get { return baseTransformation; }
                set { baseTransformation = value; }
            }

            public Field(Tools.CoordinateSystem coordinateSystem)
            {
                this.coordinateSystem = coordinateSystem;
                baseTransformation = null;
            }

            public void Init()
            {
                ID = "";

                PositionX = null;
                PositionY = null;
                Width = null;
                Height = null;
                Rotation = 0;
            }

            public virtual void Parse(XmlNode node)
            {
                Init();

                foreach (XmlAttribute attrib in node.Attributes)
                {
                    if (attrib.Name.Equals("id", StringComparison.OrdinalIgnoreCase))
                    {
                        ID = attrib.Value;
                    }
                    if (attrib.Name.Equals("valueref", StringComparison.OrdinalIgnoreCase))
                    {
                        ValueRef = attrib.Value;
                    }
                }

                foreach (XmlNode nodex in node.ChildNodes)
                {
                    if (nodex.Name.Equals("horzpos", StringComparison.OrdinalIgnoreCase))
                    {
                        if (nodex.InnerText.Length > 0)
                            PositionX = new Tools.Length((float)Convert.ToDouble(nodex.InnerText,CultureInfo.InvariantCulture), coordinateSystem.units);
                    }
                    else if (nodex.Name.Equals("vertpos", StringComparison.OrdinalIgnoreCase))
                    {
                        if (nodex.InnerText.Length > 0)
                            PositionY = new Tools.Length((float)Convert.ToDouble(nodex.InnerText, CultureInfo.InvariantCulture), coordinateSystem.units);
                    }
                    else if (nodex.Name.Equals("width", StringComparison.OrdinalIgnoreCase))
                    {
                        if (nodex.InnerText.Length > 0)
                            Width = new Tools.Length((float)Convert.ToDouble(nodex.InnerText, CultureInfo.InvariantCulture), coordinateSystem.units);
                    }
                    else if (nodex.Name.Equals("height", StringComparison.OrdinalIgnoreCase))
                    {
                        if (nodex.InnerText.Length > 0)
                            Height = new Tools.Length((float)Convert.ToDouble(nodex.InnerText, CultureInfo.InvariantCulture), coordinateSystem.units);
                    }
                    else if (nodex.Name.Equals("rotation", StringComparison.OrdinalIgnoreCase))
                    {
                        if (nodex.InnerText.Length > 0)
                            Rotation = (float)Convert.ToDouble(nodex.InnerText, CultureInfo.InvariantCulture);
                    }

                }
            }
            
            public virtual Rectangle Draw(Graphics Graph, Point Offset, string Value)
            {
                string sDefaultPrintString;
                Rectangle returnRectangle;
                PrintFormat printFormat;
                FontX Font;
                Region r;
                
                int widthInPixels = 0;  //Physical with and height (may depend on rotation!!
                int heightInPixels = 0; //
                
                r = Graph.Clip; //current clipping region
              
                printFormat = new PrintFormat();
                printFormat.format = new StringFormat();
                printFormat.format.Align = FieldFormat.Alignment.Left;

                Font = new FontX();
                Font.Font = new System.Drawing.Font("Arial", 8);
                
                //This routine should be overriden. If not yet implemented, 
                //show a string with the value typename:value
                
                sDefaultPrintString = Value;
                
                int X = (int)(PositionX.InPixels(Graph.DpiX) + Offset.X);
                int Y = (int)(PositionY.InPixels(Graph.DpiY) + Offset.Y);

                if ((Width != null && Width.length != 0) && (Height != null && Height.length != 0))
                {
                    widthInPixels = Width.InPixels(Graph.DpiX);
                    heightInPixels = Height.InPixels(Graph.DpiY);

                    //If this field has a with and a height, it has it's own clipping region,
                    //which will override the label clipping area. But it still has to be
                    //within the clipping area of the label.

                    if (Rotation == 90) //TODO -> not used?
                    {
                        widthInPixels = Height.InPixels(Graph.DpiX);
                        heightInPixels = Width.InPixels(Graph.DpiY);
                    }
                    else
                    {
                        widthInPixels = Width.InPixels(Graph.DpiX);
                        heightInPixels = Height.InPixels(Graph.DpiY);
                    }

                    Rectangle newRect;
                    newRect = new Rectangle(X, Y, widthInPixels, heightInPixels);
                    
                    //Determine the visible part of this rectangle within the current clipping area
                    //of the label. Take the resulting rectangle as the new clipping rectangle.
                    Graph.SetClip(newRect,System.Drawing.Drawing2D.CombineMode.Intersect);
                    Graph.FillRegion(Brushes.LightGray, Graph.Clip);
                }

                System.Drawing.StringFormat stringFormat = new System.Drawing.StringFormat();
                stringFormat.FormatFlags = StringFormatFlags.NoWrap;

                stringFormat.Alignment = printFormat.format.GetAsStringAlignment();

                if ((Width != null && Width.length != 0) && (Height != null && Height.length != 0))
                {
                    returnRectangle = new Rectangle(X, Y, widthInPixels, heightInPixels);
                    Graph.DrawString(sDefaultPrintString,
                        Font.Font,
                        Brushes.Black,
                        returnRectangle, stringFormat);
                }
                else
                {
                    SizeF stringSize = Graph.MeasureString(sDefaultPrintString, Font.Font);
                    returnRectangle = new Rectangle(X, Y, (int)stringSize.Width, (int)stringSize.Height);
                    Graph.Clip = new Region(returnRectangle);
                    Graph.DrawString(sDefaultPrintString,
                        Font.Font,
                        Brushes.Black,
                        new Point(X, Y), stringFormat);
                }
                Graph.Clip = r; // restore the clipping region. It has been messed up in this routine.
                Font.Dispose();
                return returnRectangle;
            }
            public virtual Rectangle Draw(Graphics Graph, Point Offset, LabelSet Labels, int language,Boolean isSelectedField)
            {
                Rectangle returnRectangle = new Rectangle();
                string sDefaultPrintString;
                PrintFormat printFormat;
                FontX Font;
                Region r;

                int widthInPixels = 0;  //Physical with and height (may depend on rotation!!
                int heightInPixels = 0; 

                r = Graph.Clip; //current clipping region

                printFormat = new PrintFormat();
                printFormat.format = new StringFormat();
                printFormat.format.Align = FieldFormat.Alignment.Center;

                Font = new FontX();
                Font.Font = new System.Drawing.Font("Arial", 8);

                //This routine should be overriden. If not yet implemented, 
                //show a string with the value typename:value
                
                sDefaultPrintString = this.ToString() + ":";
                sDefaultPrintString = sDefaultPrintString + ID.ToString();

                int X = (int)(PositionX.InPixels(Graph.DpiX) + Offset.X);
                int Y = (int)(PositionY.InPixels(Graph.DpiY) + Offset.Y);

                if ((Width != null) && (Height != null))
                {
                    widthInPixels = Width.InPixels(Graph.DpiX);
                    heightInPixels = Height.InPixels(Graph.DpiY);

                    //If this field has a with and a height, it has it's own clipping region,
                    //which will override the label clipping area. But it still has to be
                    //within the clipping area of the label.


                    if (Rotation == 90) //TODO
                    {
                        widthInPixels = Height.InPixels(Graph.DpiX);
                        heightInPixels = Width.InPixels(Graph.DpiY);
                    }
                    else
                    {
                        widthInPixels = Width.InPixels(Graph.DpiX);
                        heightInPixels = Height.InPixels(Graph.DpiY);
                    }

                    Rectangle newRect;
                    newRect = new Rectangle(X, Y, widthInPixels, heightInPixels);
                    returnRectangle = newRect;
                    //Determine the visible part of this rectangle within the current clipping area
                    //of the label. Take the resulting rectangle as the new clipping rectangle.
                    Graph.SetClip(newRect, System.Drawing.Drawing2D.CombineMode.Intersect);
                    Graph.FillRegion(Brushes.LightGray, Graph.Clip);
                }

                System.Drawing.StringFormat stringFormat = new System.Drawing.StringFormat();
                stringFormat.FormatFlags = StringFormatFlags.NoWrap;

                if (Rotation == 90) //TODO
                {
                    stringFormat.FormatFlags |= StringFormatFlags.DirectionVertical;
                }

                stringFormat.Alignment = printFormat.format.GetAsStringAlignment();

                if (Width != null && Height != null)
                {
                    Graph.DrawString(sDefaultPrintString,
                        Font.Font,
                        Brushes.Black,
                        new Rectangle(X, Y, widthInPixels, heightInPixels), stringFormat);
                }
                else
                {
                    Graph.DrawString(sDefaultPrintString,
                        Font.Font,
                        Brushes.Black,
                        new Point(X, Y), stringFormat);
                }

                //Return rectangle for use in picturebox selection
                if (Width != null && Height != null)
                {
                    returnRectangle = new Rectangle(X, Y, widthInPixels, heightInPixels);
                }

                Graph.Clip = r; // restore the clipping region. It has been messed up in this routine.
                Font.Dispose();
                return returnRectangle;
            }
            public virtual SizeF MeasureString(System.Drawing.Graphics graph, string data, System.Drawing.Font font, LabelSet Labels, int language)
            {
                SizeF size;
                size = graph.MeasureString(data,font);
                return size;
            }

            public virtual void DrawInfoControl()
            {
                
            }
        }

        public class TextField : Field
        {

            public FontX Font = null;
            public PrintFormat printFormat = null;

            public TextField(Tools.CoordinateSystem coordinateSystem)
                :base(coordinateSystem)
            {
            }
            
            public SizeF GetSize(Graphics Graph, Point Offset, string Value)
            {
                SizeF size = new SizeF(0,0);

                int X = (int)(PositionX.InPixels(Graph.DpiX) + Offset.X);
                int Y = (int)(PositionY.InPixels(Graph.DpiY) + Offset.Y);

                System.Drawing.StringFormat stringFormat = new System.Drawing.StringFormat();
                stringFormat.FormatFlags = StringFormatFlags.NoWrap;

                if (Rotation == 90)
                    stringFormat.FormatFlags |= StringFormatFlags.DirectionVertical;

                stringFormat.Alignment = printFormat.format.GetAsStringAlignment();


                if (Width != null && Height != null)
                {
                    return Graph.MeasureString(Value, Font.Font, new SizeF(Width.InPixels(Graph.DpiX), Height.InPixels(Graph.DpiY)), stringFormat);
                }
                else
                {
                    Graph.DrawString(Value,
                        Font.Font,
                        Brushes.Black,
                        new Point(X, Y), stringFormat);
                }
                return size; 
            }

            public string PreFormat(string sValue, LabelSet Labels, int language)
            {
                string sPrintValue;

                sPrintValue = sValue;

                if (printFormat != null)
                    sPrintValue = printFormat.format.Format(sPrintValue, Labels, language);

                return sPrintValue;
            }

            public override SizeF MeasureString(Graphics graph, string data, Font font, LabelSet Labels, int language)
            {
                string sPrintValue;
                sPrintValue = PreFormat(data, Labels, language);

                SizeF size;
                size = graph.MeasureString(sPrintValue, font);

                return size;
            }

            public override Rectangle Draw(Graphics Graph, Point Offset, LabelSet Labels, int language,Boolean isSelectedField)
            {
                string sPrintValue;
                int widthInPixels = 0;  //Physical with and height (may depend on rotation!!
                int heightInPixels = 0; 
                sPrintValue = string.Empty;

                Label.Value Value;
                string sID;
                if (this.ValueRef == null)
                {
                    sID = this.ID;
                }
                else
                {
                    sID = this.ValueRef;
                }

                Value = Labels.TryGetValue(sID, language);

                if (Value != null)
                    if (Value.Data != null)
                        sPrintValue = Value.Data;

                int X = (int)(PositionX.InPixels(Graph.DpiX) + Offset.X);
                int Y = (int)(PositionY.InPixels(Graph.DpiY) + Offset.Y);

                if ((Width != null) && (Height != null))
                {
                        widthInPixels = Width.InPixels(Graph.DpiX);
                        heightInPixels = Height.InPixels(Graph.DpiY);
                }

                System.Drawing.StringFormat stringFormat = new System.Drawing.StringFormat(); 
                stringFormat.FormatFlags = StringFormatFlags.NoWrap;

                if (printFormat == null)
                {
                    stringFormat.Alignment = StringAlignment.Near;
                }
                else
                {
                    stringFormat.Alignment = printFormat.format.GetAsStringAlignment();
                    try
                    {
                        sPrintValue = printFormat.format.Format(sPrintValue, Labels, language);
                    }
                    catch
                    {
                        sPrintValue = "Format Error";
                    }
                }

                Rectangle returnRectangle = DrawText(sPrintValue, X, Y, stringFormat, Graph,isSelectedField);
                return returnRectangle;
            }
                
            public Rectangle DrawText(String text, int x, int y, System.Drawing.StringFormat stringFormat, Graphics g,Boolean isSelectedField)
            {
                int originalX = x;
                int originalY = y;
                System.Drawing.Point[] points = new System.Drawing.Point[4];
                Brush drawBrush = Brushes.Black;
                
                SizeF theSize;
                theSize = g.MeasureString(text, Font.Font, new PointF(x, y), stringFormat);
                Matrix mat3 = g.Transform;  //Store Base Transformation.
                mat3.Invert();              //Inverted Base transformation;

                g.TranslateTransform(x, y);
                //Matrix mat2 = g.Transform;
                //Region r = g.Clip;
                //Region r2 = null;
                g.RotateTransform(Rotation);
                Region r = g.Clip;
                Region r2 = null;
                Matrix mat2 = g.Transform;
                
                //due to translation to x,y as the new origin of th drawing, we allways need draw at 0,0
                x = 0;
                y = 0;

                //System.Diagnostics.Debug.WriteLine(string.Format("Entering textdraw for x:{0},y:{1}", x, y));

                if (Width != null && Height != null)
                    theSize = new SizeF(Width.InPixels(g.DpiX), Height.InPixels(g.DpiY));
                else
                    theSize = g.MeasureString(text, Font.Font, new PointF(x, y), stringFormat);
                
                if (Width != null && Height != null)
                {
                    if (stringFormat.Alignment == StringAlignment.Near)
                    {
                        r2 = new Region(new Rectangle(x, y, (int)theSize.Width + 1, (int)theSize.Height + 1));
                        g.Clip = r2;
                    }
                    else
                    {
                        r2 = new Region(new Rectangle(x - (int)theSize.Width, y, (int)theSize.Width + 1, (int)theSize.Height + 1));
                        g.Clip = r2;
                    }
                }

                if (Font.InversePrint)
                {
                    if (stringFormat.Alignment == StringAlignment.Near)
                        g.FillRectangle(drawBrush, x, y, theSize.Width, theSize.Height);
                    else if (stringFormat.Alignment == StringAlignment.Far)
                        g.FillRectangle(drawBrush, x - theSize.Width, y, theSize.Width, theSize.Height);
                    drawBrush = Brushes.White;
                    
                }
                
                if (GlobalDataStore.GetInstance().DesignMode)
                {
                    Pen redPen = new Pen(Color.Red,1);
                    if (stringFormat.Alignment == StringAlignment.Near)
                        g.DrawRectangle(redPen, x, y, theSize.Width, theSize.Height);
                    else if (stringFormat.Alignment == StringAlignment.Far)
                        g.DrawRectangle(redPen, x - theSize.Width, y, theSize.Width, theSize.Height);
                    redPen.Dispose();
                }

                Rectangle returnRectangle = new Rectangle();
                if (stringFormat.Alignment == StringAlignment.Near)
                {
                    //  ----------------------------------->X
                    //  |  (0,0)              (width,0)
                    //  |   *-------------------+
                    //  |   |                   |
                    //  |   |                   |
                    //  |   +-------------------+
                    //  |  (0,height)         (width, height)
                    //  |
                    //  V  Y
                    points[0] = new Point(0, 0);
                    points[1] = new Point((int)theSize.Width, 0);
                    points[2] = new Point(0, (int)theSize.Height);
                    points[3] = new Point((int)theSize.Width, (int)theSize.Height);
                }
                else //if (stringFormat.Alignment == StringAlignment.Far)
                {
                    //far alignment rotates against the other corner (rotate point marked with *)
                    //   
                    //  ----------------------------------->X
                    //  |  (-width,0)              (0,0)
                    //  |   +-------------------*
                    //  |   |                   |
                    //  |   |                   |
                    //  |   +-------------------+
                    //  |  (-width,height)     (0, height)
                    //  |
                    //  V  Y
                    //                    
                    points[0] = new Point(0, 0);
                    points[1] = new Point(-(int)theSize.Width, 0);
                    points[2] = new Point(0, (int)theSize.Height);
                    points[3] = new Point(-(int)theSize.Width, (int)theSize.Height);
                }

                mat2.TransformPoints(points);
                mat2.Dispose();
                mat3.TransformPoints(points);
                mat3.Dispose();
                double width = Math.Sqrt(Math.Pow(points[1].X - points[0].X, 2) + Math.Pow(points[1].Y - points[0].Y, 2));
                double height = Math.Sqrt(Math.Pow(points[2].X - points[0].X, 2) + Math.Pow(points[2].Y - points[0].Y, 2));
                if (stringFormat.Alignment == StringAlignment.Near)
                {                
                    if (Rotation == 0)
                    {
                        returnRectangle = new Rectangle(points[0].X, points[0].Y, (int)width, (int)height);
                    }
                    if (Rotation == 90)
                    {
                        returnRectangle = new Rectangle(points[2].X, points[2].Y, (int)height, (int)width);
                    }
                    if (Rotation == 180)
                    {
                        returnRectangle = new Rectangle(points[3].X, points[3].Y, (int)width, (int)height);
                    }
                    if (Rotation == 270)
                    {
                        returnRectangle = new Rectangle(points[1].X, points[1].Y, (int)height, (int)width);
                    }                                      
                }
                else if (stringFormat.Alignment == StringAlignment.Far)
                {
                    if (Rotation == 0)
                    {
                        returnRectangle = new Rectangle(points[1].X, points[1].Y, (int)width, (int)height);
                    }
                    if (Rotation == 90)
                    {
                        returnRectangle = new Rectangle(points[2].X, points[1].Y, (int)height, (int)width);
                    }
                    if (Rotation == 180)
                    {
                        returnRectangle = new Rectangle(points[3].X, points[3].Y, (int)width, (int)height);
                    }
                    if (Rotation == 270)
                    {
                        returnRectangle = new Rectangle(points[1].X, points[2].Y, (int)height, (int)width);
                    }                    
                }

                if (isSelectedField)
                {
                    Pen bluePen = new Pen(Color.Blue);
                    g.DrawLine(bluePen, new Point(x - 10, y), new Point(x + 10, y));
                    g.DrawLine(bluePen, new Point(x, y - 10), new Point(x, y + 10));
                    bluePen.Dispose();
                }  
                //System.Windows.Forms.TextRenderer.DrawText(g,text,Font.Font,new Point(x,-y),Color.Black);                                
                g.DrawString(text, Font.Font, drawBrush, new PointF(x, y), stringFormat);
                if (Width != null && Height != null)
                {
                    g.Clip = r;
                    r2.Dispose();
                }
                g.ResetTransform();
                if (BaseTransformation != null)
                {
                    g.Transform = BaseTransformation;                    
                }
                //drawBrush.Dispose();
                r.Dispose();                
                return returnRectangle;
                
            }
            /*
              public Rectangle DrawText(String text, int x, int y, System.Drawing.StringFormat stringFormat, Graphics g,Boolean isSelectedField)
            {
                int originalX = x;
                int originalY = y;
                System.Drawing.Point[] points = new System.Drawing.Point[4];
                Brush drawBrush = Brushes.Black;


                //g.RotateTransform(Rotation, MatrixOrder.Append);

                SizeF theSize;
                theSize = g.MeasureString(text, Font.Font, new PointF(x, y), stringFormat);
                System.Diagnostics.Debug.WriteLine(string.Format("before transform size of {0}: width:{1}, height:{2}", text, theSize.Width, theSize.Height));
                System.Diagnostics.Debug.WriteLine(string.Format("Entering textdraw for with orgininal values x:{0},y:{1}", x, y));
                points[0] = new Point(x, y);
                points[1] = new Point(x + (int)theSize.Width, y);
                points[2] = new Point(x, (int)theSize.Height + y);
                points[3] = new Point(x +(int)theSize.Width, y+ (int)theSize.Height);
                System.Diagnostics.Debug.WriteLine("Original Rectangle Point values: ------------------------");
                for (int tt = 0; tt <= 3; tt++)
                {
                    System.Diagnostics.Debug.WriteLine(string.Format("Transformed point[{0}]: x:{1}, y:{2}", tt, points[tt].X, points[tt].Y));
                }
                Matrix mat3 = g.Transform;  //Store Base Transformation.
                mat3.Invert();              //Inverted Base transformation;

                g.TranslateTransform(x, y);
                //Matrix mat2 = g.Transform;
                //Region r = g.Clip;
                //Region r2 = null;
                g.RotateTransform(Rotation);
                Region r = g.Clip;
                Region r2 = null;
                Matrix mat2 = g.Transform;
                
                //due to translation to x,y as the new origin of th drawing, we allways need draw at 0,0
                x = 0;
                y = 0;

                System.Diagnostics.Debug.WriteLine(string.Format("Entering textdraw for x:{0},y:{1}", x, y));

                if (Width != null && Height != null)
                    theSize = new SizeF(Width.InPixels(g.DpiX), Height.InPixels(g.DpiY));
                else
                    theSize = g.MeasureString(text, Font.Font, new PointF(x, y), stringFormat);
                System.Diagnostics.Debug.WriteLine(string.Format("size of {0}: width:{1}, height:{2}", text, theSize.Width, theSize.Height));
                if (Width != null && Height != null)
                {
                    if (stringFormat.Alignment == StringAlignment.Near)
                    {
                        r2 = new Region(new Rectangle(x, y, (int)theSize.Width + 1, (int)theSize.Height + 1));
   //tempweg                     g.Clip = r2;
                    }
                    else
                    {
                        r2 = new Region(new Rectangle(x - (int)theSize.Width, y, (int)theSize.Width + 1, (int)theSize.Height + 1));
   //tempweg                     g.Clip = r2;
                    }
                }

                if (Font.InversePrint)
                {
                    if (stringFormat.Alignment == StringAlignment.Near)
                        g.FillRectangle(drawBrush, x, y, theSize.Width, theSize.Height);
                    else if (stringFormat.Alignment == StringAlignment.Far)
                        g.FillRectangle(drawBrush, x - theSize.Width, y, theSize.Width, theSize.Height);
                    drawBrush = Brushes.White;
                    
                }
                
                if (GlobalDataStore.GetInstance().DesignMode)
                {
                    Pen redPen = new Pen(Color.Red,1);
                    if (stringFormat.Alignment == StringAlignment.Near)
                        g.DrawRectangle(redPen, x, y, theSize.Width, theSize.Height);
                    else if (stringFormat.Alignment == StringAlignment.Far)
                        g.DrawRectangle(redPen, x - theSize.Width, y, theSize.Width, theSize.Height);
                    redPen.Dispose();
                }

                ////Return rectangle used for mouse selecting on the picturebox
                //Rectangle returnRectangle = new Rectangle();
                //if (stringFormat.Alignment == StringAlignment.Near)
                //    returnRectangle = new Rectangle(x, y, (int)theSize.Width, (int)theSize.Height);
                //else if (stringFormat.Alignment == StringAlignment.Far)
                //    returnRectangle = new Rectangle(x - (int)theSize.Width, y, (int)theSize.Width, (int)theSize.Height);

                //Return rectangle used for mouse selecting on the picturebox
                Rectangle returnRectangle = new Rectangle();
                if (stringFormat.Alignment == StringAlignment.Near)
                {
                    //1 if (Rotation == 0)                    
                    points[0] = new Point(0, 0);
                    points[1] = new Point((int)theSize.Width, 0);
                    points[2] = new Point(0, (int)theSize.Height);
                    points[3] = new Point((int)theSize.Width, (int)theSize.Height);
                    System.Diagnostics.Debug.WriteLine("Translated (shifted to origin) Rectangle Point values: ------------------------");
                    for (int tt = 0; tt <= 3; tt++)
                    {
                        System.Diagnostics.Debug.WriteLine(string.Format("Transformed point[{0}]: x:{1}, y:{2}", tt, points[tt].X, points[tt].Y));
                    }
                    returnRectangle = new Rectangle(points[0].X, points[0].Y, points[3].X, points[3].Y);
                    System.Diagnostics.Debug.WriteLine(string.Format("Before start:  X:{0},Y:{1} -> Right:{2},bottom:{3}",
                         returnRectangle.X, returnRectangle.Y, returnRectangle.Right, returnRectangle.Bottom));

                    mat2.TransformPoints(points);
                    mat2.Dispose();
                    System.Diagnostics.Debug.WriteLine("After Mat2 transform: ------------------------");
                    for (int tt = 0; tt <= 3; tt++)
                    {
                        System.Diagnostics.Debug.WriteLine(string.Format("Transformed point[{0}]: x:{1}, y:{2}", tt, points[tt].X, points[tt].Y));
                    }
                    mat3.TransformPoints(points);
                    mat3.Dispose();
                    //g.Transform.TransformPoints(points);
                    System.Diagnostics.Debug.WriteLine("After Mat3 transform: --------------------------");
                    for (int tt = 0; tt <= 3; tt++)
                    {
                        System.Diagnostics.Debug.WriteLine(string.Format("Transformed point[{0}]: x:{1}, y:{2}", tt, points[tt].X, points[tt].Y));
                    }
                    / *
                    Matrix mat4;
                    mat4 = new Matrix();
                    //mat4.Translate(originalX, originalY);                    
                    mat4.Rotate(Rotation);                    
                    mat4.Invert();
                    //mat4.Translate(originalX, originalY);                    
                    mat4.TransformPoints(points);
                    System.Diagnostics.Debug.WriteLine("After Mat4 transform: --------------------------");
                    for (int tt = 0; tt <= 3; tt++)
                    {
                        System.Diagnostics.Debug.WriteLine(string.Format("Transformed point[{0}]: x:{1}, y:{2}", tt, points[tt].X, points[tt].Y));
                    }
                    * /

                    double width = Math.Sqrt(Math.Pow(points[1].X  - points[0].X, 2) + Math.Pow(points[1].Y - points[0].Y, 2)); 
                    double height = Math.Sqrt(Math.Pow(points[2].X - points[0].X, 2) + Math.Pow(points[2].Y - points[0].Y, 2));
                    System.Diagnostics.Debug.WriteLine(string.Format("Calculated with: {0}, height: {1}", width, height));

                    returnRectangle = new Rectangle(originalX, originalY, (int)theSize.Width, (int)theSize.Height);
                    System.Diagnostics.Debug.WriteLine(string.Format("ReturnRectangle should be (no rotation): {0},{1} -> {2},{3}",
                         returnRectangle.X,returnRectangle.Y,returnRectangle.Right, returnRectangle.Bottom));

                    if (Rotation == 0)
                    {
                        returnRectangle = new Rectangle(points[0].X, points[0].Y, (int)width, (int)height);
                    }
                    if (Rotation == 90)
                    {
                        returnRectangle = new Rectangle(points[2].X, points[2].Y, (int)height, (int)width);
                    }
                    if (Rotation == 180)
                    {
                        returnRectangle = new Rectangle(points[3].X, points[3].Y, (int)width, (int)height);
                    }
                    if (Rotation == 270)
                    {
                        returnRectangle = new Rectangle(points[1].X, points[1].Y, (int)height, (int)width);
                    }

                    System.Diagnostics.Debug.WriteLine(string.Format("After transform:  {0},{1} -> {2},{3}",
                         returnRectangle.X, returnRectangle.Y, returnRectangle.Right, returnRectangle.Bottom));
                    
                    //    returnRectangle = new Rectangle(originalX, originalY, (int)theSize.Width, (int)theSize.Height);
                    //1 if (Rotation == 90)
                    //1   returnRectangle = new Rectangle(originalX - (int)theSize.Height, -originalY, (int)theSize.Height, (int)theSize.Width);
                    //1 if (Rotation == 180)
                    //1    returnRectangle = new Rectangle(-(originalX + (int)theSize.Width), -(originalY + (int)theSize.Height), (int)theSize.Width, (int)theSize.Height);
                    //1if (Rotation == 270)
                    //    returnRectangle = new Rectangle(-originalX, originalY - (int)theSize.Width, (int)theSize.Height, (int)theSize.Width);
                }
                else if (stringFormat.Alignment == StringAlignment.Far)
                {
                    //if(Rotation == 0)
                        returnRectangle = new Rectangle(originalX - (int)theSize.Width, originalY, (int)theSize.Width, (int)theSize.Height);
                    //if (Rotation == 90)
                    //    returnRectangle = new Rectangle(originalX - (int)theSize.Height, -originalY - (int)theSize.Width, (int)theSize.Height, (int)theSize.Width);
                    //if (Rotation == 180)
                    //    returnRectangle = new Rectangle(-originalX, -originalY - (int)theSize.Height, (int)theSize.Width, (int)theSize.Height);
                    //if (Rotation == 270)
                    //    returnRectangle = new Rectangle(-originalX, originalY, (int)theSize.Height, (int)theSize.Width);
                }

                if (isSelectedField)
                {
                    Pen bluePen = new Pen(Color.Blue);
                    g.DrawLine(bluePen, new Point(x - 10, y), new Point(x + 10, y));
                    g.DrawLine(bluePen, new Point(x, y - 10), new Point(x, y + 10));
                    bluePen.Dispose();
                }  
                //System.Windows.Forms.TextRenderer.DrawText(g,text,Font.Font,new Point(x,-y),Color.Black);                                
                System.Diagnostics.Debug.WriteLine(string.Format("Drawing in textdraw for x:{0},y:{1} Text:{2}", x, y,text));
                g.DrawString(text, Font.Font, drawBrush, new PointF(x, y), stringFormat);
                if (Width != null && Height != null)
                {
//tempweg                    g.Clip = r;
                    r2.Dispose();
                }
                g.ResetTransform();
                if (BaseTransformation != null)
                {
                    g.Transform = BaseTransformation;                    
                }
                //drawBrush.Dispose();
                r.Dispose();                
                return returnRectangle;
                
            }
*/
            /*
            public Rectangle DrawText(String text, int x, int y, System.Drawing.StringFormat stringFormat, Graphics g, Boolean isSelectedField)
            {
                int originalX = x;
                int originalY = y;
                Brush drawBrush = Brushes.Black;
                g.RotateTransform(Rotation, MatrixOrder.Append);
                SizeF theSize;
                Region r = g.Clip;
                Region r2 = null;
                System.Diagnostics.Debug.WriteLine(string.Format("Entering textdraw for x:{0},y:{1}", x, y));
                if (Width != null && Height != null)
                    theSize = new SizeF(Width.InPixels(g.DpiX), Height.InPixels(g.DpiY));
                else
                    theSize = g.MeasureString(text, Font.Font, new PointF(x, y), stringFormat);

                if (Rotation == 90)
                {
                    System.Diagnostics.Debug.WriteLine(string.Format("In Drawtext: translatetransform x:{0} y:{1}", -(x - y), -(x - y)));
                    //g.TranslateTransform(-(x - y), -(x - y));
                    g.TranslateTransform((x - y), -(x - y), MatrixOrder.Append);
                    y = -y;
                }
                else if (Rotation == 180)
                {
                    g.TranslateTransform(0, 0);
                    x = -x;
                    y = -y;
                }
                else if (Rotation == 270)
                {
                    g.TranslateTransform(-(x - y), x - y, MatrixOrder.Append);
                    //g.TranslateTransform(x - y, x - y);
                    x = -x;
                }

                if (Width != null && Height != null)
                {
                    if (stringFormat.Alignment == StringAlignment.Near)
                    {
                        r2 = new Region(new Rectangle(x, y, (int)theSize.Width + 1, (int)theSize.Height + 1));
                        g.Clip = r2;
                    }
                    else
                    {
                        r2 = new Region(new Rectangle(x - (int)theSize.Width, y, (int)theSize.Width + 1, (int)theSize.Height + 1));
                        g.Clip = r2;
                    }
                }

                if (Font.InversePrint)
                {
                    if (stringFormat.Alignment == StringAlignment.Near)
                        g.FillRectangle(drawBrush, x, y, theSize.Width, theSize.Height);
                    else if (stringFormat.Alignment == StringAlignment.Far)
                        g.FillRectangle(drawBrush, x - theSize.Width, y, theSize.Width, theSize.Height);
                    drawBrush = Brushes.White;

                }

                if (GlobalDataStore.GetInstance().DesignMode)
                {
                    Pen redPen = new Pen(Color.Red, 1);
                    if (stringFormat.Alignment == StringAlignment.Near)
                        g.DrawRectangle(redPen, x, y, theSize.Width, theSize.Height);
                    else if (stringFormat.Alignment == StringAlignment.Far)
                        g.DrawRectangle(redPen, x - theSize.Width, y, theSize.Width, theSize.Height);
                    redPen.Dispose();
                }

                ////Return rectangle used for mouse selecting on the picturebox
                //Rectangle returnRectangle = new Rectangle();
                //if (stringFormat.Alignment == StringAlignment.Near)
                //    returnRectangle = new Rectangle(x, y, (int)theSize.Width, (int)theSize.Height);
                //else if (stringFormat.Alignment == StringAlignment.Far)
                //    returnRectangle = new Rectangle(x - (int)theSize.Width, y, (int)theSize.Width, (int)theSize.Height);

                //Return rectangle used for mouse selecting on the picturebox
                Rectangle returnRectangle = new Rectangle();
                if (stringFormat.Alignment == StringAlignment.Near)
                {
                    if (Rotation == 0)
                        returnRectangle = new Rectangle(x, y, (int)theSize.Width, (int)theSize.Height);
                    if (Rotation == 90)
                        returnRectangle = new Rectangle(x - (int)theSize.Height, -y, (int)theSize.Height, (int)theSize.Width);
                    if (Rotation == 180)
                        returnRectangle = new Rectangle(-(x + (int)theSize.Width), -(y + (int)theSize.Height), (int)theSize.Width, (int)theSize.Height);
                    if (Rotation == 270)
                        returnRectangle = new Rectangle(-x, y - (int)theSize.Width, (int)theSize.Height, (int)theSize.Width);
                }
                else if (stringFormat.Alignment == StringAlignment.Far)
                {
                    if (Rotation == 0)
                        returnRectangle = new Rectangle(x - (int)theSize.Width, y, (int)theSize.Width, (int)theSize.Height);
                    if (Rotation == 90)
                        returnRectangle = new Rectangle(x - (int)theSize.Height, -y - (int)theSize.Width, (int)theSize.Height, (int)theSize.Width);
                    if (Rotation == 180)
                        returnRectangle = new Rectangle(-x, -y - (int)theSize.Height, (int)theSize.Width, (int)theSize.Height);
                    if (Rotation == 270)
                        returnRectangle = new Rectangle(-x, y, (int)theSize.Height, (int)theSize.Width);
                }

                if (isSelectedField)
                {
                    Pen bluePen = new Pen(Color.Blue);
                    g.DrawLine(bluePen, new Point(x - 10, y), new Point(x + 10, y));
                    g.DrawLine(bluePen, new Point(x, y - 10), new Point(x, y + 10));
                    bluePen.Dispose();
                }
                //System.Windows.Forms.TextRenderer.DrawText(g,text,Font.Font,new Point(x,-y),Color.Black);                                
                System.Diagnostics.Debug.WriteLine(string.Format("Drawing in textdraw for x:{0},y:{1} Text:{2}", x, y, text));
                g.DrawString(text, Font.Font, drawBrush, new PointF(x, y), stringFormat);
                if (Width != null && Height != null)
                {
                    g.Clip = r;
                    r2.Dispose();
                }
                g.ResetTransform();
                if (BaseTransformation != null)
                {
                    g.Transform = BaseTransformation;
                }
                //drawBrush.Dispose();
                r.Dispose();
                return returnRectangle;

            }
            */

            public override void Parse(XmlNode node)
            {
                throw new ApplicationException(string.Format("Please use other Parse method"));
            }

            public virtual void Parse(XmlNode node, ref IDictionary<string, FontX> availableFonts)
            {
                base.Parse(node);

                foreach (XmlNode nodex in node.ChildNodes)
                {
                    if (nodex.Name.Equals("fontref", StringComparison.OrdinalIgnoreCase))
                    {
                        foreach (XmlAttribute attrib in nodex.Attributes)
                        {
                            if (attrib.Name.Equals("id", StringComparison.OrdinalIgnoreCase))
                            {
                                FontX fontX = null;
                                if (availableFonts.TryGetValue(attrib.Value, out fontX))
                                {
                                    this.Font = fontX;
                                } else
                                {
                                    throw new ApplicationException(string.Format("Error: Textfield id: {0} -> Could not find font with id: {1}", this.ID, attrib.Value));
                                }
                            }
                        }
                    }
                    else if (nodex.Name.Equals("printformat", StringComparison.OrdinalIgnoreCase))
                    {
                        printFormat = new PrintFormat();
                        printFormat.Parse(nodex);
                    }
                }
            }
        }

        public class TextFieldGroup : TextField
        {
            public enum ConcatMethod { Horizontal, Vertical};
            public ConcatMethod concatMethod = ConcatMethod.Horizontal;

            public TextFieldGroup(Tools.CoordinateSystem coordinateSystem, ConcatMethod concatMethod)
                :base(coordinateSystem)
            {
                this.concatMethod = concatMethod;
            }

            public IList<TextField> fields = new List<TextField>();

            public override void Parse(XmlNode node)
            {
                throw new ApplicationException(string.Format("Please use other Parse method"));//GetString()
            }

            public override void Parse(XmlNode node, ref IDictionary<string, FontX> availableFonts)
            {
                base.Parse(node, ref availableFonts);

                foreach (XmlNode nodex in node.ChildNodes)
                {
                    if (nodex.Name.Equals("internalfields", StringComparison.OrdinalIgnoreCase))
                    {
                        foreach (XmlNode nodexx in nodex.ChildNodes)
                        {
                            if (nodexx.Name.Equals("textfield", StringComparison.OrdinalIgnoreCase))
                            {
                                TextField field = new TextField(coordinateSystem);
                                field.Parse(nodexx, ref availableFonts);
                                fields.Add(field);
                            }
                        }
                    }
                    if (nodex.Name.Equals("concatMethod", StringComparison.OrdinalIgnoreCase))
                    {
                        if (nodex.InnerText.Equals("horizontal", StringComparison.OrdinalIgnoreCase)) 
                        {
                            concatMethod = ConcatMethod.Horizontal;
                        }
                        else if (nodex.InnerText.Equals("vertical", StringComparison.OrdinalIgnoreCase))
                        {
                            concatMethod = ConcatMethod.Vertical;
                        }
                    }
                }
            }


            public override Rectangle Draw(Graphics Graph, Point theOffset, LabelSet Labels, int language,Boolean isSelectedField)
            {
                Rectangle returnRectangle = new Rectangle();
                int delta = 0;
                Point Offset = theOffset;
 
                Label.Value value = null;
                bool bAlignRight = false;

                if (printFormat != null)
                {
                    if (printFormat.format.Align == FieldFormat.Alignment.Right)
                    {
                        bAlignRight = true;
                    }
                }
                int from;
                int to;
                int change;
                if (bAlignRight == false)
                {
                    from = 0;
                    to = fields.Count - 1;
                    change = 1;
                } else
                {
                    from = fields.Count - 1;
                    to = 0;
                    change = -1;
                }

                TextField f;
                for (int tel = from; tel != to + change; tel = tel + change)
                {
                    f = fields[tel];
                    f.BaseTransformation = this.BaseTransformation;

                    string sID;
                    if (f.ValueRef != null)
                    {
                        sID = f.ValueRef;
                    }
                    else
                    {
                        sID = f.ID;
                    }

                    value = Labels.TryGetValue(sID, language);
                    if (value == null)
                    {
                        //A variable which is not defined in the XML is used in the label. Now
                        //We get a null reference exception.
                        //We could simple not print it, but this would leave no visible indicator.
                        //We choose to print *#* in stead.
                        //mve hotfix. Should be removed in future.
                        value = new Label.Value();
                        value.Key = sID;
                        //value.Data = "*#*";
                        value.Data = "";
                        value.Language = language;
                        //TODO, JBOS, This fix doesn't show *#* since value is only used here to measure the length of the string
                        //Changed *#* to empty string to avoid problems with wrong width detection
                    }

                    //Adjust the field to the new offset, which is determined
                    //by drawing the previous field
                    f.PositionX = this.PositionX;
                    f.PositionY = this.PositionY;
                    f.coordinateSystem = this.coordinateSystem;
                    f.Rotation = this.Rotation;

                    //All strings in a concatenated string MUST have the same allignment.
                    if (f.printFormat == null)
                    {
                        f.printFormat = this.printFormat;
                    }
                    else
                    {
                        f.printFormat.format.Align = printFormat.format.Align;
                    }
                    f.Rotation = this.Rotation;

                    if (this.Width != null && this.Height != null)
                    {
                        f.Width = this.Width;
                        f.Height = this.Height;
                    }
                    Rectangle tempRectangle = f.Draw(Graph, Offset, Labels, language,isSelectedField);

                    if (f.Width != null && f.Height != null)
                    {
                        f.Width = null;
                        f.Height = null;
                    }

                    if (returnRectangle.IsEmpty)
                    {
                        returnRectangle = tempRectangle;
                    }
                    else 
                    {
                        returnRectangle = Rectangle.Union(returnRectangle, tempRectangle);
                    }                    

                    SizeF size;
                    if (this.Height != null && this.Width != null)
                        size = new SizeF(this.Width.InPixels(Graph.DpiX), this.Height.InPixels(Graph.DpiY));
                    else
                        size = f.MeasureString(Graph, value.Data, f.Font.Font, Labels, language);
                    
                    switch (concatMethod)
                    {
                        case ConcatMethod.Horizontal:
                            //Add length of outputed string in pixels.                            
                            delta = (int)size.Width;

                            if (Rotation == 0)
                            {
                                if (bAlignRight)
                                    Offset.X -=delta;
                                else
                                    Offset.X +=delta;
                            }
                            else if (Rotation == 90)
                            {
                                if (bAlignRight)
                                    Offset.Y -= delta;
                                else
                                    Offset.Y += delta;
                            }
                            
                            else if (Rotation == 180)
                            {
                                if (bAlignRight)
                                    Offset.X += delta;
                                else
                                    Offset.X -= delta;
                            }
                            else if (Rotation == 270) 
                            {
                                if (bAlignRight)
                                    Offset.Y += delta;
                                else
                                    Offset.Y -= delta;
                            }
                            break;
                        case ConcatMethod.Vertical:
                            //We put lines stacked. We have to increase with the height of the line.                            
                            delta = (int)size.Height;

                            if (Rotation == 0)
                                Offset.Y += delta;
                            else if (Rotation == 90)
                                Offset.X -= delta;
                            else if (Rotation == 180)
                                Offset.Y -= delta;
                            else if (Rotation == 270)
                                Offset.X += delta;
                            break;
                    }
                }
                return returnRectangle;
            }
        }

        public class BarcodeField : Field
        {
            /// <summary>
            /// Give max length of barcodes for non fixed-length barcodes.
            /// Fixed barcode types as EAN8 or EAN13 will set this themselves.
            /// </summary>
            public int MaxCharCount;    
            public enum BarcodeType { EAN8, EAN13, UPCVersionA, Code39, Interleaved2Of5, EAN128, DataMatrix, PDF417 }
            public PrintFormat printFormat = null;
            public bool printText = true;
            public BarcodeType Type = BarcodeType.EAN13;

            public BarcodeField(Tools.CoordinateSystem coordinateSystem)
                :base(coordinateSystem)
            {
            }

            public override void Parse(XmlNode node)
            {
                base.Parse(node);

                foreach (XmlNode nodex in node.ChildNodes)
                {
                    if (nodex.Name.Equals("barcode", StringComparison.OrdinalIgnoreCase))
                    {
                        foreach (XmlAttribute attrib in nodex.Attributes)
                        {
                            if (attrib.Name.Equals("type", StringComparison.OrdinalIgnoreCase))
                            {
                                if (attrib.Value.Equals("ean13", StringComparison.OrdinalIgnoreCase))
                                {
                                    Type = BarcodeType.EAN13;
                                    MaxCharCount = 13;
                                }
                                else if (attrib.Value.Equals("ean8", StringComparison.OrdinalIgnoreCase))
                                {
                                    Type = BarcodeType.EAN8;
                                    MaxCharCount = 8;
                                }
                                else if (attrib.Value.Equals("upcversionA", StringComparison.OrdinalIgnoreCase))
                                {
                                    Type = BarcodeType.UPCVersionA;
                                    MaxCharCount = 13;
                                }
                                else if (attrib.Value.Equals("code39", StringComparison.OrdinalIgnoreCase))
                                {
                                    Type = BarcodeType.Code39;
                                    MaxCharCount = 22;
                                }
                                else if ( (attrib.Value.Equals("2of5", StringComparison.OrdinalIgnoreCase)) ||
                                          (attrib.Value.Equals("Interleaved2Of5", StringComparison.OrdinalIgnoreCase))
                                        ) 
                                {
                                    Type = BarcodeType.Interleaved2Of5;
                                    MaxCharCount = 22;
                                }
                                else if (attrib.Value.Equals("ean128", StringComparison.OrdinalIgnoreCase))
                                {
                                    Type = BarcodeType.EAN128;
                                    MaxCharCount = 22;
                                }
                                else if (attrib.Value.Equals("DataMatrix", StringComparison.OrdinalIgnoreCase))
                                {
                                    Type = BarcodeType.DataMatrix;
                                    MaxCharCount = 3000;
                                }
                                else if (attrib.Value.Equals("pdf417", StringComparison.OrdinalIgnoreCase))
                                {
                                    Type = BarcodeType.PDF417;
                                    MaxCharCount = 3000;
                                }
                                else
                                    throw new ApplicationException(string.Format("BarcodeField: {0} -> Wrong barcode type {1}", this.ID, attrib.Value));//GetString()
                            }
                            if (attrib.Name.Equals("maxcharcount", StringComparison.OrdinalIgnoreCase))
                            {
                                try
                                {
                                    MaxCharCount = int.Parse(attrib.Value);
                                }
                                catch (Exception ex2)
                                {
                                    throw new ApplicationException(string.Format("Barcodetype MaxCharCount is not a number: {0} ", MaxCharCount), ex2);//GetString()
                                }
                            }
                            if (attrib.Name.Equals("printtext", StringComparison.OrdinalIgnoreCase))
                            {
                                try
                                {
                                    if (attrib.Value.Equals("true", StringComparison.OrdinalIgnoreCase))
                                        printText = true;
                                    else
                                        printText = false;
                                }
                                catch (Exception ex2)
                                {
                                    throw new ApplicationException(string.Format("Barcodetype MaxCharCount is not a number: {0} ", MaxCharCount), ex2);//GetString()
                                }
                            }
                        }
                    }
                    else if (nodex.Name.Equals("printformat", StringComparison.OrdinalIgnoreCase))
                    {
                        printFormat = new PrintFormat();
                        printFormat.Parse(nodex);
                    }
                }
            }

            public override Rectangle Draw(Graphics Graph, Point Offset, LabelSet labels, int language, Boolean isSelectedField)
            {
                string sDefaultPrintString;
                Region r;
                Rectangle barRectangle;
                Label.Value Value = null;

                string sID;
                if (this.ValueRef == null)
                {
                    sID = this.ID;
                }
                else
                {
                    sID = this.ValueRef;
                }

                Value = labels.TryGetValue(sID,language);

                barRectangle = new Rectangle(0,0,0,0);

                sDefaultPrintString = null;

                //Physical with and height (may depend on rotation!!)
                int widthInPixels = 0;  
                int heightInPixels = 0; 

                r = Graph.Clip; //current clipping region

                if (Value != null)
                    if (Value.Data != null)
                        sDefaultPrintString = Value.Data;

                int X = (int)(PositionX.InPixels(Graph.DpiX) + Offset.X);
                int Y = (int)(PositionY.InPixels(Graph.DpiY) + Offset.Y);

                if ((Width != null) && (Height != null))
                {
                    if (Rotation == 0)
                    {
                        widthInPixels = Width.InPixels(Graph.DpiX);
                        heightInPixels = Height.InPixels(Graph.DpiY);
                        barRectangle = new Rectangle(X, Y, widthInPixels, heightInPixels);
                    }
                    else if (Rotation == 90)
                    {
                        widthInPixels = Height.InPixels(Graph.DpiX);
                        heightInPixels = Width.InPixels(Graph.DpiY);
                        barRectangle = new Rectangle(X - widthInPixels, Y, widthInPixels, heightInPixels);
                    }
                    else if (Rotation == 180)
                    {
                        widthInPixels = Width.InPixels(Graph.DpiX);
                        heightInPixels = Height.InPixels(Graph.DpiY);
                        barRectangle = new Rectangle(X - widthInPixels, Y - heightInPixels, widthInPixels, heightInPixels);
                    }
                    else if (Rotation == 270)
                    {
                        widthInPixels = Height.InPixels(Graph.DpiX);
                        heightInPixels = Width.InPixels(Graph.DpiY);
                        barRectangle = new Rectangle(X, Y - heightInPixels, widthInPixels, heightInPixels);
                    }

                    //Determine the visible part of this rectangle within the current clipping area
                    //of the label. Take the resulting rectangle as the new clipping rectangle.
                    Graph.SetClip(barRectangle, System.Drawing.Drawing2D.CombineMode.Intersect);
                    
                    if(GlobalDataStore.GetInstance().DesignMode)
                        Graph.FillRegion(Brushes.LightGray, Graph.Clip);
                    if(isSelectedField)
                        Graph.FillRegion(Brushes.LightBlue, Graph.Clip);
                }

                Barcode.Barcode bc;
                Barcode.Barcode2d bc2d;
                bc2d = null;
                bc = null;
                switch (Type)
                {
                    case BarcodeType.EAN13:
                        bc = new Barcode.BarcodeEAN13();
                        break;
                    case BarcodeType.Code39:
                        bc = new Barcode.Barcode39();
                        break;
                    case BarcodeType.EAN8:
                        bc = new Barcode.BarcodeEAN8();
                        break;
                    case BarcodeType.Interleaved2Of5:
                        bc = new Barcode.Barcode2Of5();
                        break;
                    case BarcodeType.UPCVersionA:
                        bc = new Barcode.BarcodeUPCversionA();
                        break;
                    case BarcodeType.EAN128:
                        bc = new Barcode.BarcodeEAN128();
                        break;
                    case BarcodeType.DataMatrix:
                        bc2d = new Barcode.BarcodeDataMatrix();
                        break;
                    case BarcodeType.PDF417:
                        bc2d = new Barcode.BarcodePDF417();
                        break;
                }

                Image barcodeImage;
                Graphics g = null;
                if (bc2d == null)
                {
                    if (printFormat == null)
                    {
                        bc.Align = StringAlignment.Near;
                    }
                    else
                    {
                        if (printFormat.format == null)
                        {
                            bc.Align = StringAlignment.Near;
                        }
                        else
                        {
                            bc.Align = printFormat.format.GetAsStringAlignment();
                        }
                    }

                    if (bc == null)
                    {
                        return base.Draw(Graph, Offset, "Type unknown");//GetString()
                    }

                    if ((Width == null) || (Height == null))
                    {
                        return base.Draw(Graph, Offset, "Error: width/height");//GetString()
                    }                    
                    barcodeImage = new Bitmap(Width.InPixels(Graph.DpiX), Height.InPixels(Graph.DpiY)); ;
                    g = Graphics.FromImage(barcodeImage);

                    try
                    {
                        bc.SetCode(Value.Data, false);
                        bc.DrawBarcode(printText, g, 0, 0, Width.InPixels(Graph.DpiX), Height.InPixels(Graph.DpiY), MaxCharCount);
                    }
                    catch (Exception e)
                    {
                        Pen redPen = new Pen(Brushes.Red, 5);
                        System.Diagnostics.Debug.WriteLine("Unable to draw barcode: " + e.Message);
                        g.DrawLine(redPen, new Point(0, 0), new Point(barcodeImage.Width, barcodeImage.Height));
                        g.DrawLine(redPen, new Point(0, barcodeImage.Height), new Point(barcodeImage.Width, 0));
                        redPen.Dispose();
                    }
                }
                else
                {
                    if (bc2d == null)
                    {
                        return base.Draw(Graph, Offset, "Type unknown");//GetString()
                    }

                    if ((Width == null) || (Height == null))
                    {
                        return base.Draw(Graph, Offset, "Error: width/height");//GetString()
                    }

                    barcodeImage = new Bitmap(Width.InPixels(Graph.DpiX), Height.InPixels(Graph.DpiY)); ;                    
                    try
                    {
                        bc2d.SetCode(Value.Data);
                        if (bc2d is Barcode.BarcodePDF417)
                        {
                            barcodeImage = bc2d.DrawBarcode(5, 0, Width.InPixels(2400), Height.InPixels(2400));
                        }
                        else
                        {
                            //mve test: 
                            barcodeImage = bc2d.DrawBarcode(5, 0, Width.InPixels(Graph.DpiX), Height.InPixels(Graph.DpiY));
                        }
                        g = Graphics.FromImage(barcodeImage);
                    }
                    catch (Exception e)
                    {
                        Pen redPen = new Pen(Brushes.Red, 5);
                        System.Diagnostics.Debug.WriteLine("Unable to draw barcode: " + e.Message);                      
                        g.DrawLine(redPen, new Point(0, 0), new Point(barcodeImage.Width, barcodeImage.Height));
                        g.DrawLine(redPen, new Point(0, barcodeImage.Height), new Point(barcodeImage.Width, 0));
                        redPen.Dispose();
                    }
                }

                //GC.Collect();
                if (Rotation == 0)
                {
                    //mvetest
                    Graph.DrawImage(barcodeImage, new Point[] { new Point(X, Y), new Point(X + widthInPixels, Y), new Point(X, Y + heightInPixels) });

                    if (GlobalDataStore.GetInstance().DesignMode == true)
                    {
                        Pen greenPen = new Pen(Color.Green, 2);                        
                        Graph.DrawRectangle(greenPen, X, Y, Width.InPixels(Graph.DpiX), Height.InPixels(Graph.DpiY));                        
                        greenPen.Dispose();
                    }
                    
                    //mvetest
                    //Graph.DrawImage(barcodeImage, new Point[] { new Point(X, Y), new Point(X + widthInPixels, Y), new Point(X, Y + heightInPixels) });
                    
                }
                else if (Rotation == 90) 
                {
                    if (GlobalDataStore.GetInstance().DesignMode == true)
                    {
                        Pen greenPen = new Pen(Color.Green, 2);
                        Graph.DrawRectangle(greenPen, X - widthInPixels, Y, Height.InPixels(Graph.DpiX), Width.InPixels(Graph.DpiY));
                        greenPen.Dispose();
                    }                    
                    Graph.DrawImage(barcodeImage, new Point[] {new Point (X, Y),new Point (X ,Y + heightInPixels),new Point (X - widthInPixels, Y)});
                }
                else if (Rotation == 180)
                {
                    if (GlobalDataStore.GetInstance().DesignMode == true)
                    {
                        Pen greenPen = new Pen(Color.Green, 2);
                        Graph.DrawRectangle(greenPen, X - widthInPixels, Y - heightInPixels, Width.InPixels(Graph.DpiX), Height.InPixels(Graph.DpiY));
                        greenPen.Dispose();
                    }

                    Graph.DrawImage(barcodeImage, new Point[] {new Point (X , Y),new Point (X - widthInPixels, Y ),new Point (X,Y - heightInPixels)});
                }
                else if (Rotation == 270)
                {
                    if (GlobalDataStore.GetInstance().DesignMode == true)
                    {
                        Pen greenPen = new Pen(Color.Green, 2);
                        Graph.DrawRectangle(greenPen, X, Y - heightInPixels, Height.InPixels(Graph.DpiX), Width.InPixels(Graph.DpiY));
                        greenPen.Dispose();
                    }
                    
                    Graph.DrawImage(barcodeImage, new Point[] {new Point (X, Y),new Point (X ,Y - heightInPixels),new Point (X + widthInPixels, Y)});
                }
                Graph.Clip = r; // restore the clipping region. It has been messed up in this routine.
                barcodeImage.Dispose();
                //GC.Collect();
                return barRectangle;
            }
        }

        public class ImageField : Field
        {
            public enum ScaleStyle { Normal, Stretch }
            public enum ColorStyle { Color, Grayscale }

            public ScaleStyle Scale = ScaleStyle.Normal;
            public ColorStyle Color = ColorStyle.Color;
            public bool KeepRatio = true;
            //mve, autorotate
            public enum AutoRotateStyle { NoAutoRotate, AutoRotateClockwise, AutoRotateCounterClockwise}
            public AutoRotateStyle AutoRotate = AutoRotateStyle.NoAutoRotate;
            //*
            public string PicturesRootFolder = String.Empty;
            public ImageField(Tools.CoordinateSystem coordinateSystem)
                :base(coordinateSystem)
            {
            }

            public override void Parse(XmlNode node)
            {
                base.Parse(node);

                Scale = ScaleStyle.Normal;
                KeepRatio = true;
                Color = ColorStyle.Color;
                //mve, autorotate
                AutoRotate = AutoRotateStyle.NoAutoRotate;
                //*

                foreach (XmlNode nodex in node.ChildNodes)
                {
                    if (nodex.Name.Equals("imagestyle", StringComparison.OrdinalIgnoreCase))
                    {
                        foreach (XmlAttribute attrib in nodex.Attributes)
                        {
                            if (attrib.Name.Equals("style", StringComparison.OrdinalIgnoreCase))
                            {
                                if (attrib.Value.Equals("stretch", StringComparison.OrdinalIgnoreCase))
                                    Scale = ScaleStyle.Stretch;
                                else if (attrib.Value.Equals("normal", StringComparison.OrdinalIgnoreCase))
                                    Scale = ScaleStyle.Normal;
                            }
                            else if (attrib.Name.Equals("keepratio", StringComparison.OrdinalIgnoreCase))
                            {
                                KeepRatio = attrib.Value.Equals("true", StringComparison.OrdinalIgnoreCase);
                            }
                            // mve,autorotate
                            else if (attrib.Name.Equals("autorotate", StringComparison.OrdinalIgnoreCase))
                            {
                                if (attrib.Value.Equals("none", StringComparison.OrdinalIgnoreCase))
                                    AutoRotate = AutoRotateStyle.NoAutoRotate;
                                else if (attrib.Value.Equals("clockwise", StringComparison.OrdinalIgnoreCase))
                                    AutoRotate = AutoRotateStyle.AutoRotateClockwise;
                                else if (attrib.Value.Equals("counterclockwise", StringComparison.OrdinalIgnoreCase))
                                    AutoRotate = AutoRotateStyle.AutoRotateCounterClockwise;
                            }
                            //*
                            else if (attrib.Name.Equals("colorstyle", StringComparison.OrdinalIgnoreCase))
                            {
                                if (attrib.Value.Equals("grayscale", StringComparison.OrdinalIgnoreCase))
                                    Color = ColorStyle.Grayscale;
                                else if (attrib.Value.Equals("color", StringComparison.OrdinalIgnoreCase))
                                    Color = ColorStyle.Color;
                            }
                        }
                    }
                }
            }
                       
            /// <summary>
            /// Fast convert a bitmap to a grayscale bitmap using a standard grayscale 
            /// color bitmap.
            /// </summary>
            /// <param name="source">Source bitmap</param>
            /// <returns>Bitmap</returns>
            private Bitmap ConvertToGrayscale(Bitmap source)
            {/* Default matrix giving equal weight to each of the colors.
                ColorMatrix cm = new ColorMatrix(new float[][]{   
                                  new float[]{0.5f,0.5f,0.5f,0,0},
                                  new float[]{0.5f,0.5f,0.5f,0,0},
                                  new float[]{0.5f,0.5f,0.5f,0,0},
                                  new float[]{0,0,0,1,0,0},
                                  new float[]{0,0,0,0,1,0},
                                  new float[]{0,0,0,0,0,1}});
                */
                //The weights used here are given by the North America Television Standaard Commitee (NTSC)
                //and seem to deliver a good representation of the image.
                ColorMatrix cm =new ColorMatrix(new float[][]{   
                                  new float[]{0.299f,0.299f,0.299f,0,0},
                                  new float[]{0.587f,0.587f,0.587f,0,0},
                                  new float[]{0.114f,0.114f,0.114f,0,0},
                                  new float[]{0,0,0,1,0,0},
                                  new float[]{0,0,0,0,1,0},
                                  new float[]{0,0,0,0,0,1}});
                
                Bitmap newImage = new Bitmap(source.Width,source.Height);
                ImageAttributes ia = new ImageAttributes();
                ia.SetColorMatrix(cm);

                Graphics g = Graphics.FromImage(newImage);
                g.DrawImage(source, 
                            new Rectangle(0,0,source.Width,source.Height),
                            0,0,source.Width,source.Height,
                            GraphicsUnit.Pixel,ia);

                
                newImage.SetResolution(source.HorizontalResolution, source.VerticalResolution);

                return newImage;
            }

            public override Rectangle Draw(Graphics Graph, Point Offset, LabelSet Labels, int language, Boolean isSelectedField)
            {
                Label.Value Value;
                Rectangle returnRectangle = new Rectangle();
                string sID;
                if (this.ValueRef == null)
                {
                    sID = this.ID;
                }
                else
                {
                    sID = this.ValueRef;
                }

                Value = Labels.TryGetValue(sID,language);


                Region r;
                Rectangle imgRectangle;

                imgRectangle = new Rectangle(0, 0, 0, 0);

                string sDefaultPrintString = null;

                int widthInPixels = 0;  //Physical with and height (may depend on rotation!!
                int heightInPixels = 0; 

                r = Graph.Clip; //current clipping region


                Boolean isValueEmpty = false;
                if (Value != null)
                {
                    if (Value.Data != null)
                    {
                        if (Value.Data.Trim().Length > 0)
                            sDefaultPrintString = GetPictureFolderLocation() + Value.Data;
                        else
                            isValueEmpty = true;
                    }
                }

                if (!isValueEmpty)
                {
                    Bitmap tmpBitmap;
                    try
                    {
                        tmpBitmap = new Bitmap(sDefaultPrintString);
                    }
                    catch (Exception x)
                    {
                        System.Diagnostics.Debug.WriteLine(x.Message);
                        return base.Draw(Graph, Offset, "Image not found.");//GetString()
                    }

                    //A bitmap, opened from a file KEEPS a lock on the file during the lifetime
                    //of the bitmap. If you do not want this behaviour and want to release the
                    //image as soon as possible, take a memory copy of the bitmap and dispose the
                    //original image.

                    Bitmap helpBitmap;
                    helpBitmap = new Bitmap(tmpBitmap);
                    helpBitmap.SetResolution(tmpBitmap.HorizontalResolution, tmpBitmap.VerticalResolution);
                    tmpBitmap.Dispose();

                    Image theImage;
                    //if we need a gray picture we make it here
                    if (Color == ColorStyle.Grayscale)
                    {
                        Bitmap temp;
                        temp = ConvertToGrayscale(helpBitmap);
                        theImage = temp;
                    }
                    else
                    {
                        theImage = helpBitmap;
                    }


                    int X = (int)(PositionX.InPixels(Graph.DpiX) + Offset.X);
                    int Y = (int)(PositionY.InPixels(Graph.DpiY) + Offset.Y);

                    if ((Width != null) && (Height != null))
                    {
                        if (Rotation == 0)
                        {
                            widthInPixels = Width.InPixels(Graph.DpiX);
                            heightInPixels = Height.InPixels(Graph.DpiY);
                            imgRectangle = new Rectangle(X, Y, widthInPixels, heightInPixels);
                        }
                        else if (Rotation == 90)
                        {
                            widthInPixels = Height.InPixels(Graph.DpiX);
                            heightInPixels = Width.InPixels(Graph.DpiY);
                            imgRectangle = new Rectangle(X - widthInPixels, Y, widthInPixels, heightInPixels);
                        }
                        else if (Rotation == 180)
                        {
                            widthInPixels = Width.InPixels(Graph.DpiX);
                            heightInPixels = Height.InPixels(Graph.DpiY);
                            imgRectangle = new Rectangle(X - widthInPixels, Y - heightInPixels, widthInPixels, heightInPixels);
                        }
                        else if (Rotation == 270)
                        {
                            widthInPixels = Height.InPixels(Graph.DpiX);
                            heightInPixels = Width.InPixels(Graph.DpiY);
                            imgRectangle = new Rectangle(X, Y - heightInPixels, widthInPixels, heightInPixels);
                        }

                        //Determine the visible part of this rectangle within the current clipping area
                        //of the label. Take the resulting rectangle as the new clipping rectangle.
                        Graph.SetClip(imgRectangle, System.Drawing.Drawing2D.CombineMode.Intersect);
                        //For testing to see the calculated rectangle...
                        if (GlobalDataStore.GetInstance().DesignMode == true)
                            Graph.FillRegion(Brushes.LightGray, Graph.Clip);
                        if (isSelectedField)
                        {
                            Graph.FillRegion(Brushes.Blue, Graph.Clip);
                        }
                        returnRectangle = imgRectangle;
                    } else
                    {
                        if (AutoRotate != AutoRotateStyle.NoAutoRotate)
                        {
                            //no scaling, width = null and height=null
                            widthInPixels = theImage.Width;
                            heightInPixels = theImage.Height;
                            imgRectangle = new Rectangle(X, Y, widthInPixels, heightInPixels);
                            returnRectangle = Rectangle.Round( r.GetBounds(Graph));                            
                        }
                    }

                    if ((Width == null) || (Height == null) || (Width.length == 0) || (Height.length == 0))
                    {
                        if (Scale == ScaleStyle.Stretch)
                        {
                            //When you have no width and/or heigt, you cannot stretch.
                            //You have to select normal mode.

                            return base.Draw(Graph, Offset, "Er! W/H/Style");//GetString()
                        }
                    }

                    //mve, autorotate
                    if ( (AutoRotate != AutoRotateStyle.NoAutoRotate) &&
                         (returnRectangle.Width != returnRectangle.Height) &&
                         (theImage.Width != theImage.Height) )
                    {
                        bool bRectangleLandscape = false;
                        bool bImageLandscape = false;
                        if (returnRectangle.Width > returnRectangle.Height)
                        {
                            bRectangleLandscape = true;
                        }
                        if  (theImage.Width > theImage.Height)
                        {
                            bImageLandscape = true;
                        }
                        if (bRectangleLandscape != bImageLandscape)
                        {
                            //We need to rotate the image...

                            switch (AutoRotate)
                            {
                                case AutoRotateStyle.AutoRotateClockwise:
                                    theImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
                                    break;
                                case AutoRotateStyle.AutoRotateCounterClockwise:
                                    theImage.RotateFlip(RotateFlipType.Rotate270FlipNone);
                                    break;
                            }
                        }
                    }
                    // *

                    switch (Scale)
                    {
                        case ScaleStyle.Normal:
                            //Here we use DrawImageUnscaled to avoid DPI problems.
                            //We move X and Y to have the same kind of rotation for each object.

                            if (Rotation == 0)
                            {
                            }
                            else if (Rotation == 90)
                            {
                                theImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
                                X = X - (int)((int)(theImage.Width) / theImage.HorizontalResolution * Graph.DpiX);
                            }
                            else if (Rotation == 180)
                            {
                                theImage.RotateFlip(RotateFlipType.Rotate180FlipNone);
                                X = X - (int)((int)(theImage.Width) / theImage.HorizontalResolution * Graph.DpiX);
                                Y = Y - (int)((int)(theImage.Height) / theImage.VerticalResolution * Graph.DpiY);
                            }
                            else if (Rotation == 270)
                            {
                                theImage.RotateFlip(RotateFlipType.Rotate270FlipNone);
                                Y = Y - (int)((int)(theImage.Height) / theImage.VerticalResolution * Graph.DpiY);
                            }
                            returnRectangle = new Rectangle(X, Y, theImage.Width, theImage.Height);
                            Graph.DrawImageUnscaled(theImage, X, Y);
                            break;
                        case ScaleStyle.Stretch:
                            if (KeepRatio)
                            {
                                SizeF sizef = new SizeF(theImage.Width, theImage.Height);
                                if (Rotation == 0)
                                {
                                    float fScale = Math.Min(widthInPixels / sizef.Width, heightInPixels / sizef.Height);
                                    sizef.Width *= fScale; // = How wide the image should become
                                    sizef.Height *= fScale; // = How high the image should become

                                    returnRectangle = new Rectangle((int)(X + (widthInPixels - sizef.Width) / 2), (int)(Y + (heightInPixels - sizef.Height) / 2), (int)sizef.Width, (int)sizef.Height);
                                    Graph.DrawImage(theImage, X + (widthInPixels - sizef.Width) / 2, Y + (heightInPixels - sizef.Height) / 2, sizef.Width, sizef.Height);
                                }
                                else if (Rotation == 90)
                                {
                                    float fScale = Math.Min(widthInPixels / sizef.Height, heightInPixels / sizef.Width);
                                    sizef.Width *= fScale;
                                    sizef.Height *= fScale;

                                    if (widthInPixels > heightInPixels)
                                    {
                                        X = X - (int)((widthInPixels / 2) - (sizef.Height / 2));
                                        Y = Y - (int)((heightInPixels / 2) - (sizef.Width / 2));
                                    }
                                    else
                                    {
                                        X = X + (int)((widthInPixels / 2) - (sizef.Height / 2));
                                        Y = Y + (int)((heightInPixels / 2) - (sizef.Width / 2));
                                    }
                                    Graph.DrawImage(theImage, new PointF[] { new PointF(X, Y), new PointF(X, Y + sizef.Width), new PointF(X - sizef.Height, Y) });
                                }
                                else if (Rotation == 180)
                                {
                                    float fScale = Math.Min(widthInPixels / sizef.Width, heightInPixels / sizef.Height);
                                    sizef.Width *= fScale;
                                    sizef.Height *= fScale;

                                    if (widthInPixels > heightInPixels)
                                    {
                                        X = X - (int)((widthInPixels / 2) - (sizef.Width / 2));
                                        Y = Y - (int)((heightInPixels / 2) - (sizef.Height / 2));
                                    }
                                    else
                                    {
                                        X = X - (int)((widthInPixels / 2) - (sizef.Width / 2));
                                        Y = Y - (int)((heightInPixels / 2) - (sizef.Height / 2));
                                    }
                                    Graph.DrawImage(theImage, new PointF[] { new PointF(X, Y), new PointF(X - sizef.Width, Y), new PointF(X, Y - sizef.Height) });
                                }
                                else if (Rotation == 270)
                                {
                                    float fScale = Math.Min(widthInPixels / sizef.Height, heightInPixels / sizef.Width);
                                    sizef.Width *= fScale;
                                    sizef.Height *= fScale;

                                    if (widthInPixels > heightInPixels)
                                    {
                                        X = X + (int)((widthInPixels / 2) - (sizef.Height / 2));
                                        Y = Y + (int)((heightInPixels / 2) - (sizef.Width / 2));
                                    }
                                    else
                                    {
                                        X = X - (int)((widthInPixels / 2) - (sizef.Height / 2));
                                        Y = Y - (int)((heightInPixels / 2) - (sizef.Width / 2));
                                    }
                                    Graph.DrawImage(theImage, new PointF[] { new PointF(X, Y), new PointF(X, Y - sizef.Width), new PointF(X + sizef.Height, Y) });
                                }
                            }
                            else
                            {
                                if (Rotation == 0)
                                {
                                    Graph.DrawImage(theImage, new Point[] { new Point(X, Y), new Point(X + widthInPixels, Y), new Point(X, Y + heightInPixels) }, new Rectangle(0, 0, theImage.Width, theImage.Height), GraphicsUnit.Pixel);
                                }
                                else if (Rotation == 90)
                                {
                                    Graph.DrawImage(theImage, new Point[] { new Point(X, Y), new Point(X, Y + heightInPixels), new Point(X - widthInPixels, Y) }, new Rectangle(0, 0, theImage.Width, theImage.Height), GraphicsUnit.Pixel);
                                }
                                else if (Rotation == 180)
                                {
                                    Graph.DrawImage(theImage, new Point[] { new Point(X, Y), new Point(X - widthInPixels, Y), new Point(X, Y - heightInPixels) }, new Rectangle(0, 0, theImage.Width, theImage.Height), GraphicsUnit.Pixel);
                                }
                                else if (Rotation == 270)
                                {
                                    Graph.DrawImage(theImage, new Point[] { new Point(X, Y), new Point(X, Y - heightInPixels), new Point(X + widthInPixels, Y) }, new Rectangle(0, 0, theImage.Width, theImage.Height), GraphicsUnit.Pixel);
                                }
                            }
                            break;
                    }
                    Graph.Clip = r; // restore the clipping region. It has been messed up in this routine.
                    theImage.Dispose();
                    helpBitmap.Dispose();
                }
                return returnRectangle;
            }

            public static string GetPictureFolderLocation()
            {
                string pictureFolderLocation;
                string appPath = GlobalDataStore.AppPath;
                
                string ConfigFilePath = Path.Combine(appPath,"ACALabelXClient.config.xml");
                if (!(File.Exists(ConfigFilePath)))
                {
                    try
                    {
                        ConfigFilePath = System.Configuration.ConfigurationSettings.AppSettings["ConfigXML"].ToString();
                        //ConfigFilePath = System.Configuration.ConfigurationManager.AppSettings["ConfigXML"].ToString();                                                    
                    }
                    catch (NullReferenceException )
                    {
                        pictureFolderLocation = "";
                        return pictureFolderLocation;
                    }
                }

                Toolbox.Toolbox tb;
                tb = new Toolbox.Toolbox();
                pictureFolderLocation = tb.GetGeneralClientPicturesFolder(ConfigFilePath);

                return pictureFolderLocation;
            }
            public bool CheckImageAvailable(LabelSet Labels, int language, bool MustLog)
            {
                Label.Value Value;                
                string sID;
                if (this.ValueRef == null)
                {
                    sID = this.ID;
                }
                else
                {
                    sID = this.ValueRef;
                }

                Value = Labels.TryGetValue(sID, language);
                
                string sDefaultPrintString = null;
                
                if (Value != null)
                {
                    if (Value.Data != null)
                    {
                        if (Value.Data.Trim().Length > 0)
                            sDefaultPrintString = GetPictureFolderLocation() + Value.Data;
                    }
                }
                bool exists = false;
                exists = File.Exists(sDefaultPrintString);
                if (MustLog && (!exists))
                {
                    GlobalDataStore.Logger.Warning(string.Format("Missing picture {0}.",sDefaultPrintString));
                }
                return exists;
            }            
        }
        
        public class CutField : Field
        {
            public UInt32 LabelCount = 0;
            public bool FullCut = true;

            public CutField(Tools.CoordinateSystem coordinateSystem)
                :base(coordinateSystem)
            {
            }

            public override void Parse(XmlNode node)
            {
                base.Parse(node);

                FullCut = true;
                LabelCount = 0;

                foreach (XmlAttribute attrib in node.Attributes)
                {
                    if (attrib.Name.Equals("labelcount", StringComparison.OrdinalIgnoreCase))
                    {
                        LabelCount = Convert.ToUInt32(attrib.Value);
                    }
                    else if (attrib.Name.Equals("type", StringComparison.OrdinalIgnoreCase))
                    {
                        FullCut = attrib.Value.Equals("full", StringComparison.OrdinalIgnoreCase);
                    }
                }
            }
        }

        public String ID = null;
        public List<PaperType> PaperTypes = new List<PaperType>();
        public Tools.CoordinateSystem coordinateSystem = new Tools.CoordinateSystem();
        public IDictionary<string, FontX> fonts = new Dictionary<string, FontX>();
        public IDictionary<string, Field> fields = new Dictionary<string, Field>();
        public Label DefaultLabel = null;
        public string luaCode = "";

        public bool CheckAllPicturesAvailable(LabelSet Labels,int language, bool MustLog)
        {
            foreach (LabelDef.Field field in fields.Values){
                if (field is LabelDef.ImageField)
                {
                    LabelDef.ImageField imagefield = (LabelDef.ImageField)field;
                    if (!imagefield.CheckImageAvailable(Labels,language,MustLog))
                    {
                        return false; //image is missing
                    }
                }
            }
            
            return true; //all images were found
        }
        public void Draw(Graphics Graph, Rectangle Rect, LabelSet Labels, int Language, Boolean isSelectedField)
        {

            foreach (KeyValuePair<string, Field> KeyValuePair in fields)
            {
                Field field = KeyValuePair.Value;
                Label.Value value = null;
                string sID;
                if (field.ValueRef == null)
                {
                    sID = field.ID;
                }
                else
                {
                    sID = field.ValueRef;
                }
                value = null;
                value = Labels.TryGetValue(sID, Language);
                
                if ( (value == null) && (!(field is TextFieldGroup) ) )
                    continue;

                //To draw a field collection we need some additional info.
                field.Draw(Graph, new Point(Rect.Left, Rect.Top), Labels, Language,isSelectedField);
                
            }
        }

        public void Parse(string FilePath)
        {
            PaperTypes.Clear();

            //mve optimize
            if (fonts.Count > 0)
            {
                foreach (FontX f in fonts.Values)
                {
                    f.Dispose();
                }
            }
            //mve optimize
            fonts.Clear();

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(FilePath);
            XmlNodeList LabelDefs = xDoc.GetElementsByTagName("labeldef");

            foreach (XmlNode LabelDef in LabelDefs)
            {
                foreach (XmlAttribute attrib in LabelDef.Attributes)
                {
                    if (attrib.Name.Equals("id", StringComparison.OrdinalIgnoreCase))
                    {
                        ID = attrib.Value;
                    }
                }

                foreach (XmlNode nodexx in LabelDef.ChildNodes)
                {
                    if (nodexx.Name.Equals("validpapertypes", StringComparison.OrdinalIgnoreCase))
                    {
                        foreach (XmlNode nodexxx in nodexx.ChildNodes)
                        {
                            if (nodexxx.Name.Equals("Paper", StringComparison.OrdinalIgnoreCase))
                            {
                                PaperType paperType = new PaperType();
                                paperType.Parse(nodexxx);                                
                                PaperTypes.Add(paperType);
                            }
                        }
                    }
                    else if (nodexx.Name.Equals("coordinates", StringComparison.OrdinalIgnoreCase))
                    {
                        coordinateSystem.Parse(nodexx);
                    }
                    else if (nodexx.Name.Equals("definition", StringComparison.OrdinalIgnoreCase))
                    {
                        foreach (XmlNode nodexxx in nodexx.ChildNodes)
                        {
                            if (nodexxx.Name.Equals("fonts", StringComparison.OrdinalIgnoreCase))
                            {
                                foreach (XmlNode nodexxxx in nodexxx.ChildNodes)
                                {
                                    if (nodexxxx.Name.Equals("font", StringComparison.OrdinalIgnoreCase))
                                    {
                                        FontX font = new FontX();
                                        font.Parse(nodexxxx);                                        
                                        fonts.Add(font.ID, font);
                                    }
                                }
                            }
                            else if (nodexxx.Name.Equals("fields", StringComparison.OrdinalIgnoreCase))
                            {
                                foreach (XmlNode nodexxxx in nodexxx.ChildNodes)
                                {
                                    if (nodexxxx.Name.Equals("textfield", StringComparison.OrdinalIgnoreCase))
                                    {
                                        TextField field = new TextField(coordinateSystem);
                                        field.Parse(nodexxxx, ref fonts);
                                        fields.Add(field.ID, field);
                                    }
                                    else if (nodexxxx.Name.Equals("textconcat", StringComparison.OrdinalIgnoreCase))
                                    {
                                        TextFieldGroup field = new TextFieldGroup(coordinateSystem, TextFieldGroup.ConcatMethod.Horizontal);
                                        field.Parse(nodexxxx, ref fonts);
                                        fields.Add(field.ID, field);
                                    }
                                    else if (nodexxxx.Name.Equals("multiline", StringComparison.OrdinalIgnoreCase))
                                    {
                                        TextFieldGroup field = new TextFieldGroup(coordinateSystem, TextFieldGroup.ConcatMethod.Vertical);
                                        field.Parse(nodexxxx, ref fonts);
                                        fields.Add(field.ID, field);
                                    }
                                    else if (nodexxxx.Name.Equals("barcodefield", StringComparison.OrdinalIgnoreCase))
                                    {
                                        BarcodeField field = new BarcodeField(coordinateSystem);
                                        field.Parse(nodexxxx);
                                        fields.Add(field.ID, field);
                                    }
                                    else if (nodexxxx.Name.Equals("imagefield", StringComparison.OrdinalIgnoreCase))
                                    {
                                        ImageField field = new ImageField(coordinateSystem);
                                        field.Parse(nodexxxx);
                                        fields.Add(field.ID, field);
                                    }
                                    else if (nodexxxx.Name.Equals("cut", StringComparison.OrdinalIgnoreCase))
                                    {
                                        CutField field = new CutField(coordinateSystem);
                                        field.Parse(nodexxxx);
                                        fields.Add(field.ID, field);
                                    }
                                }
                            }
                        }
                    }
                    else if (nodexx.Name.Equals("label", StringComparison.OrdinalIgnoreCase))
                    {
                        DefaultLabel = new Label();
                        DefaultLabel.Parse(nodexx);
                    }
                    else if (nodexx.Name.Equals("lua", StringComparison.OrdinalIgnoreCase))
                    {
                        luaCode = nodexx.InnerText;
                    }
                }
            }
            return;
        }
    }

    public class LabelList : List<Label>
    {
        public uint CountAll
        {
            get
            {
                uint totaal = 0;
                foreach (Label a in this)
                {
                    totaal += a.Quantity;
                }
                return totaal;
            }
        }
    }
    
}
/*
 AcaLabelPrint Copyright (C) 2011  Retailium Software Development BV.
 This program comes with ABSOLUTELY NO WARRANTY; 
 This is free Software, and you are welcome to redistribute it
 under certain conditions. See the License.txt file or 
 GNU GPL 3.0 License at <http://www.gnu.org/licenses>*/
