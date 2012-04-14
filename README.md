# iCal Viewer 2

## Introduction 
iCal Viewer 2 is windows7 sidebar gadget to remind your schedule.
Load your schedule anywhere from internet or local file with
iCalendar format and shows short discription of it.

## Usage
See [iCal Viewer 2 homepage](http://miyu1.github.com/iCalViewer2/)

## How to build
To download source code using git,

      git clone git://github.com/miyu1/iCalViewer2.git

iCal Viewer 2 uses iCalLibrary as submodule,
so don't forget to load the submodule.

      git submodule init
      git submodule update

To build, Microsoft Visual Studio is required.
Visual Studio Express and Visual Web Developer Express
can be downloaded free of charge.

First build WebLoader.exe. 
You can skip this process because WebLoader.exe is already included in
git repository.  
Use Visual Stduio Express and open WebLoader/WebLoader.sln file.
From menu build->build solution, you'll get WebLoader.exe
in WebLoader/bin/Release/ folder.  
Copy it to iCal.Silverlight/iCalGadgetWeb/ folder.

Now build gadget itself.
Use Visual Web Developer Express and open 
iCal.Silverlight/iCal.Silverlight.sln file.
From menu build->build solution then build starts.
If build is success, you'll get iCal2.gadget file in
iCal.Silverlight/iCalGadgetWeb/bin/Publish folder.

## Brief description of code structure

- WebLoader folder is solution for WebLoader.exe  
  WebLoader.exe is .NET framework 4 application to
  load files from internet,
  using http, caldav, webdav and webcal protocol.

- iCalLibrary is submodule to handle iCalendar format file.

- iCal.Silverlight is Silverlight solution to build the gadget.  
  Inside this folder:

  - SilverlightGadgetUtilities folder includes bridge components
    to access sidebar gadget specific information from Silverlight object.

  - iCalLibrary folder includes link to iCalLibrary submodule file.

  - iCalControls folder includes common controls like icons etc.

  - iCalDocked folder is for main Silverlight component

  - iCalFlyout folder is just placeholder, currently
    flyout function is not used.

  - iCalSettings folder is for Silverlight component for Settings window.

  - iCalGadgetWeb folder includes sidebar gadget specific file like gadget.xml

  - iCalTestWeb folder is for test.  
    Using express edition of Visual Studio, it is not possible to debug
    sidebar gadget directly.
    I used this web server to simply debug Silverlight object itself.  
    Select iCalDockedTestPage.html and right-click then select show in browser,
    Visual Studio automatically start web server and the page which includes
    Silverlight object will be displayed.  
    Set default browser to Internet Explorer with the right-click menu.  
    Use iCalSettingsTestPage.html as same.





