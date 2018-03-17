using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Configuration;
using System.Linq;
using System.Xml;
using System.Xml.XPath;
using System.IO;
using ACA.LabelX.Tools;
using ACA.LabelX.Paper;
using ACA.LabelX.Label;
using ACA.LabelX.PrintEngine;
using ACA.LabelX.Toolbox;
using System.Drawing.Drawing2D;



namespace LabelDesigner
{
    public partial class FormLabelDesignerEx : LabelDesigner.FormBase
    {


        public enum FieldInfoType { TextField, ImageField, BarcodeField, GenericField, ConcatField }

        int labelWidth = 0; //The width of the drawing area
        int labelHeight = 0; //The height of the drawing area
        string defaultLabelDef = string.Empty; //Saves the default label for the current printjob
        LabelSet labelset = new LabelSet();
        //PaperDef paperdef;
        ACA.LabelX.GlobalDataStore gds = ACA.LabelX.GlobalDataStore.GetInstance();
        ACA.LabelX.Controls.TextFieldInfoControl textFieldInfoControl;
        ACA.LabelX.Controls.BarcodeFieldInfoControl barcodeFieldInfoControl;
        ACA.LabelX.Controls.ImageFieldInfoControl imageFieldInfoControl;
        ACA.LabelX.Controls.ConcatFieldInfoControl concatFieldInfoControl;


        private IDictionary<string, LabelDef.FontX> fontList = new Dictionary<string, LabelDef.FontX>(); //list of available fonts
        private IDictionary<string, LabelDef.Field> referenceList = new Dictionary<string, LabelDef.Field>(); //list of used fields
        private IDictionary<string, string> useditemslistDictionary = new Dictionary<string, string>();
        private ACA.LabelX.PrintJob.PrintJob printJob; //The current loaded printjob
        private IDictionary<string, ACA.LabelX.Label.Label.Value> completeValues; //List of available KeyValue pairs read from XML
        private string luaCode = "";
        private CustomPictureBox drawingPictureBox;
        private string openedPrintjob = string.Empty; //Current opened printjob path
        private string currentLabelDef = string.Empty; //Currently used label definition
        private string printjobsRootFolder = string.Empty; //Folder where all printjob XML files are located
        private string paperDefinitionsRootFolder = string.Empty; //Folder where all paperdefinition XML files are located
        private string labelDefinitionsRootFolder = string.Empty; //Folder where all labeldefinition XML files are located
        private List<PaperDef> ListofAvailablePaperDefs = new List<PaperDef>();
        private float zoomFactor = 1.0f;
        private float rotation = 0.0f;


        public FormLabelDesignerEx()
        {
            gds.DesignMode = true;
            InitializeComponent();
            //Add the default font
            LabelDef.FontX testfont1 = new LabelDef.FontX("Default", false, new Font("Arial", 8, FontStyle.Regular), FontStyle.Regular);
            fontList.Add(testfont1.ID, testfont1);

            SetFormLanguage(); //Set the language
            LoadRootFolders(); //Loads the label,paper and printjob folders into variables
        }

        private void FormLabelDesignerEx_Load(object sender, EventArgs e)
        {

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void SetFormLanguage()
        {
            this.Text = GetString("LABELDESIGNER");

            #region "Menubar"
            fileToolStripMenuItem.Text = GetString("FILE");
            openPrintJobToolStripMenuItem.Text = GetString("OPENPRINTJOB");
            printPreviewToolStripMenuItem.Text = GetString("PRINTPREVIEW");
            toggleBordersToolStripMenuItem.Text = GetString("SHOWBORDERS");
            exitToolStripMenuItem.Text = GetString("EXIT");
            labelsToolStripMenuItem.Text = GetString("LABELS");
            newLabelToolStripMenuItem.Text = GetString("NEW");
            openLabelToolStripMenuItem.Text = GetString("OPEN");
            saveLabelToolStripMenuItem1.Text = GetString("SAVELABEL");
            saveLabelAsToolStripMenuItem.Text = GetString("SAVELABELAS");
            editLabelToolStripMenuItem.Text = GetString("EDIT");
            deleteLabelToolStripMenuItem.Text = GetString("DELETE");
            papersToolStripMenuItem.Text = GetString("PAPERTYPES");
            addPaperToolStripMenuItem.Text = GetString("ADD");
            editPaperToolStripMenuItem.Text = GetString("EDIT");
            deletePaperToolStripMenuItem1.Text = GetString("DELETE");
            itemsToolStripMenuItem.Text = GetString("ITEMS");
            addItemToolStripMenuItem.Text = GetString("ADD");
            editItemToolStripMenuItem.Text = GetString("EDIT");
            deleteItemToolStripMenuItem.Text = GetString("DELETE");
            fontsToolStripMenuItem1.Text = GetString("FONTS");
            editFontToolStripMenuItem.Text = GetString("EDIT");
            addFontToolStripMenuItem.Text = GetString("ADD");
            deleteFontToolStripMenuItem.Text = GetString("DELETE");
            #endregion


            availableitemslbl.Text = GetString("AVAILABLEITEMS");
            toolStripButtonText.ToolTipText = GetString("TEXTFIELD");
            toolStripButtonBarcode.ToolTipText = GetString("BARCODEFIELD");
            toolStripButtonLinkText.ToolTipText = GetString("CONCATFIELD");
            toolStripButtonImage.ToolTipText = GetString("IMAGEFIELD");
            toolStripButtonScripting.ToolTipText = "Lua Script";
            /*
            currentlabellbl.Text = GetString("CURRENTLABEL");
            toolStripButtonText.TooltipText = GetString("TEXTFIELD");
            addconcatbtn.Text = GetString("CONCATFIELD");
            addBarcodebtn.Text = GetString("BARCODEFIELD");
            addimagebtn.Text = GetString("IMAGEFIELD");
             */
            useditemslbl.Text = GetString("USEDITEMS");
             
            deletebtn.Text = GetString("DELETE");
            


        }
        private void LoadRootFolders()
        {
            XmlDocument xDoc = new XmlDocument();

            try
            {
                xDoc.Load(ConfigurationSettings.AppSettings["ConfigXML"].ToString());
            }
            catch (Exception ee)
            {
                MessageBox.Show(string.Format("The file {0} as indicated in your designer config file should exist",
                        ConfigurationSettings.AppSettings["ConfigXML"].ToString())); 
                throw ee;
            }
            XmlNode node = xDoc.SelectSingleNode("/configuration/general-settings/folders/PrintJobsRootFolder");
            printjobsRootFolder = node.InnerText;
            node = xDoc.SelectSingleNode("/configuration/general-settings/folders/PaperDefinitionsRootFolder");
            paperDefinitionsRootFolder = node.InnerText;
            node = xDoc.SelectSingleNode("/configuration/general-settings/folders/LabelDefinitionsRootFolder");
            labelDefinitionsRootFolder = node.InnerText;
        }

        private void openPrintJobToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openPrintjobFile = new OpenFileDialog();
            openPrintjobFile.InitialDirectory = printjobsRootFolder;
            openPrintjobFile.Filter = "XML Files|*.xml";
            if (openPrintjobFile.ShowDialog() == DialogResult.OK)
            {
                ACA.LabelX.PrintJob.PrintJob temp_printJob = new ACA.LabelX.PrintJob.PrintJob(paperDefinitionsRootFolder, labelDefinitionsRootFolder + @"\");

                System.Reflection.Assembly a = System.Reflection.Assembly.GetExecutingAssembly();
                System.IO.Stream printjobSchemaStream = a.GetManifestResourceStream("LabelDesigner.printjobschema.xsd");

                bool validated = ACA.PSLib.Xml.ValidateXML(openPrintjobFile.FileName, printjobSchemaStream);

                if (validated)
                {
                    temp_printJob.Parse(openPrintjobFile.FileName);

                    //Printjob temp loaded : load a labeldef for it now
                    bool isOk = false;
                    bool isCancelled = false;
                    while (!(isOk))
                    {
                        LoadLabelForm loadlabelform = new LoadLabelForm();
                        loadlabelform.ShowDialog();
                        if (loadlabelform.result.Equals("default"))
                        {
                            isOk = CheckLabelDefCompatible(labelDefinitionsRootFolder + @"\" + temp_printJob.destination.LabelType + ".xml", temp_printJob);
                            if (isOk)
                            {
                                ResetDrawingField(); //Cleans up all lists and all infocontrols and shows the genericinfocontrol
                                printJob = temp_printJob;
                                LoadLabelDef(labelDefinitionsRootFolder + @"\" + temp_printJob.destination.LabelType + ".xml");
                            }
                        }
                        else if (loadlabelform.result.Equals("existing"))
                        {
                            OpenFileDialog openFileDialog = new OpenFileDialog();
                            openFileDialog.InitialDirectory = labelDefinitionsRootFolder;
                            openFileDialog.Filter = "XML File|*.xml";
                            openFileDialog.Title = "Load XML File";
                            openFileDialog.ShowDialog();
                            if (!(string.IsNullOrEmpty(openFileDialog.FileName)))
                                isOk = CheckLabelDefCompatible(openFileDialog.FileName, temp_printJob);
                            if (isOk)
                            {
                                ResetDrawingField(); //Cleans up all lists and all infocontrols and shows the genericinfocontrol
                                printJob = temp_printJob;
                                LoadLabelDef(openFileDialog.FileName);
                            }
                        }
                        else if (loadlabelform.result.Equals("new"))
                        {
                            NewLabelDefForm newLabelDefForm = new NewLabelDefForm(paperDefinitionsRootFolder, labelDefinitionsRootFolder);
                            if (newLabelDefForm.ShowDialog() == DialogResult.OK)
                            {
                                isOk = CheckLabelDefCompatible(labelDefinitionsRootFolder + @"\" + newLabelDefForm.LabelName + ".xml", temp_printJob);
                                if (isOk)
                                {
                                    ResetDrawingField(); //Cleans up all lists and all infocontrols and shows the genericinfocontrol
                                    printJob = temp_printJob;
                                    LoadLabelDef(labelDefinitionsRootFolder + @"\" + newLabelDefForm.LabelName + ".xml");
                                }
                            }
                        }
                        else
                        {
                            isOk = true;
                            isCancelled = true;
                        }
                    }

                    if (isOk)
                    {
                        if (!(isCancelled))
                        {
                            defaultLabelDef = labelDefinitionsRootFolder + @"\" + printJob.destination.LabelType + ".xml"; //Save default labelDef(TODO:needed?)
                            toolStripStatusLabelLabelName.Text = currentLabelDef;
                            openedPrintjob = openPrintjobFile.FileName;

                            #region "init picturebox"
                            if (drawingPictureBox == null)
                            {
                                drawingPictureBox = new CustomPictureBox(labelWidth, labelHeight, labelset);
                                drawingPictureBox.Name = "drawingPictureBox";
                                drawingPictureBox.AllowDrop = true;
                                drawingPictureBox.Location = new System.Drawing.Point(0, 0);
                                drawingPictureBox.Size = GetRotatedSize();//new System.Drawing.Size(labelWidth, labelHeight);
                                drawingPictureBox.BorderStyle = BorderStyle.FixedSingle;
                                drawingPictureBox.DragEnter += new DragEventHandler(drawingPictureBox_DragEnter);
                                drawingPictureBox.DragDrop += new DragEventHandler(drawingPictureBox_DragDrop);
                                drawingPictureBox.MouseClick += new MouseEventHandler(drawingPictureBox_MouseClick);
                                drawingPictureBox.ZoomFactor = zoomFactor;
                                drawingPictureBox.UseWhiteBackground = whiteBackgroundToolStripMenuItem.Checked;
                                pictureboxPanel.Controls.Add(drawingPictureBox);
                            }
                            else
                            {
                                drawingPictureBox.Size = GetRotatedSize();//new System.Drawing.Size(labelWidth, labelHeight);
                                drawingPictureBox.ZoomFactor = zoomFactor;
                                drawingPictureBox.UseWhiteBackground = whiteBackgroundToolStripMenuItem.Checked;
                            }
                            #endregion
                            EnableForm();
                            RedrawPictureBox();
                            this.Invalidate();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("The XML file is not a verified printjob file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }
        }
        private bool CheckLabelDefCompatible(string LabelDefFilePath, ACA.LabelX.PrintJob.PrintJob printJobToCompare)
        {
            List<string> simulatedListbox = new List<string>();

            LabelDef loadedXML = new LabelDef();

            System.Reflection.Assembly a = System.Reflection.Assembly.GetExecutingAssembly();
            System.IO.Stream labelSchemaStream = a.GetManifestResourceStream("LabelDesigner.labelschema.xsd");

            bool validated = ACA.PSLib.Xml.ValidateXML(LabelDefFilePath, labelSchemaStream);

            if (validated)
            {
                loadedXML.Parse(LabelDefFilePath);
                try
                {
                    printJobToCompare.LabelDef = loadedXML;

                    IDictionary<string, ACA.LabelX.Label.Label.Value> ValueKeys = GetLabelDefValueKeys(printJobToCompare);

                    foreach (string referenceid in loadedXML.fields.Keys)
                    {
                        LabelDef.Field searchfield;
                        loadedXML.fields.TryGetValue(referenceid, out searchfield);

                        if (searchfield is LabelDef.TextFieldGroup)
                        {
                            LabelDef.TextFieldGroup textfieldgroup = (LabelDef.TextFieldGroup)searchfield;
                            simulatedListbox.Add(textfieldgroup.ID);
                        }
                        else
                        {
                            ACA.LabelX.Label.Label.Value value;
                            if (searchfield.ValueRef == null)
                                ValueKeys.TryGetValue(referenceid + ".1043", out value); //TODO: language support
                            else
                                ValueKeys.TryGetValue(searchfield.ValueRef + ".1043", out value);
                            try
                            {
                                simulatedListbox.Add(referenceid + " (" + value.Data + ")");
                            }
                            catch (NullReferenceException)
                            {
                                MessageBox.Show(String.Format("Field {0} could not be found in the available data.", referenceid), "Field Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return false;
                            }
                        }
                    }
                    ChoosePaperDef(loadedXML); //Load validated paper XML's into ListofAvailablePaperDefs
                    if (ListofAvailablePaperDefs.Count > 0)
                    {
                        //Everything ok, load first(=default) paper at load of label
                        return true;
                    }
                    else
                    {
                        //Problem! Ask user to add a new paper definition (or to create a new one), validate it and then save the new labeldef.
                        MessageBox.Show("No valid paper types were found for Label " + loadedXML.ID + ". \n Please create or add a valid paper type.", "Error loading label", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        NewLabelDefForm newLabelDefForm = new NewLabelDefForm(paperDefinitionsRootFolder, labelDefinitionsRootFolder, loadedXML);
                        if (newLabelDefForm.ShowDialog() == DialogResult.OK)
                        {
                            //    CheckLabelDefCompatible(labelDefinitionsRootFolder + @"\" + newLabelDefForm.LabelName + ".xml", printJob);
                            //    LoadLabelDef(labelDefinitionsRootFolder + @"\" + newLabelDefForm.LabelName + ".xml");
                        }
                        return true;
                    }
                }
                catch
                {
                    MessageBox.Show(GetString("LOADLABELERROR"), GetString("LOADLABELERRORTITLE"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                    //Load failed
                }
            }
            else
            {
                MessageBox.Show("The XML file (" + LabelDefFilePath + ") is not a verified label file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

        }
        private bool LoadLabelDef(string path)
        {
            LabelDef loadedXML = new LabelDef();
            loadedXML.Parse(path);
            try
            {
                referenceList = loadedXML.fields;
                fontList = loadedXML.fonts;
                printJob.LabelDef = loadedXML;
                luaCode = loadedXML.luaCode;
                FillAvailableitemsList();
                useditemslist.Items.Clear();
                useditemslistDictionary.Clear();

                foreach (string referenceid in loadedXML.fields.Keys)
                {
                    LabelDef.Field searchfield;
                    loadedXML.fields.TryGetValue(referenceid, out searchfield);

                    if (searchfield is LabelDef.TextFieldGroup)
                    {
                        LabelDef.TextFieldGroup textfieldgroup = (LabelDef.TextFieldGroup)searchfield;
                        addToUsedItemsListDictionaryWithoutPath(textfieldgroup.ID);

                    }
                    else
                    {
                        ACA.LabelX.Label.Label.Value value;
                        if (searchfield.ValueRef == null)
                            completeValues.TryGetValue(referenceid + ".1043", out value); //TODO: language support
                        else
                            completeValues.TryGetValue(searchfield.ValueRef + ".1043", out value);
                        addToUsedItemsListDictionaryWithoutPath(referenceid + " (" + value.Data + ")");
                    }
                }
                currentLabelDef = path;
                LoadPaperDef(ListofAvailablePaperDefs.ElementAt(0)); //load first paperdef (=default)
                RedrawPictureBox();
                toolStripStatusLabelLabelName.Text = currentLabelDef;
                return true;
            }
            catch
            {
                return false;
                //Load failed
            }

        }
        private void ChoosePaperDef(LabelDef labeldef)
        {
            ListofAvailablePaperDefs.Clear();

            foreach (LabelDef.PaperType tempPaperType in labeldef.PaperTypes)
            {
                try
                {
                    System.Reflection.Assembly a = System.Reflection.Assembly.GetExecutingAssembly();
                    System.IO.Stream paperSchemaStream = a.GetManifestResourceStream("LabelDesigner.paperschema.xsd");

                    bool validated = ACA.PSLib.Xml.ValidateXML(paperDefinitionsRootFolder + @"\" + tempPaperType.ID + ".xml", paperSchemaStream);
                    if (validated)
                    {
                        PaperDef localPaperDef = new PaperDef();
                        localPaperDef.Parse(paperDefinitionsRootFolder + @"\" + tempPaperType.ID + ".xml");
                        if (tempPaperType.Default)
                            ListofAvailablePaperDefs.Insert(0, localPaperDef);
                        else
                            ListofAvailablePaperDefs.Add(localPaperDef);
                    }
                }
                catch (FileNotFoundException)
                {
                    System.Diagnostics.Debug.WriteLine("File not found: " + tempPaperType.ID);
                }
            }
        }
        private void ResetDrawingField()
        {
            treeView1.Nodes.Clear();//Treeview
            useditemslist.Items.Clear();
            referenceList.Clear();
            if (textFieldInfoControl != null)
            {
                textFieldInfoControl.Dispose();
                textFieldInfoControl = null;
            }
            if (barcodeFieldInfoControl != null)
            {
                barcodeFieldInfoControl.Dispose();
                barcodeFieldInfoControl = null;
            }
            if (concatFieldInfoControl != null)
            {
                concatFieldInfoControl.Dispose();
                concatFieldInfoControl = null;
            }
            if (imageFieldInfoControl != null)
            {
                imageFieldInfoControl.Dispose();
                imageFieldInfoControl = null;
            }
            genericFieldInfoControl1.Visible = true;


        }
        System.Drawing.Size GetRotatedSize()
        {
            if (rotation == 0.0f)
            {
                return new System.Drawing.Size(labelWidth, labelHeight);
            }
            else
            {
                return new System.Drawing.Size(labelHeight, labelWidth);
            }
        }
        private void drawingPictureBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effect = DragDropEffects.All;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void drawingPictureBox_DragDrop(object sender, DragEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine(string.Format("dragdrop on X:{0},Y:{1}", e.X, e.Y));
            if (e.Data.GetData(DataFormats.Text).ToString().Contains('('))
            {
                //Get Correct Coordinates
                System.Drawing.Point point = drawingPictureBox.PointToClient(new System.Drawing.Point(e.X, e.Y));
                //   System.Diagnostics.Debug.WriteLine(string.Format("After PointToClient: X:{0},Y:{1}", point.X, point.Y));
                Graphics graph = drawingPictureBox.CreateGraphics();

                float TranslateX = 0 , TranslateY = 0;
                Matrix mat = new Matrix();//graph.Transform;
                mat.Scale(zoomFactor, zoomFactor, MatrixOrder.Append);
                if (rotation != 0)
                {
                    mat.Rotate(rotation, MatrixOrder.Append);
                    if (rotation > 0)
                    {
                        TranslateX = 0.0f;// -Size.Width / 2;
                        TranslateY = -drawingPictureBox.Size.Width / zoomFactor;
                    }
                    else
                    {
                        TranslateX = -drawingPictureBox.Size.Height / zoomFactor;
                        TranslateY = 0.0f;
                    }
                    mat.Translate(TranslateX, TranslateY);
                }                
                mat.Invert();
                /*
                    System.Diagnostics.Debug.WriteLine(string.Format("rotation: ", rotation));
                    System.Diagnostics.Debug.WriteLine(string.Format("Translation: X:{0}, Y:{1}", TranslateX, TranslateY));
                    System.Diagnostics.Debug.WriteLine(string.Format("zoomFactor: {0}", zoomFactor));
                 */
                System.Drawing.Point[] arrayPoints = new System.Drawing.Point[1] { point };
                mat.TransformPoints(arrayPoints);

                //  System.Diagnostics.Debug.WriteLine(string.Format("After Matrix Transformation: X:{0},Y:{1}", arrayPoints[0].X, arrayPoints[0].Y));
                ACA.LabelX.Tools.Length lengthX = new Length(arrayPoints[0].X, System.Drawing.GraphicsUnit.Pixel);
                ACA.LabelX.Tools.Length lengthY = new Length(arrayPoints[0].Y, System.Drawing.GraphicsUnit.Pixel);                               
                lengthX.DPI = graph.DpiX;
                lengthY.DPI = graph.DpiY;
                int X = (int)lengthX.InMM();
                int Y = (int)lengthY.InMM();

                AddTextFieldForm addTextFieldForm = new AddTextFieldForm(fontList, referenceList, Toolbox.getSelectedIdFromTreeView(treeView1), Toolbox.getSelectedValueFromTreeView(treeView1), X, Y); //Treeview
                if (addTextFieldForm.ShowDialog() == DialogResult.OK)
                {
                    fontList = addTextFieldForm.FontList;
                    referenceList.Add(addTextFieldForm.Textfield.ID, addTextFieldForm.Textfield);
                    addToUsedItemsListDictionaryWithoutPath(addTextFieldForm.Textfield.ID + " (" + Toolbox.getSelectedValueFromTreeView(treeView1) + ")");
                    useditemslist.SelectedIndex = useditemslist.Items.Count - 1;
                    RedrawPictureBox();
                }
            }
        }
        private void drawingPictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            int X2, Y2;

            X2 = e.X;
            Y2 = e.Y;
            //System.Diagnostics.Debug.WriteLine(string.Format("mouseclick on X:{0},Y:{1}", e.X, e.Y));

            /*
            if (zoomFactor != 1.0f)
            {
                X2 = (int)(X2 / zoomFactor);
                Y2 = (int)(Y2 / zoomFactor);
            }
            */
            //mve,1.3.2
            //System.Diagnostics.Debug.WriteLine(string.Format("before Transform: {0},{1}: ", e.X, e.Y));

            System.Drawing.Point[] p = new System.Drawing.Point[1];

            Matrix mat = new Matrix();
            float TranslateX, TranslateY;
            mat.Scale(zoomFactor, zoomFactor, MatrixOrder.Append);
            mat.Invert();
            p[0] = new System.Drawing.Point(X2, Y2);
            mat.TransformPoints(p);
            X2 = p[0].X;
            Y2 = p[0].Y;
            //System.Diagnostics.Debug.WriteLine(string.Format("After undoing the zoom: {0},{1} ", X2, Y2));
            mat.Dispose();
            mat = new Matrix();
            if (rotation != 0)
            {
                mat.Rotate(rotation);

                if (rotation > 0)
                {
                    TranslateX = 0.0f;// -Size.Width / 2;
                    TranslateY = -drawingPictureBox.Size.Width / zoomFactor;
                }
                else
                {
                    TranslateX = -drawingPictureBox.Size.Height / zoomFactor;
                    TranslateY = 0.0f;
                }
                mat.Translate(TranslateX, TranslateY);


                p[0] = new System.Drawing.Point(X2, Y2);
                if (mat.IsInvertible)
                {
                    //System.Diagnostics.Debug.WriteLine("Matrix is invertible...");
                    mat.Invert();
                }
                //else
                //{
                //System.Diagnostics.Debug.WriteLine("Matrix is not invertable...");
                //}
                mat.TransformPoints(p);
                X2 = p[0].X;
                Y2 = p[0].Y;

                //System.Diagnostics.Debug.WriteLine(string.Format("After undoing the rotation: {0},{1} ", X2, Y2));
            }
            //System.Diagnostics.Debug.WriteLine(string.Format("Final result of click: {0},{1} ", X2, Y2));
            drawingPictureBox.SetPointToSelect(X2, Y2);
            Application.DoEvents();

            string selectedFieldName = drawingPictureBox.FieldIDToSelect;
            LabelDef.Field selectedField;

            referenceList.TryGetValue(selectedFieldName, out selectedField);
            if (selectedField != null)
            {
                if (selectedField is LabelDef.TextFieldGroup)
                {
                    string temp = removePath(selectedField.ID);
                    useditemslist.SelectedItem = temp;
                }
                else
                {
                    ACA.LabelX.Label.Label.Value value;
                    if (selectedField.ValueRef == null)
                        completeValues.TryGetValue(selectedFieldName + ".1043", out value); //TODO: language support
                    else
                        completeValues.TryGetValue(selectedField.ValueRef + ".1043", out value);

                    string temp = removePath(selectedField.ID + " (" + value.Data + ")");
                    useditemslist.SelectedItem = temp;
                }
            }
            else
            {
                useditemslist.SelectedItem = null;
            }
        }
        public void RedrawPictureBox()
        {
            highlightUsedTreeNodes();
            //picturebox is docked in panel
            //pictureboxPanel.Size = new System.Drawing.Size(useditemslist.Location.X - ((addTextfieldbtn.Location.X + addTextfieldbtn.Width) + 15) - (int)(SortDownBtn.Width * 1.1),
            //                                              treeView1.Height); //Treeview
            try
            {
                drawingPictureBox.DrawingreferenceList = referenceList;

                if (useditemslist.SelectedItems.Count == 1)
                    drawingPictureBox.SelectedFieldId = getValueFromUsedItemsListDictionaryItem(useditemslist.SelectedItem.ToString()).Split('(')[0].Trim();
                else
                    drawingPictureBox.SelectedFieldId = "";

                drawingPictureBox.Labelset = labelset;
                drawingPictureBox.Size = GetRotatedSize();//new System.Drawing.Size(labelWidth, labelHeight);

                drawingPictureBox.Invalidate();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Failed to redraw PictureBox: " + e.Message);
            }
        }
        private void EnableForm()
        {
            availableitemslbl.Enabled = true;
            treeView1.Enabled = true;//Treeview
            toolStripButtonText.Enabled = true;
            toolStripButtonLinkText.Enabled = true;
            toolStripButtonBarcode.Enabled = true;
            toolStripButtonImage.Enabled = true;
            toolStripButtonScripting.Enabled = true;
            editToolStripMenuItem.Enabled = true;

            labelsToolStripMenuItem.Enabled = true;
            newLabelToolStripMenuItem.Enabled = true;
            openLabelToolStripMenuItem.Enabled = true;
            saveLabelAsToolStripMenuItem.Enabled = true;
            saveLabelToolStripMenuItem1.Enabled = true;
            deleteLabelToolStripMenuItem.Enabled = true;
            editLabelToolStripMenuItem.Enabled = true;
            toolStripMenuView.Enabled = true;

            papersToolStripMenuItem.Enabled = true;
            addPaperToolStripMenuItem.Enabled = true;
            editPaperToolStripMenuItem.Enabled = true;
            deletePaperToolStripMenuItem1.Enabled = true;

            itemsToolStripMenuItem.Enabled = true;
            addItemToolStripMenuItem.Enabled = true;
            editItemToolStripMenuItem.Enabled = true;
            deleteItemToolStripMenuItem.Enabled = true;

            fontsToolStripMenuItem1.Enabled = true;
            addFontToolStripMenuItem.Enabled = true;
            editFontToolStripMenuItem.Enabled = true;
            deleteFontToolStripMenuItem.Enabled = true;

            printPreviewToolStripMenuItem.Enabled = true;
            useditemslbl.Enabled = true;
            useditemslist.Enabled = true;
            openLabelToolStripMenuItem.Enabled = true;

            addItemToolStripMenuItem.Enabled = true;
            toggleBordersToolStripMenuItem.Enabled = true;

            //currentlabellbl.Enabled = true;

            if (treeView1.Nodes.Count > 0) //Treeview
                treeView1.SelectedNode = treeView1.TopNode;

        }
        private IDictionary<string, ACA.LabelX.Label.Label.Value> GetLabelDefValueKeys(ACA.LabelX.PrintJob.PrintJob printjob)
        {
            IDictionary<string, ACA.LabelX.Label.Label.Value> result = new Dictionary<string, ACA.LabelX.Label.Label.Value>();
            foreach (KeyValuePair<string, ACA.LabelX.Label.Label.Value> value in printjob.labels[0].Values)
            {
                if (!result.ContainsKey(value.Key))
                    result.Add(value);
            }

            foreach (KeyValuePair<string, ACA.LabelX.Label.Label.Value> value in printjob.Defaultlabel.Values)
            {
                if (!result.ContainsKey(value.Key))
                    result.Add(value);
            }

            foreach (KeyValuePair<string, ACA.LabelX.Label.Label.Value> value in printjob.LabelDef.DefaultLabel.Values)
            {
                if (!result.ContainsKey(value.Key))
                    result.Add(value);
            }

            return result;
        }
        private void FillAvailableitemsList()
        {
            ACA.LabelX.Label.Label CurrentLabel = printJob.labels[0];
            ACA.LabelX.Label.Label DefaultLabel = printJob.Defaultlabel;
            ACA.LabelX.Label.Label BaseLabel = printJob.LabelDef.DefaultLabel;

            labelset.CurrentLabel = CurrentLabel;
            labelset.DefaultLabel = DefaultLabel;
            labelset.BaseLabel = BaseLabel;


            completeValues = new Dictionary<string, ACA.LabelX.Label.Label.Value>();
            foreach (KeyValuePair<string, ACA.LabelX.Label.Label.Value> value in labelset.CurrentLabel.Values)
            {
                if (!completeValues.ContainsKey(value.Key))
                    completeValues.Add(value);
            }

            foreach (KeyValuePair<string, ACA.LabelX.Label.Label.Value> value in DefaultLabel.Values)
            {
                if (!completeValues.ContainsKey(value.Key))
                    completeValues.Add(value);
            }

            foreach (KeyValuePair<string, ACA.LabelX.Label.Label.Value> value in BaseLabel.Values)
            {
                if (!completeValues.ContainsKey(value.Key))
                    completeValues.Add(value);
            }

            treeView1.Nodes.Clear();
            treeView1.BeginUpdate();
            foreach (KeyValuePair<string, ACA.LabelX.Label.Label.Value> pair in completeValues)
            {
                string[] ar = pair.Key.Split('.')[0].Split('\\');
                TreeNodeCollection currentNodeCollection = treeView1.Nodes;
                TreeNode currentNode = new TreeNode();

                for (int i = 0; i < ar.Length; i++)
                {
                    string item = ar[i];

                    currentNode = GetTreeNodeFromCollection(currentNodeCollection, item);
                    if (currentNode == null)
                    {
                        if (i == (ar.Length - 1))
                            item = item + " (" + pair.Value.Data + ")";

                        currentNodeCollection.Add(item);
                        currentNode = GetTreeNodeFromCollection(currentNodeCollection, item);
                        currentNodeCollection = currentNode.Nodes;
                    }
                    else
                    {
                        currentNodeCollection = currentNode.Nodes;
                    }

                }
            }
            treeView1.EndUpdate();
        }
        private string getValueFromUsedItemsListDictionaryItem(string value)
        {
            string temp = "";
            try
            {
                useditemslistDictionary.TryGetValue(value, out temp);
            }
            catch
            {
                Console.WriteLine("Error in getValueFromUsedItemsListDictionaryItem");
            }
            return temp;
        }
        private void addToUsedItemsListDictionaryWithoutPath(string value)
        {
            string temp = removePath(value);
            try
            {
                useditemslistDictionary.Add(temp, value);
                useditemslist.Items.Add(temp);
            }
            catch (ArgumentException) {
                MessageBox.Show("Error: " + temp + " already exists in the used items listbox.","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }            
        }

        private TreeNode GetTreeNodeFromCollection(TreeNodeCollection treeNodeCollection, string value)
        {
            if (treeNodeCollection != null)
            {
                foreach (TreeNode node in treeNodeCollection)
                {
                    if (node.Text.Equals(value, StringComparison.OrdinalIgnoreCase))
                    {
                        return node;
                    }
                }
            }
            return null;
        }
        private void LoadPaperDef(PaperDef paperdefToLoad)
        {
            labelWidth = (int)(paperdefToLoad.GetLabelSize().Width.InInch() * this.CreateGraphics().DpiX);
            labelHeight = (int)(paperdefToLoad.GetLabelSize().Height.InInch() * this.CreateGraphics().DpiY);
            printJob.PaperDef = paperdefToLoad;
        }
        private string removePath(string value)
        {
            string[] ar = value.Split('\\');
            return ar[ar.Length - 1];
        }
        private void highlightUsedTreeNodes()
        {
            TreeNodeCollection treeNodeCollection = treeView1.Nodes;
            foreach (TreeNode treeNode in treeNodeCollection)
            {
                processTreeNodes(treeNode);
            }
        }
        private void processTreeNodes(TreeNode treeNode)
        {
            if (treeNode != null)
            {
                TreeNodeCollection treeNodeCollection = treeNode.Nodes;
                if (treeNodeCollection != null && treeNodeCollection.Count > 0)
                {
                    foreach (TreeNode node in treeNodeCollection)
                    {
                        processTreeNodes(node);
                    }
                }
                else
                {
                    if (referenceList.ContainsKey(treeNode.FullPath.Split('(')[0].Trim()))
                    {
                        treeNode.ForeColor = Color.Red;
                    }
                    else
                    {
                        treeNode.ForeColor = Color.Black;
                    }
                }
            }
        }

        private void deletebtn_Click(object sender, EventArgs e)
        {
            LabelDef.Field fieldToDelete;
            referenceList.TryGetValue(getValueFromUsedItemsListDictionaryItem(useditemslist.SelectedItem.ToString()).Split('(')[0].Trim(), out fieldToDelete);
            DeleteField(fieldToDelete);

            if (useditemslist.Items.Count <= 0)
            {
                useditemslist_SelectedIndexChanged(null, null);
            }
        }
        private void DeleteField(LabelDef.Field fieldToDelete)
        {

            if (fieldToDelete is LabelDef.TextFieldGroup)
            {
                LabelDef.TextFieldGroup textFieldGroupToDelete = (LabelDef.TextFieldGroup)fieldToDelete;
                foreach (LabelDef.TextField textField in textFieldGroupToDelete.fields)
                {
                    ACA.LabelX.Label.Label.Value value;
                    completeValues.TryGetValue(textField.ID + ".1043", out value); //TODO: Language support
                }

            }
            else
            {
                List<string> templist = new List<string>();
                foreach (string usedfield in useditemslist.Items)
                {
                    templist.Add(usedfield);
                }

                foreach (string usedfield in templist)
                {
                    LabelDef.Field tempfield;
                    referenceList.TryGetValue(getValueFromUsedItemsListDictionaryItem(usedfield).Split('(')[0].Trim(), out tempfield);
                    if (tempfield.ValueRef != null && tempfield.ValueRef.Equals(fieldToDelete.ID))
                    {
                        DeleteField(tempfield);
                    }
                }
            }

            referenceList.Remove(fieldToDelete.ID);
            ReloadUseditemslist();

            if (useditemslist.Items.Count > 0)
                useditemslist.SelectedIndex = 0;

            if (useditemslist.Items.Count < 1)
                deletebtn.Enabled = false;

            RedrawPictureBox();
            treeView1.Invalidate(); //TreeView
        }
        private void useditemslist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (useditemslist.SelectedItems.Count == 1)
            {
                LabelDef.Field tempField;
                referenceList.TryGetValue(getValueFromUsedItemsListDictionaryItem(useditemslist.SelectedItem.ToString()).Split('(')[0].Trim(), out tempField);
                if (tempField != null)
                {
                    if (tempField is LabelDef.TextFieldGroup)
                    {
                        if (concatFieldInfoControl == null)
                            createConcatFieldInfoControl();
                        concatFieldInfoControl.HideTextFieldProperties();
                        concatFieldInfoControl.FillData(tempField);
                        bringToFront(FieldInfoType.ConcatField);
                    }
                    else if (tempField is LabelDef.TextField)
                    {
                        if (textFieldInfoControl == null)
                            createTextFieldInfoControl();
                        textFieldInfoControl.FillData(tempField);
                        bringToFront(FieldInfoType.TextField);
                    }
                    else if (tempField is LabelDef.ImageField)
                    {
                        if (imageFieldInfoControl == null)
                            createImageFieldInfoControl();
                        imageFieldInfoControl.FillData(tempField);
                        bringToFront(FieldInfoType.ImageField);
                    }
                    else if (tempField is LabelDef.BarcodeField)
                    {
                        if (barcodeFieldInfoControl == null)
                            createBarcodeFieldInfoControl();
                        barcodeFieldInfoControl.FillData(tempField);
                        bringToFront(FieldInfoType.BarcodeField);
                    }


                    if (useditemslist.Items.Count > 0)
                        deletebtn.Enabled = true;
                    if (useditemslist.Items.Count > 1)
                    {
                        SortDownBtn.Enabled = true;
                        SortUpBtn.Enabled = true;
                    }
                    else
                    {
                        SortDownBtn.Enabled = false;
                        SortUpBtn.Enabled = false;
                    }
                    RedrawPictureBox();
                }
            }
            else
            {
                bringToFront(FieldInfoType.GenericField);
                RedrawPictureBox();
            }
        }
        private void ReloadUseditemslist()
        {
            useditemslist.Items.Clear();
            useditemslistDictionary.Clear();
            foreach (KeyValuePair<string, LabelDef.Field> pair in referenceList)
            {
                if (pair.Value is LabelDef.TextFieldGroup)
                {
                    addToUsedItemsListDictionaryWithoutPath(pair.Value.ID);
                }
                else
                {
                    ACA.LabelX.Label.Label.Value value;
                    if (pair.Value.ValueRef == null)
                        completeValues.TryGetValue(pair.Key + ".1043", out value); //TODO: language support
                    else
                        completeValues.TryGetValue(pair.Value.ValueRef + ".1043", out value);

                    addToUsedItemsListDictionaryWithoutPath(pair.Key + " (" + value.Data + ")");
                }
            }
        }
        private void createConcatFieldInfoControl()
        {
            concatFieldInfoControl = new ACA.LabelX.Controls.ConcatFieldInfoControl(referenceList, labelset, fontList, this);
            concatFieldInfoControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            concatFieldInfoControl.Name = "concatFieldInfoControl";
            concatFieldInfoControl.Location = genericFieldInfoControl1.Location;
            concatFieldInfoControl.Dock = genericFieldInfoControl1.Dock; 
            this.splitContainer3.Panel1.Controls.Add(concatFieldInfoControl);
        }
        private void bringToFront(FieldInfoType fieldInfoType)
        {
            if (genericFieldInfoControl1 != null)
                genericFieldInfoControl1.Visible = false;
            if (textFieldInfoControl != null)
                textFieldInfoControl.Visible = false;
            if (imageFieldInfoControl != null)
                imageFieldInfoControl.Visible = false;
            if (barcodeFieldInfoControl != null)
                barcodeFieldInfoControl.Visible = false;
            if (concatFieldInfoControl != null)
                concatFieldInfoControl.Visible = false;

            if (fieldInfoType == FieldInfoType.TextField)
                textFieldInfoControl.Visible = true;
            else if (fieldInfoType == FieldInfoType.ImageField)
                imageFieldInfoControl.Visible = true;
            else if (fieldInfoType == FieldInfoType.BarcodeField)
                barcodeFieldInfoControl.Visible = true;
            else if (fieldInfoType == FieldInfoType.GenericField)
                genericFieldInfoControl1.Visible = true;
            else if (fieldInfoType == FieldInfoType.ConcatField)
                concatFieldInfoControl.Visible = true;

            setDrawStatus();
        }
        private void createTextFieldInfoControl()
        {
            textFieldInfoControl = new ACA.LabelX.Controls.TextFieldInfoControl(referenceList, labelset, fontList, this);
            textFieldInfoControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            textFieldInfoControl.Name = "TextFieldInfoControl";
            textFieldInfoControl.Location = genericFieldInfoControl1.Location;
            textFieldInfoControl.Dock = genericFieldInfoControl1.Dock; 
            this.splitContainer3.Panel1.Controls.Add(textFieldInfoControl);
        }
        private void createImageFieldInfoControl()
        {
            imageFieldInfoControl = new ACA.LabelX.Controls.ImageFieldInfoControl(referenceList, labelset, this);
            imageFieldInfoControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            imageFieldInfoControl.Name = "ImageFieldInfoControl";
            imageFieldInfoControl.Location = genericFieldInfoControl1.Location;
            imageFieldInfoControl.Dock = genericFieldInfoControl1.Dock; 
            this.splitContainer3.Panel1.Controls.Add(imageFieldInfoControl);
        }
        private void createBarcodeFieldInfoControl()
        {
            barcodeFieldInfoControl = new ACA.LabelX.Controls.BarcodeFieldInfoControl(referenceList, labelset, this);
            barcodeFieldInfoControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            barcodeFieldInfoControl.Name = "barcodeFieldInfoControl";
            barcodeFieldInfoControl.Location = genericFieldInfoControl1.Location;
            barcodeFieldInfoControl.Dock = genericFieldInfoControl1.Dock; 
            this.splitContainer3.Panel1.Controls.Add(barcodeFieldInfoControl);
        }
        private void setDrawStatus()
        {
            bool zoomed = false;
            bool rotated = false;
            bool zoomedandrotated = false;
            zoomed = zoom150pct.Checked || zoom200pct.Checked || zoomCustom.Checked;
            rotated = rotate90DegreesToolStripMenuItem.Checked || rotate90DegreesToolStripMenuItem1.Checked;
            zoomedandrotated = zoomed && rotated;
            ACA.LabelX.Controls.GenericFieldInfoControl.Statuses status;
            ACA.LabelX.Controls.GenericFieldInfoControl.Rotations rotation = ACA.LabelX.Controls.GenericFieldInfoControl.Rotations.norotation;
            if (rotate90DegreesToolStripMenuItem.Checked) rotation = ACA.LabelX.Controls.GenericFieldInfoControl.Rotations.degrees90;
            if (rotate90DegreesToolStripMenuItem1.Checked) rotation = ACA.LabelX.Controls.GenericFieldInfoControl.Rotations.degreesminus90;
            status = ACA.LabelX.Controls.GenericFieldInfoControl.Statuses.normal;
            if (zoomedandrotated)
            {
                status = ACA.LabelX.Controls.GenericFieldInfoControl.Statuses.rotatezoom;
            }
            else
            {
                if (zoomed)
                {
                    status = ACA.LabelX.Controls.GenericFieldInfoControl.Statuses.zoom;
                }
                if (rotated)
                {
                    status = ACA.LabelX.Controls.GenericFieldInfoControl.Statuses.rotate;
                }
            }
            if (genericFieldInfoControl1 != null)
            {
                genericFieldInfoControl1.Status = status;
                genericFieldInfoControl1.Rotation = rotation;
            }
            if (textFieldInfoControl != null)
            {
                textFieldInfoControl.Status = status;
                textFieldInfoControl.Rotation = rotation;
            }
            if (imageFieldInfoControl != null)
            {
                imageFieldInfoControl.Status = status;
                imageFieldInfoControl.Rotation = rotation;
            }
            if (barcodeFieldInfoControl != null)
            {
                barcodeFieldInfoControl.Status = status;
                barcodeFieldInfoControl.Rotation = rotation;
            }
            if (concatFieldInfoControl != null)
            {
                concatFieldInfoControl.Status = status;
                concatFieldInfoControl.Rotation = rotation;
            }
        }
        private void SortDownBtn_Click(object sender, EventArgs e)
        {
            int index = useditemslist.SelectedIndex;
            object swap = useditemslist.SelectedItem;
            if (index != -1 && index != useditemslist.Items.Count - 1)
            {
                useditemslist.Items.RemoveAt(index);
                useditemslist.Items.Insert(index + 1, swap);
                useditemslist.SelectedItem = swap;
            }

            List<string> list = new List<string>();
            foreach (string value in useditemslist.Items)
            {
                list.Add(value);
            }
            OrderReferenceList(list);

        }
        private void SortUpBtn_Click(object sender, EventArgs e)
        {
            int index = useditemslist.SelectedIndex;
            object swap = useditemslist.SelectedItem;
            if (index != -1 && index != 0)
            {
                useditemslist.Items.RemoveAt(index);
                useditemslist.Items.Insert(index - 1, swap);
                useditemslist.SelectedItem = swap;
            }

            List<string> list = new List<string>();
            foreach (string value in useditemslist.Items)
            {
                list.Add(value);
            }
            OrderReferenceList(list);

        }
        private void OrderReferenceList(List<string> list)
        {
            IDictionary<string, LabelDef.Field> old_referencelist = new Dictionary<string, LabelDef.Field>(referenceList);

            referenceList.Clear();

            foreach (string id in list)
            {
                LabelDef.Field fieldToAdd;
                old_referencelist.TryGetValue(getValueFromUsedItemsListDictionaryItem(id).Split('(')[0].Trim(), out fieldToAdd);
                referenceList.Add(fieldToAdd.ID, fieldToAdd);
            }
        }
        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                try
                {
                    TreeNode targetNode = treeView1.GetNodeAt(e.X, e.Y);
                    if (targetNode.FullPath.Contains('('))
                    {
                        treeView1.SelectedNode = targetNode;
                        if (targetNode != null)
                        {
                            treeView1.DoDragDrop(targetNode.FullPath, DragDropEffects.Copy);
                        }
                    }
                }
                catch { }
            }
        }
        private void aboutACALabelprintDesignerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Designer_About_Box1 dialog;
            dialog = new Designer_About_Box1();
            dialog.ShowDialog();
        }
        private void addTextfieldbtn_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null && treeView1.SelectedNode.Text.Contains('('))//Treeview
            {
                AddTextFieldForm addTextFieldForm = new AddTextFieldForm(fontList, referenceList, Toolbox.getSelectedIdFromTreeView(treeView1), Toolbox.getSelectedValueFromTreeView(treeView1)); //Treeview
                if (addTextFieldForm.ShowDialog() == DialogResult.OK)
                {
                    fontList = addTextFieldForm.FontList;
                    referenceList.Add(addTextFieldForm.Textfield.ID, addTextFieldForm.Textfield);
                    addToUsedItemsListDictionaryWithoutPath(addTextFieldForm.Textfield.ID + " (" + Toolbox.getSelectedValueFromTreeView(treeView1) + ")");
                    useditemslist.SelectedIndex = useditemslist.Items.Count - 1;
                    RedrawPictureBox();
                }
            }
            else
            {
                MessageBox.Show(GetString("SELECTANITEMFIRSTERROR"));
            }
        }
        private void addimagebtn_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null && treeView1.SelectedNode.Text.Contains('('))//Treeview
            {
                AddImageForm addImageForm = new AddImageForm(referenceList, Toolbox.getSelectedIdFromTreeView(treeView1), Toolbox.getSelectedValueFromTreeView(treeView1)); //Treeview
                if (addImageForm.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        referenceList.Add(addImageForm.Imagefield.ID, addImageForm.Imagefield);
                        addToUsedItemsListDictionaryWithoutPath(addImageForm.Imagefield.ID + " (" + Toolbox.getSelectedValueFromTreeView(treeView1) + ")");
                    }
                    catch (System.ArgumentException)
                    {
                        MessageBox.Show("There is already an item on this label with the name " + addImageForm.Imagefield.ID);//GetString()
                    }
                    useditemslist.SelectedIndex = useditemslist.Items.Count - 1;
                    RedrawPictureBox();
                }
            }
            else
            {
                MessageBox.Show("Select an item in the list first.");//GetString()
            }
        }
        private void addBarcodebtn_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null && treeView1.SelectedNode.Text.Contains('('))//Treeview
            {
                AddBarcodeForm addBarcodeForm = new AddBarcodeForm(referenceList, Toolbox.getSelectedIdFromTreeView(treeView1), Toolbox.getSelectedValueFromTreeView(treeView1));//Treeview
                if (addBarcodeForm.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        referenceList.Add(addBarcodeForm.Barcodefield.ID, addBarcodeForm.Barcodefield);
                        addToUsedItemsListDictionaryWithoutPath(addBarcodeForm.Barcodefield.ID + " (" + Toolbox.getSelectedValueFromTreeView(treeView1) + ")");
                    }
                    catch (System.ArgumentException)
                    {
                        MessageBox.Show("There is already an item on this label with the name " + addBarcodeForm.Barcodefield.ID);//GetString()
                    }
                    useditemslist.SelectedIndex = useditemslist.Items.Count - 1;
                    RedrawPictureBox();
                }
            }
            else
            {
                MessageBox.Show("Select an item in the list first.");//GetString()
            }
        }
        private void addconcatbtn_Click(object sender, EventArgs e)
        {
            if (useditemslist.Items.Count >= 2)
            {
                AddConcatForm addConcatForm = new AddConcatForm(fontList, referenceList, this);
                if (!addConcatForm.IsDisposed && addConcatForm.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        fontList = addConcatForm.Fontlist;
                        referenceList.Add(addConcatForm.Textfieldgroup.ID, addConcatForm.Textfieldgroup);
                        addToUsedItemsListDictionaryWithoutPath(addConcatForm.Textfieldgroup.ID);
                        foreach (LabelDef.TextField temptextfield in addConcatForm.Textfieldgroup.fields)
                        {
                            DeleteField(temptextfield);
                        }
                    }
                    catch (System.ArgumentException)
                    {
                        MessageBox.Show(GetString("ITEMNAMEEXISTSERROR") + addConcatForm.Textfieldgroup.ID);
                    }
                    useditemslist.SelectedIndex = useditemslist.Items.Count - 1;
                    RedrawPictureBox();
                }
            }
            else
            {
                MessageBox.Show(GetString("NEEDTWOTEXTFIELDSERROR"), GetString("NEEDTWOTEXTFIELDSERRORTITLE"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void scriptingBtn_Click(object sender, EventArgs e)
        {
            ACA.LabelX.Label.LabelSet labelset = new LabelSet();
            labelset.CurrentLabel = printJob.labels[0];
            labelset.DefaultLabel = printJob.Defaultlabel;
            labelset.BaseLabel = printJob.LabelDef.DefaultLabel;
            labelset.StaticVarsLabel = printJob.StaticVarslabel;
            LuaEditor luaEditor = new LuaEditor(luaCode, labelset);

            if (luaEditor.ShowDialog() == DialogResult.OK)
            {
                luaCode = luaEditor.code;
            }
        }
        private void addItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddItemForm additemform = new AddItemForm();
            if (additemform.ShowDialog() == DialogResult.OK)
            {
                ACA.LabelX.Label.Label.Value tempvalue = new ACA.LabelX.Label.Label.Value();
                tempvalue.Key = additemform.id;
                tempvalue.Data = additemform.value;
                tempvalue.Language = additemform.language;
                tempvalue.Type = "xs:string";
                try
                {
                    printJob.LabelDef.DefaultLabel.Values.Add(additemform.id + "." + additemform.language, tempvalue);
                }
                catch
                {
                    MessageBox.Show("An item with this name already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                FillAvailableitemsList();
            }
        }
        private void addFontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InputBox inputbox = new InputBox(GetString("FONTNAMEQUESTION"));
            if (inputbox.ShowDialog() != DialogResult.Cancel)
            {
                if (inputbox.Answer.Equals("") || inputbox.Answer.Contains(' '))
                    MessageBox.Show(GetString("INVALIDFONTNAME"), GetString("INVALIDFONTNAME"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (fontList.ContainsKey(inputbox.Answer))
                {
                    MessageBox.Show(GetString("FONTNAMEEXISTSERROR"), GetString("FONTNAMEEXISTSERRORTITLE"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    FontDialog fontdialog = new FontDialog();
                    if (fontdialog.ShowDialog() != DialogResult.Cancel)
                    {
                        {
                            LabelDef.FontX newfontx = new LabelDef.FontX();
                            newfontx.ID = inputbox.Answer;
                            newfontx.Font = fontdialog.Font;
                            newfontx.Style = fontdialog.Font.Style;

                            switch (MessageBox.Show(GetString("INVERTFONTQUESTION"), GetString("INVERTFONTQUESTIONTITLE"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                            {
                                case DialogResult.Yes:
                                    newfontx.InversePrint = true;
                                    break;
                                case DialogResult.No:
                                    newfontx.InversePrint = false;
                                    break;
                            }
                            fontList.Add(newfontx.ID, newfontx);
                        }
                    }
                }
            }
            useditemslist_SelectedIndexChanged(null, null);
        }
        private void addPaperToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewPaperTypeForm newPaperTypeForm = new NewPaperTypeForm(paperDefinitionsRootFolder);
            if (newPaperTypeForm.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("The new paper file has been saved.", "File Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void newLabelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewLabelDefForm newLabelDefForm = new NewLabelDefForm(paperDefinitionsRootFolder, labelDefinitionsRootFolder);
            if (newLabelDefForm.ShowDialog() == DialogResult.OK)
            {
                if (CheckLabelDefCompatible(labelDefinitionsRootFolder + @"\" + newLabelDefForm.LabelName + ".xml", printJob))
                    LoadLabelDef(labelDefinitionsRootFolder + @"\" + newLabelDefForm.LabelName + ".xml");
            }
        }

        private void zoom100pct_Click(object sender, EventArgs e)
        {
            zoomFactor = 1.0f;

            if (drawingPictureBox != null)
            {
                drawingPictureBox.ZoomFactor = zoomFactor;
                RedrawPictureBox();
            }
            SetZoomSelected(1);
        }

        private void zoom150pct_Click(object sender, EventArgs e)
        {
            zoomFactor = 1.5f;
            if (drawingPictureBox != null)
            {
                drawingPictureBox.ZoomFactor = zoomFactor;
                drawingPictureBox.Size = new System.Drawing.Size(10, 10);
                //drawingPictureBox.Invalidate();
                RedrawPictureBox();
            }
            SetZoomSelected(2);
        }

        private void zoom200pct_Click(object sender, EventArgs e)
        {
            zoomFactor = 2.0f;
            if (drawingPictureBox != null)
            {
                drawingPictureBox.ZoomFactor = zoomFactor;
                drawingPictureBox.Size = new System.Drawing.Size(10, 10);
                //drawingPictureBox.Invalidate();
                RedrawPictureBox();
            }
            SetZoomSelected(3);
        }

        private void zoomCustom_Click(object sender, EventArgs e)
        {
            FormCustomZoomFactor custZoom = new FormCustomZoomFactor();
            custZoom.ZoomFactor = zoomFactor;
            if (custZoom.ShowDialog() == DialogResult.OK)
            {
                zoomFactor = custZoom.ZoomFactor;
            }
            if (drawingPictureBox != null)
            {
                drawingPictureBox.ZoomFactor = zoomFactor;
                drawingPictureBox.Size = new System.Drawing.Size(10, 10);
                //drawingPictureBox.Invalidate();
                RedrawPictureBox();
            }
            SetZoomSelected(4);
        }

        private void SetZoomSelected(int where)
        {
            zoom100pct.Checked = false;
            zoom200pct.Checked = false;
            zoom150pct.Checked = false;
            zoomCustom.Checked = false;
            switch (where)
            {
                case 1:
                    zoom100pct.Checked = true;
                    break;
                case 2:
                    zoom150pct.Checked = true;
                    break;
                case 3:
                    zoom200pct.Checked = true;
                    break;
                case 4:
                    zoomCustom.Checked = true;
                    break;
            }
            setDrawStatus();
        }


        private void rotate90DegreesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            noRotationToolStripMenuItem.Checked = false;
            rotate90DegreesToolStripMenuItem.Checked = true;
            rotate90DegreesToolStripMenuItem1.Checked = false;
            rotation = -90.0f;
            if (drawingPictureBox != null)
            {
                drawingPictureBox.Rotation = rotation;
                RedrawPictureBox();
            }
            setDrawStatus();
        }

        private void rotate90DegreesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            noRotationToolStripMenuItem.Checked = false;
            rotate90DegreesToolStripMenuItem.Checked = false;
            rotate90DegreesToolStripMenuItem1.Checked = true;
            rotation = 90.0f;
            if (drawingPictureBox != null)
            {
                drawingPictureBox.Rotation = rotation;
                RedrawPictureBox();
            }
            setDrawStatus();
        }

        private void noRotationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            noRotationToolStripMenuItem.Checked = true;
            rotate90DegreesToolStripMenuItem.Checked = false;
            rotate90DegreesToolStripMenuItem1.Checked = false;
            rotation = 0.0f;
            if (drawingPictureBox != null)
            {
                drawingPictureBox.Rotation = rotation;
                RedrawPictureBox();
            }
            setDrawStatus();
        }
        private void openLabelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = labelDefinitionsRootFolder;
            openFileDialog.Filter = "XML File|*.xml";
            openFileDialog.Title = "Load XML File";
            openFileDialog.ShowDialog();

            if (!(string.IsNullOrEmpty(openFileDialog.FileName)))
            {
                if (CheckLabelDefCompatible(openFileDialog.FileName, printJob))
                    LoadLabelDef(openFileDialog.FileName);
            }
        }
        private void editLabelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult reply = MessageBox.Show("Editting your label requires you to save first. \nWould you like to save now and continue?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (reply == DialogResult.Yes)
            {
                saveLabelDefTo(currentLabelDef);
                LabelDef savedLabelDef = new LabelDef();
                savedLabelDef.Parse(currentLabelDef);
                printJob.LabelDef = savedLabelDef;

                NewLabelDefForm newLabelDefForm = new NewLabelDefForm(paperDefinitionsRootFolder, labelDefinitionsRootFolder, printJob.LabelDef);
                if (newLabelDefForm.ShowDialog() == DialogResult.OK)
                {
                    if (CheckLabelDefCompatible(labelDefinitionsRootFolder + @"\" + newLabelDefForm.LabelName + ".xml", printJob))
                        LoadLabelDef(labelDefinitionsRootFolder + @"\" + newLabelDefForm.LabelName + ".xml");
                }
            }
        }
        private void editFontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectListForm editfontForm = new SelectListForm(GetString("SELECTFONTTOEDIT"));
            foreach (string font in fontList.Keys)
            {
                editfontForm.AddItem(font);
            }
            editfontForm.ShowDialog();
            if (editfontForm.DialogResult == DialogResult.OK)
            {
                LabelDef.FontX fontx;
                fontList.TryGetValue(editfontForm.result, out fontx);
                if (fontx != null)
                {
                    FontDialog fontdialog = new FontDialog();
                    fontdialog.Font = fontx.Font;
                    if (fontdialog.ShowDialog() == DialogResult.OK)
                    {
                        fontx.Font = fontdialog.Font;
                        fontx.Style = fontdialog.Font.Style;

                        switch (MessageBox.Show(GetString("INVERTFONTQUESTION"), GetString("INVERTFONTQUESTIONTITLE"), MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                        {
                            case DialogResult.Yes:
                                fontx.InversePrint = true;
                                break;
                            case DialogResult.No:
                                fontx.InversePrint = false;
                                break;
                        }
                        if (useditemslist.Items.Count > 0)
                        {
                            useditemslist_SelectedIndexChanged(null, null);
                        }
                    }
                }
            }
        }
        private void editItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectListForm edititemForm = new SelectListForm(GetString("SELECTITEMTOEDIT"));
            foreach (string item in printJob.LabelDef.DefaultLabel.Values.Keys)
            {
                edititemForm.AddItem(item);
            }
            edititemForm.ShowDialog();
            if (edititemForm.DialogResult == DialogResult.OK)
            {
                ACA.LabelX.Label.Label.Value item;
                printJob.LabelDef.DefaultLabel.Values.TryGetValue(edititemForm.result, out item);
                if (item != null)
                {
                    AddItemForm edititem = new AddItemForm(item);
                    edititem.ShowDialog();
                    if (edititem.DialogResult == DialogResult.OK)
                    {
                        ACA.LabelX.Label.Label.Value tempvalue = new ACA.LabelX.Label.Label.Value();
                        tempvalue.Key = edititem.id;
                        tempvalue.Data = edititem.value;
                        tempvalue.Language = edititem.language;
                        tempvalue.Type = "xs:string";
                        item = tempvalue;
                        printJob.LabelDef.DefaultLabel.Values.Remove(edititemForm.result);
                        printJob.LabelDef.DefaultLabel.Values.Add(edititemForm.result, item);
                        FillAvailableitemsList();
                        ReloadUseditemslist();
                        useditemslist_SelectedIndexChanged(null, null);
                    }
                }
            }
        }
        private void editPaperToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult reply = MessageBox.Show("Editting paper types requires you to save first. \nWould you like to save now and continue?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (reply == DialogResult.Yes)
            {
                saveLabelDefTo(currentLabelDef);
                LabelDef savedLabelDef = new LabelDef();
                savedLabelDef.Parse(currentLabelDef);
                printJob.LabelDef = savedLabelDef;

                SelectListForm editpaperForm = new SelectListForm(GetString("SELECTPAPERTOEDIT"));
                string[] filenames = Directory.GetFiles(paperDefinitionsRootFolder);
                foreach (string filename in filenames)
                {
                    editpaperForm.AddItem(filename);
                }

                editpaperForm.ShowDialog();
                if (editpaperForm.DialogResult == DialogResult.OK)
                {
                    if (!string.IsNullOrEmpty(editpaperForm.result))
                    {
                        PaperDef paper = new PaperDef();
                        paper.Parse(editpaperForm.result);
                        NewPaperTypeForm editpapertypeform = new NewPaperTypeForm(paperDefinitionsRootFolder, paper);
                        editpapertypeform.ShowDialog();
                        if (editpapertypeform.DialogResult == DialogResult.OK)
                        {
                            if (CheckLabelDefCompatible(currentLabelDef, printJob))
                                LoadLabelDef(currentLabelDef);
                        }
                    }
                }
            }
        }
        private void saveLabelDefTo(string path)
        {
            XmlTextWriter xmlWriter = new XmlTextWriter(path, Encoding.UTF8);
            xmlWriter.Formatting = Formatting.Indented;
            xmlWriter.WriteStartDocument();

            xmlWriter.WriteStartElement("labeldef");
            xmlWriter.WriteAttributeString("id", printJob.LabelDef.ID);
            xmlWriter.WriteStartElement("validpapertypes");
            foreach (LabelDef.PaperType papertype in printJob.LabelDef.PaperTypes)
            {
                xmlWriter.WriteStartElement("paper");
                xmlWriter.WriteAttributeString("type", papertype.ID);
                xmlWriter.WriteAttributeString("default", papertype.Default.ToString().ToLower());
                xmlWriter.WriteEndElement(); //</paper>
            }
            xmlWriter.WriteEndElement(); //</validpapertypes>

            xmlWriter.WriteStartElement("coordinates");
            xmlWriter.WriteStartElement("dpifactor");
            xmlWriter.WriteAttributeString("x", printJob.LabelDef.coordinateSystem.dpiFactor.X.ToString());
            xmlWriter.WriteAttributeString("y", printJob.LabelDef.coordinateSystem.dpiFactor.Y.ToString());
            xmlWriter.WriteEndElement(); //</dpifactor>
            xmlWriter.WriteElementString("units", printJob.LabelDef.coordinateSystem.units.UnitType.ToString());
            xmlWriter.WriteEndElement(); //</coordinates>

            xmlWriter.WriteStartElement("definition");
            xmlWriter.WriteStartElement("fonts");
            foreach (KeyValuePair<string, LabelDef.FontX> fontPair in fontList)
            {
                xmlWriter.WriteStartElement("font");
                xmlWriter.WriteAttributeString("id", fontPair.Value.ID);
                xmlWriter.WriteElementString("typeface", fontPair.Value.Font.Name);             
   
                //mve fix xmlWriter.WriteElementString("size", fontPair.Value.Font.SizeInPoints.ToString());
                xmlWriter.WriteElementString("size", fontPair.Value.Font.SizeInPoints.ToString(System.Globalization.CultureInfo.InvariantCulture));

                xmlWriter.WriteElementString("inverseprint", fontPair.Value.InversePrint.ToString().ToLower());

                string fontinfo = "";

                if (fontPair.Value.Font.Bold)
                    fontinfo += "bold,";
                if (fontPair.Value.Font.Italic)
                    fontinfo += "italic,";
                if (fontPair.Value.Font.Underline)
                    fontinfo += "underLine,";
                if (fontPair.Value.Font.Strikeout)
                    fontinfo += "strikeout,";
                if (fontinfo.EndsWith(","))
                {
                    fontinfo = fontinfo.Substring(0, fontinfo.Length - 1);
                }
                if (!fontinfo.Equals(""))
                    xmlWriter.WriteElementString("style", fontinfo);

                xmlWriter.WriteEndElement();
            }
            xmlWriter.WriteEndElement(); //</fonts>

            xmlWriter.WriteStartElement("fields");
            foreach (KeyValuePair<string, LabelDef.Field> fieldPair in referenceList)
            {
                if (fieldPair.Value is LabelDef.TextFieldGroup)
                {
                    LabelDef.TextFieldGroup tempTextFieldGroup = (LabelDef.TextFieldGroup)fieldPair.Value;
                    xmlWriter.WriteStartElement("textconcat");
                    xmlWriter.WriteAttributeString("id", tempTextFieldGroup.ID);
                    xmlWriter.WriteElementString("horzpos", tempTextFieldGroup.PositionX.length.ToString());
                    xmlWriter.WriteElementString("vertpos", tempTextFieldGroup.PositionY.length.ToString());
                    xmlWriter.WriteStartElement("printformat");
                    xmlWriter.WriteStartElement("format");
                    xmlWriter.WriteAttributeString("type", "xs:string");
                    xmlWriter.WriteAttributeString("align", tempTextFieldGroup.printFormat.format.Align.ToString());
                    xmlWriter.WriteEndElement(); //format
                    xmlWriter.WriteEndElement(); //printformat
                    if (tempTextFieldGroup.Width != null && tempTextFieldGroup.Width.length != 0)
                        xmlWriter.WriteElementString("width", tempTextFieldGroup.Width.length.ToString());
                    if (tempTextFieldGroup.Height != null && tempTextFieldGroup.Height.length != 0)
                        xmlWriter.WriteElementString("height", tempTextFieldGroup.Height.length.ToString());
                    xmlWriter.WriteElementString("rotation", tempTextFieldGroup.Rotation.ToString());
                    if (tempTextFieldGroup.concatMethod == LabelDef.TextFieldGroup.ConcatMethod.Vertical)
                    {
                        xmlWriter.WriteElementString("concatMethod", "vertical");
                    }
                    else
                    {
                        xmlWriter.WriteElementString("concatMethod", "horizontal");
                    }
                    
                    xmlWriter.WriteStartElement("fontref");
                    xmlWriter.WriteAttributeString("id", "Default");
                    xmlWriter.WriteEndElement(); //fontref
                    xmlWriter.WriteStartElement("internalfields");
                    foreach (LabelDef.TextField linkedfield in tempTextFieldGroup.fields)
                    {
                        xmlWriter.WriteStartElement("textfield");
                        xmlWriter.WriteAttributeString("id", linkedfield.ID);
                        if ((linkedfield.ValueRef != null) && (linkedfield.ValueRef != ""))
                        {
                            xmlWriter.WriteAttributeString("valueref", linkedfield.ValueRef);
                        }
                        xmlWriter.WriteStartElement("fontref");
                        xmlWriter.WriteAttributeString("id", linkedfield.Font.ID);
                        xmlWriter.WriteEndElement(); //fontref
                        xmlWriter.WriteStartElement("printformat");
                        xmlWriter.WriteStartElement("format");
                        if (linkedfield.printFormat.format is LabelDef.StringFormat)
                            xmlWriter.WriteAttributeString("type", "xs:string");
                        else if (linkedfield.printFormat.format is LabelDef.DateTimeFormat)
                        {
                            xmlWriter.WriteAttributeString("type", "xs:datetime");
                            xmlWriter.WriteAttributeString("dateformat", linkedfield.printFormat.format.FormatString);
                        }
                        else if (linkedfield.printFormat.format is LabelDef.DecimalFormat)
                        {
                            xmlWriter.WriteAttributeString("type", "xs:decimal");
                            LabelDef.DecimalFormat temp = (LabelDef.DecimalFormat)linkedfield.printFormat.format;
                            if (temp.Portion == LabelDef.DecimalFormat.DecimalPortion.Entire) { }
                            else if (temp.Portion == LabelDef.DecimalFormat.DecimalPortion.Fraction)
                                xmlWriter.WriteAttributeString("macro", "onlyfraction");
                            else if (temp.Portion == LabelDef.DecimalFormat.DecimalPortion.Integer)
                                xmlWriter.WriteAttributeString("macro", "onlywhole");

                            if (linkedfield.printFormat.format.FormatString != null)
                            {
                                if (linkedfield.printFormat.format.FormatString.Length > 0)
                                    xmlWriter.WriteAttributeString("format", linkedfield.printFormat.format.FormatString);
                            }
                        }
                        xmlWriter.WriteAttributeString("align", linkedfield.printFormat.format.Align.ToString());
                        xmlWriter.WriteEndElement(); //</format>
                        xmlWriter.WriteEndElement(); //printformat
                        xmlWriter.WriteEndElement(); //textfield
                    }
                    xmlWriter.WriteEndElement(); //internalfields
                    xmlWriter.WriteEndElement(); //textconcat
                }
                else if (fieldPair.Value is LabelDef.TextField)
                {
                    LabelDef.TextField tempTextField = (LabelDef.TextField)fieldPair.Value;
                    xmlWriter.WriteStartElement("textfield");
                    xmlWriter.WriteAttributeString("id", tempTextField.ID);
                    if (tempTextField.ValueRef != null)
                        xmlWriter.WriteAttributeString("valueref", tempTextField.ValueRef);
                    xmlWriter.WriteStartElement("fontref");
                    xmlWriter.WriteAttributeString("id", tempTextField.Font.ID);
                    xmlWriter.WriteEndElement(); //</fontref>
                    xmlWriter.WriteElementString("horzpos", tempTextField.PositionX.length.ToString());
                    xmlWriter.WriteElementString("vertpos", tempTextField.PositionY.length.ToString());
                    if (tempTextField.Width != null && tempTextField.Width.length != 0)
                        xmlWriter.WriteElementString("width", tempTextField.Width.length.ToString());
                    if (tempTextField.Height != null && tempTextField.Height.length != 0)
                        xmlWriter.WriteElementString("height", tempTextField.Height.length.ToString());
                    xmlWriter.WriteElementString("rotation", tempTextField.Rotation.ToString());
                    xmlWriter.WriteStartElement("printformat");
                    xmlWriter.WriteStartElement("format");
                    if (tempTextField.printFormat.format is LabelDef.StringFormat)
                        xmlWriter.WriteAttributeString("type", "xs:string");
                    else if (tempTextField.printFormat.format is LabelDef.DateTimeFormat)
                    {
                        xmlWriter.WriteAttributeString("type", "xs:datetime");
                        xmlWriter.WriteAttributeString("dateformat", tempTextField.printFormat.format.FormatString);
                    }
                    else if (tempTextField.printFormat.format is LabelDef.DecimalFormat)
                    {
                        xmlWriter.WriteAttributeString("type", "xs:decimal");
                        LabelDef.DecimalFormat temp = (LabelDef.DecimalFormat)tempTextField.printFormat.format;
                        if (temp.Portion == LabelDef.DecimalFormat.DecimalPortion.Entire) { }
                        else if (temp.Portion == LabelDef.DecimalFormat.DecimalPortion.Fraction)
                            xmlWriter.WriteAttributeString("macro", "onlyfraction");
                        else if (temp.Portion == LabelDef.DecimalFormat.DecimalPortion.Integer)
                            xmlWriter.WriteAttributeString("macro", "onlywhole");

                        if (tempTextField.printFormat.format.FormatString != null)
                        {
                            if (tempTextField.printFormat.format.FormatString.Length > 0)
                                xmlWriter.WriteAttributeString("format", tempTextField.printFormat.format.FormatString);
                        }
                    }
                    xmlWriter.WriteAttributeString("align", tempTextField.printFormat.format.Align.ToString());
                    xmlWriter.WriteEndElement(); //</format>
                    xmlWriter.WriteEndElement(); //</printformat>
                    xmlWriter.WriteEndElement(); //</textfield>
                }
                else if (fieldPair.Value is LabelDef.ImageField)
                {
                    LabelDef.ImageField tempImageField = (LabelDef.ImageField)fieldPair.Value;
                    xmlWriter.WriteStartElement("imagefield");
                    xmlWriter.WriteAttributeString("id", tempImageField.ID);
                    if (tempImageField.ValueRef != null)
                        xmlWriter.WriteAttributeString("valueref", tempImageField.ValueRef);
                    xmlWriter.WriteElementString("horzpos", tempImageField.PositionX.length.ToString());
                    xmlWriter.WriteElementString("vertpos", tempImageField.PositionY.length.ToString());
                    if (tempImageField.Width != null && tempImageField.Width.length != 0)
                        xmlWriter.WriteElementString("width", tempImageField.Width.length.ToString());
                    if (tempImageField.Height != null && tempImageField.Height.length != 0)
                        xmlWriter.WriteElementString("height", tempImageField.Height.length.ToString());
                    xmlWriter.WriteElementString("rotation", tempImageField.Rotation.ToString());
                    xmlWriter.WriteStartElement("imagestyle");
                    xmlWriter.WriteAttributeString("style", tempImageField.Scale.ToString());
                    xmlWriter.WriteAttributeString("keepratio", tempImageField.KeepRatio.ToString().ToLower());
                    xmlWriter.WriteAttributeString("colorstyle", tempImageField.Color.ToString());
                    xmlWriter.WriteEndElement(); //</imagestyle>                                                       
                    xmlWriter.WriteEndElement(); //</imagefield>
                }
                else if (fieldPair.Value is LabelDef.BarcodeField)
                {
                    LabelDef.BarcodeField tempBarcodeField = (LabelDef.BarcodeField)fieldPair.Value;
                    xmlWriter.WriteStartElement("barcodefield");
                    xmlWriter.WriteAttributeString("id", tempBarcodeField.ID);
                    if (tempBarcodeField.ValueRef != null)
                        xmlWriter.WriteAttributeString("valueref", tempBarcodeField.ValueRef);
                    xmlWriter.WriteStartElement("barcode");
                    xmlWriter.WriteAttributeString("type", tempBarcodeField.Type.ToString());
                    xmlWriter.WriteAttributeString("maxcharcount", tempBarcodeField.MaxCharCount.ToString());
                    xmlWriter.WriteAttributeString("printtext", tempBarcodeField.printText ? "true" : "false");
                    xmlWriter.WriteEndElement(); //</barcode>
                    xmlWriter.WriteElementString("horzpos", tempBarcodeField.PositionX.length.ToString());
                    xmlWriter.WriteElementString("vertpos", tempBarcodeField.PositionY.length.ToString());
                    xmlWriter.WriteElementString("width", tempBarcodeField.Width.length.ToString());
                    xmlWriter.WriteElementString("height", tempBarcodeField.Height.length.ToString());
                    xmlWriter.WriteElementString("rotation", tempBarcodeField.Rotation.ToString());
                    xmlWriter.WriteStartElement("printformat");
                    xmlWriter.WriteStartElement("format");
                    if (tempBarcodeField.printFormat.format is LabelDef.StringFormat)
                        xmlWriter.WriteAttributeString("type", "xs:string");
                    xmlWriter.WriteAttributeString("align", tempBarcodeField.printFormat.format.Align.ToString());
                    xmlWriter.WriteEndElement(); //</format>
                    xmlWriter.WriteEndElement(); //</printformat>
                    xmlWriter.WriteEndElement(); //</barcodefield>
                }
            }
            xmlWriter.WriteEndElement(); //</fields>
            xmlWriter.WriteEndElement(); //</definition>
            xmlWriter.WriteStartElement("label");
            xmlWriter.WriteAttributeString("type", "default");
            xmlWriter.WriteStartElement("options");
            xmlWriter.WriteElementString("quantity", "1");
            xmlWriter.WriteStartElement("cultureinfos");
            xmlWriter.WriteStartElement("cultureinfo");
            xmlWriter.WriteAttributeString("language", "system");
            xmlWriter.WriteEndElement(); //</cultureinfo>
            xmlWriter.WriteEndElement(); //</cultureinfos>
            xmlWriter.WriteEndElement(); //</options>

            //Default values
            xmlWriter.WriteStartElement("values");
            foreach (KeyValuePair<string, ACA.LabelX.Label.Label.Value> pair in printJob.LabelDef.DefaultLabel.Values)
            {
                xmlWriter.WriteStartElement("value");
                xmlWriter.WriteAttributeString("key", pair.Value.Key);
                xmlWriter.WriteAttributeString("type", pair.Value.Type);
                xmlWriter.WriteAttributeString("language", pair.Value.Language.ToString());
                xmlWriter.WriteRaw(pair.Value.Data);
                xmlWriter.WriteEndElement(); //</value>
            }

            xmlWriter.WriteEndElement(); //</values>
            xmlWriter.WriteEndElement(); //</label>

            xmlWriter.WriteStartElement("lua");
            xmlWriter.WriteCData(luaCode);
            xmlWriter.WriteEndElement(); //</lua>

            xmlWriter.WriteEndElement(); //</labeldef>
            xmlWriter.WriteEndDocument();
            xmlWriter.Flush();
            xmlWriter.Close();
        }
        private void deletePaperToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            RemoveForm removeForm = new RemoveForm(GetString("PAPERTYPES"));
            string[] filenames = Directory.GetFiles(paperDefinitionsRootFolder);
            foreach (string filename in filenames)
            {
                removeForm.AddItem(filename);
            }
            if (removeForm.ShowDialog() == DialogResult.OK)
            {
                foreach (string item in removeForm.Resultlist)
                {
                    try
                    {
                        if (!(paperDefinitionsRootFolder.EndsWith(@"\"))) paperDefinitionsRootFolder += @"\";
                        string currentPaperType = paperDefinitionsRootFolder + printJob.PaperDef.ID + ".xml";
                        if (currentPaperType.Equals(item, StringComparison.OrdinalIgnoreCase))
                        {
                            MessageBox.Show("Unable to remove paper type. It is in use.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                            File.Delete(item);
                    }
                    catch
                    {
                        MessageBox.Show("Error deleting file " + item + ".", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void deleteLabelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveForm removeForm = new RemoveForm(GetString("LABELS"));
            string[] filenames = Directory.GetFiles(labelDefinitionsRootFolder);
            foreach (string filename in filenames)
            {
                removeForm.AddItem(filename);
            }
            if (removeForm.ShowDialog() == DialogResult.OK)
            {
                foreach (string item in removeForm.Resultlist)
                {
                    try
                    {
                        if (currentLabelDef.Equals(item, StringComparison.OrdinalIgnoreCase))
                        {
                            MessageBox.Show("Error deleting label. It is in use", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                            File.Delete(item);
                    }
                    catch
                    {
                        MessageBox.Show("Error deleting file " + item + ".", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void deleteFontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveForm removeForm = new RemoveForm(GetString("FONTS"));
            foreach (KeyValuePair<string, LabelDef.FontX> pair in fontList)
            {
                removeForm.AddItem(pair.Key);
            }
            if (removeForm.ShowDialog() == DialogResult.OK)
            {
                foreach (string item in removeForm.Resultlist)
                {
                    if (fontList.ContainsKey(item))
                    {
                        if (item.Equals("Default"))
                        {
                            MessageBox.Show("Error deleting font. The Default font cannot be deleted", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            LabelDef.FontX fontx;
                            fontList.TryGetValue(item, out fontx);
                            RemoveFontReferences(fontx);
                            fontList.Remove(item);
                        }
                    }
                }
            }
            useditemslist_SelectedIndexChanged(null, null);
        }
        private void RemoveFontReferences(LabelDef.FontX font)
        {
            foreach (LabelDef.Field field in referenceList.Values)
            {
                if (field is LabelDef.TextField)
                {
                    LabelDef.TextField textfield = (LabelDef.TextField)field;
                    if (textfield.Font == font)
                    {
                        LabelDef.FontX defaultFont;
                        fontList.TryGetValue("Default", out defaultFont);
                        textfield.Font = defaultFont;
                    }
                }
            }
        }
        private void deleteItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveForm removeForm = new RemoveForm(GetString("ITEMS"));
            foreach (KeyValuePair<string, ACA.LabelX.Label.Label.Value> pair in printJob.LabelDef.DefaultLabel.Values)
            {
                removeForm.AddItem(pair.Key.Split('.')[0]);
            }
            if (removeForm.ShowDialog() == DialogResult.OK)
            {
                foreach (string item in removeForm.Resultlist)
                {
                    RemoveItemReferences(item);
                    RemoveItemFromUsedList(item);
                    RemoveItemFromAvailableList(item);
                }
            }
        }
        private void RemoveItemReferences(string item)
        {
            List<string> listofitems = new List<string>();
            foreach (string listitem in useditemslist.Items)
            {
                listofitems.Add(listitem);
            }
            foreach (string usedfield in listofitems)
            {
                LabelDef.Field tempfield;
                referenceList.TryGetValue(getValueFromUsedItemsListDictionaryItem(usedfield).Split('(')[0].Trim(), out tempfield);
                if (tempfield.ValueRef != null && tempfield.ValueRef.Equals(item))
                    RemoveItemFromUsedList(tempfield.ID);
            }
        }
        private void RemoveItemFromUsedList(string item)
        {
            LabelDef.Field fieldToDelete;
            referenceList.TryGetValue(item, out fieldToDelete);
            if (fieldToDelete != null)
            {

                referenceList.Remove(item);

                ACA.LabelX.Label.Label.Value value;
                string sID;
                if (fieldToDelete.ValueRef == null)
                {
                    sID = fieldToDelete.ID;
                }
                else
                {
                    sID = fieldToDelete.ValueRef;
                }

                value = labelset.TryGetValue(sID, 1043); //TODO: language support

                useditemslist.SelectedItem = removePath(item + " (" + value.Data + ")");
                useditemslistDictionary.Remove(useditemslist.SelectedItem.ToString());
                useditemslist.Items.RemoveAt(useditemslist.SelectedIndex);


                if (useditemslist.Items.Count > 0)
                    useditemslist.SelectedIndex = 0;

                if (useditemslist.Items.Count < 1)
                    deletebtn.Enabled = false;

                RedrawPictureBox();
                FillAvailableitemsList();
            }
        }
        private void RemoveItemFromAvailableList(string item)
        {
            if (printJob.LabelDef.DefaultLabel.Values.ContainsKey(item + ".1043"))
                printJob.LabelDef.DefaultLabel.Values.Remove(item + ".1043");
            FillAvailableitemsList();
        }

        private void LabelDesignerFrm_KeyDown(object sender, KeyEventArgs e)
        {
            ACA.LabelX.Controls.GenericFieldInfoControl temp = GetCurrentControl();
            if (temp != null)
            {
                if (e.KeyCode == Keys.Left)
                {
                    temp.moveField(4);//Left
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.Right)
                {
                    temp.moveField(3);//Right
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.Up)
                {
                    temp.moveField(1);//Up
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.Down)
                {
                    temp.moveField(2); //Down
                    e.Handled = true;
                }
            }
        }
        private ACA.LabelX.Controls.GenericFieldInfoControl GetCurrentControl()
        {
            ACA.LabelX.Controls.GenericFieldInfoControl temp = null;
            if (barcodeFieldInfoControl != null && barcodeFieldInfoControl.Visible)
            {
                temp = barcodeFieldInfoControl;
            }
            else if (imageFieldInfoControl != null && imageFieldInfoControl.Visible)
            {
                temp = imageFieldInfoControl;
            }
            else if (textFieldInfoControl != null && textFieldInfoControl.Visible)
            {
                temp = textFieldInfoControl;
            }
            else if (concatFieldInfoControl != null && concatFieldInfoControl.Visible)
            {
                temp = concatFieldInfoControl;
            }
            return temp;
        }
        //Drawing
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            RedrawPictureBox();
        }

        private void pictureboxPanel_Scroll(object sender, ScrollEventArgs e)
        {
            RedrawPictureBox();
        }

        private void saveLabelToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            saveLabelDefTo(currentLabelDef);
            LabelDef savedLabelDef = new LabelDef();
            savedLabelDef.Parse(currentLabelDef);
            printJob.LabelDef = savedLabelDef;
        }

        private void saveLabelAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = labelDefinitionsRootFolder;
            saveFileDialog.Filter = "XML File|*.xml";
            saveFileDialog.Title = "Save XML File";
            saveFileDialog.ShowDialog();
            if (saveFileDialog.FileName != "")
            {
                saveLabelDefTo(saveFileDialog.FileName);
                LabelDef savedLabelDef = new LabelDef();
                savedLabelDef.Parse(saveFileDialog.FileName);
                printJob.LabelDef = savedLabelDef;
                currentLabelDef = saveFileDialog.FileName;
                toolStripStatusLabelLabelName.Text = currentLabelDef;
            }

        }

        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Todo: dit maakt het bestand aan in de 'current default folder' waar dit ook is op het systeem. Dat is niet de bedoeling.
            //      ik zou zeggen... de print preview map gebruiken!
            saveLabelDefTo("PrintPreview.xml");
            LabelDef oldLabelDef = new LabelDef();
            oldLabelDef = printJob.LabelDef;

            LabelDef previewLabelDef = new LabelDef();
            previewLabelDef.Parse("PrintPreview.xml");
            printJob.LabelDef = previewLabelDef;

            PrintEngine printEngine = new PrintEngine("Preview");
            printEngine.AddPrintJob(printJob);
            printEngine.PrintPreview("Microsoft XPS Document Writer", printJob.PaperDef.ID, "Automatically Select", gds.DesignMode, 0, uint.MaxValue);
            //printEngine.PrintPreview("TEC B-SA4T", printJob.PaperDef.ID, "Automatically Select", gds.DesignMode, 0, uint.MaxValue);

            printJob.LabelDef = oldLabelDef;
        }

        private void toggleBordersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gds.DesignMode = !gds.DesignMode;
            if (gds.DesignMode)
            {
                toggleBordersToolStripMenuItem.Checked = true;
                toggleBordersToolStripMenuItem.Text = GetString("HIDEBORDERS");
            }
            else
            {
                toggleBordersToolStripMenuItem.Checked = false;
                toggleBordersToolStripMenuItem.Text = GetString("SHOWBORDERS");
            }
            RedrawPictureBox();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void whiteBackgroundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            whiteBackgroundToolStripMenuItem.Checked = !whiteBackgroundToolStripMenuItem.Checked; 
            if (drawingPictureBox != null)
            {
                drawingPictureBox.UseWhiteBackground = whiteBackgroundToolStripMenuItem.Checked;
                RedrawPictureBox();
            }  
            
        }
    }
}
