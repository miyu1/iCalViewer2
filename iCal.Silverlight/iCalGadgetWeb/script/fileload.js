// Copyright 2011 Miyako Komooka
var Busy = false;
var Mode = 0; // 1: Load Event Calendar, 2: Load Holiday Calendar

var FileName = null;
var UID = null;
var Password = null;
var FileContent = null;
var FileInfo = null;

var EventCalendarContent = null;
var HolidayCalendarContent = null;

var EventCalendarInfo = null;
var HolidayCalendarInfo = null;

var FSO = null;

var Log = "";

// entry function of this module
function startLoadCalendar() {
    Log = "startLoadCalendar\n";

    if( Busy == true ){
        // do nothing
        return;
    }

    Busy = true;
    Mode = 1;
    
    var sgd = document.getElementById("silgd");
    try {
        if (sgd != null && sgd != undefined) {
            sgd.content.dockedGadget.BeforeLoadCalendar();
        }
    } catch( ex ){
    }

    gTimerId = setTimeout( loadEventCalendar, 50 );
}

function loadEventCalendar() {
    Log += "loadEventCalendar\n";

    FileName = System.Gadget.Settings.readString( "CalendarFile" );
    UID = System.Gadget.Settings.readString( "CalendarUID" );
    Password = System.Gadget.Settings.readString( "CalendarPass" );
    Password = passwordDecode( Password );

    setTimeout( loadFile );
}

function loadHolidayCalendar() {
    Log += "loadHolidayCalendar\n";
    
    FileName = System.Gadget.Settings.readString( "HolidayFile" );
    UID = System.Gadget.Settings.readString( "HolidayUID" );
    Password = System.Gadget.Settings.readString( "HolidayPass" );
    Password = passwordDecode( Password );

    setTimeout( loadFile );
}

function loadFile() {
    Log += "loadFile\n";

    if( FileName.substring( 0, 4 ) == "http" ){
        loadFromWeb( FileName, UID, Password );
    } else if( FileName.toLowerCase().substring( 0, 6 ) == "webcal" ){
        FileName = "http" + FileName.substring( 6 );
        loadFromWeb( FileName, UID, Password );
    } else if( FileName.toLowerCase().substring( 0, 6 ) == "caldav" ){
        loadFromWeb( FileName, UID, Password );
    } else if( FileName.toLowerCase().substring( 0, 6 ) == "webdav" ){
        loadFromWeb( FileName, UID, Password );
    } else {
        loadFromLocal( FileName, UID, Password, false );
    }
}

function loadFromLocal( filename, uid, pass, deleteFlag ) {
    Log += "loadFromLocal : " + filename + "\n";

    var error = false;

    if( filename && filename.length > 0 ){
        var content = null;

        try {
            var stream = new ActiveXObject("ADODB.Stream");
            stream.Type = 2; // StreamTypeEnum.adTypeText;
            stream.Charset = "UTF-8";
            stream.Open();
            stream.LoadFromFile( filename );
            
            content = stream.ReadText();
            stream.Close();

            FileInfo = "OK";
        } catch( ex ) {
            error = true;
            content = "";

            var s = "";
            for( var key in ex ){
                s += key + "=" + ex[key] + ",";
            }
            FileInfo = "Exception:" + s;
        }
        
        FileContent = content;

        if( deleteFlag == true ){
            if( error ){
                if( ErrorRetry > 0 ){
                    ErrorRetry --;
                    setTimeout( loadFromWebTimerHandler, 100 ); /**/
                    return;
                }
            }
            
            try {
                FSO.DeleteFile( filename );
            } catch ( ex ) { }
        }


    } else {
        FileContent = "";
        FileInfo = ""; // no file specified, no info to set;
    }

    // setTimeout( modeChange );
    modeChange();
}

var FilePath = null;
var RetryCount = 100;
var ErrorRetry = 2;

function loadFromWeb( url, uid, pass ) {
    Log += "loadFromWeb : " + url + "\n";

    FilePath = System.Environment.getEnvironmentVariable( "TMP" ) 
        + "\\" + Math.floor( Math.random() * 1000000 );
    
    var loader = System.Gadget.path + "\\WebLoader.exe";

    var options = "\"" + url + "\"" + " \"" + FilePath + "\"";
    if( uid && uid.length > 0 ){
        options += " /user:" + uid + " /password:" + pass;
    }
    options += " /log:" + FilePath + ".log";
    System.Shell.execute( loader, options );

    FSO = new ActiveXObject("Scripting.FileSystemObject");

    RetryCount = 100;
    ErrorRetry = 2;
    setTimeout( loadFromWebTimerHandler );

    // FileContent = options;
    // setTimeout( modeChange );
}

function loadFromWebTimerHandler() {
    Log += "loadFromWebTimerHandler : " + RetryCount + "\n";

    var error = false;

    if( FSO.FileExists( FilePath + ".success" ) ){
        loadFromLocal( FilePath + ".success", null, null, true );
    } else if( FSO.FileExists( FilePath + ".fail" ) ) {
        FileContent = "";

        // first line of fail file is short description of error
        try {
            var stream = new ActiveXObject("ADODB.Stream");
            stream.Type = 2; // StreamTypeEnum.adTypeText;
            stream.Charset = "UTF-8";
            stream.Open();
            stream.LoadFromFile( FilePath + ".fail" );
            
            content = stream.ReadText();
            var index = content.indexOf( "\x0A" );
            if( index > 0 ){
                FileInfo = content.substring( 0, index );
                if( FileInfo.charAt( FileInfo.length - 1 ) == "\x0D" ){
                    FileInfo = FileInfo.substring( 0, FileInfo.length -1 );
                }
            } else {
                FileInfo = content;
            }
            stream.Close();

        } catch ( ex ){
            error = true;
            var s = "";
            for( var key in ex ){
                s += key + "=" + ex[key] + ",";
            }
            FileInfo = "Exception:" + s;
            // FileInfo = "Exception:" + ex;
        }

        if( error == true ){
            if( ErrorRetry > 0 ){
                ErrorRetry --;
                setTimeout( loadFromWebTimerHandler, 100 );
                return;
            }
        }

        try {
            FSO.DeleteFile( FilePath + ".fail" );
        } catch ( ex ){
        }

        // setTimeout( modeChange );
        modeChange();
    } else {
        RetryCount --;
        if( RetryCount > 0 ){
            setTimeout( loadFromWebTimerHandler, 100 );
        } else {
            FileContent = "";
            FileInfo = "Timeout";
            // setTimeout( modeChange );
            modeChange();
        }
    }
}

function modeChange()
{
    Log += "modeChange : " + Mode + "\n";

    if( Mode == 1 ){
        EventCalendarContent = FileContent;
        EventCalendarInfo = FileInfo;
        Mode = 2;
        setTimeout( loadHolidayCalendar );
    } else if( Mode == 2 ){
        HolidayCalendarContent = FileContent;
        HolidayCalendarInfo = FileInfo;

        var sgd = document.getElementById("silgd");
        var sgs = document.getElementById("silgs");
        try {
            if (sgd != null && sgd != undefined) {
                sgd.content.dockedGadget.SetCalendars( EventCalendarContent,
                                                       // Log,
                                                       HolidayCalendarContent);
            }

            if (sgs != null && sgs != undefined) {
                sgs.content.settingGadget.SetCalendarInfo( EventCalendarInfo,
                                                           HolidayCalendarInfo);
            }
        }
        catch (ex) {
        }

        Mode = 0;
        Busy = false;
    }
}

function passwordDecode( str ) {
    if( !str || str == "" ){
        return "";
    }

    var result = "";
    var num_in;
    for(i = 0; i < str.length; i += 2) {
        num_in = parseInt(str.substr(i,[2])) + 23;
        num_in = unescape('%' + num_in.toString(16));
        result += num_in;
    }
    return result;
}


