namespace Shapfurl.Api.Controllers

open System
open Microsoft.AspNetCore.Mvc
open Shapfurl.Api.Domain.Entities

[<ApiController>]
[<Route("[controller]")>]
type FurlController () =
    inherit ControllerBase()    

    [<HttpGet>]
    member __.Get() =        
        __.Ok [|
            for index in 1..5 ->
                let tempId = String.Format ("furled-{0}", index)
                {
                    ShortenedUrl.id = tempId
                    friendlyName =  tempId
                    createdOn = DateTime.Now
                    hits = 0
                    url = "https://google.com"
                }
        |] :> IActionResult

    [<HttpGet("{id}")>]
    member __.Detail(id: string): IActionResult =
        __.Ok {
            ShortenedUrl.id = id
            friendlyName =  id
            createdOn = DateTime.Now
            hits = 1
            url = "https://google.com"
        } :> IActionResult

    [<HttpDelete("{id}")>]
    member __.Delete(id: string): IActionResult =
        __.NoContent() :> IActionResult

    [<HttpPut("{id}")>]
    member __.Update(id: string, [<FromBody>] payload: ShortenedUrl): IActionResult = 
        __.NoContent() :> IActionResult

    [<HttpPut()>]
    member __.Create([<FromBody>] payload: ShortenedUrl): IActionResult =
        let result = { payload with id = "a new id approaches" }
        __.Created ("", result) :> IActionResult        
