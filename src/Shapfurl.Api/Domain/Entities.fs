module Shapfurl.Api.Domain.Entities
    open System

    /// A shortened (furled) URL.
    type ShortenedUrl = {
        id: string
        friendlyName: string
        url: string
        createdOn: DateTime
        hits: int
    }

    /// Fetches an URL by a string field
    type FetchUrl = string -> ShortenedUrl

    /// Updates an URL by fetching it and applying new values
    type UpdateUrl = FetchUrl -> ShortenedUrl -> ShortenedUrl

    /// Lists all URLs
    type ListUrls = unit -> ShortenedUrl[]

    /// Stores an URL into the System
    type StoreUrl = ShortenedUrl -> ShortenedUrl

    /// Deletes an URL from the System
    type DeleteUrl = string -> unit