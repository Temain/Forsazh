var CrashTypeViewModel = function (app, dataModel) {
    var self = this;

    self.list = ko.observableArray([]);
    self.selectedPage = ko.observable(1);
    self.pageSizes = ko.observableArray([10, 25, 50, 100, 200]);
    self.selectedPageSize = ko.observable(10);
    self.crashTypesCount = ko.observable();
    self.pagesCount = ko.observable();

    self.selectedPageChanged = function (page) {
        if (page > 0 && page <= self.pagesCount()) {
            self.selectedPage(page);
            self.loadCrashTypes();

            window.scrollTo(0, 0); 
        }
    }

    self.pageSizeChanged = function () {
        self.selectedPage(1);
        self.loadCrashTypes();

        window.scrollTo(0, 0);
    };

    Sammy(function () {
        this.get('#crashType', function () {
            app.markLinkAsActive('crashType');
            app.breadcrumb(['Поломки']);

            self.loadCrashTypes();
        });
    });

    self.loadCrashTypes = function () {
        $.ajax({
            method: 'get',
            url: '/api/CrashType',
            data: { page: self.selectedPage(), pageSize: self.selectedPageSize() },
            contentType: "application/json; charset=utf-8",
            headers: {
                'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
            },
            success: function (response) {
                ko.mapping.fromJS(response.items, {}, self.list);
                self.pagesCount(response.pagesCount);
                self.crashTypesCount(response.itemsCount);
                app.view(self);
            }
        });
    }

    self.removeCrashType = function (crashType) {
        $.ajax({
            method: 'delete',
            url: '/api/CrashType/' + crashType.crashTypeId(),
            data: JSON.stringify(ko.toJS(self)),
            contentType: "application/json; charset=utf-8",
            headers: {
                'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
            },
            success: function (response) {
                self.list.remove(crashType);
                showAlert('success', 'Запись успешно удалена.');
            }
        });
    }

    return self;
}

var EditCrashTypeViewModel = function (app, dataModel) {
    var self = this;

    self.crashTypeName = ko.observable().extend({
        required: {
            params: true,
            message: "Необходимо указать наименование."
        }
    });

    self.save = function() {
        var result = ko.validation.group(self, { deep: true });
        if (!self.isValid()) {
            result.showAllMessages(true);

            return false;
        }

        $.ajax({
            method: 'put',
            url: '/api/CrashType/',
            data: JSON.stringify(ko.toJS(self)),
            contentType: "application/json; charset=utf-8",
            headers: {
                'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
            },
            success: function(response) {
                app.navigateToCrashType();
                showAlert('success', 'Изменения успешно сохранены.');
            }
        });
    }

    Sammy(function () {
        this.get('#crashType/:id', function () {
            app.markLinkAsActive('crashType');
            app.returnUrl = '#crashType';

            var id = this.params['id'];
            if (id === 'create') {
                app.breadcrumb(['Поломки', 'Новая']);
                app.view(app.Views.CreateCrashType);
            } else {
                app.breadcrumb(['Поломки', 'Редактирование']);
                $.ajax({
                    method: 'get',
                    url: '/api/CrashType/' + id,
                    contentType: "application/json; charset=utf-8",
                    headers: {
                        'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
                    },
                    success: function (response) {
                        ko.mapping.fromJS(response, {}, self);
                        app.view(self);
                    }
                });
            }
        });
    });
}

var CreateCrashTypeViewModel = function (app, dataModel) {
    var self = this;

    self.crashTypeName = ko.observable().extend({
        required: {
            params: true,
            message: "Необходимо указать наименование."
        }
    });
    self.repairCost = ko.observable();

    self.save = function () {
        var result = ko.validation.group(self, { deep: true });
        if (!self.isValid()) {
            result.showAllMessages(true);

            return false;
        }

        $.ajax({
            method: 'post',
            url: '/api/CrashType/',
            data: JSON.stringify(ko.toJS(self)),
            contentType: "application/json; charset=utf-8",
            headers: {
                'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
            },
            error: function (response) {
                // showAlert('danger', 'Произошла ошибка при добавлении сотрудника. Обратитесь в службу технической поддержки.');
            },
            success: function (response) {
                self.crashTypeName('');
                self.repairCost();

                result.showAllMessages(false);

                app.navigateToCrashType();
                showAlert('success', 'Поломка успешно добавлена.');
            }
        });
    }
}
 
app.addViewModel({
    name: "CrashType",
    bindingMemberName: "crashType",
    factory: CrashTypeViewModel
});

app.addViewModel({
    name: "EditCrashType",
    bindingMemberName: "editCrashType",
    factory: EditCrashTypeViewModel
});

app.addViewModel({
    name: "CreateCrashType",
    bindingMemberName: "createCrashType",
    factory: CreateCrashTypeViewModel
});