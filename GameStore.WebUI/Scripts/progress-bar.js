// progress-bar.js

$(document).ready(function () {
    // Получаем стоимость товара из сессии
    var price = @HttpContext.Session.Get<decimal?>("CartPrice") ?? 0;

    // Вычисляем время заполнения прогресс бара в зависимости от стоимости товара
    var timeInSeconds = price > 200 ? 300 : 60;

        // Устанавливаем интервал обновления прогресс бара
        var interval = setInterval(updateProgressBar, timeInSeconds * 10, timeInSeconds);

        // Функция для обновления прогресс бара
        function updateProgressBar(timeInSeconds) {
        var progressBar = $(".progress-bar");

        // Получаем текущее значение прогресса
        var currentValue = progressBar.attr("aria-valuenow");

        // Увеличиваем текущее значение на шаг
        var newValue = parseInt(currentValue) + (100 / timeInSeconds);

        // Обновляем значение прогресса
        progressBar.attr("aria-valuenow", newValue);
        progressBar.css("width", newValue + "%");

        // Проверяем, достигнут ли максимальный прогресс
        if (newValue >= 100) {
            // Прогресс достигнут, очищаем интервал
            clearInterval(interval);
        }
    }
});
