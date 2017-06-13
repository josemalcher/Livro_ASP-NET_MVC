$(function () {
    app.initialize();

    // Ativar Knockout
    ko.validation.init({ grouping: { observable: false } });
    ko.applyBindings(app);
});
