const btn = document.querySelector('.btn');

const sum = document.querySelector('#sum');
const prc = document.querySelector('#prc');
const year = document.querySelector('#year');

const monthpay = document.querySelector('#monthpay');
const pay = document.querySelector('#pay');
const overpay = document.querySelector('#overpay');

prc.addEventListener('input', () => {
    document.querySelector('.prc').textContent = prc.value;
})

year.addEventListener('input', () => {
    document.querySelector('.year').textContent = year.value;
})

btn.addEventListener('click', () => {
    let isNumber = /^\d+$/.test(sum.value);

    if (!parseInt(sum.value)) {
        alert('Введено некорректное число');
        return;
    }

    let prcMonth = prc.value / 12 / 100;
    let mon = year.value * 12;

    monthpay.value = (sum.value * (prcMonth * (1 + prcMonth)**mon / ((1 + prcMonth)**mon - 1))).toFixed(4);
    pay.value = monthpay.value * mon;
    overpay.value = pay.value - sum.value;
})