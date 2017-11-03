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

# What to look for
The _dist_ folder contains the 5 different executables, each ending with its version number.  You may run more than 1 executable at a time.  I would suggest opening 2 at a time, going in order, that is 

- Open 1 and 2
- Examine differences between 1 and 2
- Close 1 and open 3
- Examine differences between 2 and 3
- Close 2 and open 4
- and so on

## Version 1
The AF UI controls are on the left-hand side.  You should not the flow and relationship between the PISystemPicker, AFDatabasePicker, AFFindElementCtrl, and finally the AFTreeView.  

No special anchoring is applied to any controls.  If you attempt to resize the form (wider, taller), you will examine the controls are fixed.

The biggest distinction is that the **Get Data** button is always enabled, even if the user inputs are missing or bad.  Validation of the inputs is not performed _until_ the click event.  This is the typical behavior I would expect to see in a beginner level form.

## Version 2
Has much tighter validation of user inputs.  In fact, the **Get Data** button will not be enabled _unless_ the inputs are valid first.  This means we must help eliminate missing data if we can, as well as add extra visual feedback that the form is incomplete.  There are several methods changed or added to help with this endeavor, but in a nutshell it boils down to having a validation event per control, and even then having all controls funnel to one major validation method to determine whether to enable or disable **Get Data**.

There is also some special anchoring.  Stretch the form and you will see the data grid and the tree view will grown or shrink accordingly.

If you pick a remote Asset Server and you see it taking 1 minute or longer to fetch a lot of data, you should also witness that the Winform appears frozen during the data retrieval.  That's because the retrieval request runs on the UI thread and blocks timely updates.

## Version 3
We replace RecordedValues and PlotValues with their async counterparts: RecordedValuesAsync and PlotValuesAsync.  There is no counterpart for GetValues, but we still wrap it in a background task.  Why?  In order for the Winform to not be blocked.  During a long request, you should be able to move the form around the desktop.

To run async, we pass in a CancellationToken, but we do not yet support cancellation.  

## Version 4
We add support for a user requested cancellation.  The proper async calls for RecordedValuesAsync and PlotValuesAsync should respond to the cancellation request.  However, GetValues will ignore any cancellation request.  We have to add a try-catch block because cancelling will thrown an exception.

## Version 5
Lastly for a bit of fun, we add pure visual razzle-dazzle.  We add a split container to separate the form into a left panel with AF UI controls, and the right panel with the data grid.  Each panel is resizeable.  Try resizing the form, and then using the splitter try resizing the panels.  It doesn't take a lot of code to make this happen, but gives a very nice polished look to the boring Version 1.

We also add a liberal use of font bolding to add a little extra touch to our visual clues.

Finally, an extension method is added that can draw row numbers for each row in the data grid.  That way you can scroll and tell the difference between being on row 300 or 3000.

## Common Features
There are 2 common features to all versions, particulary with information displayed in the data grid.  You may change the displayed UOM.  And you may change the time zone among Local, Utc, or any available Windows time zones on your PC.  Note this is applied to data that has already been retrieved, and therefore does not require an extra trip to the server.



