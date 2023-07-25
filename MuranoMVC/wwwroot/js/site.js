// JavaScript-функция для добавления слова в список
function addWord() {

    var inputWord = document.getElementById('inputWord');
    var wordList = document.getElementById('wordList');
    var listItem = document.createElement('li');
    listItem.innerHTML = inputWord.value + ' <button onclick="editWord(\'' + inputWord.value + '\')">Изменить</button>' +
        ' <button onclick="deleteWord(\'' + inputWord.value + '\')">Удалить</button>';
    wordList.appendChild(listItem);
    inputWord.value = '';
}

// JavaScript-функция для удаления слова из списка
function deleteWord(word) {
    var wordList = document.getElementById('wordList');
    var listItems = wordList.getElementsByTagName('li');
    for (var i = 0; i < listItems.length; i++) {
        if (listItems[i].textContent.includes(word)) {
            wordList.removeChild(listItems[i]);
            break;
        }
    }
}

// JavaScript-функция для изменения слова в списке
function editWord(word) {
    var newWord = prompt('Введите новое значение для слова', word);
    if (newWord !== null) {
        var wordList = document.getElementById('wordList');
        var listItems = wordList.getElementsByTagName('li');
        for (var i = 0; i < listItems.length; i++) {
            if (listItems[i].textContent.includes(word)) {
                listItems[i].innerHTML = newWord + ' <button onclick="editWord(\'' + newWord + '\')">Изменить</button>' +
                    ' <button onclick="deleteWord(\'' + newWord + '\')">Удалить</button>';
                break;
            }
        }
    }
}

// JavaScript-функция для отправки слов на сервер
function sendWords() {
    var words = [];
    var wordList = document.getElementById('wordList');
    var listItems = wordList.getElementsByTagName('li');
    for (var i = 0; i < listItems.length; i++) {
        var word = listItems[i].textContent.split(' ')[0];
        words.push(word);
    }

    // Отправляем данные на сервер с помощью fetch API
    fetch('/SendWords', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(words)
    })
        .then(response => response.json())
        .then(data => {
            var searchResultsDiv = document.getElementById('searchResults');
            searchResultsDiv.innerHTML = '';
            if (data != null) {
                data.forEach(result => {
                    var pElement = document.createElement('p');
                    pElement.innerHTML = '<a href="' + result.url + '">' + result.url + '</a><br />' + result.snippet;
                    searchResultsDiv.appendChild(pElement);
                });
            }
        })
        .catch(error => console.error('Ошибка:', error));
}
