namespace StockTicker

open Agents
open MonoTouch.Foundation
open MonoTouch.UIKit
open System
open System.Drawing
open Steema.TeeChart

[<Register("StockTickerViewController")>]
type StockTickerViewController() = 
    inherit UIViewController()

    let appleLine = new Styles.FastLine(Title = "Apple")
    let msLine = new Styles.FastLine(Title = "Microsoft")
    let googleLine = new Styles.FastLine(Title = "Google")

    let mutable AgentState = Stop

    let rec DrawLine n agentState stock (stockLine:Styles.FastLine) (stockPriceAgent:MailboxProcessor<Message>) =
        match agentState with 
        | Start -> 
            let stockAsync = stockPriceAgent.PostAndAsyncReply id
            Async.StartWithContinuations(stockAsync, 
                 (fun reply -> 
                     printfn "%s %i %f" stock n reply
                     stockLine.Add reply |> ignore
                     do DrawLine (n+1) AgentState stock stockLine stockPriceAgent),
                 (fun _ -> ()),
                 (fun _ -> ())) 
        | Stop -> ()

    let StartStopSlider = 
        let s = new UISegmentedControl(RectangleF(85.f, 350.f, 150.f, 50.f))
        s.InsertSegment("Start", 0, false)
        s.InsertSegment("Stop", 1, false)
        s.SelectedSegment <- 1

        let HandleSegmentChanged = 
            new EventHandler(fun sender _ -> 
                let s = sender :?> UISegmentedControl
                match s.SelectedSegment with 
                    | 0 -> AgentState <- Start
                    | 1 -> AgentState <- Stop
                    | _ -> failwith "No such segment!"

                DrawLine 0 AgentState "apple" appleLine AppleStockPriceAgent
                DrawLine 0 AgentState "ms" msLine MSStockPriceAgent
                DrawLine 0 AgentState "google" googleLine GoogleStockPriceAgent
            )
        s.ValueChanged.AddHandler HandleSegmentChanged
        s

    override this.ViewDidLoad() = 
        base.ViewDidLoad()

        let stockView = new UIView(this.View.Bounds, BackgroundColor = UIColor.White)
        let chart = new TChart(Frame = RectangleF(10.f, 10.f, 300.f, 300.f))

        chart.Series.Add appleLine |> ignore
        chart.Series.Add msLine |> ignore
        chart.Series.Add googleLine |> ignore

        stockView.AddSubview chart
        stockView.AddSubview StartStopSlider
        this.View.AddSubview stockView
