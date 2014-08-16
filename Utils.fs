namespace StockTicker

open System
open System.Drawing
open Agents
open Steema.TeeChart
open MonoTouch.UIKit

module Utils = 
    let appleData = ResizeArray<float>()
    let msData = ResizeArray<float>()
    let googleData = ResizeArray<float>()

    let rec drawLine stock (stockLine:Styles.FastLine) n (stockPriceAgent:MailboxProcessor<Message>) =
        let stockAsync = stockPriceAgent.PostAndAsyncReply id
        Async.StartWithContinuations(stockAsync, 
             (fun reply -> 
                 printfn "%s %i %f" stock n reply
                 stockLine.Add reply |> ignore
                 do drawLine stock stockLine (n+1) stockPriceAgent),
             (fun _ -> ()),
             (fun _ -> ()))
    
    let DrawAppleLine appleLine = drawLine "apple" appleLine 0 AppleStockPriceAgent
    let DrawMSLine msLine = drawLine "ms" msLine 0 MSStockPriceAgent
    let DrawGoogleLine googleLine = drawLine "google" googleLine 0 GoogleStockPriceAgent
