// Copyright 2011 Miyako Komooka
// Based on Silverlight Vista Sidebar Gadget Template by Ioan Lazarciuc

function init()
{
    checkDockState(); // check the initial state

    // setup the event handlers for gadget events
    // The javascript Gadget object does not accept multiple event handlers for the same event,
    // so triggering the Silverlight events has to be done from one location
    // or the event handlers have to be reregistered each time the page comes into view
    System.Gadget.onDock = dockGadget;
    //System.Gadget.onDock = test2;
    System.Gadget.onUndock = undockGadget;
    //System.Gadget.onUndock = test1;

    System.Gadget.onShowSettings = showSettings;
    System.Gadget.visibilityChanged = visibilityChanged;

    // Flyout page
    System.Gadget.Flyout.file = "flyout.html";
    // Settings page
    System.Gadget.settingsUI = "settings.html";

}

function undockGadget() {
    var oBody = System.Gadget.document.body.style;
    oBody.width = '130px';
    oBody.height = '260px';

    var sgd = document.getElementById("silgd");
    try {
        if (sgd != null && sgd != undefined) {
            // sgd.content.dockedGadget.IsVisible = false;
            sgd.content.SilverlightGadgetEventsD.ScriptUndockCallback();
        }
    }
    catch (ex) {
    }

}

function dockGadget() {
    var oBody = System.Gadget.document.body.style;
    oBody.width = '130px';
    oBody.height = '131px';

    var sgd = document.getElementById("silgd");
    try {
        if (sgd != null && sgd != undefined) {
            // sgd.content.dockedGadget.IsVisible = false;
            sgd.content.SilverlightGadgetEventsD.ScriptDockCallback();
        }
    }
    catch (ex) {
    }
}

function showSettings()
{
    var sgd = document.getElementById("silgd");
    // var sgud = document.getElementById("silgud");
    //var sgs = document.getElementById("silgs"); //cannot receive event
    //var sgf = document.getElementById("silgf"); //cannot receive event

    try {
        if (sgd != null && sgd != undefined)
            sgd.content.SilverlightGadgetEventsD.ScriptShowSettingsCallback();
    }
    catch (ex) {
    }
    // try {
    //     if (sgud != null && sgud != undefined)
    //         sgud.content.SilverlightGadgetEventsU.ScriptShowSettingsCallback();
    // }
    // catch (ex) {
    // }
}

function visibilityChanged()
{
    var sgd = document.getElementById("silgd");
    // var sgud = document.getElementById("silgud");
    //var sgs = document.getElementById("silgs"); //cannot receive event
    //var sgf = document.getElementById("silgf"); //cannot receive event

    try {
        if (sgd != null && sgd != undefined)
            sgd.content.SilverlightGadgetEventsD.ScriptVisibilityChangedCallback();
    }
    catch (ex) {
    }
    // try {
    //     if (sgud != null && sgud != undefined)
    //         sgud.content.SilverlightGadgetEventsU.ScriptVisibilityChangedCallback();
    // }
    // catch (ex) {
    // }
}

// function dockGadget()
// {
//     // TODO: add your docking functions here
//     showOrHide("docked", true);
//     showOrHide("undocked", false);

//     var sgd = document.getElementById("silgd"); //cannot receive event
//     var sgud = document.getElementById("silgud"); //cannot receive event
//     //var sgs = document.getElementById("silgs"); //cannot receive event
//     //var sgf = document.getElementById("silgf"); //cannot receive event

//     try {
//         if (sgd != null && sgd != undefined) {
//             sgd.content.dockedGadget.IsVisible = true;
//             sgd.content.SilverlightGadgetEventsD.ScriptDockCallback();
//         }
//     }
//     catch (ex) {
//     }
//     try {

//         if (sgud != null && sgud != undefined) {
//             sgud.content.undockedGadget.IsVisible = false;
//             sgud.content.SilverlightGadgetEventsU.ScriptDockCallback();
//         }
//     }
//     catch (ex) {
//     }
// }

// function undockGadget()
// {
//     // TODO: add your undocking functions here
//     //var oBody = document.body.style;
//     //oBody.width = '130px';
//     //oBody.height = '200px';
//     showOrHide("undocked", true);
//     showOrHide("docked", false);

//     var sgd = document.getElementById("silgd"); //cannot receive event
//     var sgud = document.getElementById("silgud"); //cannot receive event
//     //var sgs = document.getElementById("silgs"); //cannot receive event
//     //var sgf = document.getElementById("silgf"); //cannot receive event

//     try {
//         if (sgd != null && sgd != undefined) {
//             // sgd.content.dockedGadget.IsVisible = false;
//             sgd.content.SilverlightGadgetEventsD.ScriptUndockCallback();
//         }
//     }
//     catch (ex) {
//     }
//     try {
//         if (sgud != null && sgud != undefined) {
//             sgud.content.undockedGadget.IsVisible = true;
//             sgud.content.SilverlightGadgetEventsU.ScriptUndockCallback();
//         }
//     }
//     catch (ex) {
//     }
// }

function checkDockState()
{
    if (System.Gadget.docked) {
        dockGadget();
    }
    else {
        undockGadget();
    }
}

// function showOrHide(oHTMLElement, bShowOrHide)
// {
//     try {
//         if (typeof (oHTMLElement) == "string") {
//             oHTMLElement = document.getElementById(oHTMLElement);
//         }
//         if (oHTMLElement && oHTMLElement.style) {
//             if (bShowOrHide == 'inherit') {
//                 oHTMLElement.style.visibility = 'inherit';
//             }
//             else {
//                 if (bShowOrHide) {
//                     oHTMLElement.style.visibility = 'visible';
//                 }
//                 else {
//                     oHTMLElement.style.visibility = 'hidden';
//                 }
//                 try {
//                     if (bShowOrHide) {
//                         oHTMLElement.style.display = 'block';
//                     }
//                     else {
//                         oHTMLElement.style.display = 'none';
//                     }
//                 }
//                 catch (ex) {
//                 }
//             }
//         }
//     }
//     catch (ex) {
//     }
// }
function flyout()
{
    System.Gadget.Flyout.show = true;
}