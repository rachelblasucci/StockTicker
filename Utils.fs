namespace StockTicker

open System
open System.Drawing
open Agents
open Steema.TeeChart
open MonoTouch.UIKit

module Utils = 
    let appleData = new ResizeArray<float>()
    let msData = new ResizeArray<float>()
    let googleData = new ResizeArray<float>()

    let rec DrawAppleLine price (appleLine:Styles.FastLine) n =
        let appleAsync = GenerateApplePrices.PostAndAsyncReply(fun replyChannel -> price, replyChannel)
        Async.StartWithContinuations(appleAsync, 
             (fun reply -> 
                 printfn "apple %i %f" n reply
                 appleLine.Add reply |> ignore
                 do DrawAppleLine reply appleLine (n+1)),
             (fun _ -> ()),
             (fun _ -> ()))

    let rec DrawMSLine price (msLine:Styles.FastLine) n =
        let msAsync = GenerateMSPrices.PostAndAsyncReply(fun replyChannel -> price, replyChannel)
        Async.StartWithContinuations(msAsync, 
             (fun reply -> 
                 printfn "ms %i %f" n reply
                 msLine.Add reply |> ignore
                 do DrawMSLine reply msLine (n+1)),
             (fun _ -> ()),
             (fun _ -> ()))

    let rec DrawGoogleLine price (googleLine:Styles.FastLine) n =
        let googleAsync = GenerateGooglePrices.PostAndAsyncReply(fun replyChannel -> price, replyChannel)
        Async.StartWithContinuations(googleAsync, 
             (fun reply -> 
                 printfn "google %i %f" n reply
                 googleLine.Add reply |> ignore
                 do DrawGoogleLine reply googleLine (n+1)),
             (fun _ -> ()),
             (fun _ -> ()))

