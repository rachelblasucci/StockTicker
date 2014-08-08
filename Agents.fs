namespace StockTicker

open System
open System.Threading

type Agent<'T> = MailboxProcessor<'T>
type Message = float * AsyncReplyChannel<float>

module Agents = 
    let rand = new Random()
    let GenerateApplePrices = MailboxProcessor<Message>.Start(fun inbox ->
        let rec loop () =
            async {
                    do! Async.Sleep(1500)
                    let! (message, replyChannel) = inbox.Receive();
                    let next = rand.NextDouble()
                    let sign = if rand.Next()%2-1 = 0 then 1. else -1.
                    let revalue last = last + sign * next
                    replyChannel.Reply(revalue message)
                    do! loop ()
            } 
        loop ())

    let GenerateMSPrices = MailboxProcessor<Message>.Start(fun inbox ->
        let rec loop () =
            async {
                    do! Async.Sleep(2000)
                    let! (message, replyChannel) = inbox.Receive();
                    let next = rand.NextDouble()
                    let sign = if rand.Next()%2-1 = 0 then 1. else -1.
                    let revalue last = last + sign * next
                    replyChannel.Reply(revalue message)
                    do! loop ()
            }
        loop ())

    let GenerateGooglePrices = MailboxProcessor<Message>.Start(fun inbox ->
        let rec loop () =
            async {
                    do! Async.Sleep(2500)
                    let! (message, replyChannel) = inbox.Receive();
                    let next = rand.NextDouble()
                    let sign = if rand.Next()%2-1 = 0 then 1. else -1.
                    let revalue last = last + sign * next
                    replyChannel.Reply(revalue message)
                    do! loop ()
            }
        loop ())
