/* Graphics creation */
$('.monthChoice').on('change', function () {
    let monthId = $('.monthChoice').val();

    $.ajax({
        url: '/Expenses/MonthExpenses',
        method: 'POST',
        data: {
            monthId: monthId
        },
        success: function (data) {
            $('#monthExpensesGraphic').remove();
            $('.monthExpensesGraphic').append('<canvas id="monthExpensesGraphic" style="height: 270px;"></canvas>');

            let ctx = document.getElementById('monthExpensesGraphic').getContext('2d');

            let graphic = new Chart(ctx, {
                type: 'doughnut',
                data: {
                    labels: ['Balance', 'Total expended'],
                    datasets: [
                        {
                            label: 'Total expended',
                            backgroundColor: ["#978462", "#dc3f4e"],
                            data: [(data.salary - data.totalValue), data.totalValue]
                        }
                    ]
                },
                options: {
                    responsive: true,
                    title: {
                        display: true,
                        text: 'Total value expended in month'
                    }
                }
            });
        }

    });

    $.ajax({
        url: '/Expenses/MonthTypes',
        method: 'POST',
        data: {
            monthId: monthId
        },
        success: function (data) {
            $('#monthTypesGraphic').remove();
            $('.monthTypesGraphic').append('<canvas id="monthTypesGraphic" style="height: 270px;"></canvas>');

            let ctx = document.getElementById('monthTypesGraphic').getContext('2d');
            let genColors = GetColors(data.length);

            let graphic = new Chart(ctx, {
                type: 'doughnut',
                data: {
                    labels: GetExpenseTypes(data),
                    datasets: [
                        {
                            label: 'Expenses by type',
                            backgroundColor: genColors,
                            hoverBackgroundColor: genColors,
                            data: GetValues(data)
                        }
                    ]
                },
                options: {
                    responsive: true,
                    title: {
                        display: true,
                        text: 'Total value by type'
                    }
                }
            });
        }

    });
});

function loadMonthData() {
    let monthId = $('.monthChoice').val();

    $.ajax({
        url: '/Expenses/MonthExpenses',
        method: 'POST',
        data: {
            monthId: monthId
        },
        success: function (data) {
            $('#monthExpensesGraphic').remove();
            $('.monthExpensesGraphic').append('<canvas id="monthExpensesGraphic" style="height: 270px;"></canvas>');

            let ctx = document.getElementById('monthExpensesGraphic').getContext('2d');

            let graphic = new Chart(ctx, {
                type: 'doughnut',
                data: {
                    labels: ['Balance', 'Total expended'],
                    datasets: [
                        {
                            label: 'Total expended',
                            backgroundColor: ["#978462", "##dc3f4e"],
                            data: [(data.salary - data.totalValue), data.totalValue]
                        }
                    ]
                },
                options: {
                    responsive: true,
                    title: {
                        display: true,
                        text: 'Total value expended in month'
                    }
                }
            });
        }

    });
}

function loadTypeData() {
    let monthId = $('.monthChoice').val();

    $.ajax({
        url: '/Expenses/MonthTypes',
        method: 'POST',
        data: {
            monthId: monthId
        },
        success: function (data) {
            $('#monthTypesGraphic').remove();
            $('.monthTypesGraphic').append('<canvas id="monthTypesGraphic" style="height: 270px;"></canvas>');

            let ctx = document.getElementById('monthTypesGraphic').getContext('2d');
            let genColors = GetColors(data.length);

            let graphic = new Chart(ctx, {
                type: 'doughnut',
                data: {
                    labels: GetExpenseTypes(data),
                    datasets: [
                        {
                            label: 'Expenses by type',
                            backgroundColor: genColors,
                            hoverBackgroundColor: genColors,
                            data: GetValues(data)
                        }
                    ]
                },
                options: {
                    responsive: true,
                    title: {
                        display: true,
                        text: 'Total value by type'
                    }
                }
            });
        }

    });
}

function GetValues(data) {
    let values = [];
    let size = data.length;
    let index = 0;

    while (index < size) {
        values.push(data[index].values);
        index++;
    }

    return values;
}

function GetExpenseTypes(data) {
    let labels = [];
    let size = data.length;
    let index = 0;

    while (index < size) {
        labels.push(data[index].expenseTypes);
        index++;
    }

    return labels;
}

function GetColors(values) {
    let colors = [];

    while (values > 0) {
        let r = Math.floor(Math.random() * 255);
        let g = Math.floor(Math.random() * 255);
        let b = Math.floor(Math.random() * 255);

        colors.push("rgb(" + r + "," + g + "," + b + ")");

        values--;
    }

    return colors;
}