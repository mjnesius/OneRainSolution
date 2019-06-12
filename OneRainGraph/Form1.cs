using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Spire.Pdf;
using Spire.Pdf.Graphics;
using Spire.Pdf.Tables;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace OneRainGraph
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //InitTable( new DataTable("tblDepth"));
            //InitTable(new DataTable("tblIntensity"));
            
            fillChartFDOT();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            fillChartFDOT();
            //rbGetOneRain, rbPublishToDashboard
        }

        //fillChartFDOT parses the csv containing the fdot curve info. based on the app.config   
        private void fillChartFDOT()
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["fdotCSV"].ConnectionString;
            OleDbConnection myConnection = new OleDbConnection(connStr);
            string fdotCSV = System.Configuration.ConfigurationManager.AppSettings["fdotCSVFilename"];
            string mySelectQuery = "Select * from " + fdotCSV;
            OleDbCommand myCommand = new OleDbCommand(mySelectQuery, myConnection);
            myCommand.Connection.Open();
            OleDbDataReader myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            chart1.Series["FDOT 2-Year (50%)"].Points.DataBindXY(myReader, "0", myReader, "1");
            chart1.Series["FDOT 2-Year (50%)"].BorderWidth = 2;

            myCommand.Connection.Open();
            myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            chart1.Series["FDOT 5-Year (20%)"].Points.DataBindXY(myReader, "0", myReader, "2");

            myCommand.Connection.Open();
            myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            chart1.Series["FDOT 10-Year (10%)"].Points.DataBindXY(myReader, "0", myReader, "3");

            myCommand.Connection.Open();
            myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            chart1.Series["FDOT 25-Year (4%)"].Points.DataBindXY(myReader, "0", myReader, "4");

            myCommand.Connection.Open();
            myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            chart1.Series["FDOT 50-Year (2%)"].Points.DataBindXY(myReader, "0", myReader, "5");

            myCommand.Connection.Open();
            myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            chart1.Series["FDOT 100-Year (1%)"].Points.DataBindXY(myReader, "0", myReader, "6");

            myReader.Close();
            myConnection.Close();

            chart1.ChartAreas[0].AxisY.Interval = 2;
            chart1.ChartAreas[0].AxisX.Maximum = 240;
            chart1.ChartAreas[0].AxisX.Minimum = 0.1;
            chart1.ChartAreas[0].AxisX.IsLogarithmic = true;
            chart1.ChartAreas[0].AxisX.MinorGrid.Interval = 1;
            chart1.ChartAreas[0].AxisX.MinorGrid.Enabled = true;
            chart1.ChartAreas[0].AxisX.LabelStyle.Interval = 0.1;

            //LOGARITHMIC scale labels
            chart1.ChartAreas[0].AxisX.CustomLabels.Add(0.0, 0.001, "1");
            chart1.ChartAreas[0].AxisX.CustomLabels.Add(0.5, 1.51, "10");
            chart1.ChartAreas[0].AxisX.CustomLabels.Add(1.5, 2.51, "100");
            Title title = chart1.Titles.Add("Rainfall Depth-Duration-Frequency Curves");
            title.Font = new System.Drawing.Font("Arial", 16, FontStyle.Bold);
            chart1.ChartAreas[0].AxisY2.Enabled = AxisEnabled.True;
            chart1.ChartAreas[0].AxisY2.LabelStyle.Enabled = false;
            chart1.ChartAreas[0].AxisY2.MajorTickMark.Enabled = false;
            chart1.ChartAreas[0].AxisY2.LineWidth = 1;
            chart1.AntiAliasing = AntiAliasingStyles.All;
        }

        private void startDatePicker_ValueChanged(object sender, EventArgs e)
        {

        }

        private void rbGetOneRain_Click(object sender, EventArgs e)
        {
            //startDatePicker, endDatePicker
            string sTime = startDatePicker.Value.ToString("yyyy-MM-dd hh:mm:ss tt");
            string endTime = endDatePicker.Value.ToString("yyyy-MM-dd hh:mm:ss tt");
            Debug.WriteLine("start date: {0} end date: {1}", (string)sTime, (string)endTime);

            int result = DateTime.Compare(startDatePicker.Value, endDatePicker.Value);
            System.TimeSpan diff1 = endDatePicker.Value.Subtract(startDatePicker.Value);
            TimeSpan maxWindow = TimeSpan.FromDays(15);
            if (result < 0 & DateTime.Compare(endDatePicker.Value, DateTime.Now) < 1 & TimeSpan.Compare(diff1, maxWindow) < 1)
            {
                Debug.WriteLine("dates are valid");
                getOneRain(sTime, endTime);
                chart1.Titles[0].Text = string.Format("Rainfall Depth-Duration-Frequency Curves\n{0} - {1}", startDatePicker.Value.ToShortDateString(), endDatePicker.Value.ToShortDateString());
            }
            else
            {
                // Initializes the variables to pass to the MessageBox.Show method.
                string message = string.Format("You did not enter valid dates\n\nEnter a date range that is less than 15 days long, _" +
                    " has end-date & time that has already occurred, and the start-time is before the end-time ");
                string caption = "Error Detected in Input";
                DialogResult dResult;

                // Displays the MessageBox.
                dResult = MessageBox.Show(message, caption);
            }

        }

        //download OneRain data csv files to user's temp dir  
        private async void getOneRain(string _startDate, string _endDate) //IProgress<int> progress
        {
            try
            {
                string dirTemp = Path.GetTempPath();
                //OneRainURL
                // example request URL for City Hall
                //Request URL:https://tallahassee.onerain.com/export/file/rollup/?site_id=4&site=b42bb2fe-9fb8-47a4-967e-9e31d8e856b4&device_id=1&device=ea310921-a66d-4401-88fe-5fd60a6efb2c&mode=&hours=&data_start=2019-05-30 08:44:12&data_end=2019-06-06 08:44:12&tz=US/Eastern&format_datetime=%m/%d/%Y+%H:%i:%S&bin=300&mime=txt&delimiter=comma
                string baseURLOneRain = System.Configuration.ConfigurationManager.AppSettings["OneRainURL"];

                //parse dictionary of OneRain sites & sensors from App.config
                // the Values are csv: site id,site,device id,device

                var section = (Hashtable)ConfigurationManager.GetSection("SitesAndSensors");
                //Dictionary<string, List<string>> dictSensors = section.Cast<DictionaryEntry>().ToDictionary(d => (string)d.Key, d => ((string)d.Value).Split(',').ToList());
                Dictionary<string, List<string>> _dictSensors = section.Cast<DictionaryEntry>().ToDictionary(d => (string)d.Key, d => ((string)d.Value).Split(',').ToList());
                SortedDictionary<string, List<string>> dictSensors = new SortedDictionary<string, List<string>>(_dictSensors);
                int j = 0;
                int totalSensors = dictSensors.Count;
                progressBar1.Value = 0;
                progressBar1.Show();
                lblProgressBar.Text = string.Format("Downloading datasets: {0} of {1}", j, totalSensors);
                lblProgressBar.Show();
                progressBar1.Maximum = totalSensors;
                progressBar1.Step = 1;

                foreach (KeyValuePair<string, List<string>> entry in dictSensors)
                {
                    string queryTemplate = "site_id={0}&site={1}&device_id={2}&device={3}&data_start={4}&data_end={5}&tz=US/Eastern&format_datetime=%m/%d/%Y+%H:%i:%S&bin=300&mime=txt&delimiter=comma";
                    string query = string.Format(queryTemplate, entry.Value[0], entry.Value[1], entry.Value[2], entry.Value[3], _startDate, _endDate);

                    string urlRequest = baseURLOneRain + query;
                    Debug.WriteLine("{0} \n\t {1}", entry.Key, urlRequest);
                    string outCSV = Path.GetTempPath() + "\\" + entry.Key + ".csv";

                    if (File.Exists(outCSV))
                    {
                        File.Delete(outCSV);
                    }

                    byte[] urlContents = await GetURLContentsAsync(urlRequest);
                    //FileStream fileStream = File.Create(outCSV);
                    using (BinaryWriter bWriter = new BinaryWriter(File.OpenWrite(outCSV)))
                    {
                        bWriter.Write(urlContents);
                        bWriter.Flush();
                        bWriter.Close();
                    }
                    Debug.WriteLine("downloaded {0}", outCSV);
                    List<double> arrRawRainData = extractRainData(outCSV, Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["OneRainDataColumn"]));

                    plotOneRain(arrRawRainData, entry.Key);
                    progressBar1.PerformStep();
                    j++;
                    lblProgressBar.Text = string.Format("Downloading dataset {0} of {1}", j, totalSensors);
                }
                progressBar1.Hide();
                lblProgressBar.Hide();
            }
            catch (NullReferenceException ex)
            {
                Debug.WriteLine("Null Reference Exception\n\t {0}", ex);
                return;
            }
            catch(Exception e){
                Debug.WriteLine("Exception\n\t {0}", e);
                return;
            }
            finally
            {
                progressBar1.Hide();
                lblProgressBar.Hide();
            }
        }

        private async Task<byte[]> GetURLContentsAsync(string url)
        {
            // The downloaded resource ends up in the variable named content.
            var content = new MemoryStream();

            // Initialize an HttpWebRequest for the current URL.
            var webReq = (HttpWebRequest)WebRequest.Create(url);
            webReq.Method = WebRequestMethods.Http.Get;
            webReq.CookieContainer = new CookieContainer();

            // Send the request and wait for the response.
            using (WebResponse response = await webReq.GetResponseAsync())
            {
                // Get the data stream that is associated with the specified url.
                using (Stream responseStream = response.GetResponseStream())
                {
                    // Read the bytes in responseStream and copy them to content stream.
                    await responseStream.CopyToAsync(content);
                }
            }
            // Return the result as a byte array.
            return content.ToArray();
        }


        //parse OneRain CSV into an array of doubles
        private List<double> extractRainData(string _CSVpath, int colNum) 
        {
            List<double> rawRainData = new List<double>();

            try
            {
                
                using (TextFieldParser csvParser = new TextFieldParser(_CSVpath))
                {
                    csvParser.CommentTokens = new string[] { "#" };
                    csvParser.SetDelimiters(new string[] { "," });
                    csvParser.HasFieldsEnclosedInQuotes = true;

                    // Skip the row with the column names
                    csvParser.ReadLine();

                    while (!csvParser.EndOfData)
                    {
                        // Read current line fields, pointer moves to the next line.

                        string[] fields = csvParser.ReadFields();

                        if (fields.Count() < 2)
                        {
                            string message = string.Format("Error Message: {0}", fields[0]);
                            throw new NoDataException(message);
                        }
                        Debug.WriteLine(fields.ToString());

                        rawRainData.Add(Convert.ToDouble(fields[colNum]));
                    }
                }
                return rawRainData;
            }
            catch (Exception e)
            {
                Console.WriteLine($"extractRainData error: '{e}'");
                return null;
            }
            
        }

        // driver for accumulation calcs, adds results to the chart
        private void plotOneRain(List<double> _arrRawData, string _plotSeriesName)
        {
            //FiveMinDurationIntervals
            string setting = System.Configuration.ConfigurationManager.AppSettings["FiveMinDurationIntervals"].ToString();
            List<short> cumulationIntervals = setting.Split(',').Select(short.Parse).ToList();

            if (chart1.Series.IndexOf(_plotSeriesName) < 0)
            {
                chart1.Series.Add(_plotSeriesName);
                chart1.Series[_plotSeriesName].ChartArea = "DDF Curve";
                chart1.Series[_plotSeriesName].ChartType = SeriesChartType.Spline;
                chart1.Series[_plotSeriesName].XValueType = ChartValueType.Double;
                chart1.Series[_plotSeriesName].YValueType = ChartValueType.Double;
                chart1.Series[_plotSeriesName].Legend = "Legend1";
                chart1.Series[_plotSeriesName].YValuesPerPoint = 1;
            }
            
            chart1.Series[_plotSeriesName].Points.Clear();
            
            List<double> arrTemp = new List<double>();
            arrTemp.Add(-1.0);
            foreach (short _interval in cumulationIntervals)
            {
                double y = calcAccumulation(_arrRawData, _interval);

                double x = _interval * 5.0 / 60.0;
                if(y > -1)
                {
                    arrTemp.Add(y);
                    Debug.WriteLine(string.Format("\tadding x: {0}\t y: {1}", x, y));
                    chart1.Series[_plotSeriesName].Points.AddXY(x, y);
                    chart1.Series[_plotSeriesName].BorderWidth = 2;
                    if (y > chart1.ChartAreas[0].AxisY.Maximum)
                    {
                        chart1.ChartAreas[0].AxisY.Maximum = y + 1;
                    }
                }
                else
                {
                    Debug.WriteLine(string.Format("\tnot enough data to calculate x: {0}", x));
                    double max = arrTemp.Max();
                    if(max > -1)
                    {
                        chart1.Series[_plotSeriesName].Points.AddXY(x, max);
                    }
                }
                
            }
            chart1.Series[_plotSeriesName].LegendText = string.Format("{0} - {1}\"", _plotSeriesName.Replace("and","&"), _arrRawData.Sum().ToString("0.##"));
        }

        private double calcAccumulation(List<double> _arrRawData, short _fiveMinInterval)
        {
            List<double> arrTemp = new List<double>();
            arrTemp.Add(-1.0); //default value
            short window = _fiveMinInterval;
            if (window == 1)
            {
                return _arrRawData.Max();
            }
            else
            {
                short ctr = (short)(window - 1);
                while (ctr < (_arrRawData.Count - 1))
                {
                    short startIdx = (short)(ctr - (window - 1));
                    //Debug.WriteLine(string.Format("subList {0} - {1}", startIdx.ToString(), ctr.ToString()));
                    List<double> subList = _arrRawData.GetRange(startIdx, window);
                    arrTemp.Add(subList.Sum());
                    ctr += (short)1;
                }
                return arrTemp.Max();
            } 
        }

        private void rbSavePlot_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "PDF(*.pdf)|*.pdf";
                saveFileDialog1.Title = "Select PDF  output file. A PNG of the graph will be saved with the same name";
                saveFileDialog1.ShowDialog();

                // If the file name is not an empty string open it for saving.  
                if (saveFileDialog1.FileName != "")
                {
                    //saveFileDialog1.FileName = saveFileDialog1.FileName + ".png";
                    string outPath = Path.GetDirectoryName(saveFileDialog1.FileName);
                    //string filename_with_ext = Path.GetFileName(saveFileDialog1.FileName);
                    string filename_without_ext = Path.GetFileNameWithoutExtension(saveFileDialog1.FileName);
                    string outImage = outPath + "\\" + filename_without_ext + ".png";
                    
                    using (FileStream fs = new FileStream(outImage, FileMode.Create))
                    {
                        switch (saveFileDialog1.FilterIndex)
                        {
                            case 1:
                                this.chart1.SaveImage(fs, ChartImageFormat.Png);
                                
                                break;
                        }
                    }
                    //Image img = Image.FromFile(outImage);//941, 457
                    Bitmap bitmap = new Bitmap(chart1.ClientSize.Width, chart1.ClientSize.Height);//new Bitmap((int)(941 * 0.9f), (int)(457 * 0.9f), PixelFormat.Format32bppArgb);
                    System.Drawing.Graphics g = Graphics.FromImage(bitmap);
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    Rectangle rect = new Rectangle(0, (int)(457 * 0.9f), (int)(941 * 0.9f), (int)(457 * 0.9f));
                    //this.chart1.Printing.PrintPaint(g, rect);
                    //Rectangle rect = new Rectangle(0, bitmap.Height, bitmap.Width, bitmap.Height);
                    //g.DrawImage(bitmap, rect, 0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel);
                    chart1.DrawToBitmap(bitmap, chart1.ClientRectangle);
                    bitmap.Save(outImage, System.Drawing.Imaging.ImageFormat.Png);

                    PdfDocument doc = new PdfDocument();

                    PdfImage image = PdfImage.FromFile(outImage);
                    PdfUnitConvertor unitCvtr = new PdfUnitConvertor();
                    PdfMargins margin = new PdfMargins();
                    margin.Top = unitCvtr.ConvertUnits(2.54f, PdfGraphicsUnit.Centimeter, PdfGraphicsUnit.Point);
                    margin.Bottom = margin.Top;
                    margin.Left = unitCvtr.ConvertUnits(3.17f, PdfGraphicsUnit.Centimeter, PdfGraphicsUnit.Point);
                    margin.Right = margin.Left;
                    PdfPageBase page = doc.Pages.Add(PdfPageSize.A4, margin);
                    float width = image.Width * 0.5f;
                    float height = image.Height * 0.5f;
                    float x = (page.Canvas.ClientSize.Width - width) / 2;

                    PdfPageBase page0 = doc.Pages[0];
                    PointF position = new PointF((page0.Canvas.ClientSize.Width - image.Width) / 2, 210);

                    page0.Canvas.DrawImage(image, x, 60, width, height);
                    page0.Canvas.SetTransparency(1.0f);
                    try
                    {
                        //build depth table
                        PdfPageBase page1 = doc.Pages.Add(PdfPageSize.A4);
                        buildTable("Depth Duration Table", page1);

                        // build intensity table
                        PdfPageBase page2 = doc.Pages.Add(PdfPageSize.A4);
                        buildTable("Max Intensity / Duration Table", page2);
                    }
                    catch (ArgumentException aEx)
                    {
                        DialogResult dResult;
                        string caption = " buildTable error";
                        dResult = MessageBox.Show(string.Format("Load OneRain data before saving so that the tables have data\n\nError message: {0}\n\nInner Exception: {1}\n\nStack Trace: {2}",
                            aEx.Message, aEx.InnerException, aEx.StackTrace), caption);
                        doc.Pages.RemoveAt(1);
                    }
                    
                    doc.SaveToFile(saveFileDialog1.FileName);
                    System.Diagnostics.Process.Start(saveFileDialog1.FileName);
                    doc.Close();
                }
            }
            catch(IOException ioEx)
            {
                DialogResult dResult;
                string caption = "IOException will trying to save your files";
                dResult = MessageBox.Show(string.Format("Make sure you don't already have the file open\n\nError message: {0}\n\nInner Exception: {1}\n\nStack Trace: {2}",
                    ioEx.Message, ioEx.InnerException, ioEx.StackTrace),caption);
            }
            catch (Exception ex)
            {
                DialogResult dResult;
                string caption = " rbSavePlot_Click error";
                dResult = MessageBox.Show(string.Format("Error message: {0}\n\nInner Exception: {1}\n\nStack Trace: {2}",
                    ex.Message, ex.InnerException, ex.StackTrace), caption);
            }
        }

        private async void rbPublishToDashboard_Click(object sender, EventArgs e)
        {
            try
            {
                progressBar1.Value = 0;
                progressBar1.Show();
                lblProgressBar.Text = string.Format("Uploading png to dashboard");
                lblProgressBar.Show();
                progressBar1.Maximum = 3;
                progressBar1.Step = 1;

                SaveFileDialog saveFileDialogPublish = new SaveFileDialog();
                string pngPath = Path.GetTempPath() + "\\" + "DDF_graph.png";
                Console.WriteLine(pngPath);
                saveFileDialogPublish.FileName = pngPath;
                saveFileDialogPublish.InitialDirectory = Path.GetTempPath();
                using (System.IO.FileStream fs = (System.IO.FileStream)saveFileDialogPublish.OpenFile())
                {
                    this.chart1.SaveImage(fs, ChartImageFormat.Png);
                }

                progressBar1.PerformStep();
                Debug.WriteLine(Path.GetTempPath());
                string tokenResponse = await getToken();
                JsonTextReader reader = new JsonTextReader(new StringReader(tokenResponse.ToString()));
                JObject jsonResponse = JObject.Load(reader);
                string token = jsonResponse["token"].ToString();
                Debug.WriteLine(string.Format(" generated token: {0}", token));
                progressBar1.PerformStep();
                byte[] pngData = File.ReadAllBytes(pngPath);
                makePostRequest(pngData, token);
                progressBar1.PerformStep();
                using (System.Timers.Timer aTimer = new System.Timers.Timer())
                {

                    lblProgressBar.Text = "Upload Complete";
                    await Task.Delay(1500);
                }
                progressBar1.Hide();
                lblProgressBar.Hide();
                DialogResult dResult;
                string caption = " Successfully published graph to ArcGIS Online";
                dResult = MessageBox.Show(string.Format("Published Successfully\n\n" +
                    "The AGOL item has been updated: https://tlcgis.maps.arcgis.com/sharing/rest/content/users/NesiusM/items/0ea878364bd94c7e9eba2f28f65d7cdc" +
                    "\n\n Note: the publication settings are currently hardcoded/compiled"
                    ));
            }
            catch (Exception ex)
            {
                DialogResult dResult;
                string caption = " Error publishing graph to ArcGIS Online";
                dResult = MessageBox.Show(string.Format("Error message: {0}\n\nInner Exception: {1}\n\nStack Trace: {2}",
                    ex.Message, ex.InnerException, ex.StackTrace), caption);
            }
            
            //Debug.WriteLine(string.Format(" post response: {0}", postResponse.ToString()));
        }

        private async Task<string> getToken()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                string url = "https://www.arcgis.com/sharing/rest/generateToken?username=" +{username}+"&password="+{password} + "&expiration=60&client=referer&referer=http://www.arcgis.com/&f=json";
                HttpContent httpContent = new StringContent("test");
                HttpResponseMessage response = new HttpResponseMessage();

                //var result = await client.PostAsync("https://www.arcgis.com/sharing/generateToken", content);
                try
                {
                    response = await httpClient.PostAsync(url, httpContent).ConfigureAwait(false);
                    
                }
                catch (HttpRequestException hre)
                {
                    Console.WriteLine(hre.Message);
                }
                Task<string> result = response.Content.ReadAsStringAsync();
                Debug.WriteLine(string.Format(" api resonse: {0}", result.Result));
                return (result.Result);
            }
        }

        private void makePostRequest(byte[] _content, string _token)
        {
            string pngPath = Path.GetTempPath() + "\\" + "DDF_graph.png";

            HttpClient httpClient = new HttpClient();
            HttpContent httpContent = new ByteArrayContent(_content);
            httpContent.Headers.Add("Content-Type", "Image/png");

            HttpResponseMessage serverReply = new HttpResponseMessage();
            string dateString = string.Format("{0} - {1}", startDatePicker.Value.ToShortDateString(), endDatePicker.Value.ToShortDateString());
            string baseUrl = "https://tlcgis.maps.arcgis.com/sharing/rest/content/users/NesiusM/items/0ea878364bd94c7e9eba2f28f65d7cdc/update";
            string updateUrl = baseUrl + "?f=json&token=" + _token + "&async=true&file=&overwrite=true&file=" + _content +"&name=DDF_graph.png&filename=DDF_graph.png&title=DDF_graph&type=Image&tags=test&snippet=testing "+ dateString + "&description=testing" + dateString + "&accessInformation=" + System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            using (WebClient client = new WebClient())
            {
                System.Uri updateUri = new System.Uri(updateUrl);
                client.UploadFileAsync(updateUri, pngPath);
            }
            
        }

        private void buildTable(string _tableName, PdfPageBase _page)
        {
            string setting = System.Configuration.ConfigurationManager.AppSettings["FiveMinDurationIntervals"].ToString();
            List<short> cumulationIntervals = setting.Split(',').Select(short.Parse).ToList();

            String[] data = new string[cumulationIntervals.Count() + 1]; // # of rows + 1 for the headers
                                                                            //Build column headers
            var section = (Hashtable)ConfigurationManager.GetSection("SitesAndSensors");
            //Dictionary<string, List<string>> dictSensors = section.Cast<DictionaryEntry>().ToDictionary(d => (string)d.Key, d => ((string)d.Value).Split(',').ToList());
            Dictionary<string, List<string>> _dictSensors = section.Cast<DictionaryEntry>().ToDictionary(d => (string)d.Key, d => ((string)d.Value).Split(',').ToList());
            SortedDictionary<string, List<string>> dictSensors = new SortedDictionary<string, List<string>>(_dictSensors);

            string headers = "Duration (hrs)";
            foreach (KeyValuePair<string, List<string>> entry in dictSensors)
            {
                headers += ";" + chart1.Series[entry.Key].LegendText;
            }
            if (_tableName.Contains("Depth"))
            {
                headers += ";" + "Max Depth (in)";
                data[0] = headers;
                int i = 1;
                foreach (short _interval in cumulationIntervals)
                {
                    string row = (_interval * 5.0 / 60.0).ToString("0.###");
                    List<double> vals = new List<double>();

                    foreach (KeyValuePair<string, List<string>> entry in dictSensors)
                    {
                        // _interval * 5.0 / 60.0
                        DataPoint collection = chart1.Series[entry.Key].Points.First(point => point.XValue == (_interval * 5.0 / 60.0));
                        Debug.WriteLine(String.Format("Interval {0} \tTable: grabbed x: {1}, y: {1}", _interval, row, collection.YValues.LongLength));
                        row += ";" + collection.YValues[0].ToString("0.###");
                        vals.Add(collection.YValues[0]);
                        Debug.WriteLine(String.Format("Table: grabbed x: {0}, y: {1}", row, collection.YValues[0].ToString()));
                    }
                    row += ";" + vals.Max().ToString("0.###");
                    data[i] = row;
                    i++;
                }
            }

            else
            {
                headers += ";" + "Max Inten. (in/hr)";
                data[0] = headers;
                int i = 1;
                foreach (short _interval in cumulationIntervals)
                {
                    string row = (_interval * 5.0 / 60.0).ToString("0.###");
                    double x = _interval * 5.0 / 60.0;
                    List<double> vals = new List<double>();
                    foreach (KeyValuePair<string, List<string>> entry in dictSensors)
                    {
                        // _interval * 5.0 / 60.0
                        DataPoint collection = chart1.Series[entry.Key].Points.First(point => point.XValue == (_interval * 5.0 / 60.0));
                        Debug.WriteLine(String.Format("Interval {0} \tTable: grabbed x: {1}, y: {1}", _interval, row, collection.YValues.LongLength));
                        row += ";" + (collection.YValues[0] / x).ToString("0.###");
                        vals.Add(collection.YValues[0] / x);
                        Debug.WriteLine(String.Format("Table: grabbed x: {0}, y: {1}", row, collection.YValues[0].ToString()));
                    }
                    row += ";" + vals.Max().ToString("0.###");
                    data[i] = row;
                    i++;
                }
            }

            String[][] dataSource = new String[data.Length][];
            for (int j = 0; j < data.Length; j++)
            {
                dataSource[j] = data[j].Split(';');
            }

            PdfBrush brush1 = PdfBrushes.Black;
            PdfTrueTypeFont font1 = new PdfTrueTypeFont(new Font("Arial", 16f, FontStyle.Bold));
            PdfStringFormat format1 = new PdfStringFormat(PdfTextAlignment.Center);
            float y = 10;
            _page.Canvas.DrawString(string.Format("{0}\n{1} - {2}", _tableName,
                startDatePicker.Value.ToShortDateString(), endDatePicker.Value.ToShortDateString()), font1, brush1, _page.Canvas.ClientSize.Width / 2, y, format1);
            y = y + font1.MeasureString(string.Format("{0}\n{1} - {2}", _tableName, startDatePicker.Value.ToShortDateString(),
                endDatePicker.Value.ToShortDateString()), format1).Height;
            y = y + 5;

            PdfTable tableDepth = new PdfTable();
            tableDepth.Style.CellPadding = 2;
            tableDepth.Style.HeaderSource = PdfHeaderSource.Rows;
            tableDepth.Style.HeaderRowCount = 1;
            tableDepth.Style.ShowHeader = true;
            tableDepth.Style.HeaderStyle.BackgroundBrush = PdfBrushes.DarkGray;
            tableDepth.Style.HeaderStyle.Font = new PdfTrueTypeFont(new Font("Arial", 10f, FontStyle.Bold));
            tableDepth.Style.HeaderStyle.StringFormat = format1;
            tableDepth.Style.DefaultStyle.StringFormat = format1;
            tableDepth.DataSource = dataSource;

            tableDepth.Style.AlternateStyle.BackgroundBrush = PdfBrushes.AliceBlue;
            tableDepth.Style.AlternateStyle.StringFormat = format1;
            PdfLayoutResult result = tableDepth.Draw(_page, new PointF(0, y));
            
            
            
        }


private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void tblDepth_Paint(object sender, PaintEventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}
