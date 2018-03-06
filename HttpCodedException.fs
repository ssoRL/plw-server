namespace PLW

open System.Net

exception HttpCodedException of HttpStatusCode * string