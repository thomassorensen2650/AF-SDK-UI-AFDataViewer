/***************************************************************************
   Copyright 2017 OSIsoft, LLC.
   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at
       http://www.apache.org/licenses/LICENSE-2.0
   
   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
 ***************************************************************************/

//--------------------------------------------------------------------------
//
//  Version 3:
//
//      Async data retrieval.  Try fetching data for a much wider time
//      range and you notice the form looks frozen.  With async, the form
//      should be more responsive while a laborious data request is being
//      fulfilled.
//
//      This does not support a Cancellation feature (yet).
//      That will be shown in Version 4.
//
//--------------------------------------------------------------------------


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using OSIsoft.AF;
using OSIsoft.AF.PI;
using OSIsoft.AF.Asset;
using OSIsoft.AF.Time;
using OSIsoft.AF.Data;
using OSIsoft.AF.UnitsOfMeasure;
using OSIsoft.AF.UI;
using System.Diagnostics;

// NEW to Version 3
using System.Threading;

namespace AFDataViewer
{
    public partial class MainForm : Form
    {

        public MainForm()
        {
            // Unless you are an advanced developer who knows (1) the rare exceptions and (2) what they are doing,
            // the initialize call should always be the first line in a form's constructor.
            InitializeComponent();

            // Need to programmatically link the UI controls together.
            // By declaring a PISystemPicker to the AFDatabasePicker, any changes made our PISystem picker
            // will automatically be reflected in the AFDatabase picker.  Our PISystem picker is to Connect Automatically,
            // which means that when the user picks a different PISystem, then (1) that selected PISystem is automatically 
            // connected, and (2) the AFDatabase picker has its PISystem updated with the newly selected PISystem, which
            // (3) causes the databases in the AFDatabase picker to be refreshed to those belonging to the newly
            // selected PISystem.  However, there is a timing issue if going to VM's in the cloud or across a laggy WAN.
            // Therefore you want to declare the the database picker also connects automatically to its PISystem.
            afDatabasePicker1.ConnectAutomatically = true;  // Already set in property sheet.
            afDatabasePicker1.SystemPicker = piSystemPicker1;

            LoadTimeZonePicks();
            PlotIntervals = this.Width;
            lblElapsed.Text = string.Empty;

            SetNitPickyAnchoringDetails();
            cbxDataMethods.SelectedIndex = 0;  // Make sure we always have SOMETHING
            cbxDataMethods.Tag = true;  // Using Tag property to hold a validation flag.
            CheckUserInputsState();
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            var sb = new StringBuilder();
            sb.AppendLine("We use Async data retrieval.  While there are built-in methods for RecordedValuesAsync and PlotValuesAsync,");
            sb.AppendLine(" there is  no such method for GetValues.");
            sb.AppendLine();
            sb.AppendLine("We also tighten up code when the form is busy performing a long data retrieval request by disabling any user inputs.");
            MessageBox.Show(this, sb.ToString(), "About Version 3", MessageBoxButtons.OK);
        }

        // CHANGED for Version 3
        private void SetNitPickyAnchoringDetails()
        {
            dataGridView1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            gbxPostFetch.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            afTreeView1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Top;
        }

        private int PlotIntervals { get; set; }

        private const int GetValuesIndex = 0;
        private const int RecordedValuesIndex = 1;
        private const int PlotValuesIndex = 2;
        private AFValues OriginalValues = new AFValues();

        // NEW to Version 3
        private CancellationToken _cancelToken = CancellationToken.None;
        private Stopwatch _stopwatch = new Stopwatch();
        

        #region "Convenience Properties"

        // These properties are not absolutely necessary by any means but they are quite convenient.
        // They help to make the code shorter and more readable.
        private PISystem AssetServer => piSystemPicker1.PISystem;
        private AFDatabase Database => afDatabasePicker1.AFDatabase;
        private AFElement Element => afElementFindCtrl1.AFElement;
        private AFAttribute SelectedAttribute => afTreeView1.AFSelection as AFAttribute;
        private AFAttribute DataAttribute { get; set; }

        #endregion

        #region "Leftside"

        private void piSystemPicker1_SelectionChange(object sender, OSIsoft.AF.UI.SelectionChangeEventArgs e)
        {
            // No need to update the database picker as that is done automatically (see MainForm() constructor).
        }

        private void afDatabasePicker1_SelectionChange(object sender, SelectionChangeEventArgs e)
        {
            // Propagate changes down the chain.  Code below works as desired even if objects being set are null.
            afElementFindCtrl1.Database = Database;

            // Select a default element, i.e. the first if any are found.
            if (Database != null && Database.Elements.Count > 0)
            {
                afElementFindCtrl1.AFElement = Database.Elements[0];
            }
            else
            {
                afElementFindCtrl1.AFElement = null;
            }

            // Trigger the appropriate event handler to continue the propagation.
            afElementFindCtrl1_AFElementUpdated(afElementFindCtrl1, new CancelEventArgs());
        }

        private void afElementFindCtrl1_AFElementUpdated(object sender, CancelEventArgs e)
        {
            // Propagate changes down the chain.  Code below works as desired even if objects being set are null.
            afTreeView1.AFRoot = Element?.Attributes;
        }

        private void afTreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // NEW to Version 2
            if (SelectedAttribute != null)
            {
                lblSelectedAttrPath.Text = SelectedAttribute.GetPath(SelectedAttribute.Database);
                lblSelectedAttrPath.ForeColor = Color.Black;
                lblTreeHeading.ForeColor = Color.Black;
            }
            else
            {
                lblSelectedAttrPath.Text = "Select an AFAttribute from the tree on the lower left";
                lblSelectedAttrPath.ForeColor = Color.Red;
                lblTreeHeading.ForeColor = Color.Red;
            }

            cbxDataMethods_SelectedIndexChanged(cbxDataMethods, EventArgs.Empty);
            // this later calls CheckUserInputsState();
        }

        #endregion

        #region "Time Zone"

        private const int LocalTimeIndex = 0;
        private const int UtcTimeIndex = 1;

        private void LoadTimeZonePicks()
        {
            // The next line is already set in the Property sheet, but the DropDownList style does 2 very useful
            // things to our UI: (1) it restricts the available picks to only those items in the list, and
            // (2) we can now reliably trigger on a very specficic event of SelectedIndex changed.
            cbxTimeZones.DropDownStyle = ComboBoxStyle.DropDownList;

            cbxTimeZones.Items.Clear();

            // Add a fixed 0 and 1 item to Local (default) and UTC respectively.
            // Note the first 2 items will be literal text strings, whereas subsequent items
            // will be TimeZoneInfo objects.  We don't set the first 2 by index, so ther order
            // will Add is important to align with the LocalTimeIndex and UtcTimeIndex constants.
            cbxTimeZones.Items.Add("AFTime LocalTime");
            cbxTimeZones.Items.Add("AFTime UtcTime");
            cbxTimeZones.Items.AddRange(TimeZoneInfo.GetSystemTimeZones().ToArray());
            cbxTimeZones.SelectedIndex = LocalTimeIndex; // or LocalTime
        }

        private void cbxTimeZones_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConvertGridTimestamps();
        }

        #endregion

        private async void btnGetData_Click(object sender, EventArgs e)
        {
            _cancelToken = new CancellationToken();

            PrepareBeforeDataRetrieval();

            // Need this here for local variables.
            TimeZoneBasis tzBasis = TimeZoneBasis.Local;
            TimeZoneInfo destinationTimeZone = null;
            SetAndGetTimeZoneHeading(ref tzBasis, ref destinationTimeZone);
            var targetUom = SetAndGetUomHeading();
            var dataMethodIndex = cbxDataMethods.SelectedIndex;
            var timeRange = new AFTimeRange(GetStartTime().Value, GetEndTime().Value);
            IEnumerable<AFValue> streamingValues = null;

            FreezeUserInputs(); // last thing it does is start timers


            //---------------------------------------------------------------------------
            // Two examples for async calls are provided, but only 1 may be active.
            // The first is quite straight-forward to write, but the UI may still be
            // a bit sluggish.  The second is more complicated as it tries to create a
            // task and force it to run in the background by declaring it to be 
            // LongRunning, but the UI should be more responsive.
            //---------------------------------------------------------------------------
            // You are encouraged to experiment and review to see for yourself.
            //---------------------------------------------------------------------------
            var forceToBackground = true;

            if (forceToBackground)
            {
                // More complicated:
                streamingValues = await RetrieveDataInBackgroundAsync(dataMethodIndex, timeRange).ConfigureAwait(true);
            }
            else
            {
                // Straight-forward:
                streamingValues = await RetrieveDataAsync(dataMethodIndex, timeRange).ConfigureAwait(true);
            }

            if (streamingValues != null)
            {
                foreach (var pv in streamingValues)
                {
                    OriginalValues.Add(pv);
                    var displayTime = ConvertAFTime(pv.Timestamp, tzBasis, destinationTimeZone);
                    var displayValue = ConvertValueUom(pv, targetUom);
                    var rowIndex = dataGridView1.Rows.Add(displayTime, displayValue);
                }
            }

            UnfreezeUserInputs(); // first thing it does is stop timers

            _cancelToken = CancellationToken.None;
        }

        private async Task<AFValues> RetrieveDataAsync(int dataMethodIndex, AFTimeRange timeRange)
        {
            // In this straightforward version, we use a simple "await" for each respective line.
            // This works and for RecordedValuesAsync and PlotValuesAsync we will truly get streaming values.
            // However, the UI may still appear frozen.
            AFValues streamingValues = null;

            switch (dataMethodIndex)
            {
                case RecordedValuesIndex:
                    streamingValues = await DataAttribute.Data.RecordedValuesAsync(timeRange, AFBoundaryType.Interpolated, null, null, false, 0, _cancelToken);
                    break;
                case PlotValuesIndex:
                    streamingValues = await DataAttribute.Data.PlotValuesAsync(timeRange, PlotIntervals, null, _cancelToken);
                    break;
                default:
                    // GetValues does not have an async version to produce IEnumerable<AFValue>.
                    // So we write a custom method async method that can do that in a background task.
                    // Except it returns the full list at once rather than streaming it in pages.
                    // Synopis: Background = Yes, Streaming = No, Cancellable = No.
                    streamingValues = await GetValuesAsync(DataAttribute, timeRange);
                    break;
            }

            return streamingValues;
        }

        private async Task<AFValues> RetrieveDataInBackgroundAsync(int dataMethodIndex, AFTimeRange timeRange)
        {
            // This more complicated version tries to force the task to be in the background thanks to
            // the TaskCreationOptions.LongRunning option.  We still use the respective Async data methods,
            // but rather than await, because its already running in a background thread and not on the UI,
            // we will just grab the .Result.
            AFValues streamingValues = null;

            await Task.Factory.StartNew(() =>
            {
                // Well, what do you know?  
                // Ultimately, we ARE calling the straight-forward method.
                streamingValues = RetrieveDataAsync(dataMethodIndex, timeRange).Result;

            }, _cancelToken, TaskCreationOptions.LongRunning, TaskScheduler.Current);

            return streamingValues;
        }

        private async Task<AFValues> GetValuesAsync(AFAttribute attribute, AFTimeRange timeRange)
        {
            var values = new AFValues();

            await Task.Factory.StartNew(() =>
            {
                // Why wrap the non-async GetValues call in this task?  The only good reason is
                // to keep the form responsive, that is the timer keeps ticking, or the user
                // may resize or move the form within his or her desktop.
                values = attribute.GetValues(timeRange, 0, null);

            }, _cancelToken, TaskCreationOptions.LongRunning, TaskScheduler.Current);

            return values;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_stopwatch != null)
            {
                DisplayStopwatch();
            }
        }

        private void DisplayStopwatch()
        {
            lblElapsed.Text = _stopwatch.Elapsed.TotalSeconds.ToString("N2") + " seconds";
        }

        private void PrepareBeforeDataRetrieval()
        {
            var differentUomClass = SelectedAttribute?.DefaultUOM != DataAttribute?.DefaultUOM;

            DataAttribute = SelectedAttribute;

            lblDataAttrPath.Text = (DataAttribute != null) ? DataAttribute.GetPath(DataAttribute.Database) : "Click 'Get Data'";
            lblElapsed.Text = string.Empty;

            dataGridView1.Rows.Clear();
            OriginalValues = new AFValues();

            if (differentUomClass)
            {
                // Whenever the Uom pick box is re-built, the default UOM will be selected.
                FillUomPickbox(DataAttribute?.DefaultUOM);
            }
        }

        private void FreezeUserInputs()
        {
            UseWaitCursor = true;

            piSystemPicker1.Enabled = false;
            afDatabasePicker1.Enabled = false;
            afElementFindCtrl1.Enabled = false;
            afTreeView1.Enabled = false;
            tbStartTime.Enabled = false;
            tbEndTime.Enabled = false;
            cbxDataMethods.Enabled = false;
            tbEndTime.Enabled = false;
            tbStartTime.Enabled = false;
            cbxDataMethods.Enabled = false;
            btnGetData.Enabled = false;

            // Start timers
            timer1.Enabled = true;
            timer1.Start();
            _stopwatch = Stopwatch.StartNew();
        }

        private void UnfreezeUserInputs()
        {
            // Stop timers
            timer1.Stop();
            _stopwatch.Stop();
            DisplayStopwatch();

            piSystemPicker1.Enabled = true;
            afDatabasePicker1.Enabled = true;
            afElementFindCtrl1.Enabled = true;
            afTreeView1.Enabled = true;
            tbStartTime.Enabled = true;
            tbEndTime.Enabled = true;
            cbxDataMethods.Enabled = true;
            tbEndTime.Enabled = true;
            tbStartTime.Enabled = true;
            cbxDataMethods.Enabled = true;
            btnGetData.Enabled = true;

            UseWaitCursor = false;
        }

        private void CheckUserInputsState()
        {
            // Bare minimum information needed to fetch the data:
            //  (1) An attribute selected in the afTreeView1
            //  (2) tbStartTime parses to valid AFTime
            //  (3) tbEndTime parses to valid AFTime
            //  (4) cbxDataMethods has a selected item AND the attribute supports that selected method.

            var inputsAreValid = true; // assume to be true

            //  (1) An attribute selected in the afTreeView1
            if (SelectedAttribute == null)
            {
                inputsAreValid = false;
            }

            //  (2) tbStartTime parses to valid AFTime
            if (GetStartTime() == null)
            {
                inputsAreValid = false;
            }

            //  (3) tbEndTime parses to valid AFTime
            if (GetEndTime() == null)
            {
                inputsAreValid = false;
            }

            //  (4) cbxDataMethods has a selected item AND the attribute supports that method.
            //      If not valid, the .Tag property will be false.
            if (cbxDataMethods.Tag == (object)false)
            {
                inputsAreValid = false;
            }

            btnGetData.Enabled = inputsAreValid;
        }



        // While this seems to be a likely candidate for an extension method,
        // there is a strong possibility that text could be null, which
        // doesn't work well with extension methods.
        public static AFTime? ParseToAFTime(string text)
        {
            try
            {
                var time = AFTime.MaxValue;
                // AFTime.TryParse
                // https://techsupport.osisoft.com/Documentation/PI-AF-SDK/Html/M_OSIsoft_AF_Time_AFTime_TryParse_2.htm
                // DateTime.TryParse
                // https://msdn.microsoft.com/en-us/library/ch92fbc1(v=vs.110).aspx
                if (AFTime.TryParse(text, out time))
                {
                    return time;
                }
            }
            catch
            { }
            return null;
        }

        private AFTime? GetStartTime() => ParseToAFTime(tbStartTime.Text);
        private AFTime? GetEndTime() => ParseToAFTime(tbEndTime.Text);

        private void TimeBox_KeyUp(object sender, KeyEventArgs e)
        {
            var textBox = sender as TextBox;
            TimeBoxValidate(textBox);
        }

        private void TimeBoxValidate(TextBox textBox)
        {
            if (textBox == null)
                return;

            if (ParseToAFTime(textBox.Text).HasValue)
            {
                textBox.BackColor = Color.White;
                textBox.ForeColor = Color.Black;
            }
            else
            {
                textBox.BackColor = Color.LightYellow;
                textBox.ForeColor = Color.Red;
            }
            CheckUserInputsState();
        }

        private void cbxDataMethods_SelectedIndexChanged(object sender, EventArgs e)
        {
            var supported = true;
            var dataMethodIndex = cbxDataMethods.SelectedIndex;
            if (dataMethodIndex < 0)
            {
                supported = false;
            }
            else if (SelectedAttribute != null)
            {
                if (dataMethodIndex == RecordedValuesIndex && !SelectedAttribute.IsSupportedDataMethod(AFDataMethods.RecordedValues))
                {
                    supported = false;
                }
                else if (dataMethodIndex == PlotValuesIndex && !SelectedAttribute.IsSupportedDataMethod(AFDataMethods.PlotValues))
                {
                    supported = false;
                }
            }
            if (supported)
            {
                cbxDataMethods.BackColor = Color.White;
                cbxDataMethods.ForeColor = Color.Black;
            }
            else
            {
                cbxDataMethods.BackColor = Color.Yellow;
                cbxDataMethods.ForeColor = Color.Red;
            }

            cbxDataMethods.Tag = supported;

            CheckUserInputsState();
        }

        private enum TimeZoneBasis { Local, Utc, Custom };

        private DateTime ConvertAFTime(AFTime time, TimeZoneBasis tzBasis, TimeZoneInfo destinationTimeZone)
        {
            switch (tzBasis)
            {
                case TimeZoneBasis.Local:
                    return time.LocalTime;
                case TimeZoneBasis.Custom:
                    return TimeZoneInfo.ConvertTimeFromUtc(time.UtcTime, destinationTimeZone);
                case TimeZoneBasis.Utc:
                default:
                    return time.UtcTime;
            }
        }

        private void ConvertGridTimestamps()
        {
            TimeZoneBasis tzBasis = TimeZoneBasis.Local;
            TimeZoneInfo destinationTimeZone = null;
            SetAndGetTimeZoneHeading(ref tzBasis, ref destinationTimeZone);

            for (int rowIndex = 0; rowIndex < dataGridView1.Rows.Count; rowIndex++)
            {
                // Yes, the following could be 1 long one-liner of code.
                // But if you are following along in a debugger, it helps to have these appear in Locals window.
                var cell = dataGridView1.Rows[rowIndex].Cells[0];
                var displayTime = ConvertAFTime(OriginalValues[rowIndex].Timestamp, tzBasis, destinationTimeZone);
                cell.Value = displayTime;
            }
        }

        // Avoided ValueTuple to target earlier framework.
        private void SetAndGetTimeZoneHeading(ref TimeZoneBasis tzBasis, ref TimeZoneInfo destinationTimeZone)
        {
            tzBasis = GetTimeZoneBasis();
            destinationTimeZone = null;

            switch (tzBasis)
            {
                case TimeZoneBasis.Local:
                    dataGridView1.Columns[0].HeaderText = "Local Time";
                    break;
                case TimeZoneBasis.Utc:
                    dataGridView1.Columns[0].HeaderText = "UTC Time";
                    break;
                case TimeZoneBasis.Custom:
                    destinationTimeZone = cbxTimeZones.SelectedItem as TimeZoneInfo;
                    if (destinationTimeZone != null)
                    {
                        dataGridView1.Columns[0].HeaderText = destinationTimeZone.DisplayName;
                    }
                    else
                    {
                        dataGridView1.Columns[0].HeaderText = "Unknown Time Zone";
                        tzBasis = TimeZoneBasis.Utc;
                    }
                    break;
            }
        }

        private TimeZoneBasis GetTimeZoneBasis()
        {
            if (cbxTimeZones.SelectedIndex == UtcTimeIndex)
            {
                return TimeZoneBasis.Utc;
            }
            else if (cbxTimeZones.SelectedIndex > LocalTimeIndex)
            {
                return TimeZoneBasis.Custom;
            }
            return TimeZoneBasis.Local;
        }

        private void FillUomPickbox(UOM defaultUom)
        {
            cbxUoms.Items.Clear();

            // Index 0 is a literal text string
            cbxUoms.Items.Add("( default )");
            cbxUoms.SelectedIndex = 0;

            if (defaultUom == null)
            {
                return;
            }

            // All subsequent items are UOM objects!
            cbxUoms.Items.AddRange(defaultUom.Class.UOMs.ToArray());
        }

        private void ConvertGridUoms()
        {
            var targetUOM = SetAndGetUomHeading();

            for (int rowIndex = 0; rowIndex < dataGridView1.Rows.Count; rowIndex++)
            {
                var cell = dataGridView1.Rows[rowIndex].Cells[1];
                var pv = OriginalValues[rowIndex];
                var displayValue = ConvertValueUom(pv, targetUOM);
                cell.Value = displayValue;
            }
        }

        private object ConvertValueUom(AFValue pv, UOM targetUOM)
        {
            if (!pv.IsGood || pv.UOM == null || targetUOM == null || pv.UOM == targetUOM)
            {
                return pv.Value;
            }
            return pv.Convert(targetUOM).Value;
        }

        private UOM SetAndGetUomHeading()
        {
            UOM targetUOM = null;

            if (cbxUoms.SelectedIndex <= 0)
            {
                targetUOM = DataAttribute?.DefaultUOM;
            }
            else
            {
                targetUOM = cbxUoms.SelectedItem as UOM;
            }

            if (targetUOM != null)
            {
                dataGridView1.Columns[1].HeaderText = $"Value ( {targetUOM.Abbreviation} )";
            }
            else
            {
                dataGridView1.Columns[1].HeaderText = "Value";
            }

            return targetUOM;
        }

        private void cbxUoms_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConvertGridUoms();
        }

        #region "Not Needed"

        // The code here is not needed.  It's not even called by anything.
        // But its included to let you see which changes are needed in each property sheet.

        private void NotCalled_piSystemPicker1()
        {
            // The following properties make the picker appear much like a combo box
            piSystemPicker1.ConnectAutomatically = false;  // Okay if its true, but more important that Database Picker connects automatically.
            piSystemPicker1.ShowBegin = false;
            piSystemPicker1.ShowConnect = false;  // since we connect automatically
            piSystemPicker1.ShowDelete = false;
            piSystemPicker1.ShowEnd = false;
            piSystemPicker1.ShowFind = false;
            piSystemPicker1.ShowList = false;
            piSystemPicker1.ShowNavigation = false;
            piSystemPicker1.ShowNew = false;
            piSystemPicker1.ShowNext = false;
            piSystemPicker1.ShowPrevious = false;
            piSystemPicker1.ShowProperties = false;

            // The key event we want to handle:     SelectionChange

            // Key property to reference in project:   piSystemPicker1.PISystem
        }

        private void NotCalled_afDatabasePicker1()
        {
            // The following properties make the picker appear much like a combo box

            // Technically you don't connect to an AFDatabase, but rather the PISystem that owns it.
            afDatabasePicker1.ConnectAutomatically = true;

            afDatabasePicker1.ShowBegin = false;
            afDatabasePicker1.ShowConfigurationDatabase = ShowConfigurationDatabase.Hide;
            afDatabasePicker1.ShowDelete = false;
            afDatabasePicker1.ShowEnd = false;
            afDatabasePicker1.ShowFind = false;
            afDatabasePicker1.ShowList = false;
            afDatabasePicker1.ShowNavigation = false;
            afDatabasePicker1.ShowNew = false;
            afDatabasePicker1.ShowNext = false;
            afDatabasePicker1.ShowPrevious = false;
            afDatabasePicker1.ShowProperties = false;

            // The key event we want to handle: SelectionChange.

            // Key property to reference in project:   afDatabasePicker1.AFDatabase

            // This line is added to our form's constructor:
            afDatabasePicker1.SystemPicker = piSystemPicker1;
        }

        private void NotUsed_afElementFindCtrl1()
        {
            // No changes to property sheet but you do want to set afElementFindCtrl1.AFDatabase at runtime.

            // The key event we want to handle: AFElementUpdated

            // Key property to reference in project:   afElementFindCtrl1.AFElement
        }

        private void NotUsed_afTreeView1()
        {
            // The AFTreeView has a lot of flexibility.  You declare what is allowed or not allowed in a given view.
            // I disallow any library objects here.  You could have the tree show:
            //      1) elements and attributes
            //      2) elements only
            //      3) attributes only
            // Since I also use afElementFindCtrl1, I will show attributes only.
            // The key to use the tree view is setting the AFRoot, which can be any AFObject.
            // Given the many different types of AFObjects as well as the over 2 dozen Show-related properties,
            // there is a large amount of possible different views you can use.

            // Since I want to show attributes only, the AFRoot object is not the selected Element, but rather
            // the Element.Attributes.

            afTreeView1.ShowAnalyses = false;
            afTreeView1.ShowAnalysisTemplates = false;
            afTreeView1.ShowAttributes = true;  // Only thing we really want to see in tree.
            afTreeView1.ShowCases = false;
            afTreeView1.ShowCaseTemplates = false;
            afTreeView1.ShowCategories = false;
            afTreeView1.ShowConfigurationDatabase = ShowConfigurationDatabase.Hide;
            afTreeView1.ShowConnections = false;
            afTreeView1.ShowContacts = false;
            afTreeView1.ShowDatabases = false;
            afTreeView1.ShowElements = false;
            afTreeView1.ShowElementTemplates = false;
            afTreeView1.ShowEnumerations = false;
            afTreeView1.ShowEventFrames = false;
            afTreeView1.ShowEventFrameTemplates = false;
            afTreeView1.ShowLayers = false;
            afTreeView1.ShowLines = true;
            afTreeView1.ShowModels = false;
            afTreeView1.ShowModelTemplates = false;
            afTreeView1.ShowNodeToolTips = false;
            afTreeView1.ShowNotifications = false;
            afTreeView1.ShowNotificationTemplates = false;
            afTreeView1.ShowPlugIns = false;
            afTreeView1.ShowPlusMinus = true;
            afTreeView1.ShowPorts = false;
            afTreeView1.ShowReferenceTypes = false;
            afTreeView1.ShowRootLines = true;
            afTreeView1.ShowStateInImage = true;
            afTreeView1.ShowTableConnections = false;
            afTreeView1.ShowTables = false;
            afTreeView1.ShowTransfers = false;
            afTreeView1.ShowTransferTemplates = false;
            afTreeView1.ShowUOMClasses = false;
            afTreeView1.ShowUOMs = false;

            // The key event we want to handle: AfterSelect

            // Key property to reference in project:   afTreeView1.AFSelection
            // You most likely will want to to cast the AFSelection from generic AFObject to something more type specific.
        }

        #endregion
    }
}
