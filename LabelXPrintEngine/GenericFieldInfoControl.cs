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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;
using System.Resources;
using System.Reflection;
using ACA.LabelX.PrintEngine;
using ACA.LabelX.Label;

namespace ACA.LabelX.Controls
{
    public partial class GenericFieldInfoControl : UserControl
    {
        /// <summary>
        /// Possible statuses which can be shown in the center of the directional arrows.
        /// </summary>
        public enum Statuses { normal, rotate, zoom, rotatezoom };
        protected LabelXResourceManager rcManager;

        protected Label.LabelDef.Field field;
        protected IDictionary<string, LabelDef.Field> localreferenceList;
        protected Label.LabelSet labelset = new Label.LabelSet();
        protected Form mainForm;
        protected Image statusImage = null;
        int moveTo = 0;
        protected Label.LabelDef.Field Field
        {
            get { return field; }
            set { field = value; }
        }
        protected bool IsCallBack = false;

        protected int moveDelayCounter = 0;

        private Statuses status = Statuses.normal;
        public enum Rotations {norotation, degrees90, degreesminus90, degrees180};
        private Rotations rotation = Rotations.norotation;
        private enum Directions { up = 1, down = 2, left = 4, right = 3 };
        /// <summary>
        /// Translate a directorion in a new direction depending on the current rotation.
        /// </summary>
        /// <param name="direction">Physical direction</param>
        /// <returns>Logical direction</returns>
        private Directions GetDirectionTranslated(Directions direction)
        {
            return GetDirectionTranslated(direction, rotation);
        }
        /// <summary>
        /// Translate a physical direction to a logical direction based on the rotation given
        /// </summary>
        /// <param name="direction">Physical direction</param>
        /// <param name="rotation">Rotation</param>
        /// <returns>Logical direction</returns>
        private Directions GetDirectionTranslated(Directions direction, Rotations rotation)
        {
            Directions retVal = Directions.up;
            switch (direction)
            {
                case Directions.up:
                    switch (rotation)
                    {
                        case Rotations.norotation:
                            retVal = Directions.up;
                            break;
                        case Rotations.degrees90:
                            retVal = Directions.right;
                            break;
                        case Rotations.degreesminus90:
                            retVal = Directions.left;
                            break;
                        case Rotations.degrees180:
                            retVal = Directions.down;
                            break;
                    }                    
                    break;
                case Directions.down:
                    switch (rotation)
                    {
                        case Rotations.norotation:
                            retVal = Directions.down;
                            break;
                        case Rotations.degrees90:
                            retVal = Directions.left;
                            break;
                        case Rotations.degreesminus90:
                            retVal = Directions.right;
                            break;
                        case Rotations.degrees180:
                            retVal = Directions.up;
                            break;
                    }
                    break;
                case Directions.left:
                    switch (rotation)
                    {
                        case Rotations.norotation:
                            retVal = Directions.left;
                            break;
                        case Rotations.degrees90:
                            retVal = Directions.up;
                            break;
                        case Rotations.degreesminus90:
                            retVal = Directions.down;
                            break;
                        case Rotations.degrees180:
                            retVal = Directions.right;
                            break;
                    }
                    break;
                case Directions.right:
                    switch (rotation)
                    {
                        case Rotations.norotation:
                            retVal = Directions.right;
                            break;
                        case Rotations.degrees90:
                            retVal = Directions.down;
                            break;
                        case Rotations.degreesminus90:
                            retVal = Directions.up;
                            break;
                        case Rotations.degrees180:
                            retVal = Directions.left;
                            break;
                    }
                    break;
            }
            return retVal;
        }
        public Rotations Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }
        /// <summary>
        /// The status to be shown, inside the directional arrows. 
        /// </summary>
        public Statuses Status
        {
            get 
            { 
                return status; 
            }
            set 
            {
                if (status != value)
                {
                    status = value;
                    if (statusPictureBox != null)
                    {
                        Bitmap map = null;
                        switch (status)
                        {
                            case Statuses.rotate:
                                map = ACA.LabelX.PrintEngine.Properties.Resources.rotate_128;

                                break;
                            case Statuses.zoom:
                                map = ACA.LabelX.PrintEngine.Properties.Resources.zoom_128;
                                //map = new Bitmap(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ACA.LabelX.PrintEngine.Properties.Resources.resources.zoom_128"));
                                break;
                            case Statuses.rotatezoom:
                                map = ACA.LabelX.PrintEngine.Properties.Resources.rotate_zoom_128;
                                break;
                        }
                        
                        if (map != null)
                        {
                            Bitmap map2 = new Bitmap(16, 16);
                            Graphics g2 = Graphics.FromImage((Image)map2);
                            g2.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                            g2.DrawImage(map, 0, 0, 16, 16);
                            g2.Dispose();
                            statusPictureBox.Image = map2;
                            statusPictureBox.Visible = true;
                            //map.Dispose();
                            //map2.Dispose();
                        }
                        else
                        {
                            statusPictureBox.Visible = false;
                        }
                    }
                }
            }
        }



        public GenericFieldInfoControl() {
            InitializeComponent();
            InitResources();
            SetLanguage();
        }

        public void InitResources()
        {
            int nLanguageId;
            nLanguageId = GlobalDataStore.LanguageId;
            try
            {
                CultureInfo cultureInfo = new CultureInfo(nLanguageId, true);
                Thread.CurrentThread.CurrentCulture = cultureInfo;
                Thread.CurrentThread.CurrentUICulture = cultureInfo;
            }
            catch
            {
                //Nope;
            }
            rcManager = new LabelXResourceManager();
            ResourceManager myRes = new ResourceManager("ACALabelXToolbox.strings", Assembly.GetAssembly(typeof(ACA.LabelX.Toolbox.Toolbox)));
            rcManager.RegisterResource(myRes);
            ResourceManager myRes2 = new ResourceManager("ACA.LabelX.PrintEngine.strings", Assembly.GetExecutingAssembly());
            rcManager.RegisterResource(myRes2);
        }

        public GenericFieldInfoControl(IDictionary<string, LabelDef.Field> referenceList,Label.LabelSet labelset, Form mainForm)
        {
            InitializeComponent();
            InitResources();
            this.mainForm = mainForm;
            this.localreferenceList = referenceList;
            this.labelset = labelset; 
            rotationcombo.Items.AddRange(new String[] { "0", "90", "180", "270" });
            rotationcombo.SelectedIndex = 0;
            manualwhcheck.Checked = false;
        }

        public virtual void SetFieldData()
        {
            xpostxt.Text = field.PositionX.length.ToString();
            ypostxt.Text = field.PositionY.length.ToString();
            rotationcombo.SelectedItem = field.Rotation.ToString();

            if (field.Width == null && field.Height == null)
            {
                manualwhcheck.Checked = false;
                widthtxt.Text = String.Empty;
                heighttxt.Text = String.Empty;
            }
            else
            {
                manualwhcheck.Checked = true;
                if (field.Width != null)
                    widthtxt.Text = field.Width.length.ToString();

                if (field.Height != null)
                    heighttxt.Text = field.Height.length.ToString();
            }
           
            string tempreference = field.ValueRef;
            referencecombo.Items.Clear();
            try
            {
                referencecombo.Items.Add(GetString("NOTHING"));
            }
            catch
            {
                referencecombo.Items.Add("Nothing");
            }
            foreach (LabelDef.Field fieldToAdd in localreferenceList.Values)
            {
                if(!(fieldToAdd is LabelDef.TextFieldGroup))
                    referencecombo.Items.Add(fieldToAdd.ID);
            }
            if (tempreference != null)
                referencecombo.SelectedItem = tempreference;
            else
                referencecombo.SelectedIndex = 0;

        }

        public void FillData(Label.LabelDef.Field field)
        {
            this.field = field;
            SetFieldData();
            SetData();
            this.Invalidate();
        }
        public virtual void SetData()
        { 
        }
        public virtual void SetLanguage()
        {
            xposlbl.Text = GetString("XPOSITION");
            yposlbl.Text = GetString("YPOSITION");
            rotationlbl.Text = GetString("ROTATION");
            referencelbl.Text = GetString("REFERENCE");
            manualwhcheck.Text = GetString("USEMANUALWIDTHHEIGHT");
            widthlbl.Text = GetString("WIDTH");
            heightlbl.Text = GetString("HEIGHT");
            adjustmentlbl.Text = GetString("ADJUSTMENT");
        }

        private void manualwhcheck_CheckedChanged(object sender, EventArgs e)
        {
            if (manualwhcheck.Checked)
            {
                widthlbl.Enabled = true;
                widthtxt.Enabled = true;
                heightlbl.Enabled = true;
                heighttxt.Enabled = true;
            }
            else
            {
                widthlbl.Enabled = false;
                widthtxt.Enabled = false;
                widthtxt.Text = String.Empty;
                field.Width = null;
                heightlbl.Enabled = false;
                heighttxt.Enabled = false;
                heighttxt.Text = String.Empty;
                field.Height = null;
            }
            mainForm.Invalidate();
        }

        private void xpostxt_LostFocus(object sender, EventArgs e)
        {
            //mve,1.3.2
            try
            {
                field.PositionX.length = float.Parse(xpostxt.Text);
            }
            catch (Exception e2)
            {
                MessageBox.Show(e2.Message);
                xpostxt.Focus();
                return;
            }
            //*
            FillData(field);
            mainForm.Invalidate();

        }

        private void ypostxt_LostFocus(object sender, EventArgs e)
        {
            //mve,1.3.2
            try
            {
                field.PositionY.length = float.Parse(ypostxt.Text);
            }
            catch (Exception e2)
            {
                MessageBox.Show(e2.Message);
                ypostxt.Focus();
            }
            //*
            FillData(field);
            mainForm.Invalidate();
        }

        private void rotationcombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rotationcombo.SelectedItem != null && field != null)
            {
                field.Rotation = Convert.ToInt32(rotationcombo.SelectedItem.ToString());
                FillData(field);
                mainForm.Invalidate();
            }
        }

        public virtual void referencecombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (!IsCallBack)
            //{
                if (referencecombo.SelectedItem != null && field != null)
                {
                    if (referencecombo.SelectedItem.ToString().Equals(GetString("NOTHING")) || referencecombo.SelectedItem.ToString().Equals(field.ID))
                        field.ValueRef = null;
                    else
                        field.ValueRef = referencecombo.SelectedItem.ToString();
                    IsCallBack = true;
                    FillData(field);
                    mainForm.Invalidate();
                }
            //}
            //else
            //    IsCallBack = false;
        }

        private void widthtxt_LostFocus(object sender, EventArgs e)
        {
            if (field.Width == null)
                field.Width = new ACA.LabelX.Tools.Length(new ACA.LabelX.Tools.CoordinateSystem.Units(GraphicsUnit.Millimeter));
            if (widthtxt.Text.Equals("0") || (widthtxt.Text.Length == 0))
                field.Width = null;
            else
                field.Width.length = Convert.ToInt32(widthtxt.Text);

            FillData(field);
            mainForm.Invalidate();
        }

        private void heighttxt_LostFocus(object sender, EventArgs e)
        {
            if (field.Height == null)
                field.Height = new ACA.LabelX.Tools.Length(new ACA.LabelX.Tools.CoordinateSystem.Units(GraphicsUnit.Millimeter));

            if (heighttxt.Text.Equals("0") || (heighttxt.Text.Length == 0))
                field.Height = null;
            else
                field.Height.length = Convert.ToInt32(heighttxt.Text);

            FillData(field);
            mainForm.Invalidate();
        }

        protected virtual void EnableComponents()
        {
            xposlbl.Enabled = true;
            xpostxt.Enabled = true;
            yposlbl.Enabled = true;
            ypostxt.Enabled = true;
            rotationlbl.Enabled = true;
            rotationcombo.Enabled = true;
            referencelbl.Enabled = true;
            referencecombo.Enabled = true;
            manualwhcheck.Enabled = true;
            adjustmentlbl.Enabled = true;
            movedownbtn.Enabled = true;
            moveleftbtn.Enabled = true;
            moverightbtn.Enabled = true;
            moveupbtn.Enabled = true;
        }

        #region "move buttons"
        public void moveField(int moveTo)
        { //1 = up , 2 = down, 3 = left, 4 = right
            bool haschanged = false;
            try
            {
                if (moveTo == 1)
                {
                    field.PositionY.length = field.PositionY.length - 1;
                    haschanged = true;
                }
                else if (moveTo == 2)
                {
                    field.PositionY.length = field.PositionY.length + 1;
                    haschanged = true;
                }
                else if (moveTo == 3)
                {
                    field.PositionX.length = field.PositionX.length + 1;
                    haschanged = true;
                }
                else if (moveTo == 4)
                {
                    field.PositionX.length = field.PositionX.length - 1;
                    haschanged = true;
                }
                if (haschanged)
                {
                    FillData(field);
                    mainForm.Invalidate();
                }
            }
            catch (Exception ex)
            {
                GlobalDataStore.Logger.Info(string.Format("Move error: {0}", ex.Message));                
            }
        }

        private void moveupbtn_MouseDown(object sender, MouseEventArgs e)
        {
            StartMoveUp();
        }
        private void moveupbtn_MouseUp(object sender, MouseEventArgs e)
        {
            StopMoveUp();
        }
        public void StartMoveUp()
        {
            moveTo = (int)GetDirectionTranslated(Directions.up); // 1;
            moveField(moveTo);//Up
            timer1.Enabled = true;
        }
        public void StopMoveUp()
        {
            timer1.Enabled = false;
            moveDelayCounter = 0;
        }

        private void movedownbtn_MouseDown(object sender, MouseEventArgs e)
        {
            StartMoveDown();
        }
        private void movedownbtn_MouseUp(object sender, MouseEventArgs e)
        {
            StopMoveDown();
        }
        public void StartMoveDown()
        {
            moveTo = (int)GetDirectionTranslated(Directions.down); //2;
            moveField(moveTo);//Down
            timer1.Enabled = true;
        }
        public void StopMoveDown()
        {
            timer1.Enabled = false;
            moveDelayCounter = 0;
        }

        private void moveleftbtn_MouseDown(object sender, MouseEventArgs e)
        {
            StartMoveLeft();
        }
        private void moveleftbtn_MouseUp(object sender, MouseEventArgs e)
        {
            StopMoveLeft();
        }
        public void StartMoveLeft()
        {
            moveTo = (int)GetDirectionTranslated(Directions.left); //4;
            moveField(moveTo);//Left
            timer1.Enabled = true;
        }
        public void StopMoveLeft()
        {
            timer1.Enabled = false;
            moveDelayCounter = 0;
        }

        private void moverightbtn_MouseDown(object sender, MouseEventArgs e)
        {
            StartMoveRight();
        }
        private void moverightbtn_MouseUp(object sender, MouseEventArgs e)
        {
            StopMoveRight();
        }
        public void StartMoveRight()
        {
            moveTo = (int)GetDirectionTranslated(Directions.right); //3;
            moveField(moveTo);//Right
            timer1.Enabled = true;
        }
        public void StopMoveRight()
        {
            timer1.Enabled = false;
            moveDelayCounter = 0;
        }

        public void timer1_Tick(object sender, EventArgs e)
        {
            if (moveDelayCounter > 5)
            {
                moveField(moveTo);
            }
            else
            {
                moveDelayCounter++;
            }
        }
        #endregion

        public ACA.LabelX.LabelXResourceManager ACARM
        {
            get
            {
                return rcManager;
            }
        }
        public string GetString(string sName)
        {
            return rcManager.GetString(sName);
        }
        public string NC(string sName)
        {
            return sName;
        }
    }

     
}
/*
 AcaLabelPrint Copyright (C) 2011  Retailium Software Development BV.
 This program comes with ABSOLUTELY NO WARRANTY; 
 This is free Software, and you are welcome to redistribute it
 under certain conditions. See the License.txt file or 
 GNU GPL 3.0 License at <http://www.gnu.org/licenses>*/
