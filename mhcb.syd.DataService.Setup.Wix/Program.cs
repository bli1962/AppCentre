using System;
using System.Windows.Forms;
using WixSharp;
using WixSharp.Forms;

namespace WixSharpSetup
{
    class Program
    {
        static void Main()
        {

#if PROD
    var env = mhcb.syd.Wix.Projects.Environments.PROD;              
#elif UAT
    var env = mhcb.syd.Wix.Projects.Environments.UAT;   
#else
    var env = mhcb.syd.Wix.Projects.Environments.DEBUG;    
#endif

            WixSharp.Compiler.WixLocation = @"..\packages\WiX.3.11.0\tools";
            mhcb.syd.Wix.Projects.WebSite(
               "mhcb.syd.DataService",
               env,
               new Guid("BAB15880-AE1F-40C1-B6CC-3F3469BAFD83"),
               "DataCenter",
               "DataCenter",
               @"D:\MizuhoApps\WebApp\DataCenter",
               true,
               new DirFiles(@"..\mhcb.syd.DataService\*.html"),

               new Dir("bin", new Files(@"..\mhcb.syd.DataService\bin\*.*")),
               new Dir("Content", new Files(@"..\mhcb.syd.DataService\*.*")),
               new Dir("fonts", new Files(@"..\mhcb.syd.DataService\fonts\*.*")),
               new Dir("Images", new Files(@"..\mhcb.syd.DataService\images\*.*")),
               new Dir("Scripts", new Files(@"..\mhcb.syd.DataService\*.*")),
               new Dir("Src", new Files(@"..\mhcb.syd.DataService\src\*.*")),
               new Dir("Views", new Files(@"..\mhcb.syd.DataService\Views\*.*"))
               ).BuildMsi();
        }
    }
}


//Install-Package C:\TFS\Trunk\Source\MizuhoNuget\mhcb.syd.Wix.1.0.0.20.nupkg  -ProjectName mhcb.syd.DataService.Setup.Wix

//Building MSI:
//After building the project the corresponding.msi file can be found in the root project folder.

//Tips and Hints:
//If you are implementing managed CA you may want to set "Target Framework" to "v3.5" as the lower CLR version will help avoid potential conflicts during the installation (e.g.target system has .NET v3.5 only).

//Note: 
//Wix# requires WiX Toolset (tools and binaries) to function properly. Wix# is capable of automatically finding WiX tools only if WiX Toolset installed. In all other cases you need to set the environment variable WIXSHARP_WIXDIR or WixSharp.Compiler.WixLocation to the valid path to the WiX binaries.

//WiX binaries can be brought to the build environment by either installing WiX Toolset, downloading Wix# suite or by adding WixSharp.wix.bin NuGet package to your project.
                                            
//Because of the excessive size of the WiX Toolset the WixSharp.wix.bin NuGet package isn't a direct dependency of the WixSharp package and it needs to be added to the project explicitly:

//Compiler.WixLocation = @"..\packages\WixSharp.wix.bin.<version>\bin";