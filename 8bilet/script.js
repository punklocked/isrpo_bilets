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

function funcResult(input, from, to) {
    switch (from + "->" + to) {
        case "m->in" : return input + " м = " + (input / 0.0254).toFixed(2) + " дюймов";
        case "in->m" : return input + " дюймов = " + (input * 0.0254).toFixed(2) + " метров";
        case "m->ft" : return input + " метров = " + (input / 0.3048).toFixed(2) + " футов";
        case "ft->m" : return input + " футов = " + (input * 0.3048).toFixed(2) + " метров";
        case "m->yd" : return input + " метров = " + (input / 0.9144).toFixed(2) + " ярдов";
        case "yd->m" : return input + " ярдов = " + (input * 0.9144).toFixed(2) + " метров";
        case "in->ft" : return input + " дюймов = " + ((input * 0.0254) / 0.3048).toFixed(2) + " футов";
        case "ft->in" : return input + " футов = " + ((input * 0.3048) / 0.0254).toFixed(2) + " дюймов";
        case "in->yd" : return input + " дюймов = " + ((input * 0.0254) / 0.9144).toFixed(2) + " ярдов";
        case "yd->in" : return input + " ярдов = " + ((input * 0.9144) / 0.0254).toFixed(2) + " дюймов";
        case "ft->yd" : return input + " футов = " + ((input * 0.3048) / 0.9144).toFixed(2) + " ярдов";
        case "yd->ft" : return input + " ярдов = " + ((input * 0.9144) / 0.3048).toFixed(2) + " футов";
        case "m->m" : return input + " метров = " + input * 1 + " метров";
        case "in->in" : return input + " дюймов = " + input * 1 + " дюймов";
        case "ft->ft" : return input + " футов = " + input * 1 + " футов";
        case "yd->yd" : return input + " ярдов = " + input * 1 + " ярдов";
        default : return "Выберите параметры";
    }
}