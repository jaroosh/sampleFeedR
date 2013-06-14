// Feed view model.
function Feed(id, title, source, description, date) {
    this.Id = ko.observable(id);
    this.Title = ko.observable(title);
    this.Source = ko.observable(source);
    this.Description = ko.observable(description);
    this.Date = ko.observable(date);
}

