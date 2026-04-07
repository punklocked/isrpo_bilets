setInterval(() => {
    const now = new Date();

    const formatTime = (value) => {
        return `${(now.getUTCHours() + value).toString().padStart(2, "0")}:${(now.getMinutes()).toString().padStart(2, "0")}:${(now.getSeconds()).toString().padStart(2, "0")}`;
    }

    document.querySelector('.item_moscow').textContent = formatTime(3); // utc + 3
    document.querySelector('.item_london').textContent = formatTime(0); // utc 0
    document.querySelector('.item_newyork').textContent = formatTime(-4); // utc - 4
    document.querySelector('.item_kalina').textContent = formatTime(2); // utc + 2
    document.querySelector('.item_vladik').textContent = formatTime(10); // utc 10
    document.querySelector('.item_irbit').textContent = formatTime(5); // utc + 5
})