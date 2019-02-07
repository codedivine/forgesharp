(* Copyright (c) 2019 Rahul Garg
This file is part of the forgesharp project and distributed under the BSD 3-clause license.
Full terms of the license are available in the LICENSE file distributed with the project
*)

open System
open System.Xml.Linq
open System.IO

let NewLine = System.Environment.NewLine

let parseVkTypeStructMembers (vkMember:XElement) = 
    let typename = vkMember.Element(XName.Get("type"))
    let name = vkMember.Element(XName.Get("name"))
    let constString = if vkMember.ToString().Contains("const") then "const " else ""
    let ptrString = if vkMember.ToString().Contains("*") then "* " else ""
    constString + typename.Value + ptrString + " " + name.Value + ";"

let parseVkTypeStruct (vkType:XElement) =
    let sname = vkType.Attribute(XName.Get("name")).Value
    let header = "struct " + sname + "{"
    let body = 
        vkType.Elements(XName.Get("member")) 
        |> Seq.map parseVkTypeStructMembers
        |> Seq.fold (fun s1 s2 -> s1 + NewLine + s2) ""
    let footer = NewLine + "}"
    header + body + footer

let vkParseTypeHandle (vkHandle:XElement) = 
    "handle"

let vkParseTypeEnum (vkType:XElement) = 
    "Type Enum"

let parseVkType (vkType:XElement) = 
    let category = vkType.Attribute(XName.Get("category"))
    let cname = if category = null then "" else category.Value
    match cname with 
    | "struct"  ->  parseVkTypeStruct vkType 
    | "define" -> "Define"
    | "bitmask" -> "Type Bitmask"
    | "include" -> ""
    | "handle" -> vkParseTypeHandle vkType
    | "enum"  -> vkParseTypeEnum vkType
    | "basetype" -> "BaseType"
    | "funcpointer" -> "FuncPointer"
    | "union" -> "Union"
    | "" -> ""
    | _ -> failwith ("Unknown type " + vkType.ToString())

let parseVkApiConstants (vkEnum:XElement) = 
    "APIConstant"

let parseVkEnumEnum (vkEnum:XElement) = 
    "TypedEnum"

let parseVkEnumBitmask (vkEnum:XElement) = 
    "Bitmask"

let parseVkEnum (vkEnum:XElement) = 
    let name = vkEnum.Attribute(XName.Get("name"))
    if name.Value = "API Constants" then parseVkApiConstants vkEnum
    else 
        let enumType = vkEnum.Attribute(XName.Get("type"))
        match enumType.Value with 
            | "enum" -> parseVkEnumEnum vkEnum
            | "bitmask" -> parseVkEnumBitmask vkEnum
            | _ -> failwith "Unknown enum " + name.Value

let parseVkCommand (vkCommand:XElement) = 
    "Command"

let parseVkXml (fname:string) = 
    let reader = XDocument.Load(fname)
    let registry = reader.Element(XName.Get("registry"))
    let vkTypes = 
        registry.Element(XName.Get("types")).Elements(XName.Get("type")) 
        |> List.ofSeq
        |> List.map parseVkType
    let vkEnums = 
        registry.Elements(XName.Get("enums")) 
        |> List.ofSeq
        |> List.map parseVkEnum
    let vkCommands = 
        registry.Element(XName.Get("commands")).Elements(XName.Get("command"))
        |> List.ofSeq 
        |> List.map parseVkCommand
    List.concat [| vkEnums; vkTypes; vkCommands |] 

[<EntryPoint>]
let main argv =
    let outputData = parseVkXml argv.[0]
    use outputFile = new StreamWriter("VkGen.cs")
    for str in outputData do
        outputFile.Write(str)
        if str <> "" then outputFile.WriteLine()
    0 // return an integer exit code
