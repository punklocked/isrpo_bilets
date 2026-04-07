const lang = document.querySelector('.lang');
let state = 0;

const textRus = `<div class="up">
            <div class="logo">ЮристПро</div>
            <nav class="menu_nav">
                <a href="#">Юристы</a>
                <a href="#">Услуги</a>
                <a href="#">Консультации</a>
            </nav>
            <div class="menu_link">
                <a href="#"><img src="img/MAX.png" alt="max"></a>
                <a href="#"><img src="img/vk.png" alt="vk"></a>
                <a href="#"><img src="img/ok.png" alt="ok"></a>
            </div>
        </div>
        <div class="content">
            <div class="content_text">
                <p class="content_slogan">Выбор лучших компаний</p>
                <h1 class="content_title">Ваш <span>надёжный</span> партнёр в мире бизнеса</h1>
                <p class="content_desc">Классные юридические решения для роста и защиты вашего бизнеса - от стартапов до крупных корпораций</p>
            </div>
            <form action="#" class="content_form">
                <h2 class="form_title">Запишитесь на <span>консультацию</span> прямо сейчас</h2>
                <div class="form_item">
                    <input type="text" placeholder="Введите Ваше ФИО">
                    <input type="text" placeholder="Введите Ваш номер телефона">
                    <input type="text" placeholder="Введите Ваш электронный адрес">
                </div>
                <button class="form_btn">Записаться</button>
            </form>
        </div>`;

const textEng = `<div class="up">
            <div class="logo">UristPro</div>
            <nav class="menu_nav">
                <a href="#">Lawyers</a>
                <a href="#">Services</a>
                <a href="#">Consultations</a>
            </nav>
            <div class="menu_link">
                <a href="#"><img src="img/MAX.png" alt="max"></a>
                <a href="#"><img src="img/vk.png" alt="vk"></a>
                <a href="#"><img src="img/ok.png" alt="ok"></a>
            </div>
        </div>
        <div class="content">
            <div class="content_text">
                <p class="content_slogan">Best companies chosen</p>
                <h1 class="content_title">Your <span>reliable</span> partner in business world</h1>
                <p class="content_desc">Best law tactics for defending your business - from startups to big corporations</p>
            </div>
            <form action="#" class="content_form">
                <h2 class="form_title">Sign up to a <span>consultation</span> right now</h2>
                <div class="form_item">
                    <input type="text" placeholder="Enter your credentials">
                    <input type="text" placeholder="Enter your phone number">
                    <input type="text" placeholder="Enter your email">
                </div>
                <button class="form_btn">Sign up</button>
            </form>
        </div>`;

const textFr = `<div class="up">
            <div class="logo">UristPro</div>
            <nav class="menu_nav">
                <a href="#">Avocats</a>
                <a href="#">Services</a>
                <a href="#">Consultations</a>
            </nav>
            <div class="menu_link">
                <a href="#"><img src="img/MAX.png" alt="max"></a>
                <a href="#"><img src="img/vk.png" alt="vk"></a>
                <a href="#"><img src="img/ok.png" alt="ok"></a>
            </div>
        </div>
        <div class="content">
            <div class="content_text">
                <p class="content_slogan">Les meilleures entreprises ont été sélectionnées.</p>
                <h1 class="content_title">Votre partenaire de <span>confiance</span> dans le monde des affaires</h1>
                <p class="content_desc">Meilleures pratiques pour protéger votre entreprise – Des startups aux grandes entreprises</p>
            </div>
            <form action="#" class="content_form">
                <h2 class="form_title">Réservez une <span>consultation</span> dès maintenant</h2>
                <div class="form_item">
                    <input type="text" placeholder="Veuillez saisir votre nom complet">
                    <input type="text" placeholder="Saisissez votre numéro de téléphone">
                    <input type="text" placeholder="Saisissez votre adresse e-mail">
                </div>
                <button class="form_btn">S'inscrire</button>
            </form>
        </div>`;

lang.addEventListener('click', () => {

    if (state == 0) {
        lang.children[0].src = 'img/fr.png';
        document.querySelector('main').innerHTML = textEng;
        state++;
    }
    else if (state == 1) {
        lang.children[0].src = 'img/rus.png';
        document.querySelector('main').innerHTML = textFr;
        state++;
    }
    else {
        lang.children[0].src = 'img/grb.png';
        document.querySelector('main').innerHTML = textRus;
        state = 0; 
    }
})