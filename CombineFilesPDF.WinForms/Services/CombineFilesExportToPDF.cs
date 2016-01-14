using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CombineFilesPDF.WinForms.Services
{
    public class CombineFilesExportToPDF
    {
        public void Combine(string saveFileName, IEnumerable<string> files)
        {
            byte[] mergedPdf = null;
            using (MemoryStream ms = new MemoryStream())
            {
                using (Document document = new Document())
                {
                    using (PdfCopy copy = new PdfCopy(document, ms))
                    {
                        document.Open();

                        foreach (var item in files.OrderBy(x=> x))
                        {
                            PdfReader reader = new PdfReader(item);

                            // loop over the pages in that document
                            int n = reader.NumberOfPages;
                            for (int page = 0; page < n; )
                            {
                                copy.AddPage(copy.GetImportedPage(reader, ++page));
                            }

                            reader.Close();
                        }                        
                    }
                }
                mergedPdf = ms.ToArray();
            }

            File.WriteAllBytes(String.Concat(saveFileName, ".pdf"), mergedPdf);
        }
    }
}
