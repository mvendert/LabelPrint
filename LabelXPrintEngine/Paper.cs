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

namespace ACA.LabelX.Paper
{
    public class Offset
    {
        public Tools.Length TopMarginOffset = null;
        public Tools.Length LeftMarginOffset = null;
        public Tools.Length HorizontalInterlabelGapOffset = null;
        public Tools.Length VerticalInterlabelGapOffset = null;
        public String Machine = null;
        public String Printer = null;
        public Tools.CoordinateSystem.Units DefaultUnits = null;

        public Offset(Tools.CoordinateSystem.Units Units)
        {
            DefaultUnits = Units;
        }

        public Offset(System.Drawing.GraphicsUnit Unit)
        {
            DefaultUnits = new ACA.LabelX.Tools.CoordinateSystem.Units(Unit);
        }

        public void Parse(XmlNode node)
        {
            TopMarginOffset = null;
            LeftMarginOffset = null;
            HorizontalInterlabelGapOffset = null;
            VerticalInterlabelGapOffset = null;
            Machine = null;
            Printer = null;

            foreach (XmlAttribute attrib in node.Attributes)
            {
                if (attrib.Name.Equals("horzoffset", StringComparison.OrdinalIgnoreCase))
                {
                    LeftMarginOffset = new Tools.Length(Convert.ToInt32(attrib.Value), DefaultUnits);
                }
                else if (attrib.Name.Equals("vertoffset", StringComparison.OrdinalIgnoreCase))
                {
                    TopMarginOffset = new Tools.Length(Convert.ToInt32(attrib.Value), DefaultUnits);
                }
                else if (attrib.Name.Equals("horzinterlabelgapdelta", StringComparison.OrdinalIgnoreCase))
                {
                    HorizontalInterlabelGapOffset = new Tools.Length(Convert.ToInt32(attrib.Value), DefaultUnits);
                }
                else if (attrib.Name.Equals("vertinterlabeldelta", StringComparison.OrdinalIgnoreCase))
                {
                    VerticalInterlabelGapOffset = new Tools.Length(Convert.ToInt32(attrib.Value), DefaultUnits);
                }
                else if (attrib.Name.Equals("machine", StringComparison.OrdinalIgnoreCase))
                {
                    Machine = attrib.Value;
                }
                else if (attrib.Name.Equals("printer", StringComparison.OrdinalIgnoreCase))
                {
                    Printer = attrib.Value;
                }
            }
        }

    }

    public class LabelLayout
    {
        public UInt32 HorizontalCount = 0;
        public UInt32 VerticalCount = 0;
        public Tools.Length TopMargin = null;
        public Tools.Length LeftMargin = null;
        public Tools.Length HorizontalInterlabelGap = null;
        public Tools.Length VerticalInterlabelGap = null;
        public Tools.CoordinateSystem.Units DefaultUnits = null;

        public LabelLayout(Tools.CoordinateSystem.Units DefaultUnits)
        {
            this.DefaultUnits = DefaultUnits;
        }

        public LabelLayout(System.Drawing.GraphicsUnit unit)
        {
            this.DefaultUnits = new ACA.LabelX.Tools.CoordinateSystem.Units(unit);
        }

        public void Parse(XmlNode node)
        {
            HorizontalCount = 0;
            VerticalCount = 0;
            TopMargin = null;
            LeftMargin = null;
            HorizontalInterlabelGap = null;
            VerticalInterlabelGap = null;

            foreach (XmlNode nodex in node)
            {
                if (nodex.Name.Equals("horizontal", StringComparison.OrdinalIgnoreCase))
                    HorizontalCount = Convert.ToUInt32(nodex.InnerText);
                else if (nodex.Name.Equals("vertical", StringComparison.OrdinalIgnoreCase))
                    VerticalCount = Convert.ToUInt32(nodex.InnerText);
                else if (nodex.Name.Equals("horzoffset", StringComparison.OrdinalIgnoreCase))
                {
                    LeftMargin = new ACA.LabelX.Tools.Length(DefaultUnits);
                    LeftMargin.Parse(nodex);
                }
                else if (nodex.Name.Equals("vertoffset", StringComparison.OrdinalIgnoreCase))
                {
                    TopMargin = new ACA.LabelX.Tools.Length(DefaultUnits);
                    TopMargin.Parse(nodex);
                }
                else if (nodex.Name.Equals("horzinterlabelgap", StringComparison.OrdinalIgnoreCase))
                {
                    HorizontalInterlabelGap = new ACA.LabelX.Tools.Length(DefaultUnits);
                    HorizontalInterlabelGap.Parse(nodex);
                }
                else if (nodex.Name.Equals("vertinterlabelgap", StringComparison.OrdinalIgnoreCase))
                {
                    VerticalInterlabelGap = new ACA.LabelX.Tools.Length(DefaultUnits);
                    VerticalInterlabelGap.Parse(nodex);
                }
            }
        }

    }

    public class PaperDef
    {
        public String ID = "";
        public Tools.CoordinateSystem coordinateSystem = null;
        public Tools.Size size = null;
        public LabelLayout labelLayout = null;
        public IDictionary<string, Offset> Offsets = new Dictionary<string, Offset>();
        public String Machine = "";
        public String Printer = "";

        //
        //When the physical printing takes place some printers have a left and top
        //margin, which the printer can never print upon. In .NET these printing always
        //take place in respect to these margins. 0,0 actually is mapped upon
        //physical coordinates PhysicalLeftMargin, PhysicalTopMargin. So if we describe
        //our page and we do this in mm, it is best to subtract these physical margings
        //from the given coordinates, so all is where expected.
        //But because this is not done before printing (als we only known the margins then
        //it is set upon printing in the paperdef. When retrieving coordinates we subtract this value.
        //
        public ACA.LabelX.Tools.Length PhysicalLeftMargin;
        public ACA.LabelX.Tools.Length PhysicalTopMargin;

        public PaperDef()
        {
            PhysicalTopMargin = new Tools.Length(0,new ACA.LabelX.Tools.CoordinateSystem.Units(System.Drawing.GraphicsUnit.Millimeter));
            PhysicalLeftMargin = new Tools.Length(0, new ACA.LabelX.Tools.CoordinateSystem.Units(System.Drawing.GraphicsUnit.Millimeter));
        }

        public void SetDestination(String Machine, String Printer)
        {
            this.Machine = Machine;
            this.Printer = Printer;
        }

        public Tools.Length GetLeftMargin()
        {
            Tools.Length LeftMargin = labelLayout.LeftMargin;
            
            
            String OffsetKey = string.Format("{0}@{1}", Printer, Machine);
            if (Offsets.ContainsKey(OffsetKey)) // Use specific offset if it exists
            {
                Offset offset = Offsets[OffsetKey];
                LeftMargin += offset.LeftMarginOffset;
            }
            else if (Offsets.ContainsKey("@")) //Use default offset if no specific one exists.
            {
                Offset offset = Offsets["@"];
                LeftMargin += offset.LeftMarginOffset;
            }
            //if (LeftMargin > PhysicalLeftMargin)
            //{
                LeftMargin -= PhysicalLeftMargin;
            //}
            //else
            //{
            //    LeftMargin = new Tools.Length(0, new ACA.LabelX.Tools.CoordinateSystem.Units(System.Drawing.GraphicsUnit.Millimeter));
            //}
            
            return LeftMargin;
        }

        public Tools.Length GetTopMargin()
        {
            Tools.Length TopMargin = labelLayout.TopMargin;
            String OffsetKey = string.Format("{0}@{1}", Printer, Machine);

            if (Offsets.ContainsKey(OffsetKey)) // Use specific offset if it exists
            {
                Offset offset = Offsets[OffsetKey];
                TopMargin += offset.TopMarginOffset;
            }
            else if (Offsets.ContainsKey("@")) //Use default offset if no specific one exists.
            {
                Offset offset = Offsets["@"];
                TopMargin += offset.TopMarginOffset;
            }
            //if (TopMargin > PhysicalTopMargin)
            //{
                TopMargin -= PhysicalTopMargin;
            //}
            //else
            //{
            //    TopMargin = new Tools.Length(0, new Tools.CoordinateSystem.Units(System.Drawing.GraphicsUnit.Millimeter));
            //}
            
            return TopMargin;
        }

        public Tools.Length GetRightMargin()
        {
            return labelLayout.LeftMargin;
            //return new Tools.Length(0, new Tools.CoordinateSystem.Units(System.Drawing.GraphicsUnit.Millimeter));
        }

        public Tools.Length GetBottomMargin()
        {
            return labelLayout.TopMargin;
            //return new Tools.Length(0, new Tools.CoordinateSystem.Units(System.Drawing.GraphicsUnit.Millimeter));
        }

        public Tools.Length GetHorizontalInterlabelGap()
        {
            Tools.Length HorizontalInterlabelGap = labelLayout.HorizontalInterlabelGap;
            //String OffsetKey = string.Format("{0}@{1}", Printer, Machine);
            //if (Offsets.ContainsKey(OffsetKey))
            //{
            //    Offset offset = Offsets[OffsetKey];
            //    HorizontalInterlabelGap += offset.HorizontalInterlabelGapOffset;
            //}
            return HorizontalInterlabelGap;
        }

        public Tools.Length GetVerticalInterlabelGap()
        {
            Tools.Length VerticalInterlabelGap = labelLayout.VerticalInterlabelGap;
            //String OffsetKey = string.Format("{0}@{1}", Printer, Machine);
            //if (Offsets.ContainsKey(OffsetKey))
            //{
            //    Offset offset = Offsets[OffsetKey];
            //    VerticalInterlabelGap += offset.VerticalInterlabelGapOffset;
            //}
            return VerticalInterlabelGap;
        }

        public Tools.Size GetPrintablePageSize()
        {
            if (labelLayout == null)
                throw new ApplicationException("Please, parse a paper definition first");

            Tools.Length NettoWidthPage = size.Width - (labelLayout.LeftMargin * 2);
            Tools.Length NettoHeightPage = size.Height - (labelLayout.TopMargin *2);
            /* //JBOS            
            Tools.Length NettoWidthPage = size.Width - (GetLeftMargin()+ GetRightMargin());
            Tools.Length NettoHeightPage = size.Height - (GetTopMargin()+GetBottomMargin());
            */ //JBOS
            return new ACA.LabelX.Tools.Size(NettoWidthPage, NettoHeightPage);
        }

        public Tools.Size GetPhysicalLabelSize()
        {
            if (labelLayout == null)
                throw new ApplicationException("Please, parse a paper definition first");

            Tools.Length NettoWidthPage = size.Width;
            Tools.Length NettoHeightPage = size.Height;
            return new ACA.LabelX.Tools.Size(NettoWidthPage, NettoHeightPage);
  
        }
        public Tools.Size GetLabelSize()
        {
            if (labelLayout == null)
                throw new ApplicationException("Please, parse a paper definition first");

            Tools.Size NettoPageSize = GetPrintablePageSize();

            Tools.Length TotalHorizontalInterlabelGap = GetHorizontalInterlabelGap() * (int)(labelLayout.HorizontalCount - 1);
            Tools.Length LabelWidth = (NettoPageSize.Width - TotalHorizontalInterlabelGap) / (int)labelLayout.HorizontalCount;

            Tools.Length TotalVerticalInterlabelGap = GetVerticalInterlabelGap() * (int)(labelLayout.VerticalCount - 1);
            Tools.Length LabelHeight = (NettoPageSize.Height - TotalVerticalInterlabelGap) / (int)labelLayout.VerticalCount;

            return new Tools.Size(LabelWidth, LabelHeight);
        }

        public Tools.Rectangle GetLabelRectangle(int HorizontalIndex, int VerticalIndex)
        {
            if (labelLayout == null)
                throw new ApplicationException("Please, parse a paper definition first");

            Tools.Length Left = GetLeftMargin() + ((GetLabelSize().Width + GetHorizontalInterlabelGap()) * HorizontalIndex);
            Tools.Length Top = GetTopMargin() + ((GetLabelSize().Height + GetVerticalInterlabelGap()) * VerticalIndex);

            Tools.Rectangle rect = new Tools.Rectangle(Left, Top, GetLabelSize());
            return rect;
        }

        public void Parse(string FilePath)
        {
            ID = "";
            coordinateSystem = null;
            labelLayout = null;

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(FilePath);
            XmlNodeList node = xDoc.GetElementsByTagName("paper");

            foreach (XmlNode nodex in node)
            {
                foreach (XmlAttribute attrib in nodex.Attributes)
                {
                    if (attrib.Name.Equals("type", StringComparison.OrdinalIgnoreCase))
                    {
                        ID = attrib.Value;
                    }
                }

                foreach (XmlNode nodexx in nodex.ChildNodes)
                {
                    if (nodexx.Name.Equals("coordinates", StringComparison.OrdinalIgnoreCase))
                    {
                        coordinateSystem = new Tools.CoordinateSystem();
                        coordinateSystem.Parse(nodexx);
                    }
                    else if (nodexx.Name.Equals("size", StringComparison.OrdinalIgnoreCase))
                    {
                        size = new ACA.LabelX.Tools.Size(0, 0, coordinateSystem.units);
                        size.Parse(nodexx);
                    }
                    else if (nodexx.Name.Equals("labelpos", StringComparison.OrdinalIgnoreCase))
                    {
                        labelLayout = new LabelLayout(coordinateSystem.units);
                        labelLayout.Parse(nodexx);
                    }
                    else if (nodexx.Name.Equals("offsets", StringComparison.OrdinalIgnoreCase))
                    {
                        foreach (XmlNode nodexxx in nodexx)
                        {
                            if (nodexxx.Name.Equals("offset", StringComparison.OrdinalIgnoreCase))
                            {
                                Offset offset = new Offset(coordinateSystem.units);
                                offset.Parse(nodexxx);

                                Offsets.Add(string.Format("{0}@{1}", offset.Printer, offset.Machine), offset);
                            }
                        }
                    }
                }
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
