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
            $('.monthExpensesGraphic').append('<canvas id="monthExpensesGraphic" style="width: 400px; height: 400px;"></canvas>');

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
                    responsive: false,
                    title: {
                        display: true,
                        text: 'Total value expended in month'
                    }
                }
            });
        }

    });
});

function loadData() {
    let monthId = $('.monthChoice').val();

    $.ajax({
        url: '/Expenses/MonthExpenses',
        method: 'POST',
        data: {
            monthId: 1
        },
        success: function (data) {
            $('#monthExpensesGraphic').remove();
            $('.monthExpensesGraphic').append('<canvas id="monthExpensesGraphic" style="width: 400px; height: 400px;"></canvas>');

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
                    responsive: false,
                    title: {
                        display: true,
                        text: 'Total value expended in month'
                    }
                }
            });
        }

    });
}