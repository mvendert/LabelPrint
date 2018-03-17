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
using System.IO.Compression;
using System.Xml;
using System.Xml.Schema;
using System.Security.Cryptography;

namespace ACA.PSLib
{
    public class CRC
    {
        // Create an md5 sum string of this string
        static public string GetMd5Sum(string Text)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(Text);
            return GetMd5Sum(buffer);
        }

        // Create an md5 sum string of this buffer
        static public string GetMd5Sum(byte[] buffer)
        {
            // Now that we have a byte array we can ask the CSP to hash it
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(buffer);

            // Build the final string by converting each byte
            // into hex and appending it to a StringBuilder
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
                sb.Append(result[i].ToString("X2"));

            // And return it
            md5.Clear();
            md5.Dispose();
            return sb.ToString();
        }

        static public long GetCRC(byte[] buffer)
        {
            long crc;
            long q;
            byte c;
            crc = 0;
            for (int i = 0; i < buffer.Length; i++)
            {
                c = buffer[i];
                q = (crc ^ c) & 0x0f;
                crc = (crc >> 4) ^ (q * 0x1081);
                q = (crc ^ (c >> 4)) & 0xf;
                crc = (crc >> 4) ^ (q * 0x1081);
            }
            return crc;
        }
    }

    public class FilesAndFolders
    {
        static public string GetFileHash(string FilePath)
        {
            try
            {
                FileStream infile;
                // Open the file as a FileStream object.
                infile = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                byte[] buffer = new byte[infile.Length];
                // Read the file to ensure it is readable.
                int count = infile.Read(buffer, 0, buffer.Length);
                if (count != buffer.Length)
                {
                    infile.Close();
                    throw new PSLibException(string.Format("Unable to read data from file: {0}", FilePath));
                }
                infile.Close();

                return CRC.GetMd5Sum(buffer);
            }
            catch (System.OutOfMemoryException e)
            {
                throw new Exception(e.Message, e);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }            
        }
    }

    public class PSLibException : ApplicationException
    {
      public PSLibException()
      {
      }
      public PSLibException(string message)
      : base(message)
      {
      }
      public PSLibException(string message, Exception inner)
      : base(message, inner)
      {
      }
    }

    public class Compression
    {
        /// <summary>
        /// Compresses the provided data, using GZip
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public static byte[] Compress(byte[] Data)
        {
            MemoryStream ms = new MemoryStream();
            // Use the newly created memory stream for the compressed data.
            GZipStream compressedzipStream = new GZipStream(ms, CompressionMode.Compress, true);
            compressedzipStream.Write(Data, 0, Data.Length);
            // Close the stream.
            compressedzipStream.Close();
            return ms.GetBuffer();
        }

        /// <summary>
        /// Deompresses the provided data, using GZip
        /// </summary>
        /// <param name="CompressedData"></param>
        /// <param name="UncompressedDataLength"></param>
        /// <returns></returns>
        public static byte[] Decompress(byte[] CompressedData, int UncompressedDataLength)
        {
            MemoryStream ms = new MemoryStream(CompressedData);
            GZipStream zipStream = new GZipStream(ms, CompressionMode.Decompress);
            byte[] decompressedBuffer = new byte[UncompressedDataLength];
            zipStream.Read(decompressedBuffer, 0, decompressedBuffer.Length);
            return decompressedBuffer;
        }
    }

    public class Xml
    {
        /// <summary>
        /// This function puts an XML file into a dictionary structure. So you can access all values directly.
        /// The key of a value is constructed as such: 
        /// /root-element/child-element/child-element.0
        /// /root-element/child-element/child-element.1
        /// /root-element/child-element/child-element.2
        /// /root-element/child-element/child-element.3
        /// 
        /// and for attributes:
        /// /root-element/child-element/child-element.0@attribute
        /// /root-element/child-element/child-element.1@attribute
        /// /root-element/child-element/child-element.2@attribute
        /// /root-element/child-element/child-element.3@attribute
        /// </summary>
        /// <param name="Source"></param>
        /// <param name="dictionary"></param>
        public static void XMLToDictionary(Stream Source, ref IDictionary<string, string> dictionary)
        {
            XmlTextReader rdr = new XmlTextReader(Source);
            string Path = "";
            while (rdr.Read())
            {
                if (XmlNodeType.Element == rdr.NodeType)
                {
                    // Synchronize Path
                    int Pos = 0;
                    for (int Depth = 0; Depth <= rdr.Depth && Pos > -1; Depth++)
                    {
                        Pos = Path.IndexOf('/', Pos);
                        if (Pos > -1)
                            Pos++;
                    }
                    if (Pos > -1)
                    {
                        Path = Path.Substring(0, Pos - 1);
                    }
                    Path = Path + "/" + rdr.Name;

                    // Fetch Attributes
                    while (rdr.MoveToNextAttribute())
                    {
                        int Index = 0;
                        string Key = string.Format("{0}.{1:D}@{2}", Path, Index, rdr.Name);

                        while (dictionary.ContainsKey(Key))
                        {
                            Index++;
                            Key = string.Format("{0}.{1:D}@{2}", Path, Index, rdr.Name);
                        }
                        dictionary.Add(Key, rdr.Value);
                    }
                }
                else if (rdr.HasValue && XmlNodeType.Text == rdr.NodeType)
                {
                    int Index = 0;
                    string Key = string.Format("{0}.{1:D}", Path, Index);
                    while (dictionary.ContainsKey(Key))
                    {
                        Index++;
                        Key = string.Format("{0}.{1:D}", Path, Index);
                    }
                    dictionary.Add(Key, rdr.Value);
                }
            }

            rdr.Close();
        }

        /// <summary>
        /// This function puts an XML file into a dictionary structure. So you can access all values directly.
        /// The key of a value is constructed as such: 
        /// /root-element/child-element/child-element
        /// 
        /// and for attributes:
        /// /root-element/child-element/child-element@attribute
        /// </summary>
        /// <param name="XMLFilePath"></param>
        /// <param name="dictionary"></param>
        public static void XMLToDictionary(string XMLFilePath, ref IDictionary<string, string> dictionary)
        {
            Stream source = new FileStream(XMLFilePath, FileMode.Open, FileAccess.Read);
            XMLToDictionary(source, ref dictionary);
            source.Close();
        }

        public static void XMLToDictionary(byte[] XMLSourceBuffer, ref IDictionary<string, string> dictionary)
        {
            MemoryStream source = new MemoryStream();
            source.Write(XMLSourceBuffer, 0, XMLSourceBuffer.Length);
            XMLToDictionary(source, ref dictionary);
            source.Close();
        }

        public static void XMLStringToDictionary(string XMLSourceString, ref IDictionary<string, string> dictionary)
        {
            XMLToDictionary(Encoding.ASCII.GetBytes(XMLSourceString), ref dictionary);
        }

        public static bool ValidateXML(string XMLFilePath, Stream SchemaFromResource)
        {
            bool succes = false;
            try
            {
                FileStream xml = new FileStream(XMLFilePath, FileMode.Open, FileAccess.Read);
                try
                {
                    ValidateXML(xml, SchemaFromResource);
                    succes = true;
                }
                catch (ACA.PSLib.PSLibException e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                    succes = false;
                }
                catch (System.Xml.XmlException e2)
                {
                    System.Diagnostics.Debug.WriteLine(e2.Message);
                    succes = false;
                }
                catch (Exception e3)
                {
                    System.Diagnostics.Debug.WriteLine(e3.Message);
                    succes = false;
                }

                xml.Close();
                return succes;
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine("File open failed for file " + XMLFilePath);
                return succes;
            }
        }



        public static bool ValidateXML(string XMLFilePath, string SchemaUri, ref string ErrorMessage)
        {
            FileStream xml = new FileStream(XMLFilePath, FileMode.Open);
            FileStream xsd = new FileStream(SchemaUri, FileMode.Open);

            try
            {
                ValidateXML(xml, xsd);
            }
            catch (PSLibException e)
            {
                ErrorMessage = e.Message;
                return false;
            }
            finally
            {
                xsd.Close();
                xml.Close();
            }
            return true;
        }

        public static void ValidateXML(Stream XMLStream, Stream SchemaStream)
        {
            XmlReader reader = null;
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;
            settings.ValidationFlags = XmlSchemaValidationFlags.None; //ReportValidationWarnings;
            settings.ValidationType = ValidationType.Schema;
            settings.Schemas.Add(XmlSchema.Read(SchemaStream, null));
            settings.Schemas.Compile();

            // wire-up anonymous callback delegate
            int badDepth = -1;
            bool nextNodeInvalid = false;
            settings.ValidationEventHandler += delegate(object sender, ValidationEventArgs ea)
            {
                string message;
                message = string.Format("Event -- {0}: {1}", ea.Severity, ea.Message);
                message += string.Format("Event -- {0}\t{1}\t{2}\t{3}\t{4}", reader.NodeType, reader.Name, reader.Value, reader.SchemaInfo == null ? "<none>" : reader.SchemaInfo.Validity.ToString(), reader.Depth);

                if (reader.NodeType == XmlNodeType.Element &&
                    reader.SchemaInfo.Validity == XmlSchemaValidity.NotKnown &&
                    nextNodeInvalid == false)
                {
                    nextNodeInvalid = true;
                    badDepth = reader.Depth;
                }
                throw new PSLibException(message);
            };

            reader = XmlReader.Create(XMLStream, settings);
            while (reader.Read())
            {
                if (nextNodeInvalid)
                {
                    int targetDepth = badDepth - 1;
                    while (reader.Depth > targetDepth)
                    {
                        reader.Read();
                    }
                    nextNodeInvalid = false;
                    badDepth = -1;
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
