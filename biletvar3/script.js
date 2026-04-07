const theme = document.querySelector('.change_theme');
let state = true;

theme.addEventListener('click', () => {

    if (state) {
        theme.children[0].src = 'images/light-icon.png';
        document.querySelector('link').href = "dark.css"
    }
    else {
        theme.children[0].src = 'images/dark-icon.png';
        document.querySelector('link').href = "light.css"
    }
       

    state = !state;
})