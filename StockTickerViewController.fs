namespace StockTicker

open Agents
open MonoTouch.Foundation
open MonoTouch.UIKit
open System
open System.Drawing
open Utils
open Steema.TeeChart

[<Register("StockTickerViewController")>]
type StockTickerViewController() = 
    inherit UIViewController()

    override this.ViewDidLoad() = 
        base.ViewDidLoad()

        let chart = new TChart(Frame = RectangleF(0.f, 0.f, 300.f, 300.f))

        let appleLine = new Styles.FastLine(Title = "Apple")
        let msLine = new Styles.FastLine(Title = "Microsoft")
        let googleLine = new Styles.FastLine(Title = "Google")

        chart.Series.Add appleLine |> ignore
        chart.Series.Add msLine |> ignore
        chart.Series.Add googleLine |> ignore

        this.View.AddSubview chart

        DrawAppleLine appleLine 
        DrawMSLine msLine
        DrawGoogleLine googleLine
