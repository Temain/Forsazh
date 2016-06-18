var EditUserViewModel = function(app, dataModel) {
    var self = this;

    self.lastName = ko.observable().extend({
        required: {
            params: true,
            message: "Необходимо указать фамилию."
        }
    });
    self.firstName = ko.observable().extend({
        required: {
            params: true,
            message: "Необходимо указать имя."
        }
    });

    self.save = function () {
        var result = ko.validation.group(self, { deep: true });
        if (!self.isValid()) {
            result.showAllMessages(true);

            return false;
        }

        $.ajax({
            method: 'put',
            url: '/api/Me/',
            data: JSON.stringify(ko.toJS(self)),
            contentType: "application/json; charset=utf-8",
            headers: {
                'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
            },
            success: function (response) {
                app.navigateToUser();
                showAlert('success', 'Изменения успешно сохранены.');
            }
        });
    }

    Sammy(function () {
        this.get('#profile', function () {
            // app.markLinkAsActive('user');
            app.breadcrumb(['Профиль']);

            $.ajax({
                method: 'get',
                url: '/api/Me/',
                contentType: "application/json; charset=utf-8",
                headers: {
                    'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
                },
                success: function (response) {
                    ko.mapping.fromJS(response, {}, self);
                    app.view(self);
                }
            });
        });
    });
}

app.addViewModel({
    name: "Profile",
    bindingMemberName: "profile",
    factory: EditUserViewModel
});
