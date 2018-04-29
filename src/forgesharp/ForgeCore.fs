(*
Copyright (c) 2018, Rahul Garg
This file is part of forgesharp project
License terms are provided in LICENSE file provided with forgesharp
*)

namespace forgesharp

open System.Runtime.InteropServices
open System
open System.Drawing
open System.Drawing

[<Struct>]
[<StructLayout(LayoutKind.Sequential,CharSet=CharSet.Ansi)>]
type VkAppplicationInfo = 
    val sType: int32
    val pNext:  IntPtr
    val applicationName: string
    val applicationVersion: int32
    val engineName: string
    val engineVersion:  int32
    val apiVersion:  int32
    new(aname,_applicationVersion,_engineName,eversion,api) = {
        sType = 0
        pNext = IntPtr.Zero
        applicationName = aname
        applicationVersion = _applicationVersion
        engineName = _engineName
        engineVersion = eversion
        apiVersion = api
    };

      
[<Struct>]
[<StructLayout(LayoutKind.Sequential,CharSet=CharSet.Ansi)>]
type VkInstanceCreateInfo = 
    val sType: int32
    val pNext: IntPtr
    val flags:  int32
    val applicationInfo:  ref<VkAppplicationInfo>
    val enabledLayerCount: int32
    val enabledLayerNames: string[]
    val enabledExtensionCount: int32
    val enabledExtensionNames: string[]
    new(inApplicationInfo,inEnabledLayerNames,inEnabledExtensionNames) = {
        sType = 0
        pNext = IntPtr.Zero
        flags = 0
        applicationInfo = inApplicationInfo
        enabledLayerNames = inEnabledLayerNames
        enabledExtensionNames = inEnabledExtensionNames
        enabledLayerCount = inEnabledLayerNames.Length
        enabledExtensionCount = inEnabledExtensionNames.Length
    }



module ForgeCore =
    [<DllImport("vulkan-1")>]
    extern int32 vkCreateInstance([<In>] VkInstanceCreateInfo& createInfo, IntPtr allocationCallback, IntPtr instance)
 

