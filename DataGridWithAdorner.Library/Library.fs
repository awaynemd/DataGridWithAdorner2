namespace DataGridWithAdorner.Library


open Elmish
open Elmish.WPF

type IInnerRow =
   abstract member Id: int with get
   abstract member CellName: string with get
 

module Cell =

    type Model =
       {Id: int
        CellName: string}

       interface IInnerRow with
            member this.Id with get() = this.Id 
            member this.CellName with get() = this.CellName
   
    let init i j k =
            { Id = i*100 + j*10 + k
              CellName = sprintf "CellName %i  %i  %i" i j k }

    let bindings() = [
        "LastName" |> Binding.oneWay(fun (_, c) -> c.CellName)
        "SelectedLabel" |> Binding.oneWay (fun (b, _) -> if b then " - Selected" else "")
    ]
    
    
        
    

    

    
