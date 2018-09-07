namespace Samples

open WebSharper
open WebSharper.GoldenLayout
open WebSharper.JavaScript
open WebSharper.JQuery
open WebSharper.UI
open WebSharper.UI.Html
open WebSharper.UI.Html.Elt
open WebSharper.UI.Client

[<Require(typeof<GoldenLayout.Resources.BaseCss>)>]
[<Require(typeof<GoldenLayout.Resources.LightTheme>)>]
[<JavaScript>]
module HelloWorld =

    type State = {Text: string}
        
    let component_ id = 
        ItemFactory.CreateComponent(
            Component(
                componentName = "example",
                ComponentState = {Text = "This is the content of Component " + string id}
            ),
            Item(
                Title = "My " + string id + ". component"
            )
        )
    
    [<SPAEntryPoint>]
    let Main () =
            
        let gl =
            GoldenLayout(
                Layout(
                    Content = [|
                        ItemFactory.CreateRow(
                            Item(
                                Content = (
                                    seq { 1..3 }
                                    |> Seq.map (fun id -> component_ id)
                                    |> Array.ofSeq
                                )
                            )
                        )
                    |]
                ),
                JQuery.Of "#gl-container"
            )
            
        gl.RegisterComponent(
            "example", 
            fun (container, s) ->
                let state = s :?> State
                (h2 [] [text state.Text]).Html
                |> container.GetElement().Html
                |> ignore
        )
        
        gl.Init()
        
        Doc.Empty
        |> Doc.RunAppendById "main"
