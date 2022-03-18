using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using RnAds.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RnAds.Bll.Service
{
    public class ExcelService : IExcelService
    {
        public IWorkbook workBook = null;
        /// <summary>
        /// словарь собранных данных из ячеек файла excel
        /// </summary>
        private List<Dictionary<int, string>> _rawData { get; set; }
        /// <summary>
        /// получение названий регионов или городов из эксель документа в templates
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public List<string> GetNamesRegionsOrCities(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            workBook = new XSSFWorkbook(fs);
            var cols = new List<int>();
            //номера столбцов
            for (int i = 0; i < 2; i++)
                cols.Add(i);

            _rawData = GetDataFromSheet(0, 0, cols);

            var list = MappingPirObject();

            return list;
        }
        /// <summary>
        /// Считывание данных с листа
        /// </summary>
        /// <param name="indexSheet"></param>
        /// <param name="indexRowStart"></param>
        /// <param name="indexColumns"></param>
        /// <param name="indexRowEnd"></param>
        /// <returns></returns>
        public List<Dictionary<int, string>> GetDataFromSheet(int indexSheet, int indexRowStart, List<int> indexColumns, int indexRowEnd = 0)
        {
            var listData = new List<Dictionary<int, string>>();

            if (indexSheet == -1) return listData;

            var sheet = workBook.GetSheetAt(indexSheet);
            var rows = sheet.GetRowEnumerator();

            rows.Reset(); // необязательно, но для страховки оставил

            while (rows.MoveNext())
            {
                try
                {
                    var row = (IRow)rows.Current;

                    if (row != null && row.RowNum >= indexRowStart)
                    {
                        var dicRow = new Dictionary<int, string>();

                        foreach (int indexCol in indexColumns)
                        {
                            var cell = row.GetCell(indexCol);

                            var cellValue = "";

                            if (cell != null)
                                cellValue = cell.StringCellValue;

                            dicRow.Add(indexCol, cellValue);
                        } // foreach

                        listData.Add(dicRow);

                    } //end if

                    //если указан индекс последний строки, проверяем и выходим из цикла
                    if (row != null && indexRowEnd > 0 && row.RowNum == indexRowEnd)
                    {
                        break;
                    }

                } //try
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            } //while

            return listData;
        }
        public List<string> MappingPirObject()
        {
            List<string> list = new List<string>();


            foreach (var rawItem in _rawData)
            {
                string city = rawItem[0];
                string region = rawItem[1];
                if (city != null && city != string.Empty)
                    list.Add(city);
                if (region != null && region != string.Empty)
                    list.Add(region);

            }
            list = list.Distinct().ToList();
            return list;

        }


    }
}
