const btn = document.querySelectorAll('.btn');
const start = document.querySelector('.btn_start');
const result = document.querySelector('.game_container');
const choice = ["⛰️", "✂️", "📄"];

start.addEventListener('click', () => {
    let player = 0; 
    let computer = 0;
    if (player >= 3) {
        alert("Игрок выиграл");
        player = 0; 
        computer = 0;
        return;
    }
    else if (computer >= 3) {
        player = 0; 
        computer = 0;
        
        alert("Бот выиграл");
        return;
    }
    else {
        btn.forEach(item => {
        item.addEventListener('click', () => {
                let randomChoice = choice[Math.floor(Math.random() * choice.length)];
                const combinate = item.textContent + randomChoice;

                switch (combinate) {
                    case "📄✂️":
                    case "✂️📄":
                    case "📄⛰️":
                        result.innerHTML = 
                        `<p>Победа</p>
                        <p>Вы выбрали ${item.textContent}. Компьютер выбрал ${randomChoice}</p>
                        <p>Количество очков:</p>
                        <p>Игрок: ${player}; Бот: ${computer}</p>`;
                        player++;
                        break;
                    case "📄✂️":
                    case "✂️⛰️":
                    case "⛰️📄":
                        result.innerHTML = 
                        `<p>Поражение</p>
                        <p>Вы выбрали ${item.textContent}. Компьютер выбрал ${randomChoice}</p>
                        <p>Количество очков:</p>
                        <p>Игрок: ${player}; Бот: ${computer}</p>`;
                        computer++;
                        break;
                    default: result.innerHTML = 
                        `<p>Ничья</p>
                        <p>Вы выбрали ${item.textContent}. Компьютер выбрал ${randomChoice}</p>
                        <p>Количество очков:</p>
                        <p>Игрок: ${player}; Бот: ${computer}</p>`;
                        break;
                }
            })
        })
        }
    }
)