var SparePartViewModel = function (app, dataModel) {
    var self = this;

    self.list = ko.observableArray([]);
    self.selectedPage = ko.observable(1);
    self.pageSizes = ko.observableArray([10, 25, 50, 100, 200]);
    self.selectedPageSize = ko.observable(10);
    self.sparePartsCount = ko.observable();
    self.pagesCount = ko.observable();

    self.selectedPageChanged = function (page) {
        if (page > 0 && page <= self.pagesCount()) {
            self.selectedPage(page);
            self.loadSpareParts();

            window.scrollTo(0, 0); 
        }
    }

    self.pageSizeChanged = function () {
        self.selectedPage(1);
        self.loadSpareParts();

        window.scrollTo(0, 0);
    };

    Sammy(function () {
        this.get('#sparePart', function () {
            app.markLinkAsActive('sparePart');

            self.loadSpareParts();
        });
    });

    self.loadSpareParts = function () {
        $.ajax({
            method: 'get',
            url: '/api/SparePart',
            data: { page: self.selectedPage(), pageSize: self.selectedPageSize() },
            contentType: "application/json; charset=utf-8",
            headers: {
                'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
            },
            success: function (response) {
                ko.mapping.fromJS(response.items, {}, self.list);
                self.pagesCount(response.pagesCount);
                self.sparePartsCount(response.itemsCount);
                app.view(self);
            }
        });
    }

    self.removeSparePart = function (sparePart) {
        $.ajax({
            method: 'delete',
            url: '/api/SparePart/' + sparePart.sparePartId(),
            data: JSON.stringify(ko.toJS(self)),
            contentType: "application/json; charset=utf-8",
            headers: {
                'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
            },
            success: function (response) {
                self.list.remove(sparePart);
                showAlert('success', 'Запись успешно удалена.');
            }
        });
    }

    return self;
}

var EditSparePartViewModel = function (app, dataModel) {
    var self = this;

    self.sparePartName = ko.observable().extend({
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
            url: '/api/SparePart/',
            data: JSON.stringify(ko.toJS(self)),
            contentType: "application/json; charset=utf-8",
            headers: {
                'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
            },
            success: function(response) {
                app.navigateToSparePart();
                showAlert('success', 'Изменения успешно сохранены.');
            }
        });
    }

    Sammy(function () {
        this.get('#sparePart/:id', function () {
            app.markLinkAsActive('sparePart');

            var id = this.params['id'];
            if (id === 'create') {
                app.view(app.Views.CreateSparePart);
            } else {
                $.ajax({
                    method: 'get',
                    url: '/api/SparePart/' + id,
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

var CreateSparePartViewModel = function (app, dataModel) {
    var self = this;

    self.sparePartName = ko.observable().extend({
        required: {
            params: true,
            message: "Необходимо указать наименование."
        }
    });
    self.cost = ko.observable();
    self.inStock = ko.observable();

    self.save = function () {
        var result = ko.validation.group(self, { deep: true });
        if (!self.isValid()) {
            result.showAllMessages(true);

            return false;
        }

        $.ajax({
            method: 'post',
            url: '/api/SparePart/',
            data: JSON.stringify(ko.toJS(self)),
            contentType: "application/json; charset=utf-8",
            headers: {
                'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
            },
            error: function (response) {
                // showAlert('danger', 'Произошла ошибка при добавлении сотрудника. Обратитесь в службу технической поддержки.');
            },
            success: function (response) {
                self.sparePartName('');
                self.cost();

                result.showAllMessages(false);

                app.navigateToSparePart();
                showAlert('success', 'Запись успешно добавлена.');
            }
        });
    }
}
 
app.addViewModel({
    name: "SparePart",
    bindingMemberName: "sparePart",
    factory: SparePartViewModel
});

app.addViewModel({
    name: "EditSparePart",
    bindingMemberName: "editSparePart",
    factory: EditSparePartViewModel
});

app.addViewModel({
    name: "CreateSparePart",
    bindingMemberName: "createSparePart",
    factory: CreateSparePartViewModel
});