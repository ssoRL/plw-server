namespace PLW.Models

open System

type Thread = { 
    Id: int64;
    Name: string;
    LastUpdate: DateTime;
    Messages: Message list;
}