// Single feed..
function Feed(id, title, description, date) {
    this.Id = ko.observable(id);
    this.Title = ko.observable(title);
    this.Description = ko.observable(description);
    this.Date = ko.observable(date);
}

// Feeds view model to be used for knockout.
function FeedsViewModel() {
    this.feeds = ko.observableArray([]); // feeds.
}
