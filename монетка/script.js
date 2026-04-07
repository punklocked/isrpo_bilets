const btn = document.querySelector('.btn');
const coin = document.querySelector('.coin');

let i = 0;
let deg = 0;

btn.addEventListener('click', () => {

    deg += 3600;
    i++;
    coin.style.transform = `rotate3d(1, -1, -1, ${deg}deg)`;
    setTimeout(() => {
        coin.children[Math.round(Math.random())].style.zIndex = i;
    }, 250)
})