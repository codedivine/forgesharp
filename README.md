# forgesharp

This project is a .net (aiming mainly at C# and F#) binding for Vulkan graphics API.  
Several things distinguish forgesharp from other projects:

* Main development language is F# but it should be consumable from both C# and F#.
* The implementation is in two parts. First, we have a generator that reads the Khronos vk.xml and generates C# code.  Second, we have a higher level wrapper
over this C-style generated API. 
* In the higher level API, we can sacrifice exposing some parts of Vulkan API (eg: custom CPU memory allocators) to simplify API


This project is a learning/exploratory project, it is in very early stages and not currently suitable for production. 


