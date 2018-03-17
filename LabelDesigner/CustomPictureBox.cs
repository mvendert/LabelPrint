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
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using ACA.LabelX.Tools;
using ACA.LabelX.Paper;
using ACA.LabelX.Label;
using ACA.LabelX.PrintEngine;
using ACA.LabelX.Toolbox;
using System.Drawing.Drawing2D;

namespace LabelDesigner
{
    class CustomPictureBox : PictureBox
    {
        private IDictionary<string, LabelDef.Field> drawingreferenceList = new Dictionary<string, LabelDef.Field>();
        public int labelWidth = 0;
        public int labelHeight = 0;
        private Graphics Graph = null;
        private string selectedFieldId = "";
        private int selectX = -999;
        private int selectY = -999;
        private float zoomFactor = 1.0f;
        private float rotation = 0.0f;
        private bool useWhiteBackground;

        public bool UseWhiteBackground
        {
            get { return useWhiteBackground; }
            set { useWhiteBackground = value; }
        }

        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        public float ZoomFactor
        {
            get { return zoomFactor; }
            set
            {
                if (value < 0.5f)
                {
                    zoomFactor = 0.5f;
                }
                else
                {
                    if (value > 10.0f)
                    {
                        zoomFactor = 10.0f;
                    }
                    else
                    {
                        zoomFactor = value;
                    }
                }
            }
        }

        private Boolean detectedClickToSelect = false;
        private string fieldIDToSelect = "";
        public string FieldIDToSelect
        {
            get {
                string temp = fieldIDToSelect;
                fieldIDToSelect = "";
                return temp; 
            }
            set { fieldIDToSelect = value; }
        }
        private System.Drawing.Rectangle returnRectangle;
        public System.Drawing.Rectangle ReturnRectangle
        {
            get { return returnRectangle; }
            set { returnRectangle = value; }
        }
        public string SelectedFieldId
        {
            get { return selectedFieldId; }
            set { selectedFieldId = value; }
        }
        private LabelSet labelset = new LabelSet();
        public LabelSet Labelset
        {
            get { return labelset; }
            set { labelset = value; }
        }
        public IDictionary<string, LabelDef.Field> DrawingreferenceList
        {
            get { return drawingreferenceList; }
            set { drawingreferenceList = value; }
        }

        //Constructors
        public CustomPictureBox() { }
        public CustomPictureBox(int labelWidth, int labelHeight)
        {
            this.labelHeight = labelHeight;
            this.labelWidth = labelWidth;
        }
        public CustomPictureBox(int labelWidth, int labelHeight, LabelSet labelset)
        {
            this.labelHeight = labelHeight;
            this.labelWidth = labelWidth;
            this.labelset = labelset;
        }

        //Main Function
        protected override void OnPaint(PaintEventArgs e)
        {
            
            base.OnPaint(e);
            Graph = e.Graphics;
            float TranslateX = 0.0f;
            float TranslateY = 0.0f;

            //System.Diagnostics.Debug.WriteLine(string.Format("width {0}, height {1}", this.Size.Width, this.Size.Height));
            if (useWhiteBackground)
            {
                Graph.FillRectangle(Brushes.White, 0, 0, this.Size.Width, this.Size.Height); //Color label white
            }
            else
            {
                HatchBrush bb = new HatchBrush(HatchStyle.LargeCheckerBoard, Color.LightGray, Color.White);
                Graph.FillRectangle(bb/*Brushes.White*/, 0, 0, this.Size.Width, this.Size.Height); //Color label white
                bb.Dispose();
            }
            //Graph.DrawRectangle(new Pen(Brushes.CadetBlue), 0, 0, this.Size.Width, this.Size.Height); //showit, edge filling...
            //mve,1.3.2
            Matrix mat = new Matrix();
            mat.Scale(zoomFactor, zoomFactor, MatrixOrder.Append);
            Graph.Transform = mat;
            if (rotation != 0)
            {
                Graph.RotateTransform(rotation, MatrixOrder.Append);
                if (rotation > 0)
                {
                    TranslateX = 0.0f;// -Size.Width / 2;
                    TranslateY = -Size.Width / zoomFactor;
                }
                else
                {
                    TranslateX = -Size.Height / zoomFactor;
                    TranslateY = 0.0f;
                }
                Graph.TranslateTransform(TranslateX, TranslateY);
            }
            mat.Dispose();
            mat = Graph.Transform;
            Boolean isSelectedField = false;
            foreach (LabelDef.Field field in drawingreferenceList.Values) //Draw each element in drawingreferenceList
            {
                field.BaseTransformation = mat;
                //mve,1.3.2
                Graph.ResetTransform(); //Al drawing routines can perform transforms. Reset here to start fresh.
                //if (zoomFactor != 1.0f)
                //{                    
                    Graph.Transform = mat;
                //    if (rotation != 0)
                //    {
                //        Graph.RotateTransform(rotation, MatrixOrder.Append);
                //        Graph.TranslateTransform(TranslateX, TranslateY);
                //    }
                //}
                //*
                if (field.ID.Equals(selectedFieldId))
                    isSelectedField = true;
                else
                    isSelectedField = false;
                if (field is LabelDef.TextFieldGroup)
                {
                    LabelDef.TextFieldGroup textfieldgroup = (LabelDef.TextFieldGroup)field;
                    returnRectangle = textfieldgroup.Draw(Graph, new System.Drawing.Point(0, 0), labelset, 1043, isSelectedField);
                }
                else if (field is LabelDef.TextField)
                {
                    LabelDef.TextField textfield = (LabelDef.TextField)field;
                    System.Drawing.StringFormat stringFormat = new System.Drawing.StringFormat(StringFormatFlags.NoWrap);
                    if (textfield.printFormat == null)
                    {
                        stringFormat.Alignment = StringAlignment.Near;
                    }
                    else
                    {
                        stringFormat.Alignment = textfield.printFormat.format.GetAsStringAlignment();
                    }
                    returnRectangle = textfield.Draw(Graph, new System.Drawing.Point(0, 0), labelset, 1043,isSelectedField);
                    
                }
                else if (field is LabelDef.ImageField)
                {
                    LabelDef.ImageField imagefield = (LabelDef.ImageField)field;
                    returnRectangle = imagefield.Draw(Graph, new System.Drawing.Point(0, 0), labelset, 1043, isSelectedField);
                }
                else if (field is LabelDef.BarcodeField)
                {
                    LabelDef.BarcodeField barcodefield = (LabelDef.BarcodeField)field;
                    returnRectangle = barcodefield.Draw(Graph, new System.Drawing.Point(0, 0), labelset, 1043, isSelectedField);
                }

               /* System.Diagnostics.Debug.WriteLine(
                        string.Format("{6}: Returnrectangle: {0},{1} -> {2},{3}  Contains? {4},{5} ?",
                                        returnRectangle.X,
                                        returnRectangle.Y,
                                        returnRectangle.Right,
                                        returnRectangle.Bottom,
                                        selectX, selectY,
                                        field.ID.ToString()
                                     )
                      );*/
                if (detectedClickToSelect)
                {                    
                    if (returnRectangle.Contains(new System.Drawing.Point(selectX, selectY)))
                    {
                        fieldIDToSelect = field.ID;
                        detectedClickToSelect = false;
                    }
                    
                }
            }
            detectedClickToSelect = false;
            mat.Dispose();
        }

        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        public void SetPointToSelect(int X, int Y)
        {
            //System.Diagnostics.Debug.WriteLine(string.Format("Clicked on {0},{1}", X, Y));
            selectX = X;
            selectY = Y;
            detectedClickToSelect = true;
            this.Invalidate();
        }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            //System.Diagnostics.Debug.WriteLine("SetBoundsCore called!");
            if (specified == BoundsSpecified.Size)
            {
                if (zoomFactor != 1.0f)
                {
                    width = (int)(width * zoomFactor);
                    height = (int)(height * zoomFactor);
                }
            }
            base.SetBoundsCore(x, y, width, height, specified);
        }
    }
}
/*
 AcaLabelPrint Copyright (C) 2011  Retailium Software Development BV.
 This program comes with ABSOLUTELY NO WARRANTY; 
 This is free Software, and you are welcome to redistribute it
 under certain conditions. See the License.txt file or 
 GNU GPL 3.0 License at <http://www.gnu.org/licenses>*/
