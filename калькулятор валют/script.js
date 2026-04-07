const btn = document.querySelector('.btn');

const toValue = document.querySelector('#to_value');
const fromValue = document.querySelector('#from_value');

const input = document.querySelector('#input');

const result = document.querySelector('.result');

btn.addEventListener('click', () => {

    if (!parseInt(input.value)) {
        result.textContent = "Некорректные данные";
        return;
    }
    result.textContent = (funcResult(input.value, fromValue.value, toValue.value));
})

// Доллар - 1
// Рубль - 84.84
// Евро - 0.8651
// Юань - 6.89
// Британский фунт - 0.7459

function funcResult(input, from, to) {
    switch (from + "->" + to) {
        case "rub->rub" : return input * 1 + " рублей";
        case "usd->usd" : return input * 1 + " долларов";
        case "eur->eur" : return input * 1 + " евро";
        case "yan->yan" : return input * 1 + " юаней";
        case "funt->funt" : return input * 1 + " фунтов";
        case "usd->rub" : return (input * 84.84).toFixed(2) + " рублей";
        case "usd->eur" : return (input * 0.8651).toFixed(2) + " евро";
        case "usd->yan" : return (input * 6.89).toFixed(2) + " юаней";
        case "usd->funt" : return (input * 0.7459).toFixed(2) + " фунтов";
        case "rub->usd" : return (input / 84.84).toFixed(2) + " долларов";
        case "rub->eur" : return ((input / 84.84) * 0.8651).toFixed(2) + " евро";
        case "rub->yan" : return ((input / 84.84) * 6.89).toFixed(2) + " юаней";
        case "rub->funt" : return ((input / 84.84) * 0.7459).toFixed(2) + " фунтов";
        case "eur->usd" : return (input / 0.8651).toFixed(2) + " долларов";
        case "eur->rub" : return ((input / 0.8651) * 84.84).toFixed(2) + " рублей";
        case "eur->yan" : return ((input / 0.8651) * 6.89).toFixed(2) + " юаней";
        case "eur->funt" : return ((input / 0.8651) * 0.7459).toFixed(2) + " фунтов";
        case "yan->usd" : return (input / 6.89).toFixed(2) + " долларов";
        case "yan->rub" : return ((input / 6.89) * 84.84).toFixed(2) + " рублей";
        case "yan->eur" : return ((input / 6.89) * 0.8651).toFixed(2) + " евро";
        case "yan->funt" : return ((input / 6.89) * 0.7459).toFixed(2) + " фунтов";
        case "funt->usd" : return (input / 0.7459).toFixed(2) + " долларов";
        case "funt->rub" : return ((input / 0.7459) * 84.84).toFixed(2) + " рублей";
        case "funt->eur" : return ((input / 0.7459) * 0.8651).toFixed(2) + " евро";
        case "funt->yan" : return ((input / 0.7459) * 6.89).toFixed(2) + " юаней";

        default : return "Выберите параметры";
    }
}