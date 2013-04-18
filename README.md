# LessMVCFour

LESS BundleTransform, HttpHandler for integrating with ASP.NET MVC 4.

I suffered too long when trying to integrate dotless into my MVC 4 projects, dotless works fine with a simple less file, but when that file contains @import directive then the file missing error came.
I googled the solutions and it needs quite a piece of code to solve the problem, almost rewriting the process of creating dotless engine.

Because of that, I created this library for later reuse and now share it with you. There are still many limitations that I have not overcome, working hard to improve it.

## Usage

#### Install LessMVCFour into your project

Open Package Manager Console in Visual Studio to ensure that the Default Project is set to your target MVC 4 project and execute the following command

    Install-Package LessMVCFour
    
#### Changes in web.config

Open your web.config you will see the following changes

    
    ...
    <system.web>
      ...
      <httpHandlers>
        <add path="*.less" verb="GET" type="dotless.Core.LessCssHttpHandler, dotless.Core" />
        <remove path="*.less" verb="GET" />
        <add path="*.less" verb="GET" type="LessMVCFour.LessHttpHandler, LessMVCFour" />
      </httpHandlers>
    </system.web>
    <system.webServer>
      <handlers>
        ...
        <add name="dotless" path="*.less" verb="GET" type="dotless.Core.LessCssHttpHandler,dotless.Core" resourceType="File" preCondition="" />
        <remove name="dotless" />
        <add name="dotless" path="*.less" verb="GET" type="LessMVCFour.LessHttpHandler, LessMVCFour" resourceType="File" preCondition="" />
      </handlers>
    </system.webServer>
    ...
    
    
dotless registers its handlers to the web.config and due to the limitation of NuGet config transformation (it can only add more elements), LessMVCFour adds a &lt;remove&gt; tag to remove dotless's handler first and register LessMVCFour's handler.

#### Using BundleTransform

Open App_Start/BundleConfig.cs and add a new bundle into RegisterBundles method, e.g

    ...
    bundles.Add(new Bundle("~/Content/less",
                        new WebLessBundleTransform())
                        .Include("~/Content/main.less"));
    ...
    
The main.less file is put in ~/Content folder, the bundle's virtual path is **~/Content/less**

**Note** : **All the included files should be in the same folder of the virtual path**
E.g :

    ...
    bundles.Add(new Bundle("~/Content/less",
                        new WebLessBundleTransform())
                        .Include("~/Content/main.less", "~/Content/second.less"));
    ...
    
is good BUT

    ...
    bundles.Add(new Bundle("~/Content/less",
                        new WebLessBundleTransform())
                        .Include("~/Content/lessfiles/main.less"));
    ...
    
or

    ...
    bundles.Add(new Bundle("~/Content/less",
                        new WebLessBundleTransform())
                        .Include("~/Content/main.less", "~/Content/lessfiles/second.less));
    ...
    
Will give you error. I am researching to solve this problem. If you have any solution please send me email at **minhkhoa4783@gmail.com**, many thanks :).

**Note** : **Also remember that don't have virtual path conflict with your folders**. e.g "~/Content/less" will confict with Content/less folder, give it another virtual path like "~/Content/lessstyle" is good.

#### Using LESS parser

If your LESS content does not include @import directive

    var parsedContent = WebLessParser.Parse(lessContent, null);
    
In case, it contains @import directives.

    var parsedContent = WebLessParser.Parse(lessContent, "<path to the file which stored the LESS content>");

#### Limitations

These notes above.

This library is just for using with Web applications.

The package now is just for .NET 4.5
