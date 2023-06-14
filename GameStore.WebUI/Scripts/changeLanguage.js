var currentLanguage = 'ru'; // Здесь можно установить язык по умолчанию или получить из настроек

function loadLanguageFile(language) {
    var xhr = new XMLHttpRequest();
    xhr.open('GET', `/Language/${language}.json`, true);
    xhr.onreadystatechange = function () {
        if (xhr.readyState === 4 && xhr.status === 200) {
            var translations = JSON.parse(xhr.responseText);
            applyTranslations(translations);
        }
    };
    xhr.send();
}

function applyTranslations(translations) {
    for (var key in translations) {
        var element = document.getElementById(key);
        if (element) {  
            element.innerText = translations[key];
        }
    }
}

function changeLanguage(language) {
    currentLanguage = language;
    loadLanguageFile(currentLanguage);
}

// Инициализация перевода при загрузке страницы
document.addEventListener('DOMContentLoaded', function () {
    loadLanguageFile(currentLanguage);
});