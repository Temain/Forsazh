var ReportViewModel = function (app, dataModel) {
    var self = this;

    Sammy(function () {
        this.get('#reports', function () {
            app.markLinkAsActive('report');
            app.breadcrumb(['Отчёты']);
            var year = 2016;

            $.ajax({
                method: 'get',
                url: '/api/Task/ChartData/2016',
                contentType: "application/json; charset=utf-8",
                headers: {
                    'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
                },
                success: function (response) {
                    app.view(self);
                    self.showChart(response);
                }
            });

            app.view(self);
        });
    });

    //self.showChart = function(data) {
    //    $('#chart').highcharts({
    //        credits: {
    //            enabled : false
    //        },
    //        chart: {
    //            type: 'area'
    //        },
    //        title: {
    //            text: 'Продажи ОАО АТМ-Кубань',
    //            x: 0 //center
    //        },
    //        subtitle: {
    //            text: 'в 2016 году',
    //            x: 0
    //        },
    //        xAxis: {
    //            categories: ['Январь', 'Февраль', 'Март', 'Апрель', 'Май', 'Июнь',
    //                'Июль', 'Август', 'Сентябрь', 'Октябрь', 'Ноябрь', 'Декабрь']
    //        },
    //        yAxis: {
    //            title: {
    //                text: 'Сумма, руб.'
    //            },
    //            plotLines: [{
    //                value: 0,
    //                width: 1,
    //                color: '#808080'
    //            }]
    //        },
    //        tooltip: {
    //            formatter: function () {
    //                return this.x + ': ' + this.point.y + ' руб.';
    //            },
    //            valueSuffix: ' руб.'
    //        },
    //        legend: {
    //            enabled: false
    //        },
    //        series: [{
    //            name: '',
    //            data: data
    //        }]
    //    });
    //}

    self.showChart = function (data) {
        $('#chart').highcharts({
            credits: {
                enabled: false
            },
            chart: {
                type: 'column'
            },
            title: {
                text: 'Динамика выполненных работ'
            },
            subtitle: {
                text: 'в 2015 и 2016 годах'
            },
            xAxis: {
                categories: [
                     'Январь', 'Февраль', 'Март', 'Апрель', 'Май', 'Июнь','Июль', 'Август', 'Сентябрь', 'Октябрь', 'Ноябрь', 'Декабрь'
                ],
                crosshair: true
            },
            yAxis: {
                min: 0,
                title: {
                    text: 'Количество'
                }
            },
            tooltip: {
                headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                    '<td style="padding:0"><b>{point.y}</b></td></tr>',
                footerFormat: '</table>',
                shared: true,
                useHTML: true
            },
            plotOptions: {
                column: {
                    pointPadding: 0.2,
                    borderWidth: 0
                }
            },
            series: data
        });
    }

    return self;
}
 
app.addViewModel({
    name: "Report",
    bindingMemberName: "reports",
    factory: ReportViewModel
});