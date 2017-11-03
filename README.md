# AF-SDK-UI-AFDataViewer
Several versions of a generic AFDataViewer using AF SDK UI controls.  Each version builds upon the previous.  Displays data for a selected AFAttribute for a specified time range.

# At-a-glance
- WinForm application
- .NET Framework 4.5.2
- AF SDK 2.9 or later
- Visual Studio 2017
- C# 7.0 
- Does not use tuples so we can use previous version of .NET.

# Versions

| Version | Description |
| --- | --- |
| 1 | Beginner level form where all validation of user inputs are delayed until the 'Get Data' button is clicked. |
| 2 | We tighten up the UI by validating as we go.  In fact, the 'Get Data' button will not be enabled unless all inputs pass validation. |
| 3 | Add async calls for RecordedValues and PlotValues.  Cancellation is not yet active. |
| 4 | Add cancellation feature to async calls. |
| 5 | Pure razzle-dazzle.  Add a splitter to divide the form into 2 sizeable panels, add more liberal use of bolding, and show row numbers in the data grid. |
