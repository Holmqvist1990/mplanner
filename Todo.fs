(*
    Copyright (C) 2023  Fredrik Holmqvist

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.`
*)

module Todo

let unfinishedTodo (todo: string) = todo.Contains("[ ]")

let isValidBody (todo: string) = not (todo = "" || todo.Contains("[x]"))

let parseTodo line lines =
    let rec inner todo lines =
        match lines with
        | [] -> todo, []
        | todo_body :: rest ->
            match isValidBody todo_body with
            | true -> inner (todo_body :: todo) rest
            | _ -> todo, rest

    let todo, remaining = inner [ line ] lines
    (List.rev todo), remaining

let parseTodos lines =
    let rec inner lines todos =
        match lines with
        | [] -> todos
        | line :: lines ->
            match unfinishedTodo line with
            | true ->
                let newTodo, remaining = parseTodo line lines
                inner remaining (newTodo :: todos)
            | false -> inner lines todos

    List.rev (inner lines [])
