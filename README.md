# forgesharp

This project is a .net (aiming mainly at C# and F#) binding for Vulkan graphics API.  
Several things distinguish forgesharp from other projects:

* Main development language is F# but it should be consumable from both C# and F#.
* Mostly hand-written
* Aims to be high-level wrapper over Vulkan.  While a lower-level API that corresponds closely to Vulkan's C API is available, 
it is only intended to be used internally by ForgeSharp.  Application developers should use the high-level wrapper. 
* We can sacrifice exposing some parts of Vulkan API (eg: custom CPU memory allocators) to simplify API


This project is a learning/exploratory project, it is in very early stages and not currently suitable for production. 


