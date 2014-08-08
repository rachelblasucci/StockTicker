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

        let appleLine = new Styles.FastLine()
        let msLine = new Styles.FastLine()
        let googleLine = new Styles.FastLine()

        chart.Series.Add appleLine |> ignore
        chart.Series.Add msLine |> ignore
        chart.Series.Add googleLine |> ignore

        this.View.AddSubview chart
        DrawAppleLine 5.00 appleLine 0
        DrawMSLine 3.00 msLine 0
        DrawGoogleLine 4.00 googleLine 0

