// Copyright 2011 Miyako Komooka
// Based on Silverlight Vista Sidebar Gadget Template by Ioan Lazarciuc

// Put any required JavaScript code for the settings here
function init() {
    // setup the event handlers for gadget events
    // The javascript Gadget object does not accept multiple event handlers for the same event,
    // so triggering the Silverlight events has to be done from one location
    // or the event handlers have to be reregistered each time the page comes into view
    System.Gadget.onSettingsClosed = settingsClosed;
    System.Gadget.onSettingsClosing = settingsClosing;
}

function settingsClosed(event) {
    var sgs = document.getElementById("silgs");
    var sgd = System.Gadget.document.getElementById("silgd");
    // var sgud = System.Gadget.document.getElementById("silgud");

    try {
        if (sgs != null && sgs != undefined)
            sgs.content.SilverlightGadgetEvents.ScriptSettingsClosedCallback(event);
    }
    catch (ex) {
    }
    try {
        if (sgd != null && sgd != undefined)
            sgd.content.SilverlightGadgetEventsD.ScriptSettingsClosedCallback(event);
    }
    catch (ex) {
    }
    /*
    try {
        if (sgud != null && sgud != undefined)
            sgud.content.SilverlightGadgetEventsU.ScriptSettingsClosedCallback(event);
    }
    catch (ex) {
    }
    */
}

function settingsClosing(event) {
    var sgs = document.getElementById("silgs");
    var sgd = System.Gadget.document.getElementById("silgd");
    // var sgud = System.Gadget.document.getElementById("silgud");

    try {
        if (sgs != null && sgs != undefined)
            sgs.content.SilverlightGadgetEvents.ScriptSettingsClosingCallback(event);
    }
    catch (ex) {
    }
    try {
        if (sgd != null && sgd != undefined)
            sgd.content.SilverlightGadgetEvents.ScriptSettingsClosingCallback(event);
    }
    catch (ex) {
    }
    /*
    try {
        if (sgud != null && sgud != undefined)
            sgud.content.SilverlightGadgetEvents.ScriptSettingsClosingCallback(event);
    }
    catch (ex) {
    }
    */
}