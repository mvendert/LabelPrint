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
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Diagnostics;
using IEC16022Sharp;
//using ZXing;

namespace ACA.Barcode
{

    abstract class Barcode
    {

        public StringAlignment Align = StringAlignment.Near;

        public bool SetCode(String Code, Boolean AddCheckDigit)
        {
            this.Code = Code;
            this.AddCheckDigit = AddCheckDigit;

            if (AddCheckDigit)
                OnAddCheckDigit(ref this.Code);

            this.BitString = "";
            String BitString = "";

            if (OnCreateBitString(this.Code, out BitString))
            {
                this.BitString = BitString;
            }

            return true;
        }

        public abstract void DrawBarcode(Boolean WithNumbers, Graphics Graph, float Left, float Top, float Width, float Height);

        public virtual void DrawBarcode(Boolean WithNumbers, Graphics Graph, float Left, float Top, float Width, float Height, int MaxCharacterCount)
        {
            if (WithNumbers)
                DrawWithNumbers(Graph, Left, Top, Width, Height, MaxCharacterCount);
            else
                DrawWithoutNumbers(Graph, Left, Top, Width, Height);
        }


        public float InPixels(float requestedWidth, int MaxCharacterCount)
        {
            float LineWidth;
            float Width;
            int BitCount = Code.Length;
            int DigitCount = BitString.Length;
            if (DigitCount < 1)
                return 0.0F;

            Boolean Odd = (BitCount & 1) > 0;

            Width = requestedWidth * (float)Code.Length / MaxCharacterCount;
            SizeF size = new SizeF(Width, 20);
            if (!GetOverlappedDigits())
            {
                LineWidth = Width / DigitCount;
            }
            else
            {
                SizeF InnerSize = size;
                if (Odd)
                {
                    InnerSize.Width *= (float)0.90;
                }

                LineWidth = InnerSize.Width / DigitCount;
            }
            return LineWidth * DigitCount;
        }

        protected void DrawWithNumbers(Graphics Graph, float Left, float Top, float requestedWidth, float Height, int MaxCharacterCount)
        {
            float Width;

            Width = requestedWidth;
            int BitCount = Code.Length;
            int DigitCount = BitString.Length;
            if (DigitCount < 1)
                return;

            Width *= (float)Code.Length / MaxCharacterCount;

            SizeF size = new SizeF(Width, Height);
            SizeF upperLeft = new SizeF(Left, Top);

            Boolean Odd = (BitCount & 1) > 0;

            if (!GetOverlappedDigits())
            {

                float LineWidth = Width / DigitCount;

                //Font font = new Font("Courier New", Width/Code.Length);
                Font font = new Font("Arial", Width / Code.Length);
                Pen pen = new Pen(Color.Black, LineWidth);

                SizeF TextSize = Graph.MeasureString(Code, font);

                float realLeft;
                float formatpos;
                realLeft = Left;
                formatpos = Left + ((Width - TextSize.Width) / 2);

                if (Align == StringAlignment.Center)
                {
                    realLeft = Left + (requestedWidth - Width) / 2;
                    formatpos = Left + ((Width - TextSize.Width) / 2) + ((requestedWidth - Width) / 2);
                }

                Graph.DrawString(Code,
                                    font,
                                    Brushes.Black,
                                    formatpos,
                                    Top + Height - TextSize.Height, new StringFormat());


                DrawWithoutNumbers(Graph, realLeft, Top, Width, Height - TextSize.Height);
                pen.Dispose();
                font.Dispose();
            }
            else
            {
                SizeF InnerSize = size;
                if (Odd)
                    InnerSize.Width *= (float)0.90;

                SizeF InnerUpperLeft = upperLeft;
                if (Odd)
                    InnerUpperLeft.Width += (float)(size.Width * .10);

                float LineWidth = InnerSize.Width / DigitCount;

                String BarcodeFirst = "";
                String BarcodeSecond = "";
                String BarcodeThird = "";

                int Digit = 0;

                int Half = BitCount / 2;
                if (Odd)
                {
                    BarcodeFirst += Code[Digit++];
                    Half = (BitCount - 1) / 2;
                }

                for (int n = 0; n < Half; n++)
                {
                    BarcodeSecond += Code[Digit++];
                }

                for (int n = 0; n < Half; n++)
                {
                    BarcodeThird += Code[Digit++];
                }

                Pen pen = new Pen(Color.Black, LineWidth);

                Boolean BeginOfFirstBlockDetected = false;
                Boolean EndOfFirstBlockDetected = false;
                Boolean BeginOfSecondBlockDetected = false;
                Boolean EndOfSecondBlockDetected = false;
                float FirstDigitBlockX = 0;
                float FirstDigitBlockEndX = 0;
                float SecondDigitBlockX = 0;
                float SecondDigitBlockEndX = 0;

                for (int n = 0; n < DigitCount; n++)
                {
                    if (BitString[n] == '1' || BitString[n] == '0')
                    {
                        if (!BeginOfFirstBlockDetected)
                        {
                            BeginOfFirstBlockDetected = true;
                            FirstDigitBlockX = InnerUpperLeft.Width + (n * LineWidth);
                        }
                        if (EndOfFirstBlockDetected && !BeginOfSecondBlockDetected)
                        {
                            BeginOfSecondBlockDetected = true;
                            SecondDigitBlockX = InnerUpperLeft.Width + (n * LineWidth);
                        }
                    }
                    else if (BitString[n] == '2' || BitString[n] == '3')
                    {
                        if (BeginOfFirstBlockDetected && !EndOfFirstBlockDetected)
                        {
                            EndOfFirstBlockDetected = true;
                            FirstDigitBlockEndX = InnerUpperLeft.Width + (n * LineWidth);
                        }
                        if (EndOfFirstBlockDetected && BeginOfSecondBlockDetected && !EndOfSecondBlockDetected)
                        {
                            EndOfSecondBlockDetected = true;
                            SecondDigitBlockEndX = InnerUpperLeft.Width + ((n - 1) * LineWidth);
                        }
                    }
                }

                float CharHeight = 0;
                if (BeginOfFirstBlockDetected && !BeginOfSecondBlockDetected)
                {
                    //Font font = new Font("Courier New", InnerSize.Width / (Code.Length + 1));
                    Font font = new Font("Arial", InnerSize.Width / (Code.Length + 1));
                    SizeF TextSize = Graph.MeasureString(Code, font);
                    CharHeight = font.Height;
                    float CharWidth = TextSize.Width / Code.Length;

                    if (Odd)
                        Graph.DrawString(BarcodeFirst, font, Brushes.Black, Left, InnerUpperLeft.Height + Height - CharHeight);

                    String Total = BarcodeSecond + BarcodeThird;
                    float CurrentLength = Total.Length * CharWidth;
                    Graph.DrawString(Total, font, Brushes.Black,
                        FirstDigitBlockX + (((FirstDigitBlockEndX - FirstDigitBlockX) - Graph.MeasureString(Total, font).Width) / 2),
                        InnerUpperLeft.Height + Height - CharHeight);
                    font.Dispose();
                }
                else if (BeginOfFirstBlockDetected && BeginOfSecondBlockDetected)
                {
                    StringFormat drawFormat = new StringFormat();
                    drawFormat.Alignment = StringAlignment.Center;

                    float width1 = FirstDigitBlockEndX - FirstDigitBlockX;
                    float width2 = SecondDigitBlockEndX - SecondDigitBlockX;

                    float size1 = width1 / BarcodeSecond.Length;
                    float size2 = width2 / BarcodeThird.Length;
                    float sizeSmallest = size1;
                    if (size2 < sizeSmallest)
                        sizeSmallest = size2;

                    //Font font = new Font("Courier New", sizeSmallest);
                    Font font = new Font("Arial", sizeSmallest);
                    SizeF TextSize = Graph.MeasureString(Code, font);
                    CharHeight = font.Height;
                    float CharWidth = TextSize.Width / Code.Length;

                    float Y = InnerUpperLeft.Height + Height - CharHeight;

                    RectangleF rect1 = new RectangleF(FirstDigitBlockX, Y, width1, CharHeight);
                    RectangleF rect2 = new RectangleF(SecondDigitBlockX, Y, width2, CharHeight);

                    if (Odd)
                        Graph.DrawString(BarcodeFirst, font, Brushes.Black, Left, InnerUpperLeft.Height + Height - CharHeight);

                    Graph.DrawString(BarcodeSecond, font, Brushes.Black, rect1, drawFormat);
                    Graph.DrawString(BarcodeThird, font, Brushes.Black, rect2, drawFormat);
                    font.Dispose();
                }

                for (int n = 0; n < DigitCount; n++)
                {
                    if (BitString[n] == '1' || BitString[n] == '2')
                    {
                        float X = InnerUpperLeft.Width + (n * LineWidth);
                        float Y1 = InnerUpperLeft.Height;
                        float Y2;

                        if (BitString[n] == '1')
                            Y2 = (InnerUpperLeft.Height + size.Height - CharHeight);
                        else
                            Y2 = (InnerUpperLeft.Height + size.Height - (CharHeight / 2));

                        Graph.DrawLine(pen, X, Y1, X, Y2);
                    }
                }
                pen.Dispose();
            }
        }

        protected void DrawWithoutNumbers(Graphics Graph, float Left, float Top, float Width, float Height)
        {
            //at the end this needs to be done in pixels as each calculation may cause some errors.

            int BitCount = BitString.Length;
            if (BitCount < 1)
                return;

            float LineWidth = Width / BitCount;

            Pen pen = new Pen(Color.Black, LineWidth);

            for (int n = 0; n < BitCount; n++)
            {
                if (BitString[n] == '1' || BitString[n] == '2')
                {
                    float X = Left + n * LineWidth;
                    float Y1 = Top;
                    float Y2 = Top + Height;

                    Graph.DrawLine(pen, X, Y1, X, Y2);
                }
            }
            pen.Dispose();
        }

        protected abstract Boolean GetOverlappedDigits();
        protected abstract bool OnCreateBitString(String Code, out String BitString);

        protected virtual void OnAddCheckDigit(ref String Code)
        {
        }

        public String BitString = "";
        public String Code = "";
        public Boolean AddCheckDigit = false;
    }

    class BarcodeEAN13 : Barcode
    {
        static protected String[,] Bitcodes = new String[10, 3]{
                                                    /*0.*/ {"0001101", "0100111", "1110010"},
                                                    /*1.*/ {"0011001", "0110011", "1100110"},
                                                    /*2.*/ {"0010011", "0011011", "1101100"},
                                                    /*3.*/ {"0111101", "0100001", "1000010"},
                                                    /*4.*/ {"0100011", "0011101", "1011100"},
                                                    /*5.*/ {"0110001", "0111001", "1001110"},
                                                    /*6.*/ {"0101111", "0000101", "1010000"},
                                                    /*7.*/ {"0111011", "0010001", "1000100"},
                                                    /*8.*/ {"0110111", "0001001", "1001000"},
                                                    /*9.*/ {"0001011", "0010111", "1110100"}
                                                    };

        static protected int[,] Lettertabel = new int[10, 6]{
	                                                    {1,1,1,1,1,1}, //0
	                                                    {1,1,2,1,2,2}, //1
	                                                    {1,1,2,2,1,2}, //2
	                                                    {1,1,2,2,2,1}, //3
	                                                    {1,2,1,1,2,2}, //4
	                                                    {1,2,2,1,1,2}, //5
	                                                    {1,2,2,2,1,1}, //6
	                                                    {1,2,1,2,1,2}, //7
	                                                    {1,2,1,2,2,1}, //8
	                                                    {1,2,2,1,2,1}
                                                    };

        protected static String Randteken = "232";
        protected static String Scheidingsteken = "32323";

        protected char[] EanDigits = new char[13];  // No including codedigit
        protected String[] BitmapStrings = new String[2];

        public BarcodeEAN13()
        {
        }

        public override void DrawBarcode(Boolean WithNumbers, Graphics Graph, float Left, float Top, float Width, float Height)
        {
            if (WithNumbers)
                DrawWithNumbers(Graph, Left, Top, Width, Height, 13);
            else
                DrawWithoutNumbers(Graph, Left, Top, Width, Height);
        }

        protected override Boolean GetOverlappedDigits()
        {
            return true;
        }

        int CalcCheckDigit()
        {
            int k;
            int faktor;
            int som;
            int cdigit;
            const int nNumEanDigits = 13;

            som = 0;
            faktor = 3;

            for (k = 1; k <= nNumEanDigits - 1; k++)
            {
                som += EanDigits[k] * faktor;
                faktor = 4 - faktor;                /* faktor alternerend 1 en 3    */
            }

            cdigit = som % 10;
            if (cdigit != 0)
            {
                cdigit = 10 - cdigit;
            }

            return (cdigit);
        }

        Boolean ImportEan(String EanString, Boolean Addcode)
        {
            int nTeller = EanString.Length;
            int nI, nJ, nOffset;
            int nChecksum;

            if (Addcode == true)
            {
                nI = 1;
            }
            else
            {
                nI = 0;
            }

            nI += EanString.Length;

            if (Addcode == true)
            {
                EanDigits[0] = '0';
                nOffset = 1;
            }
            else
            {
                nOffset = 0;
            }

            for (nI = nTeller - 1, nJ = nOffset; nI >= 0; nI--, nJ++)
            {
                char eanchar = EanString[nI];

                if (Char.IsDigit(eanchar))
                {
                    EanDigits[nJ] = Convert.ToChar(Convert.ToInt32(eanchar) - Convert.ToInt32('0'));
                }
                else
                {
                    EanDigits[nJ] = Convert.ToChar(0);
                    return false;
                }
            }

            nChecksum = CalcCheckDigit();
            if (Addcode == true)
            {
                this.Code += Convert.ToChar(nChecksum + '0');
                EanDigits[0] = Convert.ToChar(nChecksum);
                return true;
            }
            else
            {
                if (EanDigits[0] == nChecksum)
                {
                    return true;
                }
                else
                {
                    return true;
                }
            }
        }

        Boolean CreateBitmapString()
        {
            //	Aanmaken van bitmapstring rechts
            int i;

            BitmapStrings[0] = "";
            for (i = 5; i >= 0; i--)
            {
                BitmapStrings[0] += Bitcodes[EanDigits[i], 2];
            }

            // Aanmaken van de bitmapstring links
            BitmapStrings[1] = "";
            for (i = 11; i >= 6; i--)
            {
                int col = EanDigits[12];
                int welke_tabel = Lettertabel[col, 11 - i];
                BitmapStrings[1] += Bitcodes[EanDigits[i], welke_tabel - 1];
            }
            return true;
        }

        protected override Boolean OnCreateBitString(String Code, out String BitString)
        {
            BitString = "";

            if (!ImportEan(Code, AddCheckDigit))
                return false;

            if (!CreateBitmapString())
                return false;

            BitString = Randteken + BitmapStrings[1] + Scheidingsteken + BitmapStrings[0] + Randteken;

            return true;
        }
    }

    class Barcode2Of5 : Barcode
    {
        static protected String[] Matrix = new String[10]{
				        "NNWWN", // 0
				        "WNNNW", // 1
				        "NWNNW", // 2
				        "WWNNN", // 3
				        "NNWNW", // 4
				        "WNWNN", // 5
				        "NWWNN", // 6
				        "NNNWW", // 7
				        "WNNWN", // 8
				        "NWNWN"	 // 9
				        };

        const String WHITENARROWBAR = "0";
        const String WHITEWIDEBAR = "000";
        const String BLACKNARROWBAR = "1";
        const String BLACKWIDEBAR = "111";

        const String StartCode = BLACKNARROWBAR + WHITENARROWBAR + BLACKNARROWBAR + WHITENARROWBAR;
        const String EndCode = BLACKWIDEBAR + WHITENARROWBAR + BLACKNARROWBAR;

        public Barcode2Of5()
        {
        }

        public override void DrawBarcode(Boolean WithNumbers, Graphics Graph, float Left, float Top, float Width, float Height)
        {
            Debug.Assert(false, "Please use the other DrawBarcode method");
        }

        public override void DrawBarcode(bool WithNumbers, Graphics Graph, float Left, float Top, float Width, float Height, int MaxCharacterCount)
        {
            base.DrawBarcode(WithNumbers, Graph, Left, Top, Width, Height, MaxCharacterCount);
        }

        protected override Boolean GetOverlappedDigits()
        {
            return false;
        }

        int CalcCheckDigit()
        {
            int nSumOdd = 0;
            int nSumEven = 0;
            for (int n = 0; n < Code.Length; n++)
            {
                int nValue = Code[n] - '0';
                if ((nValue & 1) > 0) // odd
                {
                    nSumOdd += nValue;
                }
                else
                {
                    nSumEven += nValue;
                }
            }
            int nEvenTimeThree = nSumEven * 3;
            int nTotalSum = nSumOdd + nEvenTimeThree;

            int nCheckDigit = nTotalSum % 10;
            if (nCheckDigit != 0)
                nCheckDigit = 10 - nCheckDigit;

            return nCheckDigit;
        }

        protected override void OnAddCheckDigit(ref String Code)
        {
            Code += CalcCheckDigit().ToString();
        }

        String Merge(int nDigit1, int nDigit2)
        {
            // first the black bars
            String Merge = Matrix[nDigit1];
            Merge = Merge.Replace("N", "N ");
            Merge = Merge.Replace("W", "W ");

            // now add the white bars
            String White = Matrix[nDigit2];
            White = White.ToLower();
            int nPosInWhiteString = 0;
            for (int n = 1; n < Merge.Length; n += 2)
            {
                Merge = Merge.Remove(n, 1);
                Merge = Merge.Insert(n, White[nPosInWhiteString++].ToString());
            }

            Merge = Merge.Replace("N", BLACKNARROWBAR);
            Merge = Merge.Replace("W", BLACKWIDEBAR);
            Merge = Merge.Replace("n", WHITENARROWBAR);
            Merge = Merge.Replace("w", WHITEWIDEBAR);

            return Merge;
        }

        protected override Boolean OnCreateBitString(String Code, out String BitString)
        {
            BitString = "";

            // make it even
            if ((Code.Length & 1) > 0) // odd
            {
                Code = '0' + Code;
            }

            // add beginning of bitstring
            BitString = StartCode;

            // add the real data of the bitstring
            for (int n = 0; n < Code.Length; n += 2)
            {
                int nDigit1 = Code[n] - '0';
                int nDigit2 = Code[n + 1] - '0';
                String Part = Merge(nDigit1, nDigit2);
                BitString += Part;
            }

            // add the end of the bitstring
            BitString += EndCode;

            return true;
        }
    }

    class Barcode39 : Barcode
    {
        const String WHITENARROWBAR = "0";
        const String WHITEWIDEBAR = "00";
        const String BLACKNARROWBAR = "1";
        const String BLACKWIDEBAR = "11";

        const String INTERCHARACTERGAP = "0";

        // The start and stop pattern is a * character
        // "*" = "nwnnwnwnn"                      n          w             n               n              w            n              w             n              n
        const String StartCode = BLACKNARROWBAR + WHITEWIDEBAR + BLACKNARROWBAR + WHITENARROWBAR + BLACKWIDEBAR + WHITENARROWBAR + BLACKWIDEBAR + WHITENARROWBAR + BLACKNARROWBAR;
        const String EndCode = BLACKNARROWBAR + WHITEWIDEBAR + BLACKNARROWBAR + WHITENARROWBAR + BLACKWIDEBAR + WHITENARROWBAR + BLACKWIDEBAR + WHITENARROWBAR + BLACKNARROWBAR;

        const String Characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ-. $/+%";

        static IDictionary<string, string> patterns = new Dictionary<string, string>();

        public Barcode39()
        {
            if (patterns.Count == 0)
            {
                //                     bsbsbsbsb      b=bar, s=space
                patterns.Add("0", "nnnwwnwnn");
                patterns.Add("1", "wnnwnnnnw");
                patterns.Add("2", "nnwwnnnnw");
                patterns.Add("3", "wnwwnnnnn");
                patterns.Add("4", "nnnwwnnnw");
                patterns.Add("5", "wnnwwnnnn");
                patterns.Add("6", "nnwwwnnnn");
                patterns.Add("7", "nnnwnnwnw");
                patterns.Add("8", "wnnwnnwnn");
                patterns.Add("9", "nnwwnnwnn");
                patterns.Add("A", "wnnnnwnnw");
                patterns.Add("B", "nnwnnwnnw");
                patterns.Add("C", "wnwnnwnnn");
                patterns.Add("D", "nnnnwwnnw");
                patterns.Add("E", "wnnnwwnnn");
                patterns.Add("F", "nnwnwwnnn");
                patterns.Add("G", "nnnnnwwnw");
                patterns.Add("H", "wnnnnwwnn");
                patterns.Add("I", "nnwnnwwnn");
                patterns.Add("J", "nnnnwwwnn");
                patterns.Add("K", "wnnnnnnww");
                patterns.Add("L", "nnwnnnnww");
                patterns.Add("M", "wnwnnnnwn");
                patterns.Add("N", "nnnnwnnww");
                patterns.Add("O", "wnnnwnnwn");
                patterns.Add("P", "nnwnwnnwn");
                patterns.Add("Q", "nnnnnnwww");
                patterns.Add("R", "wnnnnnwwn");
                patterns.Add("S", "nnwnnnwwn");
                patterns.Add("T", "nnnnwnwwn");
                patterns.Add("U", "wwnnnnnnw");
                patterns.Add("V", "nwwnnnnnw");
                patterns.Add("W", "wwwnnnnnn");
                patterns.Add("X", "nwnnwnnnw");
                patterns.Add("Y", "wwnnwnnnn");
                patterns.Add("Z", "nwwnwnnnn");
                patterns.Add("-", "nwnnnnwnw");
                patterns.Add(".", "wwnnnnwnn");
                patterns.Add(" ", "nwwnnnwnn");
                //		patterns.Add("*", "nwnnwnwnn"); // should only be used as start and stop character
                patterns.Add("$", "nwnwnwnnn");
                patterns.Add("/", "nwnwnnnwn");
                patterns.Add("+", "nwnnnwnwn");
                patterns.Add("%", "nnnwnwnwn");
            }
        }

        protected override Boolean GetOverlappedDigits()
        {
            return false;
        }

        public override void DrawBarcode(Boolean WithNumbers, Graphics Graph, float Left, float Top, float Width, float Height)
        {
            Debug.Assert(false, "Please use the other DrawBarcode method");
        }

        int GetCharacterIndex(char cCharacter)
        {
            int nCharCount = Characters.Length;
            for (int n = 0; n < nCharCount; n++)
            {
                if (Characters[n] == cCharacter)
                    return n;
            }

            return -1;
        }

        protected override void OnAddCheckDigit(ref String Code)
        {
            // Ensure uppercase (only uppercase characters are supported by Code39)
            Code = Code.ToUpper();

            int nSum = 0;

            for (int n = 0; n < Code.Length; n++)
                nSum += GetCharacterIndex(Code[n]);

            int nCheckDigit = nSum % 43;

            char cCheckChar = Characters[nCheckDigit];

            Code += cCheckChar;
        }

        protected override Boolean OnCreateBitString(String Code, out String BitString)
        {
            BitString = "";

            // Ensure uppercase (only uppercase characters are supported by Code39)
            Code = Code.ToUpper();

            BitString += StartCode;

            String Pattern;
            Boolean bOK = false;
            for (int n = 0; n < Code.Length; n++)
            {
                bOK = patterns.TryGetValue(Code[n].ToString(), out Pattern);
                if (!bOK)
                    return false;

                String Bits = "";
                Bits += (Pattern[0] == 'n') ? BLACKNARROWBAR : BLACKWIDEBAR;
                Bits += (Pattern[1] == 'n') ? WHITENARROWBAR : WHITEWIDEBAR;
                Bits += (Pattern[2] == 'n') ? BLACKNARROWBAR : BLACKWIDEBAR;
                Bits += (Pattern[3] == 'n') ? WHITENARROWBAR : WHITEWIDEBAR;
                Bits += (Pattern[4] == 'n') ? BLACKNARROWBAR : BLACKWIDEBAR;
                Bits += (Pattern[5] == 'n') ? WHITENARROWBAR : WHITEWIDEBAR;
                Bits += (Pattern[6] == 'n') ? BLACKNARROWBAR : BLACKWIDEBAR;
                Bits += (Pattern[7] == 'n') ? WHITENARROWBAR : WHITEWIDEBAR;
                Bits += (Pattern[8] == 'n') ? BLACKNARROWBAR : BLACKWIDEBAR;

                BitString += INTERCHARACTERGAP;
                BitString += Bits;
            }

            BitString += INTERCHARACTERGAP;
            BitString += EndCode;

            return true;
        }
    }

    class BarcodeEAN8 : Barcode
    {
        static protected String[,] Bitcodes = new String[10, 3]{
                                                        /*0.*/ {"0001101", "0100111", "1110010"},
                                                        /*1.*/ {"0011001", "0110011", "1100110"},
                                                        /*2.*/ {"0010011", "0011011", "1101100"},
                                                        /*3.*/ {"0111101", "0100001", "1000010"},
                                                        /*4.*/ {"0100011", "0011101", "1011100"},
                                                        /*5.*/ {"0110001", "0111001", "1001110"},
                                                        /*6.*/ {"0101111", "0000101", "1010000"},
                                                        /*7.*/ {"0111011", "0010001", "1000100"},
                                                        /*8.*/ {"0110111", "0001001", "1001000"},
                                                        /*9.*/ {"0001011", "0010111", "1110100"}
                                                    };

        static protected int[,] Lettertabel = new int[10, 6]{
	                                                    {1,1,1,1,1,1}, //0
	                                                    {1,1,2,1,2,2}, //1
	                                                    {1,1,2,2,1,2}, //2
	                                                    {1,1,2,2,2,1}, //3
	                                                    {1,2,1,1,2,2}, //4
	                                                    {1,2,2,1,1,2}, //5
	                                                    {1,2,2,2,1,1}, //6
	                                                    {1,2,1,2,1,2}, //7
	                                                    {1,2,1,2,2,1}, //8
	                                                    {1,2,2,1,2,1}
                                                    };

        protected static String Randteken = "232";
        protected static String Scheidingsteken = "32323";

        protected char[] EanDigits = new char[8];  // No including codedigit
        protected String[] BitmapStrings = new String[2];

        public BarcodeEAN8()
        {
        }

        public override void DrawBarcode(Boolean WithNumbers, Graphics Graph, float Left, float Top, float Width, float Height)
        {
            if (WithNumbers)
                DrawWithNumbers(Graph, Left, Top, Width, Height, 8);
            else
                DrawWithoutNumbers(Graph, Left, Top, Width, Height);
        }

        protected override Boolean GetOverlappedDigits()
        {
            return true;
        }

        int CalcCheckDigit()
        {
            int k;
            int faktor;
            int som;
            int cdigit;
            const int nNumEanDigits = 8;

            som = 0;
            faktor = 3;

            for (k = 1; k <= nNumEanDigits - 1; k++)
            {
                som += EanDigits[k] * faktor;
                faktor = 4 - faktor;                /* faktor alternerend 1 en 3    */
            }

            cdigit = som % 10;
            if (cdigit != 0)
            {
                cdigit = 10 - cdigit;
            }

            return (cdigit);
        }

        Boolean ImportEan(String EanString, Boolean Addcode)
        {
            int nTeller = EanString.Length;
            int nI, nJ, nOffset;
            int nChecksum;

            if (Addcode == true)
            {
                nI = 1;
            }
            else
            {
                nI = 0;
            }

            nI += EanString.Length;

            if (Addcode == true)
            {
                EanDigits[0] = '0';
                nOffset = 1;
            }
            else
            {
                nOffset = 0;
            }

            for (nI = nTeller - 1, nJ = nOffset; nI >= 0; nI--, nJ++)
            {
                char eanchar = EanString[nI];

                if (Char.IsDigit(eanchar))
                {
                    EanDigits[nJ] = Convert.ToChar(Convert.ToInt32(eanchar) - Convert.ToInt32('0'));
                }
                else
                {
                    EanDigits[nJ] = Convert.ToChar(0);
                    return false;
                }
            }

            nChecksum = CalcCheckDigit();
            if (Addcode == true)
            {
                this.Code += Convert.ToChar(nChecksum + '0');
                EanDigits[0] = Convert.ToChar(nChecksum);
                return true;
            }
            else
            {
                if (EanDigits[0] == nChecksum)
                {
                    return true;
                }
                else
                {
                    return true;
                }
            }
        }

        Boolean CreateBitmapString()
        {
            //	Aanmaken van bitmapstring rechts
            int i;

            BitmapStrings[0] = "";
            for (i = 3; i >= 0; i--)
            {
                BitmapStrings[0] += Bitcodes[EanDigits[i], 2];
            }

            // Aanmaken van de bitmapstring links
            BitmapStrings[1] = "";
            for (i = 7; i >= 4; i--)
            {
                BitmapStrings[1] += Bitcodes[EanDigits[i], 0];
            }
            return true;
        }

        protected override Boolean OnCreateBitString(String Code, out String BitString)
        {
            BitString = "";

            if (!ImportEan(Code, AddCheckDigit))
                return false;

            if (!CreateBitmapString())
                return false;

            BitString = Randteken + BitmapStrings[1] + Scheidingsteken + BitmapStrings[0] + Randteken;

            return true;
        }
    }

    class BarcodeUPCversionA : Barcode
    {
        protected static String StartCode = "232";
        protected static String MiddleCode = "32323";
        protected static String EndCode = "232";

        static protected String[] LEFTMATRIX = new String[10]{
				"0001101", // 0
				"0011001", // 1
				"0010011", // 2
				"0111101", // 3
				"0100011", // 4
				"0110001", // 5
				"0101111", // 6
				"0111011", // 7
				"0110111", // 8
				"0001011"  // 9
				};

        static protected String[] RIGHTMATRIX = new String[10]{
				"1110010", // 0
				"1100110", // 1
				"1101100", // 2
				"1000010", // 3
				"1011100", // 4
				"1001110", // 5
				"1010000", // 6
				"1000100", // 7
				"1001000", // 8
				"1110100"  // 9
				};

        public BarcodeUPCversionA()
        {
        }

        public override void DrawBarcode(Boolean WithNumbers, Graphics Graph, float Left, float Top, float Width, float Height)
        {
            if (WithNumbers)
                DrawWithNumbers(Graph, Left, Top, Width, Height, 12);
            else
                DrawWithoutNumbers(Graph, Left, Top, Width, Height);
        }

        protected override Boolean GetOverlappedDigits()
        {
            return true;
        }

        protected override void OnAddCheckDigit(ref String Code)
        {
            Boolean bOdd = true;
            int nSumOdd = 0;
            int nSumEven = 0;
            for (int n = Code.Length - 1; n >= 0; n--)
            {
                if (bOdd)
                    nSumOdd += Code[n] - '0';
                else
                    nSumEven += Code[n] - '0';

                bOdd = !bOdd;
            }

            int nCheckDigit = ((nSumOdd * 3) + nSumEven) % 10;
            if (nCheckDigit != 0)
                nCheckDigit = 10 - nCheckDigit;

            Code += (char)(nCheckDigit + '0');
        }

        protected override Boolean OnCreateBitString(String Code, out String BitString)
        {
            BitString = "";

            String Left = "";
            for (int n = 0; n < 6; n++)
            {
                int nValue = Code[n] - '0';
                Left += LEFTMATRIX[nValue];
            }
            for (int i = 0; i < 7; i++)
                if (Left[i] == '0')
                {
                    Left.Remove(i, 1);
                    Left.Insert(i, "3");

                }
                else
                {
                    Left.Remove(i, 1);
                    Left.Insert(i, "2");
                }

            String Right = "";
            for (int n = 6; n < Code.Length; n++)
            {
                int nValue = Code[n] - '0';
                Right += RIGHTMATRIX[nValue];
            }
            int nIndex = Right.Length - 1;
            for (int i = 0; i < 7; i++)
                if (Right[nIndex - i] == '0')
                {
                    Right.Remove(nIndex - i, 1);
                    Right.Insert(nIndex - i, "3");
                }
                else
                {
                    Right.Remove(nIndex - i, 1);
                    Right.Insert(nIndex - i, "2");
                }

            BitString += StartCode;
            BitString += Left;
            BitString += MiddleCode;
            BitString += Right;
            BitString += EndCode;

            return true;
        }
    }

    class BarcodeEAN128 : Barcode
    {
        private static char[] Code128ComboAB = new char[] {
                ' ', '!', '"', '#', '$', '%', '&', '\'', '(', ')', '*',
                '+', ',', '-', '.', '/', '0',  '1', '2', '3', '4', '5',
                '6', '7', '8', '9', ':', ';',  '<', '=', '>', '?', '@',
                'A', 'B', 'C', 'D', 'E', 'F',  'G', 'H', 'I', 'J', 'K',
                'L', 'M', 'N', 'O', 'P', 'Q',  'R', 'S', 'T', 'U', 'V',
                'W', 'X', 'Y', 'Z', '[', '\\', ']', '^', '_'
            };

        private static char[] Code128B = new char[] {
               '`', 'a', 'b',  'c', 'd', 'e', 'f',  'g', 'h', 'i', 'j',
                'k', 'l', 'm',  'n', 'o', 'p', 'q',  'r', 's', 't', 'u',
                'v', 'w', 'x',  'y', 'z', '{', '|',  '}', '~'
            };

        private static string[] Code128Encoding = new string[] {
                "11011001100", "11001101100", "11001100110", "10010011000", "10010001100", "10001001100", "10011001000",
                "10011000100", "10001100100", "11001001000", "11001000100", "11000100100", "10110011100", "10011011100",
                "10011001110", "10111001100", "10011101100", "10011100110", "11001110010", "11001011100", "11001001110",
                "11011100100", "11001110100", "11101101110", "11101001100", "11100101100", "11100100110", "11101100100",
                "11100110100", "11100110010", "11011011000", "11011000110", "11000110110", "10100011000", "10001011000",
                "10001000110", "10110001000", "10001101000", "10001100010", "11010001000", "11000101000", "11000100010",
                "10110111000", "10110001110", "10001101110", "10111011000", "10111000110", "10001110110", "11101110110",
                "11010001110", "11000101110", "11011101000", "11011100010", "11011101110", "11101011000", "11101000110",
                "11100010110", "11101101000", "11101100010", "11100011010", "11101111010", "11001000010", "11110001010",
                "10100110000", "10100001100", "10010110000", "10010000110", "10000101100", "10000100110", "10110010000",
                "10110000100", "10011010000", "10011000010", "10000110100", "10000110010", "11000010010", "11001010000",
                "11110111010", "11000010100", "10001111010", "10100111100", "10010111100", "10010011110", "10111100100",
                "10011110100", "10011110010", "11110100100", "11110010100", "11110010010", "11011011110", "11011110110",
                "11110110110", "10101111000", "10100011110", "10001011110", "10111101000", "10111100010", "11110101000",
                "11110100010", "10111011110", "10111101110", "11101011110", "11110101110", "11010000100", "11010010000",
                "11010011100"
            };

        private static string Code128Stop = "11000111010";
        private enum Code128ChangeModes { CodeA = 101, CodeB = 100, CodeC = 99 };
        private enum Code128StartModes { CodeUnset = 0, CodeA = 103, CodeB = 104, CodeC = 105 };
        private enum Code128Modes { CodeUnset = 0, CodeA = 1, CodeB = 2, CodeC = 3 };

        public override void DrawBarcode(bool WithNumbers, Graphics Graph, float Left, float Top, float Width, float Height)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        public override void DrawBarcode(bool WithNumbers, Graphics Graph, float Left, float Top, float Width, float Height, int MaxCharacterCount)
        {
            base.DrawBarcode(WithNumbers, Graph, Left, Top, Width, Height, MaxCharacterCount);
        }
        protected override void OnAddCheckDigit(ref string Code)
        {
            base.OnAddCheckDigit(ref Code);
        }
        protected override bool OnCreateBitString(string Code, out string BitString)
        {
            BitString = string.Empty;

            if (Code.Length == 0)
                return false;

            List<int> encoded = new List<int>();
            Code128Modes currentMode = Code128Modes.CodeUnset;
            for (int i = 0; i < Code.Length; i++)
            {
                if (IsNumber(Code[i]) && i + 1 < Code.Length && IsNumber(Code[i + 1]))
                {
                    if (currentMode != Code128Modes.CodeC)
                    {
                        if (currentMode == Code128Modes.CodeUnset)
                        {
                            encoded.Add((int)Code128StartModes.CodeC);
                        }
                        else
                        {
                            encoded.Add((int)Code128ChangeModes.CodeC);
                        }
                        currentMode = Code128Modes.CodeC;
                    }
                    encoded.Add(Int32.Parse(Code.Substring(i, 2)));
                    i++;
                }
                else
                {
                    if (currentMode != Code128Modes.CodeB)
                    {
                        if (currentMode == Code128Modes.CodeUnset)
                        {
                            encoded.Add((int)Code128StartModes.CodeB);
                        }
                        else
                        {
                            encoded.Add((int)Code128ChangeModes.CodeB);
                        }
                        currentMode = Code128Modes.CodeB;
                    }
                    //else
                    //{
                    //    if (currentMode == Code128Modes.CodeUnset)
                    //    {
                    //        encoded.Add((int)Code128StartModes.CodeA);
                    //    }
                    //    else
                    //    {
                    //        encoded.Add((int)Code128ChangeModes.CodeA);
                    //    }
                    //    currentMode = Code128Modes.CodeA;
                    //}
                    encoded.Add(EncodeCodeB(Code[i]));
                }
            }
            encoded.Add(CheckDigitCode128(encoded));

            StringBuilder barBits;
            barBits = new StringBuilder();
            for (int i = 0; i < encoded.Count; i++)
            {
                barBits.Append(Code128Encoding[encoded[i]]);
            }
            barBits.Append(Code128Stop);
            barBits.Append("11"); //end code

            BitString = barBits.ToString();

            return true;
        }

        private int CheckDigitCode128(List<int> codes)
        {
            int check = codes[0];
            for (int i = 1; i < codes.Count; i++)
            {
                check = check + (codes[i] * i);
            }
            return check % 103;
        }


        private bool IsNumber(char p)
        {
            return Char.IsNumber(p);
        }

        protected override bool GetOverlappedDigits()
        {
            return false;
        }

        private int EncodeCodeB(char value)
        {
            for (int i = 0; i < Code128ComboAB.Length; i++)
            {
                if (Code128ComboAB[i] == value)
                    return i;
            }
            for (int i = 0; i < Code128B.Length; i++)
            {
                if (Code128B[i] == value)
                    return i + Code128ComboAB.Length;
            }
            throw new Exception("Invalid Character");
        }

    }


    interface IBarcode2D
    {
        void SetCode(string barcode);
        Bitmap DrawBarcode(int resizeFactor, int boderSize, int pixelWidth, int pixelHeight);
    }
    public abstract class Barcode2d : IBarcode2D
    {
        public abstract void SetCode(string barcode);
        public abstract Bitmap DrawBarcode(int resizeFactor, int boderSize, int pixelWidth, int pixelHeight);
    }
    public class BarcodePDF417 : Barcode2d
    {
        string sBarcodeData;
        public override void SetCode(string barcode)
        {
            sBarcodeData = barcode;
        }
        public override Bitmap DrawBarcode(int resizeFactor, int boderSize, int pixelWidth, int pixelHeight)
        {
            Bitmap result = null;
            try
            {
                ZXing.BarcodeFormat bcFormat = ZXing.BarcodeFormat.PDF_417;
                ZXing.Common.EncodingOptions Options = new ZXing.PDF417.PDF417EncodingOptions
                    //new ZXing.Common.EncodingOptions
                {                   
                    Dimensions = new ZXing.PDF417.Internal.Dimensions(8,18,30,40),
                    ErrorCorrection = ZXing.PDF417.Internal.PDF417ErrorCorrectionLevel.L5,                    
                    Height = pixelHeight,
                    Width = pixelWidth,
                    PureBarcode = true,
                    Margin = 0
                    
                };
                ZXing.Rendering.BitmapRenderer renderer = new ZXing.Rendering.BitmapRenderer();
                //(ZXing.Rendering.IBarcodeRenderer<Bitmap>)Activator.CreateInstance(renderer));

                ZXing.BarcodeWriter writer = new ZXing.BarcodeWriter();
                writer.Format = bcFormat;
                writer.Options = Options;
                writer.Renderer = renderer;
                result = writer.Write(sBarcodeData);
                // width and height are overruled here
            }
            catch (Exception exc)
            {
                System.Diagnostics.Debug.WriteLine(exc.Message);
                throw (exc);
            }
            return result;
        }
    }
    public class BarcodeDataMatrix : Barcode2d
    {
        protected String[] BitmapStrings = new String[2];
        private byte[] _data;
        private string _pixArr;
        private int _w;
        private int _h;
        private EncodingType _globalEncoding = EncodingType.NotDef;
        private byte[] _encoding = null;
        private byte[,] _byteArray = null;

        public override void SetCode(string barcode)
        {
            _data = Encoding.ASCII.GetBytes(barcode);
            _w = 0;
            _h = 0;
            _globalEncoding = EncodingType.NotDef;
            int lenp = 0;
            int maxp = 0;
            int eccp = 0;

            if (_globalEncoding != EncodingType.NotDef)
            {
                _encoding = new byte[_data.Length + 1];
                byte e = EncodingTypeToByte(_globalEncoding);
                for (int i = 0; i < _encoding.Length; i++)
                    _encoding[i] = e;
                _encoding[_data.Length] = 0;
            }
            // Matrix creation
            IEC16022ecc200 iec16022 = new IEC16022ecc200();
            byte[] array = iec16022.iec16022ecc200(
                       ref _w,
                       ref _h,
                       ref _encoding,
                       _data.Length,
                       _data,
                       ref lenp,
                       ref maxp,
                       ref eccp);

            if (array == null)
                throw new Exception("Error building datamtrix: " + iec16022.ErrorMessage);

            _byteArray = new byte[_w, _h];
            for (int x = 0; x < _w; x++)
                for (int y = 0; y < _h; y++)
                {
                    _byteArray[x, y] = array[_w * y + x];
                    _pixArr = _pixArr + _byteArray[x, y];
                }
        }

        public override Bitmap DrawBarcode(int resizeFactor, int boderSize, int pixelWidth, int pixelHeight)
        {
            int W = _byteArray.GetLength(0);
            int H = _byteArray.GetLength(1);

            Bitmap bmp = new Bitmap(W, H, PixelFormat.Format24bppRgb);
            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, W, H), ImageLockMode.ReadWrite, bmp.PixelFormat);
            IntPtr ptr = bmpData.Scan0;
            int bytes = bmpData.Stride * bmp.Height;
            byte[] rgbValues = new byte[bytes];
            int tel = 0;

            for (int x = 0; x < W; x++)
            {
                for (int y = 0; y < H; y++)
                {
                    tel += 1;
                    int idx = ((H - y - 1) * bmpData.Stride) + x * 3;
                    if (_pixArr[tel - 1] == '0')
                    {
                        rgbValues[idx] = 255;
                        rgbValues[idx + 1] = 255;
                        rgbValues[idx + 2] = 255;
                    }
                    else
                    {
                        rgbValues[idx] = 0;
                        rgbValues[idx + 1] = 0;
                        rgbValues[idx + 2] = 0;
                    }
                }
            }
            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);
            bmp.UnlockBits(bmpData);

            int drawAreaW = resizeFactor * bmp.Width;
            int drawAreaH = resizeFactor * bmp.Height;
            Bitmap outBmp = new Bitmap(drawAreaW + 2 * boderSize, drawAreaH + 2 * boderSize);
            Graphics g = Graphics.FromImage(outBmp);

            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            g.FillRectangle(Brushes.White, 0, 0, outBmp.Width, outBmp.Height);
            g.DrawImage(bmp, new Rectangle(boderSize, boderSize, drawAreaW, drawAreaH), 0, 0, bmp.Width, bmp.Height, GraphicsUnit.Pixel);
            g.Dispose();
            return outBmp;
        }

        public enum EncodingType
        {
            NotDef,
            Ascii,
            C40,
            Text,
            X12,
            Edifact,
            Binary
        }

        private byte EncodingTypeToByte(EncodingType et)
        {
            switch (et)
            {
                case EncodingType.Ascii: return (byte)'A';
                case EncodingType.Binary: return (byte)'B';
                case EncodingType.C40: return (byte)'C';
                case EncodingType.Edifact: return (byte)'E';
                case EncodingType.Text: return (byte)'T';
                case EncodingType.X12: return (byte)'X';
                case EncodingType.NotDef: throw new ApplicationException("EncodingType not valid");
                default: throw new ApplicationException("Unknown EncodingType");
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
