// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// site.js

// Define an array to store the words
let wordsArray = [];

// Function to add a word to the array
function addWord() {
    const wordInput = document.getElementById('wordInput');
    const word = wordInput.value.trim();
    if (word !== '') {
        wordsArray.push(word);
        wordInput.value = '';
        refreshWordList();
        enableSendButton();
    }
}

// Function to edit a word in the array
function editWord(index) {
    const newWord = prompt('Enter the new word:');
    if (newWord !== null && newWord.trim() !== '') {
        wordsArray[index] = newWord.trim();
        refreshWordList();
    }
}

// Function to delete a word from the array
function deleteWord(index) {
    wordsArray.splice(index, 1);
    refreshWordList();
    if (wordsArray.length === 0) {
        disableSendButton();
    }
}

// Function to refresh the word list in the HTML
function refreshWordList() {
    const wordListDiv = document.getElementById('wordList');
    wordListDiv.innerHTML = '';

    for (let i = 0; i < wordsArray.length; i++) {
        const word = wordsArray[i];
        const div = document.createElement('div');

        const span = document.createElement('span');
        span.innerText = word;
        div.appendChild(span);

        const editButton = document.createElement('button');
        editButton.innerText = 'Изменить';
        editButton.className = 'btn-outline-primary';
        editButton.onclick = () => editWord(i);
        div.appendChild(editButton);

        const deleteButton = document.createElement('button');
        deleteButton.innerText = 'Удалить';
        deleteButton.className = 'btn-danger';
        deleteButton.onclick = () => deleteWord(i);
        div.appendChild(deleteButton);

        wordListDiv.appendChild(div);
    }
}

// Function to enable the "Отправить" (Send) button
function enableSendButton() {
    const sendButton = document.getElementById('sendButton');
    sendButton.disabled = false;
}

// Function to disable the "Отправить" (Send) button
function disableSendButton() {
    const sendButton = document.getElementById('sendButton');
    sendButton.disabled = true;
}

// Function to send the words array to the controller
function sendWords() {
    const sendButton = document.getElementById('sendButton');
    sendButton.disabled = true; // Disable the button to prevent multiple clicks

    fetch('/Search/SendWords', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(wordsArray)
    })
        .then(response => {
            if (response.ok) {
                wordsArray = []; // Clear the array after successful submission
                refreshWordList();
                disableSendButton();
            } else {
                // Handle the error if needed
                console.error('Failed to send words to the server.');
                enableSendButton();
            }
        })
        .catch(error => {
            console.error('Error while sending words:', error);
            enableSendButton();
        });
}



