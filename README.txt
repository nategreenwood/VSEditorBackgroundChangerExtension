/* VSEditorBackgroundChangerExtension v1.0.0.0 [20121018]
 *
 * Author: Nate Greenwood
 *
 * ====================================================================
 *
 * Copyright (C) 2012 by Nate Greenwood (nategreenwood.com, nDev Consulting)
 *
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; either version 2
 * of the License, or any later version.
 *
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 * GNU General Public License for more details.
 *
 *
 * Correspondence and Marketing Questions can be sent to:
 * contact at nategreenwood.com
 *
 */

/*
    Description
    ====================================================================
    The Visual Studio 2012 Text Editor backround changer extension allows
    you to customize the background of the normally boring text editor. 

    By default, when the extension is installed, a predefined image directory
    and default image has been setup. These can be easily changed by modifying
    the config.dat file within the extension's installation directory. The most common
    installation location is:
    C:\Users\<User Name>\AppData\Local\Microsoft\VisualStudio\11.0\Extensions\
    
    Config.dat
    =====================================================================
    This is the primary method for manually adding/changing the background 
    image. By changing the contained parameters, the associated changes
    will appear immediately the next time a new text editor window is opened. 
    There's no need to restart Visual Studio. 

    Config.dat Parameters
    =====================================================================
    img_directory:  This is the location that the extension will seearch 
                    when looking for the image defined in the img_name 
                    property.
                    Default: The default location is [VSIXInstallDirectory]\Images 
                    where [VSIXInstallDirectory] is interpreted by the extension to be 
                    whatever folder the extension was installed into. 
                    Any folder can be listed as long as the proper read/write 
                    permissions are available on the folder.
    img_name:       The specific image name. Only .jpg and .png formats have been 
                    tested, so your mileage may vary.
    opacity:        A 0 - 100 value which sets the images opacity. An opacity of 100
                    will have no opacity. 
                    Default: The default opactiy is set at 35. This seems to be a
                    good blend to allow text to flow over the image without much 
                    interference
    location:       There are 5 discrete position the image can be put. They are, 
                    TopLeft, TopRight, BottomLeft, BottomRight and Center. 
                    Default: The default watermark location is BottomRight.
    fill:           NOTE: This setting is still a little iffy. As of this version, 
                    there are 4 discrete values that can be set, all corresponding 
                    to the System.Windows.FrameworkElement's Stretch property. 
                    None, Uniform, Fill, UniformToFill. 
                    Default: The default value is None.

    Planned Features
    ========================================================================
    There is only one more main feature being worked on. That is an IDE integrated
    Window to allow setting the changes right from within Visual Studio as working 
    with config files in some obscure folder is a pain, amirite?

*/
 